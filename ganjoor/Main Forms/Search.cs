using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class Search : Form
    {
        public Search()
        {
            InitializeComponent();
            numItemsInPage.Value = Properties.Settings.Default.SearchPageItems;
            txtPhrase.Text = Properties.Settings.Default.LastSearchPhrase;
        }
        public string Phrase
        {
            get
            {
                return txtPhrase.Text.Replace('ك', 'ک').Replace('ي', 'ی');
            }
        }
        public int ItemsInPage
        {
            get
            {
                return (int)numItemsInPage.Value;
            }
        }
        public string[] Poets
        {
            set
            {
                cmbPoets.Items.AddRange(value);
            }
        }
        public int PoetOrder
        {
            get
            {
                return cmbPoets.SelectedIndex;
            }
            set
            {
                cmbPoets.SelectedIndex = value;
            }
        }


        private void txtPhrase_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Enabled = txtPhrase.Text.Length > 0;
        }
    }
}
