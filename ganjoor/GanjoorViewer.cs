using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;


namespace ganjoor
{
    public partial class GanjoorViewer : UserControl
    {
        #region Constructor
        public GanjoorViewer()
        {
            InitializeComponent();

            ApplyUISettings();
            

            _iCurCat = _iCurPoem = 0;
            _strLastPhrase = "";
            _history = new Stack<GarnjoorBrowsingHistory>();

            if (!DesignMode)
            {
                _db = new DbBrowser();                
            }
           
        }
        #endregion

        #region Database Browser
        private DbBrowser _db;
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
        private static int DistanceBetweenLines = 20;
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
                    if (Properties.Settings.Default.WasShowingFavs)
                    {
                        ShowFavs(Properties.Settings.Default.LastSeachStart, Properties.Settings.Default.SearchPageItems, false);
                    }
                    else
                    if (Properties.Settings.Default.LastCat != 0)
                    {
                        if (Properties.Settings.Default.LastPoem != 0)
                        {
                            ShowPoem(_db.GetPoem(Properties.Settings.Default.LastPoem), false);
                        }
                        else
                        {
                            GanjoorCat cat = _db.GetCategory(Properties.Settings.Default.LastCat);
                            cat._StartPoem =  Properties.Settings.Default.LastCatStart;                            
                            ShowCategory(cat, false);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Properties.Settings.Default.LastSearchPhrase))
                        {
                            ShowSearchResults(Properties.Settings.Default.LastSearchPhrase, Properties.Settings.Default.LastSeachStart, Properties.Settings.Default.SearchPageItems, Properties.Settings.Default.LastSearchPoetID, false);
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
            
            Cursor = Cursors.WaitCursor; Application.DoEvents();
            this.SuspendLayout();
            this.VerticalScroll.Value = 0;this.HorizontalScroll.Value = 0;
            this.Controls.Clear();


            int catsTop = DistanceFromTop;
            int lastDistanceFromRight;
            ShowCategory(category, ref catsTop, out lastDistanceFromRight, true, keepTrack);

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
                lblCat.Click += new EventHandler(lblCat_Click);
                this.Controls.Add(lblCat);
            }

            int poemsDistanceFromRight = DistanceFromRight + lastDistanceFromRight;
            int poemsTop = catsTop + subcats.Count * DistanceBetweenLines;
            List<GanjoorPoem> poems = _db.GetPoems(category._ID);
            category._StartPoem = Math.Max(0, category._StartPoem);
            GanjoorCat preCat = category._StartPoem == 0 ? null : new GanjoorCat(category, category._StartPoem - Properties.Settings.Default.MaxPoemsInList);
            GanjoorCat nextCat = category._StartPoem + Properties.Settings.Default.MaxPoemsInList < poems.Count ? new GanjoorCat(category, category._StartPoem + Properties.Settings.Default.MaxPoemsInList) : null;            
            for (int i = category._StartPoem; i < Math.Min(poems.Count, category._StartPoem + Properties.Settings.Default.MaxPoemsInList); i++)
            {
                
                LinkLabel lblPoem = new LinkLabel();
                lblPoem.Tag = poems[i];
                lblPoem.AutoSize = true;
                lblPoem.Text = poems[i]._Title;
                List<GanjoorVerse> v = _db.GetVerses(poems[i]._ID, 1);
                if (v.Count > 0)
                    lblPoem.Text += " : " + v[0]._Text;
                lblPoem.Location = new Point(poemsDistanceFromRight, poemsTop + (i-category._StartPoem) * DistanceBetweenLines);
                lblPoem.LinkBehavior = LinkBehavior.HoverUnderline;
                lblPoem.BackColor = Color.Transparent;
                lblPoem.Click += new EventHandler(lblPoem_Click);
                this.Controls.Add(lblPoem);
            }

            //یک بر چسب اضافی برای اضافه شدن فضای پایین فرم
            Label lblDummy = new Label();
            lblDummy.Text = " ";
            int poemsCount = category._StartPoem + Properties.Settings.Default.MaxPoemsInList < poems.Count ? Properties.Settings.Default.MaxPoemsInList : poems.Count - category._StartPoem;
            lblDummy.Location = new Point(200, poemsTop + poemsCount * DistanceBetweenLines);
            lblDummy.BackColor = Color.Transparent;
            this.Controls.Add(lblDummy);


            //کلک راست به چپ!
            foreach (Control ctl in this.Controls)
                ctl.Location = new Point(this.Width - ctl.Right, ctl.Location.Y);


