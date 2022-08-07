using ganjoor.Audio_Support;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RMuseum.Models.GanjoorAudio.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class WSSelectSounds : WizardStage
    {
        public WSSelectSounds() : this(0, 0, 0, "")
        {
        }

        public WSSelectSounds(int nPoemId, int nPoetId, int nCatId, string searchTerm)
        {
            InitializeComponent();
            _PoemId = nPoemId;
            _PoetId = nPoetId;
            _CatId = nCatId;
            _SearchTerm = searchTerm;
        }

        //شناسه شعر
        //۰ یعنی کلیه شعرها
        private int _PoemId
        {
            set;
            get;
        }

        /// <summary>
        /// شناسهٔ شاعر - صفر تنظیم نشده همه‌ شاعران
        /// </summary>
        public int _PoetId { get; set; }

        /// <summary>
        /// شناسهٔ بخش
        /// </summary>
        public int _CatId { get; set; }

        /// <summary>
        /// عبارت جستجو
        /// </summary>
        public string _SearchTerm { get; set; }

        public override async void OnActivated()
        {
            await TryDownloadList();
        }

        //دریافت فهرست
        private async Task TryDownloadList()
        {
            btnRefresh.Visible = false;
            tlbr.Enabled = false;
            lblDesc.BackColor = System.Drawing.SystemColors.Window;
            lblDesc.Text = "در حال دریافت اطلاعات ...";
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            if (await RetrieveList())
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

        private const int GRDCLMN_CHECK = 2;


        private List<Dictionary<string, string>> _Lst = new List<Dictionary<string, string>>();

        private async Task<Tuple<List<Dictionary<string, string>>, string>> _RetrieveDictionaryListAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                HttpResponseMessage response = _PoemId == 0 ?
                    await httpClient.GetAsync($"{Properties.Settings.Default.GanjoorServiceUrl}/api/audio/published?searchTerm={_SearchTerm}&poetId={_PoetId}&catId={_CatId}")
                    :
                    await httpClient.GetAsync($"{Properties.Settings.Default.GanjoorServiceUrl}/api/ganjoor/poem/{_PoemId}/recitations");

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(await response.Content.ReadAsStringAsync());
                    return new Tuple<List<Dictionary<string, string>>, string>(null, await response.Content.ReadAsStringAsync());
                }

                response.EnsureSuccessStatusCode();

                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

                bool finished = _PoemId != 0;

                do
                {
                    foreach (PublicRecitationViewModel recitation in JArray.Parse(await response.Content.ReadAsStringAsync()).ToObject<List<PublicRecitationViewModel>>())
                    {
                        //تبدیل شیئ به چیزی که کد قدیمی با آن کار می‌کرده!
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("audio_post_ID", recitation.PoemId.ToString());
                        dic.Add("audio_order", recitation.Id.ToString());
                        dic.Add("audio_xml", $"{Properties.Settings.Default.GanjoorServiceUrl}/api/audio/file/{recitation.Id}.xml");
                        dic.Add("audio_mp3", recitation.Mp3Url);
                        dic.Add("audio_src", recitation.AudioSrc);
                        dic.Add("audio_title", recitation.AudioTitle);
                        dic.Add("audio_artist", recitation.AudioArtist);
                        dic.Add("audio_artist_url", recitation.AudioArtistUrl);
                        dic.Add("audio_guid", recitation.LegacyAudioGuid.ToString());
                        dic.Add("audio_fchecksum", recitation.Mp3FileCheckSum);
                        dic.Add("audio_mp3bsize", recitation.Mp3SizeInBytes.ToString());
                        list.Add(dic);
                    }

                    if (_PoemId == 0)
                    {
                        //در این حالت اطلاعات به صورت صفحه بندی شده ارسال می‌شود
                        string paginnationMetadata = response.Headers.GetValues("paging-headers").FirstOrDefault();
                        if (!string.IsNullOrEmpty(paginnationMetadata))
                        {
                            PaginationMetadata paginationMetadata = JsonConvert.DeserializeObject<PaginationMetadata>(paginnationMetadata);
                            if (paginationMetadata.totalPages == 0 || paginationMetadata.currentPage == paginationMetadata.totalPages)
                            {
                                finished = true;
                            }
                            else
                            {
                                lblDesc.Text = $"در حال دریافت اطلاعات (صفحهٔ {paginationMetadata.currentPage + 1} از {paginationMetadata.totalPages}) ...";
                                Application.DoEvents();
                                response = await httpClient.GetAsync($"{Properties.Settings.Default.GanjoorServiceUrl}/api/audio/published?searchTerm={_SearchTerm}&poetId={_PoetId}&catId={_CatId}&PageNumber={paginationMetadata.currentPage + 1}&PageSize={paginationMetadata.pageSize}");
                                if (response.StatusCode != HttpStatusCode.OK)
                                {
                                    MessageBox.Show(await response.Content.ReadAsStringAsync());
                                    break;
                                }

                                response.EnsureSuccessStatusCode();
                            }
                        }
                        else
                        {
                            finished = true;
                        }
                    }

                }
                while (!finished);



                return new Tuple<List<Dictionary<string, string>>, string>(list, "");

            }
        }

        //دریافت فهرست
        private async Task<bool> RetrieveList()
        {
            bool reS = true;
            var r = await _RetrieveDictionaryListAsync();

            if (!string.IsNullOrEmpty(r.Item2))
            {
                MessageBox.Show(r.Item2, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                reS = false;
            }
            else
            {
                _Lst = r.Item1;

                DbBrowser db = new DbBrowser();

                //در صورتی که تمام خوانشهای موجود را دریافت می‌کنیم برای تعیین وجود فایل صوتی از روش بهینه‌تری استفاده می‌کنیم.
                PoemAudio[] poemAudios = null;
                if (_PoemId == 0)
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
                        (int.Parse(audioInfo["audio_mp3bsize"]) / 1024.0 / 1024.0).ToString("0.00") + " مگابایت",
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
                if (firstSuggestableDownload != -1)
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

        private void btnMarkSelected_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in grdList.SelectedRows)
            {
                row.Cells[GRDCLMN_CHECK].Value = true;
            }
        }

        private void btnUnMarkSelected_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in grdList.SelectedRows)
            {
                row.Cells[GRDCLMN_CHECK].Value = false;
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
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await TryDownloadList();
        }

        private async void btnAllDownloadable_Click(object sender, EventArgs e)
        {
            using (AudioDownloadMethod audioDownloadMethod = new AudioDownloadMethod())
            {
                if (audioDownloadMethod.ShowDialog(this) == DialogResult.OK)
                {
                    _PoemId = 0;
                    _PoetId = audioDownloadMethod.PoetId;
                    _CatId = audioDownloadMethod.CatId;
                    Cursor.Current = Cursors.WaitCursor;
                    Application.DoEvents();
                    await TryDownloadList();
                    Cursor.Current = Cursors.Default;
                }
            }

        }

    }
}
