using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class MrgTwoClmns : Form
    {
        public MrgTwoClmns()
        {
            InitializeComponent();
            clmn1.Focus();
        }

        public string ResulText
        {
            get
            {
                return result.Text;
            }
        }

        private void clmn1_TextChanged(object sender, EventArgs e)
        {
            btnMerge.Enabled = !string.IsNullOrEmpty(clmn1.Text.Trim()) && !string.IsNullOrEmpty(clmn2.Text.Trim());
            if (chkAutomaticFocus.Checked)
            {
                if (sender is TextBox)
                {
                    if ((sender as TextBox).Name == clmn1.Name)
                        clmn2.Focus();
                    else
                        btnMerge.Focus();
                }
            }
        }

        private void result_TextChanged(object sender, EventArgs e)
        {
            btnInsert.Enabled = !string.IsNullOrEmpty(result.Text.Trim());
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            bool err = false;
            int iLine1 = 0;
            int iLine2 = 0;
            string resultText = result.Text;
            if (!string.IsNullOrEmpty(resultText.Trim()))
                if(resultText[resultText.Length - 1] != '\n')
                    resultText += "\r\n";
            while(iLine1 < clmn1.Lines.Length)
            {
                bool ignore = false;
                if (chkIgnoreBlankLines.Checked)
                {
                    if (string.IsNullOrEmpty(clmn1.Lines[iLine1].Trim()))
                        ignore = true;
                }
                if (!ignore)
                {
                    resultText += clmn1.Lines[iLine1] + "\r\n";                    
                }

                if (iLine2 >= clmn2.Lines.Length)
                    err = true;

                while (iLine2 < clmn2.Lines.Length)
                {
                    ignore = false;
                    if (chkIgnoreBlankLines.Checked)
                    {
                        if (string.IsNullOrEmpty(clmn2.Lines[iLine2].Trim()))
                            ignore = true;
                    }
                    if (!ignore)
                    {
                        resultText += clmn2.Lines[iLine2] + "\r\n";
                        iLine2++;
                        break;
                    }
                    iLine2++;
                }
                iLine1++;
            }

            while (iLine2 < clmn2.Lines.Length)
            {
                err = true;
                bool ignore = false;
                if (chkIgnoreBlankLines.Checked)
                {
                    if (string.IsNullOrEmpty(clmn2.Lines[iLine2].Trim()))
                        ignore = true;
                }
                if (!ignore)
                {
                    resultText += clmn2.Lines[iLine2] + "\r\n";                    
                }
                iLine2++;
            }

            result.Text = resultText;

            if (err)
                MessageBox.Show("تعداد خطوط ستون اول و دوم با هم همخوانی نداشت. لطفاً نتیجه را جهت پیدا کردن خطا بررسی کنید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                clmn1.Text = clmn2.Text = "";
                btnInsert.Focus();
            }
            
        }
    }
}
