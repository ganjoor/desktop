using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ganjoor.Properties;

namespace ganjoor
{
    public partial class GDBWizOptions : Form
    {
        public GDBWizOptions()
        {
            InitializeComponent();
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            if (Settings.Default.CustomDownloadUrls != null)
                Settings.Default.CustomDownloadUrls.Clear();
            if (Settings.Default.CustomDownloadListNames != null)
                Settings.Default.CustomDownloadListNames.Clear();
            if (Settings.Default.CustomDownloadListDescriptions != null)
                Settings.Default.CustomDownloadListDescriptions.Clear();
            Settings.Default.LastDownloadUrl = DownloadListManager.Urls[0];
            Settings.Default.Save();
        }

        private void btnGDBListEditor_Click(object sender, EventArgs e) {
            using var dlg = new GDBListEditor();
            dlg.ShowDialog(this);
        }

        private void GDBWizOptions_Load(object sender, EventArgs e)
        {
            txtTempPath.Text = GDBListProcessor.DownloadPath;
            chkDeleteDownloadedFiles.Checked = Settings.Default.DeleteDownloadedFiles;
        }

        private void btnSelectTempPath_Click(object sender, EventArgs e) {
            using var dlg = new FolderBrowserDialog();
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
                GDBListProcessor.DownloadPath = txtTempPath.Text;
            }
            catch
            {
            }
            Settings.Default.DeleteDownloadedFiles = chkDeleteDownloadedFiles.Checked;
            Settings.Default.Save();
        }
    }
}
