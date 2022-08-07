using System;
using System.Windows.Forms;
using ganjoor.Properties;

namespace ganjoor
{
    public partial class Search : Form
    {
        public Search()
        {
            InitializeComponent();
            numItemsInPage.Value = Settings.Default.SearchPageItems;
            txtPhrase.Text = Settings.Default.LastSearchPhrase;
            comboBoxSearchType.SelectedIndex = Settings.Default.LastSearchType;
            comboBoxSearchLocationType.SelectedIndex = Settings.Default.LastSearchLocationType;
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

        public int SearchType
        {
            get => comboBoxSearchType.SelectedIndex;
            set => comboBoxSearchType.SelectedIndex = value;
        }

        public int SearchLocationType
        {
            get => comboBoxSearchLocationType.SelectedIndex;
            set => comboBoxSearchLocationType.SelectedIndex = value;
        }

        private void txtPhrase_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Enabled = txtPhrase.Text.Length > 0;
        }

        private void Search_Load(object sender, EventArgs e)
        {

        }
    }
}
