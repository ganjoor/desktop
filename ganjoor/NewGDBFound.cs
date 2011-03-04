using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ganjoor
{
    public partial class NewGDBFound : Form
    {
        public NewGDBFound(List<GDBInfo> Lst)
        {
            InitializeComponent();
            foreach (GDBInfo gdbInfo in Lst)
            {
                int RowIndex = grdList.Rows.Add();
                grdList.Rows[RowIndex].Cells[GRDCLMN_CAT].Value = gdbInfo.CatName;
                grdList.Rows[RowIndex].Cells[GRDCLMN_DWNLD].Value = "دریافت";
                if (!string.IsNullOrEmpty(gdbInfo.BlogUrl))
                    grdList.Rows[RowIndex].Cells[GRDCLMN_MORE].Value = "ببینید";
                grdList.Rows[RowIndex].Cells[GRDCLMN_IGNORE].Value = false;
                grdList.Rows[RowIndex].Tag = gdbInfo;
                grdList.FirstDisplayedScrollingRowIndex = RowIndex;
            }
        }

        private const int GRDCLMN_CAT = 0;
        private const int GRDCLMN_DWNLD = 1;
        private const int GRDCLMN_MORE = 2;
        private const int GRDCLMN_IGNORE = 3;
        private const int GRDCLMN_CHECK = 4;

        private void grdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            GDBInfo gdb = grdList.Rows[e.RowIndex].Tag as GDBInfo;
            if(gdb != null)
                try
                {
                    switch (e.ColumnIndex)
                    {
                        case GRDCLMN_DWNLD:
                            if (!string.IsNullOrEmpty(gdb.DownloadUrl))
                                Process.Start(gdb.DownloadUrl);
                            break;
                        case GRDCLMN_MORE:
                            if (!string.IsNullOrEmpty(gdb.BlogUrl))
                                Process.Start(gdb.BlogUrl);
                            break;
                        case GRDCLMN_IGNORE:
                            grdList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !((bool)grdList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);//why do I need this?                            
                            break;
                        case GRDCLMN_CHECK:
                            grdList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !Convert.ToBoolean(grdList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                            break;

                    }
                }
                catch
                {
                    //Sometimes something occurs here!
                }
        }

        public List<int> IgnoreList
        {
            get
            {
                List<int> Lst = new List<int>();
                foreach (DataGridViewRow Row in grdList.Rows)
                    if((bool)Row.Cells[GRDCLMN_IGNORE].Value)
                    {
                        GDBInfo gdb = Row.Tag as GDBInfo;
                        if (gdb != null)
                            Lst.Add(gdb.CatID);
                    }
                return Lst;
            }
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

        private void grdList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == GRDCLMN_CHECK)
                EnableDownloadCheckedButton();

        }

        public List<GDBInfo> dwnldList
        {
            get;
            set;
        }

        private void btnDownloadChecked_Click(object sender, EventArgs e)
        {
            dwnldList = new List<GDBInfo>();
            foreach (DataGridViewRow Row in grdList.Rows)
                if (Convert.ToBoolean(Row.Cells[GRDCLMN_CHECK].Value))
                    dwnldList.Add(Row.Tag as GDBInfo);
        }



    }
}
