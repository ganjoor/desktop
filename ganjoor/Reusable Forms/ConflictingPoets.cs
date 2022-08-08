using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class ConflictingPoets : Form
    {
        public ConflictingPoets()
        {
            InitializeComponent();
        }
        public ConflictingPoets(GanjoorPoet[] Poets) : this()
        {
            foreach (var Poet in Poets)
            {
                var RowIndex = grdConflictingPoets.Rows.Add();
                grdConflictingPoets.Rows[RowIndex].Tag = Poet;
                grdConflictingPoets.Rows[RowIndex].Cells[0].Value = Poet._Name;
                grdConflictingPoets.Rows[RowIndex].Cells[1].Value = true;
            }
        }

        public GanjoorPoet[] DeleteList
        {
            get
            {
                var lstDelete = new List<GanjoorPoet>();

                foreach (DataGridViewRow Row in grdConflictingPoets.Rows)
                    if (Convert.ToBoolean(Row.Cells[1].Value))
                        if (Row.Tag is GanjoorPoet tag)
                            lstDelete.Add(tag);
                return lstDelete.ToArray();
            }
        }
    }
}
