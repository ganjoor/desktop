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
            numMaxPoems.Value = Settings.Default.MaxPoemsInList;
            chkGradiantBk.Checked = Settings.Default.GradiantBackground;
            btnGradiantBegin.BackColor = Settings.Default.GradiantBegin;
            btnGradiantEnd.BackColor = Settings.Default.GradiantEnd;
            btnBackColor.BackColor = Settings.Default.BackColor;
            lblImagePath.Text = Settings.Default.BackImagePath;
            btnTextColor.BackColor = Settings.Default.TextColor;
            chkCheckForUpdate.Checked = Settings.Default.CheckForUpdate;
            if (!(chkRandomOnlyHafez.Checked = Settings.Default.RandomOnlyHafez))
                chkRandomAll.Checked = true;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ViewFont = ViewFont;
            Properties.Settings.Default.HighlightKeyword = chkHighlightSearchResults.Checked;
            Settings.Default.BrowseButtonVisible = chkBrowseButton.Checked;
            Settings.Default.CommentsButtonVisible = chkCommentsButton.Checked;
            Settings.Default.CopyButtonVisible = chkCopyButton.Checked;
            Settings.Default.PrintButtonVisible = chkPrintButton.Checked;
            Settings.Default.ShowNumsVisible = chkShowNumsButton.Checked;
            Settings.Default.HomeButtonVisible = chkHomeButton.Checked;
            Settings.Default.RandomButtonVisible = chkRandomButton.Checked;
            Settings.Default.MaxPoemsInList = (int)numMaxPoems.Value;
            Settings.Default.GradiantBackground = chkGradiantBk.Checked;
            Settings.Default.GradiantBegin = btnGradiantBegin.BackColor;
            Settings.Default.GradiantEnd = btnGradiantEnd.BackColor;
            Settings.Default.BackColor = btnBackColor.BackColor;
            Settings.Default.BackImagePath = lblImagePath.Text;
            Settings.Default.TextColor = btnTextColor.BackColor;
            Settings.Default.CheckForUpdate = chkCheckForUpdate.Checked;
            Settings.Default.RandomOnlyHafez = chkRandomOnlyHafez.Checked;

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
            lblNormalBk.Enabled = btnBackColor.Enabled = lblImagePath.Enabled = lblImage.Enabled = btnSelect.Enabled = !chkGradiantBk.Checked;
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
            Settings.Default.Reset();
            InitializeControls();
        }

        private void btnNoBkImage_Click(object sender, EventArgs e)
        {
            lblImagePath.Text = "";
        }

    }
}
