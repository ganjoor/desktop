using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using ganjoor.Audio_Support;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Net;

namespace ganjoor
{
    public partial class AudioFiles : Form
    {
        public AudioFiles(int nPoemId = -1)
        {
            InitializeComponent();


            _PoemId = nPoemId;
            _DbBrowser = new DbBrowser();
        }

        /// <summary>
        /// شعر جاری
        /// </summary>
        private int _PoemId;
        /// <summary>
        /// نحوه اتصال و کار با دیتابیس
        /// </summary>
        private DbBrowser _DbBrowser;

        private const int GRDCOLUMN_IDX_DESC = 0;
        private const int GRDCOLUMN_IDX_FILEPATH = 1;
        private const int GRDCOLUMN_IDX_SYNCED = 2;

        /// <summary>
        /// فهرست کردن فایلهای صوتی مرتبط
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AudioFiles_Load(object sender, EventArgs e)
        {
            FillGrid();
        }

        /// <summary>
        /// فهرست کردن فایلهای صوتی مرتبط
        /// </summary>
        private void FillGrid()
        {
            grdList.Rows.Clear();
            foreach (PoemAudio Audio in _DbBrowser.GetPoemAudioFiles(_PoemId))
            {
                AddAudioInfoToGrid(Audio);
            }
        }

        /// <summary>
        /// اضافه کردن اطلاعات یک ردیف به گرید
        /// </summary>
        /// <param name="Audio"></param>
        private int AddAudioInfoToGrid(PoemAudio Audio)
        {
            int nRowIdx = grdList.Rows.Add();
            grdList.Rows[nRowIdx].Cells[GRDCOLUMN_IDX_DESC].Value = Audio.Description;
            grdList.Rows[nRowIdx].Cells[GRDCOLUMN_IDX_FILEPATH].Value = Audio.FilePath;
            grdList.Rows[nRowIdx].Cells[GRDCOLUMN_IDX_SYNCED].Value = Audio.IsSynced;
            grdList.Rows[nRowIdx].Tag = Audio;
            return nRowIdx;
        }


