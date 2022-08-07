using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class WSDownloadSounds : WizardStage
    {
        public WSDownloadSounds()
        {
            InitializeComponent();
        }

        public List<Dictionary<string, string>> DownloadList
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
            if (DownloadList != null)
                foreach (Dictionary<string, string> audioInfo in DownloadList)
                {
                    SndDownloadInfo ctl = new SndDownloadInfo(audioInfo);
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
        private List<Dictionary<string, string>> _DownloadedSounds = new List<Dictionary<string, string>>();


        public List<Dictionary<string, string>> DownloadedSounds
        {
            get
            {
                return _DownloadedSounds;
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
            string targetDir = DownloadableAudioListProcessor.SoundsPath;
            if (!Directory.Exists(targetDir))
                Directory.CreateDirectory(targetDir);
            Dictionary<string, string> audioInfo = (this.pnlList.Controls[_RealDownloadIndex] as SndDownloadInfo).Tag as Dictionary<string, string>;
            string strException;
            if (!DownloadableAudioListProcessor.DownloadAudioXml(audioInfo["audio_xml"], targetDir, true, out strException))
            {
                MessageBox.Show(string.Format("دریافت فایل XML خوانش {0} با خطا مواجه شد.", DownloadableAudioListProcessor.SuggestTitle((this.pnlList.Controls[_RealDownloadIndex] as SndDownloadInfo).Tag as Dictionary<string, string>)), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
            else
            {
                string sFileDownloaded = DownloadUtilityClass.DownloadFileIgnoreFail(
                    ((this.pnlList.Controls[_RealDownloadIndex] as SndDownloadInfo).Tag as Dictionary<string, string>)["audio_mp3"],
                    targetDir,
                    this.backgroundWorker, out string expString);
                if (!string.IsNullOrEmpty(sFileDownloaded))
                    _DownloadedSounds.Add(audioInfo);
                else
                    if (_RealDownloadIndex >= 0)
                    MessageBox.Show(string.Format("دریافت فایل صوتی خوانش {0} با خطا مواجه شد.\n{1}", DownloadableAudioListProcessor.SuggestTitle((this.pnlList.Controls[_RealDownloadIndex] as SndDownloadInfo).Tag as Dictionary<string, string>), expString), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (_DownloadIndex < this.pnlList.Controls.Count)//شانسی که من دارم باید چک کنم ;)
            {
                (this.pnlList.Controls[_RealDownloadIndex] as SndDownloadInfo).Progress = e.ProgressPercentage;
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
