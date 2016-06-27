using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;

namespace ganjoor
{
    partial class WSDownloadItems : WizardStage
    {
    
        public WSDownloadItems()
            : base()
        {
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
                foreach (GDBInfo gdbInfo in dwnldList)
                {
                    GdbDownloadInfo ctl = new GdbDownloadInfo(gdbInfo);
                    this.pnlList.Controls.Add(ctl);
                    ctl.Dock = DockStyle.Top;
                    ctl.SendToBack();
                }

            (this.Parent.Parent as Form).AcceptButton = btnStop;
            btnStop.Focus();

            BeginNextDownload();

        }

        #region Automatic Next Stage Event
        public event EventHandler OnStageDone = null;
        #endregion

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

        private void BeginNextDownload()
        {
            if (_DownloadIndex < this.pnlList.Controls.Count)
            {
                backgroundWorker.RunWorkerAsync();
            }
            else
                if (this.OnStageDone != null)
                    OnStageDone(this, new EventArgs());                    

        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string targetDir = GDBListProcessor.DownloadPath;
            if (!Directory.Exists(targetDir))
                Directory.CreateDirectory(targetDir);
            string sFileDownloaded = DownloadUtilityClass.DownloadFileIgnoreFail(
                ((this.pnlList.Controls[_RealDownloadIndex] as GdbDownloadInfo).Tag as GDBInfo).DownloadUrl,
                targetDir,
                this.backgroundWorker);
            if (!string.IsNullOrEmpty(sFileDownloaded))
                _DownloadedFiles.Add(sFileDownloaded);
            else
                if(_RealDownloadIndex>=0)
                    MessageBox.Show(string.Format("دریافت مجموعهٔ {0} با خطا مواجه شد.", ((this.pnlList.Controls[_RealDownloadIndex] as GdbDownloadInfo).Tag as GDBInfo).CatName), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
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
            {
                if (this.OnStageDone != null)
                    OnStageDone(this, new EventArgs());
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
                if (MessageBox.Show("از توقف دریافت مطمئنید؟", "پرسش", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == System.Windows.Forms.DialogResult.Yes)
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
