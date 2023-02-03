using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
            foreach (GanjoorPoet Poet in Poets)
            {
                int RowIndex  = grdConflictingPoets.Rows.Add();
                grdConflictingPoets.Rows[RowIndex].Tag = Poet;
                grdConflictingPoets.Rows[RowIndex].Cells[0].Value = Poet._Name;
                grdConflictingPoets.Rows[RowIndex].Cells[1].Value = true;
            }
        }

        public GanjoorPoet[] DeleteList
        {
            get
            {
                List<GanjoorPoet> lstDelete = new List<GanjoorPoet>();

                foreach (DataGridViewRow Row in grdConflictingPoets.Rows)
                    if(Convert.ToBoolean(Row.Cells[1].Value))
                    if (Row.Tag != null && Row.Tag is GanjoorPoet)
                        lstDelete.Add(Row.Tag as GanjoorPoet);
                return lstDelete.ToArray();
            }
        }
    }
}
