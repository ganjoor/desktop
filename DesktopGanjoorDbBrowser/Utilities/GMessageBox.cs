using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ganjoor.Utilities
{
    /// <summary>
    /// مشکلات راست به چپ و کد تکراری لازم را این کدها کمتر می‌کنند
    /// </summary>
    public static class GMessageBox
    {
        public static void SayError(string strMessage, string strCaption = "خطا")
        {
            MessageBox.Show(strMessage, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }

        public static void Announce(string strMessage, string strCaption = "اعلان")
        {
            MessageBox.Show(strMessage, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }

        public static DialogResult Ask(string strQuestion, string strCaption = "تأییدیه")
        {
            return MessageBox.Show(strQuestion, strCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }

    }
}
