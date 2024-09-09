namespace ganjoor
{
    partial class SyncPoemAudio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncPoemAudio));
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbr = new System.Windows.Forms.ToolStrip();
            this.btnNextVerse = new System.Windows.Forms.ToolStripButton();
            this.btnPreVerse = new System.Windows.Forms.ToolStripButton();
            this.btnSearchText = new System.Windows.Forms.ToolStripButton();
            this.btnTrack = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPlayPause = new System.Windows.Forms.ToolStripButton();
            this.btnStartFromHere = new System.Windows.Forms.ToolStripButton();
            this.btnStopHere = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnTest = new System.Windows.Forms.ToolStripButton();
            this.btnReset = new System.Windows.Forms.ToolStripButton();
            this.chkWaveForm = new System.Windows.Forms.ToolStripButton();
            this.chkShowNextVerse = new System.Windows.Forms.ToolStripButton();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblVerse = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.lblNextVerse = new System.Windows.Forms.Label();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.stsBar = new System.Windows.Forms.StatusStrip();
            this.lblTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnConfig = new System.Windows.Forms.ToolStripSplitButton();
            this.chkWordMode = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSrtOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.waveViewer = new ganjoor.CustomWaveViewer();
            this.tlbr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.stsBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 53);
            // 
            // tlbr
            // 
            this.tlbr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbr.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlbr.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlbr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.btnNextVerse,
            this.btnPreVerse,
            this.btnSearchText,
            this.btnTrack,
            this.toolStripSeparator2,
            this.btnPlayPause,
            this.btnStartFromHere,
            this.btnStopHere,
            this.toolStripSeparator3,
            this.btnSave,
            this.btnTest,
            this.btnReset,
            this.chkWaveForm,
            this.chkShowNextVerse});
            this.tlbr.Location = new System.Drawing.Point(0, 66);
            this.tlbr.Name = "tlbr";
            this.tlbr.Size = new System.Drawing.Size(764, 53);
            this.tlbr.TabIndex = 10;
            this.tlbr.Text = "نوار ابزار انتخاب";
            // 
            // btnNextVerse
            // 
            this.btnNextVerse.Image = ((System.Drawing.Image)(resources.GetObject("btnNextVerse.Image")));
            this.btnNextVerse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNextVerse.Name = "btnNextVerse";
            this.btnNextVerse.Size = new System.Drawing.Size(58, 50);
            this.btnNextVerse.Text = "مصرع بعد";
            this.btnNextVerse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNextVerse.ToolTipText = "مصرع بعد (Space)";
            this.btnNextVerse.Click += new System.EventHandler(this.btnNextVerse_Click);
            // 
            // btnPreVerse
            // 
            this.btnPreVerse.Image = global::ganjoor.Properties.Resources.repeat;
            this.btnPreVerse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPreVerse.Name = "btnPreVerse";
            this.btnPreVerse.Size = new System.Drawing.Size(60, 50);
            this.btnPreVerse.Text = "مصرع قبل";
            this.btnPreVerse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPreVerse.ToolTipText = "مصرع قبل (Ctrl +Space)";
            this.btnPreVerse.Click += new System.EventHandler(this.btnPreVerse_Click);
            // 
            // btnSearchText
            // 
            this.btnSearchText.Image = global::ganjoor.Properties.Resources.search;
            this.btnSearchText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearchText.Name = "btnSearchText";
            this.btnSearchText.Size = new System.Drawing.Size(49, 50);
            this.btnSearchText.Text = "جستجو";
            this.btnSearchText.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSearchText.ToolTipText = "جستجو (Ctrl+F)";
            this.btnSearchText.Click += new System.EventHandler(this.btnSearchText_Click);
            // 
            // btnTrack
            // 
            this.btnTrack.Checked = true;
            this.btnTrack.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnTrack.Image = global::ganjoor.Properties.Resources.track32;
            this.btnTrack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTrack.Name = "btnTrack";
            this.btnTrack.Size = new System.Drawing.Size(48, 50);
            this.btnTrack.Text = "رهگیری";
            this.btnTrack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTrack.Click += new System.EventHandler(this.btnTrack_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 53);
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
            // btnStartFromHere
            // 
            this.btnStartFromHere.Image = global::ganjoor.Properties.Resources.rewind;
            this.btnStartFromHere.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartFromHere.Name = "btnStartFromHere";
            this.btnStartFromHere.Size = new System.Drawing.Size(66, 50);
            this.btnStartFromHere.Text = "آغاز از اینجا";
            this.btnStartFromHere.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnStartFromHere.Click += new System.EventHandler(this.btnStartFromHere_Click);
            // 
            // btnStopHere
            // 
            this.btnStopHere.Image = global::ganjoor.Properties.Resources.stop;
            this.btnStopHere.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStopHere.Name = "btnStopHere";
            this.btnStopHere.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.btnStopHere.Size = new System.Drawing.Size(73, 50);
            this.btnStopHere.Text = "پایان در اینجا";
            this.btnStopHere.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnStopHere.Click += new System.EventHandler(this.btnStopHere_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 53);
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
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnTest
            // 
            this.btnTest.Image = global::ganjoor.Properties.Resources.sound;
            this.btnTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(47, 50);
            this.btnTest.Text = "آزمایش";
            this.btnTest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnReset
            // 
            this.btnReset.Image = global::ganjoor.Properties.Resources.remove;
            this.btnReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(36, 50);
            this.btnReset.Text = "از نو";
            this.btnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // chkWaveForm
            // 
            this.chkWaveForm.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.chkWaveForm.Image = global::ganjoor.Properties.Resources.displaywaveform;
            this.chkWaveForm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.chkWaveForm.Name = "chkWaveForm";
            this.chkWaveForm.Size = new System.Drawing.Size(66, 50);
            this.chkWaveForm.Text = "نمایش موج";
            this.chkWaveForm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.chkWaveForm.Click += new System.EventHandler(this.chkWaveForm_Click);
            // 
            // chkShowNextVerse
            // 
            this.chkShowNextVerse.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.chkShowNextVerse.Image = global::ganjoor.Properties.Resources.help;
            this.chkShowNextVerse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.chkShowNextVerse.Name = "chkShowNextVerse";
            this.chkShowNextVerse.Size = new System.Drawing.Size(74, 50);
            this.chkShowNextVerse.Text = "نمایش بعدی";
            this.chkShowNextVerse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.chkShowNextVerse.Click += new System.EventHandler(this.chkShowNextVerse_Click);
            // 
            // lblDesc
            // 
            this.lblDesc.BackColor = System.Drawing.SystemColors.Window;
            this.lblDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDesc.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDesc.Location = new System.Drawing.Point(0, 0);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(764, 66);
            this.lblDesc.TabIndex = 9;
            this.lblDesc.Text = resources.GetString("lblDesc.Text");
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVerse
            // 
            this.lblVerse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVerse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVerse.Location = new System.Drawing.Point(0, 119);
            this.lblVerse.Name = "lblVerse";
            this.lblVerse.Size = new System.Drawing.Size(764, 142);
            this.lblVerse.TabIndex = 11;
            this.lblVerse.Text = "هنوز شروع نشده!";
            this.lblVerse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer
            // 
            this.timer.Interval = 500;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // lblNextVerse
            // 
            this.lblNextVerse.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblNextVerse.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblNextVerse.Location = new System.Drawing.Point(0, 261);
            this.lblNextVerse.Name = "lblNextVerse";
            this.lblNextVerse.Size = new System.Drawing.Size(764, 31);
            this.lblNextVerse.TabIndex = 13;
            this.lblNextVerse.Text = "مصرع بعد: ";
            this.lblNextVerse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNextVerse.Visible = false;
            // 
            // trackBar
            // 
            this.trackBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.trackBar.Enabled = false;
            this.trackBar.Location = new System.Drawing.Point(0, 292);
            this.trackBar.Name = "trackBar";
            this.trackBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBar.Size = new System.Drawing.Size(764, 45);
            this.trackBar.TabIndex = 14;
            this.trackBar.TickFrequency = 60000;
            this.trackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // stsBar
            // 
            this.stsBar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stsBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTime,
            this.toolStripStatusLabel1,
            this.btnConfig});
            this.stsBar.Location = new System.Drawing.Point(0, 440);
            this.stsBar.Name = "stsBar";
            this.stsBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.stsBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.stsBar.Size = new System.Drawing.Size(764, 22);
            this.stsBar.SizingGrip = false;
            this.stsBar.TabIndex = 15;
            // 
            // lblTime
            // 
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(57, 17);
            this.lblTime.Text = "00:00:00";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(629, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // btnConfig
            // 
            this.btnConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnConfig.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSrtOutput,
            this.chkWordMode});
            this.btnConfig.Image = global::ganjoor.Properties.Resources.process;
            this.btnConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(32, 20);
            this.btnConfig.Text = "toolStripSplitButton1";
            // 
            // chkWordMode
            // 
            this.chkWordMode.Name = "chkWordMode";
            this.chkWordMode.Size = new System.Drawing.Size(214, 22);
            this.chkWordMode.Text = "همگامسازی بر اساس کلمه";
            this.chkWordMode.Click += new System.EventHandler(this.chkWordMode_Click);
            // 
            // btnSrtOutput
            // 
            this.btnSrtOutput.Name = "btnSrtOutput";
            this.btnSrtOutput.Size = new System.Drawing.Size(214, 22);
            this.btnSrtOutput.Text = "خروجی srt";
            this.btnSrtOutput.Click += new System.EventHandler(this.btnSrtOutput_Click);
            // 
            // waveViewer
            // 
            this.waveViewer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.waveViewer.Location = new System.Drawing.Point(0, 337);
            this.waveViewer.Name = "waveViewer";
            this.waveViewer.PenColor = System.Drawing.Color.DodgerBlue;
            this.waveViewer.PenWidth = 1F;
            this.waveViewer.Position = 0;
            this.waveViewer.SamplesPerPixel = 128;
            this.waveViewer.Size = new System.Drawing.Size(764, 103);
            this.waveViewer.StartPosition = ((long)(0));
            this.waveViewer.TabIndex = 12;
            this.waveViewer.Visible = false;
            this.waveViewer.WaveStream = null;
            // 
            // SyncPoemAudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(764, 462);
            this.Controls.Add(this.lblVerse);
            this.Controls.Add(this.lblNextVerse);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.waveViewer);
            this.Controls.Add(this.stsBar);
            this.Controls.Add(this.tlbr);
            this.Controls.Add(this.lblDesc);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "SyncPoemAudio";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "همگام‌سازی شعر";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SyncPoemAudio_FormClosing);
            this.Load += new System.EventHandler(this.SyncPoemAudio_Load);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.SyncPoemAudio_PreviewKeyDown);
            this.tlbr.ResumeLayout(false);
            this.tlbr.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.stsBar.ResumeLayout(false);
            this.stsBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip tlbr;
        private System.Windows.Forms.ToolStripButton btnNextVerse;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnPlayPause;
        private System.Windows.Forms.Label lblVerse;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnTest;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnReset;
        private System.Windows.Forms.ToolStripButton chkWaveForm;
        private ganjoor.CustomWaveViewer waveViewer;
        private System.Windows.Forms.ToolStripButton btnStartFromHere;
        private System.Windows.Forms.Label lblNextVerse;
        private System.Windows.Forms.ToolStripButton btnStopHere;
        private System.Windows.Forms.ToolStripButton chkShowNextVerse;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.StatusStrip stsBar;
        private System.Windows.Forms.ToolStripStatusLabel lblTime;
        private System.Windows.Forms.ToolStripButton btnPreVerse;
        private System.Windows.Forms.ToolStripButton btnTrack;
        private System.Windows.Forms.ToolStripButton btnSearchText;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSplitButton btnConfig;
        private System.Windows.Forms.ToolStripMenuItem chkWordMode;
        private System.Windows.Forms.ToolStripMenuItem btnSrtOutput;
    }
}