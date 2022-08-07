using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class GDBListEditor : Form
    {
        public GDBListEditor()
        {
            InitializeComponent();
        }


        #region Grid Columns
        private const int CLMN_CATNAME = 0;
        private const int CLMN_POETID = CLMN_CATNAME + 1;
        private const int CLMN_CATID = CLMN_POETID + 1;
        private const int CLMN_DWNLDURL = CLMN_CATID + 1;
        private const int CLMN_BLOG = CLMN_DWNLDURL + 1;
        private const int CLMN_EXT = CLMN_BLOG + 1;
        private const int CLMN_IMAGE = CLMN_EXT + 1;
        private const int CLMN_SIZE = CLMN_IMAGE + 1;
        private const int CLMN_POEMID = CLMN_SIZE + 1;
        private const int CLMN_PUBDATE = CLMN_POEMID + 1;
        #endregion
        private string _FileName = string.Empty;


        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "XML Files (*.xml)|*.xml";
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    _FileName = dlg.FileName;
                    FillGridWithListInfo(_FileName);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "XML Files (*.xml)|*.xml";
                if (!string.IsNullOrEmpty(_FileName))
                {
                    dlg.InitialDirectory = Path.GetDirectoryName(_FileName);
                    dlg.FileName = Path.GetFileName(_FileName);
                }
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    _FileName = dlg.FileName;
                    SaveToXml(_FileName);
                }
            }
        }

        private void SaveToXml(string FileName)
        {
            List<GDBInfo> lst = new List<GDBInfo>();
            foreach (DataGridViewRow Row in grd.Rows)
                if (!Row.IsNewRow)
                {
                    bool err = false;
                    GDBInfo gdb = ConvertGridRowToGDBInfo(Row, ref err);
                    lst.Add(gdb);
                    if (err)
                        MessageBox.Show(string.Format("در تبدیل داده‌های ردیف {0} مشکلی پیش آمد. اما فایل با توکل به خدا ;)  ذخیره خواهد شد. جهت اطمینان داده‌های ردیف مذکور را بازبینی کنید.", Row.Index + 1), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                }
            if (GDBListProcessor.Save(FileName, txtName.Text, txtDescription.Text, txtMoreInfoUrl.Text, lst))
            {
                if (
                MessageBox.Show("فایل به درستی ذخیره شد. می‌خواهید آن را مشاهده کنید؟", "اعلان", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign)
                    ==
                    System.Windows.Forms.DialogResult.Yes
                )
                    try
                    {
                        System.Diagnostics.Process.Start(FileName);

                    }
                    catch { }

            }
            else
                MessageBox.Show("تلاش برای ذخیره موفقیت‌آمیز نبود.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);

        }

        private static GDBInfo ConvertGridRowToGDBInfo(DataGridViewRow Row, ref bool err)
        {
            GDBInfo gdb = new GDBInfo();
            try
            {
                gdb.CatName = Row.Cells[CLMN_CATNAME].Value.ToString();
            }
            catch
            {
                err = true;
            }
            try
            {
                gdb.PoetID = Convert.ToInt32(Row.Cells[CLMN_POETID].Value);
            }
            catch
            {
                err = true;
            }
            try
            {
                gdb.CatID = Convert.ToInt32(Row.Cells[CLMN_CATID].Value);
            }
            catch
            {
                err = true;
            }
            try
            {
                gdb.DownloadUrl = Row.Cells[CLMN_DWNLDURL].Value.ToString();
            }
            catch
            {
                err = true;
            }
            try
            {
                gdb.BlogUrl = Row.Cells[CLMN_BLOG].Value == null ? "" : Row.Cells[CLMN_BLOG].Value.ToString();
            }
            catch
            {
                err = true;
            }
            try
            {
                gdb.FileExt = Row.Cells[CLMN_EXT].Value == null ? "gdb" : Row.Cells[CLMN_EXT].Value.ToString();
            }
            catch
            {
                err = true;
            }
            try
            {
                gdb.ImageUrl = Row.Cells[CLMN_IMAGE].Value == null ? "" : Row.Cells[CLMN_IMAGE].Value.ToString();
            }
            catch
            {
                err = true;
            }
            try
            {
                int iFileSizeInByte;
                if (Row.Cells[CLMN_SIZE].Value != null && Int32.TryParse(Row.Cells[CLMN_SIZE].Value.ToString(), out iFileSizeInByte))
                    gdb.FileSizeInByte = iFileSizeInByte;
                else
                    gdb.FileSizeInByte = 0;
            }
            catch
            {
                err = true;
            }
            try
            {
                int iLowestPoemID;
                if (Row.Cells[CLMN_POEMID].Value != null && Int32.TryParse(Row.Cells[CLMN_POEMID].Value.ToString(), out iLowestPoemID))
                    gdb.LowestPoemID = iLowestPoemID;
                else
                    gdb.LowestPoemID = 0;
            }
            catch
            {
                err = true;
            }
            try
            {
                DateTime dt;
                if (Row.Cells[CLMN_PUBDATE].Value != null && DateTime.TryParse(Row.Cells[CLMN_PUBDATE].Value.ToString(), out dt))
                    gdb.PubDate = dt;
                else
                    gdb.PubDate = DateTime.Now;
            }
            catch
            {
                err = true;
            }
            return gdb;
        }


        private void FillGridWithListInfo(string ListFileName)
        {
            string Name, Description, MoreInfoUrl;
            GDBListProcessor.RetrieveProperties(ListFileName, out Name, out Description, out MoreInfoUrl);
            txtName.Text = Name;
            txtDescription.Text = Description;
            txtMoreInfoUrl.Text = MoreInfoUrl;
            grd.Rows.Clear();
            string Exception;
            List<GDBInfo> gdbs = GDBListProcessor.RetrieveList(ListFileName, out Exception);
            if (string.IsNullOrEmpty(Exception))
            {
                foreach (GDBInfo gdb in gdbs)
                {
                    AddGDBInfo(gdb);
                }
            }
        }

        private int AddGDBInfo(GDBInfo gdb)
        {
            int RowIndex = grd.Rows.Add();
            grd.Rows[RowIndex].Cells[CLMN_CATNAME].Value = gdb.CatName;
            grd.Rows[RowIndex].Cells[CLMN_POETID].Value = gdb.PoetID;
            grd.Rows[RowIndex].Cells[CLMN_CATID].Value = gdb.CatID;
            grd.Rows[RowIndex].Cells[CLMN_DWNLDURL].Value = gdb.DownloadUrl;
            grd.Rows[RowIndex].Cells[CLMN_BLOG].Value = gdb.BlogUrl;
            grd.Rows[RowIndex].Cells[CLMN_EXT].Value = gdb.FileExt;
            grd.Rows[RowIndex].Cells[CLMN_IMAGE].Value = gdb.ImageUrl;
            grd.Rows[RowIndex].Cells[CLMN_SIZE].Value = gdb.FileSizeInByte;
            grd.Rows[RowIndex].Cells[CLMN_POEMID].Value = gdb.LowestPoemID;
            grd.Rows[RowIndex].Cells[CLMN_PUBDATE].Value = gdb.PubDate.ToString("yyyy-MM-dd");
            return RowIndex;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "All Supported Packages|*.gdb;*.s3db;*.zip|GDB Files(*.gdb)|*.gdb|Poem SQLite databases(*.s3db)|*.s3db|Zipped GDB Files(*.zip)|*.zip";
                dlg.Multiselect = true;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    foreach (string FileName in dlg.FileNames)
                    {
                        AddGdbOrZipFileToGrid(FileName);
                    }
                }
            }
        }
        private int AddGdbOrZipFileToGrid(string FileName)
        {
            int FileSize = File.ReadAllBytes(FileName).Length;
            GDBInfo gdb = null;
            if (Path.GetExtension(FileName).Equals(".zip", StringComparison.InvariantCultureIgnoreCase))
            {
                using (ZipStorer zip = ZipStorer.Open(FileName, FileAccess.Read))
                {
                    List<ZipStorer.ZipFileEntry> dir = zip.ReadCentralDir();
                    foreach (ZipStorer.ZipFileEntry entry in dir)
                    {
                        string gdbFileName = Path.GetFileName(entry.FilenameInZip);
                        if (Path.GetExtension(gdbFileName).Equals(".gdb") || Path.GetExtension(gdbFileName).Equals(".s3db"))
                        {
                            string ganjoorPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ganjoor");
                            if (!Directory.Exists(ganjoorPath))
                                Directory.CreateDirectory(ganjoorPath);
                            string gdbExtractPath = Path.Combine(ganjoorPath, gdbFileName);
                            if (zip.ExtractFile(entry, gdbExtractPath))
                            {
                                gdb = ExtracInfoFromGDBFileAndAddToGrid(gdbExtractPath, Path.GetExtension(FileName), FileSize);
                                File.Delete(gdbExtractPath);
                            }
                        }
                    }
                }
            }
            else
                gdb = ExtracInfoFromGDBFileAndAddToGrid(FileName, Path.GetExtension(FileName), FileSize);
            if (gdb != null)
            {
                if (string.IsNullOrEmpty(txtRefUrl.Text))
                    gdb.DownloadUrl = FileName;
                else
                    gdb.DownloadUrl = txtRefUrl.Text + Path.GetFileName(FileName);
                return AddGDBInfo(gdb);
            }
            return -1;

        }

        private GDBInfo ExtracInfoFromGDBFileAndAddToGrid(string FileName, string ext, int FileSize)
        {
            GDBInfo gdb = null;
            DbBrowser gdbBrowser = new DbBrowser(FileName);

            if (gdbBrowser.Poets.Count != 1)
            {
                MessageBox.Show("این ابزار در حال حاضر تنها مجموعه‌های حاوی یک شاعر را پشتیبانی می‌کند.");
            }
            else
            {
                GanjoorPoet poet = gdbBrowser.Poets[0];
                gdb = new GDBInfo();
                gdb.CatName = poet._Name;
                gdb.PoetID = poet._ID;
                int minCatID, minPoemID;
                gdbBrowser.GetMinIDs(poet._ID, out minCatID, out minPoemID);
                gdb.CatID = minCatID;
                gdb.FileExt = ext;
                gdb.FileSizeInByte = FileSize;
                gdb.LowestPoemID = minPoemID;
                gdb.PubDate = DateTime.Now;
            }
            gdbBrowser.CloseDb();
            return gdb;
        }

        private void btnFromDb_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "مسیر خروجیها را انتخاب کنید";
                dlg.ShowNewFolderButton = true;
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    bool embedPictures = false;
                    string picPath = string.Empty;
                    string picUrPrefix = string.Empty;
                    using (GDBPictureDirSelector plg = new GDBPictureDirSelector())
                        if (plg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            embedPictures = plg.EmbedPictures;
                            picPath = plg.PicturesPath;
                            picUrPrefix = plg.PicturesUrlPrefix;
                            if (!Directory.Exists(picPath))
                                embedPictures = false;
                        }
                    this.Enabled = false;

                    List<int> existingIDs = new List<int>();
                    foreach (DataGridViewRow Row in grd.Rows)
                        if (!Row.IsNewRow)
                        {
                            bool err = false;
                            GDBInfo gdb = ConvertGridRowToGDBInfo(Row, ref err);
                            if (!err)
                                existingIDs.Add(gdb.PoetID);
                        }

                    DbBrowser db = new DbBrowser();
                    foreach (GanjoorPoet Poet in db.Poets)
                        if (existingIDs.IndexOf(Poet._ID) == -1)//existing items in grid
                        {
                            string outFile = Path.Combine(dlg.SelectedPath, GPersianTextSync.Farglisize(Poet._Name));
                            string gdbFile = outFile + ".gdb";
                            if (db.ExportPoet(gdbFile, Poet._ID))
                            {
                                string zipFile = outFile + ".zip";
                                using (ZipStorer zipStorer = ZipStorer.Create(zipFile, ""))
                                {
                                    zipStorer.AddFile(ZipStorer.Compression.Deflate, gdbFile, Path.GetFileName(gdbFile), "");
                                    if (embedPictures)
                                    {
                                        string pngPath = Path.Combine(picPath, Poet._ID.ToString() + ".png");
                                        if (File.Exists(pngPath))
                                        {
                                            zipStorer.AddFile(ZipStorer.Compression.Deflate, pngPath, Path.GetFileName(pngPath), "");
                                        }
                                    }
                                }
                                File.Delete(gdbFile);
                                int RowIndex = AddGdbOrZipFileToGrid(zipFile);
                                if (embedPictures && !string.IsNullOrEmpty(picUrPrefix))
                                {
                                    string pngPath = Path.Combine(picPath, Poet._ID.ToString() + ".png");
                                    if (File.Exists(pngPath))
                                    {
                                        grd.Rows[RowIndex].Cells[CLMN_IMAGE].Value = picUrPrefix + Path.GetFileName(pngPath);
                                    }
                                }


                                Application.DoEvents();
                            }
                            else
                            {
                                MessageBox.Show(db.LastError);
                            }
                        }
                    this.Enabled = true;
                    db.CloseDb();
                }
            }
        }



    }
}
