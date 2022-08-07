
using System.ComponentModel;
using System.Windows.Forms;

namespace ganjoor.Audio_Support.TimingHelper
{
    partial class THHelper
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblVerse = new ganjoor.HighlightLabel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblNextVerse = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblVerse
            // 
            this.lblVerse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVerse.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblVerse.HighlightColor = System.Drawing.Color.Red;
            this.lblVerse.Keyword = "";
            this.lblVerse.Location = new System.Drawing.Point(0, 0);
            this.lblVerse.Name = "lblVerse";
            this.lblVerse.Size = new System.Drawing.Size(585, 221);
            this.lblVerse.TabIndex = 1;
            this.lblVerse.Text = "شروع";
            this.lblVerse.Click += new System.EventHandler(this.lblVerse_Click);
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar.Location = new System.Drawing.Point(0, 221);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(585, 53);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 2;
            // 
            // lblNextVerse
            // 
            this.lblNextVerse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNextVerse.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblNextVerse.Location = new System.Drawing.Point(0, 278);
            this.lblNextVerse.Name = "lblNextVerse";
            this.lblNextVerse.Size = new System.Drawing.Size(585, 87);
            this.lblNextVerse.TabIndex = 3;
            this.lblNextVerse.Text = "الا یا ایها الساقی ادر کأسا و ناولها";
            this.lblNextVerse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // THHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.lblNextVerse);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblVerse);
            this.Name = "THHelper";
            this.ResumeLayout(false);

        }

        #endregion

        private HighlightLabel lblVerse;
        private ProgressBar progressBar;
        private Label lblNextVerse;
        private Timer timer;
    }
}
