using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace ganjoor
{
    partial class WSDownloadItems : WizardStage
    {

        public WSDownloadItems() {
            InitializeComponent();
        }

        public List<GDBInfo> dwnldList
        {
            get;
            set;
        }

        public override bool NextStageButton
        {
            get
            {
                return false;
            }
        }

        public override bool PreviousStageButton
        {
            get
            {
                return false;
            }
        }

        public override void OnActivated()
        {
            if (dwnldList != null)
                foreach (var gdbInfo in dwnldList)
                {
                    var ctl = new GdbDownloadInfo(gdbInfo);
                    pnlList.Controls.Add(ctl);
                    ctl.Dock = DockStyle.Top;
                    ctl.SendToBack();
                }

            (Parent.Parent as Form).AcceptButton = btnStop;
            btnStop.Focus();

            BeginNextDownload();

        }

        #region Automatic Next Stage Event
        public event EventHandler OnStageDone;
        #endregion

        private int _DownloadIndex;
        private int _RealDownloadIndex
        {
            get
            {
                return pnlList.Controls.Count - 1 - _DownloadIndex;
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

        private void BeginNextDownload()
        {
            if (_DownloadIndex < pnlList.Controls.Count)
            {
                backgroundWorker.RunWorkerAsync();
            }
            else {
                OnStageDone?.Invoke(this, EventArgs.Empty);
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var targetDir = GDBListProcessor.DownloadPath;
            if (!Directory.Exists(targetDir))
                Directory.CreateDirectory(targetDir);
            var sFileDownloaded = DownloadUtilityClass.DownloadFileIgnoreFail(
                ((pnlList.Controls[_RealDownloadIndex] as GdbDownloadInfo).Tag as GDBInfo).DownloadUrl,
                targetDir,
                backgroundWorker, out var expString);
            if (!string.IsNullOrEmpty(sFileDownloaded))
                _DownloadedFiles.Add(sFileDownloaded);
            else
                if (_RealDownloadIndex >= 0)
                MessageBox.Show(string.Format("دریافت مجموعهٔ {0} با خطا مواجه شد.\n{1}", ((pnlList.Controls[_RealDownloadIndex] as GdbDownloadInfo).Tag as GDBInfo).CatName, expString), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (_DownloadIndex < pnlList.Controls.Count)//شانسی که من دارم باید چک کنم ;)
            {
                (pnlList.Controls[_RealDownloadIndex] as GdbDownloadInfo).Progress = e.ProgressPercentage;
                Application.DoEvents();
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled || !btnStop.Enabled) {
                OnStageDone?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                _DownloadIndex++;
                BeginNextDownload();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                if (MessageBox.Show("از توقف دریافت مطمئنید؟", "پرسش", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.Yes)
                {
                    lblMsg.Text = "لطفاً منتظر بمانید تا دریافت فایل جاری متوقف شود ...";
                    btnStop.Enabled = false;
                    backgroundWorker.CancelAsync();
                    Application.DoEvents();
                }
            }
        }



    }
}
