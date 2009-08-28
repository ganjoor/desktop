﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

/*
 * Version Pre 1.0 -> 1388/04/29
 * Hamid Reza Mohammadi http://ganjoor.net
 *
 * Version 1.0:
 *   Changelog (after a pre 1.0 release):   
 *      MainForm code moved to GanjoorView user control
 *      Added main toolbar, statusbar, about box
 *      Project code moved to sf.net : http://ganjoor.sourceforge.net
 */

namespace ganjoor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Bounds = Screen.PrimaryScreen.Bounds;
            if (Properties.Settings.Default.WindowMaximized)
                this.WindowState = FormWindowState.Maximized;
            else
                if (Properties.Settings.Default.WindowSize.Width != 0)
                {
                    this.Bounds = new Rectangle(Properties.Settings.Default.WindowLocation, Properties.Settings.Default.WindowSize);
                }
            ganjoorView.Font = Properties.Settings.Default.ViewFont;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            ganjoorView.ShowHome(true);
        }

        private void ganjoorView_OnPageChanged(string PageString, bool CanGoNextPoem, bool CanGoPreviousPoem, bool HasComments, bool CanBrowse, string HighlightedText, int ItemsFound)
        {
            lblCurrentPage.Text = PageString;
            btnNextPoem.Enabled = CanGoNextPoem;
            btnPreviousPoem.Enabled = CanGoPreviousPoem;
            btnComments.Enabled = HasComments;
            btnPrint.Enabled = btnComments.Enabled;
            btnHistoryBack.Enabled = ganjoorView.CanGoBackInHistory;
            btnViewInSite.Enabled = CanBrowse;
            processTextChanged = false;
            txtHighlight.Text = HighlightedText;
            lblFoundItemCount.Visible = string.IsNullOrEmpty(HighlightedText);
            lblFoundItemCount.Text = String.Format("{0} مورد یافت شد.", ItemsFound);
            if (string.IsNullOrEmpty(HighlightedText))
            {
                btnHighlight.Checked = false;
            }
            processTextChanged = true;
            btnHighlight.Enabled = HasComments;            

            mnuNextPoem.Enabled = btnNextPoem.Enabled;
            mnuPreviousPoem.Enabled = btnPreviousPoem.Enabled;
            mnuComments.Enabled = btnComments.Enabled;
            mnuPrintPreview.Enabled = mnuPrint.Enabled = btnPrint.Enabled;
            mnuHistoryBack.Enabled = btnHistoryBack.Enabled;
            mnuViewInSite.Enabled = btnViewInSite.Enabled;
            mnuHighlight.Enabled = btnHighlight.Enabled;
        }

        private void btnPreviousPoem_Click(object sender, EventArgs e)
        {
            ganjoorView.PreviousPoem();
        }

        private void btnNextPoem_Click(object sender, EventArgs e)
        {
            ganjoorView.NextPoem();
        }

        private void btnViewInSite_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(ganjoorView.CurrentPageGanjoorUrl);
        }

        private void btnComments_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(ganjoorView.CurrentPoemCommentsUrl);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            using (PrintDialog dlg = new PrintDialog())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {                    
                    using (dlg.Document = new System.Drawing.Printing.PrintDocument())
                    {
                        ganjoorView.Print(dlg.Document);
                    }
                }
            }
        }

        private void mnuPrintPreview_Click(object sender, EventArgs e)
        {
            using (PrintPreviewDialog dlg = new PrintPreviewDialog())
            {
                dlg.ShowIcon = false;
                dlg.UseAntiAlias = true;                
                using (PrintDocument Document = ganjoorView.PrepareForPrintPreview())
                {
                    dlg.Document = Document;
                    dlg.ShowDialog(this);
                }
            }
        }


        private void btnHistoryBack_Click(object sender, EventArgs e)
        {
            ganjoorView.GoBackInHistory();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            using (AboutForm dlg = new AboutForm())
            {
                dlg.ShowDialog(this);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (Search dlg = new Search())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {

                    Properties.Settings.Default.SearchPageItems = dlg.ItemsInPage;
                    Properties.Settings.Default.Save();
                    ganjoorView.ShowSearchResults(dlg.Phrase, 0, dlg.ItemsInPage);
                }
            }
        }

        private void btnCopyText_Click(object sender, EventArgs e)
        {
            ganjoorView.CopyText();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ganjoorView.StoreSettings();
            Properties.Settings.Default.WindowMaximized = (this.WindowState == FormWindowState.Maximized);
            Properties.Settings.Default.WindowLocation = this.Location;
            Properties.Settings.Default.WindowSize = this.Size;
            Properties.Settings.Default.Save();
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            using (Options dlg = new Options())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    ganjoorView.Font = Properties.Settings.Default.ViewFont;                    
                }
            }
        }


        private void btnHighlight_CheckedChanged(object sender, EventArgs e)
        {
            if (tlbrSearch.Visible = btnHighlight.Checked)
            {
                lblFoundItemCount.Visible = !string.IsNullOrEmpty(txtHighlight.Text);
                if (!string.IsNullOrEmpty(txtHighlight.Text))
                    txtHighlight_TextChanged(sender, e);
                txtHighlight.Focus();
            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuHighlight_Click(object sender, EventArgs e)
        {
            btnHighlight.Checked = !btnHighlight.Checked;
        }

        private bool processTextChanged = true;
        private void txtHighlight_TextChanged(object sender, EventArgs e)
        {
            if (processTextChanged)
            {
                int count = ganjoorView.HighlightText(txtHighlight.Text);
                if (lblFoundItemCount.Visible = !string.IsNullOrEmpty(txtHighlight.Text))
                    lblFoundItemCount.Text = String.Format("{0} مورد یافت شد.", count);
            }
        }


    }
}
