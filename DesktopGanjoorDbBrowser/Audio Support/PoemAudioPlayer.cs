using System;
using System.Diagnostics;
using System.IO;
using NAudio.Wave;

namespace ganjoor
{
    public class PoemAudioPlayer
    {

        #region Public / State Reporting Events
        public event EventHandler PlaybackStarted;
        public event EventHandler<StoppedEventArgs> PlaybackStopped;

        #endregion

        #region internal variables
        private IWavePlayer wavePlayer;
        private AudioFileReader file;
        #endregion

        #region Main Methods


        public PoemAudio PoemAudio;
        /// <summary>
        /// شروع پخش
        /// </summary>
        /// <param name="poemAudio"></param>
        public bool BeginPlayback(PoemAudio poemAudio)
        {
            if (wavePlayer != null)
            {
                Debug.Assert(false);
                return false;
            }
            if (!File.Exists(poemAudio.FilePath))
            {
                return false;
            }
            PoemAudio = poemAudio;
            wavePlayer = new WaveOut();
            file = new AudioFileReader(poemAudio.FilePath);
            wavePlayer.Init(file);
            wavePlayer.PlaybackStopped += wavePlayer_PlaybackStopped;
            wavePlayer.Play();

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
            if (wavePlayer == null)
                return false;
            wavePlayer.Pause();
            return true;

        }

        /// <summary>
        /// ادامۀ پخش
        /// </summary>
        /// <returns></returns>
        public bool ResumePlayBack()
        {
            if (wavePlayer == null)
                return false;
            wavePlayer.Play();
            return true;
        }

        /// <summary>
        /// توقف پخش
        /// </summary>
        public bool StopPlayBack()
        {
            if (wavePlayer == null)
                return false;
            wavePlayer.Stop();
            return true;
        }

        /// <summary>
        /// آیا در حال پخش است؟
        /// </summary>
        /// <returns></returns>
        public bool IsPlaying => wavePlayer != null && wavePlayer.PlaybackState == PlaybackState.Playing;

        /// <summary>
        /// آیا در حال توقف موقت است؟
        /// </summary>
        /// <returns></returns>
        public bool IsInPauseState => wavePlayer != null && wavePlayer.PlaybackState == PlaybackState.Paused;

        /// <summary>
        /// محل فعلی فایل صوتی بر حسب میلی ثانیه
        /// </summary>
        public int PositionInMiliseconds
        {
            get => file == null ? 0 : (int)file.CurrentTime.TotalMilliseconds;
            set
            {
                if (file != null)
                    file.CurrentTime = TimeSpan.FromMilliseconds(value);
            }
        }

        /// <summary>
        /// طول فایل بر حسب میلی ثانیه
        /// </summary>
        public int TotalTimeInMiliseconds => file == null ? 0 : (int)file.TotalTime.TotalMilliseconds;

        /// <summary>
        /// پاکسازی متغیرها
        /// </summary>
        public void CleanUp()
        {
            if (file != null)
            {
                file.Dispose();
                file = null;
            }
            if (wavePlayer != null)
            {
                wavePlayer.Dispose();
                wavePlayer = null;
            }
        }
        #endregion

        #region internal events

        private void wavePlayer_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            // we want it to be safe to clean up input stream and playback device in the handler for PlaybackStopped
            CleanUp();
            if (PlaybackStopped != null)
                PlaybackStopped(sender, e);
        }
        #endregion


    }
}
