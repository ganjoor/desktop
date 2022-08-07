using NAudio.Wave;
using System;
using System.Diagnostics;
using System.IO;


namespace ganjoor
{
    public class PoemAudioPlayer
    {
        #region Constructor
        public PoemAudioPlayer()
        {
        }
        #endregion

        #region Public / State Reporting Events
        public event EventHandler PlaybackStarted = null;
        public event EventHandler<StoppedEventArgs> PlaybackStopped = null;

        #endregion

        #region internal variables
        private IWavePlayer wavePlayer = null;
        private AudioFileReader file = null;
        #endregion

        #region Main Methods


        public PoemAudio PoemAudio;
        /// <summary>
        /// شروع پخش
        /// </summary>
        /// <param name="poemAudio"></param>
        public bool BeginPlayback(PoemAudio poemAudio)
        {
            if (this.wavePlayer != null)
            {
                Debug.Assert(false);
                return false;
            }
            if (!File.Exists(poemAudio.FilePath))
            {
                return false;
            }
            this.PoemAudio = poemAudio;
            this.wavePlayer = new WaveOut();
            this.file = new AudioFileReader(poemAudio.FilePath);
            this.wavePlayer.Init(file);
            this.wavePlayer.PlaybackStopped += wavePlayer_PlaybackStopped;
            this.wavePlayer.Play();

            if (PlaybackStarted != null)
                PlaybackStarted(this, new EventArgs());
            return true;
        }

        /// <summary>
        /// توقف موقت پخش
        /// </summary>
        /// <returns></returns>
        public bool PausePlayBack()
        {
            if (this.wavePlayer == null)
                return false;
            this.wavePlayer.Pause();
            return true;

        }

        /// <summary>
        /// ادامۀ پخش
        /// </summary>
        /// <returns></returns>
        public bool ResumePlayBack()
        {
            if (this.wavePlayer == null)
                return false;
            this.wavePlayer.Play();
            return true;
        }

        /// <summary>
        /// توقف پخش
        /// </summary>
        public bool StopPlayBack()
        {
            if (this.wavePlayer == null)
                return false;
            this.wavePlayer.Stop();
            return true;
        }

        /// <summary>
        /// آیا در حال پخش است؟
        /// </summary>
        /// <returns></returns>
        public bool IsPlaying => this.wavePlayer != null && this.wavePlayer.PlaybackState == PlaybackState.Playing;

        /// <summary>
        /// آیا در حال توقف موقت است؟
        /// </summary>
        /// <returns></returns>
        public bool IsInPauseState => this.wavePlayer != null && this.wavePlayer.PlaybackState == PlaybackState.Paused;

        /// <summary>
        /// محل فعلی فایل صوتی بر حسب میلی ثانیه
        /// </summary>
        public int PositionInMiliseconds
        {
            get => this.file == null ? 0 : (int)file.CurrentTime.TotalMilliseconds;
            set
            {
                if (this.file != null)
                    file.CurrentTime = TimeSpan.FromMilliseconds(value);
            }
        }

        /// <summary>
        /// طول فایل بر حسب میلی ثانیه
        /// </summary>
        public int TotalTimeInMiliseconds => this.file == null ? 0 : (int)file.TotalTime.TotalMilliseconds;

        /// <summary>
        /// پاکسازی متغیرها
        /// </summary>
        public void CleanUp()
        {
            if (this.file != null)
            {
                this.file.Dispose();
                this.file = null;
            }
            if (this.wavePlayer != null)
            {
                this.wavePlayer.Dispose();
                this.wavePlayer = null;
            }
        }
        #endregion

        #region internal events

        private void wavePlayer_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            // we want it to be safe to clean up input stream and playback device in the handler for PlaybackStopped
            CleanUp();
            if (this.PlaybackStopped != null)
                this.PlaybackStopped(sender, e);
        }
        #endregion


    }
}
