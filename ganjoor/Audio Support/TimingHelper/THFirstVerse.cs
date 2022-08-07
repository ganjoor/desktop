using System;
using System.Windows.Forms;
using ganjoor.Properties;

namespace ganjoor.Audio_Support.TimingHelper
{
    public partial class THFirstVerse : WizardStage
    {
        public THFirstVerse(int nPoemId = 0)
        {
            Font = Settings.Default.ViewFont;

            InitializeComponent();

            var dbBrowser = new DbBrowser();

            var verses = dbBrowser.GetVerses(nPoemId);

            if (verses.Count > 1)
            {
                _firstVerse = verses[0]._Text;
                _secondVerse = verses[1]._Text;
            }

            lblVerse.Text = _firstVerse;

            dbBrowser.CloseDb();

        }

        private TimingStep step = TimingStep.NotStarted;

        private string _firstVerse = "";
        private string _secondVerse = "";

        public TimeSpan StartToSilence = TimeSpan.Zero;
        public TimeSpan SilenceToStop = TimeSpan.Zero;

        private DateTime _start;

        private void btnStart_Click(object sender, EventArgs e)
        {
            switch (step)
            {
                case TimingStep.NotStarted:
                    _start = DateTime.Now;
                    timerStartToSilence.Enabled = true;
                    step = TimingStep.StartToSilence;
                    break;
                case TimingStep.StartToSilence:
                    StartToSilence = DateTime.Now - _start;
                    timerStartToSilence.Enabled = false;
                    lblVerse.Text = _secondVerse;
                    _start = DateTime.Now;
                    timerSilenceToStop.Enabled = true;
                    step = TimingStep.SilenceToStop;
                    break;
                case TimingStep.SilenceToStop:
                    SilenceToStop = DateTime.Now - _start;
                    timerSilenceToStop.Enabled = false;
                    lblVerse.Text = _firstVerse;
                    step = TimingStep.NotStarted;
                    break;

            }
        }

        private void timerStartToSilence_Tick(object sender, EventArgs e)
        {
            btnStart.Text = $"مصرع اول: {(DateTime.Now - _start).TotalSeconds}";
            Application.DoEvents();
        }



        private void timerSilenceToStop_Tick(object sender, EventArgs e)
        {
            btnStart.Text = $"تنفس تا مصرع دوم: {(DateTime.Now - _start).TotalSeconds}";
            Application.DoEvents();
        }

    }
}
