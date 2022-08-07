using System;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class GDBPictureDirSelector : Form
    {
        public GDBPictureDirSelector()
        {
            InitializeComponent();
        }

        public bool EmbedPictures
        {
            get
            {
                return chkPicturesEnabled.Checked;
            }
        }
        public string PicturesPath
        {
            get
            {
                return txtPath.Text;
            }
        }
        public string PicturesUrlPrefix
        {
            get
            {
                return txtPicturesUrlPrefix.Text;
            }
        }

        private void chkPicturesEnabled_CheckedChanged(object sender, EventArgs e)
        {
            grpPictures.Enabled = chkPicturesEnabled.Checked;
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    txtPath.Text = dlg.SelectedPath;
        }

    }
}
