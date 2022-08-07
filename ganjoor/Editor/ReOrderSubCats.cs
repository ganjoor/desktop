using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ganjoor.Properties;

namespace ganjoor
{
    public partial class ReOrderSubCats : Form
    {
        public ReOrderSubCats()
        {
            InitializeComponent();

            LoadGridData();
        }

        private DbBrowser _db;

        private List<int> _OriginalIdSet = new List<int>();

        private const int ClmnTitle = 0;
        private const int ClmnID = 1;


        private void LoadGridData()
        {
            if (_db == null)
                _db = new DbBrowser();

            _OriginalIdSet.Clear();
            grdMain.Rows.Clear();

            var Cats = _db.GetSubCategories(Settings.Default.LastCat);
            grdMain.SuspendLayout();
            foreach (var Cat in Cats)
            {
                var rowIndex = grdMain.Rows.Add();

                _OriginalIdSet.Add(Cat._ID);

                grdMain.Rows[rowIndex].Cells[ClmnID].Value = Cat._ID;

                grdMain.Rows[rowIndex].Cells[ClmnTitle].Value = Cat._Text;
            }
            grdMain.ResumeLayout();
            lblCatCount.Text = String.Format("{0} بخش", grdMain.RowCount);
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
            var lstSelected = new List<DataGridViewRow>();
            foreach (DataGridViewRow Row in grdMain.SelectedRows)
                lstSelected.Add(Row);
            foreach (var Row in lstSelected)
            {
                grdMain.Rows.Remove(Row);
            }
            lstSelected.Sort(CompareGridRows);
            grdMain.Rows.Insert(0, lstSelected.Count);
            grdMain.ClearSelection();
            for (var iRow = 0; iRow < lstSelected.Count; iRow++)
            {
                for (var iCell = 0; iCell < lstSelected[iRow].Cells.Count; iCell++)
                    grdMain.Rows[iRow].Cells[iCell].Value = lstSelected[iRow].Cells[iCell].Value;
                grdMain.Rows[iRow].Selected = true;
            }
        }

        private void btnMoveLast_Click(object sender, EventArgs e)
        {
            var lstSelected = new List<DataGridViewRow>();
            foreach (DataGridViewRow Row in grdMain.SelectedRows)
                lstSelected.Add(Row);
            foreach (var Row in lstSelected)
            {
                grdMain.Rows.Remove(Row);
            }
            lstSelected.Sort(CompareGridRows);
            var oldRowCount = grdMain.Rows.Count;
            grdMain.Rows.Insert(oldRowCount, lstSelected.Count);
            grdMain.ClearSelection();
            for (var iRow = 0; iRow < lstSelected.Count; iRow++)
            {
                for (var iCell = 0; iCell < lstSelected[iRow].Cells.Count; iCell++)
                    grdMain.Rows[iRow + oldRowCount].Cells[iCell].Value = lstSelected[iRow].Cells[iCell].Value;
                grdMain.Rows[iRow + oldRowCount].Selected = true;
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            var lstSelected = new List<DataGridViewRow>();
            foreach (DataGridViewRow Row in grdMain.SelectedRows)
                lstSelected.Add(Row);
            var lstNewSelection = new List<DataGridViewRow>();
            lstSelected.Sort(CompareGridRows);
            foreach (var Row in lstSelected)
            {
                var RowIndex = Row.Index;
                if (RowIndex > 0)
                {
                    RowIndex--;
                    grdMain.Rows.Insert(RowIndex, 1);
                    grdMain.Rows.Remove(Row);

                    for (var iCell = 0; iCell < Row.Cells.Count; iCell++)
                        grdMain.Rows[RowIndex].Cells[iCell].Value = Row.Cells[iCell].Value;
                }
                lstNewSelection.Add(grdMain.Rows[RowIndex]);
            }
            grdMain.ClearSelection();
            foreach (var Row in lstNewSelection)
                Row.Selected = true;
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            var lstSelected = new List<DataGridViewRow>();
            foreach (DataGridViewRow Row in grdMain.SelectedRows)
                lstSelected.Add(Row);
            var lstNewSelection = new List<DataGridViewRow>();
            lstSelected.Sort(CompareGridRowsReversed);
            foreach (var Row in lstSelected)
            {
                var RowIndex = Row.Index;
                if (RowIndex < grdMain.RowCount - 1)
                {
                    grdMain.Rows.Insert(RowIndex + 2, 1);
                    grdMain.Rows.Remove(Row);

                    RowIndex++;


                    for (var iCell = 0; iCell < Row.Cells.Count; iCell++)
                        grdMain.Rows[RowIndex].Cells[iCell].Value = Row.Cells[iCell].Value;
                    lstNewSelection.Add(grdMain.Rows[RowIndex]);
                }
                lstNewSelection.Add(grdMain.Rows[RowIndex]);
            }
            grdMain.ClearSelection();
            foreach (var Row in lstNewSelection)
                Row.Selected = true;
        }

        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
            _db.BeginBatchOperation();
            for (var iRow = 0; iRow < grdMain.RowCount; iRow++)
            {
                var CatID = Convert.ToInt32(grdMain.Rows[iRow].Cells[ClmnID].Value);
                _db.SetCatID(CatID, -_OriginalIdSet[iRow]);
                grdMain.Rows[iRow].Cells[ClmnID].Value = _OriginalIdSet[iRow];
            }
            _db.CommitBatchOperation();
            _db.BeginBatchOperation();
            foreach (DataGridViewRow Row in grdMain.Rows)
            {
                var CatID = -Convert.ToInt32(Row.Cells[ClmnID].Value);
                _db.SetCatID(CatID, -CatID);
            }
            _db.CommitBatchOperation();
        }

        private void btnMoveToCat_Click(object sender, EventArgs e)
        {
            var PoetId = _db.GetCategory(Settings.Default.LastCat)._PoetID;
            using var dlg = new CategorySelector(PoetId);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                var NewCatId = dlg.SelectedCatID;
                if (NewCatId == Settings.Default.LastCat)
                    MessageBox.Show("شما بخش جاری را انتخاب کرده‌اید!");
                else
                {
                    var cat = _db.GetCategory(NewCatId);
                    if (MessageBox.Show(String.Format("از انتقال {0} بخش انتخابی از بخش «{1}» به بخش «{2}» اطمینان دارید؟", grdMain.SelectedRows.Count, _db.GetCategory(Settings.Default.LastCat)._Text, cat._Text),
                            "تأییدیه", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                    _db.BeginBatchOperation();
                    foreach (DataGridViewRow Row in grdMain.SelectedRows)
                    {
                        var CatID = Convert.ToInt32(Row.Cells[ClmnID].Value);
                        _db.SetCatParentID(CatID, NewCatId);
                    }
                    _db.CommitBatchOperation();
                    LoadGridData();
                }
            }
        }





    }
}
