using System.Windows.Forms;

namespace ganjoor
{
    public partial class WizardStage : UserControl
    {
        public WizardStage()
        {
            InitializeComponent();
        }
        #region Overridables
        public virtual void OnBeforeActivate() { }
        public virtual void OnActivated() { }
        public virtual void OnApplied() { }
        public virtual bool OnRequestCloseWindow() { return true; }
        public virtual bool NextStageButton { get { return true; } }
        public virtual bool PreviousStageButton { get { return true; } }
        public virtual string NextStageText { get { return "ادامه"; } }
        public virtual string PreviousStageText { get { return "برگشت"; } }
        #endregion
    }
}
