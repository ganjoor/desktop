using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            ViewFont = Properties.Settings.Default.ViewFont;
            lblFont.Text = ViewFont.Name + "(" +ViewFont.Style.ToString()+") " + ViewFont.Size.ToString();
            chkHighlightSearchResults.Checked = Properties.Settings.Default.HighlightKeyword;
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ViewFont = ViewFont;
            Properties.Settings.Default.HighlightKeyword = chkHighlightSearchResults.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
