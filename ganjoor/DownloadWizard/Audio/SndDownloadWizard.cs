using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ganjoor
{
    public class SndDownloadWizard : MultiStageWizard
    {
        public SndDownloadWizard()
            : this(0, 0, 0, "")
        {
        }

        public SndDownloadWizard(int nPoemId, int nPoetId, int nCatId, string searchTerm) {

            AnythingInstalled = false;

            var selStage = new WSSelectSounds(nPoemId, nPoetId, nCatId, searchTerm);
            selStage.OnDisableNextButton += selStage_OnDisableNextButton;
            selStage.OnEnableNextButton += selStage_OnEnableNextButton;
            AddStage(selStage);

            var dwnStage = new WSDownloadSounds();
            dwnStage.OnStageDone += dwnStage_OnStageDone;
            AddStage(dwnStage);

            var instStage = new WSInstallSounds();
            instStage.OnInstallStarted += instStage_OnInstallStarted;
            instStage.OnInstallFinished += instStage_OnInstallFinished;
            AddStage(instStage);


        }

        private void selStage_OnEnableNextButton(object sender, EventArgs e)
        {
            btnPrevious.Enabled = true;
            btnNext.Enabled = true;
            AcceptButton = btnNext;
            btnNext.Focus();
            Application.DoEvents();
        }

        private void selStage_OnDisableNextButton(object sender, EventArgs e)
        {
            btnNext.Enabled = false; Application.DoEvents();
        }

        private void instStage_OnInstallStarted(object sender, EventArgs e)
        {
            btnCancel.Enabled = false; Application.DoEvents();
        }

        private void instStage_OnInstallFinished(object sender, EventArgs e)
        {
            AnythingInstalled = ((_Stages[_Stages.Count - 1]) as WSInstallSounds).InstalledFilesCount > 0;
            btnCancel.Enabled = true;
            btnCancel.Focus();
            Application.DoEvents();
        }


        private void dwnStage_OnStageDone(object sender, EventArgs e)
        {
            ActivateStage(2);//Automatic transition from download to install
        }

        private List<Dictionary<string, string>> _DownloadList;
        private List<Dictionary<string, string>> _DownloadedList;
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

        protected override void ShowSettings() {
            using var dlg = new SndWizOptions();
            dlg.ShowDialog(this);
        }



    }
}
