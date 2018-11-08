namespace ganjoor
{
    partial class WSSelectSounds
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSelAllWhites = new System.Windows.Forms.ToolStripButton();
            this.lblDesc = new System.Windows.Forms.Label();
            this.btnSelNone = new System.Windows.Forms.ToolStripButton();
            this.tlbr = new System.Windows.Forms.ToolStrip();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnAllDownloadable = new System.Windows.Forms.ToolStripButton();
            this.sep = new System.Windows.Forms.ToolStripSeparator();
            this.btnMarkSelected = new System.Windows.Forms.ToolStripButton();
            this.btnUnMarkSelected = new System.Windows.Forms.ToolStripButton();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.clmnDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnDownload = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tlbr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
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
            // lblDesc
            // 
            this.lblDesc.BackColor = System.Drawing.SystemColors.Window;
            this.lblDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDesc.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDesc.Location = new System.Drawing.Point(0, 0);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(558, 66);
            this.lblDesc.TabIndex = 12;
            this.lblDesc.Text = "در حال دریافت اطلاعات ...";
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // tlbr
            // 
            this.tlbr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbr.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlbr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSelAllWhites,
            this.btnSelNone,
            this.btnRefresh,
            this.btnAllDownloadable,
            this.sep,
            this.btnMarkSelected,
            this.btnUnMarkSelected});
            this.tlbr.Location = new System.Drawing.Point(0, 66);
            this.tlbr.Name = "tlbr";
            this.tlbr.Size = new System.Drawing.Size(558, 25);
            this.tlbr.TabIndex = 13;
            this.tlbr.Text = "نوار ابزار انتخاب";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::ganjoor.Properties.Resources.repeat1;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(85, 22);
            this.btnRefresh.Text = "تلاش مجدد";
            this.btnRefresh.Visible = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnAllDownloadable
            // 
            this.btnAllDownloadable.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnAllDownloadable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAllDownloadable.Image = global::ganjoor.Properties.Resources.down;
            this.btnAllDownloadable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAllDownloadable.Name = "btnAllDownloadable";
            this.btnAllDownloadable.Size = new System.Drawing.Size(23, 22);
            this.btnAllDownloadable.Text = "همه خوانشهای قابل دریافت";
            this.btnAllDownloadable.Click += new System.EventHandler(this.btnAllDownloadable_Click);
            // 
            // sep
            // 
            this.sep.Name = "sep";
            this.sep.Size = new System.Drawing.Size(6, 25);
            // 
            // btnMarkSelected
            // 
            this.btnMarkSelected.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMarkSelected.Image = global::ganjoor.Properties.Resources.selall;
            this.btnMarkSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMarkSelected.Name = "btnMarkSelected";
            this.btnMarkSelected.Size = new System.Drawing.Size(23, 22);
            this.btnMarkSelected.Text = "علامتگذاری ردیفهای انتخاب شده";
            this.btnMarkSelected.Click += new System.EventHandler(this.btnMarkSelected_Click);
            // 
            // btnUnMarkSelected
            // 
            this.btnUnMarkSelected.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUnMarkSelected.Image = global::ganjoor.Properties.Resources.selnone;
            this.btnUnMarkSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnMarkSelected.Name = "btnUnMarkSelected";
            this.btnUnMarkSelected.Size = new System.Drawing.Size(23, 22);
            this.btnUnMarkSelected.Text = "بازنشانی علامت ردیفهای انتخاب شده";
            this.btnUnMarkSelected.Click += new System.EventHandler(this.btnUnMarkSelected_Click);
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmnDesc,
            this.clmnSize,
            this.clmnDownload});
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Location = new System.Drawing.Point(0, 91);
            this.grdList.Name = "grdList";
            this.grdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdList.Size = new System.Drawing.Size(558, 244);
            this.grdList.TabIndex = 11;
            this.grdList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdList_CellContentClick);
            this.grdList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grdList_CellFormatting);
            this.grdList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdList_CellValueChanged);
            // 
            // clmnDesc
            // 
            this.clmnDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmnDesc.FillWeight = 50F;
            this.clmnDesc.HeaderText = "عنوان";
            this.clmnDesc.Name = "clmnDesc";
            // 
            // clmnSize
            // 
            this.clmnSize.HeaderText = "اندازه";
            this.clmnSize.Name = "clmnSize";
            this.clmnSize.Width = 110;
            // 
            // clmnDownload
            // 
            this.clmnDownload.HeaderText = "دریافت";
            this.clmnDownload.Name = "clmnDownload";
            this.clmnDownload.Width = 50;
            // 
            // WSSelectSounds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.tlbr);
            this.Controls.Add(this.lblDesc);
            this.Name = "WSSelectSounds";
            this.Size = new System.Drawing.Size(558, 335);
            this.tlbr.ResumeLayout(false);
            this.tlbr.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton btnSelAllWhites;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.ToolStripButton btnSelNone;
        private System.Windows.Forms.ToolStrip tlbr;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnSize;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmnDownload;
        private System.Windows.Forms.ToolStripButton btnAllDownloadable;
        private System.Windows.Forms.ToolStripSeparator sep;
        private System.Windows.Forms.ToolStripButton btnMarkSelected;
        private System.Windows.Forms.ToolStripButton btnUnMarkSelected;
    }
}
