using ganjoor.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ganjoor
{
    partial class WSSelectItems : WizardStage
    {

        public WSSelectItems()
            : base()
        {
            InitializeComponent();
        }

        public override void OnActivated()
        {
            tlbr.Enabled = false;
            lblDesc.Text = "در حال دریافت اطلاعات ...";
            if (OnDisableNextButton != null)
                OnDisableNextButton(this, new EventArgs());
            if (DownloadList(Settings.Default.LastDownloadUrl))
                lblDesc.Text = "ردیفهای سفیدرنگ نشانگر مجموعه‌هایی است که شما آنها را در گنجور رومیزی خود ندارید. با علامتگذاری ستون «دریافت» در هر ردیف؛ آن را به فهرست مجموعه‌هایی که می‌خواهید دریافت شوند اضافه کنید تا در مرحلهٔ بعد دریافت فهرست انتخابی شروع شود.";
            else
                lblDesc.Text = "دریافت یا پردازش فهرست مجموعه‌ها با خطا مواجه شد. لطفاً از اتصال ارتباط اینترنتیتان اطمینان حاصل کنید، دکمهٔ برگشت را بزنید و دوباره تلاش کنید.";
            tlbr.Enabled = true;
        }

        private List<GDBInfo> _Lst = new List<GDBInfo>();

        private bool DownloadList(string url)
        {
            bool reS = true;
            grdList.Rows.Clear();
            Application.DoEvents();
            string strException;
            _Lst = GDBListProcessor.RetrieveList(url, out strException);
            if (_Lst == null)
            {
                _Lst = new List<GDBInfo>();
            }
            if (!string.IsNullOrEmpty(strException))
            {
                MessageBox.Show(strException, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                reS = false;
            }


            if (_Lst.Count > 0)
            {
                DbBrowser db = new DbBrowser();
                foreach (GDBInfo gdbInfo in _Lst)
                {
                    int RowIndex = grdList.Rows.Add();
                    bool haveIt = (db.GetCategory(gdbInfo.CatID) != null);
                    grdList.Rows[RowIndex].Tag = haveIt;
                    if (haveIt)
                        grdList.Rows[RowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                    grdList.Rows[RowIndex].Cells[GRDCLMN_CAT].Value = gdbInfo.CatName;
                    grdList.Rows[RowIndex].Cells[GRDCLMN_DWNLD].Value = "ببینید";
                    if (!string.IsNullOrEmpty(gdbInfo.BlogUrl))
                        grdList.Rows[RowIndex].Cells[GRDCLMN_MORE].Value = "ببینید";
                    grdList.FirstDisplayedScrollingRowIndex = RowIndex;
                }
                db.CloseDb();
            }

            return reS;

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
                        try { Process.Start(_Lst[e.RowIndex].DownloadUrl); }
                        catch { /* this is normal on my system! works*/}
                    break;
                case GRDCLMN_MORE:
                    if (!string.IsNullOrEmpty(_Lst[e.RowIndex].BlogUrl))
                        try { Process.Start(_Lst[e.RowIndex].BlogUrl); }
                        catch { /* this is normal on my system! works*/}
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
                    if (OnEnableNextButton != null)
                        OnEnableNextButton(this, new EventArgs());
                    return;

                }
            if (OnDisableNextButton != null)
                OnDisableNextButton(this, new EventArgs());
        }


        public List<GDBInfo> dwnldList
        {
            get;
            set;
        }

        public override void OnApplied()
        {
            dwnldList = new List<GDBInfo>();
            foreach (DataGridViewRow Row in grdList.Rows)
                if (Convert.ToBoolean(Row.Cells[GRDCLMN_CHECK].Value))
                    dwnldList.Add(_Lst[Row.Index]);
        }

        public event EventHandler OnEnableNextButton = null;
        public event EventHandler OnDisableNextButton = null;

        private void btnSelNone_Click(object sender, EventArgs e)
        {
            if (grdList.IsCurrentCellInEditMode)
                grdList.EndEdit();
            foreach (DataGridViewRow Row in grdList.Rows)
                Row.Cells[GRDCLMN_CHECK].Value = false;

        }

        private void btnSelAllWhites_Click(object sender, EventArgs e)
        {
            if (grdList.IsCurrentCellInEditMode)
                grdList.EndEdit();
            foreach (DataGridViewRow Row in grdList.Rows)
                if (!((bool)Row.Tag))
                    Row.Cells[GRDCLMN_CHECK].Value = true;
        }





    }
}
