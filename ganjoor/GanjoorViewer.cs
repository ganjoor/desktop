﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using ganjoor.Properties;


namespace ganjoor
{
    public partial class GanjoorViewer : UserControl
    {
        #region Constructor
        public GanjoorViewer()
        {
            InitializeComponent();

            //scroll using arrow keys:
            this.PreviewKeyDown += new PreviewKeyDownEventHandler(GanjoorViewer_PreviewKeyDown);

            ApplyUISettings();

            _iCurCat = _iCurPoem = 0;
            _strLastPhrase = "";
            _history = new Stack<GarnjoorBrowsingHistory>();

            if (!DesignMode && _db == null)
            {
                _db = new DbBrowser();                
            }         
        }
        #endregion

        #region Database Browser
        private static DbBrowser _db =  null;
        #endregion

        #region Current Things
        private int _iCurCat;
        private int _iCurCatStart;
        private int _iCurPoem;
        private string _strPage;
        private int _iCurSearchPoet;
        private int _iCurSearchStart;
        private int _iCurSearchPageCount;
        private string _strLastPhrase;
        private bool _FavsPage;
        #endregion

        #region Constants
        private const int DistanceFromTop = 10;
        private int DistanceBetweenLines = 20;
        private const int DistanceFromRight = 20;
        private const int DistanceFromRightStep = 10;
        #endregion

