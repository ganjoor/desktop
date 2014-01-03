using System;
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
        public static string GetListMoreInfoUrl(string Url)
        {
            int index = Array.IndexOf(Urls, Url);
            if (index != -1)
            {
                if (index < _DefaultUrls.Length)
                    return string.Empty;//no default urls!
                else
                {
                    index = index - _DefaultUrls.Length;
                    if (Settings.Default.CustomDownloadListMoreInfoUrls != null && index < Settings.Default.CustomDownloadListMoreInfoUrls.Count)
                        return Settings.Default.CustomDownloadListMoreInfoUrls[index];
                }
            }
            return string.Empty;
        }
        public static bool Cache(string Url, string Name, string Description, string MoreInfoUrl)
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
                    Settings.Default.CustomDownloadListMoreInfoUrls[index] = MoreInfoUrl;
                    Settings.Default.Save();
                }
            }
            else
            {
                UnNullLists();
                Settings.Default.CustomDownloadUrls.Add(Url);
                Settings.Default.CustomDownloadListNames.Add(Name);
                Settings.Default.CustomDownloadListDescriptions.Add(Description);
                Settings.Default.CustomDownloadListMoreInfoUrls.Add(MoreInfoUrl);
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
            if (Settings.Default.CustomDownloadListMoreInfoUrls == null)
                Settings.Default.CustomDownloadListMoreInfoUrls = new System.Collections.Specialized.StringCollection();
        }
        #region Default Urls
        private static string[] _DefaultUrls = new string[]
        {
            "http://ganjoor.sourceforge.net/newgdbs.xml",
            "http://ganjoor.sourceforge.net/sitegdbs.xml",
            "http://ganjoor.sourceforge.net/programgdbs.xml",
            "http://i.ganjoor.net/android/androidgdbs.xml",
        };
        private static string[] _DefaultListNames = new string[]
        {
            "مجموعه‌های تازه",
            "شاعران سایت گنجور",
            "شاعران اختصاصی برنامه",
            "مجموعه‌های قابل دریافت برای گنجور اندروید",
        };
        private static string[] _DefaultListDescriptions = new string[]
        {
            "این فهرست مجموعه‌هایی را دربردارد که گاه به گاه به عنوان مجموعه‌های تازه از طریق سایت گنجور در دسترس قرار می‌گیرند. راه دیگر اطلاع از انتشار چنین مجموعه‌هایی؛ فعال کردن گزینهٔ متناظر برای پرس و جو به دنبال مجموعه‌های تازه در هنگام شروع برنامه در پنجرهٔ «پیکربندی» است.",
            "این فهرست شامل آثار شاعران سایت گنجور است. اگر از طریق ویرایشگر آثار شاعری را پاک کرده‌اید یا از ابتدا مجموعه‌ای با داده‌های محدود دریافت کرده‌اید می‌توانید با استفاده از این فهرست داده‌های برنامه‌تان را تکمیل کنید.",
            "این فهرست شامل آثاری است که به دلایل مختلف از طریق سایت گنجور در دسترس قرار ندارند. عموماً آثار شعرای معاصر و همینطور شعرایی که آثار ادبیشان در سایهٔ شهرتشان در زمینه‌های دیگر خواهان دارد از طریق سایت در دسترس قرار نمی‌گیرد.",
            "اگر با دریافت از سه فهرست پیش‌فرض مشکل دارید از این فهرست استفاده کنید.",
        };
        #endregion
    }
}
