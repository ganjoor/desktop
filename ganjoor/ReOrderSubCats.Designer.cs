namespace ganjoor
{
    partial class ReOrderSubCats
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReOrderSubCats));
            this.llblSelectionCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.grdMain = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnMoveFirst = new System.Windows.Forms.ToolStripButton();
            this.lblCatCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnMoveDown = new System.Windows.Forms.ToolStripButton();
            this.btnMoveLast = new System.Windows.Forms.ToolStripButton();
            this.stsBar = new System.Windows.Forms.StatusStrip();
            this.btnMoveUp = new System.Windows.Forms.ToolStripButton();
            this.tlbrMain = new System.Windows.Forms.ToolStrip();
            this.btnSaveOrder = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            this.stsBar.SuspendLayout();
            this.tlbrMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // llblSelectionCount
            // 
            this.llblSelectionCount.Name = "llblSelectionCount";
            this.llblSelectionCount.Size = new System.Drawing.Size(99, 17);
            this.llblSelectionCount.Text = "0 عنوان انتخاب شده";
            // 
            // grdMain
            // 
            this.grdMain.AllowUserToAddRows = false;
            this.grdMain.AllowUserToDeleteRows = false;
            this.grdMain.AllowUserToResizeRows = false;
            this.grdMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column5});
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.Location = new System.Drawing.Point(0, 25);
            this.grdMain.Name = "grdMain";
            this.grdMain.ReadOnly = true;
            this.grdMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdMain.Size = new System.Drawing.Size(676, 265);
            this.grdMain.TabIndex = 3;
            this.grdMain.SelectionChanged += new System.EventHandler(this.grdMain_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "عنوان";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "ID";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Visible = false;
            // 
            // btnMoveFirst
            // 
            this.btnMoveFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnMoveFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveFirst.Image")));
            this.btnMoveFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveFirst.Name = "btnMoveFirst";
            this.btnMoveFirst.Size = new System.Drawing.Size(131, 22);
            this.btnMoveFirst.Text = "انتقال به ابتدای فهرست";
            this.btnMoveFirst.Click += new System.EventHandler(this.btnMoveFirst_Click);
            // 
            // lblCatCount
            // 
            this.lblCatCount.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblCatCount.Name = "lblCatCount";
            this.lblCatCount.Size = new System.Drawing.Size(44, 17);
            this.lblCatCount.Text = "0 بخش";
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveDown.Image")));
            this.btnMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(89, 22);
            this.btnMoveDown.Text = "یک ردیف پایین‌تر";
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnMoveLast
            // 
            this.btnMoveLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnMoveLast.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveLast.Image")));
            this.btnMoveLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveLast.Name = "btnMoveLast";
            this.btnMoveLast.Size = new System.Drawing.Size(132, 22);
            this.btnMoveLast.Text = "انتقال به انتهای فهرست";
            this.btnMoveLast.Click += new System.EventHandler(this.btnMoveLast_Click);
            // 
            // stsBar
            // 
            this.stsBar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stsBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCatCount,
            this.llblSelectionCount});
            this.stsBar.Location = new System.Drawing.Point(0, 290);
            this.stsBar.Name = "stsBar";
            this.stsBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.stsBar.Size = new System.Drawing.Size(676, 22);
            this.stsBar.TabIndex = 4;
            this.stsBar.Text = "نوار وضعیت";
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveUp.Image")));
            this.btnMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(80, 22);
            this.btnMoveUp.Text = "یک ردیف بالاتر";
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // tlbrMain
            // 
            this.tlbrMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbrMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlbrMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMoveFirst,
            this.btnMoveLast,
            this.btnMoveUp,
            this.btnMoveDown,
            this.btnSaveOrder});
            this.tlbrMain.Location = new System.Drawing.Point(0, 0);
            this.tlbrMain.Name = "tlbrMain";
            this.tlbrMain.Size = new System.Drawing.Size(676, 25);
            this.tlbrMain.TabIndex = 5;
            this.tlbrMain.Text = "نوار ابزار";
            // 
            // btnSaveOrder
            // 
            this.btnSaveOrder.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnSaveOrder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSaveOrder.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveOrder.Image")));
            this.btnSaveOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveOrder.Name = "btnSaveOrder";
            this.btnSaveOrder.Size = new System.Drawing.Size(89, 22);
            this.btnSaveOrder.Text = "ذخیرۀ این ترتیب";
            this.btnSaveOrder.Click += new System.EventHandler(this.btnSaveOrder_Click);
            // 
            // ReOrderSubCats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(676, 312);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.stsBar);
            this.Controls.Add(this.tlbrMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReOrderSubCats";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "تغییر ترتیب زیربخشها";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.stsBar.ResumeLayout(false);
            this.stsBar.PerformLayout();
            this.tlbrMain.ResumeLayout(false);
            this.tlbrMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripStatusLabel llblSelectionCount;
        private System.Windows.Forms.DataGridView grdMain;
        private System.Windows.Forms.ToolStripButton btnMoveFirst;
        private System.Windows.Forms.ToolStripStatusLabel lblCatCount;
        private System.Windows.Forms.ToolStripButton btnMoveDown;
        private System.Windows.Forms.ToolStripButton btnMoveLast;
        private System.Windows.Forms.StatusStrip stsBar;
        private System.Windows.Forms.ToolStripButton btnMoveUp;
        private System.Windows.Forms.ToolStrip tlbrMain;
        private System.Windows.Forms.ToolStripButton btnSaveOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}