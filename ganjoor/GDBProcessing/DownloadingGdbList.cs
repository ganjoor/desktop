using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class DownloadingGdbList : Form
    {
        public DownloadingGdbList(List<GDBInfo> Lst)
        {
            InitializeComponent();
            foreach (GDBInfo gdbInfo in Lst)
            {
                GdbDownloadInfo ctl = new GdbDownloadInfo(gdbInfo);
                this.pnlList.Controls.Add(ctl);
                ctl.Dock = DockStyle.Top;
                ctl.SendToBack();
            }
        }


        private int _DownloadIndex = 0;
        private int _RealDownloadIndex
        {
            get
            {
                return (this.pnlList.Controls.Count - 1 - _DownloadIndex);
            }
        }
        private List<string> _DownloadedFiles = new List<string>();


        public List<string> DownloadedFiles
        {
            get
            {
                return _DownloadedFiles;
            }
        }

        private void DownloadingGdbList_Shown(object sender, EventArgs e)
        {
            BeginNextDownload();
        }

        private void BeginNextDownload()
        {
            if (_DownloadIndex < this.pnlList.Controls.Count)
            {
                backgroundWorker.RunWorkerAsync();
            }
            else
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string targetDir = GDBListProcessor.DownloadPath;
            if (!Directory.Exists(targetDir))
                Directory.CreateDirectory(targetDir);
            string sFileDownloaded = DownloadUtilityClass.DownloadFileIgnoreFail(
                ((this.pnlList.Controls[_RealDownloadIndex] as GdbDownloadInfo).Tag as GDBInfo).DownloadUrl,
                targetDir,
                this.backgroundWorker, out string expString);
            if (!string.IsNullOrEmpty(sFileDownloaded))
                _DownloadedFiles.Add(sFileDownloaded);
            else
                MessageBox.Show(string.Format("دریافت مجموعهٔ {0} با خطا مواجه شد.\n{1}", ((this.pnlList.Controls[_RealDownloadIndex] as GdbDownloadInfo).Tag as GDBInfo).CatName, expString), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (_DownloadIndex < this.pnlList.Controls.Count)//شانسی که من دارم باید چک کنم ;)
            {
                (this.pnlList.Controls[_RealDownloadIndex] as GdbDownloadInfo).Progress = e.ProgressPercentage;
                Application.DoEvents();
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled || !btnStop.Enabled)
                DialogResult = System.Windows.Forms.DialogResult.OK;
            else
            {
                _DownloadIndex++;
                BeginNextDownload();
            }
        }

        private void DownloadingGdbList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                e.Cancel = true;
                if (MessageBox.Show("از توقف دریافت مطمئنید؟", "پرسش", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == System.Windows.Forms.DialogResult.Yes)
                {
                    btnStop.Enabled = false;
                    backgroundWorker.CancelAsync();
                }
            }
            else
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
