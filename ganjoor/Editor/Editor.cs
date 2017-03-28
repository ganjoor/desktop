using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ganjoor
{
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();
            this.ganjoorView.ShowBeytNums = false;
            //this.ganjoorView.MesraWidth = 250;
            this.ganjoorView.CenteredView = true;
            this.ganjoorView.Font = ganjoor.Properties.Settings.Default.ViewFont;
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

        private void btnNewPoet_ButtonClick(object sender, EventArgs e)
        {
            using (ItemEditor dlg = new ItemEditor())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if(!ganjoorView.NewPoet(dlg.ItemName))
                        MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                }
            }
        }

        private void btnEditPoet_Click(object sender, EventArgs e)
        {
            using (ItemEditor dlg = new ItemEditor())
            {
                dlg.ItemName = ganjoorView.CurrentPoet;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (!ganjoorView.EditPoet(dlg.ItemName))
                        MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                }
            }
        }

        private void btnNewCat_ButtonClick(object sender, EventArgs e)
        {
            using (ItemEditor dlg = new ItemEditor(EditItemType.Category))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if(!ganjoorView.NewCat(dlg.ItemName))
                        MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);

                }
            }
        }

        private void btnEditPoetBio_Click(object sender, EventArgs e)
        {
            using (MemoEditor dlg = new MemoEditor(ganjoorView.CurrentPoetBio))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (!ganjoorView.EditPoetBio(dlg.MemoText))
                        MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                }
            }

        }


        private void btnEditCat_Click(object sender, EventArgs e)
        {
            using (ItemEditor dlg = new ItemEditor(EditItemType.Category))
            {
                dlg.ItemName = ganjoorView.CurrentCategory;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (!ganjoorView.EditCat(dlg.ItemName))
                        MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                }
            }
        }

        private void btnNewPoem_ButtonClick(object sender, EventArgs e)
        {
            using (ItemEditor dlg = new ItemEditor(EditItemType.Poem))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (!ganjoorView.NewPoem(dlg.ItemName))
                        MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);

                }
            }
        }

        private void btnEditPoem_Click(object sender, EventArgs e)
        {
            using (ItemEditor dlg = new ItemEditor(EditItemType.Poem))
            {
                dlg.ItemName = ganjoorView.CurrentPoem;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (!ganjoorView.EditPoem(dlg.ItemName))
                        MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                }
            }
        }

        private void btnDeletePoem_Click(object sender, EventArgs e)
        {
            if(
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

        private void btnExportPoet_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "*.gdb|*.gdb";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (File.Exists(dlg.FileName))
                        File.Delete(dlg.FileName);
                    if(ganjoorView.ExportPoet(dlg.FileName))
                        MessageBox.Show("خروجی تولید شد.", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                    else
                        MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                }
            }
        }

        private void btnExportCat_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
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
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            ganjoorView.Save();
        }

        private void btnReOrderCat_Click(object sender, EventArgs e)
        {
            ganjoorView.Save();
            ganjoorView.StoreSettings();
            this.Hide();
            using (ReOrderCat dlg = new ReOrderCat())
            {
                dlg.ShowDialog(this);
            }
            this.Show();
            ganjoorView.Font = ganjoorView.Font;
        }

        private void btnReOrderSubCat_Click(object sender, EventArgs e)
        {
            ganjoorView.Save();
            ganjoorView.StoreSettings();
            this.Hide();
            using (ReOrderSubCats dlg = new ReOrderSubCats())
            {
                dlg.ShowDialog(this);
            }
            this.Show();
            ganjoorView.Font = ganjoorView.Font;
        }

        private void btnImportFromTextFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Unicode Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    string srcText = File.ReadAllText(dlg.FileName);
                    ganjoorView.InsertVerses(srcText.Split(new char[] { (char)10, (char)13 }, StringSplitOptions.RemoveEmptyEntries), !chkEachlineOneverse.Checked, chkIgnoreBlankLines.Checked, chkIgnoreShortLines.Checked, 4);
                    ganjoorView.Save();
                }
            }
        }

        private void btnImportFromClipboard_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                ganjoorView.InsertVerses(Clipboard.GetText().Split(new char[] { (char)10, (char)13 }, StringSplitOptions.RemoveEmptyEntries), !chkEachlineOneverse.Checked, chkIgnoreBlankLines.Checked, chkIgnoreShortLines.Checked, 4);
                ganjoorView.Save();                
            }
            else
                MessageBox.Show("متنی در کلیپ بورد نیست.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnImportFromClipboadStructuredPoem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                using (PoemStructure dlg = new PoemStructure())
                {
                    if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        ganjoorView.InsertVerses(Clipboard.GetText().Split(new char[] { (char)10, (char)13 }, StringSplitOptions.RemoveEmptyEntries), dlg.LinesCount, dlg.FullLine, chkIgnoreBlankLines.Checked, chkIgnoreShortLines.Checked, 4);
                        ganjoorView.Save();
                    }
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
        private Point thumbPos = new Point();
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

        private void btnMergeTwoTextColumns_Click(object sender, EventArgs e)
        {
            using (MrgTwoClmns dlg = new MrgTwoClmns())
            {
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    ganjoorView.InsertVerses(dlg.ResulText.Split(new char[] { (char)10, (char)13 }, StringSplitOptions.RemoveEmptyEntries), true, chkIgnoreBlankLines.Checked, chkIgnoreShortLines.Checked, 4);
                }
            }
        }

        private void btnReplaceInDb_Click(object sender, EventArgs e)
        {
            using (ReplaceInDb dlg = new ReplaceInDb())
                dlg.ShowDialog(this);
               
        }

        private void btnChangeIDs_Click(object sender, EventArgs e)
        {
            using (IDChanger dlg = new IDChanger())
            {
                int PoetID, MinCatID, MinPoemID;
                ganjoorView.GetIDs(out PoetID, out MinCatID, out MinPoemID);
                dlg.PoetID = PoetID;
                dlg.StartCatID = MinCatID;
                dlg.StartPoemID = MinPoemID;
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    this.Enabled = false;                       
                    ganjoorView.SetIDs(dlg.PoetID, dlg.StartCatID, dlg.StartPoemID);
                    this.Enabled = true;
                }

            }
        }

        private void btnMoveToCategory_Click(object sender, EventArgs e)
        {
            using (CategorySelector dlg = new CategorySelector(ganjoorView.GetCurrentPoetID()))
            {
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    ganjoorView.MoveToCategory(dlg.SelectedCatID);
                }
            }

        }

        private void btnGDBListEditor_Click(object sender, EventArgs e)
        {
            using (GDBListEditor dlg = new GDBListEditor())
                dlg.ShowDialog(this);
        }

        private void btnReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ItemEditor dlgFind = new ItemEditor(EditItemType.General, "متن جستجو", "متن جستجو"))
            {
                if (dlgFind.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    string strFindText = dlgFind.ItemName;

                    using (ItemEditor dlgReplace = new ItemEditor(EditItemType.General, "متن جایگزین", "متن جایگزین"))
                    {
                        if (dlgReplace.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            string strReplaceText = dlgReplace.ItemName;
                            if(MessageBox.Show(String.Format("از جایگزینی «{0}» با «{1}» در شعر جاری اطمینان دارید؟", strFindText, strReplaceText),
                                "پرسش و هشدار", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                            return;

                            this.ganjoorView.ReplaceText(strFindText, strReplaceText);
                        }

                    }
                }                
            }
        }

        private void btnRestructurePoem_Click(object sender, EventArgs e)
        {
            using (PoemStructure dlg = new PoemStructure())
            {
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    ganjoorView.RestructureVerses(dlg.LinesCount, dlg.FullLine, -1, false);
                    ganjoorView.Save();
                }
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

        private void mnuCorrectVerses_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("این فرمان برای تصحیح جای مصرعهای اول و دوم طراحی شده و \nفقط باید در زمانی که به هم ریختگی وجود دارد استفاده شود.\nادامه می‌دهید؟",
                "اخطار",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2,
                MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == System.Windows.Forms.DialogResult.No)
                return;
            this.Enabled = false;
            ganjoorView.CurrectCurrentCatVerse();
            this.Enabled = true;

        }

        private void btnSpaceTabText_Click(object sender, EventArgs e)
        {
            using (SpaceSeparatedPoem dlg = new SpaceSeparatedPoem())
            {
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    ganjoorView.InsertVerses(dlg.ResulText.Split(new char[] { (char)10, (char)13 }, StringSplitOptions.RemoveEmptyEntries), true, chkIgnoreBlankLines.Checked, chkIgnoreShortLines.Checked, 4);
                }
            }
        }

        private void mnuBio_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("اجرای این فرمان باعث جایگزینی زندگینامهٔ شاعران با اطلاعات فایل ورودی می‌شود.\nادامه می‌دهید؟",
                "اخطار",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2,
                MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == System.Windows.Forms.DialogResult.No)
                return;

            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "*.s3db|*.s3db";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (!ganjoorView.ImportDbPoetBioText(dlg.FileName))
                    {
                        MessageBox.Show("خطا رخ داد.", "خطا");
                    }
                }
            }
        }

        private void mnuImport_Click(object sender, EventArgs e)
        {
            int nCurCatId = ganjoorView.CurrentCatId;
            int nCurPoetId = ganjoorView.CurrentPoetId;

            if(nCurCatId < 0 || nCurPoetId < 0)
            {
                MessageBox.Show("لطفا ابتدا وارد آثار شاعر (و بخش مورد نظر) شوید.");
                return;
            }


            using (TextImporter dlg = new TextImporter())
            {
                if(dlg.ShowDialog(this) == DialogResult.OK)
                {
                    string fileName = dlg.FileName;
                    string mainCatText = dlg.MainCatText;
                    string[] subCatTexts = dlg.SubCatTexts;
                    int nMaxVerseTextLength = dlg.MaxVerseTextLength;
                    bool bTabularVerses = dlg.TabularVerses;

                    Cursor = Cursors.WaitCursor;
                    Application.DoEvents();

                    string[] lines  = File.ReadAllLines(fileName);

                    Application.DoEvents();

                   

                       
                    

                    

                    GanjoorCat curMainCat = null;
                    GanjoorPoem curPoem = null;

                    int nTotalLines = lines.Length;
                    int nCurLine = 0;

                    int nCurVerse = 0;

                  

                    DbBrowser dbBrowser = new DbBrowser();

                    dbBrowser.BeginBatchOperation();
                    while (nCurLine < nTotalLines)
                    {
                        if (lines[nCurLine].Contains(mainCatText))
                        {
                            curMainCat = dbBrowser.CreateNewCategory(lines[nCurLine].Trim(), nCurCatId, nCurPoetId);                            
                        }
                        else if(curMainCat != null)
                        {
                            bool bNewCatOrPoem = false;
                            foreach(string subCatText in subCatTexts)
                            {
                                if(lines[nCurLine].Contains(subCatText))
                                {
                                    if(bTabularVerses)
                                    {
                                        ReArrangeTabularVerses(curPoem, dbBrowser);
                                    }
                                    curPoem = dbBrowser.CreateNewPoem(lines[nCurLine].Trim(), curMainCat._ID);
                                    bNewCatOrPoem = true;
                                    nCurVerse = 0;
                                    break;
                                }
                            }

                            if(!bNewCatOrPoem)
                            {
                                string line = lines[nCurLine].Trim();
                                if(!string.IsNullOrEmpty(line))
                                {
                                    int nWordsCount = line.Split(' ').Length;

                                    bool bVerseDetected = false;

                                    if(nWordsCount <= nMaxVerseTextLength)
                                    {
                                        int nNextLine = nCurLine + 1;
                                        if(nNextLine  < nTotalLines)
                                        {
                                            while(nNextLine < nTotalLines)
                                            {
                                                string nextLine = lines[nNextLine].Trim();
                                                if (string.IsNullOrEmpty(nextLine))
                                                    nNextLine++;
                                                else
                                                {
                                                    int nNextWordsCount = nextLine.Split(' ').Length;
                                                    if (nNextWordsCount <= nMaxVerseTextLength)
                                                    {
                                                        if(nextLine.Contains(mainCatText))
                                                        {
                                                            break;
                                                        }
                                                        bool bBreak = false;
                                                        foreach (string subCatText in subCatTexts)
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

                                                        GanjoorVerse v1 = dbBrowser.CreateNewVerse(curPoem._ID, nCurVerse, VersePosition.Right);
                                                        dbBrowser.SetVerseText(curPoem._ID, v1._Order, line);
                                                        nCurVerse++;
                                                        GanjoorVerse v2 = dbBrowser.CreateNewVerse(curPoem._ID, nCurVerse, VersePosition.Left);
                                                        dbBrowser.SetVerseText(curPoem._ID, v2._Order, nextLine);
                                                        nCurVerse++;

                                                        nCurLine = nNextLine;
                                                        break;
                                                    }
                                                    else
                                                        break;
                                                }
                                            }
                                        }
                                    }

                                    if(!bVerseDetected)
                                    {
                                        if (curPoem == null)
                                        {                                           
                                            MessageBox.Show("curPoem == null");
                                            return;
                                        }                                       
                                        GanjoorVerse p = dbBrowser.CreateNewVerse(curPoem._ID, nCurVerse, VersePosition.Paragraph);
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
            }
        }

        private static void ReArrangeTabularVerses(GanjoorPoem curPoem, DbBrowser dbBrowser)
        {
            if (curPoem != null)
            {
                List<GanjoorVerse> verses = dbBrowser.GetVerses(curPoem._ID);

                int nVIndex = 0;
                while (nVIndex < verses.Count)
                {
                    if (verses[nVIndex]._Position == VersePosition.Right)
                    {
                        List<string> vTexts = new List<string>();
                        vTexts.Add(verses[nVIndex]._Text);
                        int nStart = nVIndex;
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
                            int nEndPlusOne = nVIndex;
                            if ((nEndPlusOne - nStart) > 2)
                            {
                                int nText = 0;
                                for (int nRight = nStart; nRight < nEndPlusOne; nRight += 2, nText++)
                                {
                                    dbBrowser.SetVerseText(curPoem._ID, verses[nRight]._Order, vTexts[nText]);
                                }
                                for (int nLeft = nStart + 1; nLeft < nEndPlusOne; nLeft += 2, nText++)
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
    }
}
