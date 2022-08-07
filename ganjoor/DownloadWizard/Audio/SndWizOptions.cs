using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ganjoor.Properties;

namespace ganjoor
{
    public partial class SndWizOptions : Form
    {
        public SndWizOptions()
        {
            InitializeComponent();
        }

        private void SndWizOptions_Load(object sender, EventArgs e)
        {
            txtTempPath.Text = DownloadableAudioListProcessor.SoundsPath;
        }

        private void btnSelectTempPath_Click(object sender, EventArgs e) {
            using FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = txtTempPath.Text;
            if (dlg.ShowDialog(this) == DialogResult.OK)
                txtTempPath.Text = dlg.SelectedPath;
        }

        private void btnBrowseTempPath_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(txtTempPath.Text);
            }
            catch { }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(txtTempPath.Text))
                    Directory.CreateDirectory(txtTempPath.Text);
                DownloadableAudioListProcessor.SoundsPath = txtTempPath.Text;
            }
            catch
            {
            }
            Settings.Default.Save();
        }
    }
}
