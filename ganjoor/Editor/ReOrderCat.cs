using System;
using System.Collections.Generic;
using System.Linq;
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
                    string title = Row.Cells[ClmnTitle].Value.ToString().Trim();
                    string verse1 = Row.Cells[ClmnVerse1].Value.ToString().Trim();
                    if (title == verse1)
                        title = "";
                    if (title.StartsWith("شمارهٔ "))
                    {
                        if (title.IndexOf(" - ") != -1)
                        {
                            title = title.Substring(title.IndexOf(" - ") + " - ".Length);
                            newTitle = $"{newTitle} - {title}";
                        }
                    }
                    else
                    if(!string.IsNullOrEmpty(title))
                    {
                        newTitle = $"{newTitle} - {Row.Cells[ClmnTitle].Value}";
                    }
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

        private void btnFirstNoRavi_Click(object sender, EventArgs e)
        {
            int start = grdMain.SelectedRows.Count == 0 ? 0 : grdMain.SelectedRows[0].Index;
            for (int i = start; i < grdMain.Rows.Count; i++)
            {
                if(grdMain.Rows[i].Cells[ClmnRAVIAX].Value == null || grdMain.Rows[i].Cells[ClmnRAVIAX].Value.ToString() == "")
                {
                    grdMain.ClearSelection();
                    grdMain.Rows[i].Selected = true;
                    grdMain.FirstDisplayedCell = grdMain.Rows[i].Cells[0];
                    return;
                }
            }
            MessageBox.Show("نبود!");
        }

        private void btnFixFirstVerse_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از ادامهٔ عملیات اطمینان دارید؟",
                            "تأییدیه", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            if (MessageBox.Show("آیا تمایل دارید ادامه ندهید؟",
                           "تأییدیه", MessageBoxButtons.YesNo) == DialogResult.Yes)
                return;
            if (MessageBox.Show("آیا واقعاً از ادامهٔ عملیات اطمینان دارید؟",
                            "تأییدیه", MessageBoxButtons.YesNo) == DialogResult.No)
                return;


            foreach (DataGridViewRow row in grdMain.SelectedRows)
            {
                Application.DoEvents();
                int poemId = Convert.ToInt32(row.Cells[ClmnID].Value);
                var verses = _db.GetVerses(poemId);

                List<int> versesToDelete = new List<int>();
                foreach (GanjoorVerse Verse in verses)
                    versesToDelete.Add(Verse._Order);

                if (_db.DeleteVerses(poemId, versesToDelete))
                {
                    VersePosition pos = VersePosition.Right;
                    for (int i = 1; i < verses.Count; i++)
                    {
                        var v = _db.CreateNewVerse(poemId, i - 1, pos);
                        _db.SetVerseText(poemId, v._Order, verses[i]._Text);
                        pos = pos == VersePosition.Right ? VersePosition.Left : VersePosition.Right;
                    }

                    _db.SetPoemTitle(poemId, verses[0]._Text);
                }
            }

            LoadGridData();

        }

        private void btnNormalVersePositions_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از ادامهٔ عملیات اطمینان دارید؟",
                            "تأییدیه", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            if (MessageBox.Show("آیا تمایل دارید ادامه ندهید؟",
                           "تأییدیه", MessageBoxButtons.YesNo) == DialogResult.Yes)
                return;
            if (MessageBox.Show("آیا واقعاً از ادامهٔ عملیات اطمینان دارید؟",
                            "تأییدیه", MessageBoxButtons.YesNo) == DialogResult.No)
                return;


            foreach (DataGridViewRow row in grdMain.SelectedRows)
            {
                Application.DoEvents();
                int poemId = Convert.ToInt32(row.Cells[ClmnID].Value);
                var verses = _db.GetVerses(poemId);

                if(verses.Any(v => v._Position != VersePosition.Right && v._Position != VersePosition.Left))
                {
                    for (int i = 0; i < verses.Count; i++)
                    {
                        _db.SetVersePosition(poemId, verses[i]._Order, i % 2 == 0 ? VersePosition.Right : VersePosition.Left);
                    }
                }
            }

            LoadGridData();
        }

        private void btnNasafi_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از ادامهٔ عملیات اطمینان دارید؟",
                            "تأییدیه", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            if (MessageBox.Show("آیا تمایل دارید ادامه ندهید؟",
                           "تأییدیه", MessageBoxButtons.YesNo) == DialogResult.Yes)
                return;
            if (MessageBox.Show("آیا واقعاً از ادامهٔ عملیات اطمینان دارید؟",
                            "تأییدیه", MessageBoxButtons.YesNo) == DialogResult.No)
                return;


            List<GanjoorPoem> catPoems;
            bool breakHappened;
            do
            {
                breakHappened = false;
                catPoems = _db.GetPoems(Settings.Default.LastCat);
                foreach (var catPoem in catPoems)
                {
                    Application.DoEvents();
                    int poemId = catPoem._ID;
                    var verses = _db.GetVerses(poemId);
                    if (verses.Count == 2) continue;

                    var rhyme = RhymeFinder.FindRhyme(verses, false);
                    if (rhyme.FailVerseOrder != -1)
                    {
                        GanjoorPoem newPoem = _db.CreateNewPoem(catPoem._Title, Settings.Default.LastCat);
                        int nNewPoemId = newPoem._ID;
                        List<int> deletingVOrders = new List<int>();
                        for (int i = rhyme.FailVerseOrder; i < verses.Count; i++)
                        {
                            GanjoorVerse v = _db.CreateNewVerse(newPoem._ID, i - rhyme.FailVerseOrder, verses[i]._Position);
                            _db.SetVerseText(newPoem._ID, v._Order, verses[i]._Text);
                            deletingVOrders.Add(verses[i]._Order);
                        }

                        _db.DeleteVerses(poemId, deletingVOrders);




                        //Reorder poems so that the new one falls after current one

                        List<GanjoorPoem> poems = _db.GetPoems(Settings.Default.LastCat);
                        _db.BeginBatchOperation();
                        bool firstNextPoemMet = false;

                        for (int i = 0; i < poems.Count; i++)
                        {
                            GanjoorPoem poem = poems[i];
                            if (poem._ID > poemId && poem._ID != nNewPoemId)
                            {
                                if (!firstNextPoemMet)
                                {
                                    _db.SetPoemID(nNewPoemId, -poem._ID);
                                    firstNextPoemMet = true;
                                }
                                _db.SetPoemID(poem._ID, -poems[i + 1]._ID);
                            }
                        }
                        _db.CommitBatchOperation();

                        poems = _db.GetPoems(Settings.Default.LastCat);
                        _db.BeginBatchOperation();
                        foreach (GanjoorPoem poem in poems)
                        {
                            if (poem._ID < 0)
                            {
                                _db.SetPoemID(poem._ID, -poem._ID);
                            }
                        }
                        _db.CommitBatchOperation();

                        breakHappened = true;
                        break;
                    }
                }
            }
            while (breakHappened);
            

            LoadGridData();
        }
    }
}
