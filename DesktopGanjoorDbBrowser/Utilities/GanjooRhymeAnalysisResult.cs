namespace ganjoor
{
    /// <summary>
    /// Rhyme Analysis Result
    /// </summary>
    public class GanjooRhymeAnalysisResult
    {
        /// <summary>
        /// rhyme
        /// </summary>
        public string Rhyme { get; set; }

        /// <summary>
        /// the verse analysis stopped at
        /// </summary>
        public string FailVerse { get; set; }

        /// <summary>
        /// fail verse order
        /// </summary>
        public int FailVerseOrder { get; set; }
    }
}