        #region Events
        private void GanjoorViewer_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                if (_db.Failed)
                {
                    MessageBox.Show(_db.LastError, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (Settings.Default.WasShowingFavs)
                    {
                        ShowFavs(Settings.Default.LastSeachStart, Settings.Default.FavItemsInPage, false);
                    }
                    else
                    if (Settings.Default.LastCat != 0)
                    {
                        if (Settings.Default.LastPoem != 0)
                        {
                            ShowPoem(_db.GetPoem(Settings.Default.LastPoem), false);
                        }
                        else
                        {
                            GanjoorCat cat = _db.GetCategory(Settings.Default.LastCat);
                            if(cat != null)
                                cat._StartPoem =  Settings.Default.LastCatStart;                            
                            ShowCategory(cat, false);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Settings.Default.LastSearchPhrase))
                        {
                            ShowSearchResults(Settings.Default.LastSearchPhrase, Settings.Default.LastSeachStart, Settings.Default.SearchPageItems, Settings.Default.LastSearchPoetID, false);
                        }
                        else
                            ShowHome(true);
                    }
                }
            }
        }
        private void lblCat_Click(object sender, EventArgs e)
        {
            GanjoorCat category = null;
            if (sender != null)
                category = ((GanjoorCat)((LinkLabel)sender).Tag);
            else
                category = new GanjoorCat(0, 0, "", 0, "");


            ShowCategory(category, true);

        }

        private void ShowCategory(GanjoorCat category, bool keepTrack)
        {
            if (EditMode)
                Save();
            if (category == null)
            {
                ShowHome(keepTrack);
                return;
            }
            if (keepTrack)
                UpdateHistory();            
            Cursor = Cursors.WaitCursor; Application.DoEvents();
            this.SuspendLayout();
            this.VerticalScroll.Value = 0;this.HorizontalScroll.Value = 0;
            this.Controls.Clear();


            int catsTop = DistanceFromTop;
            int lastDistanceFromRight;
            ShowCategory(category, ref catsTop, out lastDistanceFromRight, true);

            List<GanjoorCat> subcats = _db.GetSubCategories(category._ID);
            if (subcats.Count != 0)
                lastDistanceFromRight += 2 * DistanceFromRightStep;
            for (int i = 0; i < subcats.Count; i++)
            {
                LinkLabel lblCat = new LinkLabel();
                lblCat.Tag = subcats[i];
                lblCat.AutoSize = true;
                lblCat.Text = subcats[i]._Text;
                lblCat.Location = new Point(lastDistanceFromRight, catsTop + i * DistanceBetweenLines);
                lblCat.LinkBehavior = LinkBehavior.HoverUnderline;
                lblCat.BackColor = Color.Transparent;
                lblCat.LinkColor = Settings.Default.LinkColor;
                lblCat.ForeColor = lblCat.LinkColor;
                lblCat.Click += new EventHandler(lblCat_Click);
                this.Controls.Add(lblCat);
            }

            int poemsDistanceFromRight = DistanceFromRight + lastDistanceFromRight;
            int poemsTop = catsTop + subcats.Count * DistanceBetweenLines;
            List<GanjoorPoem> poems = _db.GetPoems(category._ID);
            category._StartPoem = Math.Max(0, category._StartPoem);
            GanjoorCat preCat = category._StartPoem == 0 ? null : new GanjoorCat(category, category._StartPoem - Settings.Default.MaxPoemsInList);
            GanjoorCat nextCat = category._StartPoem + Settings.Default.MaxPoemsInList < poems.Count ? new GanjoorCat(category, category._StartPoem + Settings.Default.MaxPoemsInList) : null;
            int ParagraphShift = 0;
            for (int i = category._StartPoem; i < Math.Min(poems.Count, category._StartPoem + Settings.Default.MaxPoemsInList); i++)
            {                

                LinkLabel lblPoem = new LinkLabel();                
                lblPoem.Tag = poems[i];
                lblPoem.AutoSize = true;
                lblPoem.Text = poems[i]._Title;
                List<GanjoorVerse> v = _db.GetVerses(poems[i]._ID, 1);
                if (v.Count > 0)
                    lblPoem.Text += " : " + v[0]._Text;
                lblPoem.Location = new Point(poemsDistanceFromRight, poemsTop + (i - category._StartPoem) * DistanceBetweenLines + ParagraphShift);
                Size szPoemTitleSizeWithWordWrap = TextRenderer.MeasureText(lblPoem.Text, this.Font, new Size(this.Width - lastDistanceFromRight - 20, Int32.MaxValue), TextFormatFlags.WordBreak);
                Size szPoemTitleSizeWithoutWordWrap = TextRenderer.MeasureText(lblPoem.Text, this.Font, new Size(this.Width - lastDistanceFromRight - 20, Int32.MaxValue), TextFormatFlags.Default);
                if (szPoemTitleSizeWithWordWrap.Width != szPoemTitleSizeWithoutWordWrap.Width)
                {                    
                    ParagraphShift += (szPoemTitleSizeWithWordWrap.Height - lblPoem.Height / 2);
                    lblPoem.AutoSize = false;
                    lblPoem.Size = new Size(this.Width - lastDistanceFromRight - 20, szPoemTitleSizeWithWordWrap.Height);
                    
                }
                
                lblPoem.LinkBehavior = LinkBehavior.HoverUnderline;
                lblPoem.BackColor = Color.Transparent;
                lblPoem.LinkColor = Settings.Default.LinkColor;
                lblPoem.ForeColor = lblPoem.LinkColor;
                lblPoem.Click += new EventHandler(lblPoem_Click);
                this.Controls.Add(lblPoem);
                if (_db.IsPoemFaved(poems[i]._ID))
                {
                    PictureBox fav = new PictureBox();
                    fav.BackColor = Color.Transparent;
                    fav.Image = Resources.fav;
                    fav.Size = new Size(16, 16);
                    fav.Location = new Point(lblPoem.Location.X - 16, lblPoem.Location.Y + (lblPoem.Size.Height - 16) /2);
                    this.Controls.Add(fav);
                }

            }

            //یک بر چسب اضافی برای اضافه شدن فضای پایین فرم

            Label lblDummy = new Label();
            lblDummy.Text = " ";
            int poemsCount = category._StartPoem + Settings.Default.MaxPoemsInList < poems.Count ? Settings.Default.MaxPoemsInList : poems.Count - category._StartPoem;
            lblDummy.Location = new Point(200, poemsTop + poemsCount * DistanceBetweenLines);
            lblDummy.BackColor = Color.Transparent;
            this.Controls.Add(lblDummy);


            //کلک راست به چپ!
            foreach (Control ctl in this.Controls)
            {
                ctl.Location = new Point(this.Width - ctl.Right, ctl.Location.Y);
            }
            AssignPreviewKeyDownEventToControls();
            this.ResumeLayout();
            Cursor = Cursors.Default;
            _strLastPhrase = null;
            if (null != OnPageChanged)
                OnPageChanged(_strPage, false, true, false, false, string.Empty, preCat, nextCat);
        }

        private void ShowCategory(GanjoorCat category, ref int catsTop, out int lastDistanceFromRight, bool highlightCat)
        {
            lastDistanceFromRight = DistanceFromRight;


            _strPage = "";

            //اجداد این دسته
            List<GanjoorCat> ancestors = _db.GetParentCategories(category);

            for (int i = 0; i < ancestors.Count; i++)
            {
                _strPage += ancestors[i]._Text;
                if (category!=null && 0 != category._ID) _strPage += " -> ";

                LinkLabel lblCat = new LinkLabel();                
                lblCat.Tag = ancestors[i];
                lblCat.AutoSize = true;
                lblCat.Text = ancestors[i]._Text;
                lastDistanceFromRight += 2 * DistanceFromRightStep;
                lblCat.Location = new Point(lastDistanceFromRight, catsTop + i * DistanceBetweenLines);
                lblCat.LinkBehavior = LinkBehavior.HoverUnderline;
                lblCat.BackColor = Color.Transparent;
                lblCat.LinkColor = Settings.Default.LinkColor;
                lblCat.ForeColor = lblCat.LinkColor;
                lblCat.Click += new EventHandler(lblCat_Click);
                this.Controls.Add(lblCat);
            }


            catsTop += (ancestors.Count * DistanceBetweenLines);
            //خود این دسته

            if (category !=null && category._ID != 0)
            {
                _strPage += category._Text;

                LinkLabel lblMe = new LinkLabel();                
                lblMe.Tag = category;
                lblMe.AutoSize = true;
                lblMe.Text = category._Text;
                lastDistanceFromRight += 2 * DistanceFromRightStep;
                lblMe.Location = new Point(lastDistanceFromRight, catsTop);
                lblMe.LinkVisited = highlightCat;
                lblMe.LinkBehavior = LinkBehavior.HoverUnderline;
                lblMe.BackColor = Color.Transparent;
                lblMe.VisitedLinkColor = Settings.Default.CurrentLinkColor;
                lblMe.LinkColor = Settings.Default.LinkColor;
                lblMe.ForeColor = highlightCat ? lblMe.VisitedLinkColor : lblMe.LinkColor;
                lblMe.Click += new EventHandler(lblCat_Click);
                this.Controls.Add(lblMe);

                catsTop += DistanceBetweenLines;
            }
            if (category != null)
            {
                GanjoorPoet poet = _db.GetPoet(category._PoetID);
                if (poet != null && poet._CatID == category._ID)
                {
                    HighlightLabel lblBio = new HighlightLabel();
                    lblBio.BackColor = Color.Transparent;
                    lblBio.AutoSize = false;
                    {
                        int labelHeight = lblBio.Height;
                        Size sz = new Size(this.Width - lastDistanceFromRight - 20, Int32.MaxValue);
                        sz = TextRenderer.MeasureText(poet._Bio, this.Font, sz, TextFormatFlags.WordBreak);
                        lblBio.Size = new Size(this.Width - lastDistanceFromRight - 20, sz.Height);                        
                        lblBio.Location = new Point(lastDistanceFromRight, catsTop);
                        lblBio.Text = poet._Bio;
                        this.Controls.Add(lblBio);
                        catsTop += sz.Height + 10;
                    }

                }
            }

            _FavsPage = false;
            _iCurCat = category!=null ? category._ID : 0;
            _iCurCatStart = category!=null ? category._StartPoem : 0;
            _iCurPoem = 0;
        }
        private void lblPoem_Click(object sender, EventArgs e)
        {
            ShowPoem(
                ((GanjoorPoem)((LinkLabel)sender).Tag)
                ,
                true                
                );
        }
        void lblNum_Click(object sender, EventArgs e)
        {
            int y = VerticalScroll.Value;
            GanjoorVerse verse = (sender as Control).Tag as GanjoorVerse;
            if (!_db.ToggleFav(verse._PoemID, verse._Order))
            {
                Control pbx = null;
                foreach (Control ctl in this.Controls)
                    if (ctl.Tag != null && ctl.Tag is GanjoorVerse && (ctl.Tag as GanjoorVerse)._Order == verse._Order)
                    {
                        if (ctl is PictureBox)
                        {
                            pbx = ctl;
                        }
                        else
                        {
                            if (ctl is LinkLabel)
                                ctl.Visible = true;
                        }
                    }
                if (pbx != null)
                    this.Controls.Remove(pbx);
            }
            else
            {
                PictureBox fav = new PictureBox();
                fav.BackColor = Color.Transparent;
                fav.Image = Resources.fav;
                fav.Size = new Size(16, 16);
                fav.Location = new Point((sender as Control).Location.X + (sender as Control).Size.Width - 16, (sender as Control).Location.Y);
                fav.Tag = verse;
                fav.Cursor = Cursors.Hand;
                fav.Click += new EventHandler(lblNum_Click);
                this.Controls.Add(fav);
                (sender as Control).Visible = false;
            }            
            OnPageChanged(_strPage, true, true, _db.IsPoemFaved(verse._PoemID), false, string.Empty, null, null);
            VerticalScroll.Value = y;            
        }
        private int ShowPoem(GanjoorPoem poem, bool keepTrack)
        {
            if (poem == null)
                return 0;//this happened one time!
            return ShowPoem(poem, keepTrack, poem._HighlightText);
        }
        private int ShowPoem(GanjoorPoem poem, bool keepTrack, string highlightWord)
        {
            if (EditMode)
                Save();
            if (keepTrack)
                UpdateHistory();            
            Cursor = Cursors.WaitCursor; Application.DoEvents();
            this.SuspendLayout();
            this.VerticalScroll.Value = 0;this.HorizontalScroll.Value = 0;
            this.Controls.Clear();

            int catsTop = DistanceFromTop;
            int lastDistanceFromRight;
            ShowCategory(_db.GetCategory(poem._CatID), ref catsTop, out lastDistanceFromRight, false);
            lastDistanceFromRight += DistanceFromRightStep;

            _strPage += " -> " + poem._Title;


            LinkLabel lblPoem = new LinkLabel();            
            poem._HighlightText = string.Empty;
            lblPoem.Tag = poem;
            lblPoem.AutoSize = true;
            lblPoem.Text = poem._Title;
            Size szPoemTitleSizeWithWordWrap = TextRenderer.MeasureText(poem._Title, this.Font, new Size(this.Width - lastDistanceFromRight - 20, Int32.MaxValue), TextFormatFlags.WordBreak);
            Size szPoemTitleSizeWithoutWordWrap = TextRenderer.MeasureText(poem._Title, this.Font, new Size(this.Width - lastDistanceFromRight - 20, Int32.MaxValue), TextFormatFlags.Default);
            int ParagraphShift = 0;
            if (szPoemTitleSizeWithWordWrap.Width != szPoemTitleSizeWithoutWordWrap.Width)
            {
                ParagraphShift += (szPoemTitleSizeWithWordWrap.Height - lblPoem.Height);
                lblPoem.AutoSize = false;
                lblPoem.Size = new Size(this.Width - lastDistanceFromRight - 20, szPoemTitleSizeWithWordWrap.Height);
                lblPoem.Location = new Point(lastDistanceFromRight, catsTop);

            }
            else
            if (CenteredView)
                lblPoem.Location = new Point(this.Width/2 - TextRenderer.MeasureText(poem._Title, this.Font).Width/2, catsTop);
            else
                lblPoem.Location = new Point(lastDistanceFromRight, catsTop);
            lblPoem.LinkVisited = true;
            lblPoem.LinkBehavior = LinkBehavior.HoverUnderline;
            lblPoem.BackColor = Color.Transparent;
            lblPoem.Click += new EventHandler(lblPoem_Click);
            lblPoem.VisitedLinkColor = Settings.Default.CurrentLinkColor;
            lblPoem.ForeColor = lblPoem.VisitedLinkColor;
            this.Controls.Add(lblPoem);

            catsTop += DistanceBetweenLines;
            lastDistanceFromRight += DistanceFromRightStep;


            List<GanjoorVerse> verses = _db.GetVerses(poem._ID);

            
            int WholeBeytNum = 0;
            int BeytNum = 0;
            int BandNum = 0;
            int BandBeytNums = 0;
            int WholeNimayeeLines = 0;
            bool MustHave2ndBandBeyt = false;
            int MissedMesras = 0;

            int MesraWidth = 250;
            if (EditMode)
            {
                for (int i = 0; i < verses.Count; i++)
                    if(verses[i]._Position != VersePosition.Paragraph && verses[i]._Position != VersePosition.Single)
                        MesraWidth = Math.Max(MesraWidth, TextRenderer.MeasureText(verses[i]._Text, this.Font).Width);
            }

            
            int versDistanceFromRight = ShowBeytNums ? lastDistanceFromRight + TextRenderer.MeasureText((verses.Count).ToString(), this.Font).Width: lastDistanceFromRight;
            for (int i = 0; i < verses.Count; i++)
            {
                Control lblVerse;
                if (EditMode)
                {
                    lblVerse = new TextBox();
                    lblVerse.Font = this.Font;
                    if (CenteredView)
                    {
                        lblVerse.Size = new Size(MesraWidth, lblVerse.Size.Height);
                        int vTop = catsTop + ((i - MissedMesras) / 2 + BandBeytNums) * DistanceBetweenLines + ParagraphShift;
                        switch (verses[i]._Position)
                        {
                            case VersePosition.Right:
                                lblVerse.Location = new Point(this.Width / 2 - 5 - MesraWidth, vTop);
                                if (MustHave2ndBandBeyt)
                                {
                                    MissedMesras++;
                                    MustHave2ndBandBeyt = false;
                                }
                                break;
                            case VersePosition.Left:
                                lblVerse.Location = new Point(this.Width / 2 + 5, vTop);
                                break;
                            case VersePosition.CenteredVerse1:
                                lblVerse.Location = new Point(this.Width / 2 - MesraWidth / 2, vTop);
                                BandBeytNums++;
                                MustHave2ndBandBeyt = true;
                                break;
                            case VersePosition.CenteredVerse2:
                                MustHave2ndBandBeyt = false;
                                lblVerse.Location = new Point(this.Width / 2 - MesraWidth / 2, vTop);
                                break;
                            case VersePosition.Single:
                            case VersePosition.Paragraph:
                                {
                                    int txtHeight = lblVerse.Height;
                                    (lblVerse as TextBox).Multiline = true;
                                    Size sz = new Size(this.Width - versDistanceFromRight - 20, Int32.MaxValue);
                                    string txtMeasure = verses[i]._Text;
                                    if (string.IsNullOrEmpty(txtMeasure))
                                    {
                                        //احتمالاً پاراگراف جدیدی بدون متن
                                        for (int c = 0; c < 50; c++)
                                            txtMeasure += "گنجور ";
                                    }
                                    sz = TextRenderer.MeasureText(txtMeasure, this.Font, sz, TextFormatFlags.WordBreak);
                                    Size sz2 = TextRenderer.MeasureText("گنجور", this.Font, sz, TextFormatFlags.WordBreak);
                                    lblVerse.Size = new Size(this.Width - versDistanceFromRight - 20, sz.Height / sz2.Height * txtHeight);                                    
                                    ParagraphShift += lblVerse.Height;
                                    lblVerse.Location = new Point(versDistanceFromRight, vTop);
                                    (lblVerse as TextBox).WordWrap = true;
                                    MissedMesras++;
                                }
                                break;
                        }
                    }
                    else
                        lblVerse.Location = new Point(versDistanceFromRight, catsTop + i * DistanceBetweenLines);
                }
                else
                {
                    lblVerse = new HighlightLabel();
                    lblVerse.BackColor = Color.Transparent;
                    lblVerse.AutoSize = true;
                    if (CenteredView)
                    {
                        int vTop = catsTop + ((i - MissedMesras) / 2 + BandBeytNums) * DistanceBetweenLines + ParagraphShift;
                        switch (verses[i]._Position)
                        {
                            case VersePosition.Right:
                                lblVerse.Location = new Point(this.Width / 2 - 5 - TextRenderer.MeasureText(verses[i]._Text, this.Font).Width, vTop);
                                if (MustHave2ndBandBeyt)
                                {
                                    MissedMesras++;
                                    MustHave2ndBandBeyt = false;
                                }
                                break;
                            case VersePosition.Left:
                                lblVerse.Location = new Point(this.Width / 2 + 5, vTop);
                                break;
                            case VersePosition.CenteredVerse1:
                                lblVerse.Location = new Point(this.Width / 2 - TextRenderer.MeasureText(verses[i]._Text, this.Font).Width / 2, vTop);
                                BandBeytNums++;
                                MustHave2ndBandBeyt = true;
                                break;
                            case VersePosition.CenteredVerse2:
                                MustHave2ndBandBeyt = false;
                                lblVerse.Location = new Point(this.Width / 2 - TextRenderer.MeasureText(verses[i]._Text, this.Font).Width / 2, vTop);
                                break;
                            case VersePosition.Single:
                            case VersePosition.Paragraph:
                                (lblVerse as Label).AutoSize = false;
                                {
                                    int labelHeight = lblVerse.Height;
                                    Size sz = new Size(this.Width - versDistanceFromRight - 20, Int32.MaxValue);
                                    sz = TextRenderer.MeasureText(verses[i]._Text, this.Font, sz, TextFormatFlags.WordBreak);
                                    lblVerse.Size = new Size(this.Width - versDistanceFromRight - 20, sz.Height);
                                    ParagraphShift += sz.Height;
                                    lblVerse.Location = new Point(versDistanceFromRight, vTop);
                                    MissedMesras++;
                                }
                                break;

                        }
                    }
                    else
                    {
                        lblVerse.Location = new Point(versDistanceFromRight, catsTop + i * DistanceBetweenLines + ParagraphShift);
                        if (verses[i]._Position == VersePosition.Paragraph || verses[i]._Position == VersePosition.Single)
                        {
                            (lblVerse as Label).AutoSize = false;
                            int labelHeight = lblVerse.Height;
                            Size sz = new Size(this.Width - versDistanceFromRight - 20, Int32.MaxValue);
                            sz = TextRenderer.MeasureText(verses[i]._Text, this.Font, sz, TextFormatFlags.WordBreak);
                            lblVerse.Size = new Size(this.Width - versDistanceFromRight - 20, sz.Height);
                            ParagraphShift += sz.Height;
                        }
                    }

                }

                lblVerse.Tag = verses[i];
                lblVerse.Text = verses[i]._Text;
                if (EditMode)
                {
                    lblVerse.Leave += new EventHandler(lblVerse_Leave);
                    lblVerse.TextChanged += new EventHandler(lblVerse_TextChanged);
                    lblVerse.KeyDown += new KeyEventHandler(lblVerse_KeyDown);
                }
                this.Controls.Add(lblVerse);
                

                if (verses[i]._Position == VersePosition.Right || verses[i]._Position == VersePosition.CenteredVerse1 || (verses[i]._Position == VersePosition.Single))
                {
                    if (!string.IsNullOrEmpty(verses[i]._Text.Trim()))//empty verse strings have been seen sometimes, it seems that we have some errors in our database
                    {
                        if (verses[i]._Position == VersePosition.Single)
                            WholeNimayeeLines++;
                        else
                            WholeBeytNum++;
                        bool isBand = verses[i]._Position == VersePosition.CenteredVerse1;
                        if (isBand)
                        {
                            BeytNum = 0;
                            BandNum++;
                        }
                        else
                            BeytNum++;
                        int xDistance = TextRenderer.MeasureText("345", this.Font).Width;
                        if (ShowBeytNums)
                        {
                            LinkLabel lblNum = new LinkLabel();
                            lblNum.AutoSize = true;
                            lblNum.Text = isBand ? BandNum.ToString() : BeytNum.ToString();
                            lblNum.Tag = verses[i];
                            lblNum.BackColor = Color.Transparent;
                            lblNum.LinkBehavior = LinkBehavior.HoverUnderline;
                            lblNum.Location = new Point(lastDistanceFromRight - xDistance, lblVerse.Location.Y);
                            lblNum.LinkColor = isBand ? Settings.Default.BandLinkColor : Settings.Default.LinkColor;
                            lblNum.ForeColor = lblNum.LinkColor;
                            lblNum.Click += new EventHandler(lblNum_Click);
                            this.Controls.Add(lblNum);
                            if (_db.IsVerseFaved(poem._ID, verses[i]._Order))
                            {
                                PictureBox fav = new PictureBox();
                                fav.BackColor = Color.Transparent;
                                fav.Image = Resources.fav;
                                fav.Size = new Size(16, 16);
                                fav.Location = new Point(lastDistanceFromRight - xDistance, lblVerse.Location.Y);
                                fav.Tag = verses[i];
                                fav.Cursor = Cursors.Hand;
                                fav.Click += new EventHandler(lblNum_Click);
                                this.Controls.Add(fav);
                                lblNum.Visible = false;
                            }
                        }
                        else
                        {
                            if (_db.IsVerseFaved(poem._ID, verses[i]._Order))
                            {
                                PictureBox fav = new PictureBox();
                                fav.BackColor = Color.Transparent;
                                fav.Image = Resources.fav;
                                fav.Size = new Size(16, 16);
                                fav.Location = new Point(lastDistanceFromRight - xDistance, lblVerse.Location.Y);
                                fav.Tag = verses[i];
                                fav.Cursor = Cursors.Hand;
                                fav.Click += new EventHandler(lblNum_Click);
                                this.Controls.Add(fav);
                            }
                        }
                    }
                }                


            }
            //یک بر چسب اضافی برای اضافه شدن فضای پایین فرم
            Label lblDummy = new Label();
            lblDummy.Text = " ";
            int yLocDummy = 
                CenteredView ? 
                    catsTop + ((verses.Count - MissedMesras)/ 2 + BandBeytNums) * DistanceBetweenLines
                    :
                    catsTop + verses.Count * DistanceBetweenLines;
            lblDummy.Location = new Point(200, yLocDummy);
            lblDummy.BackColor = Color.Transparent;
            this.Controls.Add(lblDummy);

            //کلک راست به چپ!
            foreach (Control ctl in this.Controls)
                ctl.Location = new Point(this.Width - ctl.Right, ctl.Location.Y);

            AssignPreviewKeyDownEventToControls();
            this.ResumeLayout();

            Cursor = Cursors.Default;
            _FavsPage = false;
            _iCurPoem = poem._ID;

            _strPage += " (";
            if(WholeBeytNum > 0)
                 _strPage +=  WholeBeytNum.ToString() + " بیت";
            if (WholeNimayeeLines > 0)
            {
                if (WholeBeytNum > 0)
                    _strPage += "، ";
                _strPage += WholeNimayeeLines.ToString() + " خط";
            }
            if (BandNum == 0)
                _strPage += ")";
            else
                _strPage += "، " + BandNum.ToString() + " بند)";
            _strLastPhrase = null;
            if (null != OnPageChanged)
                OnPageChanged(_strPage, true, true, poem._Faved, false, highlightWord, null, null);
            return string.IsNullOrEmpty(highlightWord) || !Settings.Default.HighlightKeyword ? 0 : HighlightText(highlightWord);
        }
        private void lblVerse_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    {
                        if (sender is TextBox)
                        {
                            TextBox txtBox = (sender as TextBox);
                            if (!txtBox.Multiline)
                            {
                                int oldSelStart = txtBox.SelectionStart;
                                GanjoorVerse CurrentVerse = txtBox.Tag as GanjoorVerse;
                                int newFocusTag =
                                    (CurrentVerse._Position == VersePosition.Right) || (CurrentVerse._Position == VersePosition.Left)
                                    ?
                                    CurrentVerse._Order - 2
                                    :
                                    CurrentVerse._Order - 1;

                                ActivateTextBox(oldSelStart, newFocusTag);
                            }
                        }
                    }
                    break;
                case Keys.Down:
                    {
                        if (sender is TextBox)
                        {
                            TextBox txtBox = (sender as TextBox);
                            if (!txtBox.Multiline)
                            {
                                int oldSelStart = txtBox.SelectionStart;
                                GanjoorVerse CurrentVerse = txtBox.Tag as GanjoorVerse;
                                int newFocusTag =
                                    (CurrentVerse._Position == VersePosition.Right) || (CurrentVerse._Position == VersePosition.Left)
                                    ?
                                    CurrentVerse._Order + 2
                                    :
                                    CurrentVerse._Order + 1;

                                ActivateTextBox(oldSelStart, newFocusTag);
                            }
                        }
                    }
                    break;
                case Keys.Enter:
                    {
                        if (sender is TextBox)
                        {
                            TextBox txtBox = (sender as TextBox);
                            if (!txtBox.Multiline)
                            {
                                int oldSelStart = txtBox.SelectionStart;
                                GanjoorVerse CurrentVerse = txtBox.Tag as GanjoorVerse;
                                if (CurrentVerse._Position == VersePosition.Right)
                                    ActivateTextBox(oldSelStart, CurrentVerse._Order + 1);
                                else
                                {
                                    NewLine(CurrentVerse._Position == VersePosition.Single ? VersePosition.Single : VersePosition.Right);
                                }
                            }
                        }
                    }
                    break;
            }
        }
        private void ActivateTextBox(int SelStart, int verseOrder)
        {
            if (verseOrder >= 0)
                foreach (Control ctl in this.Controls)
                    if (ctl is TextBox)
                        if ((ctl.Tag as GanjoorVerse)._Order == verseOrder)
                        {
                            (ctl as TextBox).SelectionStart = SelStart;
                            ctl.Focus();
                            break;
                        }
        }

        #endregion

        #region Fancy Stuff!
        private Color bBegin;
        private Color bEnd;
        private bool GradiantBackground;        
        public void ApplyUISettings()
        {
            this.ForeColor = Settings.Default.TextColor;
            this.BackColor = Settings.Default.BackColor;
            if (!string.IsNullOrEmpty(Settings.Default.BackImagePath))
            {
                if (System.IO.File.Exists(Settings.Default.BackImagePath))
                    try
                    {
                        this.BackgroundImage = new Bitmap(Settings.Default.BackImagePath);
                    }
                    catch
                    {
                    }
            }
            else
                this.BackgroundImage = null;
            GradiantBackground = Settings.Default.GradiantBackground;
            if (GradiantBackground)
                this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            else
            {
                this.SetStyle(ControlStyles.AllPaintingInWmPaint , false);
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            }
            this.bBegin = Settings.Default.GradiantBegin;
            this.bEnd = Settings.Default.GradiantEnd;
            foreach(Control ctl in this.Controls)
                if (ctl is LinkLabel)
                {
                    LinkLabel lbl = (ctl as LinkLabel);
                    if (lbl.Tag is GanjoorVerse && ((lbl.Tag as GanjoorVerse)._Position == VersePosition.CenteredVerse1))
                        lbl.LinkColor = Settings.Default.BandLinkColor;
                    else
                        lbl.LinkColor = Settings.Default.LinkColor;
                    lbl.VisitedLinkColor = Settings.Default.CurrentLinkColor;
                    lbl.ForeColor = lbl.LinkVisited ? lbl.VisitedLinkColor : lbl.LinkColor;
                }
                else
                    if (ctl is HighlightLabel)
                    {
                        (ctl as HighlightLabel).HighlightColor = Settings.Default.HighlightColor;
                    }
            this.ScrollingSpeed = Math.Max(1, Settings.Default.ScrollingSpeed);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (GradiantBackground)
            {
                using (LinearGradientBrush brsh = new LinearGradientBrush(this.Bounds, bBegin, bEnd, 0.0f))
                {
                    e.Graphics.FillRectangle(brsh, e.ClipRectangle);
                }
            }
            else
                base.OnPaint(e);
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (this.BackgroundImage != null && this.Width > 0 && this.Height>0)
            {
                e.Graphics.DrawImageUnscaled(this.BackgroundImage, new Rectangle(0,0,this.Width, this.Height));
            }   
            else
                base.OnPaintBackground(e);

        }
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                AdjustLocationsByFont();

            }
        }
        private void AdjustLocationsByFont()
        {
            DistanceBetweenLines = TextRenderer.MeasureText("ا", this.Font).Width * 2;
            if (_db != null)
            {
                if (_FavsPage)
                {
                    ShowFavs(_iCurSearchStart, _iCurSearchPageCount, false);
                }
                else
                    if (_iCurCat != 0)
                    {
                        if (_iCurPoem != 0)
                        {
                            ShowPoem(_db.GetPoem(_iCurPoem), false);
                        }
                        else
                        {
                            GanjoorCat cat = _db.GetCategory(_iCurCat);
                            cat._StartPoem = _iCurCatStart;
                            ShowCategory(cat, false);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_strLastPhrase))
                        {
                            ShowSearchResults(_strLastPhrase, _iCurSearchStart, Settings.Default.SearchPageItems, Settings.Default.LastSearchPoetID, false);
                        }
                        else
                            ShowHome(false);
                    }
            }
        }
        protected override void OnScroll(ScrollEventArgs se)
        {
            if (this.BackgroundImage != null)
                this.Invalidate();
            base.OnScroll(se);
        }
        #endregion

        #region Public Methods
        public void ShowHome(bool keepTrack)
        {
            ShowCategory(new GanjoorCat(0, 0, "", 0, ""), keepTrack);
        }
        public bool NextPoem()
        {
            if (_db.Connected)
            {
                GanjoorPoem poem = _db.GetNextPoem(_iCurPoem, _iCurCat);
                if (null != poem)
                {
                    ShowPoem(poem, true);
                    return true;
                }
            }
            return false;
        }
        public bool PreviousPoem()
        {
            if (_db.Connected)
            {
                GanjoorPoem poem = _db.GetPreviousPoem(_iCurPoem, _iCurCat);
                if (null != poem)
                {
                    ShowPoem(poem, true);
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Public Events
        public event PageChangedEvent OnPageChanged = null;
        #endregion

        #region Public Properties
        public bool CanGoToNextPoem
        {
            get
            {
                if (_db.Connected)
                {
                    return null != _db.GetNextPoem(_iCurPoem, _iCurCat);
                }
                return false;
            }
        }
        public bool CanGoToPreviousPoem
        {
            get
            {
                if (_db.Connected)
                {
                    return null != _db.GetPreviousPoem(_iCurPoem, _iCurCat);
                }
                return false;
            }
        }
        public string CurrentPoet
        {
            get
            {
                if (_db.Connected)
                {
                    GanjoorPoet poet = _db.GetPoetForCat(_iCurCat);
                    if (null != poet)
                        return poet._Name;
                }
                return string.Empty;
            }
        }
        public string CurrentPoetBio
        {
            get
            {
                if (_db.Connected)
                {
                    GanjoorPoet poet = _db.GetPoetForCat(_iCurCat);
                    if (null != poet)
                        return poet._Bio;
                }
                return string.Empty;
            }
        }

        public string CurrentCategory
        {
            get
            {
                if (_db.Connected)
                {
                    GanjoorCat cat = _db.GetCategory(_iCurCat);
                    if (null != cat)
                        return cat._Text;
                }
                return string.Empty;
            }
        }
        public string CurrentPoem
        {
            get
            {
                if (_db.Connected)
                {
                    GanjoorPoem poem = _db.GetPoem(_iCurPoem);
                    if (null != poem)
                        return poem._Title;
                }
                return string.Empty;
            }
        }
        public string CurrentPageGanjoorUrl
        {
            get
            {
                if(0 == _iCurCat)
                    return "http://ganjoor.net";
                if (0 == _iCurPoem)
                    return _db.GetCategory(_iCurCat)._Url;
                return _db.GetPoem(_iCurPoem)._Url;
                /*
                 * using following code you can delete url field in poem table,
                 * 
                 * return "http://ganjoor.net/?p=" + _iCurPoem.ToString();
                 * 
                 * it causes ganjoor.net to perform a redirect to SEO frinedly url,
                 * however size of database is reduced only by 2 mb (for 69 mb one) in this way                    
                 * so I thought it might not condisered harmful to keep current structure.
                 */
            }
        }
        public string CurrentPoemCommentsUrl
        {
            get
            {
                if(0 == _iCurPoem)
                    return "http://ganjoor.net";
                return "http://ganjoor.net/?comments_popup=" + _iCurPoem.ToString();
            }
        }
        [DefaultValue(false)]
        public bool ShowBeytNums { set; get; }
        [DefaultValue(false)]
        public bool CenteredView { set; get; }
        public string LastError
        {
            get
            {
                return _db.LastError;
            }
        }
        #endregion

        #region Browing History
        private Stack<GarnjoorBrowsingHistory> _history;
        public bool CanGoBackInHistory
        {
            get
            {
                return _history.Count > 0;
            }
        }
        private void UpdateHistory()
        {
            if (_FavsPage)
            {
                _history.Push(new GarnjoorBrowsingHistory(string.Empty, 0, _iCurSearchStart, _iCurSearchPageCount, true, this.AutoScrollPosition));
            }
            else
            if (!string.IsNullOrEmpty(_strLastPhrase))
            {
                _history.Push(new GarnjoorBrowsingHistory(_strLastPhrase, _iCurSearchPoet, _iCurSearchStart, _iCurSearchPageCount, false, this.AutoScrollPosition));
            }
            else
                if (
                    (_history.Count == 0) || !((_history.Peek()._CatID == _iCurCat) && (_history.Peek()._CatPageStart == _iCurCatStart) && ((_history.Peek()._PoemID == _iCurPoem)))
                    )
                {
                    _history.Push(new GarnjoorBrowsingHistory(_iCurCat, _iCurPoem, _iCurCatStart, this.AutoScrollPosition));

                }
        }
        public void GoBackInHistory()//forward twards back!
        {
            if (CanGoBackInHistory)
            {
                GarnjoorBrowsingHistory back = _history.Pop();
                if (back._FavsPage)
                {
                    ShowFavs(back._SearchStart, back._PageItemsCount, false);
                }
                else
                if (0 == back._PoemID)
                {
                    if (0 == back._CatID)
                    {
                        if (0 == back._PageItemsCount)
                        {
                            ShowHome(false);
                        }
                        else
                        {
                            ShowSearchResults(back._SearchPhrase, back._SearchStart, back._PageItemsCount, back._PoetID, false);
                        }
                    }
                    else
                    {
                        GanjoorCat cat = _db.GetCategory(back._CatID);
                        cat._StartPoem = back._CatPageStart;
                        ShowCategory(cat, false);
                    }
                }
                else
                    ShowPoem(_db.GetPoem(back._PoemID), false);
                this.AutoScrollPosition = new Point(-back._AutoScrollPosition.X, -back._AutoScrollPosition.Y);
            }
        }
        #endregion

        #region Printing
        public void Print(PrintDocument Document)
        {
            Document.PrintPage += new PrintPageEventHandler(Document_PrintPage);
            _iRemainingUnprintedLines = 0;
            Document.Print();
        }
        public PrintDocument PrepareForPrintPreview()
        {
            _iRemainingUnprintedLines = 0;
            PrintDocument Document= new PrintDocument();
            Document.PrintPage += new PrintPageEventHandler(Document_PrintPage);
            return Document;
        }
        private void Document_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            _iRemainingUnprintedLines = this.PrintPoem(e, _iRemainingUnprintedLines);
            e.HasMorePages = (0 != _iRemainingUnprintedLines);

        }
        private int _iRemainingUnprintedLines = 0;
        private int PrintPoem(PrintPageEventArgs e, int StartFrom)
        {
            if (0 != _iCurPoem)
            {
                int dist = (0 == StartFrom) ? (int)(2.5f*DistanceBetweenLines) : DistanceBetweenLines + (int)(0.25f*DistanceBetweenLines);
                int top = e.PageBounds.Top + 100;
                int mid = e.PageBounds.Left + e.PageBounds.Width / 2;
                StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                int line = -1;
                foreach (Control ctl in this.Controls)
                    if (ctl is Label)
                        if (!(ctl is LinkLabel) || ctl.Tag is GanjoorPoem)
                        {
                            line++;
                            if (line >= StartFrom)
                            {
                                e.Graphics.DrawString(ctl.Text, this.Font, Brushes.Black,
                                    new PointF(
                                        mid + TextRenderer.MeasureText(ctl.Text, this.Font).Width / 2,
                                        top),
                                        format);
                                top += dist;
                                if (top > e.PageBounds.Bottom - dist)
                                    return line + 1;
                                dist = DistanceBetweenLines + (int)(0.25f * DistanceBetweenLines);//25;
                            }
                        }

            }
            return 0;
        }
        #endregion

        #region Search
        public void ShowSearchResults(string phrase, int PageStart, int Count,int PoetID)
        {
            ShowSearchResults(phrase, PageStart, Count, PoetID, true);
        }
        public void ShowSearchResults(string phrase, int PageStart, int Count, int PoetID, bool keepTrack)
        {
            phrase = GPersianTextSync.Sync(phrase);
            if (keepTrack)
                UpdateHistory();
            Cursor = Cursors.WaitCursor; Application.DoEvents();
            this.SuspendLayout();
            this.VerticalScroll.Value = 0;this.HorizontalScroll.Value = 0;
            this.Controls.Clear();
            GanjoorSearchPage prePage=null, nextPage = null;
            using(DataTable poemsList = _db.FindPoemsContaingPhrase(phrase, PageStart, Count+1, PoetID))
            {
                int CountCopy = Count;
                bool HasMore = Count>0 && poemsList.Rows.Count == Count+1;
                if (poemsList.Rows.Count <= Count)
                    Count = poemsList.Rows.Count;
                else
                    Count = HasMore ? Count : poemsList.Rows.Count - 1;
                int catsTop = DistanceFromTop;
                for (int i = 0; i < Count; i++)
                {
                    GanjoorPoem poem = _db.GetPoem(Convert.ToInt32(poemsList.Rows[i].ItemArray[0]));
                    poem._HighlightText = phrase;
                    int lastDistanceFromRight;
                    ShowCategory(_db.GetCategory(poem._CatID), ref catsTop, out lastDistanceFromRight, false);
                    lastDistanceFromRight += DistanceFromRightStep;
                    LinkLabel lblPoem = new LinkLabel();                    
                    lblPoem.Tag = poem;
                    lblPoem.AutoSize = true;
                    lblPoem.Text = poem._Title;
                    lblPoem.Location = new Point(lastDistanceFromRight, catsTop);
                    lblPoem.LinkBehavior = LinkBehavior.HoverUnderline;
                    lblPoem.BackColor = Color.Transparent;
                    lblPoem.LinkColor = Settings.Default.LinkColor;
                    lblPoem.ForeColor = lblPoem.LinkColor;
                    lblPoem.Click += new EventHandler(lblPoem_Click);
                    this.Controls.Add(lblPoem);

                    catsTop += DistanceBetweenLines;
                    lastDistanceFromRight += DistanceFromRightStep;

                    

                    using (DataTable firsVerse = _db.FindFirstVerseContaingPhrase(poem._ID, phrase))
                    {
                        System.Diagnostics.Debug.Assert(firsVerse.Rows.Count == 1);


                        HighlightLabel lblVerse = new HighlightLabel(phrase, Settings.Default.HighlightColor);
                        lblVerse.AutoSize = true;
                        lblVerse.Tag = null;
                        lblVerse.Text = firsVerse.Rows[0].ItemArray[0].ToString();
                        lblVerse.Location = new Point(lastDistanceFromRight, catsTop);
                        lblVerse.BackColor = Color.Transparent;
                        this.Controls.Add(lblVerse);
                        catsTop += 2*DistanceBetweenLines;
                    }
                }

                if (PageStart > 0)
                {
                    LinkLabel lblPrevPage = new LinkLabel();                    
                    prePage = new GanjoorSearchPage(phrase, PageStart - CountCopy, CountCopy, PoetID);
                    lblPrevPage.Tag = prePage;
                    lblPrevPage.AutoSize = true;
                    lblPrevPage.Text = "صفحۀ قبل";
                    lblPrevPage.Location = new Point(200, catsTop);
                    lblPrevPage.LinkBehavior = LinkBehavior.HoverUnderline;
                    lblPrevPage.BackColor = Color.Transparent;
                    lblPrevPage.LinkColor = Settings.Default.LinkColor;
                    lblPrevPage.ForeColor = lblPrevPage.LinkColor;
                    lblPrevPage.Click += new EventHandler(lblNextPage_Click);
                    this.Controls.Add(lblPrevPage);

                }

                if (HasMore)
                {
                    LinkLabel lblNextPage = new LinkLabel();                    
                    nextPage = new GanjoorSearchPage(phrase, PageStart + Count, Count, PoetID);
                    lblNextPage.Tag = nextPage;
                    lblNextPage.AutoSize = true;
                    lblNextPage.Text = "صفحۀ بعد";
                    lblNextPage.Location = new Point(this.Width - 200, catsTop);
                    lblNextPage.LinkBehavior = LinkBehavior.HoverUnderline;
                    lblNextPage.BackColor = Color.Transparent;
                    lblNextPage.LinkColor = Settings.Default.LinkColor;
                    lblNextPage.ForeColor = lblNextPage.LinkColor;
                    lblNextPage.Click += new EventHandler(lblNextPage_Click);
                    this.Controls.Add(lblNextPage);
                    catsTop += DistanceBetweenLines;
                }


                //یک بر چسب اضافی برای اضافه شدن فضای پایین فرم
                Label lblDummy = new Label();
                lblDummy.Text = " ";
                lblDummy.Location = new Point(200, catsTop);
                lblDummy.BackColor = Color.Transparent;
                this.Controls.Add(lblDummy);

                _FavsPage = false;
                _iCurCat = 0;
                _iCurSearchStart = PageStart;
                _iCurSearchPageCount = Count;
                _strLastPhrase = phrase;
                _iCurSearchPoet = PoetID;


            }


            //کلک راست به چپ!
            foreach (Control ctl in this.Controls)
                ctl.Location = new Point(this.Width - ctl.Right, ctl.Location.Y);

            AssignPreviewKeyDownEventToControls();
            this.ResumeLayout();
            Cursor = Cursors.Default;
            _iCurPoem = 0;


            _strPage = "نتایج جستجو برای \"" + phrase + "\" در آثار " + _db.GetPoet(PoetID)._Name + " صفحۀ " + (Count < 1 ? "1 (موردی یافت نشد.)" : (1 + PageStart / Count).ToString() + " (مورد " + (PageStart + 1).ToString() + " تا " + (PageStart + Count).ToString() + ")");
            if (null != OnPageChanged)
                OnPageChanged(_strPage, false, false, false, false, string.Empty, prePage, nextPage);

            if (Count < 1)
                MessageBox.Show("موردی یافت نشد.", "جستجو", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);


        }
        public const string OnlyScrollString = "$545#4*77";
        public int HighlightText(string phrase)
        {
            return HighlightText(phrase, 0);
        }
        public int HighlightText(string phrase, int scrollindex)
        {  
            if (_iCurCat != 0)
            {
                if (_iCurPoem != 0)
                {
                    bool OnlyScroll = OnlyScrollString == phrase;
                    int count = 0;
                    bool scrolled = false;
                    foreach (Control ctl in this.Controls)
                    {
                        if (ctl is HighlightLabel)
                        {
                            if (OnlyScroll)
                            {
                                if ((ctl.Tag is GanjoorVerse) && _db.IsVerseFaved((ctl.Tag as GanjoorVerse)._PoemID, (ctl.Tag as GanjoorVerse)._Order))
                                {
                                    this.AutoScrollPosition = ctl.Location;
                                    return 0;
                                }
                            }
                            else
                            {
                                (ctl as HighlightLabel).Keyword = phrase;
                                if (!string.IsNullOrEmpty(phrase))//this is here to prevent a bug caused by bad data (empty verses)
                                {
                                    int index = ctl.Text.IndexOf(phrase);
                                    if (index != -1)
                                    {
                                        if (!scrolled && (scrollindex == count))
                                        {
                                            this.AutoScrollPosition = new Point(-this.AutoScrollPosition.X + ctl.Left, -this.AutoScrollPosition.Y + ctl.Top);
                                            scrolled = true;
                                        }
                                        count++;
                                        while ((index + 1 != ctl.Text.Length) && (index = ctl.Text.IndexOf(phrase, index + 1)) != -1)
                                        {
                                            count++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (count == 0)
                        this.AutoScrollPosition = new Point();
                    this.Invalidate();

                    return count;
                }
            }

            if (_iCurCat == 0 || _iCurPoem == 0)
            {
                int count = 0;
                bool scrolled = false;
                    foreach (Control ctl in this.Controls)
                        if (ctl is LinkLabel)
                        {
                            if (!string.IsNullOrEmpty(phrase))
                            {
                                int index = ctl.Text.IndexOf(phrase);
                                if (index != -1)
                                {
                                    (ctl as LinkLabel).LinkColor = Settings.Default.HighlightColor;
                                    if (!scrolled && (scrollindex == count))
                                    {
                                        this.AutoScrollPosition = new Point(-this.AutoScrollPosition.X + ctl.Left, -this.AutoScrollPosition.Y + ctl.Top);
                                        scrolled = true;
                                    }
                                    count++;
                                    while ((index + 1 != ctl.Text.Length) && (index = ctl.Text.IndexOf(phrase, index + 1)) != -1)
                                    {
                                        count++;
                                    }
                                }
                                else
                                    (ctl as LinkLabel).LinkColor = Settings.Default.LinkColor;
                            }
                            else
                                (ctl as LinkLabel).LinkColor = Settings.Default.LinkColor;

                        }
                return count;
            }
            return 0;
        }
        void lblNextPage_Click(object sender, EventArgs e)
        {
            ProcessPagingTag((sender as LinkLabel).Tag);
        }
        #endregion

        #region Simple Copy
        public void CopyText()
        {
            string txt = "";
            foreach (Control ctl in this.Controls)
                if (ctl is Label)
                {
                    if (ctl is LinkLabel && ctl.Tag is GanjoorVerse)
                        continue;//bypass beyt nums
                    txt += ctl.Text + "\r\n";
                }
            Clipboard.SetText(txt);
        }
        public void StoreSettings()
        {
            Settings.Default.LastCat = _iCurCat;
            Settings.Default.LastCatStart = _iCurCatStart;
            Settings.Default.LastPoem = _iCurPoem;
            Settings.Default.LastSearchPhrase = _strLastPhrase;
            Settings.Default.LastSeachStart = _iCurSearchStart;
            Settings.Default.WasShowingFavs = _FavsPage;
        }
        #endregion

        #region Fav/UnFave
        public bool ToggleFav()
        {
            if (!_db.ToggleFav(_iCurPoem, -1))
            {
                int i = 0;
                while (i < this.Controls.Count)
                {
                    if (Controls[i] is PictureBox)
                        Controls.RemoveAt(i);
                    else
                    {
                        Controls[i].Visible = true;
                        i++;
                    }
                }
                return false;
            }
            return true;
        }
        public void ShowFavs(int PageStart, int Count)
        {
            ShowFavs(PageStart, Count, true);
        }
        public bool ShowFavs(int PageStart, int Count, bool keepTrack)
        {
            if (_FavsPage && _iCurSearchStart == PageStart)
                return false;
            if (keepTrack)
                UpdateHistory();
            Cursor = Cursors.WaitCursor; Application.DoEvents();
            this.SuspendLayout();
            this.VerticalScroll.Value = 0;this.HorizontalScroll.Value = 0;
            this.Controls.Clear();
            int CountCopy = Count;
            GanjoorFavPage prePage = null, nextPage = null;
            using (DataTable poemsList = _db.GetFavs(PageStart, Count + 1))
            {
                bool HasMore = Count > 0 && poemsList.Rows.Count == Count + 1;
                if (poemsList.Rows.Count <= Count)
                    Count = poemsList.Rows.Count;
                else
                    Count = HasMore ? Count : poemsList.Rows.Count - 1;
                int catsTop = DistanceFromTop;
                for (int i = 0; i < Count; i++)
                {
                    GanjoorPoem poem = _db.GetPoem(Convert.ToInt32(poemsList.Rows[i].ItemArray[0]));
                    if(Settings.Default.ScrollToFavedVerse)
                        poem._HighlightText = OnlyScrollString;
                    int lastDistanceFromRight;
                    ShowCategory(_db.GetCategory(poem._CatID), ref catsTop, out lastDistanceFromRight, false);
                    lastDistanceFromRight += DistanceFromRightStep;
                    LinkLabel lblPoem = new LinkLabel();                    
                    lblPoem.Tag = poem;
                    lblPoem.AutoSize = true;
                    lblPoem.Text = poem._Title;
                    lblPoem.Location = new Point(lastDistanceFromRight, catsTop);
                    lblPoem.LinkBehavior = LinkBehavior.HoverUnderline;
                    lblPoem.BackColor = Color.Transparent;
                    lblPoem.LinkColor = Settings.Default.LinkColor;
                    lblPoem.ForeColor = lblPoem.LinkColor;
                    lblPoem.Click += new EventHandler(lblPoem_Click);
                    this.Controls.Add(lblPoem);

                    catsTop += DistanceBetweenLines;
                    lastDistanceFromRight += DistanceFromRightStep;                    

                    HighlightLabel lblVerse = new HighlightLabel();
                    lblVerse.AutoSize = true;
                    lblVerse.Tag = null;
                    lblVerse.Text = _db.GetPreferablyAFavVerse(poem._ID)._Text;
                    lblVerse.Location = new Point(lastDistanceFromRight, catsTop);
                    lblVerse.BackColor = Color.Transparent;
                    this.Controls.Add(lblVerse);
                    catsTop += 2 * DistanceBetweenLines;

                }

                if (PageStart > 0)
                {
                    LinkLabel lblPrevPage = new LinkLabel();                    
                    prePage = new GanjoorFavPage(PageStart - CountCopy, CountCopy);
                    lblPrevPage.Tag = prePage;
                    lblPrevPage.AutoSize = true;
                    lblPrevPage.Text = "صفحۀ قبل";
                    lblPrevPage.Location = new Point(200, catsTop);
                    lblPrevPage.LinkBehavior = LinkBehavior.HoverUnderline;
                    lblPrevPage.BackColor = Color.Transparent;
                    lblPrevPage.LinkColor = Settings.Default.LinkColor;
                    lblPrevPage.ForeColor = lblPrevPage.LinkColor;
                    lblPrevPage.Click += new EventHandler(lblNextPage_Click);
                    this.Controls.Add(lblPrevPage);

                }

                if (HasMore)
                {
                    LinkLabel lblNextPage = new LinkLabel();                    
                    nextPage = new GanjoorFavPage(PageStart + Count, Count);
                    lblNextPage.Tag = nextPage;
                    lblNextPage.AutoSize = true;
                    lblNextPage.Text = "صفحۀ بعد";
                    lblNextPage.Location = new Point(this.Width - 200, catsTop);
                    lblNextPage.LinkBehavior = LinkBehavior.HoverUnderline;
                    lblNextPage.BackColor = Color.Transparent;
                    lblNextPage.LinkColor = Settings.Default.LinkColor;
                    lblNextPage.ForeColor = lblNextPage.LinkColor;
                    lblNextPage.Click += new EventHandler(lblNextPage_Click);
                    this.Controls.Add(lblNextPage);
                    catsTop += DistanceBetweenLines;
                }


                //یک بر چسب اضافی برای اضافه شدن فضای پایین فرم
                Label lblDummy = new Label();
                lblDummy.Text = " ";
                lblDummy.Location = new Point(200, catsTop);
                lblDummy.BackColor = Color.Transparent;
                this.Controls.Add(lblDummy);

                _iCurCat = 0;
                _iCurSearchStart = PageStart;
                _iCurSearchPageCount = Count;
                _FavsPage = true;
                _strLastPhrase = string.Empty;


            }


            //کلک راست به چپ!
            foreach (Control ctl in this.Controls)
                ctl.Location = new Point(this.Width - ctl.Right, ctl.Location.Y);

            AssignPreviewKeyDownEventToControls();
            this.ResumeLayout();
            Cursor = Cursors.Default;


            _strPage = "نشانه‌ها - صفحۀ " + (Count < 1 ? "1 (موردی یافت نشد.)" : (1 + PageStart / Count).ToString() + " (مورد " + (PageStart + 1).ToString() + " تا " + (PageStart + Count).ToString() + ")");
            if (null != OnPageChanged)
                OnPageChanged(_strPage, false, false, false, true, string.Empty, prePage, nextPage);

            if (Count < 1)
                MessageBox.Show("موردی یافت نشد.", "نشانه‌ها", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            return true;
        }
        #endregion

        #region Paging
        public void ProcessPagingTag(object tag)
        {
            if (tag is GanjoorSearchPage)
            {
                GanjoorSearchPage g = (tag as GanjoorSearchPage);
                ShowSearchResults(g._SearchPhrase, g._PageStart, g._MaxItemsCount, g._PoetID);
            }
            else
                if (tag is GanjoorFavPage)
                {
                    GanjoorFavPage g = tag as GanjoorFavPage;
                    ShowFavs(g._PageStart, g._MaxItemsCount);
                }
                else            
                    if (tag is GanjoorCat)
                    {
                        GanjoorCat g = tag as GanjoorCat;
                        ShowCategory(g, true);
                    }
        }
        #endregion

        #region Toggle Beyt Nums
        public void SetShowBeytNums(bool newValue)
        {
            if (_iCurPoem != 0)
            {
                ShowBeytNums = newValue;
                ShowPoem(_db.GetPoem(_iCurPoem), false);
            }
        }
        #endregion

        #region Poets Info
        public string[] Poets
        {
            get
            {
                List<string> lstPoets = new List<string>();
                lstPoets.Add("همه");
                foreach (GanjoorPoet poet in _db.Poets)
                    lstPoets.Add(poet._Name);
                return lstPoets.ToArray();
            }
        }
        public int GetPoetOrder(int ID)
        {            
            List<GanjoorPoet> lstPoets = _db.Poets;
            for (int i = 0; i < lstPoets.Count; i++)
                if (ID == lstPoets[i]._ID)
                    return i+1;
            return 0;

        }
        public int GetPoetID(int Order)
        {
            if (Order == 0) return 0;
            return _db.Poets[Order - 1]._ID;
        }
        public int GetCurrentPoetID()
        {
            GanjoorCat cat = _db.GetCategory(_iCurCat);
            if (cat != null)
                return cat._PoetID;
            return 0;
        }
        #endregion

        #region Random Poem
        public void ShowRandomPoem()
        {
            if (_LastRandomCatID != Settings.Default.RandomCatID)
            {
                _LastRandomCatID = Settings.Default.RandomCatID;
                _LastRandomCatList = new List<int>();
                if (_LastRandomCatID != 0)
                {
                    if (_db.HasAnyPoem(_LastRandomCatID))
                        _LastRandomCatList.Add(_LastRandomCatID);
                    foreach (int CatID in _db.GetAllSubCats(_LastRandomCatID))
                    {
                        if (_db.HasAnyPoem(CatID))
                            _LastRandomCatList.Add(CatID);
                    }
                }
            }
            int PoemID = _db.GetRandomPoem(_LastRandomCatList);
            if (PoemID == -1)
            {
                MessageBox.Show("خطا در یافتن شعر تصادفی!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                return;
            }
            GanjoorPoem poem = _db.GetPoem(PoemID);
            if (poem != null)
            {
                if (_LastRandomCatList.Count==0 || _LastRandomCatList.IndexOf(poem._CatID) != -1)
                    ShowPoem(poem, true);
                else
                    ShowRandomPoem();//this might happen, because _db.GetRandomPoem returns a poem between MIN-MAX poem id of cat and this might be something which is not from CatIDs we wanted 
            }
            else
                ShowRandomPoem();//not any random id exists, so repeat until finding a valid id
        }
        private int _LastRandomCatID = 0;
        private static List<int> _LastRandomCatList = new List<int>();
        #endregion

        #region Import Db / Import+Export Favs
        public void ImportDb(string fileName)
        {
            GanjoorPoet[] cnflts = _db.GetConflictingPoets(fileName);
            if (cnflts.Length > 0)
            {
                using (ConflictingPoets dlg = new ConflictingPoets(cnflts))
                {
                    if (dlg.ShowDialog(this.Parent) == DialogResult.Cancel)
                        return;
                    cnflts = dlg.DeleteList;
                    foreach (GanjoorPoet delPoet in cnflts)
                        _db.DeletePoet(delPoet._ID);
                }
            }
            if (_db.ImportDb(fileName))
                ShowHome(true);
            else
                MessageBox.Show(_db.LastError);
        }
        public void ImportDbUnsafe(string fileName)
        {
            if (_db.ImportDbFastUnsafe(fileName))
                ShowHome(true);
            else
                MessageBox.Show(_db.LastError);
        }
        public void ExportFavs(string fileName)
        {
            _db.ExportFavs(fileName);
        }
        public void ImportMixFavs(string fileName)
        {
            int ignoredFavs;
            int importedFavs = _db.ImportMixFavs(fileName, out ignoredFavs);
            MessageBox.Show(String.Format("{0} نشانه اضافه شد و از اضافه کردن {1} به دلیل تکراری بودن صرف نظر شد.", importedFavs, ignoredFavs), "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            ShowFavs(0, Settings.Default.FavItemsInPage);
        }
        #endregion

        #region Scroll Using Arrow Keys
        private void GanjoorViewer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control || e.Alt)
                return;
            bool isInputKey = true;
            switch (e.KeyCode)
            {
                case Keys.Down:
                    SetVerticalScrollValue(VerticalScroll.Value + ScrollingSpeed * VerticalScroll.SmallChange);
                    break;
                case Keys.Up:
                    SetVerticalScrollValue(VerticalScroll.Value - ScrollingSpeed * VerticalScroll.SmallChange);
                    break;                    
                case Keys.PageDown:
                    SetVerticalScrollValue(VerticalScroll.Value + VerticalScroll.LargeChange);
                    break;
                case Keys.PageUp:
                    SetVerticalScrollValue(VerticalScroll.Value - VerticalScroll.LargeChange);
                    break;                
                case Keys.Right:
                    SetHorizontalScrollValue(HorizontalScroll.Value + ScrollingSpeed * HorizontalScroll.SmallChange);
                    break;
                case Keys.Left:
                    SetHorizontalScrollValue(HorizontalScroll.Value - ScrollingSpeed * HorizontalScroll.SmallChange);
                    break;
                default:
                    isInputKey = false;
                    break;
            }
            if (isInputKey)
                e.IsInputKey = true;
        }
        private int ScrollingSpeed = 1;
        private void SetVerticalScrollValue(int newValue)
        {
            newValue = Math.Max(this.VerticalScroll.Minimum, Math.Min(newValue, this.VerticalScroll.Maximum));
            int oldValue = this.VerticalScroll.Value;
            int i = 0;
            while (this.VerticalScroll.Value == oldValue && i++ < 3)//!
            {
                this.VerticalScroll.Value = newValue;
            }
        }
        private void SetHorizontalScrollValue(int newValue)
        {
            newValue = Math.Max(this.HorizontalScroll.Minimum, Math.Min(newValue, this.HorizontalScroll.Maximum));
            int oldValue = this.HorizontalScroll.Value;
            int i = 0;
            while (this.HorizontalScroll.Value == oldValue && i++ < 3)//!
            {
                this.HorizontalScroll.Value = newValue;
            }
        }
        private void AssignPreviewKeyDownEventToControls()
        {
            foreach (Control ctl in this.Controls)
                ctl.PreviewKeyDown += GanjoorViewer_PreviewKeyDown;
        }
        #endregion

        #region Edit Mode
        [DefaultValue(false)]
        public bool EditMode { get; set; }
        public bool IsInPoetRootPage
        {
            get
            {
                if (_iCurCat > 0)
                {
                    GanjoorCat cat = _db.GetCategory(_iCurCat);
                    return cat._ParentID == 0;
                }
                else
                    return true;
            }
        }
        public bool NewPoet(string PoetName)
        {
            if (_db != null)
            {
                int poetID = _db.NewPoet(PoetName);
                if (-1 != poetID)
                {
                    ShowCategory(_db.GetCategory(_db.GetPoet(poetID)._CatID), true);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        public bool EditPoet(string NewName)
        {
            GanjoorPoet poet = _db.GetPoetForCat(_iCurCat);
            if (null == poet)
                return false;
            if (_db.SetPoetName(poet._ID, NewName))
            {
                ShowCategory(_db.GetCategory(_iCurCat), false);
                return true;
            }
            return false;
        }
        public bool EditPoetBio(string NewBio)
        {
            GanjoorPoet poet = _db.GetPoetForCat(_iCurCat);
            if (null == poet)
                return false;
            if (_db.ModifyPoetBio(poet._ID, NewBio))
            {
                ShowCategory(_db.GetCategory(_iCurCat), false);
                return true;
            }
            return false;
        }
        public bool NewCat(string CatName)
        {
            GanjoorCat cat = _db.GetCategory(_iCurCat);
            if (null != cat)
            {
                cat = _db.CreateNewCategory(CatName, cat._ID, cat._PoetID);
                if (cat != null)
                {
                    ShowCategory(cat, true);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        public bool EditCat(string NewName)
        {
            if (_db.SetCatTitle(_iCurCat, NewName))
            {
                ShowCategory(_db.GetCategory(_iCurCat), false);
                return true;
            }
            return false;
        }
        public bool NewPoem(string PoemName)
        {
            GanjoorCat cat = _db.GetCategory(_iCurCat);
            if (null != cat)
            {
                 GanjoorPoem poem = _db.CreateNewPoem(PoemName, _iCurCat);
                 if (poem != null)
                {
                    ShowPoem(poem, true);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        public bool EditPoem(string NewTitle)
        {
            if (_db.SetPoemTitle(_iCurPoem, NewTitle))
            {
                ShowPoem(_db.GetPoem(_iCurPoem), false);
                return true;
            }
            return false;
        }
        public bool DeletePoem()
        {
            if (_db.DeletePoem(_iCurPoem))
            {
                ShowCategory(_db.GetCategory(_iCurCat), true);
                return true;
            }
            return false;
        }
        public bool NewNormalLine()
        {
            return NewLine(VersePosition.Right);
        }
        public bool NewBandLine()
        {
            return NewLine(VersePosition.CenteredVerse2);
        }
        public bool NewBandVerse()
        {
            return NewLine(VersePosition.CenteredVerse1);
        }
        public bool NewSingleVerse()
        {
            return NewLine(VersePosition.Single);
        }
        public bool NewParagraph()
        {
            return NewLine(VersePosition.Paragraph);
        }
        public bool InsertVerses(string[] verses, bool IsClassicPoem, bool IgnoreEmptyLines, bool IgnoreShortLines, int minLength)
        {
            Save();
            int LinePosition = GetCurrentLine();
            VersePosition Position = IsClassicPoem ? VersePosition.Right : VersePosition.Single;
            _db.BeginBatchOperation();
            foreach (string verse in verses)
            {
                if (IgnoreEmptyLines && string.IsNullOrEmpty(verse.Trim()))
                    continue;
                if (IgnoreShortLines && verse.Trim().Length < minLength)
                    continue;
                GanjoorVerse newVerse;
                switch (Position)
                {
                    case VersePosition.Single:
                        {
                            newVerse = _db.CreateNewVerse(_iCurPoem, LinePosition, VersePosition.Single);
                            if (newVerse == null)
                                return false;                            
                        }
                        break;
                    case VersePosition.Right:
                        {
                            newVerse = _db.CreateNewVerse(_iCurPoem, LinePosition, VersePosition.Right);
                            if (newVerse == null)
                                return false;
                            Position = VersePosition.Left;
                        }
                        break;
                    default:
                        {
                            newVerse = _db.CreateNewVerse(_iCurPoem, LinePosition, VersePosition.Left);
                            if (newVerse == null)
                                return false;
                            Position = VersePosition.Right;
                        }
                        break;
                }
                _db.SetVerseText(newVerse._PoemID, newVerse._Order, verse);
                LinePosition = newVerse._Order;
            }
            _db.CommitBatchOperation();
            ShowPoem(_db.GetPoem(_iCurPoem), false);
            return true;
        }
        public bool InsertVerses(string[] verses, int LineCount, bool FullLine, bool IgnoreEmptyLines, bool IgnoreShortLines, int minLength)
        {
            Save();
            int LinePosition = GetCurrentLine();
            VersePosition Position = VersePosition.Right;
            int numPassed = 0;
            _db.BeginBatchOperation();
            foreach (string verse in verses)
            {
                if (IgnoreEmptyLines && string.IsNullOrEmpty(verse.Trim()))
                    continue;
                if (IgnoreShortLines && verse.Trim().Length < minLength)
                    continue;
                GanjoorVerse newVerse;
                switch (Position)
                {
                    case VersePosition.Right:
                        {
                            newVerse = _db.CreateNewVerse(_iCurPoem, LinePosition, VersePosition.Right);
                            if (newVerse == null)
                                return false;
                            Position = VersePosition.Left;
                        }
                        break;
                    case VersePosition.Left:
                        {
                            newVerse = _db.CreateNewVerse(_iCurPoem, LinePosition, VersePosition.Left);
                            if (newVerse == null)
                                return false;
                            numPassed++;
                            Position = (numPassed == LineCount) ? VersePosition.CenteredVerse1 : VersePosition.Right;
                        }
                        break;
                    case VersePosition.CenteredVerse1:
                        {
                            newVerse = _db.CreateNewVerse(_iCurPoem, LinePosition, VersePosition.CenteredVerse1);
                            if (newVerse == null)
                            {
                                _db.CommitBatchOperation();
                                return false;
                            }
                            numPassed = 0;
                            Position = FullLine ? VersePosition.CenteredVerse2 : VersePosition.Right;
                        }
                        break;
                    default://case VersePosition.CenteredVerse2:                    
                        {
                            newVerse = _db.CreateNewVerse(_iCurPoem, LinePosition, VersePosition.CenteredVerse2);
                            if (newVerse == null)
                            {
                                _db.CommitBatchOperation();
                                return false;
                            }
                            Position = VersePosition.Right;
                        }
                        break;

                }
                _db.SetVerseText(newVerse._PoemID, newVerse._Order, verse);
                LinePosition = newVerse._Order;
            }
            _db.CommitBatchOperation();
            ShowPoem(_db.GetPoem(_iCurPoem), false);
            return true;
        }
        private bool NewLine(VersePosition Position)
        {
            Save();
            int LinePosition = GetCurrentLine();
            GanjoorVerse firstVerse;
            _db.BeginBatchOperation();
            switch (Position)
            {
                case VersePosition.CenteredVerse1:
                    {
                        firstVerse = _db.CreateNewVerse(_iCurPoem, LinePosition, VersePosition.CenteredVerse1);
                        if (firstVerse == null)
                        {
                            _db.CommitBatchOperation();
                            return false;
                        }
                    }
                    break;
                case VersePosition.CenteredVerse2:
                    {
                        firstVerse = _db.CreateNewVerse(_iCurPoem, LinePosition, VersePosition.CenteredVerse1);
                        if (firstVerse == null)
                        {
                            _db.CommitBatchOperation();
                            return false;
                        }

                        GanjoorVerse secondVerse = _db.CreateNewVerse(_iCurPoem, firstVerse._Order, VersePosition.CenteredVerse2);
                        if (secondVerse == null)
                        {
                            _db.CommitBatchOperation();
                            return false;
                        }
                    }
                    break;
                case VersePosition.Single:
                case VersePosition.Paragraph:
                    {
                        firstVerse = _db.CreateNewVerse(_iCurPoem, LinePosition, Position);
                        if (firstVerse == null)
                        {
                            _db.CommitBatchOperation();
                            return false;
                        }
                    }
                    break;
                default:
                    {
                        firstVerse = _db.CreateNewVerse(_iCurPoem, LinePosition, VersePosition.Right);
                        if (firstVerse == null)
                        {
                            _db.CommitBatchOperation();
                            return false;
                        }

                        GanjoorVerse secondVerse = _db.CreateNewVerse(_iCurPoem, firstVerse._Order, VersePosition.Left);
                        if (secondVerse == null)
                        {
                            _db.CommitBatchOperation();
                            return false;
                        }
                    }
                    break;
            }
            _db.CommitBatchOperation();
            ShowPoem(_db.GetPoem(_iCurPoem), false);
            foreach (Control ctl in this.Controls)
                if (ctl is TextBox)
                    if ((ctl.Tag as GanjoorVerse)._Order == firstVerse._Order)
                    {
                        ctl.Focus();
                        break;
                    }
            return true;

        }
        private int GetCurrentLine()
        {
            int LinePosition = 0;
            int catsTop = 0;
            foreach (Control ctl in this.Controls)
            {
                if (ctl is LinkLabel)
                {
                    catsTop = Math.Max(ctl.Top, catsTop);
                }
                else
                    if (ctl.Focused)
                    {
                        if (ctl is TextBox)
                        {
                            GanjoorVerse verseBefore = (ctl.Tag as GanjoorVerse);
                            if (verseBefore._Position == VersePosition.Right)
                            {
                                foreach (Control ctlO in this.Controls)
                                {
                                    if (ctlO is TextBox)
                                    {
                                        if ((ctlO.Tag as GanjoorVerse)._Order == verseBefore._Order + 1)
                                        {
                                            verseBefore = (ctlO.Tag as GanjoorVerse);
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                                if (verseBefore._Position == VersePosition.CenteredVerse1)
                                {
                                    foreach (Control ctlO in this.Controls)
                                    {
                                        if (ctlO is TextBox)
                                        {
                                            if ((ctlO.Tag as GanjoorVerse)._Order == verseBefore._Order + 1)
                                            {
                                                if ((ctlO.Tag as GanjoorVerse)._Position == VersePosition.CenteredVerse2)
                                                {
                                                    verseBefore = (ctlO.Tag as GanjoorVerse);
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                            LinePosition = verseBefore._Order;
                            catsTop = ctl.Top;
                        }
                        break;
                    }
            }
            return LinePosition;
        }
        public void Save()
        {
            foreach(Control ctl in this.Controls)
                if (ctl is TextBox)
                {
                    DRY_SaveVerse(ctl as TextBox);
                }
        }
        private void lblVerse_Leave(object sender, EventArgs e)
        {
            DRY_SaveVerse(sender as TextBox);
        }
        private static void DRY_SaveVerse(TextBox verseBox)
        {
            if (verseBox.CanUndo)
            {
                GanjoorVerse verse = verseBox.Tag as GanjoorVerse;
                _db.SetVerseText(verse._PoemID, verse._Order, verseBox.Text);
                verseBox.ClearUndo();
                verseBox.BackColor = Color.White;
            }
        }
        private void lblVerse_TextChanged(object sender, EventArgs e)
        {
            TextBox verseBox = sender as TextBox;
            verseBox.BackColor = Color.LightYellow;
        }
        public bool DeleteAllLines()
        {
            List<GanjoorVerse> Verses = _db.GetVerses(_iCurPoem);
            List<int> versesToDelete = new List<int>();
            foreach (GanjoorVerse Verse in Verses)
                versesToDelete.Add(Verse._Order);

            if (_db.DeleteVerses(_iCurPoem, versesToDelete))
            {
                ShowPoem(_db.GetPoem(_iCurPoem), false);
                return true;
            }
            return false;
        }
        public bool DeleteLine()
        {
            List<int> versesToDelete = new List<int>();
            foreach (Control ctl in this.Controls)
            {
                if (ctl.Focused)
                {
                    if (ctl is TextBox)
                    {
                        GanjoorVerse verseBefore = (ctl.Tag as GanjoorVerse);
                        versesToDelete.Add(verseBefore._Order);
                        if (verseBefore._Position == VersePosition.Left)
                        {
                            versesToDelete.Add(verseBefore._Order-1);
                        }
                        else
                        if (verseBefore._Position == VersePosition.Right)
                        {
                            versesToDelete.Add(verseBefore._Order + 1);
                        }
                        else
                            if (verseBefore._Position == VersePosition.CenteredVerse2)
                            {
                                versesToDelete.Add(verseBefore._Order - 1);
                            }
                            else
                                if (verseBefore._Position == VersePosition.CenteredVerse1)
                                {
                                    foreach (Control ctlO in this.Controls)
                                    {
                                        if (ctlO is TextBox)
                                        {
                                            if ((ctlO.Tag as GanjoorVerse)._Order == verseBefore._Order + 1)
                                            {
                                                if ((ctlO.Tag as GanjoorVerse)._Position == VersePosition.CenteredVerse2)
                                                {
                                                    versesToDelete.Add(verseBefore._Order + 1);
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                    }
                    break;
                }
            }
            if (versesToDelete.Count == 0)
                return false;
            int MinVerseIndex = versesToDelete[0];
            if (_db.DeleteVerses(_iCurPoem, versesToDelete))
            {
                ShowPoem(_db.GetPoem(_iCurPoem), false);
                Control lastCtl = null;
                foreach (Control ctl in this.Controls)
                    if (ctl is TextBox)
                    {
                        if ((ctl.Tag as GanjoorVerse)._Order == MinVerseIndex)
                        {
                            lastCtl = ctl;
                            break;
                        }
                        else
                            lastCtl = ctl;
                    }
                if (lastCtl != null)
                    lastCtl.Focus();

            
                return true;
            }
            return false;

        }
        public bool DeletePoet()
        {
            GanjoorPoet poet = _db.GetPoetForCat(_iCurCat);
            if (null == poet)
            {
                //Somethig that should never happen, happened!
                //So try to fix it hoping not just adding another mess:
                GanjoorCat cat = _db.GetCategory(_iCurCat);
                if (null == cat)
                    return false;
                while (cat._ParentID != 0)
                {
                    cat = _db.GetCategory(cat._ParentID);
                    if (null == cat)
                        return false;
                }
                //I guess this might be dangerous:
                GanjoorPoet correspondingPoet = null;
                foreach(GanjoorPoet p in _db.Poets)
                    if (p._Name == cat._Text)
                    {
                        correspondingPoet = p;
                        break;
                    }

                if (_db.GetCategory(correspondingPoet._CatID) == null)
                    _db.DeletePoet(correspondingPoet._ID);

                _db.DeleteCat(cat._ID);
                ShowHome(true);
                return true;
            }
                
            _db.DeletePoet(poet._ID);
            ShowHome(true);
            return true;
        }
        public bool DeleteCategory()
        {
            GanjoorCat cat = _db.GetCategory(_iCurCat);
            if (null == cat)
                return false;
            _db.DeleteCat(_iCurCat);
            ShowHome(true);
            return true;
        }
        public bool ExportCategory(string fileName)
        {
            if (_db.GetCategory(_iCurCat) == null)
                return false;
            return _db.ExportCategory(fileName, _iCurCat);
        }
        public bool ExportPoet(string fileName)
        {
            GanjoorPoet poet = _db.GetPoetForCat(_iCurCat);
            if (null == poet)
                return false;
            return _db.ExportPoet(fileName, poet._ID);
        }
        public void GetIDs(out int PoetID, out int MinCatID, out int MinPoemID)
        {
            GanjoorPoet poet = _db.GetPoetForCat(_iCurCat);
            PoetID = poet == null ? 0 : poet._ID;
            _db.GetMinIDs(PoetID, out MinCatID, out MinPoemID);

        }
        public void SetIDs(int PoetID, int MinCatID, int MinPoemID)
        {
            _db.ChangePoetID(_db.GetPoetForCat(_iCurCat)._ID, PoetID);
            _db.ChangeCatIDs(PoetID, MinCatID);
            _db.ChangePoemIDs(PoetID, MinPoemID);
            ShowHome(true);
        }
        public void MoveToCategory(int CatID)
        {
            if(_db.ChangePoemCategory(_iCurPoem, CatID))
            {
                ShowPoem(_db.GetPoem(_iCurPoem), false);
            }
        }
        #endregion

    }
}
