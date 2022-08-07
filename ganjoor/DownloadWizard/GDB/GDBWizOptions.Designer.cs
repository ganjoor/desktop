using System.ComponentModel;
using System.Windows.Forms;

namespace ganjoor
{
    partial class GDBWizOptions
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
            this.btnClearHistory = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTempPath = new System.Windows.Forms.TextBox();
            this.btnSelectTempPath = new System.Windows.Forms.Button();
            this.btnBrowseTempPath = new System.Windows.Forms.Button();
            this.chkDeleteDownloadedFiles = new System.Windows.Forms.CheckBox();
            this.btnGDBListEditor = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClearHistory
            // 
            this.btnClearHistory.Location = new System.Drawing.Point(456, 94);
            this.btnClearHistory.Name = "btnClearHistory";
            this.btnClearHistory.Size = new System.Drawing.Size(168, 23);
            this.btnClearHistory.TabIndex = 8;
            this.btnClearHistory.Text = "پاکسازی تاریخچهٔ نشانیها";
            this.btnClearHistory.UseVisualStyleBackColor = true;
            this.btnClearHistory.Click += new System.EventHandler(this.btnClearHistory_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "مسیر دریافت فایلهای میانی:";
            // 
            // txtTempPath
            // 
            this.txtTempPath.Location = new System.Drawing.Point(164, 15);
            this.txtTempPath.Name = "txtTempPath";
            this.txtTempPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTempPath.Size = new System.Drawing.Size(357, 21);
            this.txtTempPath.TabIndex = 3;
            // 
            // btnSelectTempPath
            // 
            this.btnSelectTempPath.Location = new System.Drawing.Point(527, 13);
            this.btnSelectTempPath.Name = "btnSelectTempPath";
            this.btnSelectTempPath.Size = new System.Drawing.Size(28, 23);
            this.btnSelectTempPath.TabIndex = 4;
            this.btnSelectTempPath.Text = "...";
            this.btnSelectTempPath.UseVisualStyleBackColor = true;
            this.btnSelectTempPath.Click += new System.EventHandler(this.btnSelectTempPath_Click);
            // 
            // btnBrowseTempPath
            // 
            this.btnBrowseTempPath.Location = new System.Drawing.Point(561, 13);
            this.btnBrowseTempPath.Name = "btnBrowseTempPath";
            this.btnBrowseTempPath.Size = new System.Drawing.Size(63, 23);
            this.btnBrowseTempPath.TabIndex = 5;
            this.btnBrowseTempPath.Text = "مرور";
            this.btnBrowseTempPath.UseVisualStyleBackColor = true;
            this.btnBrowseTempPath.Click += new System.EventHandler(this.btnBrowseTempPath_Click);
            // 
            // chkDeleteDownloadedFiles
            // 
            this.chkDeleteDownloadedFiles.AutoSize = true;
            this.chkDeleteDownloadedFiles.Location = new System.Drawing.Point(23, 42);
            this.chkDeleteDownloadedFiles.Name = "chkDeleteDownloadedFiles";
            this.chkDeleteDownloadedFiles.Size = new System.Drawing.Size(215, 17);
            this.chkDeleteDownloadedFiles.TabIndex = 6;
            this.chkDeleteDownloadedFiles.Text = "فایلهای دریافتی پس از پردازش پاک شوند.";
            this.chkDeleteDownloadedFiles.UseVisualStyleBackColor = true;
            // 
            // btnGDBListEditor
            // 
            this.btnGDBListEditor.Location = new System.Drawing.Point(456, 65);
            this.btnGDBListEditor.Name = "btnGDBListEditor";
            this.btnGDBListEditor.Size = new System.Drawing.Size(168, 23);
            this.btnGDBListEditor.TabIndex = 7;
            this.btnGDBListEditor.Text = "ویرایشگر فهرست مجموعه‌ها";
            this.btnGDBListEditor.UseVisualStyleBackColor = true;
            this.btnGDBListEditor.Click += new System.EventHandler(this.btnGDBListEditor_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(23, 117);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "تأیید";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(104, 117);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "انصراف";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // GDBWizOptions
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(646, 152);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnGDBListEditor);
            this.Controls.Add(this.chkDeleteDownloadedFiles);
            this.Controls.Add(this.btnBrowseTempPath);
            this.Controls.Add(this.btnSelectTempPath);
            this.Controls.Add(this.txtTempPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClearHistory);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GDBWizOptions";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تنظیمات دریافت مجموعه‌ها";
            this.Load += new System.EventHandler(this.GDBWizOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnClearHistory;
        private Label label1;
        private TextBox txtTempPath;
        private Button btnSelectTempPath;
        private Button btnBrowseTempPath;
        private CheckBox chkDeleteDownloadedFiles;
        private Button btnGDBListEditor;
        private Button btnOK;
        private Button btnCancel;

    }
}