using System.Windows.Forms;

namespace ganjoor
{
    public partial class GdbDownloadInfo : UserControl
    {
        public GdbDownloadInfo(GDBInfo gdbInfo)
        {
            InitializeComponent();

            Tag = gdbInfo;
            lblGdbName.Text = gdbInfo.CatName;
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
