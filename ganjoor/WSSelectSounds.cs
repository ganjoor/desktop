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
            lblDesc.Text = "در حال دریافت اطلاعات ...";
            Application.DoEvents();
            if (RetrieveList())
                lblDesc.Text = "ردیفهای سفیدرنگ نشانگر خوانشهایی است که شما آنها را در گنجور رومیزی خود ندارید. با علامتگذاری ستون «دریافت» در هر ردیف؛ آن را به فهرست خوانشهایی که می‌خواهید دریافت شوند اضافه کنید تا در مرحلهٔ بعد دریافت فهرست انتخابی شروع شود.";
            else
            {
                btnRefresh.Visible = true;
                lblDesc.Text = "دریافت یا پردازش فهرست خوانشها با خطا مواجه شد. لطفاً از اتصال ارتباط اینترنتیتان اطمینان حاصل کنید و دکمهٔ تلاش مجدد را بزنید.";
            }
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
            grdList.Rows.Clear();
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
                bool firstNotHaveItMet = false;
                foreach (Dictionary<string, string> audioInfo in _Lst)
                {
                    int RowIndex = grdList.Rows.Add();
                    bool haveIt = db.PoemAudioExists(_PoemId, audioInfo["audio_guid"]);
                    grdList.Rows[RowIndex].Tag = haveIt;
                    if (haveIt)
                        grdList.Rows[RowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                    string title = audioInfo["audio_title"];
                    title += " - به روایت ";
                    title += audioInfo["audio_artist"];
                    if (!string.IsNullOrWhiteSpace(audioInfo["audio_src"]))
                    {
                        title += " ";
                        title += audioInfo["audio_src"];
                    }
                    grdList.Rows[RowIndex].Cells[GRDCLMN_TITLE].Value = title;
                    grdList.Rows[RowIndex].Cells[GRDCLMN_SIZE].Value = (Int32.Parse(audioInfo["audio_mp3bsize"]) / 1024.0 / 1024.0).ToString("0.00") + " مگابایت";
                    grdList.Rows[RowIndex].Cells[GRDCLMN_CHECK].Value = !haveIt;
                    if (!haveIt && !firstNotHaveItMet)
                    {
                        firstNotHaveItMet = true;
                        grdList.FirstDisplayedScrollingRowIndex = RowIndex;
                    }
                }
                db.CloseDb();
            }

            return reS;
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


    }
}
