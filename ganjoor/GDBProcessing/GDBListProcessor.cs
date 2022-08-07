using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Xml;
using ganjoor.Properties;

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
            var gdbRootNode = doc.CreateNode(XmlNodeType.Element, "DesktopGanjoorGDBList", "");
            doc.AppendChild(gdbRootNode);
            var newNode = doc.CreateNode(XmlNodeType.Element, "RedirectInfo", "");
            var redirUrl = doc.CreateNode(XmlNodeType.Element, "Url", "");
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
            foreach (var gdb in List)
            {
                if (!string.IsNullOrEmpty(gdb.DownloadUrl))
                {
                    var gdbNode = doc.CreateNode(XmlNodeType.Element, "gdb", "");
                    foreach (var prop in typeof(GDBInfo).GetProperties())
                    {
                        var ignoreProp = false;
                        var propNode = doc.CreateNode(XmlNodeType.Element, prop.Name, "");
                        if (prop.PropertyType == typeof(string))
                        {
                            var value = prop.GetValue(gdb, null).ToString();
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
                            var value = Convert.ToInt32(prop.GetValue(gdb, null));
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
                var req = WebRequest.Create(url);
                GConnectionManager.ConfigureProxy(ref req);
                using var response = req.GetResponse();
                using var stream = response.GetResponseStream();
                using StreamReader reader = new(stream);
                XmlDocument doc = new();
                doc.LoadXml(reader.ReadToEnd());

                //Should Redirect?
                var ndListRedirect = doc.GetElementsByTagName("RedirectInfo");
                if (ndListRedirect.Count > 0)
                {
                    var redirectNode = ndListRedirect[0];
                    foreach (XmlNode Node in redirectNode.ChildNodes)
                    {
                        if (Node.Name == "Url")
                            if (!string.IsNullOrEmpty(Node.InnerText))
                            {
                                return RetrieveProperties(Node.InnerText, out Name, out Description, out MoreInfoUrl);
                            }
                    }
                }

                var nameNodeList = doc.GetElementsByTagName("Name");
                if (nameNodeList is {Count: 1})
                    Name = nameNodeList[0].InnerText;
                var descNodeList = doc.GetElementsByTagName("Description");
                if (descNodeList is {Count: 1})
                    Description = descNodeList[0].InnerText;
                var urlNodeList = doc.GetElementsByTagName("MoreInfoUrl");
                if (urlNodeList is {Count: 1})
                    MoreInfoUrl = urlNodeList[0].InnerText;
                return true;
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
                var req = WebRequest.Create(url);
                GConnectionManager.ConfigureProxy(ref req);
                using (var response = req.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new(stream))
                        {
                            XmlDocument doc = new();
                            doc.LoadXml(reader.ReadToEnd());

                            //Should Redirect?
                            var ndListRedirect = doc.GetElementsByTagName("RedirectInfo");
                            if (ndListRedirect.Count > 0)
                            {
                                var redirectNode = ndListRedirect[0];
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
                            var gdbNodes = doc.GetElementsByTagName("gdb");
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
                                                var dateParts = Node.InnerText.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                                                var Year = Convert.ToInt32(dateParts[0]);
                                                var Month = Convert.ToInt32(dateParts[1]);
                                                var Day = Convert.ToInt32(dateParts[2]);
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
                var reS = Settings.Default.DownloadPath;

                if (string.IsNullOrEmpty(reS))
                {
                    var ganjoorUserDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ganjoor");
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
