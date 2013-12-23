namespace ganjoor
{
    partial class AudioFiles
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AudioFiles));
            this.tlbr = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblDesc = new System.Windows.Forms.Label();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnPlayStop = new System.Windows.Forms.ToolStripButton();
            this.btnDel = new System.Windows.Forms.ToolStripButton();
            this.tlbr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // tlbr
            // 
            this.tlbr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbr.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlbr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnPlayStop,
            this.toolStripSeparator1,
            this.btnDel});
            this.tlbr.Location = new System.Drawing.Point(0, 66);
            this.tlbr.Name = "tlbr";
            this.tlbr.Size = new System.Drawing.Size(608, 25);
            this.tlbr.TabIndex = 7;
            this.tlbr.Text = "نوار ابزار انتخاب";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // lblDesc
            // 
            this.lblDesc.BackColor = System.Drawing.SystemColors.Window;
            this.lblDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDesc.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDesc.Location = new System.Drawing.Point(0, 0);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(608, 66);
            this.lblDesc.TabIndex = 6;
            this.lblDesc.Text = resources.GetString("lblDesc.Text");
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column4});
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Location = new System.Drawing.Point(0, 91);
            this.grdList.Name = "grdList";
            this.grdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdList.Size = new System.Drawing.Size(608, 277);
            this.grdList.TabIndex = 5;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "مسیر فایل";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "همزمان شده";
            this.Column4.Name = "Column4";
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::ganjoor.Properties.Resources.sound_on;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(113, 22);
            this.btnAdd.Text = "انتخاب فایل جدید";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnPlayStop
            // 
            this.btnPlayStop.Image = global::ganjoor.Properties.Resources.play16;
            this.btnPlayStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlayStop.Name = "btnPlayStop";
            this.btnPlayStop.Size = new System.Drawing.Size(54, 22);
            this.btnPlayStop.Text = "پخش";
            this.btnPlayStop.Click += new System.EventHandler(this.btnPlayStop_Click);
            // 
            // btnDel
            // 
            this.btnDel.Image = global::ganjoor.Properties.Resources.sound_off;
            this.btnDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(52, 22);
            this.btnDel.Text = "حذف";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // AudioFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(608, 368);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.tlbr);
            this.Controls.Add(this.lblDesc);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AudioFiles";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "بازخوانیهای شعر جاری";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AudioFiles_FormClosing);
            this.Load += new System.EventHandler(this.AudioFiles_Load);
            this.tlbr.ResumeLayout(false);
            this.tlbr.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tlbr;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnDel;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.ToolStripButton btnPlayStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column4;
    }
}