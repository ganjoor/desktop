using System.Collections.Generic;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class SndDownloadInfo : UserControl
    {
        public SndDownloadInfo(Dictionary<string, string> audioInfo)
        {
            InitializeComponent();
            Tag = audioInfo;
            lblSndName.Text = DownloadableAudioListProcessor.SuggestShortTitle(audioInfo);
        }

        public int Progress
        {
            set
            {
                prgess.Value = value;
            }
        }

    }
}
