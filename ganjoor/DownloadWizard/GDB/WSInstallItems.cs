using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using ganjoor.Properties;

namespace ganjoor
{
    partial class WSInstallItems : WizardStage
    {
        public WSInstallItems() {
            InitializeComponent();

            InstalledFilesCount = 0;

        }

        public override bool PreviousStageButton
        {
            get
            {
                return false;
            }
        }

        public List<string> DownloadedFiles
        {
            set;
            get;
        }


        private void ImportGdb(string FileName, DbBrowser db)
        {
            if (Path.GetExtension(FileName).Equals(".zip", StringComparison.InvariantCultureIgnoreCase)) {
                using var zip = ZipStorer.Open(FileName, FileAccess.Read);
                var dir = zip.ReadCentralDir();
                foreach (var entry in dir)
                {
                    var gdbFileName = Path.GetFileName(entry.FilenameInZip);
                    if (Path.GetExtension(gdbFileName).Equals(".gdb") || Path.GetExtension(gdbFileName).Equals(".s3db"))
                    {
                        var ganjoorPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ganjoor");
                        if (!Directory.Exists(ganjoorPath))
                            Directory.CreateDirectory(ganjoorPath);
                        var gdbExtractPath = Path.Combine(ganjoorPath, gdbFileName);
                        if (zip.ExtractFile(entry, gdbExtractPath))
                        {
                            ImportDb(gdbExtractPath, db);
                            File.Delete(gdbExtractPath);
                        }
                    }
                }
            }
            else
                ImportDb(FileName, db);
        }

        public void ImportDb(string fileName, DbBrowser db)
        {
            var cnflts = db.GetConflictingPoets(fileName);
            if (cnflts.Length > 0) {
                using var dlg = new ConflictingPoets(cnflts);
                if (dlg.ShowDialog(Parent) == DialogResult.Cancel)
                {
                    grdList.Rows[grdList.RowCount - 1].Cells[1].Value = "صرف نظر به علت تداخل شاعر";
                    return;
                }
                cnflts = dlg.DeleteList;
                foreach (var delPoet in cnflts)
                    db.DeletePoet(delPoet._ID);
            }
            var catCnlts = db.GetConflictingCats(fileName);
            if (catCnlts.Length > 0) {
                using var dlg = new ConflictingCats(catCnlts);
                if (dlg.ShowDialog(Parent) == DialogResult.Cancel)
                {
                    grdList.Rows[grdList.RowCount - 1].Cells[1].Value = "صرف نظر به علت تداخل بخش";
                    return;
                }
                catCnlts = dlg.DeleteList;
                foreach (var delCat in catCnlts)
                    db.DeleteCat(delCat._ID);
            }
            var missingPoets = db.GetCategoriesWithMissingPoet(fileName);
            if (missingPoets.Length > 0)
            {
                if (MessageBox.Show(
                    "مجموعه‌ای که تلاش می‌کنید آن را اضافه کنید شامل بخشهایی از آثار شاعرانی است که آنها را در گنجور رومیزی خود ندارید."
                    + Environment.NewLine
                    + "گنجور رومیزی می‌تواند با ایجاد شاعران جدید تلاش کند این مشکل را حل کند، اما این همیشه حل کنندهٔ این مشکل نیست."
                    + Environment.NewLine
                    + "آیا می‌خواهید از اضافه کردن این مجموعه صرف نظر کنید؟"
                    , "هشدار"
                    , MessageBoxButtons.YesNo
                    , MessageBoxIcon.Warning
                    , MessageBoxDefaultButton.Button1
                    , MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading
                    ) == DialogResult.Yes
                    )
                {
                    grdList.Rows[grdList.RowCount - 1].Cells[1].Value = "صرف نظر به علت عدم وجود شاعر";
                    return;
                }
            }
            if (db.ImportDb(fileName))
            {
                grdList.Rows[grdList.RowCount - 1].Cells[1].Value = "اضافه شد.";
                InstalledFilesCount++;
            }
            else
            {
                grdList.Rows[grdList.RowCount - 1].Cells[1].Value = "خطا رخ داد.";
                MessageBox.Show(db.LastError);
            }
        }

        public override void OnActivated()
        {
            if (OnInstallStarted != null)
                OnInstallStarted(this, new EventArgs());
            var db = new DbBrowser();
            Application.DoEvents();
            if (DownloadedFiles != null)
                foreach (var gdb in DownloadedFiles)
                {
                    grdList.Rows[grdList.Rows.Add()].Cells[0].Value = Path.GetFileName(gdb);
                    ImportGdb(gdb, db);
                    if (Settings.Default.DeleteDownloadedFiles)
                        File.Delete(gdb);
                    Application.DoEvents();
                }
            db.CloseDb();
            if (OnInstallFinished != null)
                OnInstallFinished(this, new EventArgs());
        }

        public event EventHandler OnInstallStarted;
        public event EventHandler OnInstallFinished;

        public int InstalledFilesCount
        {
            get;
            private set;
        }




    }


}
