using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;
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
            List<Dictionary<string, string>> lst = new List<Dictionary<string, string>>();
            try
            {
                WebRequest req = WebRequest.Create(url);
                GConnectionManager.ConfigureProxy(ref req);
                using (WebResponse response = req.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(reader.ReadToEnd());


                            //Collect List:
                            XmlNodeList paudioNodes = doc.GetElementsByTagName("PoemAudio");
                            foreach (XmlNode gdbNode in paudioNodes)
                            {
                                Dictionary<string, string> audioInfo = new Dictionary<string, string>();
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
            Uri uri = new Uri(url);
            string targetFilePath = Path.Combine(targetDir, Path.GetFileName(uri.LocalPath));

            try
            {
                if(File.Exists(targetFilePath))
                    if (!overwrite)
                    {
                        return true;
                    }
                    else
                    {
                        File.Delete(targetFilePath);
                    }
                WebRequest req = WebRequest.Create(url);
                GConnectionManager.ConfigureProxy(ref req);
                using (WebResponse response = req.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {

                            XmlDocument doc = new XmlDocument(); //this is unnecessary, but at least does some kind of verification
                            doc.LoadXml(reader.ReadToEnd());                            

                            doc.Save(targetFilePath);

                        }
                    }
                }                
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
            string title = audioInfo["audio_title"];
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
                string reS = Settings.Default.SoundsPath;

                if (string.IsNullOrEmpty(reS))
                {
                    string ganjoorUserDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ganjoor");
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
