using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ganjoor
{
    public class GDBDownloadWizard : MultiStageWizard
    {
        public GDBDownloadWizard() : base()
        {

            AnythingInstalled = false;
            AddStage(new WSSelectList());
            WSSelectItems selStage = new WSSelectItems();
            selStage.OnDisableNextButton += new EventHandler(selStage_OnDisableNextButton);
            selStage.OnEnableNextButton += new EventHandler(selStage_OnEnableNextButton);
            AddStage(selStage);
            WSDownloadItems dwnStage = new WSDownloadItems();
            dwnStage.OnStageDone += new EventHandler(dwnStage_OnStageDone);
            AddStage(dwnStage);
            WSInstallItems instStage = new WSInstallItems();
            instStage.OnInstallStarted += new EventHandler(instStage_OnInstallStarted);
            instStage.OnInstallFinished += new EventHandler(instStage_OnInstallFinished);
            AddStage(instStage);
        }

        private void instStage_OnInstallStarted(object sender, EventArgs e)
        {
            btnCancel.Enabled = false; Application.DoEvents();
        }

        private void instStage_OnInstallFinished(object sender, EventArgs e)
        {
            this.AnythingInstalled = ((_Stages[_Stages.Count - 1]) as WSInstallItems).InstalledFilesCount > 0;
            btnCancel.Enabled = true;
            btnCancel.Focus();
            Application.DoEvents();
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

        private void dwnStage_OnStageDone(object sender, EventArgs e)
        {
            ActivateStage(3);//Automatic transition from download to install
        }

        private List<GDBInfo> _DownloadList = null;
        private List<string> DownloadedFiles = null;

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

        protected override void ShowSettings()
        {
            using (GDBWizOptions dlg = new GDBWizOptions())
                dlg.ShowDialog(this);
        }


    }
}
