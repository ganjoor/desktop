using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class ConflictingCats : Form
    {
        public ConflictingCats()
        {
            InitializeComponent();
        }
        public ConflictingCats(GanjoorCat[] Cats)
            : this()
        {
            foreach (GanjoorCat Cat in Cats)
            {
                int RowIndex  = grdConflictingCats.Rows.Add();
                grdConflictingCats.Rows[RowIndex].Tag = Cat;
                grdConflictingCats.Rows[RowIndex].Cells[0].Value = Cat._Text;
                grdConflictingCats.Rows[RowIndex].Cells[1].Value = true;
            }
        }

        public GanjoorCat[] DeleteList
        {
            get
            {
                List<GanjoorCat> lstDelete = new List<GanjoorCat>();

                foreach (DataGridViewRow Row in grdConflictingCats.Rows)
                    if(Convert.ToBoolean(Row.Cells[1].Value))
                        if (Row.Tag != null && Row.Tag is GanjoorCat)
                        lstDelete.Add(Row.Tag as GanjoorCat);
                return lstDelete.ToArray();
            }
        }

    }
}
