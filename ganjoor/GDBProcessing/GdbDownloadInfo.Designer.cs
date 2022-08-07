using System.ComponentModel;
using System.Windows.Forms;

namespace ganjoor
{
    partial class GdbDownloadInfo
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
            this.lblGdbName = new System.Windows.Forms.Label();
            this.prgess = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lblGdbName
            // 
            this.lblGdbName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGdbName.Location = new System.Drawing.Point(276, 0);
            this.lblGdbName.Name = "lblGdbName";
            this.lblGdbName.Size = new System.Drawing.Size(160, 23);
            this.lblGdbName.TabIndex = 0;
            this.lblGdbName.Text = "نام بسته";
            this.lblGdbName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // prgess
            // 
            this.prgess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.prgess.Location = new System.Drawing.Point(3, 3);
            this.prgess.Name = "prgess";
            this.prgess.Size = new System.Drawing.Size(267, 20);
            this.prgess.TabIndex = 1;
            // 
            // GdbDownloadInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.prgess);
            this.Controls.Add(this.lblGdbName);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Name = "GdbDownloadInfo";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(437, 26);
            this.ResumeLayout(false);

        }

        #endregion

        private Label lblGdbName;
        private ProgressBar prgess;
    }
}
