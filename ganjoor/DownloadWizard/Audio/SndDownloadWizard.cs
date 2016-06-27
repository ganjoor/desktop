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

        private void dwnStage_OnStageDone(object sender, EventArgs e)
        {
            ActivateStage(3);//Automatic transition from download to install
        }

        private List<Dictionary<string, string>> _DownloadList = null;
        public bool AnythingInstalled
        {
            get;
            private set;
        }

        protected override void GetDataFromPreStage(int StageIndex) 
        {
            if (_Stages[_CurrentStage] is WSSelectSounds)
                _DownloadList = (_Stages[_CurrentStage] as WSSelectSounds).dwnldList;
        }

        protected override void PostDataToNextStage(int StageIndex)
        {
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
        }


    }
}
