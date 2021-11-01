using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ganjoor.Properties;

namespace ganjoor
{
    public partial class ReOrderCat : Form
    {
        public ReOrderCat()
        {
            InitializeComponent();

        }


        private DbBrowser _db;

        private List<int> _IDs = new List<int>();

        private const int ClmnTitle     = 0;
        private const int ClmnVerse1    = 1;
        private const int ClmnVerse2    = 2;
        private const int ClmnRAVI      = 3;
        private const int ClmnRAVIAX    = 4;
        private const int ClmnID        = 5;

        private void ReOrderCat_Load(object sender, EventArgs e)
        {
            LoadGridData();
        }

        private void LoadGridData()
        {
            if (_db == null)
                _db = new DbBrowser();

            _IDs.Clear();
            grdMain.Rows.Clear();

            List<GanjoorPoem> Poems = _db.GetPoems(Settings.Default.LastCat);
            grdMain.SuspendLayout();
            foreach (GanjoorPoem Poem in Poems)
            {
                int rowIndex = grdMain.Rows.Add();

                _IDs.Add(Poem._ID);

                grdMain.Rows[rowIndex].Cells[ClmnID].Value = Poem._ID;

                grdMain.Rows[rowIndex].Cells[ClmnTitle].Value = Poem._Title;

                List<GanjoorVerse> Verses = _db.GetVerses(Poem._ID, 2);
                if (Verses.Count > 0)
                    grdMain.Rows[rowIndex].Cells[ClmnVerse1].Value = Verses[0]._Text;
                if (Verses.Count > 1)
                    grdMain.Rows[rowIndex].Cells[ClmnVerse2].Value = Verses[1]._Text;
            }
            grdMain.ResumeLayout();
            lblPoemCount.Text = String.Format("{0} شعر", grdMain.RowCount);
        }

        private void grdMain_SelectionChanged(object sender, EventArgs e)
        {
            llblSelectionCount.Text = String.Format("{0} عنوان انتخاب شده", grdMain.SelectedRows.Count);
        }

        private static int CompareGridRows(DataGridViewRow Row1, DataGridViewRow Row2)
        {
            return Row1.Index.CompareTo(Row2.Index);
        }

        private static int CompareGridRowsReversed(DataGridViewRow Row1, DataGridViewRow Row2)
        {
            return Row2.Index.CompareTo(Row1.Index);
        }


        private void btnMoveFirst_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> lstSelected = new List<DataGridViewRow>();
            foreach (DataGridViewRow Row in grdMain.SelectedRows)
                lstSelected.Add(Row);
            foreach (DataGridViewRow Row in lstSelected)
            {
                grdMain.Rows.Remove(Row);
            }
            lstSelected.Sort(CompareGridRows);
            grdMain.Rows.Insert(0, lstSelected.Count);
            grdMain.ClearSelection();
            for (int iRow = 0; iRow < lstSelected.Count ; iRow++)
            {                
                for (int iCell = 0; iCell < lstSelected[iRow].Cells.Count; iCell++)
                    grdMain.Rows[iRow].Cells[iCell].Value = lstSelected[iRow].Cells[iCell].Value;
                grdMain.Rows[iRow].Selected = true;
            }
            
        }