            this.ResumeLayout();
            Cursor = Cursors.Default;
            _strLastPhrase = null;
            if (null != OnPageChanged)
                OnPageChanged(_strPage, false, true, false, false, string.Empty, preCat, nextCat);
        }
        private void ShowCategory(GanjoorCat category, ref int catsTop, out int lastDistanceFromRight, bool highlightCat,bool keepTrack)
        {
            if(keepTrack)
                UpdateHistory();            
            lastDistanceFromRight = DistanceFromRight;


            _strPage = "";

            //اجداد این دسته
            List<GanjoorCat> ancestors = _db.GetParentCategories(category);

            for (int i = 0; i < ancestors.Count; i++)
            {
                _strPage += ancestors[i]._Text;
                if (0 != category._ID) _strPage += " -> ";

                LinkLabel lblCat = new LinkLabel();
                lblCat.Tag = ancestors[i];
                lblCat.AutoSize = true;
                lblCat.Text = ancestors[i]._Text;
                lastDistanceFromRight += 2 * DistanceFromRightStep;
                lblCat.Location = new Point(lastDistanceFromRight, catsTop + i * DistanceBetweenLines);
                lblCat.LinkBehavior = LinkBehavior.HoverUnderline;
                lblCat.BackColor = Color.Transparent;
                lblCat.Click += new EventHandler(lblCat_Click);
                this.Controls.Add(lblCat);
            }


            catsTop += (ancestors.Count * DistanceBetweenLines);
            //خود این دسته
            
            if (category._ID != 0)
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
                lblMe.Click += new EventHandler(lblCat_Click);
                this.Controls.Add(lblMe);

                catsTop += DistanceBetweenLines;
            }

