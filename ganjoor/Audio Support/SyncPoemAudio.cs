﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using ganjoor.Properties;
using NAudio.Wave;
using System.Linq;
using ganjoor.Audio_Support;
using static System.Windows.Forms.LinkLabel;
using Newtonsoft.Json;

namespace ganjoor
{
    public partial class SyncPoemAudio : Form
    {
        public SyncPoemAudio(DbBrowser dbBrowser, PoemAudio poemAudio)
        {
            InitializeComponent();

            _DbBrowser = dbBrowser;
            _PoemAudio = poemAudio;

            _PoemAudioPlayer = new PoemAudioPlayer();
            _PoemAudioPlayer.PlaybackStarted += new EventHandler(_PoemAudioPlayer_PlaybackStarted);
            _PoemAudioPlayer.PlaybackStopped += new EventHandler<NAudio.Wave.StoppedEventArgs>(_PoemAudioPlayer_PlaybackStopped);

            _Modified = false;
            _Saved = false;
            _SyncOrder = -1;
            _LastSearchText = "";
            _Modifying = false;
            _PoemVerses = _DbBrowser.GetVerses(poemAudio.PoemId).ToArray();
            if (poemAudio.SyncArray != null)
            { 
                _VerseMilisecPositions = new List<PoemAudio.SyncInfo>(poemAudio.SyncArray);

                if(_VerseMilisecPositions.Count > 0 && 
                    _VerseMilisecPositions[_VerseMilisecPositions.Count - 1].VerseOrder != (_PoemVerses[_PoemVerses.Length - 1]._Order - 1)
                    )
                {
                    if (MessageBox.Show("آیا تمایل دارید همگامسازی را از آخرین نقطهٔ همگام شده ادامه دهید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.Yes)
                    {

                        if (!_PoemAudioPlayer.BeginPlayback(_PoemAudio))
                        {
                            MessageBox.Show("خطایی در پخش فایل صوتی رخ داد. لطفا چک کنید فایل در مسیر تعیین شده قرار داشته باشد.");                            
                        }
                        else
                        {
                            _Modifying = true;

                            btnPlayPause.Text = "توقف";
                            btnPlayPause.Image = Properties.Resources.pause;
                            timer.Start();
                            trackBar.Maximum = _PoemAudioPlayer.TotalTimeInMiliseconds;


                            _PoemAudioPlayer.PositionInMiliseconds = _VerseMilisecPositions[_VerseMilisecPositions.Count - 1].AudioMiliseconds;                            
                            _TrackbarValueSetting = true;
                            trackBar.Value = _PoemAudioPlayer.PositionInMiliseconds;
                            _TrackbarValueSetting = false;
                            trackBar.Enabled = true;
                            btnPlayPause_Click(null, null);



                            _SyncOrder = _VerseMilisecPositions[_VerseMilisecPositions.Count - 1].VerseOrder;

                            if (_SyncOrder >= 0 && _SyncOrder < _PoemVerses.Length)
                            {
                                if (_SyncOrder < _PoemVerses.Length - 1)
                                    lblNextVerse.Text = "مصرع بعد: " + _PoemVerses[_SyncOrder + 1]._Text;
                                else
                                    lblNextVerse.Text = "این مصرع آخر است.";

                                lblVerse.Text = _PoemVerses[_VerseMilisecPositions[_SyncOrder].VerseOrder]._Text;
                            }

                        }
                    }
                   
                }

            }
            else
                _VerseMilisecPositions = new List<PoemAudio.SyncInfo>();

           

            EnableButtons();

        }

        private void EnableButtons()
        {
            btnSave.Enabled = _Modified;
            btnPreVerse.Enabled = btnTrack.Enabled = btnNextVerse.Enabled = btnPlayPause.Enabled = btnStartFromHere.Enabled = btnStopHere.Enabled = btnSearchText.Enabled = _Modifying || _VerseMilisecPositions.Count == 0;
            btnTest.Enabled = btnReset.Enabled = !btnNextVerse.Enabled;
        }

        private bool _WordMode = false;
        private int _SyncOrder;
        private int _WordOrder;
        private GanjoorVerse[] _PoemVerses;
        private List<PoemAudio.SyncInfo> _VerseMilisecPositions;
        private bool _Modified;
        private bool _Saved;
        private bool _Modifying;
        private string _LastSearchText;

        /// <summary>
        /// آیا اطلاعات ذخیره شده است
        /// </summary>
        public bool Saved
        {
            get { return _Saved; }
        }


        /// <summary>
        /// نمایش شکل موج
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SyncPoemAudio_Load(object sender, EventArgs e)
        {
            DisplayWaveForm();
            if(_PoemVerses.Length > 0)
                lblNextVerse.Text = "مصرع بعد: " + _PoemVerses[0]._Text;
        }

        /// <summary>
        /// نمایش / عدم نمایش شکل موج
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkWaveForm_Click(object sender, EventArgs e)
        {
            waveViewer.Visible = !waveViewer.Visible;
            trackBar.Visible = !waveViewer.Visible;
            chkWaveForm.Checked = waveViewer.Visible;
            DisplayWaveForm();
        }

        /// <summary>
        /// نمایش شکل موج
        /// </summary>
        private void DisplayWaveForm()
        {
            if (chkWaveForm.Checked)
            {
                if (waveViewer.WaveStream == null)
                {
                    if (File.Exists(_PoemAudio.FilePath))
                    {
                        this.Cursor = Cursors.WaitCursor;
                        Application.DoEvents();

                        
                        string ext = Path.GetExtension(_PoemAudio.FilePath).ToLower();
                        if (ext == ".mp3")
                        {
                            waveViewer.WaveStream = new NAudio.Wave.Mp3FileReader(_PoemAudio.FilePath);
                        }
                        else
                            if (ext == ".wav")
                            {
                                waveViewer.WaveStream = new NAudio.Wave.WaveFileReader(_PoemAudio.FilePath);
                            }                       

                        //waveViewer.WaveStream = new NAudio.Wave.AudioFileReader(_PoemAudio.FilePath);
                        waveViewer.FitToScreen();
                        waveViewer.OnPositionChanged += new EventHandler(waveViewer_OnPositionChanged);
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void waveViewer_OnPositionChanged(object sender, EventArgs e)
        {
            if (_PoemAudioPlayer.IsPlaying)
            {
                _PoemAudioPlayer.PositionInMiliseconds = (int)(waveViewer.Position / (float)waveViewer.Width * _PoemAudioPlayer.TotalTimeInMiliseconds);
            }
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            if (_TrackbarValueSetting)
                return;
            if (_PoemAudioPlayer.IsPlaying)
            {
                _PoemAudioPlayer.PositionInMiliseconds = trackBar.Value;
            }

        }




        /// <summary>
        /// مصرع بعد
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextVerse_Click(object sender, EventArgs e)
        {
            bool increaseSyncOrder = true;
            if(_WordMode && _SyncOrder != -1)
            {
                if(_WordOrder < (_PoemVerses[_SyncOrder]._Text.Split(' ').Length -1 ))
                {
                    _WordOrder++;
                    increaseSyncOrder = false;
                }
            }
            if(increaseSyncOrder)
            {
                _SyncOrder++;
                _WordOrder = 0;
            }
            if (_SyncOrder < _PoemVerses.Length)
            {
                if (btnTrack.Checked)
                {
                    _VerseMilisecPositions.Add
                        (
                        new PoemAudio.SyncInfo()
                        {
                            VerseOrder = _SyncOrder,
                            AudioMiliseconds = _PoemAudioPlayer.PositionInMiliseconds
                        }
                        );
                }

                lblVerse.Text = _WordMode ? _PoemVerses[_SyncOrder]._Text.Split(' ')[_WordOrder] :  _PoemVerses[_SyncOrder]._Text;

                if (_SyncOrder < _PoemVerses.Length - 1)
                    lblNextVerse.Text = "مصرع بعد: " + _PoemVerses[_SyncOrder + 1]._Text;
                else
                    lblNextVerse.Text = "این مصرع آخر است.";
            }
            else
            {
                _SyncOrder = _PoemVerses.Length;
                lblVerse.Text = "شعر تمام شده!";
            }
            _Modified = true;
        }

        private void btnPreVerse_Click(object sender, EventArgs e)
        {
            _SyncOrder--;
            if (_SyncOrder > 0)
            {
                if (btnTrack.Checked)
                {
                    _VerseMilisecPositions.Add
                        (
                        new PoemAudio.SyncInfo()
                        {
                            VerseOrder = _SyncOrder,
                            AudioMiliseconds = _PoemAudioPlayer.PositionInMiliseconds
                        }
                        );
                }

                lblVerse.Text = _PoemVerses[_SyncOrder]._Text;

                if (_SyncOrder < _PoemVerses.Length - 1)
                    lblNextVerse.Text = "مصرع بعد: " + _PoemVerses[_SyncOrder + 1]._Text;
                else
                    lblNextVerse.Text = "این مصرع آخر است.";
            }
            else
            {
                _SyncOrder = -1;
                lblVerse.Text = "هنوز شروع نشده!";
            }
            _Modified = true;

        }

        private void btnSearchText_Click(object sender, EventArgs e)
        {
            using (ItemEditor itemEditor = new ItemEditor(EditItemType.General, "جستجوی بعدی", "متن:"))
            {
                itemEditor.ItemName = _LastSearchText;
                if (itemEditor.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    _LastSearchText = itemEditor.ItemName;
                    int nStart = _SyncOrder;
                    for (int n = 0; n < 2; n++)
                    {
                        for (int i = nStart + 1; i < _PoemVerses.Length; i++)
                            if (_PoemVerses[i]._Text.Contains(_LastSearchText))
                            {
                                _SyncOrder = i;

                                if (btnTrack.Checked)
                                {
                                    _VerseMilisecPositions.Add
                                        (
                                        new PoemAudio.SyncInfo()
                                        {
                                            VerseOrder = _SyncOrder,
                                            AudioMiliseconds = _PoemAudioPlayer.PositionInMiliseconds
                                        }
                                        );
                                }

                                lblVerse.Text = _PoemVerses[_SyncOrder]._Text;

                                if (_SyncOrder < _PoemVerses.Length - 1)
                                    lblNextVerse.Text = "مصرع بعد: " + _PoemVerses[_SyncOrder + 1]._Text;
                                else
                                    lblNextVerse.Text = "این مصرع آخر است.";
                                _Modified = true;
                                return;
                            }
                        if (n == 0)
                        {
                            if (MessageBox.Show("متن یافت نشد. آیا تمایل دارید جستجو از ابتدای شعر صورت گیرد؟", "تأییدیه", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                            {
                                return;
                            }
                            nStart = -1;
                        }
                        else
                        {
                            MessageBox.Show("متن یافت نشد.", "خطا", MessageBoxButtons.OK);
                        }
                    }
                }
            }

        }



        private void SyncPoemAudio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_Modified)
            {
                if (MessageBox.Show("تغییرات ذخیره نشده‌اند. فرم را می‌بندید؟", "تأییدیه", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            _PoemAudioPlayer.CleanUp();
            if (waveViewer.WaveStream != null)
            {
                waveViewer.WaveStream.Dispose();
                waveViewer.WaveStream = null;
            }
        }


        private DbBrowser _DbBrowser;
        private PoemAudio _PoemAudio;

        #region Audio Playback
        private PoemAudioPlayer _PoemAudioPlayer;
        private bool _TrackbarValueSetting = false;

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            btnTest.Enabled = false;
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

            btnTest.Text = "آزمایش";
            btnTest.Image = Properties.Resources.sound;

            _SyncOrder = -1;


            _Modifying = false;
            EnableButtons();

            _TrackbarValueSetting = true;
            trackBar.Value = 0;
            _TrackbarValueSetting = false;
            trackBar.Enabled = false;
            _Testing = false;
            if (timer.Enabled)
                timer.Stop();
            

        }

        private void _PoemAudioPlayer_PlaybackStarted(object sender, EventArgs e)
        {
        }


        #endregion

        private void btnTest_Click(object sender, EventArgs e)
        {
            
            if (_PoemAudioPlayer.IsPlaying || _PoemAudioPlayer.IsInPauseState)
            {
                _PoemAudioPlayer.StopPlayBack();
                _SyncOrder = -1;
                return;
            }
            btnPreVerse.Enabled = btnTrack.Enabled =  btnStartFromHere.Enabled = btnPlayPause.Enabled = btnNextVerse.Enabled = btnStopHere.Enabled = false;
            btnReset.Enabled = false;

            if (!_PoemAudioPlayer.BeginPlayback(_PoemAudio))
            {
                MessageBox.Show("خطایی در پخش فایل صوتی رخ داد. لطفا چک کنید فایل در مسیر تعیین شده قرار داشته باشد.");
                EnableButtons();
                return;
            }

            //رفع اشکال نسخه قدیمی NAudio           
            int nLen = _VerseMilisecPositions.Count;
            if (nLen > 0 && _VerseMilisecPositions[nLen - 1].AudioMiliseconds > _PoemAudioPlayer.TotalTimeInMiliseconds)
            {
                for (int i = 0; i < nLen; i++)
                {
                    _VerseMilisecPositions[i] = new PoemAudio.SyncInfo()
                    {
                        AudioMiliseconds = _VerseMilisecPositions[i].AudioMiliseconds / 2,
                        VerseOrder = _VerseMilisecPositions[i].VerseOrder
                    };
                }
            }

            _SyncOrder = -1;
            _Testing = true;
            trackBar.Maximum = _PoemAudioPlayer.TotalTimeInMiliseconds;
            _TrackbarValueSetting = true;
            trackBar.Value = 0;
            _TrackbarValueSetting = false;
            trackBar.Enabled = true;
            timer.Start();
            btnTest.Text = "توقف";
            btnTest.Image = Properties.Resources.pause;

            if (_VerseMilisecPositions.Count > 0)
            {
                if (_VerseMilisecPositions[0].VerseOrder == -1)
                {
                    _PoemAudioPlayer.PositionInMiliseconds = _VerseMilisecPositions[0].AudioMiliseconds;
                    _TrackbarValueSetting = true;
                    trackBar.Value = _PoemAudioPlayer.PositionInMiliseconds;
                    _TrackbarValueSetting = false;
                    _SyncOrder++;
                }

            }



        }

        private bool _Testing = false;

        private void timer_Tick(object sender, EventArgs e)
        {
            int nPositionInMiliseconds = _PoemAudioPlayer.PositionInMiliseconds;
             _TrackbarValueSetting = true;
            trackBar.Value = nPositionInMiliseconds;
            _TrackbarValueSetting = false;
            waveViewer.Position = nPositionInMiliseconds;
            lblTime.Text = TimeSpan.FromMilliseconds(nPositionInMiliseconds).ToString();
            if (!_Testing)
                return;

            int nNextSyncOrder = _SyncOrder + 1;
            if (nNextSyncOrder < _VerseMilisecPositions.Count)
            {

                if (_PoemAudioPlayer.PositionInMiliseconds > _VerseMilisecPositions[nNextSyncOrder].AudioMiliseconds)
                    if (nNextSyncOrder < _PoemVerses.Length)
                    {
                        if(_VerseMilisecPositions[nNextSyncOrder].VerseOrder< 0)
                        {
                            _PoemAudioPlayer.StopPlayBack();
                            return;
                        }
                        lblVerse.Text = _PoemVerses[_VerseMilisecPositions[nNextSyncOrder].VerseOrder]._Text;
                        _SyncOrder++;
                        if (_SyncOrder < _PoemVerses.Length - 1)
                            lblNextVerse.Text = "مصرع بعد: " + _PoemVerses[_SyncOrder + 1]._Text;
                        else
                            lblNextVerse.Text = "این مصرع آخر است.";
                    }
                    else
                    {
                        lblVerse.Text = "پایان شعر";
                    }
            }            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_VerseMilisecPositions.Count == 0)
            {
                if (MessageBox.Show("ذخیره باعث از دست رفتن اطلاعات همگام‌سازی می شود. اطمینان دارید؟", "تأییدیه", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }
            _DbBrowser.SavePoemSync(_PoemAudio, _VerseMilisecPositions.ToArray(), true);
            _Saved = true;
            _Modified = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
                if(MessageBox.Show("اطلاعات مکان مصرعها در فایل صوتی از بین خواهد رفت. اطمینان دارید؟", "تأییدیه", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    _SyncOrder = -1;
                    _VerseMilisecPositions = new List<PoemAudio.SyncInfo>();
                    lblVerse.Text = "هنوز شروع نشده!";
                    if (_PoemVerses.Length > 0)
                        lblNextVerse.Text = "مصرع بعد: " + _PoemVerses[0]._Text;
                    else
                        lblNextVerse.Text = "این مصرع آخر است.";

                    _Modified = true;                    
                    EnableButtons();                    
                }
        }

        private void btnStartFromHere_Click(object sender, EventArgs e)
        {
            if (_PoemAudioPlayer.IsPlaying || _PoemAudioPlayer.IsInPauseState)
            {
                _VerseMilisecPositions.Add
                    (
                    new PoemAudio.SyncInfo()
                    {
                        VerseOrder = -1,
                        AudioMiliseconds = _PoemAudioPlayer.PositionInMiliseconds
                    }
                    );
                btnStartFromHere.Enabled = false;
            }
            else
            {
                MessageBox.Show("ابتدا فایل صوتی را پخش کنید و به نقطۀ مورد نظر ببرید.");
            }
        }

        private void btnStopHere_Click(object sender, EventArgs e)
        {
            if (_PoemAudioPlayer.IsPlaying || _PoemAudioPlayer.IsInPauseState)
            {
                _VerseMilisecPositions.Add
                    (
                    new PoemAudio.SyncInfo()
                    {
                        VerseOrder = -2,
                        AudioMiliseconds = _PoemAudioPlayer.PositionInMiliseconds
                    }
                    );
                _PoemAudioPlayer.StopPlayBack();
            }
            else
            {
                MessageBox.Show("ابتدا فایل صوتی را پخش کنید و به نقطۀ مورد نظر ببرید.");
            }
        }

        private void chkShowNextVerse_Click(object sender, EventArgs e)
        {
            lblNextVerse.Visible = !lblNextVerse.Visible;
            chkShowNextVerse.Checked = lblNextVerse.Visible;
        }

        private void btnTrack_Click(object sender, EventArgs e)
        {
            btnTrack.Checked = !btnTrack.Checked;
            btnTrack.Image = btnTrack.Checked ? Properties.Resources.track32 : Properties.Resources.notrack32;
            btnTrack.Text = btnTrack.Checked ? "رهگیری" : "عدم رهگیری";
        }

        private void SyncPoemAudio_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (e.Control)
                {
                    btnPreVerse_Click(sender, new EventArgs());
                }
                else
                {
                    btnNextVerse_Click(sender, new EventArgs());
                }                
            }
            else
                if (e.Control)
                {
                    if (e.KeyCode == Keys.P)
                    {
                        btnPlayPause_Click(sender, new EventArgs());
                    }
                    else
                        if (e.KeyCode == Keys.F)
                        {
                            btnSearchText_Click(sender, new EventArgs());
                        }
                }


                
        }

        private void chkWordMode_Click(object sender, EventArgs e)
        {
            _WordMode = !_WordMode;
            chkWordMode.Checked = _WordMode;
        }

        private void btnSrtOutput_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "SRT Files (*.srt)|*.srt";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    CreateSubTitle(dlg.FileName);
                }
            }
        }

        private void CreateSubTitle(string outfilePath)
        {

            List<string> lines = CreateSubTitle();

            if (lines != null)
                File.WriteAllLines(outfilePath, lines.ToArray());



            if (lines != null)
                MessageBox.Show("زیرنویس ایجاد شد.", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);

        }

        private List<string> CreateSubTitle()
        {

            Mp3FileReader r = new Mp3FileReader(_PoemAudio.FilePath);
            int playtime = r.TotalTime.Milliseconds;

            using (Mp3FileReader mp3 = new Mp3FileReader(_PoemAudio.FilePath))
            {
                playtime = (int)mp3.TotalTime.TotalMilliseconds;
            }
            List<string> lines = new List<string>();
            int nIdxSrtLine = 0;
            for (int i = 0; i < _VerseMilisecPositions.Count; i++)
            {

                nIdxSrtLine++;
                DateTime dtStart = new DateTime(0).AddMilliseconds(_VerseMilisecPositions[i].AudioMiliseconds);
                int duration = i != (_VerseMilisecPositions.Count - 1) ? _VerseMilisecPositions[i + 1].AudioMiliseconds  - _VerseMilisecPositions[i].AudioMiliseconds : (playtime - _VerseMilisecPositions[i].AudioMiliseconds);                
                DateTime dtEnd = dtStart.AddMilliseconds(duration);


                lines.Add(nIdxSrtLine.ToString());
                lines.Add(
                    dtStart.ToString("HH:mm:ss,fff")
                    + " --> "
                    + dtEnd.ToString("HH:mm:ss,fff")
                    );
                lines.Add(_PoemVerses.Where(v => v._Order == (_VerseMilisecPositions[i].VerseOrder + 1)).Any() ?
                    _PoemVerses.Where(v => v._Order == (_VerseMilisecPositions[i].VerseOrder + 1)).First()._Text : "");
                lines.Add("");



            }

            return lines;

        }

        private void btnSaveJson_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "JSON Files (*.json)|*.json";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    string[] translations = { };
                    using(OpenFileDialog tlg = new OpenFileDialog())
                    {
                        tlg.Filter = "Text Files (*.txt)|*.txt";
                        if(tlg.ShowDialog(this) == DialogResult.OK)
                        {
                            translations = File.ReadAllLines(tlg.FileName);
                            if(translations.Length != _PoemVerses.Length)
                            {
                                MessageBox.Show("translations.Length != _PoemVerses.Length");
                                return;
                            }
                        }
                    }
                    Mp3FileReader r = new Mp3FileReader(_PoemAudio.FilePath);
                    int playtime = r.TotalTime.Milliseconds;

                    using (Mp3FileReader mp3 = new Mp3FileReader(_PoemAudio.FilePath))
                    {
                        playtime = (int)mp3.TotalTime.TotalMilliseconds;
                    }
                    var poem = _DbBrowser.GetPoem(_PoemAudio.PoemId);
                    var cat = _DbBrowser.GetCategory(poem._CatID);
                    var poet = _DbBrowser.GetPoet(cat._PoetID);
                    LyricsModel model = new LyricsModel()
                    {
                        Title = poem._Title,
                        Artist = poet._Name,
                        Lines = new List<LyricsLineModel>(),
                    };
                    int verseOrder = -1;
                    int wordOrder = 0;
                    for (int i = 0; i < _VerseMilisecPositions.Count; i++)
                    {
                        string text = _PoemVerses.Where(v => v._Order == (_VerseMilisecPositions[i].VerseOrder + 1)).Any() ?
                               _PoemVerses.Where(v => v._Order == (_VerseMilisecPositions[i].VerseOrder + 1)).First()._Text : "";

                        int duration = i != (_VerseMilisecPositions.Count - 1) ? _VerseMilisecPositions[i + 1].AudioMiliseconds - _VerseMilisecPositions[i].AudioMiliseconds : (playtime - _VerseMilisecPositions[i].AudioMiliseconds);
                        if (verseOrder != _VerseMilisecPositions[i].VerseOrder)
                        {
                            verseOrder = _VerseMilisecPositions[i].VerseOrder;
                            wordOrder = 0;
                           
                            LyricsLineModel line = new LyricsLineModel()
                            {
                                Line = text,
                                Translation = translations.Length > 0 ? translations[_PoemVerses.Where(v => v._Order == (_VerseMilisecPositions[i].VerseOrder + 1)).First()._Order - 1] : "",
                                StartInMilliseconds = _VerseMilisecPositions[i].AudioMiliseconds,
                                EndInMilliseconds = _VerseMilisecPositions[i].AudioMiliseconds + duration,
                                Words = new List<LyricsWordModel>(),
                            };
                            line.Words.Add(
                                 new LyricsWordModel()
                                 {
                                     Word = text.Split(' ')[wordOrder],
                                     StartInMilliseconds = line.StartInMilliseconds,
                                     EndInMilliseconds = line.EndInMilliseconds,
                                 });
                            model.Lines.Add(line);
                        }
                        else
                        {
                            wordOrder++;
                            LyricsLineModel line = model.Lines[model.Lines.Count - 1];
                            line.EndInMilliseconds += duration;
                            line.Words.Add(
                               new LyricsWordModel()
                               {
                                   Word = text.Split(' ')[wordOrder],
                                   StartInMilliseconds = _VerseMilisecPositions[i].AudioMiliseconds,
                                   EndInMilliseconds = _VerseMilisecPositions[i].AudioMiliseconds + duration,
                               });
                        }
                    }
                    using(StreamWriter tw = new StreamWriter(dlg.FileName))
                    {
                        var j = new JsonSerializer();
                        j.Formatting = Formatting.Indented;
                        j.Serialize(tw, model);
                    }
                    MessageBox.Show("خروجی تولید شد.");

                }
            }
        }
    }
}
