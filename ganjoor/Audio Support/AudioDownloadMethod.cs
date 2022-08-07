using System;
using System.Windows.Forms;

namespace ganjoor.Audio_Support
{
    public partial class AudioDownloadMethod : Form
    {
        public AudioDownloadMethod()
        {
            InitializeComponent();
            PoetId = 0;
        }


        private void rdSelected_CheckedChanged(object sender, EventArgs e)
        {
            btnSelect.Enabled = rdSelected.Checked;
        }

        public int PoetId { get; set; }

        public int CatId { get; set; }

        public string SearchTerm { get; set; }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            var db = new DbBrowser();
            using var poetSeletor = new ItemSelector("انتخاب شاعر", db.Poets.ToArray(), null);
            if (poetSeletor.ShowDialog(this) == DialogResult.OK)
            {
                var poet = poetSeletor.SelectedItem as GanjoorPoet;
                PoetId = poet._ID;
                txtSelectedPoetOrCategory.Text = poet._Name;
                if (MessageBox.Show(
                        $"آیا تمایل دارید خوانش‌های بخش خاصی از آثار {poet._Name} را دریافت کنید؟",
                        "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
                        MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign) == DialogResult.No)
                {
                    db.CloseDb();

                    return;
                }

                using var catSelector = new CategorySelector(poet._ID, db);
                if (catSelector.ShowDialog(this) == DialogResult.OK)
                {
                    CatId = catSelector.SelectedCatID;
                    txtSelectedPoetOrCategory.Text = $"{poet._Name} » {db.GetCategory(CatId)._Text}";
                    db.CloseDb();
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SearchTerm = txtSearchTerm.Text;
            if (CatId != 0)
            {
                PoetId = 0;
            }
        }
    }
}
