using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ganjoor.Audio_Support;

namespace ganjoor
{
    public partial class NarratedPoems : Form
    {
        public NarratedPoems()
        {
            InitializeComponent();
            _DbBrowser = new DbBrowser();
            SelectedPoem = null;
        }

        public GanjoorPoem SelectedPoem
        {
            get;
            private set;
        }


        /// <summary>
        /// نحوه اتصال و کار با دیتابیس
        /// </summary>
        private DbBrowser _DbBrowser;

        private const int GRDCOLUMN_IDX_POET = 0;
        private const int GRDCOLUMN_IDX_TITLE = 1;
        private const int GRDCOLUMN_IDX_DESC = 2;

        /// <summary>
        /// پر کردن فهرست
        /// </summary>
        private void FillGrid()
        {
            lblCount.Text = "0";
            grdList.Rows.Clear();
            foreach (var Audio in _DbBrowser.GetAllPoemAudioFiles())
            {
                AddAudioInfoToGrid(Audio);
            }
            lblCount.Text = grdList.RowCount.ToString();
        }

        /// <summary>
        /// اضافه کردن اطلاعات یک ردیف به گرید
        /// </summary>
        /// <param name="Audio"></param>
        private int AddAudioInfoToGrid(PoemAudio Audio)
        {
            var nRowIdx = grdList.Rows.Add();
            grdList.Rows[nRowIdx].Cells[GRDCOLUMN_IDX_POET].Value = Audio.PoetName;
            grdList.Rows[nRowIdx].Cells[GRDCOLUMN_IDX_TITLE].Value = Audio.PoemTitle;
            grdList.Rows[nRowIdx].Cells[GRDCOLUMN_IDX_DESC].Value = Audio.Description;
            grdList.Rows[nRowIdx].Tag = Audio;
            return nRowIdx;
        }

        private void NarratedPoems_Load(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void grdList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                SelectedPoem = _DbBrowser.GetPoem((grdList.Rows[e.RowIndex].Tag as PoemAudio).PoemId);
                if (SelectedPoem != null)
                    DialogResult = DialogResult.OK;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (grdList.SelectedRows.Count == 1)
            {
                SelectedPoem = _DbBrowser.GetPoem((grdList.SelectedRows[0].Tag as PoemAudio).PoemId);
                if (SelectedPoem != null)
                    DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("ردیفی انتخاب نشده است.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            }
        }

        /// <summary>
        /// پشتیبان‌گیری
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e) {
            using var dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                var strOutDir = dlg.SelectedPath;
                var bIsEmpty = Directory.GetFiles(strOutDir).Length == 0;
                if (!bIsEmpty)
                {
                    if (MessageBox.Show("مسیر انتخاب شده خالی نیست. آیا از انتخاب این مسیر اطمینان دارید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign) == DialogResult.No)
                    {
                        return;
                    }
                }

                prgss.Maximum = grdList.RowCount;
                prgss.Value = 0;
                Enabled = false;

                foreach (DataGridViewRow Row in grdList.Rows)
                {
                    prgss.Value++;
                    var bRes = true;
                    Row.Selected = true;
                    grdList.FirstDisplayedCell = Row.Cells[0];
                    Application.DoEvents();
                    var audio = Row.Tag as PoemAudio;
                    if (audio == null)
                        bRes = false;
                    audio.SyncArray = _DbBrowser.GetPoemSync(audio);
                    if (bRes)
                    {
                        var outFileName = Path.Combine(strOutDir, Path.GetFileName(audio.FilePath));
                        if (!File.Exists(audio.FilePath))
                            bRes = false;
                        if (bRes)
                        {
                            if (File.Exists(outFileName))
                                outFileName = Path.Combine(strOutDir, audio.PoemId + ".mp3");

                            if (File.Exists(outFileName))
                                outFileName = Path.Combine(strOutDir, audio.SyncGuid + ".mp3");

                            var xmlFilePath = Path.Combine(strOutDir, Path.GetFileNameWithoutExtension(outFileName) + ".xml");
                            if (bRes)
                            {
                                if (!PoemAudioListProcessor.Save(xmlFilePath, audio, false))
                                {
                                    bRes = false;
                                }
                            }

                            if (bRes)
                            {
                                try
                                {
                                    File.Copy(audio.FilePath, outFileName, true);
                                }
                                catch
                                {
                                    bRes = false;
                                    File.Delete(xmlFilePath);
                                }
                            }

                        }
                    }

                    Row.DefaultCellStyle.BackColor = bRes ? Color.LightGreen : Color.Red;
                }

                Enabled = true;

                MessageBox.Show("فرایند پشتیبانگیری انجام شد. ردیفهای قرمزرنگ به دلایلی از قبیل عدم وجود فایل صوتی دارای اشکال بوده‌اند.", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);

            }
        }

        /// <summary>
        /// بازگشت پشتیبان
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e) {
            using var dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                var strInDir = dlg.SelectedPath;



                var xmlFiles = Directory.GetFiles(strInDir, "*.xml");

                if (xmlFiles.Length == 0)
                {
                    MessageBox.Show("در مسیر انتخابی فایل XML وجود ندارد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                    return;
                }


                prgss.Maximum = xmlFiles.Length;
                prgss.Value = 0;

                Enabled = false;

                var nErr = 0;
                foreach (var xmlFile in xmlFiles)
                {
                    prgss.Value++;
                    Application.DoEvents();

                    var lstPoemAudio = PoemAudioListProcessor.Load(xmlFile);

                    if (lstPoemAudio.Count == 1)
                    {
                        foreach (var xmlAudio in lstPoemAudio)
                        {
                            var mp3FilePath = Path.Combine(strInDir, Path.GetFileNameWithoutExtension(xmlFile)) + Path.GetExtension(xmlAudio.FilePath);
                            if (!File.Exists(mp3FilePath))
                            {
                                nErr++;
                                break;
                            }

                            if (_DbBrowser.AddAudio(
                                    mp3FilePath,
                                    xmlAudio
                                )
                                == null)
                                nErr++;
                        }
                    }
                    else
                    {
                        nErr++;
                    }




                }//foreach

                Enabled = true;

                FillGrid();

                if (nErr > 0)
                {
                    MessageBox.Show(String.Format("خطا در اضافه کردن {0} مورد", nErr), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                }
                else
                {
                    MessageBox.Show("فرایند بازگشت پشتیبان خوانشها بدون خطا انجام شد.", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                }




            }//if
        }

        /// <summary>
        /// همه خوانشهای قابل دریافت
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllDownloadable_Click(object sender, EventArgs e) {
            using var audioDownloadMethod = new AudioDownloadMethod();
            if (audioDownloadMethod.ShowDialog(this) == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();
                using (var dlg = new SndDownloadWizard(0, audioDownloadMethod.PoetId, audioDownloadMethod.CatId, audioDownloadMethod.SearchTerm))
                {
                    dlg.ShowDialog(this);
                    if (dlg.AnythingInstalled)
                    {
                        FillGrid();
                    }
                }
                Cursor.Current = Cursors.Default;
            }
        }


    }
}
