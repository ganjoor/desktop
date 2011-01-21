using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ganjoor
{
    class GAdvisor
    {
        public static void AdviseOnUnhandledException(string exception)
        {
            string expMessage = "خطای پیشبینی نشده‌ای روی سیستم شما رخ داد. لطفاً محتوای این پیغام را برای ایمیل ganjoor@ganjoor.net ارسال کنید تا دربارۀ آن تحقیق شود: \r\n با زدن کلید Ctrl+C می‌توانید متن این پنجره را کپی کنید.\r\n";
            expMessage += "Application Path = " + Application.ExecutablePath + Environment.NewLine;
            expMessage += "Windows Version = " + Environment.OSVersion.VersionString + Environment.NewLine;
            expMessage += ((IntPtr.Size == 8) ? "64 bit mode" : (IntPtr.Size == 4) ? "32 bit mode" : "unknown mode") + Environment.NewLine;
            expMessage += exception;
            MessageBox.Show(expMessage, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
        }
        public static void AdviseOnSQLiteDllNotfound()
        {
            string expMessage =
                    "یکی از اجزای گنجور رومیزی روی سیستم شما وجود ندارد یا نوع آن متناسب با سیستم عامل شما نیست. "
                    +
                    "این خطا معمولاً زمانی رخ می‌دهد که شما به جای نصب گنجور رومیزی آن را با کپی روی سیستم خود اجرا کرده‌اید ."
                    +
                    "گنجور رومیزی نرم‌افزاری آزاد و رایگان است و شما می‌توانید با مراجعه به نشانی "
                    +
                    "http://ganjoor.sourceforge.net "
                    +
                    "نصاب کامل و بدون ایراد آن را دریافت کنید. "
                    +
                    "گنجور رومیزی به دلیل رخداد این خطا نمی‌تواند روی سیستم شما اجرا شود و شما باید با دریافت و نصب نصاب کامل "
                    +
                    "این مشکل را حل کنید.";

            MessageBox.Show(expMessage, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);            
        }
    }
}
