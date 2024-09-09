using System.Collections.Generic;

namespace ganjoor.Audio_Support
{
    public class LyricsModel
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public List<LyricsLineModel> Lines { get; set; }
    }

    public class LyricsLineModel
    {
        public string Line { get; set; }
        public int StartInMilliseconds { get; set; }
        public int EndInMilliseconds { get; set; }
        public List<LyricsWordModel> Words { get; set; }
    }

    public class LyricsWordModel
    {
        public string Word { get; set; }
        public int StartInMilliseconds { get; set; }
        public int EndInMilliseconds { get; set; }

    }
}
