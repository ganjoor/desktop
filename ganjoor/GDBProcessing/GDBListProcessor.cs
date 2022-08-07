using ganjoor.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Xml;

namespace ganjoor
{
    /// <summary>
    /// جهت ذخیره، دریافت و پردازش مخازن مجموعه های شعرها به کار می رود
    /// </summary>
    public class GDBListProcessor
    {
        /// <summary>
        /// ذخیرۀ لیستی از GDBInfoها در یک فایل xml
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="Name"></param>
        /// <param name="Description"></param>
        /// <param name="MoreInfoUrl"></param>
        /// <param name="List"></param>
        /// <returns></returns>
        public static bool Save(string FileName, string Name, string Description, string MoreInfoUrl, List<GDBInfo> List)
        {
            XmlDocument doc = new();
            XmlNode gdbRootNode = doc.CreateNode(XmlNodeType.Element, "DesktopGanjoorGDBList", "");
            doc.AppendChild(gdbRootNode);
            XmlNode newNode = doc.CreateNode(XmlNodeType.Element, "RedirectInfo", "");
            XmlNode redirUrl = doc.CreateNode(XmlNodeType.Element, "Url", "");
            newNode.AppendChild(redirUrl);
            gdbRootNode.AppendChild(newNode);
            if (!string.IsNullOrEmpty(Name))
            {
                newNode = doc.CreateNode(XmlNodeType.Element, "Name", "");
                newNode.InnerText = Name;
                gdbRootNode.AppendChild(newNode);
            }
            if (!string.IsNullOrEmpty(Description))
            {
                newNode = doc.CreateNode(XmlNodeType.Element, "Description", "");
                newNode.InnerText = Description;
                gdbRootNode.AppendChild(newNode);
            }
            if (!string.IsNullOrEmpty(MoreInfoUrl))
            {
                newNode = doc.CreateNode(XmlNodeType.Element, "MoreInfoUrl", "");
                newNode.InnerText = MoreInfoUrl;
                gdbRootNode.AppendChild(newNode);
            }
            foreach (GDBInfo gdb in List)
            {
                if (!string.IsNullOrEmpty(gdb.DownloadUrl))
                {
                    XmlNode gdbNode = doc.CreateNode(XmlNodeType.Element, "gdb", "");
                    foreach (PropertyInfo prop in typeof(GDBInfo).GetProperties())
                    {
                        bool ignoreProp = false;
                        XmlNode propNode = doc.CreateNode(XmlNodeType.Element, prop.Name, "");
                        if (prop.PropertyType == typeof(string))
                        {
                            string value = prop.GetValue(gdb, null).ToString();
                            if (string.IsNullOrEmpty(value))
                            {
                                ignoreProp = true;
                            }
                            else
                                propNode.InnerText = value;
                        }
                        else
                            if (prop.PropertyType == typeof(Int32))
                        {
                            int value = Convert.ToInt32(prop.GetValue(gdb, null));
                            if (value == 0)
                            {
                                ignoreProp = true;
                            }
                            else
                                propNode.InnerText = value.ToString();
                        }
                        else
                                if (prop.PropertyType == typeof(DateTime))
                        {
                            try
                            {
                                propNode.InnerText = ((DateTime)prop.GetValue(gdb, null)).ToString("yyyy-MM-dd");
                            }
                            catch//fix it!
                            {
                                propNode.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                            }
                        }
                        if (!ignoreProp)
                            gdbNode.AppendChild(propNode);
                    }
                    gdbRootNode.AppendChild(gdbNode);
                }

            }
            try
            {
                doc.Save(FileName);
                return true;
            }
            catch
            {

                return false;
            }
        }

