﻿namespace ganjoor
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
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPlayStop = new System.Windows.Forms.ToolStripButton();
            this.btnSync = new System.Windows.Forms.ToolStripButton();
            this.btnMoveToTop = new System.Windows.Forms.ToolStripButton();
            this.btnDel = new System.Windows.Forms.ToolStripButton();
            this.btnFixExport = new System.Windows.Forms.ToolStripButton();
            this.btnImport = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDownload = new System.Windows.Forms.ToolStripButton();
            this.lblDesc = new System.Windows.Forms.Label();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.clmnDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnSynced = new System.Windows.Forms.DataGridViewCheckBoxColumn();
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
            this.sep1,
            this.btnPlayStop,
            this.btnSync,
            this.btnMoveToTop,
            this.btnDel,
            this.btnFixExport,
            this.btnImport,
            this.btnExport,
            this.sep2,
            this.btnDownload});
            this.tlbr.Location = new System.Drawing.Point(0, 66);
            this.tlbr.Name = "tlbr";
            this.tlbr.Size = new System.Drawing.Size(697, 25);
            this.tlbr.TabIndex = 7;
            this.tlbr.Text = "نوار ابزار انتخاب";
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::ganjoor.Properties.Resources.sound_on;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 22);
            this.btnAdd.Text = "انتخاب فایل ...";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // sep1
            // 
            this.sep1.Name = "sep1";
            this.sep1.Size = new System.Drawing.Size(6, 25);
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
            // btnSync
            // 
            this.btnSync.Image = global::ganjoor.Properties.Resources.clock;
            this.btnSync.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(93, 22);
            this.btnSync.Text = " همگام‌سازی";
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // btnMoveToTop
            // 
            this.btnMoveToTop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveToTop.Image = global::ganjoor.Properties.Resources.up;
            this.btnMoveToTop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveToTop.Name = "btnMoveToTop";
            this.btnMoveToTop.Size = new System.Drawing.Size(23, 22);
            this.btnMoveToTop.Text = "انتقال فایل جاری به بالای فهرست";
            this.btnMoveToTop.Click += new System.EventHandler(this.btnMoveToTop_Click);
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
            // btnFixExport
            // 
            this.btnFixExport.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnFixExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFixExport.Image = global::ganjoor.Properties.Resources.gear16;
            this.btnFixExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFixExport.Name = "btnFixExport";
            this.btnFixExport.Size = new System.Drawing.Size(23, 22);
            this.btnFixExport.Text = "رفع اشکال همگامسازیهای قدیمی با تقسیم شروع آنها بر ۲";
            this.btnFixExport.Click += new System.EventHandler(this.btnFixExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImport.Image = global::ganjoor.Properties.Resources.folder_down;
            this.btnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(23, 22);
            this.btnImport.Text = "بارگذاری اطلاعات همگام‌سازی از فایل XML";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExport.Image = global::ganjoor.Properties.Resources.folder_up;
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(23, 22);
            this.btnExport.Text = "ذخیرۀ اطلاعات همگام‌سازی در فایل XML";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // sep2
            // 
            this.sep2.Name = "sep2";
            this.sep2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDownload
            // 
            this.btnDownload.Image = global::ganjoor.Properties.Resources.down;
            this.btnDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(60, 22);
            this.btnDownload.Text = "دریافت";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // lblDesc
            // 
            this.lblDesc.BackColor = System.Drawing.SystemColors.Window;
            this.lblDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDesc.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDesc.Location = new System.Drawing.Point(0, 0);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(697, 66);
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
            this.clmnDesc,
            this.clmnPath,
            this.clmnSynced});
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Location = new System.Drawing.Point(0, 91);
            this.grdList.MultiSelect = false;
            this.grdList.Name = "grdList";
            this.grdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdList.Size = new System.Drawing.Size(697, 314);
            this.grdList.TabIndex = 5;
            // 
            // clmnDesc
            // 
            this.clmnDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmnDesc.FillWeight = 50F;
            this.clmnDesc.HeaderText = "شرح";
            this.clmnDesc.Name = "clmnDesc";
            this.clmnDesc.ReadOnly = true;
            // 
            // clmnPath
            // 
            this.clmnPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmnPath.FillWeight = 50F;
            this.clmnPath.HeaderText = "مسیر فایل";
            this.clmnPath.Name = "clmnPath";
            this.clmnPath.ReadOnly = true;
            // 
            // clmnSynced
            // 
            this.clmnSynced.HeaderText = "همگام";
            this.clmnSynced.Name = "clmnSynced";
            this.clmnSynced.ReadOnly = true;
            this.clmnSynced.Width = 50;
            // 
            // AudioFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(697, 405);
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
            this.Text = "خوانشهای شعر جاری";
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
        private System.Windows.Forms.ToolStripSeparator sep1;
        private System.Windows.Forms.ToolStripButton btnMoveToTop;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnPath;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmnSynced;
        private System.Windows.Forms.ToolStripButton btnSync;
        private System.Windows.Forms.ToolStripButton btnExport;
        private System.Windows.Forms.ToolStripButton btnImport;
        private System.Windows.Forms.ToolStripSeparator sep2;
        private System.Windows.Forms.ToolStripButton btnDownload;
        private System.Windows.Forms.ToolStripButton btnFixExport;
    }
}