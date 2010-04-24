using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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

        private List<int> _IDs = new List<int>();

        private const int ClmnTitle = 0;
        private const int ClmnID = 1;


        private void LoadGridData()
        {
            if (_db == null)
                _db = new DbBrowser();

            _IDs.Clear();
            grdMain.Rows.Clear();

            List<GanjoorCat> Cats = _db.GetSubCategories(Settings.Default.LastCat);
            grdMain.SuspendLayout();
            foreach (GanjoorCat Cat in Cats)
            {
                int rowIndex = grdMain.Rows.Add();

                _IDs.Add(Cat._ID);

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
            for (int iRow = 0; iRow < lstSelected.Count; iRow++)
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
            grdMain.Rows.Insert(oldRowCount, lstSelected.Count);
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
                int CatID = Convert.ToInt32(grdMain.Rows[iRow].Cells[ClmnID].Value);
                _db.SetCatID(CatID, -_IDs[iRow]);
                grdMain.Rows[iRow].Cells[ClmnID].Value = _IDs[iRow];
            }
            _db.CommitBatchOperation();
            _db.BeginBatchOperation();
            foreach (DataGridViewRow Row in grdMain.Rows)
            {
                int CatID = -Convert.ToInt32(Row.Cells[ClmnID].Value);
                _db.SetCatID(CatID, -CatID);
            }
            _db.CommitBatchOperation();
        }



        

    }
}
