using System.ComponentModel;
using System.Windows.Forms;

namespace ganjoor
{
    partial class NarratedPoems
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.grdList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlbr = new System.Windows.Forms.ToolStrip();
            this.btnView = new System.Windows.Forms.ToolStripButton();
            this.sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.btnImport = new System.Windows.Forms.ToolStripButton();
            this.strip = new System.Windows.Forms.StatusStrip();
            this.lblCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSpring = new System.Windows.Forms.ToolStripStatusLabel();
            this.prgss = new System.Windows.Forms.ToolStripProgressBar();
            this.btnAllDownloadable = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.tlbr.SuspendLayout();
            this.strip.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.AllowUserToResizeRows = false;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column3,
            this.Column4});
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Location = new System.Drawing.Point(0, 25);
            this.grdList.MultiSelect = false;
            this.grdList.Name = "grdList";
            this.grdList.ReadOnly = true;
            this.grdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdList.Size = new System.Drawing.Size(873, 431);
            this.grdList.TabIndex = 1;
            this.grdList.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdList_CellContentDoubleClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "شاعر";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.FillWeight = 50F;
            this.Column3.HeaderText = "عنوان";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.FillWeight = 50F;
            this.Column4.HeaderText = "شرح خوانش";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // tlbr
            // 
            this.tlbr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbr.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlbr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnView,
            this.sep1,
            this.btnExport,
            this.btnImport,
            this.btnAllDownloadable});
            this.tlbr.Location = new System.Drawing.Point(0, 0);
            this.tlbr.Name = "tlbr";
            this.tlbr.Size = new System.Drawing.Size(873, 25);
            this.tlbr.TabIndex = 2;
            this.tlbr.Text = "toolStrip1";
            // 
            // btnView
            // 
            this.btnView.Image = global::ganjoor.Properties.Resources.search;
            this.btnView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(145, 22);
            this.btnView.Text = "نمایش شعر ردیف جاری";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // sep1
            // 
            this.sep1.Name = "sep1";
            this.sep1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExport
            // 
            this.btnExport.Image = global::ganjoor.Properties.Resources.folder_up;
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(150, 22);
            this.btnExport.Text = "پشتیبان‌گیری از خوانشها";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Image = global::ganjoor.Properties.Resources.folder_down;
            this.btnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(159, 22);
            this.btnImport.Text = "بازگشت پشتیبان خوانشها";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // strip
            // 
            this.strip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCount,
            this.lblSpring,
            this.prgss});
            this.strip.Location = new System.Drawing.Point(0, 456);
            this.strip.Name = "strip";
            this.strip.Size = new System.Drawing.Size(873, 22);
            this.strip.TabIndex = 3;
            this.strip.Text = "statusStrip1";
            // 
            // lblCount
            // 
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(14, 17);
            this.lblCount.Text = "0";
            // 
            // lblSpring
            // 
            this.lblSpring.Name = "lblSpring";
            this.lblSpring.Size = new System.Drawing.Size(742, 17);
            this.lblSpring.Spring = true;
            // 
            // prgss
            // 
            this.prgss.Name = "prgss";
            this.prgss.Size = new System.Drawing.Size(100, 16);
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
            // NarratedPoems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 478);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.tlbr);
            this.Controls.Add(this.strip);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Name = "NarratedPoems";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "همه خوانش‌ها";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.NarratedPoems_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.tlbr.ResumeLayout(false);
            this.tlbr.PerformLayout();
            this.strip.ResumeLayout(false);
            this.strip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView grdList;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private ToolStrip tlbr;
        private ToolStripButton btnView;
        private ToolStripSeparator sep1;
        private ToolStripButton btnExport;
        private ToolStripButton btnImport;
        private StatusStrip strip;
        private ToolStripProgressBar prgss;
        private ToolStripStatusLabel lblCount;
        private ToolStripStatusLabel lblSpring;
        private ToolStripButton btnAllDownloadable;
    }
}