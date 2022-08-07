using System;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class TextImporter : Form
    {
        public TextImporter()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e) {
            using OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Unicode Text File (*.txt)|*.txt";
            if (dlg.ShowDialog(this) != DialogResult.OK)
            {
                DialogResult = DialogResult.None;
                return;
            }

            FileName = dlg.FileName;
            MainCatText = txtMainCat.Text;
            SubCatTexts = txtSubCats.Lines;
            MaxVerseTextLength = (int)numMaxVerse.Value;
            TabularVerses = chkTabularVerses.Checked;
        }

        public string FileName
        {
            get;
            private set;
        }

        public string MainCatText
        {
            get;
            private set;
        }

        public string[] SubCatTexts
        {
            get;
            private set;
        }

        public int MaxVerseTextLength
        {
            get;
            private set;
        }

        public bool TabularVerses
        {
            get;
            private set;
        }
    }
}
