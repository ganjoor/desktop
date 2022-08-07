using System.Collections.Generic;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class SndDownloadInfo : UserControl
    {
        public SndDownloadInfo(Dictionary<string, string> audioInfo)
        {
            InitializeComponent();
            this.Tag = audioInfo;
            this.lblSndName.Text = DownloadableAudioListProcessor.SuggestShortTitle(audioInfo);
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
