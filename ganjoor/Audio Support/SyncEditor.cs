
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ganjoor.Audio_Support
{
    public partial class SyncEditor : Form
    {
        public SyncEditor(DbBrowser dbBrowser, PoemAudio poemAudio)
        {
            InitializeComponent();
            _PoemAudio = poemAudio;
            _DbBrowser = dbBrowser;
            _PoemAudioPlayer = new PoemAudioPlayer();
            _PoemAudioPlayer.PlaybackStarted += new EventHandler(_PoemAudioPlayer_PlaybackStarted);
            _PoemAudioPlayer.PlaybackStopped += new EventHandler<NAudio.Wave.StoppedEventArgs>(_PoemAudioPlayer_PlaybackStopped);
            _PoemVerses = _DbBrowser.GetVerses(poemAudio.PoemId).ToArray();
            if (poemAudio.SyncArray != null)
            {
                _VerseMilisecPositions = new List<PoemAudio.SyncInfo>(poemAudio.SyncArray);
            }
            else
                _VerseMilisecPositions = new List<PoemAudio.SyncInfo>();

            for(int i=0; i<_VerseMilisecPositions.Count; i++)
            {
                var miliSec = _VerseMilisecPositions[i];
                var verse = _PoemVerses.Where(v => v._Order == (miliSec.VerseOrder + 1)).FirstOrDefault();
                if(verse != null)
                {
                    miliSec.VerseText = verse._Text;
                }
                cmbAudioInMiliseconds.Items.Add(miliSec);
            }
            
        }
        private DbBrowser _DbBrowser;
        private PoemAudio _PoemAudio;
        private GanjoorVerse[] _PoemVerses;
        private List<PoemAudio.SyncInfo> _VerseMilisecPositions;
        #region Audio Playback
        private PoemAudioPlayer _PoemAudioPlayer;
        private bool _TrackbarValueSetting = false;

        private void EnableButtons()
        {
            
        }

        private void _PoemAudioPlayer_PlaybackStopped(object sender, NAudio.Wave.StoppedEventArgs e)
        {
            // we want to be always on the GUI thread and be able to change GUI components
            Debug.Assert(!this.InvokeRequired, "PlaybackStopped on wrong thread");
            if (e.Exception != null)
            {
                MessageBox.Show(String.Format("Playback Stopped due to an error {0}", e.Exception.Message));
            }
            btnPlayPause.Text = "پخش صدا";
            btnPlayPause.Image = Properties.Resources.sound;



        }

        private void _PoemAudioPlayer_PlaybackStarted(object sender, EventArgs e)
        {
        }



        #endregion

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            
            if (_PoemAudioPlayer.IsPlaying)
            {
                _PoemAudioPlayer.PausePlayBack();
                btnPlayPause.Text = "ادامۀ پخش";
                btnPlayPause.Image = Properties.Resources.play;
                btnSave.Enabled = true;
                return;
            }
            if (_PoemAudioPlayer.IsInPauseState)
            {
                btnPlayPause.Text = "توقف";
                btnPlayPause.Image = Properties.Resources.pause;
                _PoemAudioPlayer.ResumePlayBack();
                return;
            }
            if (!_PoemAudioPlayer.BeginPlayback(_PoemAudio))
            {
                MessageBox.Show("خطایی در پخش فایل صوتی رخ داد. لطفا چک کنید فایل در مسیر تعیین شده قرار داشته باشد.");
                EnableButtons();
                return;
            }


            btnPlayPause.Text = "توقف";
            btnPlayPause.Image = Properties.Resources.pause;
            timer.Start();
            trackBar.Maximum = _PoemAudioPlayer.TotalTimeInMiliseconds;
            _TrackbarValueSetting = true;
            trackBar.Value = 0;
            _TrackbarValueSetting = false;
            trackBar.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            int nPositionInMiliseconds = _PoemAudioPlayer.PositionInMiliseconds;
            _TrackbarValueSetting = true;
            trackBar.Value = nPositionInMiliseconds;
            _TrackbarValueSetting = false;
            
            lblTime.Text = TimeSpan.FromMilliseconds(nPositionInMiliseconds).ToString();

            findItem();
        }

        private void findItem()
        {
            var selectedItem = cmbAudioInMiliseconds.SelectedItem;
            foreach (var item in cmbAudioInMiliseconds.Items)
            {
                if ((item as PoemAudio.SyncInfo?).Value.AudioMiliseconds < _PoemAudioPlayer.PositionInMiliseconds)
                {
                    selectedItem = item;
                }
            }

            if (selectedItem != cmbAudioInMiliseconds.SelectedItem)
            {
                cmbAudioInMiliseconds.SelectedItem = selectedItem;
            }
        }

        private void SyncEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_PoemAudioPlayer.IsPlaying)
            {
                _PoemAudioPlayer.StopPlayBack();
            }
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            if (_TrackbarValueSetting)
                return;
            if (_PoemAudioPlayer.IsPlaying || _SelectMode)
            {
                _PoemAudioPlayer.PositionInMiliseconds = trackBar.Value;
                if(!_SelectMode)
                {
                    findItem();
                }
                
            }
        }

        bool _SelectMode = false;
        private void btnSelectPosition_Click(object sender, EventArgs e)
        {
            if(_SelectMode)
            {
                _SelectMode = false;
                if(cmbAudioInMiliseconds.SelectedItem != null)
                {
                    var syncInfo = (cmbAudioInMiliseconds.SelectedItem as PoemAudio.SyncInfo?).Value;
                    var syncVerse = _VerseMilisecPositions.Where(v => v.AudioMiliseconds == syncInfo.AudioMiliseconds).SingleOrDefault();
                    if(syncVerse.AudioMiliseconds != 0)
                    {
                        syncVerse.AudioMiliseconds = trackBar.Value;
                    }
                    cmbAudioInMiliseconds.Items.Clear();
                    for (int i = 0; i < _VerseMilisecPositions.Count; i++)
                    {
                        var miliSec = _VerseMilisecPositions[i];
                        var verse = _PoemVerses.Where(v => v._Order == (miliSec.VerseOrder + 1)).FirstOrDefault();
                        if (verse != null)
                        {
                            miliSec.VerseText = verse._Text;
                        }
                        cmbAudioInMiliseconds.Items.Add(miliSec);
                    }
                    findItem();
                }
            }
            else
            {
                _SelectMode = true;
            }
        }
    }
}
