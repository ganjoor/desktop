using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class DownloadWizard : Form
    {
        public DownloadWizard()
        {
            InitializeComponent();
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

        public void AddStage(WizardStage Stage)
        {
            _Stages.Add(Stage);
            Stage.Visible = false;
            Stage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Stage.Dock = DockStyle.Fill;
            pnlCurrentStage.Controls.Add(Stage);
        }

        public void ActivateStage(int StageIndex)
        {
            if (StageIndex >= 0 && StageIndex < _Stages.Count)
            {
                if (_CurrentStage != -1)
                {
                    _Stages[_CurrentStage].OnApplied();
                    _Stages[_CurrentStage].Visible = false;

                    if (_Stages[_CurrentStage] is WSSelectItems)
                        _DownloadList = (_Stages[_CurrentStage] as WSSelectItems).dwnldList;
                    if (_Stages[_CurrentStage] is WSDownloadItems)
                        DownloadedFiles = (_Stages[_CurrentStage] as WSDownloadItems).DownloadedFiles;
                }


                btnNext.Visible = _Stages[StageIndex].NextStageButton && (StageIndex < (_Stages.Count - 1));
                btnPrevious.Visible = _Stages[StageIndex].PreviousStageButton && (StageIndex > 0);

                btnNext.Text = _Stages[StageIndex].NextStageText;
                btnPrevious.Text = _Stages[StageIndex].PreviousStageText;

                btnNext.Size = new Size(TextRenderer.MeasureText(btnNext.Text, btnNext.Font).Width + 40, 23);
                btnPrevious.Size = new Size(TextRenderer.MeasureText(btnPrevious.Text, btnPrevious.Font).Width + 22, 23);


                if (btnPrevious.Visible)
                {
                    btnPrevious.Location = new Point(16, btnPrevious.Top);
                    btnNext.Location = new Point(btnPrevious.Left + btnNext.Width + 16, btnNext.Top);
                }
                else
                    if(btnNext.Visible)
                        btnNext.Location = new Point(16, btnNext.Top);

                if (_Stages[StageIndex] is WSDownloadItems)
                    (_Stages[StageIndex] as WSDownloadItems).dwnldList = _DownloadList;
                if (_Stages[StageIndex] is WSInstallItems)
                    (_Stages[StageIndex] as WSInstallItems).DownloadedFiles = DownloadedFiles;
                if (StageIndex == (_Stages.Count - 1))
                {
                    btnCancel.Text = "تأیید";
                    this.AcceptButton = btnCancel;
                    btnCancel.Focus();
                }
                btnNext.Enabled = btnPrevious.Enabled = false;Application.DoEvents();
                _Stages[StageIndex].OnBeforeActivate();
                _Stages[StageIndex].Visible = true;
                btnNext.Enabled = btnPrevious.Enabled = true; Application.DoEvents();
                _Stages[StageIndex].OnActivated();
                _CurrentStage = StageIndex;

            }
        }

        private List<WizardStage> _Stages = new List<WizardStage>();
        private int _CurrentStage = -1;

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_CurrentStage > 0)
            {
                ActivateStage(_CurrentStage - 1);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_CurrentStage < _Stages.Count - 1)
            {
                ActivateStage(_CurrentStage + 1);
            }
        }

        private void DownloadWizard_Load(object sender, EventArgs e)
        {
            ActivateStage(0);
        }

        private List<GDBInfo> _DownloadList = null;
        private List<string> DownloadedFiles = null;

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (GDBWizOptions dlg = new GDBWizOptions())
                dlg.ShowDialog(this);
        }

        public bool AnythingInstalled
        {
            get;
            private set;
        }


    }
}
