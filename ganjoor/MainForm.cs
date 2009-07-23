using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

/*
 * اولین ویرایش:
 *  بیست و نهم تیر هشتاد و هشت
 *  حمیدرضا محمدی
 *  http://ganjoor.net   
 */

namespace ganjoor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            _db = new DbBrowser();
        }

        DbBrowser _db;

        const int DistanceFromTop = 10;
        const int DistanceBetweenLines = 20;
        const int DistanceFromRight = 10;

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (_db.Failed)
            {
                MessageBox.Show(_db.LastError, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ShowHome();
            }
        }

        private void ShowHome()
        {
            lblCat_Click(null, new EventArgs());

        }

        private void lblCat_Click(object sender, EventArgs e)
        {

            GanjoorCat category=null;
            if(sender!=null)
                category = ((GanjoorCat)((LinkLabel)sender).Tag);
            else
                category = new GanjoorCat(0, 0, "", 0, "");


            Cursor = Cursors.WaitCursor; Application.DoEvents();
            this.SuspendLayout();
            this.Controls.Clear();


            int catsTop;
            int lastDistanceFromRight;
            ShowCategory(category, out catsTop, out lastDistanceFromRight, true);


            
            
            List<GanjoorCat> subcats = _db.GetSubCategories(category._ID);
            if(subcats.Count !=0)
                lastDistanceFromRight += 2 * DistanceFromRight;
            for (int i = 0; i < subcats.Count; i++)
            {
                LinkLabel lblCat = new LinkLabel();
                lblCat.Tag = subcats[i];
                lblCat.AutoSize = true;
                lblCat.Text = subcats[i]._Text;
                lblCat.Location = new Point(lastDistanceFromRight, catsTop + i * DistanceBetweenLines);
                lblCat.LinkBehavior = LinkBehavior.HoverUnderline;
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
                lblPoem.Click += new EventHandler(lblPoem_Click);
                this.Controls.Add(lblPoem);
            }

            //یک بر چسب اضافی برای اضافه شدن فضای پایین فرم
            Label lblDummy = new Label();
            lblDummy.Text = " ";
            lblDummy.Location = new Point(DistanceFromRight, poemsTop + poems.Count * DistanceBetweenLines);
            this.Controls.Add(lblDummy);


            this.ResumeLayout();
            Cursor = Cursors.Default;

        }

        private void ShowCategory(GanjoorCat category, out int catsTop, out int lastDistanceFromRight, bool highlightCat)
        {
            catsTop = DistanceFromTop;
            lastDistanceFromRight = DistanceFromRight;

            //اجداد این دسته
            List<GanjoorCat> ancestors = _db.GetParentCategories(category);

            for (int i = 0; i < ancestors.Count; i++)
            {
                LinkLabel lblCat = new LinkLabel();
                lblCat.Tag = ancestors[i];
                lblCat.AutoSize = true;
                lblCat.Text = ancestors[i]._Text;
                lastDistanceFromRight += 2 * DistanceFromRight;
                lblCat.Location = new Point(lastDistanceFromRight, catsTop + i * DistanceBetweenLines);
                lblCat.LinkBehavior = LinkBehavior.HoverUnderline;
                lblCat.Click += new EventHandler(lblCat_Click);
                this.Controls.Add(lblCat);
            }


            catsTop += (ancestors.Count * DistanceBetweenLines);
            //خود این دسته

            if (category._ID != 0)
            {
                LinkLabel lblMe = new LinkLabel();
                lblMe.Tag = category;
                lblMe.AutoSize = true;
                lblMe.Text = category._Text;
                lastDistanceFromRight += 2 * DistanceFromRight;
                lblMe.Location = new Point(lastDistanceFromRight, catsTop);
                lblMe.LinkVisited = highlightCat;
                lblMe.LinkBehavior = LinkBehavior.HoverUnderline;
                lblMe.Click += new EventHandler(lblCat_Click);
                this.Controls.Add(lblMe);

                catsTop += DistanceBetweenLines;
            }
        }

        private void lblPoem_Click(object sender, EventArgs e)
        {
            GanjoorPoem poem = ((GanjoorPoem)((LinkLabel)sender).Tag);

            Cursor = Cursors.WaitCursor; Application.DoEvents();
            this.SuspendLayout();
            this.Controls.Clear();

            int catsTop;
            int lastDistanceFromRight;
            ShowCategory(_db.GetCategory(poem._CatID), out catsTop, out lastDistanceFromRight, false);
            lastDistanceFromRight += DistanceFromRight;

            LinkLabel lblPoem = new LinkLabel();
            lblPoem.Tag = poem;
            lblPoem.AutoSize = true;
            lblPoem.Text = poem._Title;
            lblPoem.Location = new Point(lastDistanceFromRight, catsTop);
            lblPoem.LinkVisited = true;
            lblPoem.LinkBehavior = LinkBehavior.HoverUnderline;
            lblPoem.Click += new EventHandler(lblPoem_Click);
            this.Controls.Add(lblPoem);

            catsTop += DistanceBetweenLines;
            lastDistanceFromRight += DistanceFromRight;


            List<GanjoorVerse> verses = _db.GetVerses(poem._ID);

            for (int i = 0; i < verses.Count; i++)
            {
                Label lblVerse = new Label();
                lblVerse.AutoSize = true;
                lblVerse.Tag = verses[i];
                lblVerse.Text = verses[i]._Text;
                lblVerse.Location = new Point(lastDistanceFromRight, catsTop + i * DistanceBetweenLines);
                this.Controls.Add(lblVerse);
            }
            //یک بر چسب اضافی برای اضافه شدن فضای پایین فرم
            Label lblDummy = new Label();
            lblDummy.Text = " ";
            lblDummy.Location = new Point(DistanceFromRight, catsTop + verses.Count * DistanceBetweenLines);
            this.Controls.Add(lblDummy);

            this.ResumeLayout();
            Cursor = Cursors.Default;
        }



    }
}
