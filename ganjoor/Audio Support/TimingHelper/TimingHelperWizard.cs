namespace ganjoor.Audio_Support.TimingHelper
{
    public partial class TimingHelperWizard : MultiStageWizard
    {
        public TimingHelperWizard(int nPoemId = 0)
        {
            InitializeComponent();

            THFirstVerse firstVerse = new THFirstVerse(nPoemId);
            AddStage(firstVerse);
        }
    }
}
