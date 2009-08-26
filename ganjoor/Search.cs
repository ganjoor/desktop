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
        }
        public string Phrase
        {
            get
            {
                return txtPhrase.Text;
            }
        }
        public int ItemsInPage
        {
            get
            {
                return (int)numItemsInPage.Value;
            }
        }
    }
}