        /// <summary>
        /// دریافت یک فایل xml و نمایش اطلاعات اولیه آن
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Name"></param>
        /// <param name="Description"></param>
        /// <param name="MoreInfoUrl"></param>
        /// <returns></returns>
        public static bool RetrieveProperties(string url, out string Name, out string Description, out string MoreInfoUrl)
        {
            Name = Description = MoreInfoUrl = "";
            try
            {
                WebRequest req = WebRequest.Create(url);
                GConnectionManager.ConfigureProxy(ref req);
                using (WebResponse response = req.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new(stream))
                        {
                            XmlDocument doc = new();
                            doc.LoadXml(reader.ReadToEnd());

                            //Should Redirect?
                            XmlNodeList ndListRedirect = doc.GetElementsByTagName("RedirectInfo");
                            if (ndListRedirect.Count > 0)
                            {
                                XmlNode redirectNode = ndListRedirect[0];
                                foreach (XmlNode Node in redirectNode.ChildNodes)
                                {
                                    if (Node.Name == "Url")
                                        if (!string.IsNullOrEmpty(Node.InnerText))
                                        {
                                            return RetrieveProperties(Node.InnerText, out Name, out Description, out MoreInfoUrl);
                                        }
                                }
                            }

                            XmlNodeList nameNodeList = doc.GetElementsByTagName("Name");
                            if (nameNodeList != null && nameNodeList.Count == 1)
                                Name = nameNodeList[0].InnerText;
                            XmlNodeList descNodeList = doc.GetElementsByTagName("Description");
                            if (descNodeList != null && descNodeList.Count == 1)
                                Description = descNodeList[0].InnerText;
                            XmlNodeList urlNodeList = doc.GetElementsByTagName("MoreInfoUrl");
                            if (urlNodeList != null && urlNodeList.Count == 1)
                                MoreInfoUrl = urlNodeList[0].InnerText;
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
        /// <summary>
        /// دریافت یک فایل xml و تبدیل آن به لیستی از GDBInfoها
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Exception"></param>
        /// <returns></returns>
        public static List<GDBInfo> RetrieveList(string url, out string Exception)
        {
            List<GDBInfo> lstGDBs = new();
            try
            {
                WebRequest req = WebRequest.Create(url);
                GConnectionManager.ConfigureProxy(ref req);
                using (WebResponse response = req.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new(stream))
                        {
                            XmlDocument doc = new();
                            doc.LoadXml(reader.ReadToEnd());

                            //Should Redirect?
                            XmlNodeList ndListRedirect = doc.GetElementsByTagName("RedirectInfo");
                            if (ndListRedirect.Count > 0)
                            {
                                XmlNode redirectNode = ndListRedirect[0];
                                foreach (XmlNode Node in redirectNode.ChildNodes)
                                {
                                    if (Node.Name == "Url")
                                        if (!string.IsNullOrEmpty(Node.InnerText))
                                        {
                                            return RetrieveList(Node.InnerText, out Exception);
                                        }
                                }
                            }

                            //Collect List:
                            lstGDBs.Clear();
                            XmlNodeList gdbNodes = doc.GetElementsByTagName("gdb");
                            foreach (XmlNode gdbNode in gdbNodes)
                            {
                                GDBInfo gdbInfo = new();
                                foreach (XmlNode Node in gdbNode.ChildNodes)
                                {
                                    switch (Node.Name)
                                    {
                                        case "CatName":
                                            gdbInfo.CatName = Node.InnerText;
                                            break;
                                        case "PoetID":
                                            gdbInfo.PoetID = Convert.ToInt32(Node.InnerText);
                                            break;
                                        case "CatID":
                                            gdbInfo.CatID = Convert.ToInt32(Node.InnerText);
                                            break;
                                        case "DownloadUrl":
                                            gdbInfo.DownloadUrl = Node.InnerText;
                                            break;
                                        case "BlogUrl":
                                            gdbInfo.BlogUrl = Node.InnerText;
                                            break;
                                        case "FileExt":
                                            gdbInfo.FileExt = Node.InnerText;
                                            break;
                                        case "ImageUrl":
                                            gdbInfo.ImageUrl = Node.InnerText;
                                            break;
                                        case "FileSizeInByte":
                                            gdbInfo.FileSizeInByte = Convert.ToInt32(Node.InnerText);
                                            break;
                                        case "LowestPoemID":
                                            gdbInfo.LowestPoemID = Convert.ToInt32(Node.InnerText);
                                            break;
                                        case "PubDate":
                                            {
                                                string[] dateParts = Node.InnerText.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                                                int Year = Convert.ToInt32(dateParts[0]);
                                                int Month = Convert.ToInt32(dateParts[1]);
                                                int Day = Convert.ToInt32(dateParts[2]);
                                                gdbInfo.PubDate = new DateTime(Year, Month, Day);
                                            }
                                            break;

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
            catch (Exception exp)
            {
                Exception = exp.Message;
                return null;
            }
        }
        /// <summary>
        /// مسیر دریافت پیش فرض
        /// </summary>
        public static string DownloadPath
        {
            get
            {
                string reS = Settings.Default.DownloadPath;

                if (string.IsNullOrEmpty(reS))
                {
                    string ganjoorUserDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ganjoor");
                    if (!Directory.Exists(ganjoorUserDir))
                        Directory.CreateDirectory(ganjoorUserDir);
                    reS = Path.Combine(ganjoorUserDir, "download");
                    if (!Directory.Exists(reS))
                        Directory.CreateDirectory(reS);
                    Settings.Default.DownloadPath = reS;
                    Settings.Default.Save();

                }
                return reS;
            }
            set
            {
                Settings.Default.DownloadPath = value;
                Settings.Default.Save();
            }
        }
    }
}
