using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class ReplaceInDb : Form
    {
        public ReplaceInDb()
        {
            InitializeComponent();

            _db = new DbBrowser();
        }

        private DbBrowser _db;

        private void btnYaKaf_Click(object sender, EventArgs e)
        {            

            _db.BeginBatchOperation();
            _db.Replace("ي", "ی");
            _db.Replace("ك", "ک");

            _db.CommitBatchOperation();

            MessageBox.Show("انجام شد!", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1
                , MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            
        }

        private void btnHeye_Click(object sender, EventArgs e)
        {
            _db.BeginBatchOperation();
            _db.Replace("هٔ", "هٔ");
            _db.Replace("ه‌ی", "هٔ");

            _db.CommitBatchOperation();

            MessageBox.Show("انجام شد!", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1
                , MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);

        }

        private void btnNumbers_Click(object sender, EventArgs e)
        {
            _db.BeginBatchOperation();

            _db.Replace("0", "۰");
            _db.Replace("1", "۱");
            _db.Replace("2", "۲");
            _db.Replace("3", "۳");
            _db.Replace("4", "۴");
            _db.Replace("5", "۵");
            _db.Replace("6", "۶");
            _db.Replace("7", "۷");
            _db.Replace("8", "۸");
            _db.Replace("9", "۹");

            _db.CommitBatchOperation();

            MessageBox.Show("انجام شد!", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1
                , MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);

        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            _db.BeginBatchOperation();

            try
            {
                _db.Replace(txtFind.Text, txtRep.Text);

                _db.CommitBatchOperation();

                MessageBox.Show("انجام شد!", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1
                    , MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOther_Click(object sender, EventArgs e)
        {
            _db.BeginBatchOperation();

            _db.Replace("ـ", "");
            _db.Replace("  ", " "); _db.Replace("  ", " "); _db.Replace("  ", " "); _db.Replace("  ", " "); _db.Replace("  ", " "); _db.Replace("  ", " ");
            _db.Replace(" !", "!");
            _db.Replace(" ؟", "؟");
            _db.Replace(" ،", "،");


            _db.CommitBatchOperation();

            MessageBox.Show("انجام شد!", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1
                , MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }
    }
}
