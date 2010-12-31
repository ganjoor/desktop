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
                btnNextPoem.Text = "صفحۀ بعد";
                btnNextPoem.Enabled = nextItem != null;
                btnNextPoem.Tag = nextItem;
                btnPreviousPoem.Text = "صفحۀ قبل";
                btnPreviousPoem.Enabled = preItem != null;
                btnPreviousPoem.Tag = preItem;
            }
            btnReOrderCat.Enabled = btnExportPoet.Enabled = btnNewCat.Enabled = btnNewPoem.Enabled = btnEditPoet.Enabled = btnEditPoetBio.Enabled = btnDeletePoet.Enabled = PageString != "خانه";
            btnExportCat.Enabled = btnEditCat.Enabled = btnDeleteCat.Enabled = !ganjoorView.IsInPoetRootPage;
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





    }
}
