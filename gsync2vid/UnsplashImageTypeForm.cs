using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gsync2vid.Properties;

namespace gsync2vid
{
    public partial class UnsplashImageTypeForm : Form
    {
        public UnsplashImageTypeForm()
        {
            InitializeComponent();
            txtInput.Text = Settings.Default.UnsplashSearchTerm;
            txtSearchUrl.Text = Settings.Default.UnsplashSearchUrl;
            ImageFolderPath = "";
        }

        private void rd1_CheckedChanged(object sender, EventArgs e)
        {
            txtSearchUrl.Text = "https://source.unsplash.com/random/{0}x{1}";
        }

        private void rd2_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInput.Text))
            {
                MessageBox.Show("دسته بندی را مشخص کنید.");
                return;
            }
            txtSearchUrl.Text = "https://source.unsplash.com/category/" + txtInput.Text + "/{0}x{1}";
        }

        private void rd3_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInput.Text))
            {
                MessageBox.Show("دسته بندی را مشخص کنید.");
                return;
            }
            txtSearchUrl.Text = "https://source.unsplash.com/{0}x{1}/?" + txtInput.Text;

        }

        private void lnkUnsplash_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://source.unsplash.com/?utm_campaign=api-feature&utm_medium=referral&utm_source=unsplash");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Settings.Default.UnsplashSearchTerm = txtInput.Text;
            Settings.Default.UnsplashSearchUrl = txtSearchUrl.Text;
        }

        public string ImageFolderPath { get; set; }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                if(dlg.ShowDialog(this) == DialogResult.OK)
                {
                    this.ImageFolderPath = dlg.SelectedPath;
                    DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
