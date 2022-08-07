namespace ganjoor
{
    partial class WSSelectItems
    {
        private System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.Label lblDesc;

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lblDesc = new System.Windows.Forms.Label();
            this.tlbr = new System.Windows.Forms.ToolStrip();
            this.btnSelAllWhites = new System.Windows.Forms.ToolStripButton();
            this.btnSelNone = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.tlbr.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Location = new System.Drawing.Point(0, 91);
            this.grdList.Name = "grdList";
            this.grdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdList.Size = new System.Drawing.Size(585, 274);
            this.grdList.TabIndex = 2;
            this.grdList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdList_CellContentClick);
            this.grdList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdList_CellValueChanged);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "نام مجموعه";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column2.HeaderText = "نشانی";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column3.HeaderText = "توضیحات";
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "دریافت";
            this.Column4.Name = "Column4";
            // 
            // lblDesc
            // 
            this.lblDesc.BackColor = System.Drawing.SystemColors.Window;
            this.lblDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDesc.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDesc.Location = new System.Drawing.Point(0, 0);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(585, 66);
            this.lblDesc.TabIndex = 3;
            this.lblDesc.Text = "در حال دریافت اطلاعات ...";
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlbr
            // 
            this.tlbr.Enabled = false;
            this.tlbr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbr.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlbr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSelAllWhites,
            this.btnSelNone});
            this.tlbr.Location = new System.Drawing.Point(0, 66);
            this.tlbr.Name = "tlbr";
            this.tlbr.Size = new System.Drawing.Size(585, 25);
            this.tlbr.TabIndex = 4;
            this.tlbr.Text = "نوار ابزار انتخاب";
            // 
            // btnSelAllWhites
            // 
            this.btnSelAllWhites.Image = global::ganjoor.Properties.Resources.selall;
            this.btnSelAllWhites.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelAllWhites.Name = "btnSelAllWhites";
            this.btnSelAllWhites.Size = new System.Drawing.Size(169, 22);
            this.btnSelAllWhites.Text = "علامتگذاریِ همهٔ آنچه نیست";
            this.btnSelAllWhites.Click += new System.EventHandler(this.btnSelAllWhites_Click);
            // 
            // btnSelNone
            // 
            this.btnSelNone.Image = global::ganjoor.Properties.Resources.selnone;
            this.btnSelNone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelNone.Name = "btnSelNone";
            this.btnSelNone.Size = new System.Drawing.Size(114, 22);
            this.btnSelNone.Text = "بازنشانی علامتها";
            this.btnSelNone.Click += new System.EventHandler(this.btnSelNone_Click);
            // 
            // WSSelectItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.tlbr);
            this.Controls.Add(this.lblDesc);
            this.Name = "WSSelectItems";
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.tlbr.ResumeLayout(false);
            this.tlbr.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewLinkColumn Column2;
        private System.Windows.Forms.DataGridViewLinkColumn Column3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column4;
        private System.Windows.Forms.ToolStrip tlbr;
        private System.Windows.Forms.ToolStripButton btnSelAllWhites;
        private System.Windows.Forms.ToolStripButton btnSelNone;
    }
}
