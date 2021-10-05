using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
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
            foreach (PoemAudio Audio in _DbBrowser.GetAllPoemAudioFiles())
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
            int nRowIdx = grdList.Rows.Add();
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
                    DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (grdList.SelectedRows.Count == 1)
            {
                SelectedPoem = _DbBrowser.GetPoem((grdList.SelectedRows[0].Tag as PoemAudio).PoemId);
                if (SelectedPoem != null)
                    DialogResult = System.Windows.Forms.DialogResult.OK;
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
        private void btnExport_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    string strOutDir = dlg.SelectedPath;
                    bool bIsEmpty = Directory.GetFiles(strOutDir).Length == 0;
                    if (!bIsEmpty)
                    {
                        if(MessageBox.Show("مسیر انتخاب شده خالی نیست. آیا از انتخاب این مسیر اطمینان دارید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign) == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }
                    }

                    prgss.Maximum = grdList.RowCount;
                    prgss.Value = 0;
                    this.Enabled = false;
                    
                    foreach (DataGridViewRow Row in grdList.Rows)
                    {
                        prgss.Value++;
                        bool bRes = true;
                        Row.Selected = true;
                        grdList.FirstDisplayedCell = Row.Cells[0];                        
                        Application.DoEvents();
                        PoemAudio audio = Row.Tag as PoemAudio;
                        if (audio == null)
                            bRes = false;
                        audio.SyncArray = _DbBrowser.GetPoemSync(audio);
                        if (bRes)
                        {
                            string outFileName = Path.Combine(strOutDir, Path.GetFileName(audio.FilePath));
                            if (!File.Exists(audio.FilePath))
                                bRes = false;
                            if (bRes)
                            {
                                if (File.Exists(outFileName))
                                    outFileName = Path.Combine(strOutDir, audio.PoemId.ToString() + ".mp3");

                                if (File.Exists(outFileName))
                                    outFileName = Path.Combine(strOutDir, audio.SyncGuid.ToString() + ".mp3");

                                string xmlFilePath = Path.Combine(strOutDir, Path.GetFileNameWithoutExtension(outFileName) + ".xml");
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

                    this.Enabled = true;

                    MessageBox.Show("فرایند پشتیبانگیری انجام شد. ردیفهای قرمزرنگ به دلایلی از قبیل عدم وجود فایل صوتی دارای اشکال بوده‌اند.", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);

                }
            }

        }

        /// <summary>
        /// بازگشت پشتیبان
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    string strInDir = dlg.SelectedPath;
                    


                    string[] xmlFiles = Directory.GetFiles(strInDir, "*.xml");

                    if(xmlFiles.Length == 0)
                    {
                        MessageBox.Show("در مسیر انتخابی فایل XML وجود ندارد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                        return;
                    }


                    prgss.Maximum = xmlFiles.Length;
                    prgss.Value = 0;

                    this.Enabled = false;

                    int nErr = 0;
                    foreach (string xmlFile in xmlFiles)
                    {
                        prgss.Value++;
                        Application.DoEvents();

                        List<PoemAudio> lstPoemAudio = PoemAudioListProcessor.Load(xmlFile);

                        if (lstPoemAudio.Count == 1)
                        {
                            foreach (PoemAudio xmlAudio in lstPoemAudio)
                            {
                                string mp3FilePath = Path.Combine(strInDir, Path.GetFileNameWithoutExtension(xmlFile)) + Path.GetExtension(xmlAudio.FilePath);
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

                    this.Enabled = true;

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
            }//using


        }

        /// <summary>
        /// همه خوانشهای قابل دریافت
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllDownloadable_Click(object sender, EventArgs e)
        {
            using(AudioDownloadMethod audioDownloadMethod = new AudioDownloadMethod())
            {
                if(audioDownloadMethod.ShowDialog(this) == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Application.DoEvents();
                    using (SndDownloadWizard dlg = new SndDownloadWizard(0, audioDownloadMethod.PoetId, audioDownloadMethod.CatId, audioDownloadMethod.SearchTerm))
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
}
