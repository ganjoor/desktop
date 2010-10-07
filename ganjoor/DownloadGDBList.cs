using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using System.IO;
using System.Diagnostics;


namespace ganjoor
{
    public partial class DownloadGDBList : Form
    {
        public DownloadGDBList()
        {
            InitializeComponent();
        }

        private void btnDownloadList_Click(object sender, EventArgs e)
        {
            DownloadList("http://ganjoor.sourceforge.net/newgdbs.xml");
        }

        private List<GDBInfo> _Lst = new List<GDBInfo>();             

        private void DownloadList(string url)
        {
            try
            {
                WebRequest req = WebRequest.Create(url);
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
                                        DownloadList(Node.InnerText);
                                        return;
                                    }
                            }

                            //Collect List:
                            _Lst.Clear();
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

                                _Lst.Add(gdbInfo);
                            }


                            DbBrowser db = new DbBrowser();
                            grdList.Rows.Clear();
                            foreach (GDBInfo gdbInfo in _Lst)
                            {
                                int RowIndex = grdList.Rows.Add();
                                if (db.GetCategory(gdbInfo.CatID) != null)
                                    grdList.Rows[RowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                                grdList.Rows[RowIndex].Cells[GRDCLMN_CAT].Value = gdbInfo.CatName;
                                grdList.Rows[RowIndex].Cells[GRDCLMN_DWNLD].Value = "دریافت";
                                if(!string.IsNullOrEmpty(gdbInfo.BlogUrl))
                                    grdList.Rows[RowIndex].Cells[GRDCLMN_MORE].Value = "ببینید";
                                grdList.FirstDisplayedScrollingRowIndex = RowIndex;
                            }                            
                            
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private const int GRDCLMN_CAT = 0;
        private const int GRDCLMN_DWNLD = 1;
        private const int GRDCLMN_MORE = 2;

        private void grdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch(e.ColumnIndex)
            {
                case GRDCLMN_DWNLD:
                    if(!string.IsNullOrEmpty(_Lst[e.RowIndex].DownloadUrl))
                        Process.Start(_Lst[e.RowIndex].DownloadUrl);
                    break;
                case GRDCLMN_MORE:
                    if (!string.IsNullOrEmpty(_Lst[e.RowIndex].BlogUrl))
                        Process.Start(_Lst[e.RowIndex].BlogUrl);
                    break;
            }
        }
    }
}
