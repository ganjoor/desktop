using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;
using ganjoor.Properties;

namespace ganjoor
{
    public class GDBInfo
    {
        public string CatName { get; set; }
        public int PoetID { get; set; }
        public int CatID { get; set; }
        public string DownloadUrl { get; set; }
        public string BlogUrl { get; set; }
        public string FileExt { get; set; }
        public DateTime PubDate { get; set; }

        public static bool RetrieveGDBListProperties(string url, out string Name, out string Description)
        {
            Name = Description = "";
            try
            {
                WebRequest req = WebRequest.Create(url);
                ConfigureProxy(ref req);
                using (WebResponse response = req.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(reader.ReadToEnd());

                            //Should Redirect?
                            XmlNode redirectNode = doc.GetElementsByTagName("RedirectInfo")[0];
                            foreach (XmlNode Node in redirectNode.ChildNodes)
                            {
                                if (Node.Name == "Url")
                                    if (!string.IsNullOrEmpty(Node.InnerText))
                                    {
                                        return RetrieveGDBListProperties(Node.InnerText, out Name, out Description);
                                    }
                            }

                            Name = doc.GetElementsByTagName("Name")[0].InnerText;
                            Description = doc.GetElementsByTagName("Description")[0].InnerText;
                            return true;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

        }


        public static List<GDBInfo> RetrieveNewGDBList(string url, out string Exception)
        {
            List<GDBInfo> lstGDBs = new List<GDBInfo>();
            try
            {
                WebRequest req = WebRequest.Create(url);
                ConfigureProxy(ref req);
                using (WebResponse response = req.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(reader.ReadToEnd());

                            //Should Redirect?
                            XmlNode redirectNode = doc.GetElementsByTagName("RedirectInfo")[0];
                            foreach (XmlNode Node in redirectNode.ChildNodes)
                            {
                                if (Node.Name == "Url")
                                    if (!string.IsNullOrEmpty(Node.InnerText))
                                    {
                                        return RetrieveNewGDBList(Node.InnerText, out Exception);
                                    }
                            }

                            //Collect List:
                            lstGDBs.Clear();
                            XmlNodeList gdbNodes = doc.GetElementsByTagName("gdb");
                            foreach (XmlNode gdbNode in gdbNodes)
                            {
                                GDBInfo gdbInfo = new GDBInfo();
                                foreach (XmlNode Node in gdbNode.ChildNodes)
                                {
                                    if (Node.Name == "CatName")
                                        gdbInfo.CatName = Node.InnerText;
                                    else
                                        if (Node.Name == "PoetID")
                                            gdbInfo.PoetID = Convert.ToInt32(Node.InnerText);
                                        else
                                            if (Node.Name == "CatID")
                                                gdbInfo.CatID = Convert.ToInt32(Node.InnerText);
                                            else
                                                if (Node.Name == "DownloadUrl")
                                                    gdbInfo.DownloadUrl = Node.InnerText;
                                                else
                                                    if (Node.Name == "BlogUrl")
                                                        gdbInfo.BlogUrl = Node.InnerText;
                                                    else
                                                        if (Node.Name == "FileExt")
                                                            gdbInfo.FileExt = Node.InnerText;
                                                        else
                                                            if (Node.Name == "PubDate")
                                                            {
                                                                string[] dateParts = Node.InnerText.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                                                                int Year = Convert.ToInt32(dateParts[0]);
                                                                int Month = Convert.ToInt32(dateParts[1]);
                                                                int Day = Convert.ToInt32(dateParts[2]);
                                                                gdbInfo.PubDate = new DateTime(Year, Month, Day);
                                                            }
                                }
                                lstGDBs.Add(gdbInfo);
                            }

                        }
                    }
                }
                Exception = string.Empty;
                return lstGDBs;
            }
            catch(Exception exp)
            {
                Exception = exp.Message;
                return null;
            }
        }

        public static bool ConfigureProxy(ref WebRequest req)
        {
            if (!string.IsNullOrEmpty(Settings.Default.HttpProxyServer) && !string.IsNullOrEmpty(Settings.Default.HttpProxyPort))
            {
                int port = Convert.ToInt32(Settings.Default.HttpProxyPort);//try?!
                WebProxy proxy = new WebProxy(Settings.Default.HttpProxyServer, port);
                req.Proxy = proxy;
                return true;
            }
            return true;
        }

    }
}