            _FavsPage = false;            
            _iCurCat = category._ID;
            _iCurCatStart = category._StartPoem;
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
                fav.Image = Properties.Resources.fav;
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
            return ShowPoem(poem, keepTrack, poem._HighlightText);
        }
        private int ShowPoem(GanjoorPoem poem, bool keepTrack, string highlightWord)
        {            
            Cursor = Cursors.WaitCursor; Application.DoEvents();
            this.SuspendLayout();
            this.VerticalScroll.Value = 0;this.HorizontalScroll.Value = 0;
            this.Controls.Clear();

            int catsTop = DistanceFromTop;
            int lastDistanceFromRight;
            ShowCategory(_db.GetCategory(poem._CatID), ref catsTop, out lastDistanceFromRight, false, keepTrack);
            lastDistanceFromRight += DistanceFromRightStep;

            _strPage += " -> " + poem._Title;

            LinkLabel lblPoem = new LinkLabel();
            poem._HighlightText = string.Empty;
            lblPoem.Tag = poem;
            lblPoem.AutoSize = true;
            lblPoem.Text = poem._Title;
            lblPoem.Location = new Point(lastDistanceFromRight, catsTop);
            lblPoem.LinkVisited = true;
            lblPoem.LinkBehavior = LinkBehavior.HoverUnderline;
            lblPoem.BackColor = Color.Transparent;
            lblPoem.Click += new EventHandler(lblPoem_Click);
            this.Controls.Add(lblPoem);

            catsTop += DistanceBetweenLines;
            lastDistanceFromRight += DistanceFromRightStep;


            List<GanjoorVerse> verses = _db.GetVerses(poem._ID);

            Control lblHighlight = null;
            int ItemsMatchingPhrase = 0;
            bool ShowBeytNums = Properties.Settings.Default.ShowBeytNums;
            int BeytNum = 0;
            int versDistanceFromRight = ShowBeytNums ? lastDistanceFromRight + 20 : lastDistanceFromRight;
            for (int i = 0; i < verses.Count; i++)
            {
                if (ShowBeytNums)
                {
                    if (verses[i]._Position == VersePosition.Right)
                    {
                        BeytNum++;
                        LinkLabel lblNum = new LinkLabel();
                        lblNum.AutoSize = true;
                        lblNum.Text = BeytNum.ToString();
                        lblNum.Tag = verses[i];
                        lblNum.BackColor = Color.Transparent;
                        lblNum.LinkBehavior = LinkBehavior.HoverUnderline;
                        lblNum.Location = new Point(lastDistanceFromRight, catsTop + i * DistanceBetweenLines);
                        lblNum.Click += new EventHandler(lblNum_Click);
                        this.Controls.Add(lblNum);
                        if (_db.IsVerseFaved(poem._ID, verses[i]._Order))
                        {
                            PictureBox fav = new PictureBox();
                            fav.BackColor = Color.Transparent;
                            fav.Image = Properties.Resources.fav;
                            fav.Size = new Size(16, 16);
                            fav.Location = new Point(lastDistanceFromRight, lblNum.Location.Y);
                            fav.Tag = verses[i];
                            fav.Cursor = Cursors.Hand;
                            fav.Click += new EventHandler(lblNum_Click);
                            this.Controls.Add(fav);
                            lblNum.Visible = false;
                        }
                    }
                }
                HighlightLabel lblVerse = new HighlightLabel(highlightWord, Color.LightPink);                
                lblVerse.AutoSize = true;
                lblVerse.Tag = verses[i];
                lblVerse.Text = verses[i]._Text;
                lblVerse.Location = new Point(versDistanceFromRight, catsTop + i * DistanceBetweenLines);
                lblVerse.BackColor = Color.Transparent;
                this.Controls.Add(lblVerse);

                if (!string.IsNullOrEmpty(highlightWord))
                    if (verses[i]._Text.IndexOf(highlightWord) != -1)
                    {
                        ItemsMatchingPhrase++;
                        if(lblHighlight == null)
                            lblHighlight = lblVerse;
                    }
            }
            //یک بر چسب اضافی برای اضافه شدن فضای پایین فرم
            Label lblDummy = new Label();
            lblDummy.Text = " ";
            lblDummy.Location = new Point(200, catsTop + verses.Count * DistanceBetweenLines);
            lblDummy.BackColor = Color.Transparent;
            this.Controls.Add(lblDummy);

            //کلک راست به چپ!
            foreach (Control ctl in this.Controls)
                ctl.Location = new Point(this.Width - ctl.Right, ctl.Location.Y);
            
            this.ResumeLayout();
            Cursor = Cursors.Default;
            _FavsPage = false;
            _iCurPoem = poem._ID;

            _strLastPhrase = null;
            if (null != OnPageChanged)
                OnPageChanged(_strPage, true, true, poem._Faved, false, highlightWord, null, null);
            return ItemsMatchingPhrase;
        }

        #endregion

        #region Fancy Stuff!
        private Color bBegin;
        private Color bEnd;
        private bool GradiantBackground;        
        public void ApplyUISettings()
        {
            this.ForeColor = Properties.Settings.Default.TextColor;
            this.BackColor = Properties.Settings.Default.BackColor;
            if (!string.IsNullOrEmpty(Properties.Settings.Default.BackImagePath))
            {
                if (System.IO.File.Exists(Properties.Settings.Default.BackImagePath))
                    try
                    {
                        this.BackgroundImage = new Bitmap(Properties.Settings.Default.BackImagePath);
                    }
                    catch
                    {
                    }
            }
            else
                this.BackgroundImage = null;
            GradiantBackground = Properties.Settings.Default.GradiantBackground;
            if (GradiantBackground)
                this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            else
            {
                this.SetStyle(ControlStyles.AllPaintingInWmPaint , false);
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            }
            this.bBegin = Properties.Settings.Default.GradiantBegin;
            this.bEnd = Properties.Settings.Default.GradiantEnd;
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
            if (this.BackgroundImage != null)
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
                DistanceBetweenLines = TextRenderer.MeasureText("ا", value).Width * 2;

                if(_db != null)
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
                        ShowSearchResults(_strLastPhrase, _iCurSearchStart, Properties.Settings.Default.SearchPageItems, Properties.Settings.Default.LastSearchPoetID, false);
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
                    GanjoorPoet poet = _db.GetPoet(_iCurCat);
                    if (null != poet)
                        return poet._Name;
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
                _history.Push(new GarnjoorBrowsingHistory(string.Empty, 0, _iCurSearchStart, _iCurSearchPageCount, true));
            }
            else
            if (!string.IsNullOrEmpty(_strLastPhrase))
            {
                _history.Push(new GarnjoorBrowsingHistory(_strLastPhrase, _iCurSearchPoet, _iCurSearchStart, _iCurSearchPageCount, false));
            }
            else
                if (
                    (_history.Count == 0) || !((_history.Peek()._CatID == _iCurCat) && (_history.Peek()._CatPageStart == _iCurCatStart) && ((_history.Peek()._PoemID == _iCurPoem)))
                    )
                {
                    _history.Push(new GarnjoorBrowsingHistory(_iCurCat, _iCurPoem, _iCurCatStart));

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
        void Document_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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
                    ShowCategory(_db.GetCategory(poem._CatID), ref catsTop, out lastDistanceFromRight, false, false);
                    lastDistanceFromRight += DistanceFromRightStep;
                    LinkLabel lblPoem = new LinkLabel();
                    lblPoem.Tag = poem;
                    lblPoem.AutoSize = true;
                    lblPoem.Text = poem._Title;
                    lblPoem.Location = new Point(lastDistanceFromRight, catsTop);
                    lblPoem.LinkBehavior = LinkBehavior.HoverUnderline;
                    lblPoem.BackColor = Color.Transparent;
                    lblPoem.Click += new EventHandler(lblPoem_Click);
                    this.Controls.Add(lblPoem);

                    catsTop += DistanceBetweenLines;
                    lastDistanceFromRight += DistanceFromRightStep;


                    List<GanjoorVerse> verses = _db.GetVerses(poem._ID);

                    using (DataTable firsVerse = _db.FindFirstVerseContaingPhrase(poem._ID, phrase))
                    {
                        System.Diagnostics.Debug.Assert(firsVerse.Rows.Count == 1);


                        HighlightLabel lblVerse = new HighlightLabel(phrase, Color.LightPink);
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


            this.ResumeLayout();
            Cursor = Cursors.Default;
            _iCurPoem = 0;


            _strPage = "نتایج جستجو برای \"" + phrase + "\" در آثار " + _db.GetPoet(PoetID)._Name + " صفحۀ " + (Count < 1 ? "1 (موردی یافت نشد.)" : (1 + PageStart / Count).ToString() + " (مورد " + (PageStart + 1).ToString() + " تا " + (PageStart + Count).ToString() + ")");
            if (null != OnPageChanged)
                OnPageChanged(_strPage, false, false, false, false, string.Empty, prePage, nextPage);

            if (Count < 1)
                MessageBox.Show("موردی یافت نشد.", "جستجو", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);

        }
        public int HighlightText(string phrase)
        {
            if (_iCurCat != 0)
            {
                if (_iCurPoem != 0)
                {
                    int count = 0;
                    foreach (Control ctl in this.Controls)
                        if (ctl is HighlightLabel)
                        {
                            (ctl as HighlightLabel).Keyword = phrase;
                            if (ctl.Text.IndexOf(phrase) != -1)
                                count++;
                        }
                    this.Invalidate();
                    return count;
                }
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
                    txt += ctl.Text + "\r\n";
            Clipboard.SetText(txt);
        }
        public void StoreSettings()
        {
            Properties.Settings.Default.LastCat = _iCurCat;
            Properties.Settings.Default.LastCatStart = _iCurCatStart;
            Properties.Settings.Default.LastPoem = _iCurPoem;
            Properties.Settings.Default.LastSearchPhrase = _strLastPhrase;
            Properties.Settings.Default.LastSeachStart = _iCurSearchStart;
            Properties.Settings.Default.WasShowingFavs = _FavsPage;
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
                    int lastDistanceFromRight;
                    ShowCategory(_db.GetCategory(poem._CatID), ref catsTop, out lastDistanceFromRight, false, false);
                    lastDistanceFromRight += DistanceFromRightStep;
                    LinkLabel lblPoem = new LinkLabel();
                    lblPoem.Tag = poem;
                    lblPoem.AutoSize = true;
                    lblPoem.Text = poem._Title;
                    lblPoem.Location = new Point(lastDistanceFromRight, catsTop);
                    lblPoem.LinkBehavior = LinkBehavior.HoverUnderline;
                    lblPoem.BackColor = Color.Transparent;
                    lblPoem.Click += new EventHandler(lblPoem_Click);
                    this.Controls.Add(lblPoem);

                    catsTop += DistanceBetweenLines;
                    lastDistanceFromRight += DistanceFromRightStep;


                    List<GanjoorVerse> verses = _db.GetVerses(poem._ID);



                    HighlightLabel lblVerse = new HighlightLabel();
                    lblVerse.AutoSize = true;
                    lblVerse.Tag = null;
                    lblVerse.Text = _db.GetVerses(poem._ID, 1)[0]._Text;
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
        public void ToggleBeytNums()
        {
            if (_iCurPoem != 0)
            {
                Properties.Settings.Default.ShowBeytNums = !Properties.Settings.Default.ShowBeytNums;
                Properties.Settings.Default.Save();
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
        #endregion

        #region Random Pome
        public void ShowRandomPoem()
        {
            GanjoorPoem poem = _db.GetPoem(_db.GetRandomPoem(Properties.Settings.Default.RandomOnlyHafez ? 24 : 0));
            if (poem != null)
                ShowPoem(poem, true);
            else
                ShowRandomPoem();//not any random id exists, so repeat until finding a valid id
        }
        #endregion

    }
}
