using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ganjoor
{
    public class GDBDownloadWizard : MultiStageWizard
    {
        public GDBDownloadWizard() {

            AnythingInstalled = false;
            AddStage(new WSSelectList());
            WSSelectItems selStage = new WSSelectItems();
            selStage.OnDisableNextButton += selStage_OnDisableNextButton;
            selStage.OnEnableNextButton += selStage_OnEnableNextButton;
            AddStage(selStage);
            WSDownloadItems dwnStage = new WSDownloadItems();
            dwnStage.OnStageDone += dwnStage_OnStageDone;
            AddStage(dwnStage);
            WSInstallItems instStage = new WSInstallItems();
            instStage.OnInstallStarted += instStage_OnInstallStarted;
            instStage.OnInstallFinished += instStage_OnInstallFinished;
            AddStage(instStage);
        }

        private void instStage_OnInstallStarted(object sender, EventArgs e)
        {
            btnCancel.Enabled = false; Application.DoEvents();
        }

        private void instStage_OnInstallFinished(object sender, EventArgs e)
        {
            AnythingInstalled = ((_Stages[_Stages.Count - 1]) as WSInstallItems).InstalledFilesCount > 0;
            btnCancel.Enabled = true;
            btnCancel.Focus();
            Application.DoEvents();
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

        private void dwnStage_OnStageDone(object sender, EventArgs e)
        {
            ActivateStage(3);//Automatic transition from download to install
        }

        private List<GDBInfo> _DownloadList;
        private List<string> DownloadedFiles;

        public bool AnythingInstalled
        {
            get;
            private set;
        }

        protected override void GetDataFromPreStage(int StageIndex)
        {
            if (_Stages[_CurrentStage] is WSSelectItems)
                _DownloadList = (_Stages[_CurrentStage] as WSSelectItems).dwnldList;
            if (_Stages[_CurrentStage] is WSDownloadItems)
                DownloadedFiles = (_Stages[_CurrentStage] as WSDownloadItems).DownloadedFiles;
        }

        protected override void PostDataToNextStage(int StageIndex)
        {
            if (_Stages[StageIndex] is WSDownloadItems)
                (_Stages[StageIndex] as WSDownloadItems).dwnldList = _DownloadList;
            if (_Stages[StageIndex] is WSInstallItems)
                (_Stages[StageIndex] as WSInstallItems).DownloadedFiles = DownloadedFiles;
        }

        protected override string DownloadCaption
        {
            get
            {
                return "دریافت مجموعه‌ها";
            }
        }

        protected override void ShowSettings() {
            using GDBWizOptions dlg = new GDBWizOptions();
            dlg.ShowDialog(this);
        }


    }
}
