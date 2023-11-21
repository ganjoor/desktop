using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                var verse = _PoemVerses.Where(v => v._Order == miliSec.VerseOrder).FirstOrDefault();
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
    }
}
