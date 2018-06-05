using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ganjoor;
using gsync2vid.Properties;
using System.IO;
using Splicer.Timeline;
using Splicer.Renderer;
using Splicer.WindowsMedia;
using NAudio.Wave;
using System.Net;
using System.Reflection;
using System.Diagnostics;

namespace gsync2vid
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            txtSrcDb.Text = DbPath;
            cmbTransitionEffect.SelectedIndex = Settings.Default.TransitionType;
            chkAAC.Checked = Settings.Default.AACSound;
            chkDebug.Checked = Settings.Default.DebugMode;
            UpdateConnectionStatus();
            UpdatePoemAndAudioInfo();
        }

        #region Vide Background
        private string _VideoBackgroundPath = "";
        #endregion


        #region s3db path

        /// <summary>
        /// اتصال به پایگاه داده ها
        /// </summary>
        /// <returns></returns>
        public DbBrowser Connect()
        {
            try
            {
                DbBrowser db = new DbBrowser(DbPath);
                if (!db.Connected)
                    return null;
                return db;
            }
            catch
            {
                return null;
            }
        }

        private void UpdateConnectionStatus()
        {
            bool bIsConnectionFine = IsConnectionFine;
            lblDbComment.Text =
                bIsConnectionFine ?
                "اتصال موفقیت‌آمیز بود."
                :
                "فایل در مسیر وجود ندارد یا امکان اتصال به آن وجود ندارد. لطفا اطمینان حاصل کنید گنجور رومیزی نصب شده و مسیر فایل درست انتخاب شده است.";

            grpPoem.Enabled = bIsConnectionFine;
            grpVerses.Enabled = bIsConnectionFine;
        }

        /// <summary>
        /// بررسی اتصال
        /// </summary>
        private bool IsConnectionFine
        {
            get
            {
                DbBrowser db = Connect();
                if (db == null)
                    return false;
                db.CloseDb();
                return true;
            }
        }

        /// <summary>
        /// مسیر فایل
        /// </summary>
        private string DbPath
        {
            get
            {
                string dbfilepath = Settings.Default.s3dbpath;
                if (string.IsNullOrEmpty(dbfilepath))
                {
                    string iniFilePath = Path.Combine(@"C:\Program Files\ganjoor", "ganjoor.ini");
                    if (File.Exists(iniFilePath))
                    {
                        GINIParser gParaser = new GINIParser(iniFilePath);
                        try
                        {
                            dbfilepath = gParaser.Values["Database"]["Path"];
                            dbfilepath = Path.Combine(dbfilepath, "ganjoor.s3db");
                        }
                        catch
                        {
                        }
                    }
                }
                return dbfilepath;
            }
            set
            {
                Settings.Default.s3dbpath = value;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// تغییر مسیر فایل
        /// </summary>
        private void btnSelDb_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.FileName = "ganjoor.s3db";
                if (!string.IsNullOrEmpty(txtSrcDb.Text))
                {
                    try
                    {
                        dlg.InitialDirectory = Path.GetDirectoryName(txtSrcDb.Text);
                    }
                    catch
                    {
                    }
                }

                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    txtSrcDb.Text = dlg.FileName;
                    DbPath = txtSrcDb.Text;
                    UpdateConnectionStatus();
                }
            }
        }
        #endregion

        #region Poem+Audio Id

        private void UpdatePoemAndAudioInfo(DbBrowser refDb = null)
        {
            txtPoemId.Text = Settings.Default.PoemId.ToString();
            txtSyncId.Text = Settings.Default.AudioId.ToString();
            decimal nImageWidth = Settings.Default.LastImageWidth;
            decimal nImageHeight = Settings.Default.LastImageHeight;

            if (nImageWidth < txtWidth.Minimum || nImageWidth > txtWidth.Maximum)
                nImageWidth = txtWidth.Minimum;

            if (nImageHeight < txtHeight.Minimum || nImageHeight > txtHeight.Maximum)
                nImageHeight = txtHeight.Minimum;

            txtWidth.Value = nImageWidth;
            txtHeight.Value = nImageHeight;

            DbBrowser db = refDb == null ? Connect() : refDb;
            if (db == null)
                return;

            string strImagePath = Settings.Default.LastImagePath;
            if (!string.IsNullOrEmpty(strImagePath))
                if (!File.Exists(strImagePath))
                    strImagePath = "";


            string strTitle = "";

            GanjoorPoem poem = db.GetPoem(Settings.Default.PoemId);
            if (poem != null)
            {
                GanjoorPoet poet = db.GetPoetForCat(poem._CatID);
                strTitle = poet.ToString();
                strTitle = strTitle + " - " + poem.ToString();

                List<GanjoorVerse> verses = db.GetVerses(poem._ID);

                PoemAudio[] audioFiles = db.GetPoemAudioFiles(poem._ID);
                if (audioFiles.Length == 0)
                {
                    PoemAudio fakeAudio = new PoemAudio()
                    {
                        Id = 0,
                        Description = "",
                    };
                    List<PoemAudio.SyncInfo> fakeSync = new List<PoemAudio.SyncInfo>();
                    foreach (GanjoorVerse v in verses)
                    {
                        fakeSync.Add(new PoemAudio.SyncInfo()
                        {
                            VerseOrder = v._Order-1,
                            AudioMiliseconds = (v._Order * 1000),                            
                        });
                    }
                    fakeAudio.SyncArray = fakeSync.ToArray();
                    audioFiles = new PoemAudio[] { fakeAudio };
                }
                if (audioFiles.Length > 0)
                {
                    foreach(PoemAudio audio in audioFiles)
                        if (audio.Id == Settings.Default.AudioId)
                        {
                            strTitle = strTitle + " - " + audio.Description;
                            txtPoem.Text = strTitle;


                            int nDurationDividedBy = 1;

                            PoemAudioPlayer player = new PoemAudioPlayer();
                            if (audio.Id != 0)
                            {
                                if (!player.BeginPlayback(audio))
                                {
                                    MessageBox.Show("تلاش برای دسترسی و پخش فایل صوتی موفق نبود", "خطا");
                                }
                                else
                                {
                                    player.StopPlayBack();
                                    if (player.TotalTimeInMiliseconds < audio.SyncArray[audio.SyncArray.Length - 1].AudioMiliseconds)
                                        nDurationDividedBy = 2;

                                }
                            }

                            

                            
                            List<GVideoFrame> frames = new List<GVideoFrame>();


                            GVideoFrame titleFrame = new GVideoFrame()
                            {
                                AudioBound = false,
                                StartInMiliseconds = -4000,
                                Text = poet._Name,
                                BackgroundImagePath = strImagePath,
                                MainTextPosRatioPortion = 1,
                                MainTextPosRatioPortionFrom = 4,
                                MainTextHPosRatioPortion = 1,
                                MainTextHPosRatioPortionFrom = 2,
                                MaxTextWidthRatioPortion = 9,
                                MaxTextWidthRatioPortionFrom = 10,
                                BackColor = Color.White,
                                TextColor = Color.White,
                                TextBackColor = Color.Black,
                                BorderColor = Color.Black,
                                TextBackColorAlpha = 100,
                                Shape = GTextBoxShape.Rectangle,
                                TextBackRectThickness = 0,
                                Font = Settings.Default.LastUsedFont,
                                ShowLogo = true
                            };

                            if (audio.SyncArray.Length > 0)
                            {
                                if (audio.SyncArray[0].AudioMiliseconds != 0)
                                {
                                    titleFrame.StartInMiliseconds = -(audio.SyncArray[0].AudioMiliseconds / nDurationDividedBy);
                                }
                            }

                            frames.Add(titleFrame);

                            GVideoFrame poemFrame = new GVideoFrame()
                            {
                                AudioBound = false,
                                StartInMiliseconds = 0,
                                Text = poem._Title,
                                BackgroundImagePath = strImagePath,
                                MainTextPosRatioPortion = 1,
                                MainTextPosRatioPortionFrom = 2,
                                MainTextHPosRatioPortion = 1,
                                MainTextHPosRatioPortionFrom = 2,
                                MaxTextWidthRatioPortion = 9,
                                MaxTextWidthRatioPortionFrom = 10,
                                BackColor = Color.White,
                                TextColor = Color.White,
                                TextBackColor = Color.Black,
                                BorderColor = Color.Black,
                                TextBackColorAlpha = 100,
                                Shape = GTextBoxShape.Rectangle,
                                TextBackRectThickness = 0,
                                Font = Settings.Default.LastUsedFont,
                                MasterFrame = titleFrame,
                                ShowLogo = false
                            };

                            frames.Add(poemFrame);

                            GVideoFrame soundFrame = new GVideoFrame()
                            {
                                AudioBound = false,
                                StartInMiliseconds = 0,
                                Text = audio.Description,
                                BackgroundImagePath = strImagePath,
                                MainTextPosRatioPortion = 3,
                                MainTextPosRatioPortionFrom = 4,
                                MainTextHPosRatioPortion = 1,
                                MainTextHPosRatioPortionFrom = 2,
                                MaxTextWidthRatioPortion = 9,
                                MaxTextWidthRatioPortionFrom = 10,
                                BackColor = Color.White,
                                TextColor = Color.White,
                                TextBackColor = Color.Black,
                                BorderColor = Color.Black,
                                TextBackColorAlpha = 100,
                                Shape = GTextBoxShape.Rectangle,
                                TextBackRectThickness = 0,
                                Font = Settings.Default.LastUsedFont,
                                MasterFrame = poemFrame,
                                ShowLogo = false
                            };

                            frames.Add(soundFrame);

                            



                            foreach (PoemAudio.SyncInfo SyncInfo in audio.SyncArray)
                            {
                                GanjoorVerse verse = null;
                                foreach (GanjoorVerse v in verses)
                                    if (v._Order == (SyncInfo.VerseOrder + 1))
                                    {
                                        verse = v;
                                        break;
                                    }

                                frames.Add(new GVideoFrame()
                                {
                                    AudioBound = true,
                                    StartInMiliseconds = SyncInfo.AudioMiliseconds / nDurationDividedBy,
                                    Text = verse == null ? "" : verse._Text,
                                    BackgroundImagePath = strImagePath,
                                    MainTextPosRatioPortion = 1,
                                    MainTextPosRatioPortionFrom = 2,
                                    MainTextHPosRatioPortion = 1,
                                    MainTextHPosRatioPortionFrom = 2,
                                    MaxTextWidthRatioPortion = 9,
                                    MaxTextWidthRatioPortionFrom = 10,
                                    BackColor = Color.White,
                                    TextColor = Color.White,
                                    TextBackColor = Color.Black,
                                    BorderColor = Color.Black,
                                    TextBackColorAlpha = 100,
                                    Shape = GTextBoxShape.Rectangle,
                                    TextBackRectThickness = 0,
                                    Font = Settings.Default.LastUsedFont,
                                    ShowLogo = false
                                });

                            }

                            cmbVerses.Items.Clear();
                            cmbVerses.Items.AddRange(frames.ToArray());
                            cmbVerses.SelectedIndex = 0;
                            
                            break;
                        }
                }
            }
            
            if (refDb == null)
                db.CloseDb();

            btnProduce.Enabled = btnSubtitle.Enabled = Settings.Default.AudioId != 0;
        }
        /// <summary>
        /// انتخاب شعر و خوانش
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelPoem_Click(object sender, EventArgs e)
        {

            DbBrowser db = Connect();
            if (db == null)
            {
                MessageBox.Show("(db == null)", "خطا", MessageBoxButtons.OK);
                return;
            }

            List<GanjoorPoet> poets =  db.Poets;

            using (ItemSelector poetSeletor = new ItemSelector("انتخاب شاعر", poets.ToArray(), null))
            {
                if (poetSeletor.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    GanjoorPoet poet = poetSeletor.SelectedItem as GanjoorPoet;

                    using (CategorySelector catSelector = new CategorySelector(poet._ID, db))
                    {
                        if (catSelector.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            List<GanjoorPoem> poems = db.GetPoems(catSelector.SelectedCatID);

                            using (ItemSelector poemSeletor = new ItemSelector("انتخاب شعر", poems.ToArray(), null))
                            {
                                if (poemSeletor.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                                {
                                    GanjoorPoem poem = poemSeletor.SelectedItem as GanjoorPoem;

                                    PoemAudio[] audio = db.GetPoemAudioFiles(poem._ID);

                                    if (audio.Length == 0)
                                    {
                                        if (MessageBox.Show("خوانشی یافت نشد. آیا تمایل دارید تصاویر ثابت تولید کنید؟", "خطا", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading)
                                             == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            Settings.Default.PoemId = poem._ID;
                                            Settings.Default.AudioId = 0;
                                            Settings.Default.Save();

                                            UpdatePoemAndAudioInfo(db);
                                        }
                                    }
                                    else
                                    {
                                        using (ItemSelector audioSelector = new ItemSelector("انتخاب خوانش", audio, audio[0]))
                                        {
                                            if (audioSelector.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                                            {
                                                Settings.Default.PoemId = poem._ID;
                                                Settings.Default.AudioId = (audioSelector.SelectedItem as PoemAudio).Id;
                                                Settings.Default.Save();

                                                UpdatePoemAndAudioInfo(db);
                                            }
                                        }
                                    }
                                }
                            }

                           
                        }
                    }
                }
            }


            db.CloseDb();

        }

        #endregion 

        #region UI / Frame Properies

        private void cmbVerses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;
            GVideoFrame frame = cmbVerses.SelectedItem as GVideoFrame;
            chkAudioBound.Checked = frame.AudioBound;
            lblStart.Visible = lblMinus.Visible = txtStartInMiliseconds.Visible = !frame.AudioBound && frame.MasterFrame == null;
            txtStartInMiliseconds.Value = frame.AudioBound || frame.MasterFrame != null? txtStartInMiliseconds.Minimum : -frame.StartInMiliseconds;
            txtBackgroundImage.Text = frame.BackgroundImagePath;
            btnBackColor.BackColor = frame.BackColor;
            btnTextColor.BackColor = frame.TextColor;
            btnTextBackColor.BackColor = frame.TextBackColor;
            btnBorderColor.BackColor = frame.BorderColor;
            trckAlpha.Value = frame.TextBackColorAlpha;
            txtThickness.Value = frame.TextBackRectThickness;
            txtFont.Text = frame.Font.Name + "(" + frame.Font.Style.ToString() + ") " + frame.Font.Size.ToString();
            trckVPosition.Value = trckVPosition.Maximum / frame.MainTextPosRatioPortionFrom * (frame.MainTextPosRatioPortionFrom - frame.MainTextPosRatioPortion);
            trckHPosition.Value = trckHPosition.Maximum / frame.MainTextHPosRatioPortionFrom * frame.MainTextHPosRatioPortion;
            trckMaxTextWidth.Value = trckMaxTextWidth.Maximum / frame.MaxTextWidthRatioPortionFrom * frame.MaxTextWidthRatioPortion;
            chkSlaveFrame.Checked = frame.MasterFrame != null;
            chkShowLogo.Checked = frame.ShowLogo;

            lblBackgroundImage.Enabled = txtBackgroundImage.Enabled = btnSelBackgroundImage.Enabled = btnResetImage.Enabled = frame.MasterFrame == null;


            InvalidatePreview();

        }

        private void InvalidatePreview()
        {
            GVideoFrame frame = cmbVerses.SelectedItem as GVideoFrame;
            Image imgOld = pbxPreview.Image;
            pbxPreview.Image = RenderFrame(frame, new Size((int)txtWidth.Value, (int)txtHeight.Value));
            if (imgOld != null)
            {
                imgOld.Dispose();
            }
        }


        /// <summary>
        /// انتخاب تصویر
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelBackgroundImage_Click(object sender, EventArgs e)
        {
            if(cmbVerses.SelectedItem == null)
                return;
            GVideoFrame frame = cmbVerses.SelectedItem as GVideoFrame;
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "JPEG Files (*.jpg)|*.jpg|MP4 Files (*.mp4)|*.mp4|MOV Files (*.mov)|*.mov";
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    string ext = Path.GetExtension(dlg.FileName).ToLower();

                    bool videoBackground = ext == ".mov" || ext == ".mp4";
                    string strNewBackground = videoBackground ? "" : dlg.FileName;
                    _VideoBackgroundPath = videoBackground ? dlg.FileName : "";

                    string strOldBackgroundImagePath = frame.BackgroundImagePath;

                    int idx = cmbVerses.SelectedIndex;
                    for (int i = videoBackground ? 0 : idx ; i < cmbVerses.Items.Count; i++)
                        if (
                            string.IsNullOrEmpty((cmbVerses.Items[i] as GVideoFrame).BackgroundImagePath)
                            ||
                            (cmbVerses.Items[i] as GVideoFrame).BackgroundImagePath == strOldBackgroundImagePath
                            )
                            (cmbVerses.Items[i] as GVideoFrame).BackgroundImagePath = strNewBackground;                            

                    txtBackgroundImage.Text = frame.BackgroundImagePath;

                    Settings.Default.LastImagePath = strNewBackground;
                    Settings.Default.Save();

                    InvalidatePreview();
                }
            }
        }

        private void btnResetImage_Click(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;
            GVideoFrame frame = cmbVerses.SelectedItem as GVideoFrame;

            string strOldBackgroundImagePath = frame.BackgroundImagePath;

            int idx = cmbVerses.SelectedIndex;
            for (int i = idx; i < cmbVerses.Items.Count; i++)
                if (                 
                    (cmbVerses.Items[i] as GVideoFrame).BackgroundImagePath == strOldBackgroundImagePath
                    )
                    (cmbVerses.Items[i] as GVideoFrame).BackgroundImagePath = "";

            txtBackgroundImage.Text = frame.BackgroundImagePath;

            Settings.Default.LastImagePath = "";
            Settings.Default.Save();

            InvalidatePreview();

        }


        private void btnBackColor_Click(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;

            using (ColorDialog dlg = new ColorDialog())
            {
                dlg.Color = btnBackColor.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    int idx = cmbVerses.SelectedIndex;

                    for (int i = idx; i < cmbVerses.Items.Count; i++)
                            (cmbVerses.Items[i] as GVideoFrame).BackColor = dlg.Color;


                    btnBackColor.BackColor = dlg.Color;

                    InvalidatePreview();
                }
            }
        }

        private void btnBorderColor_Click(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;

            using (ColorDialog dlg = new ColorDialog())
            {
                dlg.Color = btnBorderColor.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    int idx = cmbVerses.SelectedIndex;

                    for (int i = idx; i < cmbVerses.Items.Count; i++)
                        (cmbVerses.Items[i] as GVideoFrame).BorderColor = dlg.Color;


                    btnBorderColor.BackColor = dlg.Color;

                    InvalidatePreview();
                }
            }
        }


        private void btnTextColor_Click(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;

            using (ColorDialog dlg = new ColorDialog())
            {
                dlg.Color = btnBackColor.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    int idx = cmbVerses.SelectedIndex;

                    for (int i = idx; i < cmbVerses.Items.Count; i++)
                            (cmbVerses.Items[i] as GVideoFrame).TextColor = dlg.Color;


                    btnTextColor.BackColor = dlg.Color;

                    InvalidatePreview();
                }
            }
        }

        private void btnTextBackColor_Click(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;

            using (ColorDialog dlg = new ColorDialog())
            {
                dlg.Color = btnBackColor.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    int idx = cmbVerses.SelectedIndex;

                    for (int i = idx; i < cmbVerses.Items.Count; i++)
                            (cmbVerses.Items[i] as GVideoFrame).TextBackColor = dlg.Color;


                   btnTextBackColor.BackColor = dlg.Color;

                   InvalidatePreview();
                }
            }
        }

        private void trckAlpha_Scroll(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;

            int idx = cmbVerses.SelectedIndex;

            for (int i = idx; i < cmbVerses.Items.Count; i++)
                    (cmbVerses.Items[i] as GVideoFrame).TextBackColorAlpha = trckAlpha.Value;

            InvalidatePreview();
        }

        private void btnSelectFont_Click(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;

            GVideoFrame frame = cmbVerses.SelectedItem as GVideoFrame;

            using (FontDialog dlg = new FontDialog())
            {
                dlg.Font = frame.Font;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    int idx = cmbVerses.SelectedIndex;

                    for (int i = idx; i < cmbVerses.Items.Count; i++)
                    {
                        (cmbVerses.Items[i] as GVideoFrame).Font = dlg.Font;
                        if (i == idx && frame.MasterFrame != null)
                            break;

                    }

                    txtFont.Text = frame.Font.Name + "(" + frame.Font.Style.ToString() + ") " + frame.Font.Size.ToString();

                    if (frame.MasterFrame == null)
                    {
                        Settings.Default.LastUsedFont = frame.Font;
                        Settings.Default.Save();
                    }

                    InvalidatePreview();
                }
            }
        }


        private void trckVPosition_Scroll(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;

            int idx = cmbVerses.SelectedIndex;

            for (int i = idx; i < cmbVerses.Items.Count; i++)
            {
                GVideoFrame frame = (cmbVerses.Items[i] as GVideoFrame);
                if (i == idx || frame.MasterFrame == null)
                {
                    frame.MainTextPosRatioPortionFrom = trckVPosition.Maximum;
                    frame.MainTextPosRatioPortion = (trckVPosition.Maximum - trckVPosition.Value);

                    if (i == idx && frame.MasterFrame != null)
                        break;
                }
            }

            InvalidatePreview();

        }

        private void trckHPosition_Scroll(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;

            GVideoFrame frame = cmbVerses.SelectedItem as GVideoFrame;

            frame.MainTextHPosRatioPortionFrom = trckHPosition.Maximum;
            frame.MainTextHPosRatioPortion = trckHPosition.Value;


            InvalidatePreview();
        }


        /// <summary>
        /// تغییر متن
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditText_Click(object sender, EventArgs e)
        {

            if (cmbVerses.SelectedItem == null)
                return;

            GVideoFrame frame = cmbVerses.SelectedItem as GVideoFrame;
            using (ItemEditor dlg = new ItemEditor(EditItemType.General, "ویرایش متن", "متن:"))
            {
                dlg.ItemName = frame.Text;
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    
                    frame.Text = dlg.ItemName;

                    int nIdx = cmbVerses.SelectedIndex;

                    cmbVerses.Items.RemoveAt(nIdx);
                    cmbVerses.Items.Insert(nIdx, frame);

                    cmbVerses.SelectedItem = frame;

                                        
                    InvalidatePreview();
                }
            }

        }

        /// <summary>
        /// حذف قاب
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelFrame_Click(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;
            GVideoFrame frame = cmbVerses.SelectedItem as GVideoFrame;
            if(frame.MasterFrame == null)
            {
                MessageBox.Show("فقط امکان حذف قابهای یکی شده با قاب پیشین وجود دارد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                return;
            }
            if (MessageBox.Show("آیا از حذف این قاب اطمینان دارید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.No)
                return;

            foreach(object item in cmbVerses.Items)
                if((item as GVideoFrame).MasterFrame == frame)
                {
                    (item as GVideoFrame).MasterFrame = frame.MasterFrame;
                }            

            cmbVerses.Items.Remove(cmbVerses.SelectedItem);
            cmbVerses.SelectedItem = frame.MasterFrame;
            InvalidatePreview();
        }



        private void trckMaxTextWidth_Scroll(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;

            int idx = cmbVerses.SelectedIndex;

            for (int i = idx; i < cmbVerses.Items.Count; i++)
            {
                GVideoFrame frame = (cmbVerses.Items[i] as GVideoFrame);
                if (i == idx || frame.MasterFrame == null)
                {
                    frame.MaxTextWidthRatioPortionFrom = trckMaxTextWidth.Maximum;
                    frame.MaxTextWidthRatioPortion = trckMaxTextWidth.Value;

                    if (i == idx && frame.MasterFrame != null)
                        break;

                }
            }

            InvalidatePreview();
        }

        private void txtThickness_ValueChanged(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;

            int idx = cmbVerses.SelectedIndex;

            for (int i = idx; i < cmbVerses.Items.Count; i++)
            {
                GVideoFrame frame = (cmbVerses.Items[i] as GVideoFrame);
                frame.TextBackRectThickness = (int)txtThickness.Value;

                if (i == idx && frame.MasterFrame != null)
                    break;
            }

            InvalidatePreview();
        }


        private void btnProperties_Click(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;


            using (ObjectPropertisEditor dlg = new ObjectPropertisEditor())
            {
                GVideoFrame frame = cmbVerses.SelectedItem as GVideoFrame;
                dlg.Object = frame;
                GTextBoxShape oldShape = frame.Shape;
                dlg.ShowDialog(this);
                if (oldShape != frame.Shape)
                {
                    int idx = cmbVerses.SelectedIndex;

                    for (int i = idx+1; i < cmbVerses.Items.Count; i++)
                    {
                        (cmbVerses.Items[i] as GVideoFrame).Shape = frame.Shape;
                    }
                }
                cmbVerses_SelectedIndexChanged(sender, e);
            }
        }


        private void btnReset_Click(object sender, EventArgs e)
        {
            txtWidth.Value = 960M;
            txtHeight.Value = 720M;

            Settings.Default.LastImageWidth = (int)txtWidth.Value;
            Settings.Default.LastImageHeight = (int)txtHeight.Value;
            Settings.Default.Save();

            InvalidatePreview();

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Settings.Default.LastImageWidth = (int)txtWidth.Value;
            Settings.Default.LastImageHeight = (int)txtHeight.Value;
            Settings.Default.Save();

            InvalidatePreview();
        }

        private void chkShowLogo_CheckedChanged(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;
            GVideoFrame frame = cmbVerses.SelectedItem as GVideoFrame;

            frame.ShowLogo = chkShowLogo.Checked;
            InvalidatePreview();

        }

        private void chkSlaveFrame_CheckedChanged(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;
            GVideoFrame frame = cmbVerses.SelectedItem as GVideoFrame;
            if (frame != null)
            {
                if (chkSlaveFrame.Checked)
                {
                    if (frame.MasterFrame != null)
                        return;
                    int nParentIdx = cmbVerses.SelectedIndex - 1;
                    while (nParentIdx >= 0)
                    {
                        GVideoFrame p = cmbVerses.Items[nParentIdx] as GVideoFrame;
                        if (p.MasterFrame == null)
                        {
                            frame.MasterFrame = p;
                            InvalidatePreview();
                            return;
                        }
                        nParentIdx--;
                    }
                }
                else
                {
                    if (frame.MasterFrame == null)
                        return;
                    frame.MasterFrame = null;
                    InvalidatePreview();
                    return;
                }
            }
            chkSlaveFrame.Checked = false;
            MessageBox.Show("امکان فعال کردن این گزینه برای این قاب وجود ندارد.", "خطا");
        }

        /// <summary>
        /// یکی کردن بعدیها یکی در میان
        /// </summary>
        private void btnPairNext_Click(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;
            GVideoFrame frame = cmbVerses.SelectedItem as GVideoFrame;
            if (frame != null)
            {
                if (chkSlaveFrame.Checked)
                {
                    MessageBox.Show("امکان فعال کردن این گزینه برای این قاب وجود ندارد.", "خطا");
                    return;
                }
            }



            using (ItemSelector dlg = new ItemSelector("تعداد مصرعها در هر قاب", new object[] { 2, 4 }, 2))
            {
                if(dlg.ShowDialog(this) == DialogResult.OK)
                {
                    int n = (int)dlg.SelectedItem;
                    int idx = cmbVerses.SelectedIndex;

                    bool fisrtSet = true;

                    for (int i = (idx + 1); i < cmbVerses.Items.Count; i += n)
                    {
                        int max = (i + n - 1);
                        if (max > cmbVerses.Items.Count)
                            max = cmbVerses.Items.Count;
                        for (int j=i; j< max; j++)
                        {
                            frame = cmbVerses.Items[j] as GVideoFrame;
                            frame.MasterFrame = (cmbVerses.Items[i - 1] as GVideoFrame);

                            frame.MainTextPosRatioPortion = 
                                n == 2 ? frame.MainTextPosRatioPortionFrom - frame.MasterFrame.MainTextPosRatioPortion
                                       : 
                                       //4:
                                       j == i ? //2nd
                                        2 * frame.MainTextPosRatioPortion
                                       : j == i + 1 ? //3rd
                                        frame.MainTextPosRatioPortionFrom - frame.MasterFrame.MainTextPosRatioPortion - frame.MainTextPosRatioPortion
                                        //4th
                                       : frame.MainTextPosRatioPortionFrom - frame.MasterFrame.MainTextPosRatioPortion;
                        }

                        if (fisrtSet)
                        {
                            InvalidatePreview();
                            fisrtSet = false;
                            if (MessageBox.Show("این چینش برای مصرعهای بعدی هم اعمال شود؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.No)
                                return;
                        }

                    }
                    InvalidatePreview();
                }
            }

        }



        private List<string> _lstDeleteFileList = new List<string>();

        private void btnPreview_Click(object sender, EventArgs e)
        {
            SaveShowPreviewImage();
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (string fileName in _lstDeleteFileList)
                try
                {
                    File.Delete(fileName);
                }
                catch
                {
                }
        }

        private void pbxPreview_Click(object sender, EventArgs e)
        {
            SaveShowPreviewImage();
        }

        private void btnProduce_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                if (string.IsNullOrEmpty(Settings.Default.VidDefExt))
                {
                    Settings.Default.VidDefExt = ".wmv";
                }
                dlg.Filter =
                    Settings.Default.VidDefExt == ".wmv" ?
                    "WMV Files (*.wmv)|*.wmv|MP4 Files (*.mp4)|*.mp4"
                    :
                    "MP4 Files (*.mp4)|*.mp4|WMV Files (*.wmv)|*.wmv";
                dlg.DefaultExt = Settings.Default.VidDefExt;
                dlg.FileName = txtPoemId.Text;
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    Settings.Default.VidDefExt = Path.GetExtension(dlg.FileName).ToLower();
                    Settings.Default.Save();
                    InitiateRendering(dlg.FileName);
                }
            }
        }

        private void btnSubtitle_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "SRT Files (*.srt)|*.srt";
                dlg.FileName = txtPoemId.Text + ".srt";
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    CreateSubTitle(dlg.FileName);
                }
            }
        }

        /// <summary>
        /// تصاویر تصادفی
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRandomImage_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("با انتخاب این گزینه تصاویر تصادفی از سایت unsplash.com برای زوج قابهای فاقد تصویر زمینه انتخاب می‌شود. آیا موافقید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading
                | MessageBoxOptions.RightAlign) == System.Windows.Forms.DialogResult.Yes)
            {                
                using (UnsplashImageTypeForm dlg = new UnsplashImageTypeForm())
                {
                    if (dlg.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
                        return;                    

                    if(!string.IsNullOrEmpty(dlg.ImageFolderPath))
                    {
                        string[] images = Directory.GetFiles(dlg.ImageFolderPath, "*.jpg");
                        int nIdx = -1;
                        for (int i = 0; i < cmbVerses.Items.Count; i++)
                        {
                            if (i >= images.Length)
                                break;
                            GVideoFrame frame = cmbVerses.Items[i] as GVideoFrame;
                            if (!frame.AudioBound && frame.MasterFrame != null)
                            {
                                continue;
                            }
                            nIdx++;
                            if (nIdx == 0 || (nIdx % 2 == 1))
                            {
                                if (frame.MasterFrame == null && string.IsNullOrEmpty(frame.BackgroundImagePath))
                                {
                                    frame.BackgroundImagePath = images[i];
                                    if (i != cmbVerses.Items.Count - 1)
                                    {
                                        if ((string.IsNullOrEmpty((cmbVerses.Items[i + 1] as GVideoFrame).BackgroundImagePath)))
                                        {
                                            (cmbVerses.Items[i + 1] as GVideoFrame).BackgroundImagePath = images[i];
                                        }
                                    }
                                    cmbVerses.SelectedIndex = i;
                                    Application.DoEvents();
                                }
                            }
                        }
                        cmbVerses_SelectedIndexChanged(sender, e);
                        cmbVerses.Focus();

                        return;
                    }
                }
                

                string url = String.Format(Settings.Default.UnsplashSearchUrl
                    , Settings.Default.LastImageWidth, Settings.Default.LastImageHeight);

                this.Enabled = false;
                try
                {
                    int nIdx = -1;

                    for (int i = 0; i < cmbVerses.Items.Count; i++)
                    {
                        GVideoFrame frame = cmbVerses.Items[i] as GVideoFrame;
                        if (!frame.AudioBound && frame.MasterFrame != null)
                        {
                            continue;
                        }
                        nIdx++;
                        if (nIdx == 0 || (nIdx % 2 == 1))
                        {
                            if (frame.MasterFrame == null && string.IsNullOrEmpty(frame.BackgroundImagePath))
                            {
                                string strTempImage = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".jpg");
                                using (WebClient webClient = new WebClient())
                                {
                                    webClient.DownloadFile(url, strTempImage);
                                    _lstDeleteFileList.Add(strTempImage);
                                    frame.BackgroundImagePath = strTempImage;
                                    if (i != cmbVerses.Items.Count - 1)
                                    {
                                        if ((string.IsNullOrEmpty((cmbVerses.Items[i + 1] as GVideoFrame).BackgroundImagePath)))
                                        {
                                            (cmbVerses.Items[i + 1] as GVideoFrame).BackgroundImagePath = strTempImage;
                                        }
                                    }
                                }
                                cmbVerses.SelectedIndex = i;
                                Application.DoEvents();
                            }

                        }
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
                cmbVerses_SelectedIndexChanged(sender, e);
                cmbVerses.Focus();
                this.Enabled = true;

            }
        }


        private void cmbTransitionEffect_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.TransitionType = cmbTransitionEffect.SelectedIndex;
            Settings.Default.Save();
        }


        private void chkAAC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAAC.Checked == Settings.Default.AACSound)
                return;
            if (chkAAC.Checked)
            {
                if (MessageBox.Show("صدای تولید شده mp4 روی بعضی از دستگاهها پخش نمی شود.\nآیا این کار را کرده‌اید؟این گزینه این مشکل را حل می‌کند.\nآیا ffmpeg-hi10-heaac.exe را دریافت کرده اید؟", "پرسش",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign)
                      == DialogResult.Yes)
                {
                    MessageBox.Show("لطفا در مرحلهٔ بعد فایل ffmpeg-hi10-heaac.exe را از مسیر مورد نظر انتخاب کنید.", "آگاهی",
                  MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);

                    using (OpenFileDialog dlg = new OpenFileDialog())
                    {
                        dlg.Filter = "*EXE Files (*.exe)|*.exe";
                        dlg.FileName = "ffmpeg-hi10-heaac.exe";
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            Settings.Default.AACFFMpegPath = dlg.FileName;                        
                            Settings.Default.AACSound = true;
                            Settings.Default.Save();
                        }
                        else
                        {
                            chkAAC.Checked = false;
                            return;
                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("آیا تمایل دارید هم‌اکنون این نرم‌افزار را دریافت کنید؟", "پرسش",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign)
                        == DialogResult.Yes)
                    {
                        Process.Start("https://sourceforge.net/projects/ffmpeg-hi/");

                        MessageBox.Show("لطفا بعد از دریافت و باز کردن فایلها مجددا تلاش کنید.", "آگاهی",
                                          MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                    }
                    chkAAC.Checked = false;
                    return;
                }
            }
            else
            {
                Settings.Default.AACSound = false;
                Settings.Default.Save();
            }

        }

        private void chkDebug_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDebug.Checked == Settings.Default.DebugMode)
                return;
            if (chkDebug.Checked)
            {
                if (MessageBox.Show("با فعال کردن این گزینه (جهت اشکالیابی) در هنگام تولید خروجی mp4 میزان پیشرفت را نمی‌بینید." +  Environment.NewLine + "آیا موافقید؟", "تأییدیه",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign)
                      == DialogResult.No)
                {
                    chkDebug.Checked = false;
                    return;
                }
            }

            Settings.Default.DebugMode = chkDebug.Checked;
            Settings.Default.Save();
        }


        #endregion

        #region Render Frame
        private Image RenderFrame(GVideoFrame frame, Size szImageSize, Image imgMaster = null)
        {
            if (frame == null)
                return null;

            if (imgMaster == null && frame.MasterFrame != null)
                return RenderFrame(frame.MasterFrame, szImageSize);

            Image imgOutput = imgMaster == null ? new Bitmap(szImageSize.Width, szImageSize.Height) : imgMaster;

            using (Graphics g = Graphics.FromImage(imgOutput))
            {
                if (frame.MasterFrame == null)
                {
                    //BackColor
                    if (string.IsNullOrEmpty(_VideoBackgroundPath))
                    {
                        using (SolidBrush brshBackColor = new SolidBrush(frame.BackColor))
                            g.FillRectangle(brshBackColor, new Rectangle(0, 0, szImageSize.Width, szImageSize.Height));
                    }

                    //BackgroundImagePath
                    if (!string.IsNullOrEmpty(frame.BackgroundImagePath))
                    {
                        using (Image bmpBkgnImage = Bitmap.FromFile(frame.BackgroundImagePath))
                        {
                            int nW = szImageSize.Width;
                            int nH = bmpBkgnImage.Height * szImageSize.Width / bmpBkgnImage.Width;
                            if (nH < szImageSize.Height)
                            {
                                nH = szImageSize.Height;
                                nW = bmpBkgnImage.Width * szImageSize.Height / bmpBkgnImage.Height;
                            }
                            g.DrawImage(bmpBkgnImage, new Rectangle(0, 0, nW, nH));
                        }
                    }
                }

                if (frame.ShowLogo)
                {
                    g.DrawImage(Properties.Resources.glogo, new Rectangle(
                        szImageSize.Width - Properties.Resources.glogo.Width - 2, szImageSize.Height - Properties.Resources.glogo.Height - 2,
                        Properties.Resources.glogo.Width, Properties.Resources.glogo.Height)
                        );
                }

                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                SizeF szText = g.MeasureString(frame.Text, frame.Font, frame.MaxTextWidthRatioPortion * (int)(txtWidth.Value) / frame.MaxTextWidthRatioPortionFrom, new StringFormat(StringFormatFlags.DirectionRightToLeft));
                RectangleF rcText = new RectangleF((frame.MainTextHPosRatioPortion * szImageSize.Width / frame.MainTextHPosRatioPortionFrom) - szText.Width / 2, (frame.MainTextPosRatioPortion * szImageSize.Height / frame.MainTextPosRatioPortionFrom) - szText.Height / 2, szText.Width, szText.Height);
                using (SolidBrush bkTextBrush = new SolidBrush(Color.FromArgb(frame.TextBackColorAlpha, frame.TextBackColor)))
                {
                    switch (frame.Shape)
                    {
                        case GTextBoxShape.RoundRect:
                            {
                                Rectangle rcBorder = new Rectangle((int) rcText.X,(int) rcText.Y, (int)rcText.Width ,(int)rcText.Height   );
                                rcBorder.X -= frame.TextBackRectThickness;
                                rcBorder.Y -= frame.TextBackRectThickness;
                                rcBorder.Width += 2*frame.TextBackRectThickness;
                                rcBorder.Height += 2 * frame.TextBackRectThickness;
                                g.FillRoundedRectangle(bkTextBrush, rcBorder, (int)(rcBorder.Height / 4));
                                if (frame.TextBackRectThickness > 0)
                                {
                                    using (Pen pen = new Pen(frame.BorderColor, (float)frame.TextBackRectThickness))
                                        g.DrawRoundedRectangle(pen, rcBorder, (int)(rcBorder.Height / 4));
                                }
                            }
                            break;
                        default:
                            {
                                g.FillRectangle(bkTextBrush, rcText);
                                if (frame.TextBackRectThickness > 0)
                                {
                                    using (Pen pen = new Pen(frame.BorderColor, (float)frame.TextBackRectThickness))
                                        g.DrawRectangle(pen, rcText.X - frame.TextBackRectThickness / 2, rcText.Y - frame.TextBackRectThickness / 2, rcText.Width + frame.TextBackRectThickness, rcText.Height + frame.TextBackRectThickness);
                                }
                            }
                            break;
                    }
                }
                using (SolidBrush txtBrush = new SolidBrush(frame.TextColor))
                    g.DrawString(frame.Text, frame.Font, txtBrush,
                        rcText
                        , new StringFormat(StringFormatFlags.DirectionRightToLeft)
                        );

                foreach (object item in cmbVerses.Items)
                    if ((item as GVideoFrame).MasterFrame == frame)
                        RenderFrame((item as GVideoFrame), szImageSize, imgOutput);
            }


            if (!string.IsNullOrEmpty(_VideoBackgroundPath))
            {
                (imgOutput as Bitmap).MakeTransparent();
            }

            return imgOutput;

        }
        #endregion

        #region Main Operations
        /// <summary>
        /// پیش نمایش
        /// </summary>
        private void SaveShowPreviewImage()
        {
            if (pbxPreview.Image != null)
            {
                string strTempImage = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
                pbxPreview.Image.Save(strTempImage);
                _lstDeleteFileList.Add(strTempImage);
                Process.Start(strTempImage);
            }
        }

        /// <summary>
        /// رندر
        /// </summary>
        /// <param name="outfilePath"></param>
        private void InitiateRendering(string outfilePath)
        {

            bool bIsWmv = Path.GetExtension(outfilePath).Equals(".wmv", StringComparison.InvariantCultureIgnoreCase);

            string ffmpegPath = Settings.Default.FFmpegPath;
            if (!bIsWmv)
            {
                if (string.IsNullOrEmpty(ffmpegPath))
                    ffmpegPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ffmpeg");
                if(!File.Exists(Path.Combine(ffmpegPath, "ffmpeg.exe")))
                {
                    if(MessageBox.Show("برای تولید خروجی MP4 باید ffmpeg را دریافت و در مسیری باز کرده باشید.\nآیا این کار را کرده‌اید؟", "پرسش",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign)
                        == DialogResult.Yes)
                    {
                        MessageBox.Show("لطفا در مرحلهٔ بعد فایل ffmpeg.exe را از مسیر مورد نظر انتخاب کنید.", "آگاهی",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);

                        using (OpenFileDialog dlg = new OpenFileDialog())
                        {
                            dlg.Filter = "*EXE Files (*.exe)|*.exe";
                            dlg.FileName = "ffmpeg.exe";
                            if (dlg.ShowDialog(this) == DialogResult.OK)
                            {
                                ffmpegPath = Path.GetDirectoryName(dlg.FileName);
                                if (!File.Exists(Path.Combine(ffmpegPath, "ffmpeg.exe")))
                                {
                                    MessageBox.Show(String.Format("{0} وجود ندارد", Path.Combine(ffmpegPath, "ffmpeg.exe")), "خطا",
                                                      MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                                    return;
                                }
                                Settings.Default.FFmpegPath = ffmpegPath;
                                Settings.Default.Save();
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("آیا تمایل دارید هم‌اکنون این نرم‌افزار را دریافت کنید؟", "پرسش",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign)
                            == DialogResult.Yes)
                        {
                            Process.Start("https://ffmpeg.org/download.html#build-windows");

                            MessageBox.Show("لطفا بعد از دریافت و باز کردن فایلها مجددا تلاش کنید.", "آگاهی",
                                              MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                        }
                        return;
                    }
                }
            }

            DbBrowser db = Connect();
            if (db == null)
            {
                MessageBox.Show("(db == null)", "خطا", MessageBoxButtons.OK);
                return;
            }

            string audioFilePath = "";
            PoemAudio[] audioFiles = db.GetPoemAudioFiles(Settings.Default.PoemId);
            if (audioFiles.Length > 0)
            {
                foreach (PoemAudio a in audioFiles)
                    if (a.Id == Settings.Default.AudioId)
                    {
                        audioFilePath = a.FilePath;
                        break;
                    }
            }

            if (string.IsNullOrEmpty(audioFilePath))
            {
                MessageBox.Show("(string.IsNullOrEmpty(audioFilePath))", "خطا", MessageBoxButtons.OK);
                return;
            }

            if (!File.Exists(audioFilePath))
            {
                MessageBox.Show("فایل صوتی در مسیر تعیین شده وجود ندارد.", "خطا", MessageBoxButtons.OK);
                return;
            }

            this.Enabled = false;

            string wav;
            NAudio.Wave.Mp3FileReader r = new NAudio.Wave.Mp3FileReader(audioFilePath);
            int playtime = r.TotalTime.Milliseconds;
            using (Mp3FileReader mp3 = new Mp3FileReader(audioFilePath))
            {
                playtime = (int)mp3.TotalTime.TotalMilliseconds;
                wav = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".wav");
                if (bIsWmv)
                {
                    using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
                    {
                        WaveFileWriter.CreateWaveFile(wav, pcm);
                    }
                    _lstDeleteFileList.Add(wav);
                }
                else
                {
                    wav = audioFilePath;
                }
            }



            int nWidth = Settings.Default.LastImageWidth;
            int nHeight = Settings.Default.LastImageHeight;

            ITimeline timeline = bIsWmv ? new DefaultTimeline() : null;
            IGroup group = bIsWmv ? timeline.AddVideoGroup(32, nWidth, nHeight) : null;
            ITrack videoTrack = bIsWmv ? group.AddTrack() : null;

            double dSoundStart = -1.0;

            prgrss.Value = 0;
            prgrss.Maximum = cmbVerses.Items.Count;

            lblStatus.Text = "ایجاد تصاویر ...";

            
            List<string> ffmpegInputFiles = new List<string>();
            List<double> ffmpegDurations = new List<double>();


            //نمایش قابها
            for (int i = 0; i < cmbVerses.Items.Count; i++)
            {
                prgrss.Value++;
                Application.DoEvents();

                GVideoFrame frame = cmbVerses.Items[i] as GVideoFrame;
                if (frame.MasterFrame != null)
                    continue;

                if (dSoundStart < 0 && frame.AudioBound)
                {
                    if (i > 0)
                    {
                        for (int j = (i - 1); j >= 0; j--)
                        {
                            GVideoFrame pFrame = cmbVerses.Items[j] as GVideoFrame;
                            if (pFrame.MasterFrame == null)
                            {
                                dSoundStart = -(pFrame.StartInMiliseconds / 1000.0);
                                break;
                            }
                        }
                        if (frame.StartInMiliseconds != 0)
                        {
                            dSoundStart -= (frame.StartInMiliseconds / 1000.0);
                            if (dSoundStart < 0)
                            {
                                this.Enabled = true;
                                MessageBox.Show(String.Format("قدر مطلق زمان شروع اولین قاب باید بزرگتر از {0} باشد", frame.StartInMiliseconds));
                                return;
                            }
                        }
                    }
                    else
                    {
                        dSoundStart = frame.StartInMiliseconds / 1000.0;
                    }
                }

                Image img = RenderFrame(frame, new Size(nWidth, nHeight));
                string filename = Path.Combine(Path.GetTempPath(), 
                    Guid.NewGuid().ToString() + ".jpg"//this is important! : jpg not png - although it's png by heart!                    
                    ); 
                if(bIsWmv)
                    img.Save(filename);
                else
                {
                    if(string.IsNullOrEmpty(_VideoBackgroundPath))
                        img.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
                    else
                        img.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
                }

                _lstDeleteFileList.Add(filename);
                int duration = i != (cmbVerses.Items.Count - 1) ? (cmbVerses.Items[i + 1] as GVideoFrame).StartInMiliseconds - frame.StartInMiliseconds : (playtime - frame.StartInMiliseconds);
                if (frame.AudioBound)
                {
                    int nIdxNext = i + 1;
                    while (nIdxNext < cmbVerses.Items.Count)
                    {
                        GVideoFrame nextFrame = cmbVerses.Items[nIdxNext] as GVideoFrame;
                        if (nextFrame.MasterFrame == null)
                            break;
                        duration +=
                            nIdxNext != (cmbVerses.Items.Count - 1) ? (cmbVerses.Items[nIdxNext + 1] as GVideoFrame).StartInMiliseconds - nextFrame.StartInMiliseconds : (playtime - nextFrame.StartInMiliseconds);
                        nIdxNext++;
                    }
                }

                
                if(bIsWmv)
                {
                    IClip clip = videoTrack.AddImage(filename, 0, (double)(duration / 1000.0));                    
                }
                else
                {
                    ffmpegInputFiles.Add(filename);
                    ffmpegDurations.Add((double)(duration / 1000.0));
                }
            }



            if (bIsWmv)
            {
                //صدا
                ITrack audioTrack = bIsWmv ? timeline.AddAudioGroup().AddTrack() : null;
                IClip audio = bIsWmv ? audioTrack.AddAudio(wav, dSoundStart, playtime / 1000.0) : null;

                lblStatus.Text = "تولید خروجی: متأسفانه نزدیک به تا ابد طول می‌کشد :(";
                string wmvProfile = Properties.Resources.WMV_HD_960x720;
                wmvProfile = wmvProfile.Replace("960", nWidth.ToString()).Replace("720", nHeight.ToString());
                using (WindowsMediaRenderer renderer =
                   new WindowsMediaRenderer(timeline, outfilePath, wmvProfile))
                {
                    renderer.Render();
                }
            }
            else
            {
                //ایفکت جابجایی بین قابها
                if (Settings.Default.TransitionType > 0)
                {
                    GTransitionEffect effect = (GTransitionEffect)Settings.Default.TransitionType;
                    Size szImageSize = new Size(Settings.Default.LastImageWidth, Settings.Default.LastImageHeight);
                    List<string> newInputFiles = new List<string>();
                    List<double> newDurations = new List<double>();
                    for (int i = 0; i < ffmpegInputFiles.Count - 1; i++)
                    {
                        newInputFiles.Add(ffmpegInputFiles[i]);
                        newDurations.Add(i == 0 ? (ffmpegDurations[i] - 0.2) : (ffmpegDurations[i] - 0.4));
                        for (int j = 1; j < 4; j++)
                        {
                            using (Image imgOutput = new Bitmap(szImageSize.Width, szImageSize.Height))
                            {

                                using (Graphics g = Graphics.FromImage(imgOutput))
                                {
                                    switch (effect)
                                    {
                                        case GTransitionEffect.ToRight:
                                            {
                                                using (Image img1 = Bitmap.FromFile(ffmpegInputFiles[i]))
                                                {
                                                    g.DrawImage(img1, new PointF(j * (float)szImageSize.Width / 4, 0));
                                                }
                                                using (Image img2 = Bitmap.FromFile(ffmpegInputFiles[i + 1]))
                                                {
                                                    g.DrawImage(img2, new PointF((j - 4) * (float)szImageSize.Width / 4, 0));
                                                }
                                            }
                                            break;
                                        case GTransitionEffect.ToLeft:
                                            {
                                                using (Image img1 = Bitmap.FromFile(ffmpegInputFiles[i]))
                                                {
                                                    g.DrawImage(img1, new PointF(-j * (float)szImageSize.Width / 4, 0));
                                                }
                                                using (Image img2 = Bitmap.FromFile(ffmpegInputFiles[i + 1]))
                                                {
                                                    g.DrawImage(img2, new PointF((4 - j) * (float)szImageSize.Width / 4, 0));
                                                }
                                            }
                                            break;
                                        case GTransitionEffect.ToUp:
                                            {
                                                using (Image img1 = Bitmap.FromFile(ffmpegInputFiles[i]))
                                                {
                                                    g.DrawImage(img1, new PointF(0, (-j) * (float)szImageSize.Height / 4));
                                                }
                                                using (Image img2 = Bitmap.FromFile(ffmpegInputFiles[i + 1]))
                                                {
                                                    g.DrawImage(img2, new PointF(0, (4 - j) * (float)szImageSize.Height / 4));
                                                }
                                            }
                                            break;
                                        case GTransitionEffect.ToBottom:
                                            {
                                                using (Image img1 = Bitmap.FromFile(ffmpegInputFiles[i]))
                                                {
                                                    g.DrawImage(img1, new PointF(0, j * (float)szImageSize.Height / 4));
                                                }
                                                using (Image img2 = Bitmap.FromFile(ffmpegInputFiles[i + 1]))
                                                {
                                                    g.DrawImage(img2, new PointF(0, (j - 4) * (float)szImageSize.Height / 4));
                                                }
                                            }
                                            break;
                                    }
                                }

                                string ext = string.IsNullOrEmpty(_VideoBackgroundPath) ? ".jpg"//this is important! : jpg not png - although it's png by heart!                    
                                    : ".png";

                                string filename = Path.Combine(Path.GetTempPath(),
                                                    Guid.NewGuid().ToString() + ext
                                                    );
                                
                                imgOutput.Save(filename, ext == ".jpg" ? System.Drawing.Imaging.ImageFormat.Jpeg : System.Drawing.Imaging.ImageFormat.Png);
                                newInputFiles.Add(filename);
                                newDurations.Add(0.1);
                                _lstDeleteFileList.Add(filename);
                            }
                        }
                    }

                    newInputFiles.Add(ffmpegInputFiles[ffmpegInputFiles.Count - 1]);
                    newDurations.Add((ffmpegDurations[ffmpegInputFiles.Count - 1] - 0.2));

                    ffmpegInputFiles = newInputFiles;
                    ffmpegDurations = newDurations;

                }



                lblStatus.Text = "تولید خروجی";

                string ffconcat = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".ffconcat");
                StringBuilder sbFFMPegConcatDemux = new StringBuilder();
                sbFFMPegConcatDemux.AppendLine("ffconcat version 1.0");

                Debug.Assert(ffmpegInputFiles.Count == ffmpegDurations.Count);
                for (int i = 0; i < ffmpegInputFiles.Count; i++)
                {
                    sbFFMPegConcatDemux.AppendLine(String.Format("file {0}", Path.GetFileName(ffmpegInputFiles[i])));
                    sbFFMPegConcatDemux.AppendLine(String.Format("duration {0}", ffmpegDurations[i]));
                }



                File.WriteAllText(ffconcat, sbFFMPegConcatDemux.ToString());



                File.Copy(wav, Path.Combine(Path.GetTempPath(), Path.GetFileName(wav)), true);

                string outInTempPath = Path.Combine(Path.GetTempPath(), Path.GetFileName(outfilePath));

                if (File.Exists(outInTempPath))
                {
                    try
                    {
                        File.Delete(outInTempPath);
                    }
                    catch
                    {
                        MessageBox.Show("error: File.Delete(outInTempPath)");
                    }
                }

                
                if (!string.IsNullOrEmpty(_VideoBackgroundPath))
                {

                    //rescale the video and remove audio:                   
                    string resampledVideoBackground = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".mp4");
                    if (File.Exists(resampledVideoBackground))
                    {
                        try
                        {
                            File.Delete(resampledVideoBackground);
                        }
                        catch
                        {
                            MessageBox.Show("error: File.Delete(resampledVideoBackground)");
                        }
                    }

                    string strSlowDownFilter = "";
                    using (ItemEditor dlg = new ItemEditor(EditItemType.General, "بزرگتر از ۱ کاهش", "سرعت:"))
                    {
                        dlg.ItemName = "1";
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            try
                            {
                                double fSlowDown = Double.Parse(dlg.ItemName);
                                if (fSlowDown != 0 && fSlowDown != 1)
                                {
                                    strSlowDownFilter = $", setpts = {fSlowDown} * PTS";
                                }
                            }
                            catch(Exception exp)
                            {
                                if (MessageBox.Show($"مقدار نامعتبر {exp.ToString()}{Environment.NewLine} ادامه می‌دهید؟", "خطا", MessageBoxButtons.YesNo) != DialogResult.Yes)
                                {
                                    this.Enabled = true;
                                    return;
                                }
                            }
                        }
                    }

                    lblStatus.Text = "تغییر اندازه ویدیو و حذف فایل صدای آن";
                    Application.DoEvents();

                    RunFFmpegCommand(
                        ffmpegPath,
                        $"-i \"{_VideoBackgroundPath}\" -vf \"[in] scale={Settings.Default.LastImageWidth}:{Settings.Default.LastImageHeight}{strSlowDownFilter} [out]\" -an \"{resampledVideoBackground}\""
                        );

                    string mixedwithAudioVideoBackground = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".mp4");
                    if (File.Exists(mixedwithAudioVideoBackground))
                    {
                        try
                        {
                            File.Delete(mixedwithAudioVideoBackground);
                        }
                        catch
                        {
                            MessageBox.Show("error: File.Delete(resampledVideoBackground)");
                        }
                    }



                    lblStatus.Text = "ترکیب صدا با ویدیو";
                    Application.DoEvents();

                    RunFFmpegCommand(
                        ffmpegPath,
                        $"-itsoffset {dSoundStart} -i \"{Path.GetFileName(wav)}\" -filter_complex movie={Path.GetFileName(resampledVideoBackground)}:loop=0,setpts=N/FRAME_RATE/TB -shortest -codec:a libmp3lame -qscale:a 9 -f mp4 -c:v libx264 -preset slow  -async 1 \"{mixedwithAudioVideoBackground}\""
                        );
                    File.Delete(resampledVideoBackground);

                    lblStatus.Text = "ترکیب ویدیو با متن";
                    Application.DoEvents();

                    RunFFmpegCommand(
                        ffmpegPath,
                        $"-y -i \"{mixedwithAudioVideoBackground}\" -i {Path.GetFileName(ffconcat)} -filter_complex \"[1][0]scale2ref[i][m];[m] [i] overlay[v]\" -map \"[v]\" -map 0:a? -ac 2 \"{outInTempPath}\""
                        );

                    File.Delete(mixedwithAudioVideoBackground);
                }

                else
                {
                    RunFFmpegCommand(ffmpegPath,

                        String.Format("-y -i {0} -itsoffset {1} -i \"{2}\" -codec:a libmp3lame -qscale:a 9 -f mp4 -c:v libx264 -preset slow -tune stillimage -async 1 {3}",
                        Path.GetFileName(ffconcat),
                        dSoundStart,
                        Path.GetFileName(wav),
                        Path.GetFileName(outInTempPath),
                        Settings.Default.LastImageWidth,
                        Settings.Default.LastImageHeight)
                        );
                }






                if (File.Exists(outInTempPath))
                {
                    if(Settings.Default.AACSound)
                    {
                        if (!string.IsNullOrEmpty(Settings.Default.AACFFMpegPath) && File.Exists(Settings.Default.AACFFMpegPath))
                        {
                            lblStatus.Text = "aac تبدیل صدا به فرمت";
                            Application.DoEvents();

                            string outInTempPathAAC = Path.Combine(Path.GetTempPath(), "aac" + Path.GetFileName(outfilePath));
                            string cmdArgs =
                                                String.Format("-y -i {0} -c:v copy -c:a libfdk_aac -b:a 64k {1}",
                                                Path.GetFileName(outInTempPath),
                                                Path.GetFileName(outInTempPathAAC));

                            ProcessStartInfo psAAC = new ProcessStartInfo
                                (
                                Settings.Default.AACFFMpegPath
                                ,
                                cmdArgs
                                );

                            psAAC.WorkingDirectory = Path.GetTempPath();
                            psAAC.UseShellExecute = false;
                            StringBuilder sbOutput = new StringBuilder();
                            StringBuilder sbErr = new StringBuilder();
                            if (Settings.Default.DebugMode)
                            {
                                psAAC.RedirectStandardOutput = true;
                                psAAC.RedirectStandardError = true;

                            }




                            var ffmpegAACPs = Process.Start(psAAC);
                            if (Settings.Default.DebugMode)
                            {
                                ffmpegAACPs.EnableRaisingEvents = true;
                                ffmpegAACPs.OutputDataReceived += (s, e) => sbOutput.AppendLine(e.Data);
                                ffmpegAACPs.ErrorDataReceived += (s, e) => sbErr.AppendLine(e.Data);
                                ffmpegAACPs.BeginOutputReadLine();
                                ffmpegAACPs.BeginErrorReadLine();
                            }

                            ffmpegAACPs.WaitForExit();

                            if (Settings.Default.DebugMode)
                            {
                                string strErrOutput = sbErr.ToString();

                                if (!string.IsNullOrEmpty(strErrOutput))
                                {
                                    string errFile = Path.Combine(Path.GetTempPath(),
                                                        "err2-" + Guid.NewGuid().ToString() + ".txt"
                                                        );
                                    File.WriteAllText(errFile, strErrOutput);
                                    Process.Start(errFile);
                                    _lstDeleteFileList.Add(errFile);
                                }


                                string strOutOutput = sbOutput.ToString().Trim();

                                if (!string.IsNullOrEmpty(strOutOutput))
                                {
                                    string outFile = Path.Combine(Path.GetTempPath(),
                                                        "out2-" + Guid.NewGuid().ToString() + ".txt"
                                                        );
                                    File.WriteAllText(outFile, sbOutput.ToString());
                                    Process.Start(outFile);
                                    _lstDeleteFileList.Add(outFile);
                                }
                            }


                            if (File.Exists(outInTempPathAAC))
                            {
                                try
                                {
                                    File.Delete(outInTempPath);
                                    outInTempPath = outInTempPathAAC;
                                }
                                catch(Exception exp)
                                {
                                    MessageBox.Show(exp.ToString());
                                }
                                
                                 
                            }
                            else
                            {
                                MessageBox.Show("File.Exists(outInTempPathAAC) == false");
                            }
                        }
                    }

                }



                File.Delete(Path.Combine(Path.GetTempPath(), Path.GetFileName(wav)));
                File.Delete(ffconcat);

                if (File.Exists(outfilePath))
                {
                    try
                    {
                        File.Delete(outfilePath);
                    }
                    catch
                    {
                        MessageBox.Show("error: File.Delete(outInTempPath)");
                    }
                }

                if (File.Exists(outInTempPath))
                    File.Move(outInTempPath, outfilePath);

                
            }


            this.Enabled = true;

            lblStatus.Text = "آماده";

            if(File.Exists(outfilePath))
            {
                if (MessageBox.Show("آیا مایلید فایل خروجی را مشاهده کنید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign) == System.Windows.Forms.DialogResult.Yes)
                {
                    Process.Start(outfilePath);
                }
            }
            else
            {
                MessageBox.Show("فایل خروجی ایجاد نشد.", "خطا", MessageBoxButtons.OK);
            }

            if (bIsWmv)
                timeline.Dispose();

        }

        /// <summary>
        /// اجرای فرمان ffmpeg
        /// </summary>
        /// <param name="ffmpegPath"></param>
        /// <param name="cmdArgs"></param>
        private void RunFFmpegCommand(string ffmpegPath, string cmdArgs)
        {
            ProcessStartInfo ps = new ProcessStartInfo
                (
                Path.Combine(ffmpegPath, "ffmpeg.exe")
                ,
                cmdArgs
                );





            ps.WorkingDirectory = Path.GetTempPath();
            ps.UseShellExecute = false;
            if (Settings.Default.DebugMode)
            {
                ps.RedirectStandardOutput = true;
                ps.RedirectStandardError = true;
            }
            var ffmpegPs = Process.Start(ps);

            StringBuilder sbOutput = new StringBuilder();
            StringBuilder sbErr = new StringBuilder();

            if (Settings.Default.DebugMode)
            {
                ffmpegPs.EnableRaisingEvents = true;
                ffmpegPs.OutputDataReceived += (s, e) => sbOutput.AppendLine(e.Data);
                ffmpegPs.ErrorDataReceived += (s, e) => sbErr.AppendLine(e.Data);
                ffmpegPs.BeginOutputReadLine();
                ffmpegPs.BeginErrorReadLine();
            }

            ffmpegPs.WaitForExit();

            if (Settings.Default.DebugMode)
            {
                string strErrOutput = sbErr.ToString();

                if (!string.IsNullOrEmpty(strErrOutput))
                {
                    string errFile = Path.Combine(Path.GetTempPath(),
                                        "err1-" + Guid.NewGuid().ToString() + ".txt"
                                        );
                    File.WriteAllText(errFile, strErrOutput);
                    Process.Start(errFile);
                    _lstDeleteFileList.Add(errFile);
                }


                string strOutOutput = sbOutput.ToString().Trim();

                if (!string.IsNullOrEmpty(strOutOutput))
                {
                    string outFile = Path.Combine(Path.GetTempPath(),
                                        "out1-" + Guid.NewGuid().ToString() + ".txt"
                                        );
                    File.WriteAllText(outFile, sbOutput.ToString());
                    Process.Start(outFile);
                    _lstDeleteFileList.Add(outFile);
                }
            }
        }


        /// <summary>
        /// تولید زیرنویس
        /// </summary>
        /// <param name="outfilePath">مسیر خروجی</param>
        private void CreateSubTitle(string outfilePath)
        {
            DbBrowser db = Connect();
            if (db == null)
            {
                MessageBox.Show("(db == null)", "خطا", MessageBoxButtons.OK);
                return;
            }

                double dSoundStart = -1.0;

                for (int i=0; i<cmbVerses.Items.Count; i++)
                {
                    GVideoFrame frame = cmbVerses.Items[i] as GVideoFrame;
                    if (frame.MasterFrame != null)
                        continue;

                    if (dSoundStart < 0 && frame.AudioBound)
                    {
                        if (i > 0)
                        {
                            for (int j = (i - 1); j >= 0; j--)
                            {
                                GVideoFrame pFrame = cmbVerses.Items[j] as GVideoFrame;
                                if (pFrame.MasterFrame == null)
                                {
                                    dSoundStart = -(pFrame.StartInMiliseconds / 1000.0);
                                    break;
                                }
                            }
                            if (frame.StartInMiliseconds != 0)
                            {
                                dSoundStart -= (frame.StartInMiliseconds / 1000.0);
                                if (dSoundStart < 0)
                                {
                                    this.Enabled = true;
                                    MessageBox.Show(String.Format("قدر مطلق زمان شروع اولین قاب باید بزرگتر از {0} باشد", frame.StartInMiliseconds));
                                    return;
                                }
                            }
                        }
                        else
                        {
                            dSoundStart = frame.StartInMiliseconds / 1000.0;
                        }
                        break;
                    }

                }

            string audioFilePath = "";
            PoemAudio[] audioFiles = db.GetPoemAudioFiles(Settings.Default.PoemId);
            if (audioFiles.Length > 0)
            {
                foreach (PoemAudio audio in audioFiles)
                    if (audio.Id == Settings.Default.AudioId)
                    {
                        audioFilePath = audio.FilePath;
                        break;
                    }
            }
            
            if(string.IsNullOrEmpty(audioFilePath))
            {
                MessageBox.Show("(string.IsNullOrEmpty(audioFilePath))", "خطا", MessageBoxButtons.OK);
                return;
            }

            NAudio.Wave.Mp3FileReader r = new NAudio.Wave.Mp3FileReader(audioFilePath);
            int playtime = r.TotalTime.Milliseconds;



            List<string> lines = new List<string>();
            int nIdxSrtLine = 0;



            using (Mp3FileReader mp3 = new Mp3FileReader(audioFilePath))
            {
                playtime = (int)mp3.TotalTime.TotalMilliseconds;
            }

            for (int i = 0; i < cmbVerses.Items.Count; i++)
            {
                GVideoFrame frame = cmbVerses.Items[i] as GVideoFrame;
                if (frame.MasterFrame != null)
                {
                    lines.Add(frame.Text);
                    continue;
                }
                lines.Add("");
                nIdxSrtLine++;
                DateTime dtStart = frame.StartInMiliseconds < 0 ? new DateTime(0) : new DateTime(0).AddMilliseconds(((frame.StartInMiliseconds) + dSoundStart * 1000.0));
                int duration = i != (cmbVerses.Items.Count - 1) ? (cmbVerses.Items[i + 1] as GVideoFrame).StartInMiliseconds - frame.StartInMiliseconds : (playtime - frame.StartInMiliseconds);
                if (frame.AudioBound)
                {
                    int nIdxNext = i + 1;
                    while (nIdxNext < cmbVerses.Items.Count)
                    {
                        GVideoFrame nextFrame = cmbVerses.Items[nIdxNext] as GVideoFrame;
                        if (nextFrame.MasterFrame == null)
                            break;
                        duration +=
                            nIdxNext != (cmbVerses.Items.Count - 1) ? (cmbVerses.Items[nIdxNext + 1] as GVideoFrame).StartInMiliseconds - nextFrame.StartInMiliseconds : (playtime - nextFrame.StartInMiliseconds);
                        nIdxNext++;
                    }
                }
                DateTime dtEnd = dtStart.AddMilliseconds(duration);

                lines.Add(nIdxSrtLine.ToString());
                lines.Add(
                    dtStart.ToString("HH:mm:ss.fff")
                    + " --> "
                    + dtEnd.ToString("HH:mm:ss.fff")
                    );

                lines.Add(frame.Text);
            }
            File.WriteAllLines(outfilePath, lines.ToArray());

            db.CloseDb();

            MessageBox.Show("زیرنویس ایجاد شد.", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);

        }

        #endregion

    }
}
