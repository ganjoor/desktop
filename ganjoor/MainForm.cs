using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            ganjoorView.ShowHome(true);
        }

        private void ganjoorView_OnPageChanged(string PageString, bool CanGoNextPoem, bool CanGoPreviousPoem, bool HasComments, bool CanBrowse)
        {
            lblCurrentPage.Text = PageString;
            btnNextPoem.Enabled = CanGoNextPoem;
            btnPreviousPoem.Enabled = CanGoPreviousPoem;
            btnComments.Enabled = HasComments;
            btnPrint.Enabled = btnComments.Enabled;
            btnHistoryBack.Enabled = ganjoorView.CanGoBackInHistory;
            btnViewInSite.Enabled = CanBrowse;
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
                    ganjoorView.ShowSearchResults(dlg.Phrase, 0, 10);
                }
            }
        }

        private void btnCopyText_Click(object sender, EventArgs e)
        {
            ganjoorView.CopyText();
        }

    }
}
