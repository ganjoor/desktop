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

namespace gsync2vid
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            txtSrcDb.Text = DbPath;
            UpdateConnectionStatus();
            UpdatePoemAndAudioInfo();
        }

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


            string strTitle = "";

            GanjoorPoem poem = db.GetPoem(Settings.Default.PoemId);
            if (poem != null)
            {
                GanjoorPoet poet = db.GetPoetForCat(poem._CatID);
                strTitle = poet.ToString();
                strTitle = strTitle + " - " + poem.ToString();

                PoemAudio[] audioFiles = db.GetPoemAudioFiles(poem._ID);
                if (audioFiles.Length > 0)
                {
                    foreach(PoemAudio audio in audioFiles)
                        if (audio.Id == Settings.Default.AudioId)
                        {
                            strTitle = strTitle + " - " + audio.Description;
                            txtPoem.Text = strTitle;


                            int nDurationDividedBy = 1;

                            PoemAudioPlayer player = new PoemAudioPlayer();
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

                            

                            
                            List<GVideoFrame> frames = new List<GVideoFrame>();


                            frames.Add( new GVideoFrame()
                            {
                                AudioBound = false,
                                DurationInMiliseconds = 4000,
                                Text = poem._Title,
                                BackgroundImagePath = "",
                                MainTextPosRatioPortion = 1,
                                MainTextPosRatioPortionFrom = 2,
                                MaxTextWidthRatioPortion = 2,
                                MaxTextWidthRatioPortionFrom = 3,
                                BackColor = Color.White,
                                TextColor = Color.White,
                                TextBackColor = Color.Black,
                                TextBackColorAlpha = 100,
                                Font = Settings.Default.LastUsedFont
                            });

                            List<GanjoorVerse> verses = db.GetVerses(poem._ID);
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
                                    DurationInMiliseconds = SyncInfo.AudioMiliseconds / nDurationDividedBy,
                                    Text = verse == null ? "" : verse._Text,
                                    BackgroundImagePath = "",
                                    MainTextPosRatioPortion = 1,
                                    MainTextPosRatioPortionFrom = 2,
                                    MaxTextWidthRatioPortion = 2,
                                    MaxTextWidthRatioPortionFrom = 3,
                                    BackColor = Color.White,
                                    TextColor = Color.White,
                                    TextBackColor = Color.Black,
                                    TextBackColorAlpha = 100,
                                    Font = Settings.Default.LastUsedFont
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
                                        MessageBox.Show("خوانشی یافت نشد.", "خطا", MessageBoxButtons.OK);
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
            lblDuration.Visible = !frame.AudioBound;
            txtDurationInMiliseconds.Visible = !frame.AudioBound;
            txtBackgroundImage.Text = frame.BackgroundImagePath;
            btnBackColor.BackColor = frame.BackColor;
            btnTextColor.BackColor = frame.TextColor;
            btnTextBackColor.BackColor = frame.TextBackColor;
            trckAlpha.Value = frame.TextBackColorAlpha;
            txtFont.Text = frame.Font.Name + "(" + frame.Font.Style.ToString() + ") " + frame.Font.Size.ToString();
            trckHPosition.Value = trckHPosition.Maximum / frame.MainTextPosRatioPortionFrom * (frame.MainTextPosRatioPortionFrom - frame.MainTextPosRatioPortion);
            trckMaxTextWidth.Value = trckMaxTextWidth.Maximum / frame.MaxTextWidthRatioPortionFrom * frame.MaxTextWidthRatioPortion;

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
                dlg.Filter = "JPEG Files (*.jpg)|*.jpg";
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {

                    string strOldBackgroundImagePath = frame.BackgroundImagePath;

                    int idx = cmbVerses.SelectedIndex;
                    for (int i = idx ; i < cmbVerses.Items.Count; i++)
                        if (
                            string.IsNullOrEmpty((cmbVerses.Items[i] as GVideoFrame).BackgroundImagePath)
                            ||
                            (cmbVerses.Items[i] as GVideoFrame).BackgroundImagePath == strOldBackgroundImagePath
                            )
                            (cmbVerses.Items[i] as GVideoFrame).BackgroundImagePath = dlg.FileName;                            

                    txtBackgroundImage.Text = frame.BackgroundImagePath;

                    InvalidatePreview();
                }
            }
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
                            (cmbVerses.Items[i] as GVideoFrame).Font = dlg.Font;

                    txtFont.Text = frame.Font.Name + "(" + frame.Font.Style.ToString() + ") " + frame.Font.Size.ToString();

                    Settings.Default.LastUsedFont = frame.Font;
                    Settings.Default.Save();

                    InvalidatePreview();
                }
            }
        }


        private void trckHPosition_Scroll(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;

            int idx = cmbVerses.SelectedIndex;

            for (int i = idx; i < cmbVerses.Items.Count; i++)
            {
                (cmbVerses.Items[i] as GVideoFrame).MainTextPosRatioPortionFrom = trckHPosition.Maximum;
                (cmbVerses.Items[i] as GVideoFrame).MainTextPosRatioPortion = (trckHPosition.Maximum - trckHPosition.Value);                
            }

            InvalidatePreview();

        }

        private void trckMaxTextWidth_Scroll(object sender, EventArgs e)
        {
            if (cmbVerses.SelectedItem == null)
                return;

            int idx = cmbVerses.SelectedIndex;

            for (int i = idx; i < cmbVerses.Items.Count; i++)
            {
                (cmbVerses.Items[i] as GVideoFrame).MaxTextWidthRatioPortionFrom = trckMaxTextWidth.Maximum;
                (cmbVerses.Items[i] as GVideoFrame).MaxTextWidthRatioPortion = trckMaxTextWidth.Value;
            }

            InvalidatePreview();
        }


        #endregion

        #region Render Frame
        private Image RenderFrame(GVideoFrame frame, Size szImageSize)
        {
            if (frame == null)
                return null;

            Image imgOutput = new Bitmap(szImageSize.Width, szImageSize.Height);

            using (Graphics g = Graphics.FromImage(imgOutput))
            {
                //BackColor
                using (SolidBrush brshBackColor = new SolidBrush(frame.BackColor))
                    g.FillRectangle(brshBackColor, new Rectangle(0, 0, szImageSize.Width, szImageSize.Height));

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
                
                SizeF szText = g.MeasureString(frame.Text, frame.Font, frame.MaxTextWidthRatioPortion * (int)(txtWidth.Value) / frame.MaxTextWidthRatioPortionFrom , new StringFormat(StringFormatFlags.DirectionRightToLeft));
                using (SolidBrush bkTextBrush = new SolidBrush(Color.FromArgb(frame.TextBackColorAlpha, frame.TextBackColor)))
                    g.FillRectangle(bkTextBrush, (szImageSize.Width / 2) - szText.Width / 2, (frame.MainTextPosRatioPortion * szImageSize.Height / frame.MainTextPosRatioPortionFrom) - szText.Height / 2, szText.Width, szText.Height);
                using(SolidBrush txtBrush =  new SolidBrush(frame.TextColor))
                    g.DrawString(frame.Text, frame.Font, txtBrush, 
                        new RectangleF((szImageSize.Width / 2) - szText.Width / 2, (frame.MainTextPosRatioPortion * szImageSize.Height / frame.MainTextPosRatioPortionFrom) - szText.Height / 2, szText.Width, szText.Height)
                        , new StringFormat(StringFormatFlags.DirectionRightToLeft)
                        );


            }

            return imgOutput;

        }
        #endregion


        private List<string> _lstDeleteFileList = new List<string>();

        private void btnPreview_Click(object sender, EventArgs e)
        {
            SaveShowPreviewImage();
        }

        private void SaveShowPreviewImage()
        {
            if (pbxPreview.Image != null)
            {
                string strTempImage = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".jpg");
                pbxPreview.Image.Save(strTempImage);
                _lstDeleteFileList.Add(strTempImage);
                System.Diagnostics.Process.Start(strTempImage);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach(string fileName in _lstDeleteFileList)
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtWidth.Value = 960M;
            txtHeight.Value = 720M;
        }

        private void txtWidth_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.LastImageWidth = (int)txtWidth.Value;
            Settings.Default.Save();
        }

        private void txtHeight_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.LastImageHeight = (int)txtHeight.Value;
            Settings.Default.Save();
        }



    }
}
