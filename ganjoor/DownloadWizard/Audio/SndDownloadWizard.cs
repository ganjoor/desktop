using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ganjoor
{
    public class SndDownloadWizard : MultiStageWizard
    {
        public SndDownloadWizard()
            : this(0)
        {
        }

        public SndDownloadWizard(int nPoemId) : base()
        {

            AnythingInstalled = false;

            WSSelectSounds selStage = new WSSelectSounds(nPoemId);
            selStage.OnDisableNextButton += new EventHandler(selStage_OnDisableNextButton);
            selStage.OnEnableNextButton += new EventHandler(selStage_OnEnableNextButton);
            AddStage(selStage);

            WSDownloadSounds dwnStage = new WSDownloadSounds();
            dwnStage.OnStageDone += new EventHandler(dwnStage_OnStageDone);
            AddStage(dwnStage);

            WSInstallSounds instStage = new WSInstallSounds();
            instStage.OnInstallStarted += new EventHandler(instStage_OnInstallStarted);
            instStage.OnInstallFinished += new EventHandler(instStage_OnInstallFinished);
            AddStage(instStage);


        }

        private int _PoemId
        {
            get;
            set;
        }


        private void selStage_OnEnableNextButton(object sender, EventArgs e)
        {
            this.btnPrevious.Enabled = true;
            this.btnNext.Enabled = true;
            this.AcceptButton = this.btnNext;
            this.btnNext.Focus();
            Application.DoEvents();
        }

        private void selStage_OnDisableNextButton(object sender, EventArgs e)
        {
            this.btnNext.Enabled = false; Application.DoEvents();
        }

        private void instStage_OnInstallStarted(object sender, EventArgs e)
        {
            btnCancel.Enabled = false; Application.DoEvents();
        }

        private void instStage_OnInstallFinished(object sender, EventArgs e)
        {
            this.AnythingInstalled = ((_Stages[_Stages.Count - 1]) as WSInstallSounds).InstalledFilesCount > 0;
            btnCancel.Enabled = true;
            btnCancel.Focus();
            Application.DoEvents();
        }


        private void dwnStage_OnStageDone(object sender, EventArgs e)
        {
            ActivateStage(2);//Automatic transition from download to install
        }

        private List<Dictionary<string, string>> _DownloadList = null;
        private List<Dictionary<string, string>> _DownloadedList = null;
        public bool AnythingInstalled
        {
            get;
            private set;
        }

        protected override void GetDataFromPreStage(int StageIndex) 
        {
            if (_Stages[_CurrentStage] is WSSelectSounds)
                _DownloadList = (_Stages[_CurrentStage] as WSSelectSounds).dwnldList;
            if (_Stages[_CurrentStage] is WSDownloadSounds)
                _DownloadedList = (_Stages[_CurrentStage] as WSDownloadSounds).DownloadedSounds;
        }

        protected override void PostDataToNextStage(int StageIndex)
        {
            if (_Stages[StageIndex] is WSDownloadSounds)
                (_Stages[StageIndex] as WSDownloadSounds).DownloadList = _DownloadList;
            if (_Stages[StageIndex] is WSInstallSounds)
                (_Stages[StageIndex] as WSInstallSounds).DownloadedSounds = _DownloadedList;
        }

        protected override string DownloadCaption
        {
            get
            {
                return "دریافت خوانشها";
            }
        }

        protected override void ShowSettings()
        {
            using (SndWizOptions dlg = new SndWizOptions())
                dlg.ShowDialog(this);
        }



    }
}
