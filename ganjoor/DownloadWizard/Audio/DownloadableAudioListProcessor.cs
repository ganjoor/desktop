using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;


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
    }
}