        /// <summary>
        /// اضافه کردن فایل صوتی جدید
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "MP3 Files (*.mp3)|*.mp3|WAV Files (*.wav)|*.wav|All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string strDesc = "فایل صوتی " + _DbBrowser.GetPoem(_PoemId)._Title;
                    if (grdList.Rows.Count > 0)
                        strDesc += (" (" + (grdList.Rows.Count + 1).ToString() + ")");
                    using (ItemEditor itemEditor = new ItemEditor(EditItemType.General, "شرح فایل", "شرح فایل"))
                    {
                        itemEditor.ItemName = strDesc;
                        if(itemEditor.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            strDesc = itemEditor.ItemName;
                        }
                    }
                    PoemAudio newAudio = _DbBrowser.AddAudio(_PoemId, openFileDialog.FileName, strDesc);
                    if (newAudio != null)
                        grdList.Rows[AddAudioInfoToGrid(newAudio)].Selected = true;
                }
            }
        }

        /// <summary>
        /// حذف ارتباط فایل صوتی
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (grdList.SelectedRows.Count == 0)
            {
                MessageBox.Show("لطفاً ردیفی را انتخاب کنید.");
                return;
            }

            if (MessageBox.Show("از حذف ارتباط فایل صوتی ردیفی جاری با شعر اطمینان دارید؟",
                "تأییدیه",
                MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (_DbBrowser.DeleteAudio(this.grdList.SelectedRows[0].Tag as PoemAudio))
                {
                    this.grdList.Rows.Remove(this.grdList.SelectedRows[0]);
                }
            }

        }

        /// <summary>
        /// انتقال به بالای لیست
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveToTop_Click(object sender, EventArgs e)
        {
            if (grdList.SelectedRows.Count == 0)
            {
                MessageBox.Show("لطفاً ردیفی را انتخاب کنید.");
                return;
            }
            if (_DbBrowser.MoveToTop(this.grdList.SelectedRows[0].Tag as PoemAudio))
            {
                FillGrid();
            }
        }


        /// <summary>
        /// پخش و ایست پخش فایل صوتی
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlayStop_Click(object sender, EventArgs e)
        {
            if (_PoemAudioPlayer != null)
            {
                if (_PoemAudioPlayer.IsPlaying)
                {
                    _PoemAudioPlayer.StopPlayBack();
                    return;
                }

            }
            if (grdList.SelectedRows.Count == 0)
            {
                MessageBox.Show("لطفاً ردیفی را انتخاب کنید.");
                return;
            }

            if (_PoemAudioPlayer == null)
            {
                _PoemAudioPlayer = new PoemAudioPlayer();
                _PoemAudioPlayer.PlaybackStarted += new EventHandler(_PoemAudioPlayer_PlaybackStarted);
                _PoemAudioPlayer.PlaybackStopped += new EventHandler<NAudio.Wave.StoppedEventArgs>(_PoemAudioPlayer_PlaybackStopped);
            }
            PoemAudio poemAudio = grdList.SelectedRows[0].Tag as PoemAudio;
            if (!_PoemAudioPlayer.BeginPlayback(poemAudio))
            {
                btnPlayStop.Text = "پخش";
                btnPlayStop.Image = Properties.Resources.play16;

                MessageBox.Show("خطایی در پخش فایل صوتی رخ داد. لطفا چک کنید فایل در مسیر تعیین شده قرار داشته باشد.");
            }

            if (poemAudio != null && poemAudio.SyncArray != null && poemAudio.SyncArray[0].VerseOrder == -1)
            {
                _PoemAudioPlayer.PositionInMiliseconds = poemAudio.SyncArray[0].AudioMiliseconds;
            }

        }


        PoemAudioPlayer _PoemAudioPlayer = null;

        private void _PoemAudioPlayer_PlaybackStopped(object sender, NAudio.Wave.StoppedEventArgs e)
        {
            // we want to be always on the GUI thread and be able to change GUI components
            Debug.Assert(!this.InvokeRequired, "PlaybackStopped on wrong thread");
            if (e.Exception != null)
            {
                MessageBox.Show(String.Format("Playback Stopped due to an error {0}", e.Exception.Message));
            }
            btnPlayStop.Text = "پخش";
            btnPlayStop.Image = Properties.Resources.play16;

        }

        private void _PoemAudioPlayer_PlaybackStarted(object sender, EventArgs e)
        {
            btnPlayStop.Text = "ایست";
            btnPlayStop.Image = Properties.Resources.stop16;
        }




        private void AudioFiles_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_PoemAudioPlayer != null)
            {
                _PoemAudioPlayer.CleanUp();
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            if (grdList.SelectedRows.Count == 0)
            {
                MessageBox.Show("لطفاً ردیفی را انتخاب کنید.");
                return;
            }

            if (_PoemAudioPlayer != null)
            {
                _PoemAudioPlayer.CleanUp();
            }
            PoemAudio poemAudio = grdList.SelectedRows[0].Tag as PoemAudio;
            using (SyncPoemAudio dlg = new SyncPoemAudio(_DbBrowser, poemAudio))
            {
                dlg.ShowDialog(this);
                if (dlg.Saved)
                {
                    poemAudio.SyncArray = _DbBrowser.GetPoemSync(poemAudio);
                    _DbBrowser.ReadPoemAudioGuid(ref poemAudio);
                    grdList.SelectedRows[0].Cells[GRDCOLUMN_IDX_SYNCED].Value = poemAudio.IsSynced;
                }
            }
            

            
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Export(false);
        }

        private void Export(bool bFix)
        {
            if (grdList.SelectedRows.Count == 0)
            {
                MessageBox.Show("لطفاً ردیفی را انتخاب کنید.");
                return;
            }
            PoemAudio poemAudio = grdList.SelectedRows[0].Tag as PoemAudio;
            if (!poemAudio.IsSynced)
            {
                MessageBox.Show("ردیف جاری همگام نشده است.");
                return;
            }
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "XML Files (*.xml)|*.xml";
                dlg.FileName = System.IO.Path.GetFileNameWithoutExtension(poemAudio.FilePath);

                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    List<PoemAudio> lst = new List<PoemAudio>();
                    lst.Add(poemAudio);
                    if (PoemAudioListProcessor.Save(dlg.FileName, lst, bFix))
                    {
                        MessageBox.Show("فایل به درستی در مسیر انتخاب شده ذخیره شد.", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                    }
                    else
                    {
                        MessageBox.Show("خطایی در ذخیرۀ فایل رخ داد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                    }
                }
            }            
        }

        private void btnFixExport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("کاربرد این گزینه رفع اشکال نسخه های قدیمی است. آیا می دانید این گزینه چگونه کار می کند؟", "هشدار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                return;
            Export(true);

        }


        private void btnImport_Click(object sender, EventArgs e)
        {
            if (grdList.SelectedRows.Count == 0)
            {
                MessageBox.Show("لطفاً ابتدا فایل معادل اطلاعات همگام‌سازی را اضافه کرده، آن را انتخاب کنید.");
                return;
            }

            PoemAudio poemAudio = grdList.SelectedRows[0].Tag as PoemAudio;
            if (poemAudio.IsSynced)
            {
                if (MessageBox.Show(
                    String.Format("فایل انتخاب شده با شرح «{0}» و مسیر '{1}' در حال حاضر دارای اطلاعات همگام‌سازی است.\n" +
                    "از جایگزینی این اطلاعات اطمینان دارید؟", poemAudio.Description, poemAudio.FilePath),
                    "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign) == System.Windows.Forms.DialogResult.No)
                    return;
            }         
   
            using(OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "XML Files (*.xml)|*.xml";
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    List<PoemAudio> lstPoemAudio = PoemAudioListProcessor.Load(dlg.FileName);
                    if (lstPoemAudio.Count == 0)
                    {
                        MessageBox.Show("فایل انتخاب شده حاوی اطلاعات همگام‌سازی شعرها نیست.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                        return;
                    }

                    foreach (PoemAudio xmlAudio in lstPoemAudio)
                    {
                        if (xmlAudio.PoemId == poemAudio.PoemId)
                        {
                            if (xmlAudio.FileCheckSum != PoemAudio.ComputeCheckSum(poemAudio.FilePath))
                            {
                                if (MessageBox.Show(
                                    "اطلاعات فایل همگام شده با فایل انتخاب شده همسانی ندارد. از استفاده از این اطلاعات اطمینان دارید؟",
                                    "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
                                    MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign) == System.Windows.Forms.DialogResult.No)
                                    return;

                            }
                            _DbBrowser.SavePoemSync(poemAudio, xmlAudio.SyncArray, false);
                            poemAudio.SyncArray = xmlAudio.SyncArray;
                            poemAudio.SyncGuid = xmlAudio.SyncGuid;
                            grdList.SelectedRows[0].Cells[GRDCOLUMN_IDX_SYNCED].Value = poemAudio.IsSynced;
                            return;
                        }
                    }

                    MessageBox.Show("فایل انتخاب شده حاوی اطلاعات همگام‌سازی شعر جاری نیست.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);


                }
            }
            

        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            using (SndDownloadWizard dlg = new SndDownloadWizard(_PoemId))
            {
                dlg.ShowDialog(this);
                if (dlg.AnythingInstalled)
                {
                    FillGrid();
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private async void btnUpload_ClickAsync(object sender, EventArgs e)
        {
            if (grdList.SelectedRows.Count == 0)
            {
                MessageBox.Show("لطفاً ابتدا فایل معادل اطلاعات همگام‌سازی را اضافه کرده، آن را انتخاب کنید.");
                return;
            }

            PoemAudio poemAudio = grdList.SelectedRows[0].Tag as PoemAudio;
            if (!poemAudio.IsSynced)
            {
                MessageBox.Show("لطفا ابتدا خوانش را همگام کنید.");
                return;
            }
            if (string.IsNullOrEmpty(Properties.Settings.Default.MuseumToken))
            {
                using (GLogin gLogin = new GLogin())
                    if(gLogin.ShowDialog(this) != DialogResult.OK)
                    {
                        return;
                    }
            }
            if (string.IsNullOrEmpty(Properties.Settings.Default.MuseumToken))
            {
                return;
            }

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.MuseumToken);

                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                MultipartFormDataContent form = new MultipartFormDataContent();

                string xmlTempPath = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());

                PoemAudioListProcessor.Save(xmlTempPath, poemAudio, false);
                byte[] xmlFileContent = File.ReadAllBytes(xmlTempPath);
                File.Delete(xmlTempPath);
                form.Add(new ByteArrayContent(xmlFileContent, 0, xmlFileContent.Length), $"{Path.GetFileNameWithoutExtension(poemAudio.FilePath)}.xml", $"{Path.GetFileNameWithoutExtension(poemAudio.FilePath)}.xml");

                byte[] mp3FileContent = File.ReadAllBytes(poemAudio.FilePath);
                form.Add(new ByteArrayContent(mp3FileContent, 0, mp3FileContent.Length), Path.GetFileName(poemAudio.FilePath), Path.GetFileName(poemAudio.FilePath));
                
                HttpResponseMessage response = await httpClient.PostAsync($"{Properties.Settings.Default.GanjoorServiceUrl}/api/audio", form);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(response.ToString());
                    return;
                }

                response.EnsureSuccessStatusCode();
            }


            Cursor = Cursors.Default;
        }
    }
}
