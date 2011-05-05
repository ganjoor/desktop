using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ganjoor.Properties;

namespace ganjoor
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            InitializeControls();

        }

        private void InitializeControls()
        {
            ViewFont = Properties.Settings.Default.ViewFont;
            lblFont.Text = ViewFont.Name + "(" + ViewFont.Style.ToString() + ") " + ViewFont.Size.ToString();
            chkHighlightSearchResults.Checked = Properties.Settings.Default.HighlightKeyword;
            chkBrowseButton.Checked = Settings.Default.BrowseButtonVisible;
            chkCommentsButton.Checked = Settings.Default.CommentsButtonVisible;
            chkCopyButton.Checked = Settings.Default.CopyButtonVisible;
            chkPrintButton.Checked = Settings.Default.PrintButtonVisible;
            chkShowNumsButton.Checked = Settings.Default.ShowNumsVisible;
            chkHomeButton.Checked = Settings.Default.HomeButtonVisible;
            chkRandomButton.Checked = Settings.Default.RandomButtonVisible;
            chkEditorButton.Checked = Settings.Default.EditorButtonVisible;
            chkDownloadButton.Checked = Settings.Default.DownloadButtonVisible;
            numMaxPoems.Value = Settings.Default.MaxPoemsInList;
            chkGradiantBk.Checked = Settings.Default.GradiantBackground;
            btnGradiantBegin.BackColor = Settings.Default.GradiantBegin;
            btnGradiantEnd.BackColor = Settings.Default.GradiantEnd;
            btnBackColor.BackColor = Settings.Default.BackColor;
            lblImagePath.Text = Settings.Default.BackImagePath;
            btnTextColor.BackColor = Settings.Default.TextColor;
            btnLinkColor.BackColor = Settings.Default.LinkColor;
            btnCurrentLinkColor.BackColor = Settings.Default.CurrentLinkColor;
            btnHighlightColor.BackColor = Settings.Default.HighlightColor;
            btnBandLinkColor.BackColor = Settings.Default.BandLinkColor;
            chkCheckForUpdate.Checked = Settings.Default.CheckForUpdate;
            chkScrollToFaved.Checked = Settings.Default.ScrollToFavedVerse;
            chkCenteredViewMode.Checked = GanjoorViewMode.Centered == (GanjoorViewMode)Settings.Default.ViewMode;
            _RandomCatIDs = Settings.Default.RandomCats;
            lblRandomCat.Text = RandomCatPath;
            numMaxFavs.Value = Settings.Default.FavItemsInPage;
            txtProxyServer.Text = Settings.Default.HttpProxyServer;
            txtProxyPort.Text = Settings.Default.HttpProxyPort;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            Settings.Default.ViewFont = ViewFont;
            Settings.Default.HighlightKeyword = chkHighlightSearchResults.Checked;
            Settings.Default.BrowseButtonVisible = chkBrowseButton.Checked;
            Settings.Default.CommentsButtonVisible = chkCommentsButton.Checked;
            Settings.Default.CopyButtonVisible = chkCopyButton.Checked;
            Settings.Default.PrintButtonVisible = chkPrintButton.Checked;
            Settings.Default.ShowNumsVisible = chkShowNumsButton.Checked;
            Settings.Default.HomeButtonVisible = chkHomeButton.Checked;
            Settings.Default.RandomButtonVisible = chkRandomButton.Checked;
            Settings.Default.EditorButtonVisible = chkEditorButton.Checked;
            Settings.Default.DownloadButtonVisible = chkDownloadButton.Checked;
            Settings.Default.MaxPoemsInList = (int)numMaxPoems.Value;
            Settings.Default.GradiantBackground = chkGradiantBk.Checked;
            Settings.Default.GradiantBegin = btnGradiantBegin.BackColor;
            Settings.Default.GradiantEnd = btnGradiantEnd.BackColor;
            Settings.Default.BackColor = btnBackColor.BackColor;
            Settings.Default.BackImagePath = lblImagePath.Text;
            Settings.Default.TextColor = btnTextColor.BackColor;
            Settings.Default.LinkColor = btnLinkColor.BackColor;
            Settings.Default.CurrentLinkColor = btnCurrentLinkColor.BackColor;
            Settings.Default.HighlightColor = btnHighlightColor.BackColor;
            Settings.Default.CheckForUpdate = chkCheckForUpdate.Checked;
            Settings.Default.RandomCats = _RandomCatIDs;
            Settings.Default.BandLinkColor = btnBandLinkColor.BackColor;
            Settings.Default.ScrollToFavedVerse = chkScrollToFaved.Checked;
            Settings.Default.ViewMode = chkCenteredViewMode.Checked ? (int)GanjoorViewMode.Centered : (int)GanjoorViewMode.RightAligned;
            Settings.Default.FavItemsInPage = (int)numMaxFavs.Value;
            Settings.Default.HttpProxyServer = txtProxyServer.Text;
            Settings.Default.HttpProxyPort = txtProxyPort.Text;



            Properties.Settings.Default.Save();
        }
        
        private Font ViewFont { set; get; }

        private void btnSelectFont_Click(object sender, EventArgs e)
        {
            using (FontDialog dlg = new FontDialog())
            {
                dlg.Font = ViewFont;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    ViewFont = dlg.Font;
                    lblFont.Text = ViewFont.Name + "(" + ViewFont.Style.ToString() + ") " + ViewFont.Size.ToString();
                }
            }
        }

        
        private void chkGradiantBk_CheckedChanged(object sender, EventArgs e)
        {
            lblGradiantBegin.Enabled = lblGradiantEnd.Enabled = btnGradiantBegin.Enabled = btnGradiantEnd.Enabled = chkGradiantBk.Checked;
            btnNoBkImage.Enabled = lblNormalBk.Enabled = btnBackColor.Enabled = lblImagePath.Enabled = lblImage.Enabled = btnSelect.Enabled = !chkGradiantBk.Checked;
        }

        private void btnGradiantBegin_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlg = new ColorDialog())
            {
                dlg.Color = btnGradiantBegin.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    btnGradiantBegin.BackColor = dlg.Color;
                }
            }
        }

        private void btnGradiantEnd_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlg = new ColorDialog())
            {
                dlg.Color = btnGradiantEnd.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    btnGradiantEnd.BackColor = dlg.Color;
                }
            }
        }

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlg = new ColorDialog())
            {
                dlg.Color = btnBackColor.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    btnBackColor.BackColor = dlg.Color;
                }
            }
        }

        private void btnTextColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlg = new ColorDialog())
            {
                dlg.Color = btnTextColor.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    btnTextColor.BackColor = dlg.Color;
                }
            }
        }


        private void btnSelect_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "تصاویر|*.jpg;*.bmp;*.png;*.gif|انواع فایلها|*.*";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    lblImagePath.Text = dlg.FileName;
                }
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            bool showLineNumbers = Settings.Default.ShowBeytNums;
            Settings.Default.Reset();
            Settings.Default.IsNewVersion = false;
            Settings.Default.ShowBeytNums = showLineNumbers;
            InitializeControls();
        }

        private void btnNoBkImage_Click(object sender, EventArgs e)
        {
            lblImagePath.Text = "";
        }

        private void btnLinkColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlg = new ColorDialog())
            {
                dlg.Color = btnLinkColor.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    btnLinkColor.BackColor = dlg.Color;
                }
            }
        }

        private void btnCurrentLinkColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlg = new ColorDialog())
            {
                dlg.Color = btnCurrentLinkColor.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    btnCurrentLinkColor.BackColor = dlg.Color;
                }
            }
        }

        private void btnHighlightColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlg = new ColorDialog())
            {
                dlg.Color = btnHighlightColor.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    btnHighlightColor.BackColor = dlg.Color;
                }
            }
        }

        private void btnBandLinkColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlg = new ColorDialog())
            {
                dlg.Color = btnBandLinkColor.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    btnBandLinkColor.BackColor = dlg.Color;
                }
            }
        }

        private string _RandomCatIDs = "0";
        private string RandomCatPath
        {
            get
            {
                if (_RandomCatIDs == "0")
                    return "همه";
                DbBrowser db = new DbBrowser();
                string[] CatStrs = _RandomCatIDs.Split(new char[]{';'}, StringSplitOptions.RemoveEmptyEntries);

                List<GanjoorCat> SelectedCats = new List<GanjoorCat>();
                foreach(string CatStr in CatStrs)
                {
                    GanjoorCat cat = db.GetCategory(Convert.ToInt32(CatStr));
                    if(cat != null)
                        SelectedCats.Add(cat);
                        
                }
                string result = "";
                if(SelectedCats.Count == 0)
                    result = "همه";
                else
                {
                    foreach (GanjoorCat cat in SelectedCats)
                    {
                        List<GanjoorCat> cats = db.GetParentCategories(cat);
                        string catString = "";
                        foreach (GanjoorCat parCat in cats)
                            if (parCat._ID != 0)
                                catString += parCat._Text + " ->";
                        catString += cat._Text;

                        result += (catString + "؛");
                    }
                }
                db.CloseDb();
                return result;
                    
            }
        }
        private void btnSelectRandomCat_Click(object sender, EventArgs e)
        {
            using (CategorySelector dlg = new CategorySelector())
            {
                dlg.CheckedCatsString = _RandomCatIDs;
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    _RandomCatIDs = dlg.CheckedCatsString;
                    lblRandomCat.Text = RandomCatPath;
                }
            }
        }

    }
}
