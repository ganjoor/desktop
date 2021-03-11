using System;

namespace ganjoor.Audio_Support.TimingHelper
{
    public partial class TimingHelperWizard : MultiStageWizard
    {
        public TimingHelperWizard(int nPoemId = 0)
        {
            InitializeComponent();

            THFirstVerse firstVerse = new THFirstVerse(nPoemId);
            AddStage(firstVerse);

            THHelper mainStep = new THHelper(nPoemId);
            AddStage(mainStep);
        }

        public TimeSpan StartToSilence { get; set; }
        public TimeSpan SilenceToStop { get; set; }

        protected override void GetDataFromPreStage(int StageIndex)
        {
            if (_Stages[_CurrentStage] is THFirstVerse)
            {
                StartToSilence = (_Stages[_CurrentStage] as THFirstVerse).StartToSilence;
                SilenceToStop = (_Stages[_CurrentStage] as THFirstVerse).SilenceToStop;
            }
        }

        protected override void PostDataToNextStage(int StageIndex)
        {
            if (_Stages[StageIndex] is THHelper)
            {
                (_Stages[StageIndex] as THHelper).StartToSilence = StartToSilence;
                (_Stages[StageIndex] as THHelper).SilenceToStop = SilenceToStop;
            }
                
        }
    }
}
