using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using ganjoor.Properties;

namespace ganjoor
{
    public class DownloadableAudioListProcessor
    {
        /// <summary>
        /// دریافت کننده لیست خوانشها
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Exception"></param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> RetrieveList(string url, out string Exception)
        {
            var lst = new List<Dictionary<string, string>>();
            try
            {
                var req = WebRequest.Create(url);
                GConnectionManager.ConfigureProxy(ref req);
                using (var response = req.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            var doc = new XmlDocument();
                            doc.LoadXml(reader.ReadToEnd());


                            //Collect List:
                            var paudioNodes = doc.GetElementsByTagName("PoemAudio");
                            foreach (XmlNode gdbNode in paudioNodes)
                            {
                                var audioInfo = new Dictionary<string, string>();
                                foreach (XmlNode Node in gdbNode.ChildNodes)
                                {
                                    audioInfo.Add(Node.Name, Node.InnerText);
                                }
                                lst.Add(audioInfo);
                            }

                        }
                    }
                }
                Exception = string.Empty;
                return lst;
            }
            catch (Exception exp)
            {
                Exception = exp.Message;
                return null;
            }
        }


        /// <summary>
        /// دریافت فایل xml خوانش
        /// </summary>
        /// <param name="url"></param>
        /// <param name="targetFilePath"></param>
        /// <param name="overwrite"></param>
        /// <param name="Exception"></param>
        /// <returns></returns>
        public static bool DownloadAudioXml(string url, string targetDir, bool overwrite, out string Exception)
        {
            Exception = string.Empty;
            var uri = new Uri(url);
            var targetFilePath = Path.Combine(targetDir, Path.GetFileName(uri.LocalPath));

            try
            {
                if (File.Exists(targetFilePath))
                    if (!overwrite)
                    {
                        return true;
                    }
                    else
                    {
                        File.Delete(targetFilePath);
                    }
                var req = WebRequest.Create(url);
                GConnectionManager.ConfigureProxy(ref req);
                using var response = req.GetResponse();
                using var stream = response.GetResponseStream();
                using var reader = new StreamReader(stream);
                var doc = new XmlDocument(); //this is unnecessary, but at least does some kind of verification
                doc.LoadXml(reader.ReadToEnd());

                doc.Save(targetFilePath);

                return true;
            }
            catch (Exception exp)
            {
                Exception = exp.Message;
                return false;
            }
        }

        /// <summary>
        /// پیشنهاد عنوان از روی اطلاعات خوانش قابل دریافت
        /// </summary>
        /// <param name="audioInfo">اطلاعات خوانش</param>
        /// <returns>عنوان</returns>
        public static string SuggestTitle(Dictionary<string, string> audioInfo)
        {
            var title = audioInfo["audio_title"];
            title += " - به روایت ";
            title += audioInfo["audio_artist"];
            if (!string.IsNullOrWhiteSpace(audioInfo["audio_src"]))
            {
                title += " ";
                title += audioInfo["audio_src"];
            }
            return title;
        }

        /// <summary>
        /// پیشنهاد عنوان کوتاه از روی اطلاعات خوانش قابل دریافت
        /// </summary>
        /// <param name="audioInfo">اطلاعات خوانش</param>
        /// <returns>عنوان</returns>
        public static string SuggestShortTitle(Dictionary<string, string> audioInfo)
        {
            return audioInfo["audio_title"];
        }



        /// <summary>
        /// مسیر دریافت پیش فرض
        /// </summary>
        public static string SoundsPath
        {
            get
            {
                var reS = Settings.Default.SoundsPath;

                if (string.IsNullOrEmpty(reS))
                {
                    var ganjoorUserDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ganjoor");
                    if (!Directory.Exists(ganjoorUserDir))
                        Directory.CreateDirectory(ganjoorUserDir);
                    reS = Path.Combine(ganjoorUserDir, "sounds");
                    if (!Directory.Exists(reS))
                        Directory.CreateDirectory(reS);
                    Settings.Default.SoundsPath = reS;
                    Settings.Default.Save();

                }
                return reS;
            }
            set
            {
                Settings.Default.SoundsPath = value;
                Settings.Default.Save();
            }
        }

    }
}
