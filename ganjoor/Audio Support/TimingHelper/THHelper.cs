using System;
using System.Collections.Generic;
using ganjoor.Properties;

namespace ganjoor.Audio_Support.TimingHelper
{
    public partial class THHelper : WizardStage
    {
        public THHelper(int nPoemId = 0)
        {
            Font = Settings.Default.ViewFont;

            InitializeComponent();

            var dbBrowser = new DbBrowser();

            _Verses = dbBrowser.GetVerses(nPoemId);


            dbBrowser.CloseDb();

            if (_Verses.Count > 0)
            {
                lblNextVerse.Text = _Verses[0]._Text;
            }
        }

        public TimeSpan StartToSilence { get; set; }
        public TimeSpan SilenceToStop { get; set; }

        private bool _firstVerse = true;

        private bool _started;
        private TimingStep _step = TimingStep.NotStarted;

        private int _CurVerse;


        private List<GanjoorVerse> _Verses;

        private DateTime _start;

        private void lblVerse_Click(object sender, EventArgs e)
        {
            if (!_started)
            {
                _start = DateTime.Now;
                _started = true;
                _firstVerse = true;
                lblVerse.Text = _Verses.Count > 0 ? _Verses[0]._Text : "";
                lblNextVerse.Text = _Verses.Count > 1 ? _Verses[1]._Text : "";
                timer.Enabled = true;
                _step = TimingStep.StartToSilence;
                _CurVerse = 0;
            }
            else
            {
                timer.Enabled = false;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            var span = DateTime.Now - _start;
            switch (_step)
            {
                case TimingStep.StartToSilence:

                    if (span >= StartToSilence)
                    {
                        if ((_CurVerse + 1) >= _Verses.Count)
                        {
                            timer.Enabled = false;
                            progressBar.Value = 100;
                            lblVerse.Text = "پایان";
                        }
                        else
                        {
                            _step = TimingStep.SilenceToStop;
                            _start = DateTime.Now;
                            lblVerse.Text = "تنفس";
                        }
                    }
                    else
                    {
                        var ratio = span.TotalMilliseconds / StartToSilence.TotalMilliseconds;
                        var verse = lblVerse.Text;
                        lblVerse.Keyword = verse.Substring(0, (int)(ratio * verse.Length));
                        lblVerse.Invalidate();
                        progressBar.Value = (int)(ratio * 100);
                    }
                    break;
                case TimingStep.SilenceToStop:
                    if (
                        (_firstVerse && span >= SilenceToStop)
                        ||
                        (!_firstVerse && span >= (SilenceToStop + SilenceToStop))
                        )
                    {
                        _CurVerse++;
                        if (_CurVerse < _Verses.Count)
                        {
                            lblVerse.Text = _Verses[_CurVerse]._Text;

                            if ((_CurVerse + 1) < _Verses.Count)
                            {
                                lblNextVerse.Text = _Verses[_CurVerse + 1]._Text;
                            }
                            else
                            {
                                lblNextVerse.Text = "پایان";
                            }
                        }
                        _firstVerse = !_firstVerse;
                        _step = TimingStep.StartToSilence;
                        _start = DateTime.Now;
                    }

                    break;
            }
        }
    }
}
