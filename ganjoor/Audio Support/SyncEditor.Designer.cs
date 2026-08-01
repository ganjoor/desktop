namespace ganjoor.Audio_Support
{
    partial class SyncEditor
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
            this.components = new System.ComponentModel.Container();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.lvVerses = new System.Windows.Forms.ListView();
            this.colOrder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlbr = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPlayPause = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEarlier = new System.Windows.Forms.ToolStripButton();
            this.btnLater = new System.Windows.Forms.ToolStripButton();
            this.btnSetStart = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.stsBar = new System.Windows.Forms.StatusStrip();
            this.lblSelectedInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTime = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.tlbr.SuspendLayout();
            this.stsBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBar
            // 
            this.trackBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBar.Enabled = false;
            this.trackBar.Location = new System.Drawing.Point(0, 71);
            this.trackBar.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.trackBar.Name = "trackBar";
            this.trackBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBar.Size = new System.Drawing.Size(1600, 90);
            this.trackBar.TabIndex = 15;
            this.trackBar.TickFrequency = 60000;
            this.trackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // lvVerses
            // 
            this.lvVerses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colOrder,
            this.colTime,
            this.colText});
            this.lvVerses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvVerses.FullRowSelect = true;
            this.lvVerses.GridLines = true;
            this.lvVerses.HideSelection = false;
            this.lvVerses.Location = new System.Drawing.Point(0, 161);
            this.lvVerses.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.lvVerses.MultiSelect = false;
            this.lvVerses.Name = "lvVerses";
            this.lvVerses.Size = new System.Drawing.Size(1600, 656);
            this.lvVerses.TabIndex = 16;
            this.lvVerses.UseCompatibleStateImageBehavior = false;
            this.lvVerses.View = System.Windows.Forms.View.Details;
            this.lvVerses.SelectedIndexChanged += new System.EventHandler(this.lvVerses_SelectedIndexChanged);
            this.lvVerses.DoubleClick += new System.EventHandler(this.lvVerses_DoubleClick);
            // 
            // colOrder
            // 
            this.colOrder.Text = "ردیف";
            this.colOrder.Width = 45;
            // 
            // colTime
            // 
            this.colTime.Text = "زمان آغاز";
            this.colTime.Width = 100;
            // 
            // colText
            // 
            this.colText.Text = "متن مصرع";
            this.colText.Width = 620;
            // 
            // tlbr
            // 
            this.tlbr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbr.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlbr.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlbr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.btnPlayPause,
            this.toolStripSeparator2,
            this.btnEarlier,
            this.btnLater,
            this.btnSetStart,
            this.btnSave});
            this.tlbr.Location = new System.Drawing.Point(0, 0);
            this.tlbr.Name = "tlbr";
            this.tlbr.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.tlbr.Size = new System.Drawing.Size(1600, 71);
            this.tlbr.TabIndex = 17;
            this.tlbr.Text = "نوار ابزار ویرایش همگام‌سازی";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 71);
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.Image = global::ganjoor.Properties.Resources.play;
            this.btnPlayPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(116, 65);
            this.btnPlayPause.Text = "پخش صدا";
            this.btnPlayPause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPlayPause.ToolTipText = "پخش / توقف صدا (Ctrl+P)";
            this.btnPlayPause.Click += new System.EventHandler(this.btnPlayPause_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 71);
            // 
            // btnEarlier
            // 
            this.btnEarlier.Enabled = false;
            this.btnEarlier.Image = global::ganjoor.Properties.Resources.fast_forward;
            this.btnEarlier.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEarlier.Name = "btnEarlier";
            this.btnEarlier.Size = new System.Drawing.Size(64, 65);
            this.btnEarlier.Text = "زودتر";
            this.btnEarlier.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEarlier.ToolTipText = "۱۰۰ میلی‌ثانیه زودتر (Ctrl+چپ)";
            this.btnEarlier.Click += new System.EventHandler(this.btnEarlier_Click);
            // 
            // btnLater
            // 
            this.btnLater.Enabled = false;
            this.btnLater.Image = global::ganjoor.Properties.Resources.rewind;
            this.btnLater.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLater.Name = "btnLater";
            this.btnLater.Size = new System.Drawing.Size(62, 65);
            this.btnLater.Text = "دیرتر";
            this.btnLater.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLater.ToolTipText = "۱۰۰ میلی‌ثانیه دیرتر (Ctrl+راست)";
            this.btnLater.Click += new System.EventHandler(this.btnLater_Click);
            // 
            // btnSetStart
            // 
            this.btnSetStart.Enabled = false;
            this.btnSetStart.Image = global::ganjoor.Properties.Resources.application_edit;
            this.btnSetStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetStart.Name = "btnSetStart";
            this.btnSetStart.Size = new System.Drawing.Size(354, 65);
            this.btnSetStart.Text = "ثبت این لحظه به عنوان آغاز مصرع";
            this.btnSetStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSetStart.ToolTipText = "موقعیت جاری پخش را آغاز مصرع انتخاب‌شده قرار می‌دهد (Enter)";
            this.btnSetStart.Click += new System.EventHandler(this.btnSetStart_Click);
            // 
            // btnSave
            // 
            this.btnSave.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnSave.Enabled = false;
            this.btnSave.Image = global::ganjoor.Properties.Resources.accept;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(73, 65);
            this.btnSave.Text = "ذخیره";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // timer
            // 
            this.timer.Interval = 200;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // stsBar
            // 
            this.stsBar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stsBar.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.stsBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblSelectedInfo,
            this.lblTime});
            this.stsBar.Location = new System.Drawing.Point(0, 817);
            this.stsBar.Name = "stsBar";
            this.stsBar.Padding = new System.Windows.Forms.Padding(2, 0, 28, 0);
            this.stsBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.stsBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.stsBar.Size = new System.Drawing.Size(1600, 39);
            this.stsBar.SizingGrip = false;
            this.stsBar.TabIndex = 18;
            // 
            // lblSelectedInfo
            // 
            this.lblSelectedInfo.Name = "lblSelectedInfo";
            this.lblSelectedInfo.Size = new System.Drawing.Size(1463, 29);
            this.lblSelectedInfo.Spring = true;
            this.lblSelectedInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTime
            // 
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(107, 29);
            this.lblTime.Text = "00:00:00";
            // 
            // SyncEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1600, 856);
            this.Controls.Add(this.lvVerses);
            this.Controls.Add(this.stsBar);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.tlbr);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "SyncEditor";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ویرایشگر اطلاعات همگامسازی";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SyncEditor_FormClosing);
            this.Load += new System.EventHandler(this.SyncEditor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SyncEditor_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.tlbr.ResumeLayout(false);
            this.tlbr.PerformLayout();
            this.stsBar.ResumeLayout(false);
            this.stsBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.ListView lvVerses;
        private System.Windows.Forms.ColumnHeader colOrder;
        private System.Windows.Forms.ColumnHeader colTime;
        private System.Windows.Forms.ColumnHeader colText;
        private System.Windows.Forms.ToolStrip tlbr;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnPlayPause;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnEarlier;
        private System.Windows.Forms.ToolStripButton btnLater;
        private System.Windows.Forms.ToolStripButton btnSetStart;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.StatusStrip stsBar;
        private System.Windows.Forms.ToolStripStatusLabel lblSelectedInfo;
        private System.Windows.Forms.ToolStripStatusLabel lblTime;
    }
}
