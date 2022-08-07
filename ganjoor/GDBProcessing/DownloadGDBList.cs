using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


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
            DownloadList("http://i.ganjoor.net/android/androidgdbs.xml");
        }

        private List<GDBInfo> _Lst = new List<GDBInfo>();

        private void DownloadList(string url)
        {
            grdList.Rows.Clear();
            Application.DoEvents();
            string strException;
            _Lst = GDBListProcessor.RetrieveList(url, out strException);
            if (_Lst == null)
            {
                _Lst = new List<GDBInfo>();
            }
            if (!string.IsNullOrEmpty(strException))
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
        private const int GRDCLMN_CHECK = 3;

        private void grdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case GRDCLMN_DWNLD:
                    if (!string.IsNullOrEmpty(_Lst[e.RowIndex].DownloadUrl))
                        Process.Start(_Lst[e.RowIndex].DownloadUrl);
                    break;
                case GRDCLMN_MORE:
                    if (!string.IsNullOrEmpty(_Lst[e.RowIndex].BlogUrl))
                        Process.Start(_Lst[e.RowIndex].BlogUrl);
                    break;
                case GRDCLMN_CHECK:
                    grdList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !Convert.ToBoolean(grdList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    break;
            }
        }

        private void grdList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == GRDCLMN_CHECK)
                EnableDownloadCheckedButton();

        }

        private void EnableDownloadCheckedButton()
        {
            foreach (DataGridViewRow Row in grdList.Rows)
                if (Convert.ToBoolean(Row.Cells[GRDCLMN_CHECK].Value))
                {
                    btnDownloadChecked.Enabled = true;
                    return;
                }
            btnDownloadChecked.Enabled = false;
        }

        private void btnDownloadChecked_Click(object sender, EventArgs e)
        {
            dwnldList = new List<GDBInfo>();
            foreach (DataGridViewRow Row in grdList.Rows)
                if (Convert.ToBoolean(Row.Cells[GRDCLMN_CHECK].Value))
                    dwnldList.Add(_Lst[Row.Index]);
        }


        public List<GDBInfo> dwnldList
        {
            get;
            set;
        }







    }
}
