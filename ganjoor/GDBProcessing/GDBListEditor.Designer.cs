namespace ganjoor
{
    partial class GDBListEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GDBListEditor));
            this.tlbr = new System.Windows.Forms.ToolStrip();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.pnlListInfo = new System.Windows.Forms.Panel();
            this.txtMoreInfoUrl = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.tlbrGDBs = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFromDb = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblRefUrl = new System.Windows.Forms.ToolStripLabel();
            this.txtRefUrl = new System.Windows.Forms.ToolStripTextBox();
            this.grd = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlbr.SuspendLayout();
            this.pnlListInfo.SuspendLayout();
            this.tlbrGDBs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
            this.SuspendLayout();
            // 
            // tlbr
            // 
            this.tlbr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbr.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlbr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen,
            this.btnSave});
            this.tlbr.Location = new System.Drawing.Point(0, 0);
            this.tlbr.Name = "tlbr";
            this.tlbr.Size = new System.Drawing.Size(847, 25);
            this.tlbr.TabIndex = 0;
            this.tlbr.Text = "نوار ابزار";
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(68, 22);
            this.btnOpen.Text = "بارگذاری ...";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 22);
            this.btnSave.Text = "ذخیره ...";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlListInfo
            // 
            this.pnlListInfo.Controls.Add(this.txtMoreInfoUrl);
            this.pnlListInfo.Controls.Add(this.label2);
            this.pnlListInfo.Controls.Add(this.txtDescription);
            this.pnlListInfo.Controls.Add(this.label1);
            this.pnlListInfo.Controls.Add(this.txtName);
            this.pnlListInfo.Controls.Add(this.lblName);
            this.pnlListInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlListInfo.Location = new System.Drawing.Point(0, 25);
            this.pnlListInfo.Name = "pnlListInfo";
            this.pnlListInfo.Size = new System.Drawing.Size(847, 138);
            this.pnlListInfo.TabIndex = 1;
            // 
            // txtMoreInfoUrl
            // 
            this.txtMoreInfoUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMoreInfoUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMoreInfoUrl.Location = new System.Drawing.Point(327, 99);
            this.txtMoreInfoUrl.Name = "txtMoreInfoUrl";
            this.txtMoreInfoUrl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtMoreInfoUrl.Size = new System.Drawing.Size(344, 21);
            this.txtMoreInfoUrl.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(677, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "توضیحات بیشتر (Url):";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Location = new System.Drawing.Point(92, 40);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(688, 53);
            this.txtDescription.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(786, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "شرح:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Location = new System.Drawing.Point(436, 13);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(344, 21);
            this.txtName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Location = new System.Drawing.Point(786, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(37, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "عنوان:";
            // 
            // tlbrGDBs
            // 
            this.tlbrGDBs.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbrGDBs.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlbrGDBs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.toolStripSeparator1,
            this.btnFromDb,
            this.toolStripSeparator2,
            this.lblRefUrl,
            this.txtRefUrl});
            this.tlbrGDBs.Location = new System.Drawing.Point(0, 163);
            this.tlbrGDBs.Name = "tlbrGDBs";
            this.tlbrGDBs.Size = new System.Drawing.Size(847, 25);
            this.tlbrGDBs.TabIndex = 2;
            this.tlbrGDBs.Text = "نوار ابزار مجموعه‌ها";
            // 
            // btnAdd
            // 
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(87, 22);
            this.btnAdd.Text = "افزودن مجموعه";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnFromDb
            // 
            this.btnFromDb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnFromDb.Image = ((System.Drawing.Image)(resources.GetObject("btnFromDb.Image")));
            this.btnFromDb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFromDb.Name = "btnFromDb";
            this.btnFromDb.Size = new System.Drawing.Size(334, 22);
            this.btnFromDb.Text = "ایجاد مجموعه‌‎های متناظر با و افزودن مشخصات از شاعران موجود";
            this.btnFromDb.Click += new System.EventHandler(this.btnFromDb_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // lblRefUrl
            // 
            this.lblRefUrl.Name = "lblRefUrl";
            this.lblRefUrl.Size = new System.Drawing.Size(113, 22);
            this.lblRefUrl.Text = "نشانی مرجع دریافت:";
            // 
            // txtRefUrl
            // 
            this.txtRefUrl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefUrl.Name = "txtRefUrl";
            this.txtRefUrl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtRefUrl.Size = new System.Drawing.Size(300, 22);
            this.txtRefUrl.Text = "http://sourceforge.net/projects/ganjoor/files/gdb/";
            // 
            // grd
            // 
            this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10});
            this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd.Location = new System.Drawing.Point(0, 188);
            this.grd.Name = "grd";
            this.grd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grd.Size = new System.Drawing.Size(847, 257);
            this.grd.TabIndex = 3;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "شاعر/بخش";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "شناسهٔ شاعر";
            this.Column2.Name = "Column2";
            this.Column2.Width = 50;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "شناسهٔ بخش";
            this.Column3.Name = "Column3";
            this.Column3.Width = 50;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.HeaderText = "نشانی دریافت";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "نشانی توضیحات";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "پسوند";
            this.Column6.Name = "Column6";
            this.Column6.Width = 50;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "نشانی تصویر";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "اندازه";
            this.Column8.Name = "Column8";
            this.Column8.Width = 50;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "اولین شعر";
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            this.Column10.HeaderText = "تاریخ انتشار";
            this.Column10.Name = "Column10";
            // 
            // GDBListEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 445);
            this.Controls.Add(this.grd);
            this.Controls.Add(this.tlbrGDBs);
            this.Controls.Add(this.pnlListInfo);
            this.Controls.Add(this.tlbr);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Name = "GDBListEditor";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ویرایشگر فهرست مجموعه‌ها";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tlbr.ResumeLayout(false);
            this.tlbr.PerformLayout();
            this.pnlListInfo.ResumeLayout(false);
            this.pnlListInfo.PerformLayout();
            this.tlbrGDBs.ResumeLayout(false);
            this.tlbrGDBs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tlbr;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.Panel pnlListInfo;
        private System.Windows.Forms.ToolStrip tlbrGDBs;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnFromDb;
        private System.Windows.Forms.DataGridView grd;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtMoreInfoUrl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel lblRefUrl;
        private System.Windows.Forms.ToolStripTextBox txtRefUrl;
    }
}