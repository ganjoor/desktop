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
            this.cmbAudioInMiliseconds = new System.Windows.Forms.ComboBox();
            this.tlbr = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPlayPause = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.stsBar = new System.Windows.Forms.StatusStrip();
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
            this.trackBar.Location = new System.Drawing.Point(0, 0);
            this.trackBar.Name = "trackBar";
            this.trackBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBar.Size = new System.Drawing.Size(800, 45);
            this.trackBar.TabIndex = 15;
            this.trackBar.TickFrequency = 60000;
            // 
            // cmbAudioInMiliseconds
            // 
            this.cmbAudioInMiliseconds.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbAudioInMiliseconds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAudioInMiliseconds.FormattingEnabled = true;
            this.cmbAudioInMiliseconds.Location = new System.Drawing.Point(0, 45);
            this.cmbAudioInMiliseconds.Name = "cmbAudioInMiliseconds";
            this.cmbAudioInMiliseconds.Size = new System.Drawing.Size(800, 21);
            this.cmbAudioInMiliseconds.TabIndex = 16;
            // 
            // tlbr
            // 
            this.tlbr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbr.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlbr.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlbr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.btnPlayPause,
            this.btnSave});
            this.tlbr.Location = new System.Drawing.Point(0, 66);
            this.tlbr.Name = "tlbr";
            this.tlbr.Size = new System.Drawing.Size(800, 53);
            this.tlbr.TabIndex = 17;
            this.tlbr.Text = "نوار ابزار انتخاب";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 53);
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.Image = global::ganjoor.Properties.Resources.play;
            this.btnPlayPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(61, 50);
            this.btnPlayPause.Text = "پخش صدا";
            this.btnPlayPause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPlayPause.ToolTipText = "پخش صدا (Ctrl+P)";
            this.btnPlayPause.Click += new System.EventHandler(this.btnPlayPause_Click);
            // 
            // btnSave
            // 
            this.btnSave.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnSave.Enabled = false;
            this.btnSave.Image = global::ganjoor.Properties.Resources.accept;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(40, 50);
            this.btnSave.Text = "ذخیره";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // timer
            // 
            this.timer.Interval = 500;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // stsBar
            // 
            this.stsBar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stsBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTime});
            this.stsBar.Location = new System.Drawing.Point(0, 428);
            this.stsBar.Name = "stsBar";
            this.stsBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.stsBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.stsBar.Size = new System.Drawing.Size(800, 22);
            this.stsBar.SizingGrip = false;
            this.stsBar.TabIndex = 18;
            // 
            // lblTime
            // 
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(57, 17);
            this.lblTime.Text = "00:00:00";
            // 
            // SyncEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.stsBar);
            this.Controls.Add(this.tlbr);
            this.Controls.Add(this.cmbAudioInMiliseconds);
            this.Controls.Add(this.trackBar);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Name = "SyncEditor";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ویرایشگر اطلاعات همگامسازی";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SyncEditor_FormClosing);
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
        private System.Windows.Forms.ComboBox cmbAudioInMiliseconds;
        private System.Windows.Forms.ToolStrip tlbr;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnPlayPause;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.StatusStrip stsBar;
        private System.Windows.Forms.ToolStripStatusLabel lblTime;
    }
}