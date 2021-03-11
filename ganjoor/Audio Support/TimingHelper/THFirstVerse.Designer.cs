
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnSilence = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblVerse = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnStart.Location = new System.Drawing.Point(444, 0);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(141, 93);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "شروع";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // btnSilence
            // 
            this.btnSilence.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSilence.Enabled = false;
            this.btnSilence.Location = new System.Drawing.Point(141, 0);
            this.btnSilence.Name = "btnSilence";
            this.btnSilence.Size = new System.Drawing.Size(141, 93);
            this.btnSilence.TabIndex = 2;
            this.btnSilence.Text = "تنفس";
            this.btnSilence.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(0, 0);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(141, 93);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "پایان";
            this.btnStop.UseVisualStyleBackColor = true;
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
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnStart);
            this.pnlBottom.Controls.Add(this.btnSilence);
            this.pnlBottom.Controls.Add(this.btnStop);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 272);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(585, 93);
            this.pnlBottom.TabIndex = 4;
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
        private System.Windows.Forms.Button btnSilence;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblVerse;
        private System.Windows.Forms.Panel pnlBottom;
    }
}
