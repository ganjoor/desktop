using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ganjoor
{
    /// <summary>
    /// نتیجهٔ وارد کردن فایل متنی همگام‌سازی تولیدشده توسط هوش مصنوعی
    /// </summary>
    public class AiSyncImportResult
    {
        /// <summary>
        /// آرایهٔ همگام‌سازی ساخته شده (بر اساس ترتیب مصرعهای شعر، نه ترتیب سطرهای فایل ورودی)
        /// </summary>
        public List<PoemAudio.SyncInfo> SyncArray = new List<PoemAudio.SyncInfo>();

        /// <summary>
        /// سطرهایی از فایل ورودی که با هیچ مصرعی از شعر مطابقت داده نشدند
        /// </summary>
        public List<string> UnmatchedLines = new List<string>();

        /// <summary>
        /// مصرعهایی از شعر که در فایل ورودی معادلی برایشان یافت نشد
        /// </summary>
        public List<GanjoorVerse> UnmatchedVerses = new List<GanjoorVerse>();

        /// <summary>
        /// جفتهایی که فقط با تطابق تقریبی (نه دقیق) به هم متصل شدند و بهتر است بازبینی شوند
        /// </summary>
        public List<string> ApproximateMatches = new List<string>();

        /// <summary>
        /// تعداد کل سطرهای زمان‌دار معتبری که در فایل ورودی خوانده شد
        /// </summary>
        public int TotalParsedLines;

        /// <summary>
        /// اگر true باشد یعنی پس از تطبیق و مرتب‌سازی بر اساس ترتیب شعر، ترتیب زمانها با
        /// صعودی بودن مورد انتظار همخوانی ندارد (نشانهٔ احتمالی یک تطبیق نادرست)
        /// </summary>
        public bool HasOutOfOrderTimes;
    }

    /// <summary>
    /// وارد کردن خروجی متنی ابزارهای همگام‌سازی خودکار (هوش مصنوعی) که برای هر سطر
    /// یک زمان و متن مصرع می‌دهند؛ نمونه:
    /// 03:11  آن که از سنبل او غالیه تابی دارد
    /// 04:57  باز با دلشدگان ناز و عتابی دارد
    /// چون ترتیب سطرهای چنین خروجی‌ای ممکن است دقیقاً با ترتیب مصرعهای شعر یکسان نباشد،
    /// تطبیق بر اساس متن مصرع انجام می‌شود، نه بر اساس شمارهٔ سطر.
    /// </summary>
    public static class AiSyncTextImporter
    {
        private class ParsedLine
        {
            public int AudioMiliseconds;
            public string RawText;
        }

        // 03:11 text  |  1:03:11 text  |  03:11.500 text  |  03:11,500 text
        private static readonly Regex TimedLineRegex = new Regex(
            @"^\s*(?:(\d+):)?(\d{1,2}):(\d{2})(?:[.,](\d{1,3}))?\s+(.+?)\s*$",
            RegexOptions.Compiled);

        // حداقل شباهت قابل قبول برای تطبیق تقریبی (وقتی تطبیق دقیق پیدا نشود)
        private const double ApproximateMatchThreshold = 0.72;

        /// <summary>
        /// خواندن فایل و تطبیق سطرهای آن با مصرعهای شعر
        /// </summary>
        /// <param name="filePath">مسیر فایل متنی خروجی همگام‌سازی هوش مصنوعی</param>
        /// <param name="poemVerses">مصرعهای شعر (به هر ترتیبی، مرتب می‌شوند)</param>
        public static AiSyncImportResult Import(string filePath, GanjoorVerse[] poemVerses)
        {
            string[] rawLines = File.ReadAllLines(filePath, Encoding.UTF8);
            var parsedLines = new List<ParsedLine>();
            foreach (string rawLine in rawLines)
            {
                if (string.IsNullOrWhiteSpace(rawLine))
                    continue; // خطوط خالی صرفاً جداکنندهٔ بیتها هستند

                Match m = TimedLineRegex.Match(rawLine);
                if (!m.Success)
                    continue; // سطرهایی که با الگوی «زمان + متن» مطابقت ندارند نادیده گرفته می‌شوند

                int hours = m.Groups[1].Success ? int.Parse(m.Groups[1].Value) : 0;
                int minutes = int.Parse(m.Groups[2].Value);
                int seconds = int.Parse(m.Groups[3].Value);
                int millis = m.Groups[4].Success ? int.Parse(m.Groups[4].Value.PadRight(3, '0').Substring(0, 3)) : 0;
                int totalMiliseconds = ((hours * 60 + minutes) * 60 + seconds) * 1000 + millis;

                parsedLines.Add(new ParsedLine
                {
                    AudioMiliseconds = totalMiliseconds,
                    RawText = m.Groups[5].Value.Trim()
                });
            }

            var result = new AiSyncImportResult
            {
                TotalParsedLines = parsedLines.Count
            };

            var orderedVerses = poemVerses.OrderBy(v => v._Order).ToArray();

            // متن نرمال‌شدهٔ هر مصرع -> فهرست مصرعهای دارای همان متن (برای مصرعهای تکراری/ترجیع‌بند)
            var verseGroups = orderedVerses
                .GroupBy(v => NormalizeForMatch(v._Text))
                .ToDictionary(g => g.Key, g => g.ToList());

            var usedVerseOrders = new HashSet<int>();
            var matchedMiliseconds = new Dictionary<int, int>(); // verse._Order -> AudioMiliseconds
            int lastMatchedOrder = -1;

            foreach (ParsedLine line in parsedLines)
            {
                string normalizedLine = NormalizeForMatch(line.RawText);
                if (normalizedLine.Length == 0)
                    continue;

                GanjoorVerse matchedVerse = null;

                if (verseGroups.TryGetValue(normalizedLine, out List<GanjoorVerse> candidates))
                {
                    // در صورت تکرار یک مصرع در شعر، اولویت با نزدیکترین مصرع استفاده‌نشده
                    // بعد از آخرین مصرع تطبیق‌یافته است (چون خروجی هوش مصنوعی معمولاً کلی
                    // در جهت شعر پیش می‌رود، هرچند دقیقاً مرتب نباشد)
                    matchedVerse = candidates.FirstOrDefault(v => v._Order >= lastMatchedOrder && !usedVerseOrders.Contains(v._Order))
                                   ?? candidates.FirstOrDefault(v => !usedVerseOrders.Contains(v._Order));
                }

                bool approximate = false;
                if (matchedVerse == null)
                {
                    matchedVerse = FindBestApproximateMatch(normalizedLine, orderedVerses, usedVerseOrders, out double score);
                    if (matchedVerse != null && score >= ApproximateMatchThreshold)
                    {
                        approximate = true;
                    }
                    else
                    {
                        matchedVerse = null;
                    }
                }

                if (matchedVerse == null)
                {
                    result.UnmatchedLines.Add(line.RawText);
                    continue;
                }

                usedVerseOrders.Add(matchedVerse._Order);
                matchedMiliseconds[matchedVerse._Order] = line.AudioMiliseconds;
                lastMatchedOrder = matchedVerse._Order;

                if (approximate)
                {
                    result.ApproximateMatches.Add(string.Format("«{0}» → «{1}»", line.RawText, matchedVerse._Text));
                }
            }

            int previousMiliseconds = -1;
            foreach (GanjoorVerse verse in orderedVerses)
            {
                if (matchedMiliseconds.TryGetValue(verse._Order, out int miliseconds))
                {
                    if (miliseconds < previousMiliseconds)
                    {
                        result.HasOutOfOrderTimes = true;
                    }
                    previousMiliseconds = miliseconds;

                    result.SyncArray.Add(new PoemAudio.SyncInfo
                    {
                        VerseOrder = verse._Order - 1,
                        AudioMiliseconds = miliseconds,
                        VerseText = verse._Text
                    });
                }
                else
                {
                    result.UnmatchedVerses.Add(verse);
                }
            }

            return result;
        }

        private static GanjoorVerse FindBestApproximateMatch(string normalizedLine, GanjoorVerse[] orderedVerses, HashSet<int> usedVerseOrders, out double bestScore)
        {
            GanjoorVerse best = null;
            bestScore = 0;
            foreach (GanjoorVerse verse in orderedVerses)
            {
                if (usedVerseOrders.Contains(verse._Order))
                    continue;

                double score = SimilarityRatio(normalizedLine, NormalizeForMatch(verse._Text));
                if (score > bestScore)
                {
                    bestScore = score;
                    best = verse;
                }
            }
            return best;
        }

        /// <summary>
        /// یکسان‌سازی متن برای مقایسه: تبدیل حروف عربی به فارسی، حذف اعراب و نیم‌فاصله،
        /// حذف علائم نگارشی و یکدست کردن فاصله‌ها
        /// </summary>
        public static string NormalizeForMatch(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            string s = GPersianTextSync.Sync(text);

            var sb = new StringBuilder(s.Length);
            foreach (char c in s)
            {
                if (Diacritics.IndexOf(c) >= 0)
                    continue; // اعراب نادیده گرفته می‌شود
                if (c == '\u200c') // نیم‌فاصله -> فاصلهٔ معمولی
                {
                    sb.Append(' ');
                    continue;
                }
                if (Punctuation.IndexOf(c) >= 0)
                    continue; // علائم نگارشی نادیده گرفته می‌شود
                sb.Append(c);
            }

            return Regex.Replace(sb.ToString(), @"\s+", " ").Trim();
        }

        private const string Diacritics = "ًٌٍَُِّْـ";
        private const string Punctuation = "،؛؟!.,;:\"'«»()[]{}…–—-";

        private static double SimilarityRatio(string a, string b)
        {
            if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))
                return 0;
            int distance = LevenshteinDistance(a, b);
            int maxLen = Math.Max(a.Length, b.Length);
            if (maxLen == 0)
                return 1;
            return 1.0 - (double)distance / maxLen;
        }

        private static int LevenshteinDistance(string a, string b)
        {
            int[,] d = new int[a.Length + 1, b.Length + 1];
            for (int i = 0; i <= a.Length; i++) d[i, 0] = i;
            for (int j = 0; j <= b.Length; j++) d[0, j] = j;

            for (int i = 1; i <= a.Length; i++)
            {
                for (int j = 1; j <= b.Length; j++)
                {
                    int cost = a[i - 1] == b[j - 1] ? 0 : 1;
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }

            return d[a.Length, b.Length];
        }
    }
}
