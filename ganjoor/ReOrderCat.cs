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

    }
}
