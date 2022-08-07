using System.Windows.Forms;

namespace ganjoor
{
    public partial class GdbDownloadInfo : UserControl
    {
        public GdbDownloadInfo(GDBInfo gdbInfo)
        {
            InitializeComponent();

            this.Tag = gdbInfo;
            this.lblGdbName.Text = gdbInfo.CatName;
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
