using System.ComponentModel;
using System.Windows.Forms;

namespace ganjoor
{
    partial class SndWizOptions
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnBrowseTempPath = new System.Windows.Forms.Button();
            this.btnSelectTempPath = new System.Windows.Forms.Button();
            this.txtTempPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(514, 41);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "انصراف";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(433, 41);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "تأیید";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnBrowseTempPath
            // 
            this.btnBrowseTempPath.Location = new System.Drawing.Point(526, 12);
            this.btnBrowseTempPath.Name = "btnBrowseTempPath";
            this.btnBrowseTempPath.Size = new System.Drawing.Size(63, 23);
            this.btnBrowseTempPath.TabIndex = 11;
            this.btnBrowseTempPath.Text = "مرور";
            this.btnBrowseTempPath.UseVisualStyleBackColor = true;
            this.btnBrowseTempPath.Click += new System.EventHandler(this.btnBrowseTempPath_Click);
            // 
            // btnSelectTempPath
            // 
            this.btnSelectTempPath.Location = new System.Drawing.Point(492, 12);
            this.btnSelectTempPath.Name = "btnSelectTempPath";
            this.btnSelectTempPath.Size = new System.Drawing.Size(28, 23);
            this.btnSelectTempPath.TabIndex = 10;
            this.btnSelectTempPath.Text = "...";
            this.btnSelectTempPath.UseVisualStyleBackColor = true;
            this.btnSelectTempPath.Click += new System.EventHandler(this.btnSelectTempPath_Click);
            // 
            // txtTempPath
            // 
            this.txtTempPath.Location = new System.Drawing.Point(129, 14);
            this.txtTempPath.Name = "txtTempPath";
            this.txtTempPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTempPath.Size = new System.Drawing.Size(357, 21);
            this.txtTempPath.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "مسیر ذخیره خوانشها:";
            // 
            // SndWizOptions
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(596, 77);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnBrowseTempPath);
            this.Controls.Add(this.btnSelectTempPath);
            this.Controls.Add(this.txtTempPath);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SndWizOptions";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تنظیمات دریافت خوانشها";
            this.Load += new System.EventHandler(this.SndWizOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnCancel;
        private Button btnOK;
        private Button btnBrowseTempPath;
        private Button btnSelectTempPath;
        private TextBox txtTempPath;
        private Label label1;
    }
}