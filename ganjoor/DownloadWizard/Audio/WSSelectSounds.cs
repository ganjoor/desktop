using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class WSSelectSounds : WizardStage
    {
        public WSSelectSounds() : this(0)
        {
        }

        public WSSelectSounds(int nPoemId)
        {
            InitializeComponent();
            _PoemId = nPoemId;            
        }

        //شناسه شعر
        //۰ یعنی کلیه شعرها
        private int _PoemId
        {
            set;
            get;
        }

        //نشانی لیست خوانشها
        private string ListUrl
        {
            get
            {
                return "http://a.ganjoor.net/?p=" + _PoemId.ToString();
            }
        }


        public override void OnActivated()
        {
            TryDownloadList();
        }

        //دریافت فهرست
        private void TryDownloadList()
        {
            btnRefresh.Visible = false;
            tlbr.Enabled = false;
            lblDesc.BackColor = System.Drawing.SystemColors.Window;
            lblDesc.Text = "در حال دریافت اطلاعات ...";
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            if (RetrieveList())
            {
                lblDesc.Text = "ردیفهای سفیدرنگ نشانگر خوانشهایی است که شما آنها را در گنجور رومیزی خود ندارید. با علامتگذاری ستون «دریافت» در هر ردیف؛ آن را به فهرست خوانشهایی که می‌خواهید دریافت شوند اضافه کنید تا در مرحلهٔ بعد دریافت فهرست انتخابی شروع شود.";
                if (grdList.RowCount == 0)
                {
                    lblDesc.Text = "خوانشی برای این شعر یافت نشد.";
                    lblDesc.BackColor = Color.Red;
                }
            }
            else
            {
                lblDesc.BackColor = Color.Red;
                btnRefresh.Visible = true;
                lblDesc.Text = "دریافت یا پردازش فهرست خوانشها با خطا مواجه شد. لطفاً از اتصال ارتباط اینترنتیتان اطمینان حاصل کنید و دکمهٔ تلاش مجدد را بزنید.";
            }
            Cursor.Current = Cursors.Default;
            tlbr.Enabled = true;
        }

        private const int GRDCLMN_TITLE = 0;
        private const int GRDCLMN_SIZE = 1;
        private const int GRDCLMN_CHECK = 2;


        private List<Dictionary<string, string>> _Lst = new List<Dictionary<string, string>>();

        //دریافت فهرست
        private bool RetrieveList()
        {
            bool reS = true;
            string strException;
            _Lst = DownloadableAudioListProcessor.RetrieveList(ListUrl, out strException);
            if (!string.IsNullOrEmpty(strException))
            {
                MessageBox.Show(strException, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                reS = false;
            }
            else
            {
                DbBrowser db = new DbBrowser();
                
                //در صورتی که تمام خوانشهای موجود را دریافت می‌کنیم برای تعیین وجود فایل صوتی از روش بهینه‌تری استفاده می‌کنیم.
                PoemAudio[] poemAudios = null;
                if(_PoemId == 0)
                {
                    poemAudios = db.GetAllPoemAudioFiles();
                }

                grdList.Columns.Clear();
                DataTable tbl = new DataTable();
                tbl.Columns.Add("عنوان");
                tbl.Columns.Add("اندازه");
                tbl.Columns.Add("دریافت", typeof(bool));

                int firstSuggestableDownload = -1;
                int idx = -1;

                foreach (Dictionary<string, string> audioInfo in _Lst)
                {
                    idx++;
                    int nPoemId = Convert.ToInt32(audioInfo["audio_post_ID"]);
                    bool haveIt =
                        _PoemId == 0 ?
                        poemAudios.Where(p => p.SyncGuid.ToString() == audioInfo["audio_guid"]).FirstOrDefault() != null
                        :
                        db.PoemAudioExists(nPoemId, audioInfo["audio_guid"]);

                    if (!haveIt && firstSuggestableDownload == -1)
                        firstSuggestableDownload = idx;

                    tbl.Rows.Add(
                        DownloadableAudioListProcessor.SuggestTitle(audioInfo),
                        (Int32.Parse(audioInfo["audio_mp3bsize"]) / 1024.0 / 1024.0).ToString("0.00") + " مگابایت",
                        !haveIt
                        );
                }
                db.CloseDb();

                grdList.DataSource = tbl;

                grdList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grdList.Columns[0].FillWeight = 50;
                grdList.Columns[1].Width = 110;
                grdList.Columns[2].Width = 50;

                foreach (DataGridViewRow row in grdList.Rows)
                    row.Tag = !((bool)row.Cells[GRDCLMN_CHECK].Value);
                if(firstSuggestableDownload != -1)
                {
                    grdList.FirstDisplayedScrollingRowIndex = firstSuggestableDownload;
                }
                EnableDownloadCheckedButton();
            }


            return reS;
        }


        private void grdList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (grdList.Rows[e.RowIndex].Tag != null)
            {
                e.CellStyle.BackColor = ((bool)grdList.Rows[e.RowIndex].Tag) ? Color.LightGray : Color.White;
            }
        }





        private void grdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
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


        public List<Dictionary<string, string>> dwnldList
        {
            get;
            set;
        }

        public override void OnApplied()
        {
            dwnldList = new List<Dictionary<string, string>>();
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

        //تلاش مجدد
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            TryDownloadList();
        }

        private void btnAllDownloadable_Click(object sender, EventArgs e)
        {
            _PoemId = 0;
            TryDownloadList();
        }

    }
}
