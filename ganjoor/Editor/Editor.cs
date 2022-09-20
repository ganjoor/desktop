using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ganjoor.Properties;

namespace ganjoor
{
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();
            ganjoorView.ShowBeytNums = false;
            //this.ganjoorView.MesraWidth = 250;
            ganjoorView.CenteredView = true;
            ganjoorView.Font = Settings.Default.ViewFont;
        }

        private void ganjoorView_OnPageChanged(string PageString, bool HasComments, bool CanBrowse, bool IsFaved, bool FavsPage, string highlightedText, object preItem, object nextItem)
        {
            if (HasComments)
            {
                btnNextPoem.Text = "شعر بعد";
                btnNextPoem.Enabled = ganjoorView.CanGoToNextPoem;
                btnNextPoem.Tag = null;
                btnPreviousPoem.Text = "شعر قبل";
                btnPreviousPoem.Enabled = ganjoorView.CanGoToPreviousPoem;
                btnPreviousPoem.Tag = null;
            }
            else
            {
                btnNextPoem.Text = "صفحهٔ بعد";
                btnNextPoem.Enabled = nextItem != null;
                btnNextPoem.Tag = nextItem;
                btnPreviousPoem.Text = "صفحهٔ قبل";
                btnPreviousPoem.Enabled = preItem != null;
                btnPreviousPoem.Tag = preItem;
            }
            btnReOrderCat.Enabled = btnExportPoet.Enabled = btnNewCat.Enabled = btnNewPoem.Enabled = btnEditPoet.Enabled = btnEditPoetBio.Enabled = btnDeletePoet.Enabled = PageString != "خانه";
            btnExportCat.Enabled = btnEditCat.Enabled = btnDeleteCat.Enabled = !ganjoorView.IsInPoetRootPage;

            btnRestructurePoem.Enabled = btnReplaceToolStripMenuItem.Enabled = btnNormalRestructure.Enabled = btnConvertBeytToBand.Enabled = btnConvertVerseToBand.Enabled =
            btnImportFromClipboadStructuredPoem.Enabled = btnImportFromTextFile.Enabled = btnImportFromClipboard.Enabled = chkEachlineOneverse.Enabled = btnNewLine.Enabled = btnDeletePoem.Enabled = btnMoveToCategory.Enabled = btnEditPoem.Enabled = chkIgnoreBlankLines.Enabled = chkIgnoreShortLines.Enabled = HasComments;
        }

        private void btnPreviousPoem_Click(object sender, EventArgs e)
        {
            if (btnPreviousPoem.Tag == null)
            {
                ganjoorView.PreviousPoem();
            }
            else
            {
                ganjoorView.ProcessPagingTag(btnPreviousPoem.Tag);
            }
        }

        private void btnNextPoem_Click(object sender, EventArgs e)
        {
            if (btnNextPoem.Tag == null)
            {
                ganjoorView.NextPoem();
            }
            else
            {
                ganjoorView.ProcessPagingTag(btnNextPoem.Tag);
            }
        }

        private void btnNewPoet_ButtonClick(object sender, EventArgs e) {
            using var dlg = new ItemEditor();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (!ganjoorView.NewPoet(dlg.ItemName))
                    MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            }
        }

        private void btnEditPoet_Click(object sender, EventArgs e) {
            using var dlg = new ItemEditor();
            dlg.ItemName = ganjoorView.CurrentPoet;
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (!ganjoorView.EditPoet(dlg.ItemName))
                    MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            }
        }

        private void btnNewCat_ButtonClick(object sender, EventArgs e) {
            using var dlg = new ItemEditor(EditItemType.Category);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (!ganjoorView.NewCat(dlg.ItemName))
                    MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);

            }
        }

        private void btnEditPoetBio_Click(object sender, EventArgs e) {
            using var dlg = new MemoEditor(ganjoorView.CurrentPoetBio);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (!ganjoorView.EditPoetBio(dlg.MemoText))
                    MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            }
        }


        private void btnEditCat_Click(object sender, EventArgs e) {
            using var dlg = new ItemEditor(EditItemType.Category);
            dlg.ItemName = ganjoorView.CurrentCategory;
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (!ganjoorView.EditCat(dlg.ItemName))
                    MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            }
        }

        private void btnNewPoem_ButtonClick(object sender, EventArgs e) {
            using var dlg = new ItemEditor(EditItemType.Poem);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (!ganjoorView.NewPoem(dlg.ItemName))
                    MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);

            }
        }

        private void btnEditPoem_Click(object sender, EventArgs e) {
            using var dlg = new ItemEditor(EditItemType.Poem);
            dlg.ItemName = ganjoorView.CurrentPoem;
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (!ganjoorView.EditPoem(dlg.ItemName))
                    MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            }
        }

        private void btnDeletePoem_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show("آیا از حذف این شعر اطمینان دارید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign)
                ==
                DialogResult.Yes
                )
                if (!ganjoorView.DeletePoem())
                    MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);

        }



        private void btnNewLine_Click(object sender, EventArgs e)
        {
            ganjoorView.NewNormalLine();
        }

        private void btnNewBandLine_Click(object sender, EventArgs e)
        {
            ganjoorView.NewBandLine();
        }

        private void btnNewBandVerse_Click(object sender, EventArgs e)
        {
            ganjoorView.NewBandVerse();
        }

        private void btnNewSingleVerse_Click(object sender, EventArgs e)
        {
            ganjoorView.NewSingleVerse();
        }

        private void btnNewParagraph_Click(object sender, EventArgs e)
        {
            ganjoorView.NewParagraph();
        }


        private void btnDeleteLine_Click(object sender, EventArgs e)
        {
            ganjoorView.DeleteLine();
        }

        private void btnAddFirstVerseToTitle_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show("آیا می‌دانید چه می‌کنید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign)
                ==
                DialogResult.Yes
                )
            {
                ganjoorView.AppendFirstVerseToTileAndDeleteIt();
            }
        }

        private void btnDeletePoet_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show("آیا از حذف این شاعر و تمام آثار او اطمینان دارید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign)
                ==
                DialogResult.Yes
                )
                if (!ganjoorView.DeletePoet())
                    MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);

        }

        private void btnDeleteCat_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show("آیا از حذف این بخش و تمام زیربخشها و اشعار آنها اطمینان دارید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign)
                ==
                DialogResult.Yes
                )
                if (!ganjoorView.DeleteCategory())
                    MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
        }

        private void btnExportPoet_Click(object sender, EventArgs e) {
            using var dlg = new SaveFileDialog();
            dlg.Filter = "*.gdb|*.gdb";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (File.Exists(dlg.FileName))
                    File.Delete(dlg.FileName);
                if (ganjoorView.ExportPoet(dlg.FileName))
                    MessageBox.Show("خروجی تولید شد.", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                else
                    MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            }
        }

        private void btnExportCat_Click(object sender, EventArgs e) {
            using var dlg = new SaveFileDialog();
            dlg.Filter = "*.gdb|*.gdb";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (File.Exists(dlg.FileName))
                    File.Delete(dlg.FileName);
                if (ganjoorView.ExportCategory(dlg.FileName))
                    MessageBox.Show("خروجی تولید شد.", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                else
                    MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            }
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            ganjoorView.Save();
        }

        private void btnReOrderCat_Click(object sender, EventArgs e)
        {
            ganjoorView.Save();
            ganjoorView.StoreSettings();
            Hide();
            using (var dlg = new ReOrderCat())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    var dbBrowser = new DbBrowser();
                    ganjoorView.ShowPoem(dbBrowser.GetPoem(dlg.SelectedPoemId), true);
                    dbBrowser.CloseDb();
                }
            }
            Show();
            ganjoorView.Font = ganjoorView.Font;
        }

        private void btnReOrderSubCat_Click(object sender, EventArgs e)
        {
            ganjoorView.Save();
            ganjoorView.StoreSettings();
            Hide();
            using (var dlg = new ReOrderSubCats())
            {
                dlg.ShowDialog(this);
            }
            Show();
            ganjoorView.Font = ganjoorView.Font;
        }

        private void btnImportFromTextFile_Click(object sender, EventArgs e) {
            using var dlg = new OpenFileDialog();
            dlg.Filter = "Unicode Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                var srcText = File.ReadAllText(dlg.FileName);
                ganjoorView.InsertVerses(srcText.Split(new[] { (char)10, (char)13 }, StringSplitOptions.RemoveEmptyEntries), !chkEachlineOneverse.Checked, chkIgnoreBlankLines.Checked, chkIgnoreShortLines.Checked, 4);
                ganjoorView.Save();
            }
        }

        private void btnImportFromClipboard_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                ganjoorView.InsertVerses(Clipboard.GetText().Split(new[] { (char)10, (char)13 }, StringSplitOptions.RemoveEmptyEntries), !chkEachlineOneverse.Checked, chkIgnoreBlankLines.Checked, chkIgnoreShortLines.Checked, 4);
                ganjoorView.Save();
            }
            else
                MessageBox.Show("متنی در کلیپ بورد نیست.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnImportFromClipboadStructuredPoem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText()) {
                using var dlg = new PoemStructure();
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    ganjoorView.InsertVerses(Clipboard.GetText().Split(new[] { (char)10, (char)13 }, StringSplitOptions.RemoveEmptyEntries), dlg.LinesCount, dlg.FullLine, chkIgnoreBlankLines.Checked, chkIgnoreShortLines.Checked, 4);
                    ganjoorView.Save();
                }
            }
            else
                MessageBox.Show("متنی در کلیپ بورد نیست.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnDeleteAllLine_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show("آیا از حذف تمام ابیات این شعر اطمینان دارید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign)
                ==
                DialogResult.Yes
                )
                if (!ganjoorView.DeleteAllLines())
                    MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);

        }

        #region AutoScroll fix found at http://www.devnewsgroups.net/group/microsoft.public.dotnet.framework.windowsforms/topic22846.aspx
        private Point thumbPos;
        private void Editor_Activated(object sender, EventArgs e)
        {
            thumbPos.X *= -1;
            thumbPos.Y *= -1;
            ganjoorView.AutoScrollPosition = thumbPos;
        }

        private void Editor_Deactivate(object sender, EventArgs e)
        {
            thumbPos = ganjoorView.AutoScrollPosition;
        }
        #endregion

        private void btnMergeTwoTextColumns_Click(object sender, EventArgs e) {
            using var dlg = new MrgTwoClmns();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                ganjoorView.InsertVerses(dlg.ResulText.Split(new[] { (char)10, (char)13 }, StringSplitOptions.RemoveEmptyEntries), true, chkIgnoreBlankLines.Checked, chkIgnoreShortLines.Checked, 4);
            }
        }

        private void btnReplaceInDb_Click(object sender, EventArgs e) {
            using var dlg = new ReplaceInDb();
            dlg.ShowDialog(this);
        }

        private void btnChangeIDs_Click(object sender, EventArgs e) {
            using var dlg = new IDChanger();
            int PoetID, MinCatID, MinPoemID;
            ganjoorView.GetIDs(out PoetID, out MinCatID, out MinPoemID);
            dlg.PoetID = PoetID;
            dlg.StartCatID = MinCatID;
            dlg.StartPoemID = MinPoemID;
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Enabled = false;
                ganjoorView.SetIDs(dlg.PoetID, dlg.StartCatID, dlg.StartPoemID);
                Enabled = true;
            }
        }

        private void btnMoveToCategory_Click(object sender, EventArgs e) {
            using var dlg = new CategorySelector(ganjoorView.GetCurrentPoetID());
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                ganjoorView.MoveToCategory(dlg.SelectedCatID);
            }
        }

        private void btnGDBListEditor_Click(object sender, EventArgs e) {
            using var dlg = new GDBListEditor();
            dlg.ShowDialog(this);
        }

        private void btnReplaceToolStripMenuItem_Click(object sender, EventArgs e) {
            using var dlgFind = new ItemEditor(EditItemType.General, "متن جستجو", "متن جستجو");
            if (dlgFind.ShowDialog(this) == DialogResult.OK)
            {
                var strFindText = dlgFind.ItemName;

                using var dlgReplace = new ItemEditor(EditItemType.General, "متن جایگزین", "متن جایگزین");
                if (dlgReplace.ShowDialog(this) == DialogResult.OK)
                {
                    var strReplaceText = dlgReplace.ItemName;
                    if (MessageBox.Show(String.Format("از جایگزینی «{0}» با «{1}» در شعر جاری اطمینان دارید؟", strFindText, strReplaceText),
                            "پرسش و هشدار", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;

                    ganjoorView.ReplaceText(strFindText, strReplaceText);
                }
            }
        }

        private void btnRestructurePoem_Click(object sender, EventArgs e) {
            using var dlg = new PoemStructure();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                ganjoorView.RestructureVerses(dlg.LinesCount, dlg.FullLine, -1, false);
                ganjoorView.Save();
            }
        }

        private void btnNormalRestructure_Click(object sender, EventArgs e)
        {
            ganjoorView.RestructureVerses(-1, true, -1, false);
            ganjoorView.Save();
        }

        private void btnConvertBeytToBand_Click(object sender, EventArgs e)
        {
            if (!ganjoorView.ConvertLineToBandLine())
            {
                MessageBox.Show("تبدیلی انجام نشد، این فرمان فقط روی مصرعهای بیتهای معمولی کار می‌کند.", "خطا");
            }

        }

        private void btnConvertVerseToBand_Click(object sender, EventArgs e)
        {
            ganjoorView.ConvertVerseToBandVerse();
        }

        private void btnConvertLeftToRight_Click(object sender, EventArgs e)
        {
            ganjoorView.ConvertLeftToRightLine();
        }

        private void btnConvertVerseToPara_Click(object sender, EventArgs e)
        {
            ganjoorView.ConvertVerseToPara();
        }

        private void btnConvertVerseToComment_Click(object sender, EventArgs e)
        {
            ganjoorView.ConvertVerseTo(VersePosition.Comment);
        }

        private void btnConvertToSingleToEnd_Click(object sender, EventArgs e)
        {
            ganjoorView.ConvertToToEnd(VersePosition.Single);
        }

        private void btnConvertToParaToEnd_Click(object sender, EventArgs e)
        {
            ganjoorView.ConvertToToEnd(VersePosition.Paragraph);
        }

        private void btnDefaultToEnd_Click(object sender, EventArgs e)
        {
            ganjoorView.ConvertToToEnd(VersePosition.Right);
        }

        private void mnuCorrectVerses_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("این فرمان برای تصحیح جای مصرعهای اول و دوم طراحی شده و \nفقط باید در زمانی که به هم ریختگی وجود دارد استفاده شود.\nادامه می‌دهید؟",
                "اخطار",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2,
                MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.No)
                return;
            Enabled = false;
            ganjoorView.CurrectCurrentCatVerse();
            Enabled = true;

        }

        private void btnSpaceTabText_Click(object sender, EventArgs e) {
            using var dlg = new SpaceSeparatedPoem();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                ganjoorView.InsertVerses(dlg.ResulText.Split(new[] { (char)10, (char)13 }, StringSplitOptions.RemoveEmptyEntries), true, chkIgnoreBlankLines.Checked, chkIgnoreShortLines.Checked, 4);
            }
        }

        private void mnuBio_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("اجرای این فرمان باعث جایگزینی زندگینامهٔ شاعران با اطلاعات فایل ورودی می‌شود.\nادامه می‌دهید؟",
                "اخطار",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2,
                MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.No)
                return;

            using var dlg = new OpenFileDialog();
            dlg.Filter = "*.s3db|*.s3db";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (!ganjoorView.ImportDbPoetBioText(dlg.FileName))
                {
                    MessageBox.Show("خطا رخ داد.", "خطا");
                }
            }
        }

        private void mnuImport_Click(object sender, EventArgs e)
        {
            var nCurCatId = ganjoorView.CurrentCatId;
            var nCurPoetId = ganjoorView.CurrentPoetId;

            if (nCurCatId < 0 || nCurPoetId < 0)
            {
                MessageBox.Show("لطفا ابتدا وارد آثار شاعر (و بخش مورد نظر) شوید.");
                return;
            }
            #region Old Code
            if ((ModifierKeys | Keys.Shift) == ModifierKeys)
            {
                using var dlgOld = new TextImporter();
                if (dlgOld.ShowDialog(this) == DialogResult.OK)
                {
                    var fileName = dlgOld.FileName;
                    var mainCatText = dlgOld.MainCatText;
                    var subCatTexts = dlgOld.SubCatTexts;
                    var nMaxVerseTextLength = dlgOld.MaxVerseTextLength;
                    var bTabularVerses = dlgOld.TabularVerses;

                    Cursor = Cursors.WaitCursor;
                    Application.DoEvents();

                    var lines = File.ReadAllLines(fileName);

                    Application.DoEvents();








                    GanjoorCat curMainCat = null;
                    GanjoorPoem curPoem = null;

                    var nTotalLines = lines.Length;
                    var nCurLine = 0;

                    var nCurVerse = 0;



                    var dbBrowser = new DbBrowser();

                    dbBrowser.BeginBatchOperation();
                    while (nCurLine < nTotalLines)
                    {
                        if (lines[nCurLine].Contains(mainCatText))
                        {
                            curMainCat = dbBrowser.CreateNewCategory(lines[nCurLine].Trim(), nCurCatId, nCurPoetId);
                        }
                        else if (curMainCat != null)
                        {
                            var bNewCatOrPoem = false;
                            foreach (var subCatText in subCatTexts)
                            {
                                if (lines[nCurLine].Contains(subCatText))
                                {
                                    if (bTabularVerses)
                                    {
                                        ReArrangeTabularVerses(curPoem, dbBrowser);
                                    }
                                    curPoem = dbBrowser.CreateNewPoem(lines[nCurLine].Trim(), curMainCat._ID);
                                    bNewCatOrPoem = true;
                                    nCurVerse = 0;
                                    break;
                                }
                            }

                            if (!bNewCatOrPoem)
                            {
                                var line = lines[nCurLine].Trim();
                                if (!string.IsNullOrEmpty(line))
                                {
                                    var nWordsCount = line.Split(' ').Length;

                                    var bVerseDetected = false;

                                    if (nWordsCount <= nMaxVerseTextLength)
                                    {
                                        var nNextLine = nCurLine + 1;
                                        if (nNextLine < nTotalLines)
                                        {
                                            while (nNextLine < nTotalLines)
                                            {
                                                var nextLine = lines[nNextLine].Trim();
                                                if (string.IsNullOrEmpty(nextLine))
                                                    nNextLine++;
                                                else
                                                {
                                                    var nNextWordsCount = nextLine.Split(' ').Length;
                                                    if (nNextWordsCount <= nMaxVerseTextLength)
                                                    {
                                                        if (nextLine.Contains(mainCatText))
                                                        {
                                                            break;
                                                        }
                                                        var bBreak = false;
                                                        foreach (var subCatText in subCatTexts)
                                                        {
                                                            if (nextLine.Contains(subCatText))
                                                            {
                                                                bBreak = true;
                                                                break;
                                                            }
                                                        }
                                                        if (bBreak)
                                                            break;
                                                        if (curPoem == null)
                                                        {
                                                            MessageBox.Show("curPoem == null");
                                                            return;
                                                        }
                                                        bVerseDetected = true;

                                                        var v1 = dbBrowser.CreateNewVerse(curPoem._ID, nCurVerse, VersePosition.Right);
                                                        dbBrowser.SetVerseText(curPoem._ID, v1._Order, line);
                                                        nCurVerse++;
                                                        var v2 = dbBrowser.CreateNewVerse(curPoem._ID, nCurVerse, VersePosition.Left);
                                                        dbBrowser.SetVerseText(curPoem._ID, v2._Order, nextLine);
                                                        nCurVerse++;

                                                        nCurLine = nNextLine;
                                                        break;
                                                    }

                                                    break;
                                                }
                                            }
                                        }
                                    }

                                    if (!bVerseDetected)
                                    {
                                        if (curPoem == null)
                                        {
                                            MessageBox.Show("curPoem == null");
                                            return;
                                        }
                                        var p = dbBrowser.CreateNewVerse(curPoem._ID, nCurVerse, VersePosition.Paragraph);
                                        dbBrowser.SetVerseText(curPoem._ID, p._Order, line);
                                        nCurVerse++;
                                    }
                                }
                            }
                        }
                        nCurLine++;
                    }
                    if (bTabularVerses)
                    {
                        ReArrangeTabularVerses(curPoem, dbBrowser);
                    }
                    dbBrowser.CommitBatchOperation();
                    dbBrowser.CloseDb();


                    Cursor = Cursors.Default;
                    MessageBox.Show("انجام شد");

                    ganjoorView.Font = ganjoorView.Font;

                }

                return;
            }
            #endregion

            using var dlg = new GeneralTextImporter();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                var fileName = dlg.FileName;
                var starts = dlg.NextPoemStartText.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                var nextPoemStartIsAShortText = dlg.NextPoemStartIsAShortText;
                var nShortTextLength = dlg.ShortTextLength;



                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                var lines = File.ReadAllLines(fileName);

                Application.DoEvents();



                var nTotalLines = lines.Length;
                var nCurLine = 0;

                var nCurVerse = 0;

                var dbBrowser = new DbBrowser();
                var cat = dbBrowser.GetCategory(nCurCatId);
                if (cat == null)
                {
                    MessageBox.Show("cat == null");
                    dbBrowser.CloseDb();
                    return;
                }


                GanjoorPoem curPoem = null;

                dbBrowser.BeginBatchOperation();
                while (nCurLine < nTotalLines)
                {
                    var line = lines[nCurLine].Trim();
                    var startsWith = false;
                    foreach (var startText in starts)
                    {
                        if (line.IndexOf(startText) == 0)
                        {
                            startsWith = true;
                        }

                    }
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (
                            startsWith
                            ||
                            (nextPoemStartIsAShortText && line.Length < nShortTextLength)
                        )
                        {
                            curPoem = dbBrowser.CreateNewPoem(line, nCurCatId);
                            nCurVerse = 0;
                        }
                        else
                        if (curPoem != null)
                        {
                            var v = dbBrowser.CreateNewVerse(curPoem._ID, nCurVerse, nCurVerse % 2 == 0 ? VersePosition.Right : VersePosition.Left);
                            dbBrowser.SetVerseText(curPoem._ID, v._Order, line);
                            nCurVerse++;
                        }
                    }
                    nCurLine++;
                }
                dbBrowser.CommitBatchOperation();
                dbBrowser.CloseDb();


                Cursor = Cursors.Default;
                MessageBox.Show("انجام شد");

                ganjoorView.Font = ganjoorView.Font;

            }
        }

        private static void ReArrangeTabularVerses(GanjoorPoem curPoem, DbBrowser dbBrowser)
        {
            if (curPoem != null)
            {
                var verses = dbBrowser.GetVerses(curPoem._ID);

                var nVIndex = 0;
                while (nVIndex < verses.Count)
                {
                    if (verses[nVIndex]._Position == VersePosition.Right)
                    {
                        var vTexts = new List<string>();
                        vTexts.Add(verses[nVIndex]._Text);
                        var nStart = nVIndex;
                        nVIndex++;
                        if (nVIndex < verses.Count)
                        {
                            while (
                                nVIndex < verses.Count
                                &&
                                (
                                verses[nVIndex]._Position == VersePosition.Left
                                ||
                                verses[nVIndex]._Position == VersePosition.Right
                                )
                                )
                            {
                                vTexts.Add(verses[nVIndex]._Text);
                                nVIndex++;
                            }
                            var nEndPlusOne = nVIndex;
                            if (nEndPlusOne - nStart > 2)
                            {
                                var nText = 0;
                                for (var nRight = nStart; nRight < nEndPlusOne; nRight += 2, nText++)
                                {
                                    dbBrowser.SetVerseText(curPoem._ID, verses[nRight]._Order, vTexts[nText]);
                                }
                                for (var nLeft = nStart + 1; nLeft < nEndPlusOne; nLeft += 2, nText++)
                                {
                                    dbBrowser.SetVerseText(curPoem._ID, verses[nLeft]._Order, vTexts[nText]);
                                }

                            }
                            nVIndex--;
                        }

                    }
                    nVIndex++;
                }
            }
        }



        private void mnuExport_Click(object sender, EventArgs e) {
            using var eDlg = new TextExporter();
            if (eDlg.ShowDialog(this) == DialogResult.OK) {
                using var dlg = new FolderBrowserDialog();
                dlg.Description = "مسیر خروجیها را انتخاب کنید";
                dlg.ShowNewFolderButton = true;

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {

                    Cursor.Current = Cursors.WaitCursor;
                    Enabled = false;
                    Application.DoEvents();
                    var targetFolder = dlg.SelectedPath;

                    var dbBrowser = new DbBrowser();

                    for (var i = 0; i < dbBrowser.Poets.Count; i++)
                    {
                        var poet = dbBrowser.Poets[i];
                        switch (Settings.Default.ExportLevel)
                        {
                            case "Poet":
                                ExportPoetToTextFile(poet, targetFolder, dbBrowser);
                                break;
                            case "Cat":
                                ExportCatToTextFile(i, dbBrowser.GetCategory(poet._CatID), targetFolder, dbBrowser);
                                break;
                            default:
                                ExportCatToTextFile(i, dbBrowser.GetCategory(poet._CatID), targetFolder, dbBrowser, true);
                                break;
                        }
                    }

                    dbBrowser.CloseDb();

                    Cursor.Current = Cursors.Default;
                    Enabled = true;
                    MessageBox.Show("خروجی تولید شد.", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                }
            }
        }

        private void ExportPoetToTextFile(GanjoorPoet poet, string targetFolder, DbBrowser dbBrowser)
        {
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }

            var sb = new StringBuilder();
            if (Settings.Default.ExportPoetName)
            {
                var poetNameSeparator = Settings.Default.ExportPoetSep;
                if (!string.IsNullOrEmpty(poetNameSeparator))
                    sb.AppendLine(poetNameSeparator);
                sb.AppendLine(poet._Name);
                if (!string.IsNullOrEmpty(poetNameSeparator))
                    sb.AppendLine(poetNameSeparator);
            }

            var poetCat = dbBrowser.GetCategory(poet._CatID);
            DRY_ExportCat(dbBrowser, poetCat, sb);

            var lstSubCategories = dbBrowser.GetAllSubCats(poetCat._ID);
            foreach (var catId in lstSubCategories)
            {
                DRY_ExportCat(dbBrowser, dbBrowser.GetCategory(catId), sb);
            }
            File.WriteAllText(Path.Combine(targetFolder, GPersianTextSync.Farglisize(poet._Name) + ".txt"), sb.ToString());

        }


        private void ExportCatToTextFile(int nOrder, GanjoorCat cat, string targetFolder, DbBrowser dbBrowser, bool separateFiles = false)
        {
            var catInLatinLetters = (nOrder + 1).ToString("D3") + "-" + GPersianTextSync.Farglisize(cat._Text);
            if (catInLatinLetters.Length > 16)
                catInLatinLetters = catInLatinLetters[..16];
            var catFolder = Path.Combine(targetFolder, catInLatinLetters);
            if (!Directory.Exists(catFolder))
            {
                Directory.CreateDirectory(catFolder);
            }

            var sb = new StringBuilder();
            if (DRY_ExportCat(dbBrowser, cat, sb, separateFiles ? catFolder : ""))
            {
                if (!separateFiles)
                {
                    File.WriteAllText(Path.Combine(catFolder, catInLatinLetters + ".txt"), sb.ToString());
                }
            }


            var cats = dbBrowser.GetSubCategories(cat._ID);
            for (var i = 0; i < cats.Count; i++)
            {
                ExportCatToTextFile(i, cats[i], catFolder, dbBrowser, separateFiles);
            }
        }

        private bool DRY_ExportCat(DbBrowser dbBrowser, GanjoorCat cat, StringBuilder sb, string catFolderForSeparatePorms = "")
        {
            if (Settings.Default.ExportCatName)
            {
                var catNameSeparator = Settings.Default.ExportCatSep;
                if (!string.IsNullOrEmpty(catNameSeparator))
                    sb.AppendLine(catNameSeparator);
                sb.AppendLine(cat._Text);
                if (!string.IsNullOrEmpty(catNameSeparator))
                    sb.AppendLine(catNameSeparator);
            }

            var poems = dbBrowser.GetPoems(cat._ID);
            if (poems.Count > 0)
            {
                var exportPoemName = Settings.Default.ExportPoemName;
                var poemNameSeparator = Settings.Default.ExportPoemSep;
                foreach (var poem in poems)
                {
                    if (!string.IsNullOrEmpty(catFolderForSeparatePorms))
                    {
                        sb = new StringBuilder();
                    }
                    DRY_ExportPoem(dbBrowser, poem, sb, exportPoemName, poemNameSeparator);
                    if (!string.IsNullOrEmpty(catFolderForSeparatePorms))
                    {
                        File.WriteAllText(Path.Combine(catFolderForSeparatePorms, poem._ID + ".txt"), sb.ToString());
                    }
                }
                return true;
            }
            return false;
        }


        private void DRY_ExportPoem(DbBrowser dbBrowser, GanjoorPoem poem, StringBuilder sb, bool exportPoemName, string poemNameSeparator)
        {
            if (exportPoemName)
            {
                if (!string.IsNullOrEmpty(poemNameSeparator))
                    sb.AppendLine(poemNameSeparator);
                sb.AppendLine(poem._Title);
                if (!string.IsNullOrEmpty(poemNameSeparator))
                    sb.AppendLine(poemNameSeparator);
            }
            foreach (var verse in dbBrowser.GetVerses(poem._ID))
            {
                sb.AppendLine(verse._Text);
            }
        }

        private void Mnu2verseSplit_Click(object sender, EventArgs e)
        {
            var nPoemId = ganjoorView.CurrentPoemId;
            if (nPoemId < 1)
            {
                MessageBox.Show("لطفا شعری را انتخاب کنید.");
                return;
            }
            if (MessageBox.Show("با اجرای این فرمان هر دو بیت شعر جدیدی محسوب می‌شود. شعرهای جدید به بخش جاری اضافه می‌شوند.\n\rآیا ادامه می‌دهید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.Yes)
            {
                var dbBrowser = new DbBrowser();
                var verses = dbBrowser.GetVerses(nPoemId);

                var nCatId = ganjoorView.CurrentCatId;
                var nPoemNo = 0;
                var nLastPoemId = nPoemId;

                for (var nIndex = 0; nIndex < verses.Count; nIndex += 4)
                {
                    nPoemNo++;
                    dbBrowser.BeginBatchOperation();
                    var newPoem = dbBrowser.CreateNewPoem("شمارهٔ " + GPersianTextSync.Sync(nPoemNo.ToString()), nCatId);
                    nLastPoemId = newPoem._ID;
                    for (var i = 0; i < 4; i++)
                    {
                        var v = dbBrowser.CreateNewVerse(newPoem._ID, i, verses[nIndex + i]._Position);
                        dbBrowser.SetVerseText(newPoem._ID, v._Order, verses[nIndex + i]._Text);
                    }
                    dbBrowser.CommitBatchOperation();
                }
                if (MessageBox.Show("آیا تمایل دارید شعر جاری را حذف کنید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign) == DialogResult.Yes)
                {
                    dbBrowser.DeletePoem(nPoemId);
                }
                ganjoorView.ShowPoem(dbBrowser.GetPoem(nLastPoemId), true);
                dbBrowser.CloseDb();


            }

        }

        private void btnAppendToPre_Click(object sender, EventArgs e)
        {
            var nPoemId = ganjoorView.CurrentPoemId;
            if (nPoemId < 1)
            {
                MessageBox.Show("لطفا شعری را انتخاب کنید.");
                return;
            }

            if (
               MessageBox.Show("آیا از افزودن این شعر به انتهای شعر پیشین اطمینان دارید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign)
               ==
               DialogResult.Yes
               )
            {
                var dbBrowser = new DbBrowser();
                var nCatId = ganjoorView.CurrentCatId;
                var prePoem = dbBrowser.GetPreviousPoem(nPoemId, nCatId);

                var verses = dbBrowser.GetVerses(nPoemId);

                var preVerses = dbBrowser.GetVerses(prePoem._ID);

                var verseOrder = preVerses[^1]._Order;

                foreach (var verse in verses)
                {
                    var v = dbBrowser.CreateNewVerse(prePoem._ID, verseOrder + 1, verse._Position);
                    dbBrowser.SetVerseText(prePoem._ID, v._Order, verse._Text);

                    verseOrder = v._Order;
                }

                dbBrowser.CloseDb();

                MessageBox.Show("انجام شد. شعر قبلی را بررسی کنید و در صورت نیاز این شعر را به صورت دستی پاک کنید.");
            }
        }

        private void mnuSplit_Click(object sender, EventArgs e)
        {
            var nPoemId = ganjoorView.CurrentPoemId;
            if (nPoemId < 1)
            {
                MessageBox.Show("لطفا شعری را انتخاب کنید.");
                return;
            }
            #region Old Code
            if ((ModifierKeys | Keys.Shift) == ModifierKeys)
            {
                if (MessageBox.Show("با اجرای این فرمان بیتهای با مصرع خالی یا طول کمتر از ۴ آغاز شعر جدید محسوب می‌شوند.\n\rاشعار جدید به بخش جاری اضافه می‌شوند.\n\rآیا ادامه می‌دهید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.Yes)
                {
                    var dbBrowser = new DbBrowser();
                    var verses = dbBrowser.GetVerses(nPoemId);
                    var nPoemStartVerseIndex = 0;

                    var nCatId = ganjoorView.CurrentCatId;
                    var nPoemNo = 0;
                    var nLastPoemId = nPoemId;

                    for (var nIndex = 0; nIndex < verses.Count; nIndex++)
                    {
                        if (verses[nIndex]._Text.Length < 4)
                        {
                            if (nIndex == nPoemStartVerseIndex)
                            {
                                nPoemStartVerseIndex++;
                            }
                            else
                            {
                                nPoemNo++;
                                dbBrowser.BeginBatchOperation();
                                var newPoem = dbBrowser.CreateNewPoem("شمارهٔ " + GPersianTextSync.Sync(nPoemNo.ToString()), nCatId);
                                nLastPoemId = newPoem._ID;
                                for (var i = nPoemStartVerseIndex; i < nIndex; i++)
                                {
                                    var v = dbBrowser.CreateNewVerse(newPoem._ID, i - nPoemStartVerseIndex, verses[i]._Position);
                                    dbBrowser.SetVerseText(newPoem._ID, v._Order, verses[i]._Text);
                                }
                                dbBrowser.CommitBatchOperation();
                                nPoemStartVerseIndex = nIndex + 1;
                            }
                        }
                    }
                    ganjoorView.ShowPoem(dbBrowser.GetPoem(nLastPoemId), true);
                    dbBrowser.CloseDb();
                }
                return;
            }
            #endregion
            {
                var linePosition = ganjoorView.GetCurrentLine();
                var dbBrowser = new DbBrowser();
                var verses = dbBrowser.GetVerses(nPoemId);
                var verseIndex = 0;
                for (var i = 0; i < verses.Count; i++)
                {
                    if (verses[i]._Order == linePosition)
                    {
                        verseIndex = i;
                        break;
                    }
                }

                if (verses[verseIndex]._Position == VersePosition.Left || verses[verseIndex]._Position == VersePosition.CenteredVerse2)
                    if (verseIndex > 0)
                        verseIndex--;






                var nCatId = ganjoorView.CurrentCatId;

                var nNextId = -1;

                var msg = $"از «{verses[verseIndex]}» به شعر جدید شکسته شود؟";
                if (MessageBox.Show(msg, "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.Yes)
                {
                    var newPoem = dbBrowser.CreateNewPoem("", nCatId);
                    var nNewPoemId = newPoem._ID;
                    var deletingVOrders = new List<int>();
                    for (var i = verseIndex; i < verses.Count; i++)
                    {
                        var v = dbBrowser.CreateNewVerse(newPoem._ID, i - verseIndex, verses[i]._Position);
                        dbBrowser.SetVerseText(newPoem._ID, v._Order, verses[i]._Text);
                        deletingVOrders.Add(verses[i]._Order);
                    }

                    dbBrowser.DeleteVerses(nPoemId, deletingVOrders);

                    nNextId = nNewPoemId;



                    //Reorder poems so that the new one falls after current one

                    var poems = dbBrowser.GetPoems(nCatId);
                    dbBrowser.BeginBatchOperation();
                    var firstNextPoemMet = false;

                    for (var i = 0; i < poems.Count; i++)
                    {
                        var poem = poems[i];
                        if (poem._ID > nPoemId && poem._ID != nNewPoemId)
                        {
                            if (!firstNextPoemMet)
                            {
                                dbBrowser.SetPoemID(nNewPoemId, -poem._ID);
                                firstNextPoemMet = true;
                                nNextId = poem._ID;
                            }
                            dbBrowser.SetPoemID(poem._ID, -poems[i + 1]._ID);
                        }
                    }
                    dbBrowser.CommitBatchOperation();

                    poems = dbBrowser.GetPoems(nCatId);
                    dbBrowser.BeginBatchOperation();
                    foreach (var poem in poems)
                    {
                        if (poem._ID < 0)
                        {
                            dbBrowser.SetPoemID(poem._ID, -poem._ID);
                        }
                    }
                    dbBrowser.CommitBatchOperation();


                }
                ganjoorView.ShowPoem(dbBrowser.GetPoem(nNextId), true);
                dbBrowser.CloseDb();
            }

        }

        private void btnBreakParagraph_Click(object sender, EventArgs e)
        {
            if (!ganjoorView.BreakParagraph())
            {
                MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            }
        }

        private void mnuSingleVerseSplit_Click(object sender, EventArgs e)
        {
            var nPoemId = ganjoorView.CurrentPoemId;
            if (nPoemId < 1)
            {
                MessageBox.Show("لطفا شعری را انتخاب کنید.");
                return;
            }
            if (MessageBox.Show("با اجرای این فرمان هر بیت شعر جدیدی محسوب می‌شود. شعرهای جدید به بخش جاری اضافه می‌شوند.\n\rآیا ادامه می‌دهید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.Yes)
            {
                var dbBrowser = new DbBrowser();
                var verses = dbBrowser.GetVerses(nPoemId);

                var nCatId = ganjoorView.CurrentCatId;
                var nPoemNo = 0;
                var nLastPoemId = nPoemId;

                for (var nIndex = 0; nIndex < verses.Count; nIndex += 2)
                {
                    nPoemNo++;
                    dbBrowser.BeginBatchOperation();
                    var newPoem = dbBrowser.CreateNewPoem("شمارهٔ " + GPersianTextSync.Sync(nPoemNo.ToString()), nCatId);
                    nLastPoemId = newPoem._ID;
                    for (var i = 0; i < 2; i++)
                    {
                        var v = dbBrowser.CreateNewVerse(newPoem._ID, i, verses[nIndex + i]._Position);
                        dbBrowser.SetVerseText(newPoem._ID, v._Order, verses[nIndex + i]._Text);
                    }
                    dbBrowser.CommitBatchOperation();
                }
                if (MessageBox.Show("آیا تمایل دارید شعر جاری را حذف کنید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign) == DialogResult.Yes)
                {
                    dbBrowser.DeletePoem(nPoemId);
                }
                ganjoorView.ShowPoem(dbBrowser.GetPoem(nLastPoemId), true);
                dbBrowser.CloseDb();


            }
        }

        private void btnTechnicalProblems_Click(object sender, EventArgs e)
        {
            var dbBrowser = new DbBrowser();
            var list = dbBrowser.GetVersesWithTechnicalProblems(true);
            if (list.Count == 0)
            {
                MessageBox.Show("مشکلی نبود.");
            }
            else
            {
                ganjoorView.ShowPoem(dbBrowser.GetPoem(list[0]._PoemID), true);
                MessageBox.Show(list[0]._Text);
            }
            dbBrowser.CloseDb();
        }

        private void btnRhymeError_Click(object sender, EventArgs e)
        {
            var nPoemId = ganjoorView.CurrentPoemId;
            if (nPoemId < 1)
            {
                MessageBox.Show("لطفا شعری را انتخاب کنید.");
                return;
            }
            var dbBrowser = new DbBrowser();
            var verses = dbBrowser.GetVerses(nPoemId);
            var ravi = RhymeFinder.FindRhyme(verses);
            if (!string.IsNullOrEmpty(ravi.Rhyme))
            {
                MessageBox.Show($"خطایی روی نداد. حروف قافیه: {ravi.Rhyme}");
            }
            else
            {
                MessageBox.Show(ravi.FailVerse);
                ganjoorView.HighlightText(ravi.FailVerse);
            }

            dbBrowser.CloseDb();
        }
    }
}
