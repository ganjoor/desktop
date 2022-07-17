using System.Collections.Generic;
using System.Linq;

namespace ganjoor
{
    /// <summary>
    /// قافیه یاب
    /// </summary>
    public class RhymeFinder
    {
        /// <summary>
        /// find rhyme
        /// </summary>
        /// <param name="verses"></param>
        /// <param name="secondPhase"></param>
        /// <returns></returns>
        public static GanjooRhymeAnalysisResult FindRhyme(List<GanjoorVerse> verses, bool secondPhase = false)
        {
            List<string> verseTextList = verses.Count == 2 ? verses.Select(v => v._Text).ToList()
                                                           : verses.Where(v => v._Position == VersePosition.Left).Select(v => v._Text).ToList();
            if (verseTextList.Count > 1)
            {
                string rhyme = PrepareTextForFindingRhyme(verseTextList[0]);
                if (string.IsNullOrEmpty(rhyme))
                {
                    return new GanjooRhymeAnalysisResult()
                    {
                        Rhyme = "",
                        FailVerse = verseTextList[0]
                    };
                }
                if (secondPhase)
                {
                    if (secondPhase)
                        if (rhyme[rhyme.Length - 1] == 'ی')
                            rhyme = rhyme.Remove(rhyme.Length - 1);
                }

                for (int j = 1; j < verseTextList.Count; j++)
                {
                    string verseText = PrepareTextForFindingRhyme(verseTextList[j]);
                    if (secondPhase)
                    {
                        if (verseText[verseText.Length - 1] == 'ی')
                        {
                            verseText = verseText.Remove(verseText.Length - 1);
                        }
                    }
                    string oldRhyme = rhyme;
                    rhyme = "";
                    int i = oldRhyme.Length - 1;
                    while (
                        (oldRhyme[i] == verseText[verseText.Length - oldRhyme.Length + i])
                        ||
                        (
                        (oldRhyme[i] == 'ذ')
                        &&
                        (verseText[verseText.Length - oldRhyme.Length + i] == 'د')
                        )
                        ||
                        (
                        (oldRhyme[i] == 'د')
                        &&
                        (verseText[verseText.Length - oldRhyme.Length + i] == 'ذ')
                        )
                        ||

                        (
                        (oldRhyme[i] == 'ی')
                        &&
                        (verseText[verseText.Length - oldRhyme.Length + i] == 'ا')
                        )
                        ||
                        (
                        (oldRhyme[i] == 'ا')
                        &&
                        (verseText[verseText.Length - oldRhyme.Length + i] == 'ی')
                        )

                        ||

                        (oldRhyme[i] == verseText[verseText.Length - oldRhyme.Length + i])
                        ||
                        (
                        (oldRhyme[i] == 'پ')
                        &&
                        (verseText[verseText.Length - oldRhyme.Length + i] == 'ب')
                        )
                        ||
                        (
                        (oldRhyme[i] == 'ب')
                        &&
                        (verseText[verseText.Length - oldRhyme.Length + i] == 'پ')
                        )

                        ||
                        (oldRhyme[i] == 'ة')
                        &&
                        (verseText[verseText.Length - oldRhyme.Length + i] == 'ت')

                        ||
                        (oldRhyme[i] == 'ت')
                        &&
                        (verseText[verseText.Length - oldRhyme.Length + i] == 'ة')



                        )
                    {
                        rhyme = oldRhyme[i] + rhyme;
                        i--;
                        if (i == -1)
                            break;
                    }
                    if (rhyme.Length == 0)
                    {
                        if(verses.Count == 2 && verseTextList.Count == 2)
                        {
                            return new GanjooRhymeAnalysisResult()
                            {
                                Rhyme = PrepareTextForFindingRhyme(verseTextList[1]),
                                FailVerse = "",
                                FailVerseOrder = -1,
                            };
                        }
                        else
                        {
                            return new GanjooRhymeAnalysisResult()
                            {
                                Rhyme = "",
                                FailVerse = verseTextList[j],
                                FailVerseOrder = 2 * j,
                            };
                        }
                       
                    }

                }

                if (!secondPhase)
                {
                    if (string.IsNullOrEmpty(rhyme))
                    {
                        return FindRhyme(verses, true);
                    }
                }

                return new GanjooRhymeAnalysisResult()
                {
                    Rhyme = rhyme,
                    FailVerse = "",
                    FailVerseOrder = -1,
                };

            }

            return new GanjooRhymeAnalysisResult() { Rhyme = "", FailVerse = "", FailVerseOrder = -1 };
        }

        /// <summary>
        /// make text searchable
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string MakeTextSearchable(string text)
        {
            return text.Replace("‌", " ")//replace zwnj with space
                       .Replace("ّ", "")//tashdid
                       .Replace("َ", "")//a
                       .Replace("ِ", "")//e
                       .Replace("ُ", "")//o
                       .Replace("ً", "")//an
                       .Replace("ٍ", "")//en
                       .Replace("ٌ", "")//on
                       .Replace(".", "")//dot
                       .Replace("،", "")//virgool
                       .Replace("!", "")
                       .Replace("؟", "")
                       .Replace("ٔ", "")//ye
                       .Replace(":", "")
                       .Replace("ئ", "ی")
                       .Replace("؛", "")
                       .Replace(";", "")
                       .Replace("*", "")
                       .Replace(")", "")
                       .Replace("(", "")
                       .Replace("[", "")
                       .Replace("]", "")
                       .Replace("\"", "")
                       .Replace("'", "")
                       .Replace("«", "")
                       .Replace("»", "")
                       ;
        }

        private static string PrepareTextForFindingRhyme(string text)
        {
            return MakeTextSearchable(text)
                    .Replace("لله", "للاه")
                    .Replace("آ", "ا")
                    .Replace("‍", "")
                    .Replace("‏", "")
                    .Replace("‌", "")
                    .Replace(" ", "")
                    .Trim();
        }

    }
}
