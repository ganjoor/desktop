using System.ComponentModel;
using System.Windows.Forms;

namespace ganjoor
{
    partial class SndDownloadInfo
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
            this.prgess = new System.Windows.Forms.ProgressBar();
            this.lblSndName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // prgess
            // 
            this.prgess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgess.Location = new System.Drawing.Point(2, 5);
            this.prgess.Name = "prgess";
            this.prgess.Size = new System.Drawing.Size(267, 20);
            this.prgess.TabIndex = 3;
            // 
            // lblGdbName
            // 
            this.lblSndName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSndName.Location = new System.Drawing.Point(275, 2);
            this.lblSndName.Name = "lblGdbName";
            this.lblSndName.Size = new System.Drawing.Size(160, 23);
            this.lblSndName.TabIndex = 2;
            this.lblSndName.Text = "نام بسته";
            this.lblSndName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SndDownloadInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.prgess);
            this.Controls.Add(this.lblSndName);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Name = "SndDownloadInfo";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(437, 26);
            this.ResumeLayout(false);

        }

        #endregion

        private ProgressBar prgess;
        private Label lblSndName;
    }
}
