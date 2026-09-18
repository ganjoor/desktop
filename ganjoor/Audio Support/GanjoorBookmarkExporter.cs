using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ganjoor.Audio_Support
{
    /// <summary>
    /// نتیجهٔ ارسال نشانه‌های محلی برنامه به بوکمارکهای حساب کاربری در ganjoor.net
    /// </summary>
    public class BookmarkExportResult
    {
        /// <summary>
        /// با موفقیت ارسال شد (یا پیشتر روی سایت وجود داشت)
        /// </summary>
        public int Sent;

        /// <summary>
        /// نشانه‌های «کل شعر» (بدون مصرع مشخص) که سامانهٔ نشانهٔ سایت معادلی برایشان ندارد
        /// </summary>
        public int SkippedWholePoem;

        /// <summary>
        /// مصرع متناظر در داده‌های شعر یافت نشد یا در سایت قابل نشانه‌گذاری نیست (مانند پاراگرافهای توضیحی)
        /// </summary>
        public int SkippedUnavailable;

        /// <summary>
        /// خطای سرور یا شبکه
        /// </summary>
        public int Failed;

        /// <summary>
        /// شرح خطاهای رخ‌داده (حداکثر چند مورد اول، برای گزارش به کاربر)
        /// </summary>
        public List<string> FailureDetails = new List<string>();

        public int TotalLocalFavs => Sent + SkippedWholePoem + SkippedUnavailable + Failed;
    }

    /// <summary>
    /// خطایی که نشان می‌دهد رمز ورود کاربر معتبر نیست و باید دوباره وارد حساب کاربری شود
    /// </summary>
    public class GanjoorTokenInvalidException : Exception
    {
    }

    /// <summary>
    /// ارسال نشانه‌های محلی برنامه (جدول fav) به بوکمارکهای حساب کاربری در ganjoor.net،
    /// از طریق وب‌سرویس https://api.ganjoor.net/api/ganjoor/bookmark .
    /// چون نشانه‌های محلی بر اساس شمارهٔ مصرع (vorder) هستند ولی نشانه‌های سایت بر اساس
    /// شمارهٔ بیت (CoupletIndex، یک شمارهٔ مشترک برای هر دو مصرع یک بیت)، ابتدا باید
    /// معادل الگوریتم _FillPoemCoupletIndices سمت سرور برای هر شعر اجرا شود.
    /// </summary>
    public static class GanjoorBookmarkExporter
    {
        /// <summary>
        /// شمارهٔ بیت (CoupletIndex) هر مصرع را طبق همان الگوریتمی که سرور استفاده می‌کند محاسبه می‌کند:
        /// مصرعهای Left و CenteredVerse2 شمارهٔ بیت مصرع پیش از خود را می‌گیرند و مصرعهای Comment
        /// اصلاً بیت به‌حساب نمی‌آیند (خروجی null برای آنها).
        /// </summary>
        /// <returns>نگاشت از _Order مصرع به شمارهٔ بیت متناظر (یا null اگر قابل نشانه‌گذاری در سایت نباشد)</returns>
        public static Dictionary<int, int?> ComputeCoupletIndices(IEnumerable<GanjoorVerse> poemVerses)
        {
            var result = new Dictionary<int, int?>();
            int cIndex = -1;
            foreach (GanjoorVerse verse in poemVerses.OrderBy(v => v._Order))
            {
                if (verse._Position != VersePosition.Left && verse._Position != VersePosition.CenteredVerse2 && verse._Position != VersePosition.Comment)
                    cIndex++;

                result[verse._Order] = verse._Position != VersePosition.Comment ? (int?)cIndex : null;
            }
            return result;
        }

        /// <summary>
        /// تمام نشانه‌های محلی را می‌خواند و برای هرکدام که معادل قابل نشانه‌گذاری در سایت دارد،
        /// درخواست بوکمارک کردن را به ganjoor.net ارسال می‌کند.
        /// </summary>
        /// <param name="db">پایگاه دادهٔ محلی</param>
        /// <param name="baseUrl">آدرس پایهٔ وب‌سرویس (مثال: https://api.ganjoor.net)</param>
        /// <param name="token">رمز ورود معتبر (Bearer token) کاربر</param>
        /// <exception cref="GanjoorTokenInvalidException">اگر رمز ورود نامعتبر یا منقضی باشد</exception>
        public static async Task<BookmarkExportResult> ExportAsync(DbBrowser db, string baseUrl, string token)
        {
            var result = new BookmarkExportResult();

            DataTable favs = db.GetAllFavs();
            if (favs == null || favs.Rows.Count == 0)
                return result;

            var favsByPoem = favs.Rows.Cast<DataRow>().GroupBy(r => Convert.ToInt32(r["poem_id"]));

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                foreach (var poemGroup in favsByPoem)
                {
                    int poemId = poemGroup.Key;

                    GanjoorVerse[] verses;
                    try
                    {
                        verses = db.GetVerses(poemId).ToArray();
                    }
                    catch
                    {
                        verses = new GanjoorVerse[0];
                    }

                    Dictionary<int, int?> coupletIndices = ComputeCoupletIndices(verses);

                    foreach (DataRow row in poemGroup)
                    {
                        int verseId = Convert.ToInt32(row["verse_id"]);

                        if (verseId == -1)
                        {
                            // نشانهٔ کل شعر (بدون مصرع مشخص)؛ سامانهٔ نشانهٔ سایت معادلی ندارد
                            result.SkippedWholePoem++;
                            continue;
                        }

                        if (!coupletIndices.TryGetValue(verseId, out int? coupletIndex) || coupletIndex == null)
                        {
                            // مصرع دیگر وجود ندارد یا از نوع پاراگراف توضیحی است (قابل نشانه‌گذاری در سایت نیست)
                            result.SkippedUnavailable++;
                            continue;
                        }

                        HttpResponseMessage response;
                        try
                        {
                            response = await httpClient.PostAsync($"{baseUrl}/api/ganjoor/bookmark/{poemId}/{coupletIndex}", null);
                        }
                        catch (Exception ex)
                        {
                            result.Failed++;
                            result.FailureDetails.Add(string.Format("شعر {0}، بیت {1}: {2}", poemId, coupletIndex, ex.Message));
                            continue;
                        }

                        if (response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GanjoorTokenInvalidException();
                        }

                        if (response.StatusCode == HttpStatusCode.NotFound)
                        {
                            result.SkippedUnavailable++;
                            continue;
                        }

                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            result.Failed++;
                            result.FailureDetails.Add(string.Format("شعر {0}، بیت {1}: {2}", poemId, coupletIndex, await response.Content.ReadAsStringAsync()));
                            continue;
                        }

                        result.Sent++;
                    }
                }
            }

            return result;
        }
    }
}
