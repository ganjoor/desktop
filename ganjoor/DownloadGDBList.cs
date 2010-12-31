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
            grdList.Rows.Clear();
            Application.DoEvents();
            string strException;
            _Lst = GDBInfo.RetrieveNewGDBList(url, out strException);
            if (_Lst == null)
            {
                _Lst = new List<GDBInfo>();
            }
            if(!string.IsNullOrEmpty(strException))
                MessageBox.Show(strException, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

            
            if (_Lst.Count > 0)
            {
                DbBrowser db = new DbBrowser();
                foreach (GDBInfo gdbInfo in _Lst)
                {
                    int RowIndex = grdList.Rows.Add();
                    if (db.GetCategory(gdbInfo.CatID) != null)
                        grdList.Rows[RowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                    grdList.Rows[RowIndex].Cells[GRDCLMN_CAT].Value = gdbInfo.CatName;
                    grdList.Rows[RowIndex].Cells[GRDCLMN_DWNLD].Value = "دریافت";
                    if (!string.IsNullOrEmpty(gdbInfo.BlogUrl))
                        grdList.Rows[RowIndex].Cells[GRDCLMN_MORE].Value = "ببینید";
                    grdList.FirstDisplayedScrollingRowIndex = RowIndex;
                }
                db.CloseDb();
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
