
namespace ganjoor.Audio_Support.TimingHelper
{
    partial class THFirstVerse
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
            this.components = new System.ComponentModel.Container();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblVerse = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.timerStartToSilence = new System.Windows.Forms.Timer(this.components);
            this.timerSilenceToStop = new System.Windows.Forms.Timer(this.components);
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStart.Location = new System.Drawing.Point(0, 0);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(585, 93);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "شروع";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblVerse
            // 
            this.lblVerse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVerse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVerse.Location = new System.Drawing.Point(0, 0);
            this.lblVerse.Name = "lblVerse";
            this.lblVerse.Size = new System.Drawing.Size(585, 272);
            this.lblVerse.TabIndex = 0;
            this.lblVerse.Text = "الا یا ایها الساقی ادر کأسا و ناولها";
            this.lblVerse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVerse.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnStart);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 272);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(585, 93);
            this.pnlBottom.TabIndex = 4;
            // 
            // timerStartToSilence
            // 
            this.timerStartToSilence.Tick += new System.EventHandler(this.timerStartToSilence_Tick);
            // 
            // timerSilenceToStop
            // 
            this.timerSilenceToStop.Tick += new System.EventHandler(this.timerSilenceToStop_Tick);
            // 
            // THFirstVerse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblVerse);
            this.Controls.Add(this.pnlBottom);
            this.Name = "THFirstVerse";
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblVerse;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Timer timerStartToSilence;
        private System.Windows.Forms.Timer timerSilenceToStop;
    }
}
