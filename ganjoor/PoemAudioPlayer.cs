using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Diagnostics;


namespace ganjoor
{
    public class PoemAudioPlayer
    {
        public PoemAudioPlayer()
        {
        }

        public event EventHandler PlaybackStarted = null;
        public event EventHandler<StoppedEventArgs> PlaybackStopped = null;

        private IWavePlayer wavePlayer = null;
        private AudioFileReader file = null;


        public void StopPlayBack()
        {
            this.wavePlayer.Stop();
        }

        public bool IsPlaying()
        {
            return this.wavePlayer != null && this.wavePlayer.PlaybackState == PlaybackState.Playing;
        }

        public void BeginPlayback(PoemAudio poemAudio)
        {
            Debug.Assert(this.wavePlayer == null);
            this.wavePlayer = new WaveOut();
            this.file = new AudioFileReader(poemAudio.FilePath);
            //this.file.Volume = 1;
            this.wavePlayer.Init(file);
            this.wavePlayer.PlaybackStopped += wavePlayer_PlaybackStopped;
            this.wavePlayer.Play();

            if (PlaybackStarted != null)
                PlaybackStarted(this, new EventArgs());
        }

        private void wavePlayer_PlaybackStopped(object sender, StoppedEventArgs e)
        {


            // we want it to be safe to clean up input stream and playback device in the handler for PlaybackStopped
            CleanUp();
            if (this.PlaybackStopped != null)
                this.PlaybackStopped(sender, e);

        }

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

    }
}
