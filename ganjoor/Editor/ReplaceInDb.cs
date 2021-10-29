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
            _db.Replace("ي", "ی", chkOnlyPoemTitles.Checked);
            _db.Replace("ك", "ک", chkOnlyPoemTitles.Checked);

            _db.CommitBatchOperation();

            MessageBox.Show("انجام شد!", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1
                , MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            
        }

        private void btnHeye_Click(object sender, EventArgs e)
        {
            _db.BeginBatchOperation();
            _db.Replace("هٔ", "هٔ", chkOnlyPoemTitles.Checked);
            _db.Replace("ه‌ی", "هٔ", chkOnlyPoemTitles.Checked);

            _db.CommitBatchOperation();

            MessageBox.Show("انجام شد!", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1
                , MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);

        }

        private void btnNumbers_Click(object sender, EventArgs e)
        {
            _db.BeginBatchOperation();

            _db.Replace("0", "۰", chkOnlyPoemTitles.Checked);
            _db.Replace("1", "۱", chkOnlyPoemTitles.Checked);
            _db.Replace("2", "۲", chkOnlyPoemTitles.Checked);
            _db.Replace("3", "۳", chkOnlyPoemTitles.Checked);
            _db.Replace("4", "۴", chkOnlyPoemTitles.Checked);
            _db.Replace("5", "۵", chkOnlyPoemTitles.Checked);
            _db.Replace("6", "۶", chkOnlyPoemTitles.Checked);
            _db.Replace("7", "۷", chkOnlyPoemTitles.Checked);
            _db.Replace("8", "۸", chkOnlyPoemTitles.Checked);
            _db.Replace("9", "۹", chkOnlyPoemTitles.Checked);

            _db.Replace("٠", "۰", chkOnlyPoemTitles.Checked);
            _db.Replace("١", "۱", chkOnlyPoemTitles.Checked);
            _db.Replace("٢", "۲", chkOnlyPoemTitles.Checked);
            _db.Replace("٣", "۳", chkOnlyPoemTitles.Checked);
            _db.Replace("٤", "۴", chkOnlyPoemTitles.Checked);
            _db.Replace("٥", "۵", chkOnlyPoemTitles.Checked);
            _db.Replace("٦", "۶", chkOnlyPoemTitles.Checked);
            _db.Replace("٧", "۷", chkOnlyPoemTitles.Checked);
            _db.Replace("٨", "۸", chkOnlyPoemTitles.Checked);
            _db.Replace("٩", "۹", chkOnlyPoemTitles.Checked);

            _db.CommitBatchOperation();

            MessageBox.Show("انجام شد!", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1
                , MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);

        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            _db.BeginBatchOperation();

            try
            {
                _db.Replace(txtFind.Text, txtRep.Text, chkOnlyPoemTitles.Checked);

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

            _db.Replace("ـ", "", chkOnlyPoemTitles.Checked);
            _db.Replace("  ", " ", chkOnlyPoemTitles.Checked); _db.Replace("  ", " ", chkOnlyPoemTitles.Checked); _db.Replace("  ", " ", chkOnlyPoemTitles.Checked); _db.Replace("  ", " ", chkOnlyPoemTitles.Checked); _db.Replace("  ", " ", chkOnlyPoemTitles.Checked); _db.Replace("  ", " ", chkOnlyPoemTitles.Checked);
            _db.Replace(" !", "!", chkOnlyPoemTitles.Checked);
            _db.Replace(" ؟", "؟", chkOnlyPoemTitles.Checked);
            _db.Replace(" ،", "،", chkOnlyPoemTitles.Checked);


            _db.CommitBatchOperation();

            MessageBox.Show("انجام شد!", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1
                , MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }
    }
}
