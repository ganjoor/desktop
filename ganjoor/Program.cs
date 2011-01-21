﻿using System;
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
            GAdvisor.AdviseOnUnhandledException(e.ExceptionObject.ToString());
        }
    }
}
