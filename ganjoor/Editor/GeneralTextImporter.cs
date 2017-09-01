using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class GeneralTextImporter : Form
    {
        public GeneralTextImporter()
        {
            InitializeComponent();
        }

        public string FileName
        {
            get;
            private set;
        }

        public string NextPoemStartText
        {
            get;
            private set;
        }

        public bool NextPoemStartIsAShortText
        {
            get;
            private set;
        }

        public int ShortTextLength
        {
            get
            {
                return 10;
            }
        }

        private void chkStartShort_CheckedChanged(object sender, EventArgs e)
        {
            txtNextPoemStartText.Enabled = !chkStartShort.Checked;
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Unicode Text File (*.txt)|*.txt";
                if (dlg.ShowDialog(this) != DialogResult.OK)
                {
                    DialogResult = DialogResult.None;
                    return;
                }

                FileName = dlg.FileName;
                NextPoemStartText = txtNextPoemStartText.Text;
                NextPoemStartIsAShortText = chkStartShort.Checked;

            }
        }

    }


}