        private void btnMoveLast_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> lstSelected = new List<DataGridViewRow>();
            foreach (DataGridViewRow Row in grdMain.SelectedRows)
                lstSelected.Add(Row);
            foreach (DataGridViewRow Row in lstSelected)
            {
                grdMain.Rows.Remove(Row);
            }
            lstSelected.Sort(CompareGridRows);
            int oldRowCount = grdMain.Rows.Count;
            grdMain.Rows.Insert(oldRowCount , lstSelected.Count);
            grdMain.ClearSelection();
            for (int iRow = 0; iRow < lstSelected.Count; iRow++)
            {
                for (int iCell = 0; iCell < lstSelected[iRow].Cells.Count; iCell++)
                    grdMain.Rows[iRow + oldRowCount].Cells[iCell].Value = lstSelected[iRow].Cells[iCell].Value;
                grdMain.Rows[iRow + oldRowCount].Selected = true;
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> lstSelected = new List<DataGridViewRow>();
            foreach (DataGridViewRow Row in grdMain.SelectedRows)
                lstSelected.Add(Row);
            List<DataGridViewRow> lstNewSelection = new List<DataGridViewRow>();
            lstSelected.Sort(CompareGridRows);
            foreach (DataGridViewRow Row in lstSelected)
            {
                int RowIndex = Row.Index;
                if (RowIndex > 0)
                {
                    RowIndex--;
                    grdMain.Rows.Insert(RowIndex, 1);
                    grdMain.Rows.Remove(Row);

                    for (int iCell = 0; iCell < Row.Cells.Count; iCell++)
                        grdMain.Rows[RowIndex].Cells[iCell].Value = Row.Cells[iCell].Value;
                }
                lstNewSelection.Add(grdMain.Rows[RowIndex]);
            }
            grdMain.ClearSelection();
            foreach (DataGridViewRow Row in lstNewSelection)
                Row.Selected = true;

        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> lstSelected = new List<DataGridViewRow>();
            foreach (DataGridViewRow Row in grdMain.SelectedRows)
                lstSelected.Add(Row);
            List<DataGridViewRow> lstNewSelection = new List<DataGridViewRow>();
            lstSelected.Sort(CompareGridRowsReversed);
            foreach (DataGridViewRow Row in lstSelected)
            {
                int RowIndex = Row.Index;
                if (RowIndex < grdMain.RowCount - 1)
                {                    
                    grdMain.Rows.Insert(RowIndex + 2, 1);
                    grdMain.Rows.Remove(Row);

                    RowIndex++;


                    for (int iCell = 0; iCell < Row.Cells.Count; iCell++)
                        grdMain.Rows[RowIndex].Cells[iCell].Value = Row.Cells[iCell].Value;
                    lstNewSelection.Add(grdMain.Rows[RowIndex]);
                }
                lstNewSelection.Add(grdMain.Rows[RowIndex]);
            }
            grdMain.ClearSelection();
            foreach (DataGridViewRow Row in lstNewSelection)
                Row.Selected = true;
        }

        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
            _db.BeginBatchOperation();
            for (int iRow = 0; iRow < grdMain.RowCount; iRow++)
            {
                int PoemID = Convert.ToInt32(grdMain.Rows[iRow].Cells[ClmnID].Value);
                _db.SetPoemID(PoemID, -_IDs[iRow]);
                grdMain.Rows[iRow].Cells[ClmnID].Value = _IDs[iRow];
            }
            _db.CommitBatchOperation();
            _db.BeginBatchOperation();
            foreach (DataGridViewRow Row in grdMain.Rows)
            {
                int PoemID = -Convert.ToInt32(Row.Cells[ClmnID].Value);
                _db.SetPoemID(PoemID, -PoemID);
            }
            _db.CommitBatchOperation();
        }

        private void btnMoveToCat_Click(object sender, EventArgs e)
        {
            int PoetId = _db.GetCategory(Settings.Default.LastCat)._PoetID;
            using (CategorySelector dlg = new CategorySelector(PoetId))
            {
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    int NewCatId = dlg.SelectedCatID;
                    if (NewCatId == Settings.Default.LastCat)
                        MessageBox.Show("شما بخش جاری را انتخاب کرده‌اید!");
                    else
                    {
                        GanjoorCat cat = _db.GetCategory(NewCatId);
                        if (MessageBox.Show(string.Format("از انتقال {0} شعر انتخابی از بخش «{1}» به بخش «{2}» اطمینان دارید؟", grdMain.SelectedRows.Count, _db.GetCategory(Settings.Default.LastCat)._Text, cat._Text),
                            "تأییدیه", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                            return;
                        _db.BeginBatchOperation();
                        foreach(DataGridViewRow Row in grdMain.SelectedRows)
                        {
                            int PoemID = Convert.ToInt32(Row.Cells[ClmnID].Value);
                            _db.SetPoemCatID(PoemID, NewCatId);
                        }
                        _db.CommitBatchOperation();
                        LoadGridData();
                    }
                }
            }
        }

        private void btnGroupNaming_Click(object sender, EventArgs e)
        {
            bool bPrefix = MessageBox.Show("آیا تمایل دارید این نام به عنوان پیشوند به عنوان فعلی اضافه شود (در غیر این صورت عنوان فعلی حذف می‌شود)؟",
                            "تأییدیه", MessageBoxButtons.YesNo) ==DialogResult.Yes;

            if (MessageBox.Show("آیا از ادامهٔ عملیات اطمینان دارید؟",
                            "تأییدیه", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            _db.BeginBatchOperation();
            int nNum = 0;
            foreach (DataGridViewRow Row in grdMain.Rows)
            {
                nNum++;
                string newTitle = $"شمارهٔ { GPersianTextSync.Sync(nNum.ToString())}";
                if(bPrefix)
                {
                    newTitle = $"{newTitle} - {Row.Cells[ClmnTitle].Value}";
                }
                _db.SetPoemTitle(Convert.ToInt32(Row.Cells[ClmnID].Value), newTitle);
            }
            _db.CommitBatchOperation();
            LoadGridData();

        }

        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private void btnFillRhymes_Click(object sender, EventArgs e)
        {
            Enabled = false;
            Application.DoEvents();
            foreach (DataGridViewRow Row in grdMain.Rows)
            {
                List<GanjoorVerse> verses = _db.GetVerses((int)Row.Cells[ClmnID].Value);
                try
                {
                    var ravi = RhymeFinder.FindRhyme(verses, false);
                    if(!string.IsNullOrEmpty(ravi.Rhyme))
                    {
                        Row.Cells[ClmnRAVI].Value = ravi.Rhyme;
                        Row.Cells[ClmnRAVIAX].Value = Reverse(ravi.Rhyme);
                        Application.DoEvents();
                    }
                }
                catch
                {
                    MessageBox.Show(Row.Cells[ClmnTitle].Value.ToString());
                }
                
            }
            Enabled = true;
        }

        private static int CompareGridRowsByRavi(DataGridViewRow Row1, DataGridViewRow Row2)
        {
            return Row1.Cells[ClmnRAVIAX].Value.ToString().CompareTo(Row2.Cells[ClmnRAVIAX].Value.ToString());
        }

        private void btnSortOnRavi_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> lstRows = new List<DataGridViewRow>();
            foreach (DataGridViewRow Row in grdMain.Rows)
                lstRows.Add(Row);

            lstRows.Sort(CompareGridRowsByRavi);

            grdMain.Rows.Clear();

            grdMain.Rows.AddRange(lstRows.ToArray());
        }

        public int SelectedPoemId { get; set; }

        private void grdMain_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedPoemId = Convert.ToInt32(grdMain.Rows[e.RowIndex].Cells[ClmnID].Value);
            DialogResult = DialogResult.OK;
        }
    }
}
