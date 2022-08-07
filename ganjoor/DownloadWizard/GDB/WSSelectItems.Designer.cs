using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ganjoor.Properties;

namespace ganjoor
{
    partial class WSSelectItems
    {
        private DataGridView grdList;
        private Label lblDesc;

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            this.grdList = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewLinkColumn();
            this.Column3 = new DataGridViewLinkColumn();
            this.Column4 = new DataGridViewCheckBoxColumn();
            this.lblDesc = new Label();
            this.tlbr = new ToolStrip();
            this.btnSelAllWhites = new ToolStripButton();
            this.btnSelNone = new ToolStripButton();
            ((ISupportInitialize)(this.grdList)).BeginInit();
            this.tlbr.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.grdList.Dock = DockStyle.Fill;
            this.grdList.Location = new Point(0, 91);
            this.grdList.Name = "grdList";
            this.grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grdList.Size = new Size(585, 274);
            this.grdList.TabIndex = 2;
            this.grdList.CellContentClick += new DataGridViewCellEventHandler(this.grdList_CellContentClick);
            this.grdList.CellValueChanged += new DataGridViewCellEventHandler(this.grdList_CellValueChanged);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "نام مجموعه";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column2.HeaderText = "نشانی";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = DataGridViewTriState.True;
            this.Column2.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column3.HeaderText = "توضیحات";
            this.Column3.Name = "Column3";
            this.Column3.Resizable = DataGridViewTriState.True;
            this.Column3.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "دریافت";
            this.Column4.Name = "Column4";
            // 
            // lblDesc
            // 
            this.lblDesc.BackColor = SystemColors.Window;
            this.lblDesc.BorderStyle = BorderStyle.FixedSingle;
            this.lblDesc.Dock = DockStyle.Top;
            this.lblDesc.Location = new Point(0, 0);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new Size(585, 66);
            this.lblDesc.TabIndex = 3;
            this.lblDesc.Text = "در حال دریافت اطلاعات ...";
            this.lblDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tlbr
            // 
            this.tlbr.Enabled = false;
            this.tlbr.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.tlbr.GripStyle = ToolStripGripStyle.Hidden;
            this.tlbr.Items.AddRange(new ToolStripItem[] {
            this.btnSelAllWhites,
            this.btnSelNone});
            this.tlbr.Location = new Point(0, 66);
            this.tlbr.Name = "tlbr";
            this.tlbr.Size = new Size(585, 25);
            this.tlbr.TabIndex = 4;
            this.tlbr.Text = "نوار ابزار انتخاب";
            // 
            // btnSelAllWhites
            // 
            this.btnSelAllWhites.Image = Resources.selall;
            this.btnSelAllWhites.ImageTransparentColor = Color.Magenta;
            this.btnSelAllWhites.Name = "btnSelAllWhites";
            this.btnSelAllWhites.Size = new Size(169, 22);
            this.btnSelAllWhites.Text = "علامتگذاریِ همهٔ آنچه نیست";
            this.btnSelAllWhites.Click += new EventHandler(this.btnSelAllWhites_Click);
            // 
            // btnSelNone
            // 
            this.btnSelNone.Image = Resources.selnone;
            this.btnSelNone.ImageTransparentColor = Color.Magenta;
            this.btnSelNone.Name = "btnSelNone";
            this.btnSelNone.Size = new Size(114, 22);
            this.btnSelNone.Text = "بازنشانی علامتها";
            this.btnSelNone.Click += new EventHandler(this.btnSelNone_Click);
            // 
            // WSSelectItems
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.tlbr);
            this.Controls.Add(this.lblDesc);
            this.Name = "WSSelectItems";
            ((ISupportInitialize)(this.grdList)).EndInit();
            this.tlbr.ResumeLayout(false);
            this.tlbr.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private DataGridViewTextBoxColumn Column1;
        private DataGridViewLinkColumn Column2;
        private DataGridViewLinkColumn Column3;
        private DataGridViewCheckBoxColumn Column4;
        private ToolStrip tlbr;
        private ToolStripButton btnSelAllWhites;
        private ToolStripButton btnSelNone;
    }
}
