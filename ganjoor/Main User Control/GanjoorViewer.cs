using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using ganjoor.Properties;
using ganjoor.Utilities;
using NAudio.Wave;

namespace ganjoor
{
    public partial class GanjoorViewer : UserControl
    {
        #region Constructor
        public GanjoorViewer()
        {
            InitializeComponent();

            //scroll using arrow keys:
            PreviewKeyDown += GanjoorViewer_PreviewKeyDown;

            ApplyUISettings();

            _iCurCat = _iCurPoem = 0;
            _strLastPhrase = "";
            _history = new Stack<GanjoorBrowsingHistory>();

            if (!DesignMode && _db == null)
            {
                _db = new DbBrowser();
                if (!_db.Failed && !Settings.Default.DbIsIndexed)
                {
                    var msgDlg = new WaitMsg("ایندکس گذاری پایگاه داده ها برای افزایش سرعت ...");
                    msgDlg.Show();
                    Application.DoEvents();
                    _db.CreateIndexes();
                    msgDlg.Dispose();
                    Settings.Default.DbIsIndexed = true;
                    Settings.Default.Save();
                }
            }
        }

        #endregion

        #region Database Browser
        private static DbBrowser _db;
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
        private const int MaxPoemPreviewLength = 50;
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
                            var cat = _db.GetCategory(Settings.Default.LastCat);
                            if (cat != null)
                                cat._StartPoem = Settings.Default.LastCatStart;
                            ShowCategory(cat, false);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Settings.Default.LastSearchPhrase))
                        {
                            ShowSearchResults(Settings.Default.LastSearchPhrase, Settings.Default.LastSeachStart, Settings.Default.SearchPageItems, Settings.Default.LastSearchPoetID, Settings.Default.LastSearchType);
                        }
                        else
                            ShowHome(true);
                    }
                }
            }
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
            SuspendLayout();
            VerticalScroll.Value = 0; HorizontalScroll.Value = 0;
            ClearControls();


            var catsTop = DistanceFromTop;
            int lastDistanceFromRight;
            ShowCategory(category, ref catsTop, out lastDistanceFromRight, true, false);

            var subcats = _db.GetSubCategories(category._ID);
            if (subcats.Count != 0)
                lastDistanceFromRight += 2 * DistanceFromRightStep;
            for (var i = 0; i < subcats.Count; i++)
            {
                var lblCat = new LinkLabel();
                lblCat.Tag = subcats[i];
                lblCat.AutoSize = true;
                lblCat.Text = subcats[i]._Text;
                lblCat.Location = new Point(lastDistanceFromRight, catsTop + i * DistanceBetweenLines);
                lblCat.LinkBehavior = LinkBehavior.HoverUnderline;
                lblCat.BackColor = Color.Transparent;
                lblCat.LinkColor = Settings.Default.LinkColor;
                lblCat.ForeColor = lblCat.LinkColor;
                lblCat.LinkClicked += lblCat_Click;
                Controls.Add(lblCat);
            }

            var poemsDistanceFromRight = DistanceFromRight + lastDistanceFromRight;
            var poemsTop = catsTop + subcats.Count * DistanceBetweenLines;
            var poems = _db.GetPoems(category._ID);
            category._StartPoem = Math.Max(0, category._StartPoem);
            var preCat = category._StartPoem == 0 ? null : new GanjoorCat(category, category._StartPoem - Settings.Default.MaxPoemsInList);
            var nextCat = category._StartPoem + Settings.Default.MaxPoemsInList < poems.Count ? new GanjoorCat(category, category._StartPoem + Settings.Default.MaxPoemsInList) : null;
            var ParagraphShift = 0;
            for (var i = category._StartPoem; i < Math.Min(poems.Count, category._StartPoem + Settings.Default.MaxPoemsInList); i++)
            {

                var lblPoem = new LinkLabel();
                lblPoem.Tag = poems[i];
                lblPoem.AutoSize = true;
                lblPoem.Text = poems[i]._Title;
                var v = _db.GetVerses(poems[i]._ID, 1);
                if (v.Count > 0)
                {
                    var vText = v[0]._Text;
                    if (vText.Length > MaxPoemPreviewLength)
                        vText = vText.Substring(0, MaxPoemPreviewLength - 4) + " ...";
                    lblPoem.Text += " : " + vText;
                }
                lblPoem.Location = new Point(poemsDistanceFromRight, poemsTop + (i - category._StartPoem) * DistanceBetweenLines + ParagraphShift);
                var szPoemTitleSizeWithWordWrap = TextRenderer.MeasureText(lblPoem.Text, Font, new Size(Width - lastDistanceFromRight - 20, Int32.MaxValue), TextFormatFlags.WordBreak);
                var szPoemTitleSizeWithoutWordWrap = TextRenderer.MeasureText(lblPoem.Text, Font, new Size(Width - lastDistanceFromRight - 20, Int32.MaxValue), TextFormatFlags.Default);
                if (szPoemTitleSizeWithWordWrap.Width != szPoemTitleSizeWithoutWordWrap.Width)
                {
                    ParagraphShift += szPoemTitleSizeWithWordWrap.Height - lblPoem.Height / 2;
                    lblPoem.AutoSize = false;
                    lblPoem.Size = new Size(Width - lastDistanceFromRight - 20, szPoemTitleSizeWithWordWrap.Height);

                }

                lblPoem.LinkBehavior = LinkBehavior.HoverUnderline;
                lblPoem.BackColor = Color.Transparent;
                lblPoem.LinkColor = Settings.Default.LinkColor;
                lblPoem.ForeColor = lblPoem.LinkColor;
                lblPoem.LinkClicked += lblPoem_Click;
                Controls.Add(lblPoem);
                if (_db.IsPoemFaved(poems[i]._ID))
                {
                    var fav = new PictureBox();
                    fav.BackColor = Color.Transparent;
                    fav.Image = Resources.fav;
                    fav.Size = new Size(16, 16);
                    fav.Location = new Point(lblPoem.Location.X - 16, lblPoem.Location.Y + (lblPoem.Size.Height - 16) / 2);
                    fav.AccessibleName = "نشانه";
                    Controls.Add(fav);
                }

            }


            //=========== 92-04-14 ===== begin
            var poemsCount = category._StartPoem + Settings.Default.MaxPoemsInList < poems.Count ? Settings.Default.MaxPoemsInList : poems.Count - category._StartPoem;
            var goLower = false;
            if (preCat != null)
            {
                var lblPrevPage = new LinkLabel();
                lblPrevPage.Tag = preCat;
                lblPrevPage.AutoSize = true;
                lblPrevPage.Text = "صفحهٔ قبل";
                lblPrevPage.Location = new Point(200, poemsTop + poemsCount * DistanceBetweenLines);
                lblPrevPage.LinkBehavior = LinkBehavior.HoverUnderline;
                lblPrevPage.BackColor = Color.Transparent;
                lblPrevPage.LinkColor = Settings.Default.LinkColor;
                lblPrevPage.ForeColor = lblPrevPage.LinkColor;
                lblPrevPage.LinkClicked += lblNextPage_Click;
                Controls.Add(lblPrevPage);

                goLower = true;


            }

            if (nextCat != null)
            {
                var lblNextPage = new LinkLabel();
                lblNextPage.Tag = nextCat;
                lblNextPage.AutoSize = true;
                lblNextPage.Text = "صفحهٔ بعد";
                lblNextPage.Location = new Point(Width - 200, poemsTop + poemsCount * DistanceBetweenLines);
                lblNextPage.LinkBehavior = LinkBehavior.HoverUnderline;
                lblNextPage.BackColor = Color.Transparent;
                lblNextPage.LinkColor = Settings.Default.LinkColor;
                lblNextPage.ForeColor = lblNextPage.LinkColor;
                lblNextPage.LinkClicked += lblNextPage_Click;
                Controls.Add(lblNextPage);

                goLower = true;

            }

            if (goLower)
                poemsCount++;

            //=========== 92-04-14 ===== end

            //یک بر چسب اضافی برای اضافه شدن فضای پایین فرم

            var lblDummy = new Label();
            lblDummy.Text = " ";
            lblDummy.Location = new Point(200, poemsTop + poemsCount * DistanceBetweenLines);
            lblDummy.BackColor = Color.Transparent;
            Controls.Add(lblDummy);


            //کلک راست به چپ!
            foreach (Control ctl in Controls)
            {
                ctl.Location = new Point(Width - ctl.Right, ctl.Location.Y);
            }
            AssignPreviewKeyDownEventToControls();
            ResumeLayout();
            Cursor = Cursors.Default;
            _strLastPhrase = null;
            if (category._ID == 0)//نمایش تعداد شاعران
                _strPage += string.Format(" ({0} شاعر)", subcats.Count);
            StopPlayBack();
            _CurrentPoemAudio = null;
            OnPageChanged?.Invoke(_strPage, false, true, false, false, string.Empty, preCat, nextCat);
        }

        private void lblNextPage_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessPagingTag((sender as LinkLabel).Tag);
        }

        private void lblPoem_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPoem(
                (GanjoorPoem)((LinkLabel)sender).Tag
                ,
                true
                );
        }

        private void lblCat_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GanjoorCat category = null;
            if (sender != null)
                category = (GanjoorCat)((LinkLabel)sender).Tag;
            else
                category = new GanjoorCat(0, 0, "", 0, "");


            ShowCategory(category, true);
        }

        private void ShowCategory(GanjoorCat category, ref int catsTop, out int lastDistanceFromRight, bool highlightCat, bool showingPoem)
        {
            lastDistanceFromRight = DistanceFromRight;


            _strPage = "";

            //اجداد این دسته
            var ancestors = _db.GetParentCategories(category);

            for (var i = 0; i < ancestors.Count; i++)
            {
                if (ancestors[i]._Text != "خانه")
                {
                    _strPage += ancestors[i]._Text;
                    if (category != null && 0 != category._ID) _strPage += " -> ";
                }
                var lblCat = new LinkLabel();
                lblCat.Tag = ancestors[i];
                lblCat.AutoSize = true;
                lblCat.Text = ancestors[i]._Text;
                lastDistanceFromRight += 2 * DistanceFromRightStep;
                lblCat.Location = new Point(lastDistanceFromRight, catsTop + i * DistanceBetweenLines);
                lblCat.LinkBehavior = LinkBehavior.HoverUnderline;
                lblCat.BackColor = Color.Transparent;
                lblCat.LinkColor = Settings.Default.LinkColor;
                lblCat.ForeColor = lblCat.LinkColor;
                lblCat.LinkClicked += lblCat_Click;
                Controls.Add(lblCat);
            }


            catsTop += ancestors.Count * DistanceBetweenLines;
            //خود این دسته

            if (category != null && category._ID != 0)
            {
                _strPage += category._Text;

                var lblMe = new LinkLabel();
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
                lblMe.LinkClicked += lblCat_Click;
                Controls.Add(lblMe);

                catsTop += DistanceBetweenLines;
            }
            if (!showingPoem && category != null)
            {
                var poet = _db.GetPoet(category._PoetID);
                if (poet != null && poet._CatID == category._ID)
                {
                    var lblBio = new HighlightLabel();
                    lblBio.BackColor = Color.Transparent;
                    lblBio.AutoSize = false;
                    {
                        var labelHeight = lblBio.Height;
                        var sz = new Size(Width - lastDistanceFromRight - 20, Int32.MaxValue);
                        sz = TextRenderer.MeasureText(poet._Bio, Font, sz, TextFormatFlags.WordBreak);
                        lblBio.Size = new Size(Width - lastDistanceFromRight - 20, sz.Height);
                        lblBio.Location = new Point(lastDistanceFromRight, catsTop);
                        lblBio.Text = poet._Bio;
                        Controls.Add(lblBio);
                        catsTop += sz.Height + 10;
                    }

                }
            }

            _FavsPage = false;
            _iCurCat = category != null ? category._ID : 0;
            _iCurCatStart = category != null ? category._StartPoem : 0;
            _iCurPoem = 0;
        }
        public int ShowPoem(GanjoorPoem poem, bool keepTrack)
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
            SuspendLayout();
            VerticalScroll.Value = 0; HorizontalScroll.Value = 0;
            ClearControls();

            var catsTop = DistanceFromTop;
            int lastDistanceFromRight;
            ShowCategory(_db.GetCategory(poem._CatID), ref catsTop, out lastDistanceFromRight, false, true);
            lastDistanceFromRight += DistanceFromRightStep;

            _strPage += " -> " + poem._Title;


            var lblPoem = new LinkLabel();
            poem._HighlightText = string.Empty;
            lblPoem.Tag = poem;
            lblPoem.AutoSize = true;
            lblPoem.Text = poem._Title;
            var szPoemTitleSizeWithWordWrap = TextRenderer.MeasureText(poem._Title, Font, new Size(Width - lastDistanceFromRight - 20, Int32.MaxValue), TextFormatFlags.WordBreak);
            var szPoemTitleSizeWithoutWordWrap = TextRenderer.MeasureText(poem._Title, Font, new Size(Width - lastDistanceFromRight - 20, Int32.MaxValue), TextFormatFlags.Default);
            var ParagraphShift = 0;
            if (szPoemTitleSizeWithWordWrap.Width != szPoemTitleSizeWithoutWordWrap.Width)
            {
                ParagraphShift += szPoemTitleSizeWithWordWrap.Height - lblPoem.Height;
                lblPoem.AutoSize = false;
                lblPoem.Size = new Size(Width - lastDistanceFromRight - 20, szPoemTitleSizeWithWordWrap.Height);
                lblPoem.Location = new Point(lastDistanceFromRight, catsTop);

            }
            else
            if (CenteredView)
                lblPoem.Location = new Point(Width / 2 - TextRenderer.MeasureText(poem._Title, Font).Width / 2, catsTop);
            else
                lblPoem.Location = new Point(lastDistanceFromRight, catsTop);
            lblPoem.LinkVisited = true;
            lblPoem.LinkBehavior = LinkBehavior.HoverUnderline;
            lblPoem.BackColor = Color.Transparent;
            lblPoem.LinkClicked += lblPoem_Click;
            lblPoem.VisitedLinkColor = Settings.Default.CurrentLinkColor;
            lblPoem.ForeColor = lblPoem.VisitedLinkColor;
            Controls.Add(lblPoem);

            catsTop += DistanceBetweenLines;
            lastDistanceFromRight += DistanceFromRightStep;


            var verses = _db.GetVerses(poem._ID);


            var WholeBeytNum = 0;
            var BeytNum = 0;
            var BandNum = 0;
            var BandBeytNums = 0;
            var WholeNimayeeLines = 0;
            var MustHave2ndBandBeyt = false;
            var MissedMesras = 0;

            var MesraWidth = 250;
            if (EditMode)
            {
                for (var i = 0; i < verses.Count; i++)
                    if (verses[i]._Position != VersePosition.Paragraph && verses[i]._Position != VersePosition.Single)
                        MesraWidth = Math.Max(MesraWidth, TextRenderer.MeasureText(verses[i]._Text, Font).Width);
            }


            var versDistanceFromRight = ShowBeytNums ? lastDistanceFromRight + TextRenderer.MeasureText(verses.Count.ToString(), Font).Width : lastDistanceFromRight;
            for (var i = 0; i < verses.Count; i++)
            {
                Control lblVerse;
                if (EditMode)
                {
                    lblVerse = new TextBox();
                    lblVerse.Font = Font;
                    if (CenteredView)
                    {
                        lblVerse.Size = new Size(MesraWidth, lblVerse.Size.Height);
                        var vTop = catsTop + ((i - MissedMesras) / 2 + BandBeytNums) * DistanceBetweenLines + ParagraphShift;
                        switch (verses[i]._Position)
                        {
                            case VersePosition.Right:
                                lblVerse.Location = new Point(Width / 2 - 5 - MesraWidth, vTop);
                                if (MustHave2ndBandBeyt)
                                {
                                    MissedMesras++;
                                    MustHave2ndBandBeyt = false;
                                }
                                break;
                            case VersePosition.Left:
                                lblVerse.Location = new Point(Width / 2 + 5, vTop);
                                break;
                            case VersePosition.CenteredVerse1:
                                lblVerse.Location = new Point(Width / 2 - MesraWidth / 2, vTop);
                                BandBeytNums++;
                                MustHave2ndBandBeyt = true;
                                break;
                            case VersePosition.CenteredVerse2:
                                MustHave2ndBandBeyt = false;
                                lblVerse.Location = new Point(Width / 2 - MesraWidth / 2, vTop);
                                break;
                            case VersePosition.Comment:
                            case VersePosition.Single:
                            case VersePosition.Paragraph:
                                {
                                    var txtHeight = lblVerse.Height;
                                    (lblVerse as TextBox).Multiline = true;
                                    var sz = new Size(Width - versDistanceFromRight - 20, Int32.MaxValue);
                                    var txtMeasure = verses[i]._Text;
                                    if (string.IsNullOrEmpty(txtMeasure))
                                    {
                                        //احتمالاً پاراگراف جدیدی بدون متن
                                        for (var c = 0; c < 50; c++)
                                            txtMeasure += "گنجور ";
                                    }
                                    sz = TextRenderer.MeasureText(txtMeasure, Font, sz, TextFormatFlags.WordBreak);
                                    var sz2 = TextRenderer.MeasureText("گنجور", Font, sz, TextFormatFlags.WordBreak);
                                    lblVerse.Size = new Size(Width - versDistanceFromRight - 20, sz.Height / sz2.Height * txtHeight);
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
                    lblVerse.TabStop = true;
                    lblVerse.BackColor = Color.Transparent;
                    lblVerse.AutoSize = true;
                    if (CenteredView)
                    {
                        var vTop = catsTop + ((i - MissedMesras) / 2 + BandBeytNums) * DistanceBetweenLines + ParagraphShift;
                        switch (verses[i]._Position)
                        {
                            case VersePosition.Right:
                                lblVerse.Location = new Point(Width / 2 - 5 - TextRenderer.MeasureText(verses[i]._Text, Font).Width, vTop);
                                if (MustHave2ndBandBeyt)
                                {
                                    MissedMesras++;
                                    MustHave2ndBandBeyt = false;
                                }
                                break;
                            case VersePosition.Left:
                                lblVerse.Location = new Point(Width / 2 + 5, vTop);
                                break;
                            case VersePosition.CenteredVerse1:
                                lblVerse.Location = new Point(Width / 2 - TextRenderer.MeasureText(verses[i]._Text, Font).Width / 2, vTop);
                                BandBeytNums++;
                                MustHave2ndBandBeyt = true;
                                break;
                            case VersePosition.CenteredVerse2:
                                MustHave2ndBandBeyt = false;
                                lblVerse.Location = new Point(Width / 2 - TextRenderer.MeasureText(verses[i]._Text, Font).Width / 2, vTop);
                                break;
                            case VersePosition.Comment:
                            case VersePosition.Single:
                            case VersePosition.Paragraph:
                                (lblVerse as Label).AutoSize = false;
                                {
                                    var labelHeight = lblVerse.Height;
                                    var sz = new Size(Width - versDistanceFromRight - 20, Int32.MaxValue);
                                    sz = TextRenderer.MeasureText(verses[i]._Text, Font, sz, TextFormatFlags.WordBreak);
                                    lblVerse.Size = new Size(Width - versDistanceFromRight - 20, sz.Height);
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
                            var labelHeight = lblVerse.Height;
                            var sz = new Size(Width - versDistanceFromRight - 20, Int32.MaxValue);
                            sz = TextRenderer.MeasureText(verses[i]._Text, Font, sz, TextFormatFlags.WordBreak);
                            lblVerse.Size = new Size(Width - versDistanceFromRight - 20, sz.Height);
                            ParagraphShift += sz.Height;
                        }
                    }

                }

                lblVerse.Tag = verses[i];
                lblVerse.Text = verses[i]._Text;
                if (EditMode)
                {
                    if (verses[i]._Position == VersePosition.Comment)
                        lblVerse.BackColor = Color.LightGray;
                    lblVerse.Leave += lblVerse_Leave;
                    lblVerse.TextChanged += lblVerse_TextChanged;
                    lblVerse.KeyDown += lblVerse_KeyDown;
                }
                Controls.Add(lblVerse);


                if (verses[i]._Position == VersePosition.Right || verses[i]._Position == VersePosition.CenteredVerse1 || verses[i]._Position == VersePosition.Single)
                {
                    if (!string.IsNullOrEmpty(verses[i]._Text.Trim()))//empty verse strings have been seen sometimes, it seems that we have some errors in our database
                    {
                        if (verses[i]._Position == VersePosition.Single)
                            WholeNimayeeLines++;
                        else
                            WholeBeytNum++;
                        var isBand = verses[i]._Position == VersePosition.CenteredVerse1;
                        if (isBand)
                        {
                            BeytNum = 0;
                            BandNum++;
                        }
                        else
                            BeytNum++;
                        var xDistance = TextRenderer.MeasureText("345", Font).Width;
                        if (ShowBeytNums)
                        {
                            var lblNum = new LinkLabel();
                            lblNum.AutoSize = true;
                            lblNum.Text = isBand ? BandNum.ToString() : BeytNum.ToString();
                            lblNum.Tag = verses[i];
                            lblNum.BackColor = Color.Transparent;
                            lblNum.LinkBehavior = LinkBehavior.HoverUnderline;
                            lblNum.Location = new Point(lastDistanceFromRight - xDistance, lblVerse.Location.Y);
                            lblNum.LinkColor = isBand ? Settings.Default.BandLinkColor : Settings.Default.LinkColor;
                            lblNum.ForeColor = lblNum.LinkColor;
                            lblNum.LinkClicked += lblNum_Click;
                            Controls.Add(lblNum);
                            if (_db.IsVerseFaved(poem._ID, verses[i]._Order))
                            {
                                var fav = new PictureBox();
                                fav.BackColor = Color.Transparent;
                                fav.Image = Resources.fav;
                                fav.Size = new Size(16, 16);
                                fav.Location = new Point(lastDistanceFromRight - xDistance, lblVerse.Location.Y);
                                fav.Tag = verses[i];
                                fav.Cursor = Cursors.Hand;
                                fav.Click += Fav_Click;
                                Controls.Add(fav);
                                lblNum.Visible = false;
                            }
                        }
                        else
                        {
                            if (_db.IsVerseFaved(poem._ID, verses[i]._Order))
                            {
                                var fav = new PictureBox();
                                fav.BackColor = Color.Transparent;
                                fav.Image = Resources.fav;
                                fav.Size = new Size(16, 16);
                                fav.Location = new Point(lastDistanceFromRight - xDistance, lblVerse.Location.Y);
                                fav.Tag = verses[i];
                                fav.Cursor = Cursors.Hand;
                                fav.Click += Fav_Click;
                                Controls.Add(fav);
                            }
                        }
                    }
                }


            }
            //یک بر چسب اضافی برای اضافه شدن فضای پایین فرم
            var lblDummy = new Label();
            lblDummy.Text = " ";
            var yLocDummy =
                CenteredView ?
                    catsTop + ((verses.Count - MissedMesras) / 2 + BandBeytNums) * DistanceBetweenLines
                    :
                    catsTop + verses.Count * DistanceBetweenLines;
            lblDummy.Location = new Point(200, yLocDummy);
            lblDummy.BackColor = Color.Transparent;
            Controls.Add(lblDummy);

            //شعر بعد / شعر قبل

            var pre = _db.GetPreviousPoem(poem._ID, poem._CatID);
            if (pre != null)
            {
                var lblPrePoem = new LinkLabel();
                lblPrePoem.Tag = pre;
                lblPrePoem.AutoSize = true;
                lblPrePoem.Text = "شعر قبل";
                lblPrePoem.Location = new Point(200, yLocDummy + DistanceBetweenLines);
                lblPrePoem.LinkBehavior = LinkBehavior.HoverUnderline;
                lblPrePoem.BackColor = Color.Transparent;
                lblPrePoem.LinkColor = Settings.Default.LinkColor;
                lblPrePoem.ForeColor = lblPrePoem.LinkColor;
                lblPrePoem.LinkClicked += lnlPreOrNextPoemClicked;
                Controls.Add(lblPrePoem);
            }

            var next = _db.GetNextPoem(poem._ID, poem._CatID);
            if (next != null)
            {
                var lblNextPoem = new LinkLabel();
                lblNextPoem.Tag = next;
                lblNextPoem.AutoSize = true;
                lblNextPoem.Text = "شعر بعد";
                lblNextPoem.Location = new Point(Width - 200, yLocDummy + DistanceBetweenLines);
                lblNextPoem.LinkBehavior = LinkBehavior.HoverUnderline;
                lblNextPoem.BackColor = Color.Transparent;
                lblNextPoem.LinkColor = Settings.Default.LinkColor;
                lblNextPoem.ForeColor = lblNextPoem.LinkColor;
                lblNextPoem.LinkClicked += lnlPreOrNextPoemClicked;
                Controls.Add(lblNextPoem);

            }


            //کلک راست به چپ!
            foreach (Control ctl in Controls)
                ctl.Location = new Point(Width - ctl.Right, ctl.Location.Y);

            AssignPreviewKeyDownEventToControls();
            ResumeLayout();

            Cursor = Cursors.Default;
            _FavsPage = false;
            _iCurPoem = poem._ID;

            _strPage += " (";
            if (WholeBeytNum > 0)
                _strPage += WholeBeytNum + " بیت";
            if (WholeNimayeeLines > 0)
            {
                if (WholeBeytNum > 0)
                    _strPage += "، ";
                _strPage += WholeNimayeeLines + " خط";
            }
            if (BandNum == 0)
                _strPage += ")";
            else
                _strPage += "، " + BandNum + " بند)";
            _strLastPhrase = null;
            StopPlayBack();
            _CurrentPoemAudio = _db.GetMainPoemAudio(_iCurPoem);
            OnPageChanged?.Invoke(_strPage, true, true, poem._Faved, false, highlightWord, null, null);
            return string.IsNullOrEmpty(highlightWord) || !Settings.Default.HighlightKeyword ? 0 : HighlightText(highlightWord);
        }

        private void lnlPreOrNextPoemClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPoem((sender as Control).Tag as GanjoorPoem, true);
        }

        private void lblNum_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DRY_FavUnfave(sender);
        }

        private void DRY_FavUnfave(object sender)
        {
            var y = VerticalScroll.Value;
            var verse = (sender as Control).Tag as GanjoorVerse;
            if (!_db.ToggleFav(verse._PoemID, verse._Order))
            {
                Control pbx = null;
                foreach (Control ctl in Controls)
                    if (ctl.Tag is GanjoorVerse ganjoorVerse && ganjoorVerse._Order == verse._Order)
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
                    Controls.Remove(pbx);
            }
            else
            {
                var fav = new PictureBox();
                fav.BackColor = Color.Transparent;
                fav.Image = Resources.fav;
                fav.Size = new Size(16, 16);
                fav.Location = new Point((sender as Control).Location.X + (sender as Control).Size.Width - 16, (sender as Control).Location.Y);
                fav.Tag = verse;
                fav.Cursor = Cursors.Hand;
                fav.Click += Fav_Click;
                Controls.Add(fav);
                (sender as Control).Visible = false;
            }
            StopPlayBack();
            _CurrentPoemAudio = null;
            OnPageChanged(_strPage, true, true, _db.IsPoemFaved(verse._PoemID), false, string.Empty, null, null);
            VerticalScroll.Value = y;
        }

        private void Fav_Click(object sender, EventArgs e)
        {
            DRY_FavUnfave(sender);
        }

        private void lblVerse_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    {
                        if (sender is TextBox box)
                        {
                            var txtBox = box;
                            if (!txtBox.Multiline)
                            {
                                var oldSelStart = txtBox.SelectionStart;
                                var CurrentVerse = txtBox.Tag as GanjoorVerse;
                                var newFocusTag =
                                    CurrentVerse._Position == VersePosition.Right || CurrentVerse._Position == VersePosition.Left
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
                        if (sender is TextBox box)
                        {
                            var txtBox = box;
                            if (!txtBox.Multiline)
                            {
                                var oldSelStart = txtBox.SelectionStart;
                                var CurrentVerse = txtBox.Tag as GanjoorVerse;
                                var newFocusTag =
                                    CurrentVerse._Position == VersePosition.Right || CurrentVerse._Position == VersePosition.Left
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
                        if (sender is TextBox box)
                        {
                            var txtBox = box;
                            if (!txtBox.Multiline)
                            {
                                var oldSelStart = txtBox.SelectionStart;
                                var CurrentVerse = txtBox.Tag as GanjoorVerse;
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
                foreach (Control ctl in Controls)
                    if (ctl is TextBox box)
                        if ((box.Tag as GanjoorVerse)._Order == verseOrder)
                        {
                            box.SelectionStart = SelStart;
                            box.Focus();
                            break;
                        }
        }

        #endregion

        #region Fancy Stuff!
        private Color bBegin;
        private Color bEnd;
        private bool GradiantBackground;
        private TextureBrush BackgroundBrush;

        public void ApplyUISettings()
        {
            ForeColor = Settings.Default.TextColor;
            BackColor = Settings.Default.BackColor;
            GradiantBackground = Settings.Default.GradiantBackground;
            if (!GradiantBackground)
                if (!string.IsNullOrEmpty(Settings.Default.BackImagePath))
                {
                    if (File.Exists(Settings.Default.BackImagePath))
                    {
                        BackgroundBrush?.Dispose();
                        try
                        {
                            BackgroundBrush = new TextureBrush(new Bitmap(Settings.Default.BackImagePath));
                        }
                        catch
                        {
                            BackgroundBrush = null;
                        }
                    }
                }
                else
                {
                    BackgroundBrush?.Dispose();
                    BackgroundBrush = null;

                }
            if (GradiantBackground || BackgroundBrush != null)
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            else
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint, false);
                SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            }
            bBegin = Settings.Default.GradiantBegin;
            bEnd = Settings.Default.GradiantEnd;
            foreach (Control ctl in Controls)
                if (ctl is LinkLabel label)
                {
                    var lbl = label;
                    if (lbl.Tag is GanjoorVerse verse && verse._Position == VersePosition.CenteredVerse1)
                        lbl.LinkColor = Settings.Default.BandLinkColor;
                    else
                        lbl.LinkColor = Settings.Default.LinkColor;
                    lbl.VisitedLinkColor = Settings.Default.CurrentLinkColor;
                    lbl.ForeColor = lbl.LinkVisited ? lbl.VisitedLinkColor : lbl.LinkColor;
                }
                else
                    if (ctl is HighlightLabel highlightLabel)
                {
                    highlightLabel.HighlightColor = Settings.Default.HighlightColor;
                }
            ScrollingSpeed = Math.Max(1, Settings.Default.ScrollingSpeed);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (GradiantBackground) {
                using var brsh = new LinearGradientBrush(Bounds, bBegin, bEnd, 0.0f);
                e.Graphics.FillRectangle(brsh, e.ClipRectangle);
            }
            else
                if (BackgroundBrush != null)
                e.Graphics.FillRectangle(BackgroundBrush, ClientRectangle);
            else
                base.OnPaint(e);

        }
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                try
                {
                    base.Font = value;
                }
                catch
                {
                    MessageBox.Show("در تنظیم قلم نمایش اشعار اشکالی رخ داد. لطفاً از طریق پیکربندی قلم دیگری را انتخاب کنید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                }
                AdjustLocationsByFont();
            }
        }
        private void AdjustLocationsByFont()
        {
            DistanceBetweenLines = TextRenderer.MeasureText("ا", Font).Width * 2;
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
                        var cat = _db.GetCategory(_iCurCat);
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
                var poem = _db.GetNextPoem(_iCurPoem, _iCurCat);
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
                var poem = _db.GetPreviousPoem(_iCurPoem, _iCurCat);
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
        public event PageChangedEvent OnPageChanged;
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
                    var poet = _db.GetPoetForCat(_iCurCat);
                    if (null != poet)
                        return poet._Name;
                }
                return string.Empty;
            }
        }

        public int CurrentPoetId
        {
            get
            {
                if (_db.Connected)
                {
                    var poet = _db.GetPoetForCat(_iCurCat);
                    if (null != poet)
                        return poet._ID;
                }
                return -1;
            }
        }
        public string CurrentPoetBio
        {
            get
            {
                if (_db.Connected)
                {
                    var poet = _db.GetPoetForCat(_iCurCat);
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
                    var cat = _db.GetCategory(_iCurCat);
                    if (null != cat)
                        return cat._Text;
                }
                return string.Empty;
            }
        }
        public int CurrentCatId
        {
            get
            {
                return _iCurCat;
            }
        }
        public string CurrentPoem
        {
            get
            {
                if (_db.Connected)
                {
                    var poem = _db.GetPoem(_iCurPoem);
                    if (null != poem)
                        return poem._Title;
                }
                return string.Empty;
            }
        }
        public int CurrentPoemId
        {
            get
            {
                return _iCurPoem;
            }
        }
        public string CurrentPageGanjoorUrl
        {
            get
            {
                if (0 == _iCurCat)
                    return "https://ganjoor.net";
                if (0 == _iCurPoem)
                    return _db.GetCategory(_iCurCat)._Url;
                return _db.GetPoem(_iCurPoem)._Url;
                /*
                 * using following code you can delete url field in poem table,
                 * 
                 * return "https://ganjoor.net/?p=" + _iCurPoem.ToString();
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
                if (0 == _iCurPoem)
                    return "https://ganjoor.net";
                return "https://ganjoor.net/?comments_popup=" + _iCurPoem;
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
        public static string DbFilePath
        {
            get
            {
                if (_db == null || _db.Failed)
                    return DbBrowser.DefaultDbPath;
                return _db.DbFilePath;
            }
        }
        #endregion

        #region Browing History
        private Stack<GanjoorBrowsingHistory> _history;
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
                _history.Push(new GanjoorBrowsingHistory(string.Empty, 0, _iCurSearchStart, _iCurSearchPageCount, true, AutoScrollPosition));
            }
            else
            if (!string.IsNullOrEmpty(_strLastPhrase))
            {
                _history.Push(new GanjoorBrowsingHistory(_strLastPhrase, _iCurSearchPoet, _iCurSearchStart, _iCurSearchPageCount, false, AutoScrollPosition));
            }
            else
                if (
                    _history.Count == 0 || !(_history.Peek()._CatID == _iCurCat && _history.Peek()._CatPageStart == _iCurCatStart && _history.Peek()._PoemID == _iCurPoem)
                    )
            {
                _history.Push(new GanjoorBrowsingHistory(_iCurCat, _iCurPoem, _iCurCatStart, AutoScrollPosition));

            }
        }
        public void GoBackInHistory()//forward towards back!
        {
            if (CanGoBackInHistory)
            {
                var back = _history.Pop();
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
                        var cat = _db.GetCategory(back._CatID);
                        cat._StartPoem = back._CatPageStart;
                        ShowCategory(cat, false);
                    }
                }
                else
                    ShowPoem(_db.GetPoem(back._PoemID), false);
                AutoScrollPosition = new Point(-back._AutoScrollPosition.X, -back._AutoScrollPosition.Y);
            }
        }
        #endregion

        #region Printing
        public void Print(PrintDocument Document)
        {
            Document.PrintPage += Document_PrintPage;
            _iRemainingUnprintedLines = 0;
            Document.Print();
        }
        public PrintDocument PrepareForPrintPreview()
        {
            _iRemainingUnprintedLines = 0;
            var Document = new PrintDocument();
            Document.PrintPage += Document_PrintPage;
            return Document;
        }
        private void Document_PrintPage(object sender, PrintPageEventArgs e)
        {
            _iRemainingUnprintedLines = PrintPoem(e, _iRemainingUnprintedLines);
            e.HasMorePages = 0 != _iRemainingUnprintedLines;

        }
        private int _iRemainingUnprintedLines;
        private int PrintPoem(PrintPageEventArgs e, int StartFrom)
        {
            if (0 != _iCurPoem)
            {
                var dist = 0 == StartFrom ? (int)(2.5f * DistanceBetweenLines) : DistanceBetweenLines + (int)(0.25f * DistanceBetweenLines);
                var top = e.PageBounds.Top + 100;
                var mid = e.PageBounds.Left + e.PageBounds.Width / 2;
                var format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                var line = -1;
                foreach (Control ctl in Controls)
                    if (ctl is Label)
                        if (!(ctl is LinkLabel) || ctl.Tag is GanjoorPoem)
                        {
                            line++;
                            if (line >= StartFrom)
                            {
                                e.Graphics.DrawString(ctl.Text, Font, Brushes.Black,
                                    new PointF(
                                        mid + TextRenderer.MeasureText(ctl.Text, Font).Width / 2,
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
        public void ShowSearchResults(string phrase, int PageStart, int Count, int PoetID, int SearchType = 0)
        {
            ShowSearchResults(phrase, PageStart, Count, PoetID, true, SearchType);
        }
        public void ShowSearchResults(string phrase, int PageStart, int Count, int PoetID, bool keepTrack = false, int searchType = 0, int searchLocationType = 0)
        {
            phrase = GPersianTextSync.Sync(phrase);
            if (keepTrack)
                UpdateHistory();
            Cursor = Cursors.WaitCursor; Application.DoEvents();
            SuspendLayout();
            VerticalScroll.Value = 0; HorizontalScroll.Value = 0;
            ClearControls();
            GanjoorSearchPage prePage = null, nextPage = null;
            using (var poemsList = _db.FindPoemsContaingPhrase(phrase, PageStart, Count + 1, PoetID, searchType, searchLocationType))
            {
                var CountCopy = Count;
                var HasMore = Count > 0 && poemsList.Rows.Count == Count + 1;
                if (poemsList.Rows.Count <= Count)
                    Count = poemsList.Rows.Count;
                else
                    Count = HasMore ? Count : poemsList.Rows.Count - 1;
                var catsTop = DistanceFromTop;
                for (var i = 0; i < Count; i++)
                {
                    var verse = new GanjoorVerse
                    {
                        _PoemID = Convert.ToInt32(poemsList.Rows[i].ItemArray[0]),
                        _Order = Convert.ToInt32(poemsList.Rows[i].ItemArray[1]),
                        _Position = (VersePosition)Convert.ToInt32(poemsList.Rows[i].ItemArray[2]),
                        _Text = poemsList.Rows[i].ItemArray[3].ToString()
                    };
                    var poem = _db.GetPoem(verse._PoemID);
                    poem._HighlightText = phrase;
                    int lastDistanceFromRight;
                    ShowCategory(_db.GetCategory(poem._CatID), ref catsTop, out lastDistanceFromRight, false, false);
                    lastDistanceFromRight += DistanceFromRightStep;
                    var lblPoem = new LinkLabel();
                    lblPoem.Tag = poem;
                    lblPoem.AutoSize = true;
                    lblPoem.Text = poem._Title + " - بیت " + GPersianTextSync.Sync((verse._Order / 2 + 1).ToString());
                    lblPoem.Location = new Point(lastDistanceFromRight, catsTop);
                    lblPoem.LinkBehavior = LinkBehavior.HoverUnderline;
                    lblPoem.BackColor = Color.Transparent;
                    lblPoem.LinkColor = Settings.Default.LinkColor;
                    lblPoem.ForeColor = lblPoem.LinkColor;
                    lblPoem.LinkClicked += lblPoem_Click;
                    Controls.Add(lblPoem);

                    catsTop += DistanceBetweenLines;
                    lastDistanceFromRight += DistanceFromRightStep;


                    //System.Diagnostics.Debug.Assert(firsVerse.Rows.Count == 1);

                    var lblVerse = new HighlightLabel(phrase, Settings.Default.HighlightColor);
                    lblVerse.AutoSize = true;
                    lblVerse.Tag = null;
                    lblVerse.Text = verse._Text.Truncate(80);
                    lblVerse.Location = new Point(lastDistanceFromRight, catsTop);
                    lblVerse.BackColor = Color.Transparent;
                    Controls.Add(lblVerse);
                    catsTop += 2 * DistanceBetweenLines;

                }

                if (PageStart > 0)
                {
                    var lblPrevPage = new LinkLabel();
                    prePage = new GanjoorSearchPage(phrase, PageStart - CountCopy, CountCopy, PoetID, searchType);
                    lblPrevPage.Tag = prePage;
                    lblPrevPage.AutoSize = true;
                    lblPrevPage.Text = "صفحهٔ قبل";
                    lblPrevPage.Location = new Point(200, catsTop);
                    lblPrevPage.LinkBehavior = LinkBehavior.HoverUnderline;
                    lblPrevPage.BackColor = Color.Transparent;
                    lblPrevPage.LinkColor = Settings.Default.LinkColor;
                    lblPrevPage.ForeColor = lblPrevPage.LinkColor;
                    lblPrevPage.LinkClicked += lblNextPage_Click;
                    Controls.Add(lblPrevPage);

                }

                if (HasMore)
                {
                    var lblNextPage = new LinkLabel();
                    nextPage = new GanjoorSearchPage(phrase, PageStart + Count, Count, PoetID, searchType);
                    lblNextPage.Tag = nextPage;
                    lblNextPage.AutoSize = true;
                    lblNextPage.Text = "صفحهٔ بعد";
                    lblNextPage.Location = new Point(Width - 200, catsTop);
                    lblNextPage.LinkBehavior = LinkBehavior.HoverUnderline;
                    lblNextPage.BackColor = Color.Transparent;
                    lblNextPage.LinkColor = Settings.Default.LinkColor;
                    lblNextPage.ForeColor = lblNextPage.LinkColor;
                    lblNextPage.LinkClicked += lblNextPage_Click;
                    Controls.Add(lblNextPage);
                    catsTop += DistanceBetweenLines;
                }


                //یک بر چسب اضافی برای اضافه شدن فضای پایین فرم
                var lblDummy = new Label();
                lblDummy.Text = " ";
                lblDummy.Location = new Point(200, catsTop);
                lblDummy.BackColor = Color.Transparent;
                Controls.Add(lblDummy);

                _FavsPage = false;
                _iCurCat = 0;
                _iCurSearchStart = PageStart;
                _iCurSearchPageCount = Count;
                _strLastPhrase = phrase;
                _iCurSearchPoet = PoetID;


            }


            //کلک راست به چپ!
            foreach (Control ctl in Controls)
                ctl.Location = new Point(Width - ctl.Right, ctl.Location.Y);

            AssignPreviewKeyDownEventToControls();
            ResumeLayout();
            Cursor = Cursors.Default;
            _iCurPoem = 0;


            _strPage = "نتایج جستجو برای \"" + phrase + "\" در آثار " + _db.GetPoet(PoetID)._Name + " صفحهٔ " + (Count < 1 ? "1 (موردی یافت نشد.)" : 1 + PageStart / Count + " (مورد " + (PageStart + 1) + " تا " + (PageStart + Count) + ")");
            StopPlayBack();
            _CurrentPoemAudio = null;
            OnPageChanged?.Invoke(_strPage, false, false, false, false, string.Empty, prePage, nextPage);

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
                    var OnlyScroll = OnlyScrollString == phrase;
                    var count = 0;
                    var scrolled = false;
                    foreach (Control ctl in Controls)
                    {
                        if (ctl is HighlightLabel label)
                        {
                            if (OnlyScroll)
                            {
                                if (label.Tag is GanjoorVerse verse && _db.IsVerseFaved(verse._PoemID, verse._Order))
                                {
                                    AutoScrollPosition = label.Location;
                                    return 0;
                                }
                            }
                            else
                            {
                                label.Keyword = phrase;
                                if (!string.IsNullOrEmpty(phrase))//this is here to prevent a bug caused by bad data (empty verses)
                                {
                                    var index = label.Text.IndexOf(phrase);
                                    if (index != -1)
                                    {
                                        if (!scrolled && scrollindex == count)
                                        {
                                            AutoScrollPosition = new Point(-AutoScrollPosition.X + label.Left, -AutoScrollPosition.Y + label.Top);
                                            scrolled = true;
                                        }
                                        count++;
                                        while (index + 1 != label.Text.Length && (index = label.Text.IndexOf(phrase, index + 1)) != -1)
                                        {
                                            count++;
                                        }
                                    }
                                }
                            }
                        }
                        else
                            if (ctl is TextBox && ctl.Text == phrase)
                        {
                            ctl.BackColor = Color.Red;
                            AutoScrollPosition = ctl.Location;
                            return 0;
                        }
                    }
                    if (count == 0)
                        AutoScrollPosition = new Point();
                    Invalidate();

                    return count;
                }
            }

            if (_iCurCat == 0 || _iCurPoem == 0)
            {
                var count = 0;
                var scrolled = false;
                foreach (Control ctl in Controls)
                    if (ctl is LinkLabel label)
                    {
                        if (!string.IsNullOrEmpty(phrase))
                        {
                            var index = label.Text.IndexOf(phrase);
                            if (index != -1)
                            {
                                label.LinkColor = Settings.Default.HighlightColor;
                                if (!scrolled && scrollindex == count)
                                {
                                    AutoScrollPosition = new Point(-AutoScrollPosition.X + label.Left, -AutoScrollPosition.Y + label.Top);
                                    scrolled = true;
                                }
                                count++;
                                while (index + 1 != label.Text.Length && (index = label.Text.IndexOf(phrase, index + 1)) != -1)
                                {
                                    count++;
                                }
                            }
                            else
                                label.LinkColor = Settings.Default.LinkColor;
                        }
                        else
                            label.LinkColor = Settings.Default.LinkColor;

                    }
                return count;
            }
            return 0;
        }
        #endregion

        #region Simple Copy
        public void CopyText(bool ganjoorHtmlFormat)
        {
            var txt = "";
            GanjoorVerse buffer = null;
            foreach (Control ctl in Controls)
                if (ctl is Label && !(ctl is LinkLabel))
                {
                    if (ganjoorHtmlFormat)
                    {
                        if (ctl.Tag is GanjoorVerse verse)
                        {
                            if (buffer != null && verse._Position != VersePosition.CenteredVerse2)
                            {
                                txt += "<div class=\"b2\"><p>" + buffer._Text.Trim() + "</p></div>\r\n";
                                buffer = null;
                            }
                            switch (verse._Position)
                            {
                                case VersePosition.Right:
                                    txt += "<div class=\"b\"><div class=\"m1\"><p>" + verse._Text.Trim();
                                    break;
                                case VersePosition.Left:
                                    txt += "</p></div><div class=\"m2\"><p>" + verse._Text.Trim() + "</p></div></div>\r\n";
                                    break;
                                case VersePosition.CenteredVerse1:
                                    buffer = verse;
                                    break;
                                case VersePosition.CenteredVerse2:
                                    if (buffer != null)
                                    {
                                        txt += "<div class=\"b2\"><p>" + buffer._Text + "</p><p>" + verse._Text.Trim() + "</p></div>\r\n";
                                        buffer = null;
                                    }
                                    else
                                    {
                                        MessageBox.Show("CenteredVerse2 without CenteredVerse1");
                                    }
                                    break;
                                case VersePosition.Single:
                                    txt += "<div class=\"l\"><p>" + verse._Text.Trim() + "</p></div>\r\n";
                                    break;
                                case VersePosition.Paragraph:
                                    txt += "<div class=\"n\"><p>" + verse._Text.Trim() + "</p></div>\r\n";
                                    break;
                                case VersePosition.Comment:
                                    txt += "<div class=\"c\"><p>" + verse._Text.Trim() + "</p></div>\r\n";
                                    break;

                            }
                        }
                    }
                    else
                    {
                        txt += ctl.Text + "\r\n";
                    }
                }
            if (buffer != null)
            {
                txt += "<div class=\"b2\"><p>" + buffer._Text + "</p></div>\r\n";
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
                var i = 0;
                while (i < Controls.Count)
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
            SuspendLayout();
            VerticalScroll.Value = 0; HorizontalScroll.Value = 0;
            ClearControls();
            var CountCopy = Count;
            GanjoorFavPage prePage = null, nextPage = null;
            using (var poemsList = _db.GetFavs(PageStart, Count + 1))
            {
                var HasMore = Count > 0 && poemsList.Rows.Count == Count + 1;
                if (poemsList.Rows.Count <= Count)
                    Count = poemsList.Rows.Count;
                else
                    Count = HasMore ? Count : poemsList.Rows.Count - 1;
                var catsTop = DistanceFromTop;
                for (var i = 0; i < Count; i++)
                {
                    var poem = _db.GetPoem(Convert.ToInt32(poemsList.Rows[i].ItemArray[0]));
                    if (poem != null)//this might happen when a poet has been deleted from database
                    {
                        if (Settings.Default.ScrollToFavedVerse)
                            poem._HighlightText = OnlyScrollString;
                        int lastDistanceFromRight;
                        ShowCategory(_db.GetCategory(poem._CatID), ref catsTop, out lastDistanceFromRight, false, false);
                        lastDistanceFromRight += DistanceFromRightStep;
                        var lblPoem = new LinkLabel();
                        lblPoem.Tag = poem;
                        lblPoem.AutoSize = true;
                        lblPoem.Text = poem._Title;
                        lblPoem.Location = new Point(lastDistanceFromRight, catsTop);
                        lblPoem.LinkBehavior = LinkBehavior.HoverUnderline;
                        lblPoem.BackColor = Color.Transparent;
                        lblPoem.LinkColor = Settings.Default.LinkColor;
                        lblPoem.ForeColor = lblPoem.LinkColor;
                        lblPoem.LinkClicked += lblPoem_Click;
                        Controls.Add(lblPoem);

                        catsTop += DistanceBetweenLines;
                        lastDistanceFromRight += DistanceFromRightStep;

                        var lblVerse = new HighlightLabel();
                        lblVerse.AutoSize = true;
                        lblVerse.Tag = poem;
                        lblVerse.TabStop = true;
                        lblVerse.Text = _db.GetPreferablyAFavVerse(poem._ID)._Text;
                        lblVerse.Location = new Point(lastDistanceFromRight, catsTop);
                        lblVerse.BackColor = Color.Transparent;
                        lblVerse.KeyDown += lblVerseFav_KeyDown;

                        Controls.Add(lblVerse);
                        catsTop += 2 * DistanceBetweenLines;
                    }

                }

                if (PageStart > 0)
                {
                    var lblPrevPage = new LinkLabel();
                    prePage = new GanjoorFavPage(PageStart - CountCopy, CountCopy);
                    lblPrevPage.Tag = prePage;
                    lblPrevPage.AutoSize = true;
                    lblPrevPage.Text = "صفحهٔ قبل";
                    lblPrevPage.Location = new Point(200, catsTop);
                    lblPrevPage.LinkBehavior = LinkBehavior.HoverUnderline;
                    lblPrevPage.BackColor = Color.Transparent;
                    lblPrevPage.LinkColor = Settings.Default.LinkColor;
                    lblPrevPage.ForeColor = lblPrevPage.LinkColor;
                    lblPrevPage.LinkClicked += lblNextPage_Click;
                    Controls.Add(lblPrevPage);

                }

                if (HasMore)
                {
                    var lblNextPage = new LinkLabel();
                    nextPage = new GanjoorFavPage(PageStart + Count, Count);
                    lblNextPage.Tag = nextPage;
                    lblNextPage.AutoSize = true;
                    lblNextPage.Text = "صفحهٔ بعد";
                    lblNextPage.Location = new Point(Width - 200, catsTop);
                    lblNextPage.LinkBehavior = LinkBehavior.HoverUnderline;
                    lblNextPage.BackColor = Color.Transparent;
                    lblNextPage.LinkColor = Settings.Default.LinkColor;
                    lblNextPage.ForeColor = lblNextPage.LinkColor;
                    lblNextPage.LinkClicked += lblNextPage_Click;
                    Controls.Add(lblNextPage);
                    catsTop += DistanceBetweenLines;
                }


                //یک بر چسب اضافی برای اضافه شدن فضای پایین فرم
                var lblDummy = new Label();
                lblDummy.Text = " ";
                lblDummy.Location = new Point(200, catsTop);
                lblDummy.BackColor = Color.Transparent;
                Controls.Add(lblDummy);

                _iCurCat = 0;
                _iCurSearchStart = PageStart;
                _iCurSearchPageCount = Count;
                _FavsPage = true;
                _strLastPhrase = string.Empty;


            }


            //کلک راست به چپ!
            foreach (Control ctl in Controls)
                ctl.Location = new Point(Width - ctl.Right, ctl.Location.Y);

            AssignPreviewKeyDownEventToControls();
            ResumeLayout();
            Cursor = Cursors.Default;


            _strPage = "نشانه‌ها - صفحهٔ " + (Count < 1 ? "1 (موردی یافت نشد.)" : 1 + PageStart / Count + " (مورد " + (PageStart + 1) + " تا " + (PageStart + Count) + ")");
            StopPlayBack();
            _CurrentPoemAudio = null;
            OnPageChanged?.Invoke(_strPage, false, false, false, true, string.Empty, prePage, nextPage);

            if (Count < 1)
                MessageBox.Show("موردی یافت نشد.", "نشانه‌ها", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            return true;
        }

        private void lblVerseFav_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShowPoem((GanjoorPoem)((Control)sender).Tag, true);
            }
        }

        #endregion

        #region Paging
        public void ProcessPagingTag(object tag)
        {
            if (tag is GanjoorSearchPage searchPage)
            {
                var g = searchPage;
                ShowSearchResults(g._SearchPhrase, g._PageStart, g._MaxItemsCount, g._PoetID, g._SearchType);
            }
            else
                if (tag is GanjoorFavPage page)
            {
                ShowFavs(page._PageStart, page._MaxItemsCount);
            }
            else
                    if (tag is GanjoorCat cat)
            {
                ShowCategory(cat, true);
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
                var lstPoets = new List<string>();
                lstPoets.Add("همه");
                foreach (var poet in _db.Poets)
                    lstPoets.Add(poet._Name);
                return lstPoets.ToArray();
            }
        }
        public int GetPoetOrder(int ID)
        {
            var lstPoets = _db.Poets;
            for (var i = 0; i < lstPoets.Count; i++)
                if (ID == lstPoets[i]._ID)
                    return i + 1;
            return 0;

        }
        public int GetPoetID(int Order)
        {
            if (Order == 0) return 0;
            return _db.Poets[Order - 1]._ID;
        }
        public int GetCurrentPoetID()
        {
            var cat = _db.GetCategory(_iCurCat);
            if (cat != null)
                return cat._PoetID;
            return 0;
        }
        #endregion

        #region Random Poem
        public void ShowRandomPoem()
        {
            if (_LastRandomCatsString != Settings.Default.RandomCats)
            {
                _LastRandomCatsString = Settings.Default.RandomCats;

                var randomCatStrs = _LastRandomCatsString.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var randomCats = new int[randomCatStrs.Length];
                try
                {
                    for (var i = 0; i < randomCatStrs.Length; i++)
                        randomCats[i] = Convert.ToInt32(randomCatStrs[i]);
                }
                catch
                {
                    randomCats = new[] { 0 };
                }

                _LastRandomCatList = new List<int>();
                foreach (var RandomCatID in randomCats)
                {
                    if (RandomCatID != 0)
                    {
                        if (_db.HasAnyPoem(RandomCatID))
                            _LastRandomCatList.Add(RandomCatID);
                        foreach (var CatID in _db.GetAllSubCats(RandomCatID))
                        {
                            if (_db.HasAnyPoem(CatID))
                                _LastRandomCatList.Add(CatID);
                        }
                    }
                }
            }
            var PoemID = _db.GetRandomPoem(_LastRandomCatList);
            if (PoemID == -1)
            {
                MessageBox.Show("خطا در یافتن شعر تصادفی!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                return;
            }
            var poem = _db.GetPoem(PoemID);
            if (poem != null)
            {
                if (_LastRandomCatList.Count == 0 || _LastRandomCatList.IndexOf(poem._CatID) != -1)
                    ShowPoem(poem, true);
                else
                    ShowRandomPoem();//this might happen, because _db.GetRandomPoem returns a poem between MIN-MAX poem id of cat and this might be something which is not from CatIDs we wanted 
            }
            else
                ShowRandomPoem();//not any random id exists, so repeat until finding a valid id
        }
        private string _LastRandomCatsString = "0";
        private static List<int> _LastRandomCatList = new List<int>();
        #endregion

        #region Import Db / Import+Export Favs
        public void ImportDb(string fileName)
        {
            var cnflts = _db.GetConflictingPoets(fileName);
            if (cnflts.Length > 0) {
                using var dlg = new ConflictingPoets(cnflts);
                if (dlg.ShowDialog(Parent) == DialogResult.Cancel)
                    return;
                cnflts = dlg.DeleteList;
                foreach (var delPoet in cnflts)
                    _db.DeletePoet(delPoet._ID);
            }
            var catCnlts = _db.GetConflictingCats(fileName);
            if (catCnlts.Length > 0) {
                using var dlg = new ConflictingCats(catCnlts);
                if (dlg.ShowDialog(Parent) == DialogResult.Cancel)
                    return;
                catCnlts = dlg.DeleteList;
                foreach (var delCat in catCnlts)
                    _db.DeleteCat(delCat._ID);
            }
            var missingPoets = _db.GetCategoriesWithMissingPoet(fileName);
            if (missingPoets.Length > 0)
            {
                if (MessageBox.Show(
                    "مجموعه‌ای که تلاش می‌کنید آن را اضافه کنید شامل بخشهایی از آثار شاعرانی است که آنها را در گنجور رومیزی خود ندارید."
                    + Environment.NewLine
                    + "گنجور رومیزی می‌تواند با ایجاد شاعران جدید تلاش کند این مشکل را حل کند، اما این همیشه حل کنندهٔ این مشکل نیست."
                    + Environment.NewLine
                    + "آیا می‌خواهید از اضافه کردن این مجموعه صرف نظر کنید؟"
                    , "هشدار"
                    , MessageBoxButtons.YesNo
                    , MessageBoxIcon.Warning
                    , MessageBoxDefaultButton.Button1
                    , MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading
                    ) == DialogResult.Yes
                    )
                    return;
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
            int ignoredFavs, errFavs;
            var importedFavs = _db.ImportMixFavs(fileName, out ignoredFavs, out errFavs);
            MessageBox.Show(String.Format("{0} نشانه اضافه شد و از افزودن {1} نشانه به دلیل تکراری بودن و {2} به دلیل عدم وجود داده‌ها یا شاعر متناظر در داده‌های برنامه صرف نظر شد.", importedFavs, ignoredFavs, errFavs), "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            ShowFavs(0, Settings.Default.FavItemsInPage);
        }
        #endregion

        #region Scroll Using Arrow Keys
        private void GanjoorViewer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.C)
                {
                    var idx = FocusedVerseIndex;
                    if (idx == -1)
                        return;
                    if (!(Controls[idx] is HighlightLabel))
                        return;
                    e.IsInputKey = true;
                    var clipText = "";
                    if (e.Shift)
                    {
                        clipText = Clipboard.GetText();
                        if (clipText == null)
                            clipText = "";
                        else
                            clipText += Environment.NewLine;
                    }
                    clipText += Controls[idx].Text;
                    Clipboard.SetText(clipText);
                    return;
                }
            }
            if (e.Control || e.Alt)
                return;
            var isInputKey = true;
            switch (e.KeyCode)
            {
                case Keys.Down:
                    {
                        var idx = FocusedVerseIndex;
                        if (idx == -1)
                        {
                            SetVerticalScrollValue(VerticalScroll.Value + ScrollingSpeed * VerticalScroll.SmallChange);
                        }
                        else
                        {
                            if (idx < Controls.Count && Controls[idx + 1] is HighlightLabel)
                                Controls[idx + 1].Focus();
                            else
                                SetVerticalScrollValue(VerticalScroll.Value + ScrollingSpeed * VerticalScroll.SmallChange);
                        }
                    }
                    break;
                case Keys.Up:
                    {
                        var idx = FocusedVerseIndex;
                        if (idx == -1)
                        {
                            SetVerticalScrollValue(VerticalScroll.Value - ScrollingSpeed * VerticalScroll.SmallChange);
                        }
                        else
                        {
                            if (idx > 0 && Controls[idx - 1] is HighlightLabel)
                                Controls[idx - 1].Focus();
                            else
                                SetVerticalScrollValue(VerticalScroll.Value - ScrollingSpeed * VerticalScroll.SmallChange);
                        }
                    }
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
        private int FocusedVerseIndex
        {
            get
            {
                for (var idx = 0; idx < Controls.Count; idx++)
                    if (Controls[idx].Focused)
                        if (Controls[idx] is HighlightLabel)
                            return idx;
                        else
                            return -1;
                return -1;
            }
        }
        private int ScrollingSpeed = 1;
        private void SetVerticalScrollValue(int newValue)
        {
            newValue = Math.Max(VerticalScroll.Minimum, Math.Min(newValue, VerticalScroll.Maximum));
            var oldValue = VerticalScroll.Value;
            var i = 0;
            while (VerticalScroll.Value == oldValue && i++ < 3)//!
            {
                VerticalScroll.Value = newValue;
            }
        }
        private void SetHorizontalScrollValue(int newValue)
        {
            newValue = Math.Max(HorizontalScroll.Minimum, Math.Min(newValue, HorizontalScroll.Maximum));
            var oldValue = HorizontalScroll.Value;
            var i = 0;
            while (HorizontalScroll.Value == oldValue && i++ < 3)//!
            {
                HorizontalScroll.Value = newValue;
            }
        }
        private void AssignPreviewKeyDownEventToControls()
        {
            foreach (Control ctl in Controls)
                ctl.PreviewKeyDown += GanjoorViewer_PreviewKeyDown;
        }
        #endregion

        #region Edit Mode
        [DefaultValue(false)]
        public bool EditMode { get; set; }
        public bool IsInPoetRootPage
        {
            get {
                if (_iCurCat > 0)
                {
                    var cat = _db.GetCategory(_iCurCat);
                    return cat._ParentID == 0;
                }

                return true;
            }
        }
        public bool NewPoet(string PoetName) {
            if (_db != null)
            {
                var poetID = _db.NewPoet(PoetName);
                if (-1 != poetID)
                {
                    ShowCategory(_db.GetCategory(_db.GetPoet(poetID)._CatID), true);
                    return true;
                }

                return false;
            }

            return false;
        }
        public bool EditPoet(string NewName)
        {
            var poet = _db.GetPoetForCat(_iCurCat);
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
            var poet = _db.GetPoetForCat(_iCurCat);
            if (null == poet)
                return false;
            if (_db.ModifyPoetBio(poet._ID, NewBio))
            {
                ShowCategory(_db.GetCategory(_iCurCat), false);
                return true;
            }
            return false;
        }

        public bool ImportDbPoetBioText(string fileName)
        {
            if (_db.ImportDbPoetBioText(fileName))
            {
                ShowCategory(_db.GetCategory(_iCurCat), false);
                return true;
            }
            return false;
        }

        public bool NewCat(string CatName)
        {
            var cat = _db.GetCategory(_iCurCat);
            if (null != cat)
            {
                cat = _db.CreateNewCategory(CatName, cat._ID, cat._PoetID);
                if (cat != null)
                {
                    ShowCategory(cat, true);
                    return true;
                }

                return false;
            }

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
            var cat = _db.GetCategory(_iCurCat);
            if (null != cat)
            {
                var poem = _db.CreateNewPoem(PoemName, _iCurCat);
                if (poem != null)
                {
                    ShowPoem(poem, true);
                    return true;
                }

                return false;
            }

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
        public bool NewParagraph(string text = "")
        {
            return NewLine(VersePosition.Paragraph, text);
        }
        public bool InsertVerses(string[] verses, bool IsClassicPoem, bool IgnoreEmptyLines, bool IgnoreShortLines, int minLength)
        {
            Save();
            var LinePosition = GetCurrentLine();
            var Position = IsClassicPoem ? VersePosition.Right : VersePosition.Single;
            _db.BeginBatchOperation();
            foreach (var verse in verses)
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
            var LinePosition = GetCurrentLine();
            var Position = VersePosition.Right;
            var numPassed = 0;
            _db.BeginBatchOperation();
            foreach (var verse in verses)
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
                            Position = numPassed == LineCount ? VersePosition.CenteredVerse1 : VersePosition.Right;
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
        private bool NewLine(VersePosition Position, string text = "")
        {
            Save();
            var LinePosition = GetCurrentLine();
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

                        var secondVerse = _db.CreateNewVerse(_iCurPoem, firstVerse._Order, VersePosition.CenteredVerse2);
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

                        var secondVerse = _db.CreateNewVerse(_iCurPoem, firstVerse._Order, VersePosition.Left);
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
            foreach (Control ctl in Controls)
                if (ctl is TextBox box)
                    if ((box.Tag as GanjoorVerse)._Order == firstVerse._Order)
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            box.Text = text;
                            DRY_ForceSaveVerse(box);
                        }
                        box.Focus();
                        break;
                    }
            return true;

        }
        public int GetCurrentLine()
        {
            var LinePosition = 0;
            var catsTop = 0;
            foreach (Control ctl in Controls)
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
                        var verseBefore = ctl.Tag as GanjoorVerse;
                        if (verseBefore._Position == VersePosition.Right)
                        {
                            foreach (Control ctlO in Controls)
                            {
                                if (ctlO is TextBox)
                                {
                                    if ((ctlO.Tag as GanjoorVerse)._Order == verseBefore._Order + 1)
                                    {
                                        verseBefore = ctlO.Tag as GanjoorVerse;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                            if (verseBefore._Position == VersePosition.CenteredVerse1)
                        {
                            foreach (Control ctlO in Controls)
                            {
                                if (ctlO is TextBox)
                                {
                                    if ((ctlO.Tag as GanjoorVerse)._Order == verseBefore._Order + 1)
                                    {
                                        if ((ctlO.Tag as GanjoorVerse)._Position == VersePosition.CenteredVerse2)
                                        {
                                            verseBefore = ctlO.Tag as GanjoorVerse;
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
            foreach (Control ctl in Controls)
                if (ctl is TextBox box)
                {
                    DRY_SaveVerse(box);
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
                var verse = verseBox.Tag as GanjoorVerse;
                _db.SetVerseText(verse._PoemID, verse._Order, verseBox.Text);
                verseBox.ClearUndo();
                verseBox.BackColor = Color.White;
            }
        }
        private static void DRY_ForceSaveVerse(TextBox verseBox)
        {
            var verse = verseBox.Tag as GanjoorVerse;
            _db.SetVerseText(verse._PoemID, verse._Order, verseBox.Text);
            verseBox.BackColor = Color.White;
        }
        private void lblVerse_TextChanged(object sender, EventArgs e)
        {
            var verseBox = sender as TextBox;
            verseBox.BackColor = Color.LightYellow;
        }
        public bool DeleteAllLines()
        {
            var Verses = _db.GetVerses(_iCurPoem);
            var versesToDelete = new List<int>();
            foreach (var Verse in Verses)
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
            var versesToDelete = new List<int>();
            foreach (Control ctl in Controls)
            {
                if (ctl.Focused)
                {
                    if (ctl is TextBox)
                    {
                        var verseBefore = ctl.Tag as GanjoorVerse;
                        versesToDelete.Add(verseBefore._Order);
                        if (verseBefore._Position == VersePosition.Left)
                        {
                            versesToDelete.Add(verseBefore._Order - 1);
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
                            foreach (Control ctlO in Controls)
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
            var MinVerseIndex = versesToDelete[0];
            if (_db.DeleteVerses(_iCurPoem, versesToDelete))
            {
                ShowPoem(_db.GetPoem(_iCurPoem), false);
                Control lastCtl = null;
                foreach (Control ctl in Controls)
                    if (ctl is TextBox) {
                        if ((ctl.Tag as GanjoorVerse)._Order == MinVerseIndex)
                        {
                            lastCtl = ctl;
                            break;
                        }

                        lastCtl = ctl;
                    }

                lastCtl?.Focus();


                return true;
            }
            return false;

        }
        public bool DeletePoet()
        {
            var poet = _db.GetPoetForCat(_iCurCat);
            if (null == poet)
            {
                //Somethig that should never happen, happened!
                //So try to fix it hoping not just adding another mess:
                var cat = _db.GetCategory(_iCurCat);
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
                foreach (var p in _db.Poets)
                    if (p._Name == cat._Text || p._CatID == cat._ID)
                    {
                        correspondingPoet = p;
                        break;
                    }

                if (correspondingPoet != null)
                {
                    _db.DeletePoet(correspondingPoet._ID);
                }

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
            var cat = _db.GetCategory(_iCurCat);
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
            var poet = _db.GetPoetForCat(_iCurCat);
            if (null == poet)
                return false;
            return _db.ExportPoet(fileName, poet._ID);
        }
        public void GetIDs(out int PoetID, out int MinCatID, out int MinPoemID)
        {
            var poet = _db.GetPoetForCat(_iCurCat);
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
            if (_db.ChangePoemCategory(_iCurPoem, CatID))
            {
                ShowPoem(_db.GetPoem(_iCurPoem), false);
            }
        }
        public bool ReplaceText(string strFindText, string strReplaceText)
        {
            foreach (Control ctl in Controls)
                if (ctl is TextBox box)
                {
                    var txt = box;
                    txt.Text = txt.Text.Replace(strFindText, strReplaceText);
                    DRY_ForceSaveVerse(txt);
                }
            return true;
        }
        public bool RestructureVerses(int LineCount, bool FullLine, int nStartOrder, bool OnlyNormalVerses)
        {
            Save();
            var Position = VersePosition.Right;
            var numPassed = 0;
            _db.BeginBatchOperation();
            foreach (Control ctl in Controls)
            {
                if (!(ctl is TextBox txt))
                    continue;
                if (!(txt.Tag is GanjoorVerse verse))
                    continue;
                if (verse._Order < nStartOrder)
                    continue;
                var ChangeIt = true;
                if (OnlyNormalVerses)
                    if (verse._Position != VersePosition.Right && verse._Position != VersePosition.Left)
                        ChangeIt = false;

                if (ChangeIt)
                    _db.SetVersePosition(_iCurPoem, verse._Order, Position);
                switch (Position)
                {
                    case VersePosition.Right:
                        {
                            Position = VersePosition.Left;
                        }
                        break;
                    case VersePosition.Left:
                        {
                            numPassed++;
                            Position = numPassed == LineCount ? VersePosition.CenteredVerse1 : VersePosition.Right;
                        }
                        break;
                    case VersePosition.CenteredVerse1:
                        {
                            numPassed = 0;
                            Position = FullLine ? VersePosition.CenteredVerse2 : VersePosition.Right;

                        }
                        break;
                    case VersePosition.CenteredVerse2:
                        {
                            Position = VersePosition.Right;
                        }
                        break;
                }
            }
            _db.CommitBatchOperation();
            ShowPoem(_db.GetPoem(_iCurPoem), false);
            return true;
        }

        public bool ConvertLineToBandLine()
        {
            var nFocus = -1;
            foreach (Control ctl in Controls)
            {
                if (ctl.Focused)
                {
                    if (ctl is TextBox)
                    {
                        var verseBefore = ctl.Tag as GanjoorVerse;
                        nFocus = verseBefore._Order;
                        if (verseBefore._Position == VersePosition.Right)
                        {
                            _db.SetVersePosition(verseBefore._PoemID, verseBefore._Order, VersePosition.CenteredVerse1);
                            _db.SetVersePosition(verseBefore._PoemID, verseBefore._Order + 1, VersePosition.CenteredVerse2);
                        }
                        else
                            if (verseBefore._Position == VersePosition.Left)
                        {
                            _db.SetVersePosition(verseBefore._PoemID, verseBefore._Order - 1, VersePosition.CenteredVerse1);
                            _db.SetVersePosition(verseBefore._PoemID, verseBefore._Order, VersePosition.CenteredVerse2);
                        }
                    }
                    break;
                }
            }
            if (nFocus == -1)
                return false;
            ShowPoem(_db.GetPoem(_iCurPoem), false);
            foreach (Control ctl in Controls)
                if (ctl is TextBox)
                {
                    if ((ctl.Tag as GanjoorVerse)._Order == nFocus)
                    {
                        ctl.Focus();
                        break;
                    }
                }
            return true;
        }


        public bool ConvertVerseToBandVerse()
        {
            var nFocus = -1;
            foreach (Control ctl in Controls)
            {
                if (ctl.Focused)
                {
                    if (ctl is TextBox)
                    {
                        var verseBefore = ctl.Tag as GanjoorVerse;
                        nFocus = verseBefore._Order;
                        _db.SetVersePosition(verseBefore._PoemID, verseBefore._Order, VersePosition.CenteredVerse1);
                        RestructureVerses(-1, false, nFocus + 1, true);
                    }
                    break;
                }
            }
            if (nFocus == -1)
                return false;
            ShowPoem(_db.GetPoem(_iCurPoem), false);
            foreach (Control ctl in Controls)
                if (ctl is TextBox)
                {
                    if ((ctl.Tag as GanjoorVerse)._Order == nFocus)
                    {
                        ctl.Focus();
                        break;
                    }
                }
            return true;
        }

        public bool ConvertVerseToPara()
        {
            var nFocus = -1;
            foreach (Control ctl in Controls)
            {
                if (ctl.Focused)
                {
                    if (ctl is TextBox)
                    {
                        var verseBefore = ctl.Tag as GanjoorVerse;
                        nFocus = verseBefore._Order;
                        _db.SetVersePosition(verseBefore._PoemID, verseBefore._Order, VersePosition.Paragraph);
                        RestructureVerses(-1, false, nFocus + 1, true);
                    }
                    break;
                }
            }
            if (nFocus == -1)
                return false;
            ShowPoem(_db.GetPoem(_iCurPoem), false);
            foreach (Control ctl in Controls)
                if (ctl is TextBox)
                {
                    if ((ctl.Tag as GanjoorVerse)._Order == nFocus)
                    {
                        ctl.Focus();
                        break;
                    }
                }
            return true;

        }

        public bool ConvertVerseTo(VersePosition versePosition)
        {
            var nFocus = -1;
            foreach (Control ctl in Controls)
            {
                if (ctl.Focused)
                {
                    if (ctl is TextBox)
                    {
                        var verseBefore = ctl.Tag as GanjoorVerse;
                        nFocus = verseBefore._Order;
                        _db.SetVersePosition(verseBefore._PoemID, verseBefore._Order, versePosition);
                        RestructureVerses(-1, false, nFocus + 1, true);
                    }
                    break;
                }
            }
            if (nFocus == -1)
                return false;
            ShowPoem(_db.GetPoem(_iCurPoem), false);
            foreach (Control ctl in Controls)
                if (ctl is TextBox)
                {
                    if ((ctl.Tag as GanjoorVerse)._Order == nFocus)
                    {
                        ctl.Focus();
                        break;
                    }
                }
            return true;

        }



        public bool ConvertToToEnd(VersePosition newPosition)
        {
            var nFocus = -1;
            var bStartFound = false;
            foreach (Control ctl in Controls)
            {
                if (ctl.Focused || bStartFound)
                {
                    if (ctl is TextBox)
                    {
                        bStartFound = true;
                        var verseBefore = ctl.Tag as GanjoorVerse;
                        nFocus = verseBefore._Order;
                        _db.SetVersePosition(verseBefore._PoemID, verseBefore._Order, newPosition);

                        if (newPosition == VersePosition.Right)
                            newPosition = VersePosition.Left;
                        else if (newPosition == VersePosition.Left)
                            newPosition = VersePosition.Right;


                    }
                }
            }
            if (nFocus == -1)
                return false;

            RestructureVerses(-1, false, nFocus + 1, true);

            ShowPoem(_db.GetPoem(_iCurPoem), false);
            foreach (Control ctl in Controls)
                if (ctl is TextBox)
                {
                    if ((ctl.Tag as GanjoorVerse)._Order == nFocus)
                    {
                        ctl.Focus();
                        break;
                    }
                }
            return true;

        }

        public bool AppendFirstVerseToTileAndDeleteIt()
        {
            var verses = _db.GetVerses(_iCurPoem);
            if (verses.Count > 0)
            {
                var firstVerseText = verses[0]._Text;
                var poem = _db.GetPoem(_iCurPoem);
                _db.SetPoemTitle(_iCurPoem, string.IsNullOrEmpty(poem._Title) ? firstVerseText : $"{poem._Title} - {firstVerseText}");
                _db.DeleteVerses(_iCurPoem, new List<int>(new[] { verses[0]._Order }));
                RestructureVerses(-1, true, -1, false);
                Save();
                return true;
            }
            return false;
        }

        public bool BreakParagraph()
        {
            foreach (Control ctl in Controls)
            {
                if (ctl.Focused)
                {
                    if (ctl is TextBox box)
                    {
                        var para = box.Tag as GanjoorVerse;

                        if (para._Position == VersePosition.Paragraph)
                        {
                            var textBox = box;
                            var nStart = textBox.SelectionStart;
                            if (nStart >= 0)
                            {
                                var startText = textBox.Text.Substring(0, nStart).Trim();
                                var endText = textBox.Text.Substring(nStart).Trim();
                                textBox.Text = startText;
                                DRY_ForceSaveVerse(textBox);

                                return NewParagraph(endText);
                            }
                            _db.LastError = "مکان‌نما در جای درستی نیست.";
                            return false;
                        }


                    }
                    break;
                }
            }
            _db.LastError = "این قابلیت فقط روی پاراگرافها کار می‌کند.";
            return false;

        }


        public bool ConvertLeftToRightLine()
        {
            var nFocus = -1;
            foreach (Control ctl in Controls)
            {
                if (ctl.Focused)
                {
                    if (ctl is TextBox)
                    {
                        var verseBefore = ctl.Tag as GanjoorVerse;
                        nFocus = verseBefore._Order;
                        if (verseBefore._Position == VersePosition.Right)
                        {
                            //_db.SetVersePosition(verseBefore._PoemID, verseBefore._Order +  1, VersePosition.Right);
                            RestructureVerses(-1, false, verseBefore._Order + 1, true);
                        }
                        else
                            if (verseBefore._Position == VersePosition.Left)
                        {
                            //_db.SetVersePosition(verseBefore._PoemID, verseBefore._Order, VersePosition.Right);
                            RestructureVerses(-1, false, verseBefore._Order, true);
                        }
                    }
                    break;
                }
            }
            if (nFocus == -1)
                return false;
            ShowPoem(_db.GetPoem(_iCurPoem), false);
            foreach (Control ctl in Controls)
                if (ctl is TextBox)
                {
                    if ((ctl.Tag as GanjoorVerse)._Order == nFocus)
                    {
                        ctl.Focus();
                        break;
                    }
                }
            return true;
        }

        public bool CurrectCurrentCatVerse()
        {
            _db.BeginBatchOperation();
            foreach (var Poem in _db.GetPoems(_iCurCat))
            {
                var verses = _db.GetVerses(Poem._ID);
                var vPosition = VersePosition.Right;
                for (var i = 0; i < verses.Count; i++)
                {
                    if (
                        verses[i]._Position == VersePosition.Left
                        ||
                        verses[i]._Position == VersePosition.Right
                      )
                    {
                        _db.SetVersePosition(Poem._ID, verses[i]._Order, vPosition);
                        vPosition = vPosition == VersePosition.Right ? VersePosition.Left : VersePosition.Right;
                    }
                }
            }
            _db.CommitBatchOperation();
            return true;
        }



        #endregion

        #region Audio

        #region Variables
        private PoemAudioPlayer _PoemAudioPlayer;
        private Timer _PlaybackTimer;
        private DateTime _PlaybackPause;
        private int _CurAudioVerseOrder;
        private int _SyncOrder;
        private int _ControlStartFrom;
        private PoemAudio _CurrentPoemAudio;
        #endregion
        #region Events
        public event EventHandler PlaybackStarted;
        public event EventHandler PlaybackStopped;
        #endregion
        #region Methods
        public bool IsPlaying
        {
            get
            {
                return _PoemAudioPlayer is {IsPlaying: true};
            }
        }

        public bool IsInPauseState
        {
            get
            {
                return _PoemAudioPlayer is {IsInPauseState: true};
            }
        }

        public void Play(PoemAudio poemAudio)
        {
            CurrentPoemAudio = poemAudio;
            if (CurrentPoemAudio == null)
                return;
            if (_PoemAudioPlayer == null)
            {
                _PoemAudioPlayer = new PoemAudioPlayer();
                _PoemAudioPlayer.PlaybackStarted += _PoemAudioPlayer_PlaybackStarted;
                _PoemAudioPlayer.PlaybackStopped += _PoemAudioPlayer_PlaybackStopped;
            }
            _SyncOrder = -1;
            if (poemAudio.SyncArray != null)
            {
                _CurAudioVerseOrder = 0;
                _ControlStartFrom = 0;
                _PlaybackTimer = new Timer();
                _PlaybackTimer.Interval = 500;
                _PlaybackTimer.Tick += _PlaybackTimer_Tick;
                _PlaybackTimer.Start();
            }
            if (!_PoemAudioPlayer.BeginPlayback(poemAudio))
            {
                StopPlayBack();
                MessageBox.Show("خطایی در پخش فایل صوتی رخ داد. لطفا چک کنید فایل در مسیر تعیین شده قرار داشته باشد.");
            }
            if (poemAudio.SyncArray is {Length: > 0})
            {
                //رفع اشکال نسخه قدیمی NAudio
                var nLen = poemAudio.SyncArray.Length;
                if (poemAudio.SyncArray[nLen - 1].AudioMiliseconds > _PoemAudioPlayer.TotalTimeInMiliseconds)
                {
                    for (var i = 0; i < nLen; i++)
                    {
                        poemAudio.SyncArray[i].AudioMiliseconds = poemAudio.SyncArray[i].AudioMiliseconds / 2;
                    }
                }

                if (poemAudio.SyncArray[0].VerseOrder == -1)
                {
                    _PoemAudioPlayer.PositionInMiliseconds = poemAudio.SyncArray[0].AudioMiliseconds;
                    _SyncOrder++;
                }

            }

        }

        public void Pause()
        {
            _PlaybackPause = DateTime.Now;
            if (_PlaybackTimer != null)
            {
                _PlaybackTimer.Dispose();
                _PlaybackTimer = null;
            }

            _PoemAudioPlayer?.PausePlayBack();
        }

        public void Resume()
        {
            if (_PoemAudioPlayer == null)
                return;
            if (_PoemAudioPlayer.PoemAudio.SyncArray != null)
            {
                _PlaybackTimer = new Timer();
                _PlaybackTimer.Tick += _PlaybackTimer_Tick;
                _PlaybackTimer.Start();
            }

            _PoemAudioPlayer.ResumePlayBack();

        }

        public void StopPlayBack(bool bFromOutside = false)
        {
            if (_PoemAudioPlayer != null)
            {
                _PoemAudioPlayer.StopPlayBack();
                _PoemAudioPlayer.PoemAudio = null;
            }
            if (_PlaybackTimer != null)
            {
                _PlaybackTimer.Dispose();
                _PlaybackTimer = null;
            }
            if (bFromOutside)
            {
                if (_CurAudioVerseOrder > 0)
                {
                    HighlightVerse(_CurAudioVerseOrder, true, Color.Red, _ControlStartFrom);
                }
            }

            _CurAudioVerseOrder = 0;
            _ControlStartFrom = 0;
            _SyncOrder = -1;

        }
        #endregion

        #region Events
        private void _PlaybackTimer_Tick(object sender, EventArgs e)
        {
            if (_PoemAudioPlayer.PoemAudio == null)
            {
                _PlaybackTimer.Dispose();
                _PlaybackTimer = null;
                return;
            }
            var nNextSyncOrder = _SyncOrder + 1;
            while (nNextSyncOrder < 0)
                nNextSyncOrder++;
            if (nNextSyncOrder < _PoemAudioPlayer.PoemAudio.SyncArray.Length)
            {
                if (_PoemAudioPlayer.PositionInMiliseconds >= _PoemAudioPlayer.PoemAudio.SyncArray[nNextSyncOrder].AudioMiliseconds)
                {
                    if (_CurAudioVerseOrder > 0)
                    {
                        HighlightVerse(_CurAudioVerseOrder, true, Color.Red, _ControlStartFrom);
                        _ControlStartFrom++;
                    }
                    var vOrder = _PoemAudioPlayer.PoemAudio.SyncArray[nNextSyncOrder].VerseOrder + 1;
                    if (vOrder < 0)
                    {
                        _PoemAudioPlayer.StopPlayBack();
                        return;
                    }
                    if (vOrder > 0)
                    {
                        if (vOrder < _CurAudioVerseOrder)
                            _ControlStartFrom = 0;
                        _CurAudioVerseOrder = vOrder;
                        _ControlStartFrom = HighlightVerse(_CurAudioVerseOrder, false, Color.Red, _ControlStartFrom);
                    }
                    _SyncOrder = nNextSyncOrder;
                }
            }

        }



        private void _PoemAudioPlayer_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            // we want to be always on the GUI thread and be able to change GUI components
            Debug.Assert(!InvokeRequired, "PlaybackStopped on wrong thread");
            if (e.Exception != null)
            {
                MessageBox.Show(String.Format("Playback Stopped due to an error {0}", e.Exception.Message));
            }
            if (_PlaybackTimer != null)
            {
                _PlaybackTimer.Dispose();
                _PlaybackTimer = null;
                HighlightVerse(_CurAudioVerseOrder - 1, true, Color.Red, _ControlStartFrom);
            }

            PlaybackStopped?.Invoke(this, EventArgs.Empty);


        }

        private void _PoemAudioPlayer_PlaybackStarted(object sender, EventArgs e) {
            PlaybackStarted?.Invoke(sender, e);
        }

        #endregion

        public PoemAudio CurrentPoemAudio
        {
            get
            {
                return _CurrentPoemAudio;
            }
            set
            {
                _CurrentPoemAudio = value;
            }
        }

        public PoemAudio[] PoemAudioFiles
        {
            get
            {
                return _db.GetPoemAudioFiles(_iCurPoem);
            }
        }


        public int HighlightVerse(int nVerseOrder, bool dehighlight, Color clrHighlightColor, int nStartFrom)
        {
            var nCount = Controls.Count;
            for (var i = nStartFrom; i < nCount; i++)
            {
                var ctl = Controls[i];
                if (ctl is HighlightLabel)
                {
                    if (ctl.Tag is GanjoorVerse verse && verse._Order == nVerseOrder)
                    {
                        ScrollControlIntoView(ctl);
                        if (dehighlight)
                            ctl.ForeColor = ForeColor;
                        else
                            ctl.ForeColor = Color.Red;
                        return i;
                    }
                }
            }
            return 0;

        }

        #endregion

        #region Clear Controls
        private void ClearControls()
        {
            foreach (Control ctl in Controls)
                ctl.Dispose();
            Controls.Clear();
        }
        #endregion

    }
}
