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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
