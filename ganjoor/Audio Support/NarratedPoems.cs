using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class NarratedPoems : Form
    {
        public NarratedPoems()
        {
            InitializeComponent();
            _DbBrowser = new DbBrowser();
            SelectedPoem = null;
        }

        public GanjoorPoem SelectedPoem
        {
            get;
            private set;
        }


        /// <summary>
        /// نحوه اتصال و کار با دیتابیس
        /// </summary>
        private DbBrowser _DbBrowser;

        private const int GRDCOLUMN_IDX_POET = 0;
        private const int GRDCOLUMN_IDX_TITLE = 1;
        private const int GRDCOLUMN_IDX_DESC = 2;

        /// <summary>
        /// پر کردن فهرست
        /// </summary>
        private void FillGrid()
        {
            grdList.Rows.Clear();
            foreach (PoemAudio Audio in _DbBrowser.GetAllPoemAudioFiles())
            {
                AddAudioInfoToGrid(Audio);
            }
        }

        /// <summary>
        /// اضافه کردن اطلاعات یک ردیف به گرید
        /// </summary>
        /// <param name="Audio"></param>
        private int AddAudioInfoToGrid(PoemAudio Audio)
        {
            int nRowIdx = grdList.Rows.Add();
            grdList.Rows[nRowIdx].Cells[GRDCOLUMN_IDX_POET].Value = Audio.PoetName;
            grdList.Rows[nRowIdx].Cells[GRDCOLUMN_IDX_TITLE].Value = Audio.PoemTitle;
            grdList.Rows[nRowIdx].Cells[GRDCOLUMN_IDX_DESC].Value = Audio.Description;
            grdList.Rows[nRowIdx].Tag = Audio;
            return nRowIdx;
        }

        private void NarratedPoems_Load(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void grdList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                SelectedPoem = _DbBrowser.GetPoem((grdList.Rows[e.RowIndex].Tag as PoemAudio).PoemId);
                if (SelectedPoem != null)
                    DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (grdList.SelectedRows.Count == 1)
            {
                SelectedPoem = _DbBrowser.GetPoem((grdList.SelectedRows[0].Tag as PoemAudio).PoemId);
                if (SelectedPoem != null)
                    DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MessageBox.Show("ردیفی انتخاب نشده است.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            }
        }


    }
}
