namespace ganjoor
{
    partial class GDBPictureDirSelector
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
            this.chkPicturesEnabled = new System.Windows.Forms.CheckBox();
            this.grpPictures = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPicturesUrlPrefix = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.grpPictures.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkPicturesEnabled
            // 
            this.chkPicturesEnabled.AutoSize = true;
            this.chkPicturesEnabled.Location = new System.Drawing.Point(12, 12);
            this.chkPicturesEnabled.Name = "chkPicturesEnabled";
            this.chkPicturesEnabled.Size = new System.Drawing.Size(341, 17);
            this.chkPicturesEnabled.TabIndex = 1;
            this.chkPicturesEnabled.Text = "مشخصات تصاویر شاعران در فهرست و فایلهای خروجی گنجانده شود.";
            this.chkPicturesEnabled.UseVisualStyleBackColor = true;
            this.chkPicturesEnabled.CheckedChanged += new System.EventHandler(this.chkPicturesEnabled_CheckedChanged);
            // 
            // grpPictures
            // 
            this.grpPictures.Controls.Add(this.label4);
            this.grpPictures.Controls.Add(this.txtPicturesUrlPrefix);
            this.grpPictures.Controls.Add(this.label3);
            this.grpPictures.Controls.Add(this.label2);
            this.grpPictures.Controls.Add(this.btnSelectPath);
            this.grpPictures.Controls.Add(this.txtPath);
            this.grpPictures.Controls.Add(this.label1);
            this.grpPictures.Enabled = false;
            this.grpPictures.Location = new System.Drawing.Point(12, 35);
            this.grpPictures.Name = "grpPictures";
            this.grpPictures.Size = new System.Drawing.Size(561, 118);
            this.grpPictures.TabIndex = 2;
            this.grpPictures.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(143, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(276, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "اگر خالی باشد در فهرست نشانی تصاویر گنجانده نمی‌شود.";
            // 
            // txtPicturesUrlPrefix
            // 
            this.txtPicturesUrlPrefix.Location = new System.Drawing.Point(7, 61);
            this.txtPicturesUrlPrefix.Name = "txtPicturesUrlPrefix";
            this.txtPicturesUrlPrefix.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPicturesUrlPrefix.Size = new System.Drawing.Size(415, 21);
            this.txtPicturesUrlPrefix.TabIndex = 5;
            this.txtPicturesUrlPrefix.Text = "http://someurl.com/img/";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(428, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "پیشوند نشانی وب تصاویر:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(456, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "فایل png همنام با شناسهٔ عددی هر شاعر اگر در این مسیر یافت شود برای او استفاده خوا" +
                "هد شد.";
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Location = new System.Drawing.Point(7, 12);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(31, 23);
            this.btnSelectPath.TabIndex = 2;
            this.btnSelectPath.Text = "...";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(44, 14);
            this.txtPath.Name = "txtPath";
            this.txtPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPath.Size = new System.Drawing.Size(436, 21);
            this.txtPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(486, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "مسیر تصاویر:";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(12, 160);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "ادامه";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // GDBPictureDirSelector
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(603, 195);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpPictures);
            this.Controls.Add(this.chkPicturesEnabled);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Name = "GDBPictureDirSelector";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تصاویر شاعران";
            this.grpPictures.ResumeLayout(false);
            this.grpPictures.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkPicturesEnabled;
        private System.Windows.Forms.GroupBox grpPictures;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPicturesUrlPrefix;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOK;
    }
}