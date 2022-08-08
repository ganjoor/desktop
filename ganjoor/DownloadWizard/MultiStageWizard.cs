using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class MultiStageWizard : Form
    {
        public MultiStageWizard()
        {
            InitializeComponent();
            Text = DownloadCaption;
        }


        public void AddStage(WizardStage Stage)
        {
            _Stages.Add(Stage);
            Stage.Visible = false;
            Stage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Stage.Dock = DockStyle.Fill;
            pnlCurrentStage.Controls.Add(Stage);
        }

        protected virtual void GetDataFromPreStage(int StageIndex) { }
        protected virtual void PostDataToNextStage(int StageIndex) { }

        public void ActivateStage(int StageIndex)
        {
            if (StageIndex >= 0 && StageIndex < _Stages.Count)
            {
                if (_CurrentStage != -1)
                {
                    _Stages[_CurrentStage].OnApplied();
                    _Stages[_CurrentStage].Visible = false;

                    GetDataFromPreStage(StageIndex);

                }


                btnNext.Visible = _Stages[StageIndex].NextStageButton && StageIndex < _Stages.Count - 1;
                btnPrevious.Visible = _Stages[StageIndex].PreviousStageButton && StageIndex > 0;

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
                    if (btnNext.Visible)
                    btnNext.Location = new Point(16, btnNext.Top);

                PostDataToNextStage(StageIndex);
                if (StageIndex == _Stages.Count - 1)
                {
                    btnCancel.Text = "تأیید";
                    AcceptButton = btnCancel;
                    btnCancel.Focus();
                }
                btnNext.Enabled = btnPrevious.Enabled = false; Application.DoEvents();
                _Stages[StageIndex].OnBeforeActivate();
                _Stages[StageIndex].Visible = true;
                btnNext.Enabled = btnPrevious.Enabled = true; Application.DoEvents();
                _Stages[StageIndex].OnActivated();
                _CurrentStage = StageIndex;

            }
        }

        protected List<WizardStage> _Stages = new List<WizardStage>();
        protected int _CurrentStage = -1;

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


        private void btnSettings_Click(object sender, EventArgs e)
        {
            ShowSettings();
        }

        protected virtual string DownloadCaption
        {
            get
            {
                return "دریافت مجموعه‌ها";
            }
        }

        protected virtual void ShowSettings() { }



    }
}
