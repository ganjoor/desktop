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
using ganjoor.Utilities;
using System.Drawing.Drawing2D;

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

                if (dlg.ShowDialog(this) == DialogResult.OK)
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




            GanjoorPoem poem = db.GetPoem(Settings.Default.PoemId);
            if (poem != null)
            {
                GanjoorPoet poet = db.GetPoetForCat(poem._CatID);
                string strTitle = poet.ToString();
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
                                    GMessageBox.SayError("تلاش برای دسترسی و پخش فایل صوتی موفق نبود");
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
                                TextVerticalPosRatioPortion = 1,
                                TextVerticalPosRatioPortionFrom = 4,
                                TextHorizontalPosRatioPortion = 1,
                                TextHorizontalPosRatioPortionFrom = 2,
                                MaxTextWidthRatioPortion = 10,
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


                            string fullCat = poem._Title;
                            GanjoorCat cat = db.GetCategory(poem._CatID);
                            if(cat != null)
                            {
                                fullCat = cat._Text;

                                cat = db.GetCategory(cat._ParentID);
                                while(cat != null)
                                {
                                    if (cat._ID == poet._CatID)
                                        break;
                                    fullCat = cat._Text + " » " + fullCat;
                                    cat = db.GetCategory(cat._ParentID);
                                }
                            }

                            GVideoFrame poemFrame = new GVideoFrame()
                            {
                                AudioBound = false,
                                StartInMiliseconds = 0,
                                Text = fullCat,
                                BackgroundImagePath = strImagePath,
                                TextVerticalPosRatioPortion = 1,
                                TextVerticalPosRatioPortionFrom = 2,
                                TextHorizontalPosRatioPortion = 1,
                                TextHorizontalPosRatioPortionFrom = 2,
                                MaxTextWidthRatioPortion = 10,
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
                                Text = poem._Title,
                                BackgroundImagePath = strImagePath,
                                TextVerticalPosRatioPortion = 3,
                                TextVerticalPosRatioPortionFrom = 4,
                                TextHorizontalPosRatioPortion = 1,
                                TextHorizontalPosRatioPortionFrom = 2,
                                MaxTextWidthRatioPortion = 10,
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
                                    TextVerticalPosRatioPortion = 1,
                                    TextVerticalPosRatioPortionFrom = 2,
                                    TextHorizontalPosRatioPortion = 1,
                                    TextHorizontalPosRatioPortionFrom = 2,
                                    MaxTextWidthRatioPortion = 10,
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

            //btnProduce.Enabled = 
                btnSubtitle.Enabled = Settings.Default.AudioId != 0;
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
                if (poetSeletor.ShowDialog(this) == DialogResult.OK)
                {
                    GanjoorPoet poet = poetSeletor.SelectedItem as GanjoorPoet;

                    using (CategorySelector catSelector = new CategorySelector(poet._ID, db))
                    {
                        if (catSelector.ShowDialog(this) == DialogResult.OK)
                        {
                            List<GanjoorPoem> poems = db.GetPoems(catSelector.SelectedCatID);

                            using (ItemSelector poemSeletor = new ItemSelector("انتخاب شعر", poems.ToArray(), null))
                            {
                                if (poemSeletor.ShowDialog(this) == DialogResult.OK)
                                {
                                    GanjoorPoem poem = poemSeletor.SelectedItem as GanjoorPoem;

                                    PoemAudio[] audio = db.GetPoemAudioFiles(poem._ID);

                                    if (audio.Length == 0)
                                    {

                                        if (
                                            GMessageBox.Ask("خوانشی یافت نشد. آیا تمایل دارید تصاویر ثابت تولید کنید؟", "خطا")                                            
                                             == DialogResult.Yes)
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
                                            if (audioSelector.ShowDialog(this) == DialogResult.OK)
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
            txtBackgroundImage.Text = frame.BackgroundImagePath;
            btnBackColor.BackColor = frame.BackColor;
            btnTextColor.BackColor = frame.TextColor;
            btnTextBackColor.BackColor = frame.TextBackColor;
            btnBorderColor.BackColor = frame.BorderColor;
            trckAlpha.Value = frame.TextBackColorAlpha;
            txtThickness.Value = frame.TextBackRectThickness;
            txtFont.Text = frame.Font.Name + "(" + frame.Font.Style.ToString() + ") " + frame.Font.Size.ToString();
            trckVPosition.Value = trckVPosition.Maximum / frame.TextVerticalPosRatioPortionFrom * (frame.TextVerticalPosRatioPortionFrom - frame.TextVerticalPosRatioPortion);
            trckHPosition.Value = trckHPosition.Maximum / frame.TextHorizontalPosRatioPortionFrom * frame.TextHorizontalPosRatioPortion;
            trckMaxTextWidth.Value = trckMaxTextWidth.Maximum / frame.MaxTextWidthRatioPortionFrom * frame.MaxTextWidthRatioPortion;
            chkSlaveFrame.Checked = frame.MasterFrame != null;
            chkShowLogo.Checked = frame.ShowLogo;
            cmbOverlayImages.Items.Clear();
            if (frame.OverlayImages != null && frame.OverlayImages.Length > 0)
            {
                cmbOverlayImages.Items.AddRange(frame.OverlayImages);
                cmbOverlayImages.SelectedIndex = 0;
            }

            trckSize.Visible = false;

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


        private void chkAudioBound_Click(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;
            GVideoFrame frame = cmbVerses.SelectedItem as GVideoFrame;
            if (GMessageBox.Ask($"تغییر این گزینه توصیه نمی‌شود.{Environment.NewLine}آیا تمایل دارید این کار را نکنید؟") == DialogResult.No)
                frame.AudioBound = chkAudioBound.Checked;
            else
            {
                chkAudioBound.Checked = frame.AudioBound;
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
                if (dlg.ShowDialog(this) == DialogResult.OK)
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

            if(!string.IsNullOrEmpty(_VideoBackgroundPath))
            {
                _VideoBackgroundPath = "";
                InvalidatePreview();
                return;
            }

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

        private void ChangeFontSize(int nChangeValue)
        {
            if (cmbVerses.SelectedItem == null)
                return;

            GVideoFrame frame = cmbVerses.SelectedItem as GVideoFrame;

            Font fntOld = frame.Font;
            if (nChangeValue < 0 && (fntOld.Size + nChangeValue) < 2)
                return;
            frame.Font = new Font(fntOld.FontFamily, fntOld.Size + nChangeValue, fntOld.Style);
            //fntOld.Dispose();

            int idx = cmbVerses.SelectedIndex;

            if (frame.MasterFrame == null)
                for (int i = idx + 1; i < cmbVerses.Items.Count; i++)
                {
                    (cmbVerses.Items[i] as GVideoFrame).Font = frame.Font;
                }

            txtFont.Text = frame.Font.Name + "(" + frame.Font.Style.ToString() + ") " + frame.Font.Size.ToString();

            if (frame.MasterFrame == null)
            {
                Settings.Default.LastUsedFont = frame.Font;
                Settings.Default.Save();
            }

            InvalidatePreview();


        }

        private void btnIncreaseFontSize_Click(object sender, EventArgs e)
        {
            ChangeFontSize(2);
        }

        private void btnDecreaseFontSize_Click(object sender, EventArgs e)
        {
            ChangeFontSize(-2);
        }




        private void cmbOverlayImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;

            GVideoFrame frame = (cmbVerses.SelectedItem as GVideoFrame);

            if (cmbOverlayImages.SelectedItem != null)
            {
                GOverlayImage overlay = cmbOverlayImages.SelectedItem as GOverlayImage;
                if (overlay != null)
                {
                    if (!string.IsNullOrEmpty(overlay.ImagePath))
                    {
                        trckVPosition.Value = trckVPosition.Maximum / overlay.VerticalPosRatioPortionFrom * (overlay.VerticalPosRatioPortionFrom - overlay.VerticalPosRatioPortion);
                        trckHPosition.Value = trckHPosition.Maximum / overlay.HorizontalPosRatioPortionFrom * overlay.HorizontalPosRatioPortion;
                        trckSize.Value = trckSize.Maximum / overlay.ScaleRatioPortionFrom * overlay.ScaleRatioPortion;
                        trckSize.Visible = true;
                        return;
                    }
                }
            }


            trckVPosition.Value = trckVPosition.Maximum / frame.TextVerticalPosRatioPortionFrom * (frame.TextVerticalPosRatioPortionFrom - frame.TextVerticalPosRatioPortion);
            trckHPosition.Value = trckHPosition.Maximum / frame.TextHorizontalPosRatioPortionFrom * frame.TextHorizontalPosRatioPortion;
            

            trckSize.Visible = false;

        }

        private void trckVPosition_Scroll(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;

            int idx = cmbVerses.SelectedIndex;

            bool overlayScrolling = false;
            if(cmbOverlayImages.SelectedItem != null)
            {
                GOverlayImage overlay = cmbOverlayImages.SelectedItem as GOverlayImage;
                if(overlay != null)
                {
                    if(!string.IsNullOrEmpty(overlay.ImagePath))
                    {
                        overlayScrolling = true;
                        overlay.VerticalPosRatioPortionFrom = trckVPosition.Maximum;
                        overlay.VerticalPosRatioPortion = (trckVPosition.Maximum - trckVPosition.Value);
                    }
                }
            }

            if(!overlayScrolling)
            for (int i = idx; i < cmbVerses.Items.Count; i++)
            {
                GVideoFrame frame = (cmbVerses.Items[i] as GVideoFrame);
                if (i == idx || frame.MasterFrame == null)
                {
                    frame.TextVerticalPosRatioPortionFrom = trckVPosition.Maximum;
                    frame.TextVerticalPosRatioPortion = (trckVPosition.Maximum - trckVPosition.Value);

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

            bool overlayScrolling = false;
            if (cmbOverlayImages.SelectedItem != null)
            {
                GOverlayImage overlay = cmbOverlayImages.SelectedItem as GOverlayImage;
                if (overlay != null)
                {
                    if (!string.IsNullOrEmpty(overlay.ImagePath))
                    {
                        overlayScrolling = true;
                        overlay.HorizontalPosRatioPortionFrom = trckHPosition.Maximum;
                        overlay.HorizontalPosRatioPortion = trckHPosition.Value;
                    }
                }
            }

            if (!overlayScrolling)
            {
                GVideoFrame frame = cmbVerses.SelectedItem as GVideoFrame;

                frame.TextHorizontalPosRatioPortionFrom = trckHPosition.Maximum;
                frame.TextHorizontalPosRatioPortion = trckHPosition.Value;

            }



            InvalidatePreview();
        }

        private void trckSize_Scroll(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;

            if (cmbOverlayImages.SelectedItem != null)
            {
                GOverlayImage overlay = cmbOverlayImages.SelectedItem as GOverlayImage;
                if (overlay != null)
                {
                    if (!string.IsNullOrEmpty(overlay.ImagePath))
                    {
                        overlay.ScaleRatioPortionFrom = trckSize.Maximum;
                        overlay.ScaleRatioPortion = trckSize.Value;
                        InvalidatePreview();
                    }
                }
            }

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
                if (dlg.ShowDialog(this) == DialogResult.OK)
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
        /// تغییر زمان شروع
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditTime_Click(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;

            GVideoFrame frame = cmbVerses.SelectedItem as GVideoFrame;
            using (ItemEditor dlg = new ItemEditor(EditItemType.General, "زمان شروع نمایش (میلی ثانیه)", "شروع:"))
            {
                dlg.ItemName = frame.StartInMiliseconds.ToString();
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        frame.StartInMiliseconds = Int32.Parse(dlg.ItemName);                        
                    }
                    catch (Exception exp)
                    {
                        GMessageBox.SayError("لطفا یک عدد وارد کنید - " +  Environment.NewLine + exp.ToString());
                    }
                }
            }

        }


        /// <summary>
        /// اضافه کردن قاب
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddFrame_Click(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;

            using (ItemEditor dlg = new ItemEditor(EditItemType.General, "متن قاب جدید", "متن:"))
            {
                dlg.ItemName = "";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    GVideoFrame parent = cmbVerses.SelectedItem as GVideoFrame;

                    GVideoFrame frame = new GVideoFrame()
                    {
                        AudioBound = false,
                        StartInMiliseconds = 0,
                        Text = dlg.ItemName,
                        BackgroundImagePath = "",
                        TextVerticalPosRatioPortion = 1,
                        TextVerticalPosRatioPortionFrom = 2,
                        TextHorizontalPosRatioPortion = 1,
                        TextHorizontalPosRatioPortionFrom = 2,
                        MaxTextWidthRatioPortion = 10,
                        MaxTextWidthRatioPortionFrom = 10,
                        BackColor = Color.White,
                        TextColor = Color.White,
                        TextBackColor = Color.Black,
                        BorderColor = Color.Black,
                        TextBackColorAlpha = 100,
                        Shape = GTextBoxShape.Rectangle,
                        TextBackRectThickness = 0,
                        Font = Settings.Default.LastUsedFont,
                        MasterFrame = parent,
                        ShowLogo = false
                    };

                    cmbVerses.Items.Insert(cmbVerses.SelectedIndex + 1, frame);
                    cmbVerses.SelectedItem = frame;                    

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
                GMessageBox.SayError("فقط امکان حذف قابهای یکی شده با قاب پیشین وجود دارد.");
                return;
            }
            if (GMessageBox.Ask("آیا از حذف این قاب اطمینان دارید؟") == DialogResult.No)
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


        private void btnVideoTools_Click(object sender, EventArgs e)
        {
            string ffmpegPath = Settings.Default.FFmpegPath;
            if (string.IsNullOrEmpty(ffmpegPath))
                ffmpegPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ffmpeg");
            if (!File.Exists(Path.Combine(ffmpegPath, "ffmpeg.exe")))
            {
                GMessageBox.SayError("لطفا ابتدا مسیر ffmpeg.exe را با انتخاب یک خروجی از نوع mp4 مشخص کنید.");
                return;
            }


            string inputFilePath = "";
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "MP4 Files (*.mp4)|*.mp4|MOV Files (*.mov)|*.mov";
                if (dlg.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
                inputFilePath = dlg.FileName;
            }

            using (VideoTools dlg = new VideoTools())
            {
                DialogResult rs = dlg.ShowDialog(this);
                if(rs == DialogResult.Yes) //Cut
                {
                    string startTime = dlg.StartTime;
                    DateTime dtStartTime;
                    if(!DateTime.TryParse(startTime, out dtStartTime))
                    {
                        GMessageBox.SayError("زمان شروع نامعتبر است.");
                        return;
                    }
                    string endTime = dlg.EndTime;
                    DateTime dtEndTime;
                    if (!DateTime.TryParse(endTime, out dtEndTime))
                    {
                        GMessageBox.SayError("زمان پایان نامعتبر است.");
                        return;
                    }

                    TimeSpan dt = dtEndTime - dtStartTime;
                    string dtStr = dt.ToString(@"hh\:mm\:ss");

                    using (SaveFileDialog saveDlg = new SaveFileDialog())
                    {
                        saveDlg.Filter =
                            "MP4 Files (*.mp4)|*.mp4";
                        saveDlg.FileName = Path.GetFileNameWithoutExtension(inputFilePath) + "-cut";
                        if (saveDlg.ShowDialog(this) == DialogResult.OK)
                        {

                            string outfilePath = saveDlg.FileName;
                            if (File.Exists(outfilePath))
                                File.Delete(outfilePath);

                            RunFFmpegCommand(
                                ffmpegPath,
                                $"-ss {startTime} -i \"{ inputFilePath}\" -t { dtStr } -c copy  \"{outfilePath}\""
                                );

                            if (File.Exists(outfilePath))
                            {
                                if (MessageBox.Show("آیا مایلید فایل خروجی را مشاهده کنید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                                    MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign) == DialogResult.Yes)
                                {
                                    Process.Start(outfilePath);
                                }
                            }
                            else
                            {
                                MessageBox.Show("فایل خروجی ایجاد نشد.", "خطا", MessageBoxButtons.OK);
                            }
                        }
                    }

                    return;
                }
                if (rs == DialogResult.No) //Extract Audio
                {
                    using (SaveFileDialog saveDlg = new SaveFileDialog())
                    {
                        saveDlg.Filter =
                            "MP3 Files (*.mp3)|*.mp3";
                        saveDlg.FileName = Path.GetFileNameWithoutExtension(inputFilePath) + "-audio";
                        if (saveDlg.ShowDialog(this) == DialogResult.OK)
                        {

                            string outfilePath = saveDlg.FileName;
                            if (File.Exists(outfilePath))
                                File.Delete(outfilePath);

                            RunFFmpegCommand(
                                ffmpegPath,
                                $"-i \"{ inputFilePath}\" \"{outfilePath}\""
                                );

                            if (File.Exists(outfilePath))
                            {
                                if (MessageBox.Show("آیا مایلید فایل خروجی را مشاهده کنید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                                    MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign) == DialogResult.Yes)
                                {
                                    Process.Start(outfilePath);
                                }
                            }
                            else
                            {
                                MessageBox.Show("فایل خروجی ایجاد نشد.", "خطا", MessageBoxButtons.OK);
                            }
                        }
                    }

                    return;

                }
            }

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


            using (ObjectPropertiesEditor dlg = new ObjectPropertiesEditor())
            {
                GVideoFrame frame = cmbVerses.SelectedItem as GVideoFrame;
                dlg.Object = frame;
                GTextBoxShape oldShape = frame.Shape;
                int textShadowValue = frame.TextBorderValue;
                Color textShadowColor = frame.TextBorderColor;
                dlg.ShowDialog(this);
                if (oldShape != frame.Shape)
                {
                    int idx = cmbVerses.SelectedIndex;

                    for (int i = idx+1; i < cmbVerses.Items.Count; i++)
                    {
                        (cmbVerses.Items[i] as GVideoFrame).Shape = frame.Shape;
                    }
                }
                if (textShadowValue != frame.TextBorderValue)
                {
                    int idx = cmbVerses.SelectedIndex;

                    for (int i = idx + 1; i < cmbVerses.Items.Count; i++)
                    {
                        (cmbVerses.Items[i] as GVideoFrame).TextBorderValue = frame.TextBorderValue;
                    }
                }
                if (textShadowColor != frame.TextBorderColor)
                {
                    int idx = cmbVerses.SelectedIndex;

                    for (int i = idx + 1; i < cmbVerses.Items.Count; i++)
                    {
                        (cmbVerses.Items[i] as GVideoFrame).TextBorderColor = frame.TextBorderColor;
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


        private void btnAddImage_Click(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;

            GVideoFrame frame = cmbVerses.SelectedItem as GVideoFrame;

            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "PNG Images (*.png)|*.png";
                if(dlg.ShowDialog(this) == DialogResult.OK)
                {

                    GOverlayImage newImage = new GOverlayImage()
                    {
                        Name = Path.GetFileNameWithoutExtension(dlg.FileName),
                        ImagePath = dlg.FileName,
                        VerticalPosRatioPortion = 1,
                        VerticalPosRatioPortionFrom = 2,
                        HorizontalPosRatioPortion = 1,
                        HorizontalPosRatioPortionFrom = 2,
                        ScaleRatioPortion = 1,
                        ScaleRatioPortionFrom = 1,
                    };


                    List<GOverlayImage> list = new List<GOverlayImage>();
                    if (frame.OverlayImages != null)
                        list.AddRange(frame.OverlayImages);
                    if(cmbOverlayImages.SelectedIndex != -1)
                    {
                        list.Insert(cmbOverlayImages.SelectedIndex + 1, newImage);
                    }
                    else
                    {
                        list.Add(newImage);
                    }

                    frame.OverlayImages = list.ToArray();
                    cmbOverlayImages.Items.Clear();
                    cmbOverlayImages.Items.AddRange(frame.OverlayImages);
                    cmbOverlayImages.SelectedItem = newImage;
                    InvalidatePreview();

                }
            }
        }

        private void btnDelImage_Click(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;

            GVideoFrame frame = cmbVerses.SelectedItem as GVideoFrame;


            if (frame != null)
            {
                GOverlayImage overlay = cmbOverlayImages.SelectedItem as GOverlayImage;
                if (overlay != null)
                {
                    if (!string.IsNullOrEmpty(overlay.ImagePath))
                    {
                        if(GMessageBox.Ask("آیا از حذف این تصویر اطمینان دارید؟") == DialogResult.Yes)
                        {
                            List<GOverlayImage> list = new List<GOverlayImage>();
                            list.AddRange(frame.OverlayImages);
                            list.Remove(overlay);
                            frame.OverlayImages = list.ToArray();
                            cmbVerses.SelectedIndex = -1;
                            cmbVerses.SelectedItem = frame;
                        }
                    }
                    else
                    {
                        GMessageBox.SayError("لطفا یک لایه تصویری را انتخاب کنید.");
                    }

                }
            }
        }

        private void btnCopyTo_Click(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;

            GVideoFrame currentFrame = cmbVerses.SelectedItem as GVideoFrame;

            if (currentFrame != null)
            {
                if(currentFrame.MasterFrame != null)
                {
                    GMessageBox.SayError("فقط امکان کپی از قابهایی که وابسته به قاب دیگری نیستند وجود دارد.");
                    return;
                }

                GOverlayImage overlay = cmbOverlayImages.SelectedItem as GOverlayImage;
                if (overlay != null)
                {
                    if (!string.IsNullOrEmpty(overlay.ImagePath))
                    {
                        if (GMessageBox.Ask("آیا می‌خواهید این لایه به قابهای غیروابسته بعدی کپی (تک‌ نمونه) شود؟") == DialogResult.Yes)
                        {
                            int nIdx = cmbVerses.SelectedIndex;
                            int nLast = -1;
                            for(int i = nIdx+1; i<cmbVerses.Items.Count; i++)
                            {
                                GVideoFrame frame = cmbVerses.Items[i] as GVideoFrame;
                                if (frame.MasterFrame == null)
                                {
                                    List<GOverlayImage> overlays = new List<GOverlayImage>();
                                    overlays.AddRange(frame.OverlayImages);
                                    overlays.Add(overlay);//not a complete copy
                                    frame.OverlayImages = overlays.ToArray();
                                    nLast = i;
                                }
                            }
                            if (nLast != -1)
                                cmbVerses.SelectedIndex = nLast;
                        }                          
                    }
                    else
                        GMessageBox.SayError("لطفا یک لایه تصویری را انتخاب کنید.");
                }

            }
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
            GMessageBox.SayError("امکان فعال کردن این گزینه برای این قاب وجود ندارد.");
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
                    GMessageBox.SayError("امکان فعال کردن این گزینه برای این قاب وجود ندارد.");
                    return;
                }
            }



            using (ItemSelector dlg = new ItemSelector("تعداد مصرعها در هر قاب", new object[] { 2, 4 }, 2))
            {
                if(dlg.ShowDialog(this) == DialogResult.OK)
                {
                    PairNext((int)dlg.SelectedItem);                    
                }
            }

        }

        /// <summary>
        /// یکی کردن بعدیها یکی در میان
        /// </summary>
        /// <param name="n">تعداد مصرع ها در هر قاب</param>
        /// <param name="silentMode">نمایش پیغام</param>
        void PairNext(int n, bool silentMode = false)
        {
            int idx = cmbVerses.SelectedIndex;

            bool fisrtSet = true;

            for (int i = (idx + 1); i < cmbVerses.Items.Count; i += n)
            {
                int max = (i + n - 1);
                if (max > cmbVerses.Items.Count)
                    max = cmbVerses.Items.Count;
                for (int j = i; j < max; j++)
                {
                    GVideoFrame frame = cmbVerses.Items[j] as GVideoFrame;
                    frame.MasterFrame = (cmbVerses.Items[i - 1] as GVideoFrame);

                    frame.TextVerticalPosRatioPortion =
                        n == 2 ? frame.TextVerticalPosRatioPortionFrom - frame.MasterFrame.TextVerticalPosRatioPortion
                               :
                               //4:
                               j == i ? //2nd
                                2 * frame.TextVerticalPosRatioPortion
                               : j == i + 1 ? //3rd
                                frame.TextVerticalPosRatioPortionFrom - frame.MasterFrame.TextVerticalPosRatioPortion - frame.TextVerticalPosRatioPortion
                               //4th
                               : frame.TextVerticalPosRatioPortionFrom - frame.MasterFrame.TextVerticalPosRatioPortion;
                }

                if (fisrtSet)
                {
                    InvalidatePreview();
                    fisrtSet = false;
                    if(!silentMode)
                    {
                        if (MessageBox.Show("این چینش برای مصرعهای بعدی هم اعمال شود؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.No)
                            return;
                    }
                   
                }

            }
            InvalidatePreview();
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
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    Settings.Default.VidDefExt = Path.GetExtension(dlg.FileName).ToLower();
                    Settings.Default.Save();
                    InitiateRendering(dlg.FileName, Settings.Default.AudioId == 0);
                }
            }
        }

        private void btnSubtitle_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "SRT Files (*.srt)|*.srt";
                dlg.FileName = txtPoemId.Text + ".srt";
                if (dlg.ShowDialog(this) == DialogResult.OK)
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
                | MessageBoxOptions.RightAlign) == DialogResult.Yes)
            {                
                using (UnsplashImageTypeForm dlg = new UnsplashImageTypeForm())
                {
                    if (dlg.ShowDialog(this) != DialogResult.OK)
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


        private void btnCatBatch_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show
                (
                "با استفاده از این امکان، می‌توان از خوانشهای یک بخش ویدیو ساخت. ادامه می‌دهید؟",
                "تأییدیه",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading
                ) == DialogResult.No)
                return;

            DbBrowser db = Connect();
            if (db == null)
            {
                MessageBox.Show("(db == null)", "خطا", MessageBoxButtons.OK);
                return;
            }

            List<GanjoorPoet> poets = db.Poets;

            using (ItemSelector poetSeletor = new ItemSelector("انتخاب شاعر", poets.ToArray(), null))
            {
                if (poetSeletor.ShowDialog(this) == DialogResult.OK)
                {
                    GanjoorPoet poet = poetSeletor.SelectedItem as GanjoorPoet;

                    using (CategorySelector catSelector = new CategorySelector(poet._ID, db))
                    {
                        if (catSelector.ShowDialog(this) == DialogResult.OK)
                        {

                            GOverlayImage[] overlayImages = null;

                            if (cmbVerses.SelectedItem != null)
                            {
                                overlayImages = (cmbVerses.SelectedItem as GVideoFrame).OverlayImages;                               
                            }

                            

                            PoemAudio fakeAudio = new PoemAudio()
                            {
                                Id = 0,
                                Description = "",
                            };
                            List<PoemAudio.SyncInfo> fakeSync = new List<PoemAudio.SyncInfo>();
                            fakeSync.Add(new PoemAudio.SyncInfo()
                            {
                                VerseOrder = 0,
                                AudioMiliseconds = 4000,
                            });
                            fakeAudio.SyncArray = fakeSync.ToArray();

                            List<GVideoFrame> frames = new List<GVideoFrame>();

                            string strImagePath = Settings.Default.LastImagePath;
                            if (!string.IsNullOrEmpty(strImagePath))
                                if (!File.Exists(strImagePath))
                                    strImagePath = "";


                            GVideoFrame titleFrame = new GVideoFrame()
                            {
                                AudioBound = false,
                                StartInMiliseconds = -4000,
                                Text = poet._Name,
                                BackgroundImagePath = strImagePath,
                                TextVerticalPosRatioPortion = 1,
                                TextVerticalPosRatioPortionFrom = 4,
                                TextHorizontalPosRatioPortion = 1,
                                TextHorizontalPosRatioPortionFrom = 2,
                                MaxTextWidthRatioPortion = 10,
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

                            int nDurationDividedBy = 1;
                            titleFrame.StartInMiliseconds = -(fakeAudio.SyncArray[0].AudioMiliseconds / nDurationDividedBy);

                            if (overlayImages != null)
                                titleFrame.OverlayImages = overlayImages;


                            frames.Add(titleFrame);

                            string fullCat = "";
                            GanjoorCat cat = db.GetCategory(catSelector.SelectedCatID);
                            if (cat != null)
                            {
                                fullCat = cat._Text;

                                cat = db.GetCategory(cat._ParentID);
                                while (cat != null)
                                {
                                    if (cat._ID == poet._CatID)
                                        break;
                                    fullCat = cat._Text + " » " + fullCat;
                                    cat = db.GetCategory(cat._ParentID);
                                }
                            }

                            GVideoFrame poemFrame = new GVideoFrame()
                            {
                                AudioBound = false,
                                StartInMiliseconds = 0,
                                Text = fullCat,
                                BackgroundImagePath = strImagePath,
                                TextVerticalPosRatioPortion = 1,
                                TextVerticalPosRatioPortionFrom = 2,
                                TextHorizontalPosRatioPortion = 1,
                                TextHorizontalPosRatioPortionFrom = 2,
                                MaxTextWidthRatioPortion = 10,
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

                            string thirdFrameText = "به خوانش حمیدرضا محمدی";
                            using (ItemEditor dlg = new ItemEditor(EditItemType.General, "ویرایش متن", "متن:"))
                            {
                                dlg.ItemName = thirdFrameText;
                                if (dlg.ShowDialog(this) == DialogResult.OK)
                                {

                                    thirdFrameText = dlg.ItemName;                                    
                                }
                            }

                            GVideoFrame soundFrame = new GVideoFrame()
                            {
                                AudioBound = false,
                                StartInMiliseconds = 0,
                                Text = thirdFrameText,
                                BackgroundImagePath = strImagePath,
                                TextVerticalPosRatioPortion = 3,
                                TextVerticalPosRatioPortionFrom = 4,
                                TextHorizontalPosRatioPortion = 1,
                                TextHorizontalPosRatioPortionFrom = 2,
                                MaxTextWidthRatioPortion = 10,
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

                            frames.Add(titleFrame);

                            cmbVerses.Items.Clear();
                            cmbVerses.Items.AddRange(frames.ToArray());
                            cmbVerses.SelectedIndex = 0;

                            Color[] bkColors =
                                new Color[]
                                {
                                    Color.FromArgb(64, 0, 0), //darkred
                                    Color.FromArgb(0, 128, 0), //green
                                    Color.FromArgb(0, 0, 64), //darkblue
                                    Color.FromArgb(64, 0, 64), //purple
                                    Color.FromArgb(0, 64, 64), //green+blue
                                    Color.FromArgb(128, 64, 0), //brick
                                    Color.FromArgb(64, 64, 0), //dark olive
                                };
                            

                            for (int i = 0; i < cmbVerses.Items.Count; i++)
                            {
                                (cmbVerses.Items[i] as GVideoFrame).BackColor = bkColors[0];
                                (cmbVerses.Items[i] as GVideoFrame).TextBackColorAlpha = 0;
                            }

                            int colorIndex = 1;


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
                                dlg.FileName = catSelector.SelectedCatID.ToString();
                                if (dlg.ShowDialog(this) == DialogResult.OK)
                                {
                                    Settings.Default.VidDefExt = Path.GetExtension(dlg.FileName).ToLower();
                                    Settings.Default.Save();
                                    string mainOutFile = dlg.FileName;

                                    InitiateRendering(mainOutFile, true, false);



                                    int subtitleShift = 4;
                                    List<string> subTitleTexts = new List<string>();

                                    int nIdxSrtLine = 0;


                                    int catId = catSelector.SelectedCatID;                                   

                                    string catVideo = GenerateCatVideo(catId, db, Path.GetDirectoryName(mainOutFile), overlayImages, subTitleTexts, ref subtitleShift, ref nIdxSrtLine, bkColors, ref colorIndex);

                                   


                                    string mixStringPart1 = $"-i \"{mainOutFile}\" -i \"{catVideo}\"";
                                    string mixStringPart2 = "[0:v:0][0:a:0][1:v:0][1:a:0]";

                                    string tempOut = Path.Combine(Path.GetDirectoryName(mainOutFile), catId.ToString() + "-temp.mp4");

                                    RunFFmpegCommand(
                                        Settings.Default.FFmpegPath,
                                        $"{mixStringPart1} -filter_complex \"{mixStringPart2}concat=n=2:v=1:a=1[outv][outa]\" -map \"[outv]\" -map \"[outa]\" \"{tempOut}\""
                                        );

                                    File.Copy(tempOut, mainOutFile, true);

                                    File.Delete(catVideo);

                                   
                                    File.Delete(tempOut);

                                    File.WriteAllLines(Path.Combine(Path.GetDirectoryName(mainOutFile), Path.GetFileNameWithoutExtension(mainOutFile) + ".srt"), subTitleTexts.ToArray());


                                }
                            }




                        }
                    }
                }
            }


            db.CloseDb();



        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="catId"></param>
        /// <param name="db"></param>
        /// <param name="outputFolder"></param>
        /// <returns>file path to generated video</returns>
        private string GenerateCatVideo(int catId, DbBrowser db, string outputFolder, GOverlayImage[] overlayImages, List<string> subtitleTexts, ref int subtitleShift, ref int nIdxSrtLine, Color[] bkColors, ref int colorIndex)
        {
            List<string> poemVideos = new List<string>();
            int vIndex = -1;
            string mixStringPart1 = "";
            string mixStringPart2 = "";
            foreach (GanjoorPoem poem in db.GetPoems(catId))
            {
                PoemAudio[] audio = db.GetPoemAudioFiles(poem._ID);
                if (audio.Length == 0)
                {
                    MessageBox.Show("err: " + poem._Title);
                    return null;
                }

                Settings.Default.PoemId = poem._ID;
                Settings.Default.AudioId = audio[0].Id;
                Settings.Default.Save();

                UpdatePoemAndAudioInfo(db);

                for (int i = 0; i < cmbVerses.Items.Count; i++)
                {
                    (cmbVerses.Items[i] as GVideoFrame).BackColor = bkColors[colorIndex];
                    (cmbVerses.Items[i] as GVideoFrame).TextBackColorAlpha = 0;
                }





                for (int i = 3; i < cmbVerses.Items.Count; i++)
                {
                    (cmbVerses.Items[i] as GVideoFrame).TextVerticalPosRatioPortion = 8;
                    (cmbVerses.Items[i] as GVideoFrame).TextVerticalPosRatioPortionFrom = 20;
                }

                cmbVerses.SelectedIndex = 3;

                subtitleTexts.AddRange(CreateSubTitle(db, 0, subtitleShift, ref nIdxSrtLine, new int[] { 0, 1}));



                PairNext(2, true);

                string catName = db.GetCategory(poem._CatID)._Text;



                List<GVideoFrame> frames = new List<GVideoFrame>();
                for (int i = 0; i < cmbVerses.Items.Count; i++)
                {
                    GVideoFrame frame = cmbVerses.Items[i] as GVideoFrame;

                    frames.Add(frame);

                    if (frame.MasterFrame == null)
                    {
                        frame.OverlayImages = overlayImages;

                        if(i > 0)
                        {
                            GVideoFrame catFrame = new GVideoFrame()
                            {
                                AudioBound = false,
                                StartInMiliseconds = 0,
                                Text = catName,
                                BackgroundImagePath = "",
                                TextVerticalPosRatioPortion = 18,
                                TextVerticalPosRatioPortionFrom = 20,
                                TextHorizontalPosRatioPortion = 1,
                                TextHorizontalPosRatioPortionFrom = 10,
                                MaxTextWidthRatioPortion = 10,
                                MaxTextWidthRatioPortionFrom = 10,
                                BackColor = Color.White,
                                TextColor = Color.White,
                                TextBackColor = Color.Black,
                                BorderColor = Color.Black,
                                TextBackColorAlpha = 0,
                                Shape = GTextBoxShape.Rectangle,
                                TextBackRectThickness = 0,
                                Font = new Font(Settings.Default.LastUsedFont.FontFamily, Settings.Default.LastUsedFont.Size / 4),
                                MasterFrame = frame,
                                ShowLogo = false
                            };
                            frames.Add(catFrame);
                            GVideoFrame poemFrame = new GVideoFrame()
                            {
                                AudioBound = false,
                                StartInMiliseconds = 0,
                                Text = poem._Title,
                                BackgroundImagePath = "",
                                TextVerticalPosRatioPortion = 19,
                                TextVerticalPosRatioPortionFrom = 20,
                                TextHorizontalPosRatioPortion = 1,
                                TextHorizontalPosRatioPortionFrom = 10,
                                MaxTextWidthRatioPortion = 10,
                                MaxTextWidthRatioPortionFrom = 10,
                                BackColor = Color.White,
                                TextColor = Color.White,
                                TextBackColor = Color.Black,
                                BorderColor = Color.Black,
                                TextBackColorAlpha = 0,
                                Shape = GTextBoxShape.Rectangle,
                                TextBackRectThickness = 0,
                                Font = new Font(Settings.Default.LastUsedFont.FontFamily, Settings.Default.LastUsedFont.Size / 4),
                                MasterFrame = frame,
                                ShowLogo = false
                            };
                            frames.Add(poemFrame);
                        }                        
                    }
                }

                cmbVerses.Items.Clear();
                cmbVerses.Items.AddRange(frames.ToArray());





                string poemVid = Path.Combine(outputFolder, poem._ID.ToString() + ".mp4");

                int playTime = InitiateRendering(poemVid, false, false);                

                subtitleShift += (playTime / 1000);

                poemVideos.Add(poemVid);

                vIndex++;
                mixStringPart1 += $" -i \"{poemVid}\"";
                mixStringPart2 += $"[{vIndex}:v:0][{vIndex}:a:0]";

                colorIndex++;
                if (colorIndex >= bkColors.Length)
                    colorIndex = 0;
            }


            string catVideo = Path.Combine(outputFolder, catId.ToString() + "-poems.mp4");

            RunFFmpegCommand(
                Settings.Default.FFmpegPath,
                $"{mixStringPart1} -filter_complex \"{mixStringPart2}concat=n={poemVideos.Count}:v=1:a=1[outv][outa]\" -map \"[outv]\" -map \"[outa]\" \"{catVideo}\""
                );

            foreach (string poemvideo in poemVideos)
            {
                File.Delete(poemvideo);
            }

            return catVideo;
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
                        using (Image bmpBkgnImage = Image.FromFile(frame.BackgroundImagePath))
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
                    g.DrawImage(Resources.glogo, new Rectangle(
                        szImageSize.Width - Resources.glogo.Width - 2, szImageSize.Height - Resources.glogo.Height - 2,
                        Resources.glogo.Width, Resources.glogo.Height)
                        );
                }

                if(frame.OverlayImages != null)
                {
                    foreach(GOverlayImage overlay in frame.OverlayImages)
                    {
                        if(!string.IsNullOrEmpty(overlay.ImagePath))
                        {
                            using (Image bmpOverlay = Image.FromFile(overlay.ImagePath))
                            {
                                g.DrawImage(bmpOverlay,
                                     overlay.HorizontalPosRatioPortion * szImageSize.Width / overlay.HorizontalPosRatioPortionFrom  - bmpOverlay.Width / 2 * overlay.ScaleRatioPortion / overlay.ScaleRatioPortionFrom,
                                     overlay.VerticalPosRatioPortion * szImageSize.Height / overlay.VerticalPosRatioPortionFrom  - bmpOverlay.Height / 2 * overlay.ScaleRatioPortion / overlay.ScaleRatioPortionFrom,
                                     overlay.ScaleRatioPortion * bmpOverlay.Width / overlay.ScaleRatioPortionFrom,
                                     overlay.ScaleRatioPortion * bmpOverlay.Height / overlay.ScaleRatioPortionFrom
                                     );
                            }
                        }
                    }
                }

                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                SizeF szText = g.MeasureString(frame.Text, frame.Font, frame.MaxTextWidthRatioPortion * (int)(txtWidth.Value) / frame.MaxTextWidthRatioPortionFrom, new StringFormat(StringFormatFlags.DirectionRightToLeft));
                RectangleF rcText = new RectangleF((frame.TextHorizontalPosRatioPortion * szImageSize.Width / frame.TextHorizontalPosRatioPortionFrom) - szText.Width / 2, (frame.TextVerticalPosRatioPortion * szImageSize.Height / frame.TextVerticalPosRatioPortionFrom) - szText.Height / 2, szText.Width, szText.Height);


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

                if(frame.TextBorderValue > 0)
                {
                    using (Image imgTemp = new Bitmap((int)rcText.Width, (int)rcText.Height))
                    {
                        RectangleF rcTemp = rcText;
                        rcTemp.X = rcTemp.Y = 0;
                        Color bkTemp = Color.FromArgb(255 - frame.TextBorderColor.R, 255 - frame.TextBorderColor.G, 255 - frame.TextBorderColor.B);
                        using (Graphics gTemp = Graphics.FromImage(imgTemp))
                        {
                            using (SolidBrush bkBrush = new SolidBrush(bkTemp))
                                gTemp.FillRectangle(bkBrush, rcTemp);                            

                            using (SolidBrush txtBrush = new SolidBrush(frame.TextBorderColor))
                                gTemp.DrawString(frame.Text, frame.Font, txtBrush,
                                    rcTemp
                                    , new StringFormat(StringFormatFlags.DirectionRightToLeft)
                                    );
                        }

                        (imgTemp as Bitmap).MakeTransparent(bkTemp);

                        
                        for(int i= -frame.TextBorderValue; i<=frame.TextBorderValue; i++)
                        {
                            RectangleF rcTarget = rcText;
                            rcTarget.X -= i;
                            rcTarget.Y -= i;
                            rcTarget.Width += 2 * i;
                            rcTarget.Height += 2 * i;
                            g.DrawImage(imgTemp, rcTarget);
                        }
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
        private int InitiateRendering(string outfilePath, bool noAudio = false, bool askForPlaying = true)
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
                                    return 0;
                                }
                                Settings.Default.FFmpegPath = ffmpegPath;
                                Settings.Default.Save();
                            }
                            else
                            {
                                return 0;
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
                        return 0;
                    }
                }
            }

            DbBrowser db = Connect();
            if (db == null)
            {
                MessageBox.Show("(db == null)", "خطا", MessageBoxButtons.OK);
                return 0;
            }

            string audioFilePath = "";
            if(!noAudio)
            {
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
                    return 0;
                }

                if (!File.Exists(audioFilePath))
                {
                    MessageBox.Show("فایل صوتی در مسیر تعیین شده وجود ندارد.", "خطا", MessageBoxButtons.OK);
                    return 0;
                }
            }

            this.Enabled = false;

            string wav = "";
            int playtime = 
                noAudio ? 
                (cmbVerses.Items[cmbVerses.Items.Count - 1] as GVideoFrame).StartInMiliseconds  + 4000
                : 0; 
            if (!noAudio)
            {
                Mp3FileReader r = new Mp3FileReader(audioFilePath);
                playtime = r.TotalTime.Milliseconds;
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
                                return 0;
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
                //this is not a general solution and should be reviewed and rethought
                if(!frame.AudioBound && frame.StartInMiliseconds < 0)
                {                    
                    duration = -frame.StartInMiliseconds;
                }
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
                ITrack audioTrack = bIsWmv && !noAudio ? timeline.AddAudioGroup().AddTrack() : null;
                IClip audio = bIsWmv && !noAudio ? audioTrack.AddAudio(wav, dSoundStart, playtime / 1000.0) : null;

                lblStatus.Text = "تولید خروجی: متأسفانه نزدیک به تا ابد طول می‌کشد :(";
                string wmvProfile = Resources.WMV_HD_960x720;
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
                                                using (Image img1 = Image.FromFile(ffmpegInputFiles[i]))
                                                {
                                                    g.DrawImage(img1, new PointF(j * (float)szImageSize.Width / 4, 0));
                                                }
                                                using (Image img2 = Image.FromFile(ffmpegInputFiles[i + 1]))
                                                {
                                                    g.DrawImage(img2, new PointF((j - 4) * (float)szImageSize.Width / 4, 0));
                                                }
                                            }
                                            break;
                                        case GTransitionEffect.ToLeft:
                                            {
                                                using (Image img1 = Image.FromFile(ffmpegInputFiles[i]))
                                                {
                                                    g.DrawImage(img1, new PointF(-j * (float)szImageSize.Width / 4, 0));
                                                }
                                                using (Image img2 = Image.FromFile(ffmpegInputFiles[i + 1]))
                                                {
                                                    g.DrawImage(img2, new PointF((4 - j) * (float)szImageSize.Width / 4, 0));
                                                }
                                            }
                                            break;
                                        case GTransitionEffect.ToUp:
                                            {
                                                using (Image img1 = Image.FromFile(ffmpegInputFiles[i]))
                                                {
                                                    g.DrawImage(img1, new PointF(0, (-j) * (float)szImageSize.Height / 4));
                                                }
                                                using (Image img2 = Image.FromFile(ffmpegInputFiles[i + 1]))
                                                {
                                                    g.DrawImage(img2, new PointF(0, (4 - j) * (float)szImageSize.Height / 4));
                                                }
                                            }
                                            break;
                                        case GTransitionEffect.ToBottom:
                                            {
                                                using (Image img1 = Image.FromFile(ffmpegInputFiles[i]))
                                                {
                                                    g.DrawImage(img1, new PointF(0, j * (float)szImageSize.Height / 4));
                                                }
                                                using (Image img2 = Image.FromFile(ffmpegInputFiles[i + 1]))
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


                if (!noAudio)
                {
                    File.Copy(wav, Path.Combine(Path.GetTempPath(), Path.GetFileName(wav)), true);
                }

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
                                    return 0;
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


                    if (!noAudio)
                    {
                        lblStatus.Text = "ترکیب صدا با ویدیو";
                        Application.DoEvents();

                        RunFFmpegCommand(
                            ffmpegPath,
                            $"-itsoffset {dSoundStart} -i \"{Path.GetFileName(wav)}\" -filter_complex movie={Path.GetFileName(resampledVideoBackground)}:loop=0,setpts=N/FRAME_RATE/TB -shortest -codec:a libmp3lame -qscale:a 9 -f mp4 -c:v libx264 -preset slow  -async 1 \"{mixedwithAudioVideoBackground}\""
                            );
                        File.Delete(resampledVideoBackground);
                    }
                    else
                    {
                        mixedwithAudioVideoBackground = resampledVideoBackground;
                    }

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
                    if (noAudio)
                    {
                        RunFFmpegCommand(ffmpegPath,

                            String.Format("-y -i {0} -f lavfi -i anullsrc=cl=1 -shortest -qscale:a 9 -f mp4 -c:v libx264 -c:a aac -preset slow -tune stillimage -async 1 {3}",
                            Path.GetFileName(ffconcat),
                            dSoundStart,
                            Path.GetFileName(wav),
                            Path.GetFileName(outInTempPath),
                            Settings.Default.LastImageWidth,
                            Settings.Default.LastImageHeight)
                            );
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



                if (!noAudio)
                {
                    File.Delete(Path.Combine(Path.GetTempPath(), Path.GetFileName(wav)));
                }
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
                if(askForPlaying)
                if (MessageBox.Show("آیا مایلید فایل خروجی را مشاهده کنید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign) == DialogResult.Yes)
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

            return playtime;

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

            int nIdxSrtLine = 0;
            int shiftTime = 0;
            List<string> lines = CreateSubTitle(db, 0, shiftTime, ref nIdxSrtLine);

            if(lines != null)
            File.WriteAllLines(outfilePath, lines.ToArray());

            db.CloseDb();

            if (lines != null)
                MessageBox.Show("زیرنویس ایجاد شد.", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);

        }


       
        private List<string> CreateSubTitle(DbBrowser db, int playtime, int shiftTime, ref int nIdxSrtLine, int[] ignoredFrameIndices = null)
        {         

                double dSoundStart = -1.0;

                if(ignoredFrameIndices == null)
                {
                    ignoredFrameIndices = new int[] { };
                }

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
                                    return null;
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

            dSoundStart += shiftTime;

            if(playtime <= 0)
            {
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

                if (string.IsNullOrEmpty(audioFilePath))
                {
                    MessageBox.Show("(string.IsNullOrEmpty(audioFilePath))", "خطا", MessageBoxButtons.OK);
                    return null;
                }

                Mp3FileReader r = new Mp3FileReader(audioFilePath);
                playtime = r.TotalTime.Milliseconds;

                using (Mp3FileReader mp3 = new Mp3FileReader(audioFilePath))
                {
                    playtime = (int)mp3.TotalTime.TotalMilliseconds;
                }
            }           



            List<string> lines = new List<string>();




            for (int i = 0; i < cmbVerses.Items.Count; i++)
            {
                GVideoFrame frame = cmbVerses.Items[i] as GVideoFrame;
                if (frame.MasterFrame != null)
                {
                    if (!ignoredFrameIndices.Contains(i))
                    {
                        lines.Add(frame.Text);
                        lines.Add("");
                    }
                    continue;
                }
                
                nIdxSrtLine++;
                DateTime dtStart = frame.StartInMiliseconds < 0 ? new DateTime(0).AddMilliseconds(shiftTime * 1000) : new DateTime(0).AddMilliseconds(((frame.StartInMiliseconds) + dSoundStart * 1000.0));
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

                if (!ignoredFrameIndices.Contains(i))
                {
                    lines.Add(frame.Text);
                    lines.Add("");
                }
            }

            return lines;
           
        }

        #endregion
    }
}
