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
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            _iCurCat = _iCurPoem = 0;
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
        private int _iCurPoem;
        private string _strPage;
        #endregion

        #region Constants
        private const int DistanceFromTop = 10;
        private const int DistanceBetweenLines = 20;
        private const int DistanceFromRight = 20;
        private const int DistanceFromRightStep = 10;
        #endregion

        #region Click Events
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
                    ShowHome(true);
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
            this.Controls.Clear();


            int catsTop;
            int lastDistanceFromRight;
            ShowCategory(category, out catsTop, out lastDistanceFromRight, true, keepTrack);



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
            for (int i = 0; i < poems.Count; i++)
            {
                LinkLabel lblPoem = new LinkLabel();
                lblPoem.Tag = poems[i];
                lblPoem.AutoSize = true;
                lblPoem.Text = poems[i]._Title;
                List<GanjoorVerse> v = _db.GetVerses(poems[i]._ID, 1);
                if (v.Count > 0)
                    lblPoem.Text += " : " + v[0]._Text;

                lblPoem.Location = new Point(poemsDistanceFromRight, poemsTop + i * DistanceBetweenLines);
                lblPoem.LinkBehavior = LinkBehavior.HoverUnderline;
                lblPoem.BackColor = Color.Transparent;
                lblPoem.Click += new EventHandler(lblPoem_Click);
                this.Controls.Add(lblPoem);
            }

            //یک بر چسب اضافی برای اضافه شدن فضای پایین فرم
            Label lblDummy = new Label();
            lblDummy.Text = " ";
            lblDummy.Location = new Point(DistanceFromRight, poemsTop + poems.Count * DistanceBetweenLines);
            lblDummy.BackColor = Color.Transparent;
            this.Controls.Add(lblDummy);


            //کلک راست به چپ!
            foreach (Control ctl in this.Controls)
                ctl.Location = new Point(this.Width - ctl.Right, ctl.Location.Y);


            this.ResumeLayout();
            Cursor = Cursors.Default;

            if (null != OnPageChanged)
                OnPageChanged(_strPage, false, false, false);
        }
        private void ShowCategory(GanjoorCat category, out int catsTop, out int lastDistanceFromRight, bool highlightCat,bool keepTrack)
        {
            if(keepTrack)
            UpdateHistory();
            catsTop = DistanceFromTop;
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

            _iCurCat = category._ID;
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

        private void ShowPoem(GanjoorPoem poem, bool keepTrack)
        {
            Cursor = Cursors.WaitCursor; Application.DoEvents();
            this.SuspendLayout();
            this.Controls.Clear();

            int catsTop;
            int lastDistanceFromRight;
            ShowCategory(_db.GetCategory(poem._CatID), out catsTop, out lastDistanceFromRight, false, keepTrack);
            lastDistanceFromRight += DistanceFromRightStep;

            _strPage += " -> " + poem._Title;

            LinkLabel lblPoem = new LinkLabel();
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

            for (int i = 0; i < verses.Count; i++)
            {
                Label lblVerse = new Label();
                lblVerse.AutoSize = true;
                lblVerse.Tag = verses[i];
                lblVerse.Text = verses[i]._Text;
                lblVerse.Location = new Point(lastDistanceFromRight, catsTop + i * DistanceBetweenLines);
                lblVerse.BackColor = Color.Transparent;
                this.Controls.Add(lblVerse);
            }
            //یک بر چسب اضافی برای اضافه شدن فضای پایین فرم
            Label lblDummy = new Label();
            lblDummy.Text = " ";
            lblDummy.Location = new Point(DistanceFromRight, catsTop + verses.Count * DistanceBetweenLines);
            lblDummy.BackColor = Color.Transparent;
            this.Controls.Add(lblDummy);

            //کلک راست به چپ!
            foreach (Control ctl in this.Controls)
                ctl.Location = new Point(this.Width - ctl.Right, ctl.Location.Y);


            this.ResumeLayout();
            Cursor = Cursors.Default;
            _iCurPoem = poem._ID;


            if (null != OnPageChanged)
                OnPageChanged(_strPage, CanGoToNextPoem, CanGoToPreviousPoem, true);
        }
        #endregion

        #region Fancy Stuff!
        protected override void OnPaint(PaintEventArgs e)
        {
            using (LinearGradientBrush brsh = new LinearGradientBrush(this.Bounds, Color.LightGray, Color.White, 0.0f))
            {
                e.Graphics.FillRectangle(brsh, e.ClipRectangle);
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
            if (
                ((_history.Count == 0) && (_iCurCat!=0))
                ||
                (_history.Count != 0) && !((_history.Peek()._CatID == _iCurCat) && ((_history.Peek()._PoemID == _iCurPoem)))
                )
                _history.Push(new GarnjoorBrowsingHistory(_iCurCat, _iCurPoem));
        }
        public void GoBackInHistory()//forward twards back!
        {
            if (CanGoBackInHistory)
            {
                GarnjoorBrowsingHistory back = _history.Pop();
                if (0 == back._PoemID)
                {
                    if (0 == back._CatID)
                    {
                        ShowHome(false);
                    }
                    else
                        ShowCategory(_db.GetCategory(back._CatID), false);                        
                }
                else
                    ShowPoem(_db.GetPoem(back._PoemID), false);
            }
        }
        #endregion
        #region Printing
        public void Print(PrintDocument Document)
        {
            Document.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(Document_PrintPage);
            _iRemainingUnprintedLines = 0;
            Document.Print();
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
                int dist = (0 == StartFrom) ? 50 : 25;
                int top = e.PageBounds.Top + 100;
                int mid = e.PageBounds.Left + e.PageBounds.Width / 2;

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
                                        mid - TextRenderer.MeasureText(ctl.Text, this.Font).Width / 2,
                                        top));
                                top += dist;
                                if (top > e.PageBounds.Bottom - dist)
                                    return line + 1;
                                dist = 25;
                            }
                        }

            }
            return 0;
        }
        #endregion
    }
}
