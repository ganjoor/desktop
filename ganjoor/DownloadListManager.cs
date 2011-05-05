﻿using System;
using System.Collections.Generic;
using System.Text;
using ganjoor.Properties;

namespace ganjoor
{
    class DownloadListManager
    {
        public static string[] Urls
        {
            get
            {
                List<string> reS = new List<string>();
                reS.AddRange(_DefaultUrls);
                if(Settings.Default.CustomDownloadUrls != null)
                foreach (string CustomDownloadUrl in Settings.Default.CustomDownloadUrls)
                    reS.Add(CustomDownloadUrl);
                return reS.ToArray();
            }
        }
        public static string GetListName(string Url)
        {
            int index = Array.IndexOf(Urls, Url);
            if (index != -1)
            {
                if (index < _DefaultUrls.Length)
                    return _DefaultListNames[index];
                else
                {
                    index = index - _DefaultUrls.Length;
                    if(Settings.Default.CustomDownloadListNames != null && index < Settings.Default.CustomDownloadListNames.Count)
                        return Settings.Default.CustomDownloadListNames[index];
                }
            }
            return string.Empty;
        }
        public static string GetListDescription(string Url)
        {
            int index = Array.IndexOf(Urls, Url);
            if (index != -1)
            {
                if (index < _DefaultUrls.Length)
                    return _DefaultListDescriptions[index];
                else
                {
                    index = index - _DefaultUrls.Length;
                    if (Settings.Default.CustomDownloadListDescriptions != null && index < Settings.Default.CustomDownloadListDescriptions.Count)
                        return Settings.Default.CustomDownloadListDescriptions[index];
                }
            }
            return string.Empty;
        }
        public static bool Cache(string Url, string Name, string Description)
        {
            int index = Array.IndexOf(Urls, Url);
            if (index != -1)
            {
                if (index < _DefaultUrls.Length)
                    return false;
                else
                {
                    index = index - _DefaultUrls.Length;
                    Settings.Default.CustomDownloadListNames[index] = Name;
                    Settings.Default.CustomDownloadListDescriptions[index] = Description;
                    Settings.Default.Save();
                }
            }
            else
            {
                UnNullLists();
                Settings.Default.CustomDownloadUrls.Add(Url);
                Settings.Default.CustomDownloadListNames.Add(Name);
                Settings.Default.CustomDownloadListDescriptions.Add(Description);
                Settings.Default.Save();
            }
            return true;
        }

        private static void UnNullLists()
        {
            if (Settings.Default.CustomDownloadUrls == null)
                Settings.Default.CustomDownloadUrls = new System.Collections.Specialized.StringCollection();
            if (Settings.Default.CustomDownloadListNames == null)
                Settings.Default.CustomDownloadListNames = new System.Collections.Specialized.StringCollection();
            if (Settings.Default.CustomDownloadListDescriptions == null)
                Settings.Default.CustomDownloadListDescriptions = new System.Collections.Specialized.StringCollection();
        }
        #region Default Urls
        private static string[] _DefaultUrls = new string[]
        {
            "http://ganjoor.sourceforge.net/newgdbs.xml",
            "http://ganjoor.sourceforge.net/sitegdbs.xml",
            "http://ganjoor.sourceforge.net/programgdbs.xml",
        };
        private static string[] _DefaultListNames = new string[]
        {
            "مجموعه‌های تازه",
            "شاعران سایت گنجور",
            "شاعران اختصاصی برنامه",
        };
        private static string[] _DefaultListDescriptions = new string[]
        {
            "مجموعه‌های تازه",
            "شاعران سایت گنجور",
            "شاعران اختصاصی برنامه",
        };
        #endregion
    }
}
