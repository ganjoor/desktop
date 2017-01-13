namespace gsync2vid
{
    partial class UnsplashImageTypeForm
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
            this.lblImput = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.rd3 = new System.Windows.Forms.RadioButton();
            this.rd2 = new System.Windows.Forms.RadioButton();
            this.rd1 = new System.Windows.Forms.RadioButton();
            this.txtSearchUrl = new System.Windows.Forms.TextBox();
            this.lblUrl = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpHelper = new System.Windows.Forms.GroupBox();
            this.lnkUnsplash = new System.Windows.Forms.LinkLabel();
            this.grpHelper.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblImput
            // 
            this.lblImput.AutoSize = true;
            this.lblImput.Location = new System.Drawing.Point(396, 95);
            this.lblImput.Name = "lblImput";
            this.lblImput.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblImput.Size = new System.Drawing.Size(128, 13);
            this.lblImput.TabIndex = 3;
            this.lblImput.Text = "دسته‌بندی یا متن جستجو:";
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(64, 95);
            this.txtInput.Name = "txtInput";
            this.txtInput.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtInput.Size = new System.Drawing.Size(326, 21);
            this.txtInput.TabIndex = 4;
            // 
            // rd3
            // 
            this.rd3.AutoSize = true;
            this.rd3.Location = new System.Drawing.Point(417, 66);
            this.rd3.Name = "rd3";
            this.rd3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rd3.Size = new System.Drawing.Size(104, 17);
            this.rd3.TabIndex = 2;
            this.rd3.TabStop = true;
            this.rd3.Text = "بر اساس جستجو";
            this.rd3.UseVisualStyleBackColor = true;
            this.rd3.CheckedChanged += new System.EventHandler(this.rd3_CheckedChanged);
            // 
            // rd2
            // 
            this.rd2.AutoSize = true;
            this.rd2.Location = new System.Drawing.Point(403, 43);
            this.rd2.Name = "rd2";
            this.rd2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rd2.Size = new System.Drawing.Size(118, 17);
            this.rd2.TabIndex = 1;
            this.rd2.TabStop = true;
            this.rd2.Text = "بر اساس دسته‌بندی";
            this.rd2.UseVisualStyleBackColor = true;
            this.rd2.CheckedChanged += new System.EventHandler(this.rd2_CheckedChanged);
            // 
            // rd1
            // 
            this.rd1.AutoSize = true;
            this.rd1.Location = new System.Drawing.Point(460, 20);
            this.rd1.Name = "rd1";
            this.rd1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rd1.Size = new System.Drawing.Size(61, 17);
            this.rd1.TabIndex = 0;
            this.rd1.TabStop = true;
            this.rd1.Text = "تصادفی";
            this.rd1.UseVisualStyleBackColor = true;
            this.rd1.CheckedChanged += new System.EventHandler(this.rd1_CheckedChanged);
            // 
            // txtSearchUrl
            // 
            this.txtSearchUrl.Location = new System.Drawing.Point(89, 193);
            this.txtSearchUrl.Name = "txtSearchUrl";
            this.txtSearchUrl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSearchUrl.Size = new System.Drawing.Size(454, 21);
            this.txtSearchUrl.TabIndex = 6;
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(9, 193);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(74, 13);
            this.lblUrl.TabIndex = 5;
            this.lblUrl.Text = "رشتهٔ جستجو:";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(387, 220);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "تأیید";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(468, 220);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "انصراف";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // grpHelper
            // 
            this.grpHelper.Controls.Add(this.lnkUnsplash);
            this.grpHelper.Controls.Add(this.rd1);
            this.grpHelper.Controls.Add(this.rd2);
            this.grpHelper.Controls.Add(this.rd3);
            this.grpHelper.Controls.Add(this.lblImput);
            this.grpHelper.Controls.Add(this.txtInput);
            this.grpHelper.Location = new System.Drawing.Point(12, 12);
            this.grpHelper.Name = "grpHelper";
            this.grpHelper.Size = new System.Drawing.Size(531, 175);
            this.grpHelper.TabIndex = 9;
            this.grpHelper.TabStop = false;
            this.grpHelper.Text = "تولید رشتهٔ جستجو:";
            // 
            // lnkUnsplash
            // 
            this.lnkUnsplash.AutoSize = true;
            this.lnkUnsplash.Location = new System.Drawing.Point(456, 127);
            this.lnkUnsplash.Name = "lnkUnsplash";
            this.lnkUnsplash.Size = new System.Drawing.Size(68, 13);
            this.lnkUnsplash.TabIndex = 5;
            this.lnkUnsplash.TabStop = true;
            this.lnkUnsplash.Text = "روشهای دیگر";
            this.lnkUnsplash.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUnsplash_LinkClicked);
            // 
            // UnsplashImageTypeForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(560, 249);
            this.Controls.Add(this.grpHelper);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtSearchUrl);
            this.Controls.Add(this.lblUrl);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UnsplashImageTypeForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "نحوهٔ انتخاب تصویر";
            this.grpHelper.ResumeLayout(false);
            this.grpHelper.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblImput;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.RadioButton rd3;
        private System.Windows.Forms.RadioButton rd2;
        private System.Windows.Forms.RadioButton rd1;
        private System.Windows.Forms.TextBox txtSearchUrl;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpHelper;
        private System.Windows.Forms.LinkLabel lnkUnsplash;

    }
}