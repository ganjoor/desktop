using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ganjoor.Properties;

namespace ganjoor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Settings.Default.IsNewVersion)
            {
                Settings.Default.Upgrade();
                Settings.Default.IsNewVersion = false;
                Settings.Default.Save();
            }
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("خطای پیشبینی نشده‌ای روی سیستم شما رخ داد. لطفاً محتوای این پیغام را برای ایمیل ganjoor@ganjoor.net ارسال کنید تا دربارۀ آن تحقیق شود: \n با زدن کلید Ctrl+C می‌توانید متن این پنجره را کپی کنید.\n"+e.ExceptionObject.ToString(), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
        }
    }
}
