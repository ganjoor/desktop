using ganjoor.Properties;
using System;
using System.Windows.Forms;

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
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            GAdvisor.AdviseOnUnhandledException(e.ExceptionObject.ToString());
        }
    }
}
