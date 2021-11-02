namespace ganjoor
{
    partial class ReOrderCat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReOrderCat));
            this.grdMain = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stsBar = new System.Windows.Forms.StatusStrip();
            this.lblPoemCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.llblSelectionCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlbrMain = new System.Windows.Forms.ToolStrip();
            this.btnMoveFirst = new System.Windows.Forms.ToolStripButton();
            this.btnMoveLast = new System.Windows.Forms.ToolStripButton();
            this.btnMoveUp = new System.Windows.Forms.ToolStripButton();
            this.btnMoveDown = new System.Windows.Forms.ToolStripButton();
            this.btnSaveOrder = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMoveToCat = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGroupNaming = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFillRhymes = new System.Windows.Forms.ToolStripButton();
            this.btnSortOnRavi = new System.Windows.Forms.ToolStripButton();
            this.btnFirstNoRavi = new System.Windows.Forms.ToolStripButton();
            this.btnFixFirstVerse = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            this.stsBar.SuspendLayout();
            this.tlbrMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdMain
            // 
            this.grdMain.AllowUserToAddRows = false;
            this.grdMain.AllowUserToDeleteRows = false;
            this.grdMain.AllowUserToResizeRows = false;
            this.grdMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column6,
            this.Column5});
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.Location = new System.Drawing.Point(0, 38);
            this.grdMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grdMain.Name = "grdMain";
            this.grdMain.ReadOnly = true;
            this.grdMain.RowHeadersWidth = 62;
            this.grdMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdMain.Size = new System.Drawing.Size(1384, 612);
            this.grdMain.TabIndex = 0;
            this.grdMain.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdMain_CellContentDoubleClick);
            this.grdMain.SelectionChanged += new System.EventHandler(this.grdMain_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1.HeaderText = "عنوان";
            this.Column1.MinimumWidth = 8;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.FillWeight = 50F;
            this.Column2.HeaderText = "مصرع اول";
            this.Column2.MinimumWidth = 8;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.FillWeight = 50F;
            this.Column3.HeaderText = "مصرع دوم";
            this.Column3.MinimumWidth = 8;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column4.HeaderText = "حروف قافیه";
            this.Column4.MinimumWidth = 8;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 80;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "عکس قافیه";
            this.Column6.MinimumWidth = 8;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Visible = false;
            this.Column6.Width = 150;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "ID";
            this.Column5.MinimumWidth = 8;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Visible = false;
            this.Column5.Width = 150;
            // 
            // stsBar
            // 
            this.stsBar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stsBar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.stsBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblPoemCount,
            this.llblSelectionCount});
            this.stsBar.Location = new System.Drawing.Point(0, 650);
            this.stsBar.Name = "stsBar";
            this.stsBar.Padding = new System.Windows.Forms.Padding(21, 0, 2, 0);
            this.stsBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.stsBar.Size = new System.Drawing.Size(1384, 32);
            this.stsBar.TabIndex = 1;
            this.stsBar.Text = "نوار وضعیت";
            // 
            // lblPoemCount
            // 
            this.lblPoemCount.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblPoemCount.Name = "lblPoemCount";
            this.lblPoemCount.Size = new System.Drawing.Size(60, 25);
            this.lblPoemCount.Text = "0 شعر";
            // 
            // llblSelectionCount
            // 
            this.llblSelectionCount.Name = "llblSelectionCount";
            this.llblSelectionCount.Size = new System.Drawing.Size(153, 25);
            this.llblSelectionCount.Text = "0 عنوان انتخاب شده";
            // 
            // tlbrMain
            // 
            this.tlbrMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbrMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlbrMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tlbrMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMoveFirst,
            this.btnMoveLast,
            this.btnMoveUp,
            this.btnMoveDown,
            this.btnSaveOrder,
            this.toolStripSeparator1,
            this.btnMoveToCat,
            this.toolStripSeparator2,
            this.btnGroupNaming,
            this.toolStripSeparator3,
            this.btnFillRhymes,
            this.btnSortOnRavi,
            this.btnFirstNoRavi,
            this.btnFixFirstVerse});
            this.tlbrMain.Location = new System.Drawing.Point(0, 0);
            this.tlbrMain.Name = "tlbrMain";
            this.tlbrMain.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.tlbrMain.Size = new System.Drawing.Size(1384, 38);
            this.tlbrMain.TabIndex = 2;
            this.tlbrMain.Text = "نوار ابزار";
            // 
            // btnMoveFirst
            // 
            this.btnMoveFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveFirst.Image")));
            this.btnMoveFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveFirst.Name = "btnMoveFirst";
            this.btnMoveFirst.Size = new System.Drawing.Size(34, 33);
            this.btnMoveFirst.Text = "انتقال به ابتدای فهرست";
            this.btnMoveFirst.Click += new System.EventHandler(this.btnMoveFirst_Click);
            // 
            // btnMoveLast
            // 
            this.btnMoveLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveLast.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveLast.Image")));
            this.btnMoveLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveLast.Name = "btnMoveLast";
            this.btnMoveLast.Size = new System.Drawing.Size(34, 33);
            this.btnMoveLast.Text = "انتقال به انتهای فهرست";
            this.btnMoveLast.Click += new System.EventHandler(this.btnMoveLast_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveUp.Image")));
            this.btnMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(34, 33);
            this.btnMoveUp.Text = "یک ردیف بالاتر";
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveDown.Image")));
            this.btnMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(34, 33);
            this.btnMoveDown.Text = "یک ردیف پایین‌تر";
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnSaveOrder
            // 
            this.btnSaveOrder.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnSaveOrder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSaveOrder.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveOrder.Image")));
            this.btnSaveOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveOrder.Name = "btnSaveOrder";
            this.btnSaveOrder.Size = new System.Drawing.Size(132, 33);
            this.btnSaveOrder.Text = "ذخیرهٔ این ترتیب";
            this.btnSaveOrder.Click += new System.EventHandler(this.btnSaveOrder_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // btnMoveToCat
            // 
            this.btnMoveToCat.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnMoveToCat.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveToCat.Image")));
            this.btnMoveToCat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveToCat.Name = "btnMoveToCat";
            this.btnMoveToCat.Size = new System.Drawing.Size(159, 33);
            this.btnMoveToCat.Text = "انتقال به بخش دیگر";
            this.btnMoveToCat.Click += new System.EventHandler(this.btnMoveToCat_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
            // 
            // btnGroupNaming
            // 
            this.btnGroupNaming.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnGroupNaming.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGroupNaming.Name = "btnGroupNaming";
            this.btnGroupNaming.Size = new System.Drawing.Size(141, 33);
            this.btnGroupNaming.Text = "نامگذاری گروهی";
            this.btnGroupNaming.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnGroupNaming.Click += new System.EventHandler(this.btnGroupNaming_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 38);
            // 
            // btnFillRhymes
            // 
            this.btnFillRhymes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnFillRhymes.Image = ((System.Drawing.Image)(resources.GetObject("btnFillRhymes.Image")));
            this.btnFillRhymes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFillRhymes.Name = "btnFillRhymes";
            this.btnFillRhymes.Size = new System.Drawing.Size(165, 33);
            this.btnFillRhymes.Text = "محاسبهٔ حروف قافیه";
            this.btnFillRhymes.Click += new System.EventHandler(this.btnFillRhymes_Click);
            // 
            // btnSortOnRavi
            // 
            this.btnSortOnRavi.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSortOnRavi.Image = ((System.Drawing.Image)(resources.GetObject("btnSortOnRavi.Image")));
            this.btnSortOnRavi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSortOnRavi.Name = "btnSortOnRavi";
            this.btnSortOnRavi.Size = new System.Drawing.Size(214, 33);
            this.btnSortOnRavi.Text = "مرتب‌سازی بر اساس قافیه";
            this.btnSortOnRavi.Click += new System.EventHandler(this.btnSortOnRavi_Click);
            // 
            // btnFirstNoRavi
            // 
            this.btnFirstNoRavi.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnFirstNoRavi.Image = ((System.Drawing.Image)(resources.GetObject("btnFirstNoRavi.Image")));
            this.btnFirstNoRavi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFirstNoRavi.Name = "btnFirstNoRavi";
            this.btnFirstNoRavi.Size = new System.Drawing.Size(115, 33);
            this.btnFirstNoRavi.Text = "اولین بی‌قافیه";
            this.btnFirstNoRavi.Click += new System.EventHandler(this.btnFirstNoRavi_Click);
            // 
            // btnFixFirstVerse
            // 
            this.btnFixFirstVerse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnFixFirstVerse.Image = ((System.Drawing.Image)(resources.GetObject("btnFixFirstVerse.Image")));
            this.btnFixFirstVerse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFixFirstVerse.Name = "btnFixFirstVerse";
            this.btnFixFirstVerse.Size = new System.Drawing.Size(201, 33);
            this.btnFixFirstVerse.Text = "انتقال مصرع اول به عنوان";
            this.btnFixFirstVerse.Click += new System.EventHandler(this.btnFixFirstVerse_Click);
            // 
            // ReOrderCat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1384, 682);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.tlbrMain);
            this.Controls.Add(this.stsBar);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ReOrderCat";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "تغییر ترتیب اشعار بخش";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ReOrderCat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.stsBar.ResumeLayout(false);
            this.stsBar.PerformLayout();
            this.tlbrMain.ResumeLayout(false);
            this.tlbrMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdMain;
        private System.Windows.Forms.StatusStrip stsBar;
        private System.Windows.Forms.ToolStrip tlbrMain;
        private System.Windows.Forms.ToolStripStatusLabel lblPoemCount;
        private System.Windows.Forms.ToolStripStatusLabel llblSelectionCount;
        private System.Windows.Forms.ToolStripButton btnMoveFirst;
        private System.Windows.Forms.ToolStripButton btnMoveLast;
        private System.Windows.Forms.ToolStripButton btnMoveUp;
        private System.Windows.Forms.ToolStripButton btnMoveDown;
        private System.Windows.Forms.ToolStripButton btnSaveOrder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnMoveToCat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnGroupNaming;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnFillRhymes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.ToolStripButton btnSortOnRavi;
        private System.Windows.Forms.ToolStripButton btnFirstNoRavi;
        private System.Windows.Forms.ToolStripButton btnFixFirstVerse;
    }
}