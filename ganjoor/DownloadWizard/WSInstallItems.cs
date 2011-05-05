using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace ganjoor
{
    partial class WSInstallItems : WizardStage
    {
        public WSInstallItems()
            : base()
        {
            InitializeComponent();

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
                                ImportDb(gdbExtractPath, db);
                                File.Delete(gdbExtractPath);
                            }
                        }
                    }
                }
            }
            else
                ImportDb(FileName, db);
        }

        public void ImportDb(string fileName, DbBrowser db)
        {
            GanjoorPoet[] cnflts = db.GetConflictingPoets(fileName);
            if (cnflts.Length > 0)
            {
                using (ConflictingPoets dlg = new ConflictingPoets(cnflts))
                {
                    if (dlg.ShowDialog(this.Parent) == DialogResult.Cancel)
                    {
                        grdList.Rows[grdList.RowCount - 1].Cells[1].Value = "صرف نظر به علت تداخل شاعر";
                        return;
                    }
                    cnflts = dlg.DeleteList;
                    foreach (GanjoorPoet delPoet in cnflts)
                        db.DeletePoet(delPoet._ID);
                }
            }
            GanjoorCat[] catCnlts = db.GetConflictingCats(fileName);
            if (catCnlts.Length > 0)
            {
                using (ConflictingCats dlg = new ConflictingCats(catCnlts))
                {
                    if (dlg.ShowDialog(this.Parent) == DialogResult.Cancel)
                    {
                        grdList.Rows[grdList.RowCount - 1].Cells[1].Value = "صرف نظر به علت تداخل بخش";
                        return;
                    }
                    catCnlts = dlg.DeleteList;
                    foreach (GanjoorCat delCat in catCnlts)
                        db.DeleteCat(delCat._ID);
                }
            }
            GanjoorCat[] missingPoets = db.GetCategoriesWithMissingPoet(fileName);
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
                grdList.Rows[grdList.RowCount - 1].Cells[1].Value = "اضافه شد.";
            else
            {
                grdList.Rows[grdList.RowCount - 1].Cells[1].Value = "خطا رخ داد.";
                MessageBox.Show(db.LastError);
            }
        }

        public override void OnActivated()
        {
            Application.DoEvents();
            DbBrowser db = new DbBrowser();
            if (DownloadedFiles != null)
                foreach (string gdb in DownloadedFiles)
                {
                    grdList.Rows[grdList.Rows.Add()].Cells[0].Value = Path.GetFileName(gdb);
                    ImportGdb(gdb, db);
                    Application.DoEvents();
                }

            db.CloseDb();
        }


    }


}
