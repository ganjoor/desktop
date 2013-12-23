using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;


namespace ganjoor
{
    public partial class AudioFiles : Form
    {
        public AudioFiles(int nPoemId = -1)
        {
            InitializeComponent();


            _PoemId = nPoemId;
            _DbBrowser = new DbBrowser();
        }

        /// <summary>
        /// شعر جاری
        /// </summary>
        private int _PoemId;
        /// <summary>
        /// نحوه اتصال و کار با دیتابیس
        /// </summary>
        private DbBrowser _DbBrowser;

        private const int GRDCOLUMN_IDX_FILEPATH = 0;
        private const int GRDCOLUMN_IDX_SYNCED = 1;

        /// <summary>
        /// فهرست کردن فایلهای صوتی مرتبط
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AudioFiles_Load(object sender, EventArgs e)
        {
            grdList.Rows.Clear();
            foreach (PoemAudio Audio in _DbBrowser.GetPoemAudioFiles(_PoemId))
            {
                AddAudioInfoToGrid(Audio);
            }
        }

        /// <summary>
        /// اضافه کردن اطلاعات یک ردیف به گرید
        /// </summary>
        /// <param name="Audio"></param>
        private void AddAudioInfoToGrid(PoemAudio Audio)
        {
            int nRowIdx = grdList.Rows.Add();
            grdList.Rows[nRowIdx].Cells[GRDCOLUMN_IDX_FILEPATH].Value = Audio.FilePath;
            grdList.Rows[nRowIdx].Cells[GRDCOLUMN_IDX_SYNCED].Value = false;
            grdList.Rows[nRowIdx].Tag = Audio;
        }


        /// <summary>
        /// اضافه کردن فایل صوتی جدید
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "MP3 Files (*.mp3)|*.mp3|WAV Files (*.wav)|*.wav|All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    PoemAudio newAudio = _DbBrowser.AddAudio(_PoemId, openFileDialog.FileName, "");
                    if (newAudio != null)
                        AddAudioInfoToGrid(newAudio);
                }
            }
        }

        /// <summary>
        /// حذف ارتباط فایل صوتی
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (this.grdList.CurrentRow != null)
            {
                if (MessageBox.Show("از حذف ارتباط فایل صوتی ردیفی جاری با شعر اطمینان دارید؟",
                    "تأییدیه",
                    MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (_DbBrowser.DeleteAudio(this.grdList.CurrentRow.Tag as PoemAudio))
                    {
                        this.grdList.Rows.Remove(this.grdList.CurrentRow);
                    }
                }
            }
        }

        /// <summary>
        /// پخش و ایست پخش فایل صوتی
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlayStop_Click(object sender, EventArgs e)
        {
            if (_PoemAudioPlayer != null)
            {
                if (_PoemAudioPlayer.IsPlaying())
                {
                    _PoemAudioPlayer.StopPlayBack();
                    return;
                }

            }
            if (grdList.CurrentRow == null)
            {
                MessageBox.Show("لطفاً ردیفی را انتخاب کنید.");
                return;
            }

            if (_PoemAudioPlayer == null)
            {
                _PoemAudioPlayer = new PoemAudioPlayer();
                _PoemAudioPlayer.PlaybackStarted += new EventHandler(_PoemAudioPlayer_PlaybackStarted);
                _PoemAudioPlayer.PlaybackStopped += new EventHandler<NAudio.Wave.StoppedEventArgs>(_PoemAudioPlayer_PlaybackStopped);
            }
            _PoemAudioPlayer.BeginPlayback(grdList.CurrentRow.Tag as PoemAudio);

        }


        PoemAudioPlayer _PoemAudioPlayer = null;

        private void _PoemAudioPlayer_PlaybackStopped(object sender, NAudio.Wave.StoppedEventArgs e)
        {
            // we want to be always on the GUI thread and be able to change GUI components
            Debug.Assert(!this.InvokeRequired, "PlaybackStopped on wrong thread");
            if (e.Exception != null)
            {
                MessageBox.Show(String.Format("Playback Stopped due to an error {0}", e.Exception.Message));
            }
            btnPlayStop.Text = "پخش";
            btnPlayStop.Image = Properties.Resources.play16;

        }

        private void _PoemAudioPlayer_PlaybackStarted(object sender, EventArgs e)
        {
            btnPlayStop.Text = "ایست";
            btnPlayStop.Image = Properties.Resources.stop16;
        }




        private void AudioFiles_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_PoemAudioPlayer != null)
            {
                _PoemAudioPlayer.CleanUp();
            }
        }






    }
}
