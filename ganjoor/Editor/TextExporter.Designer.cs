namespace ganjoor
{
    partial class TextExporter
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
            this.lblExportLevel = new System.Windows.Forms.Label();
            this.cmbTextExportLevel = new System.Windows.Forms.ComboBox();
            this.lblExportLevelComment = new System.Windows.Forms.Label();
            this.chkPoet = new System.Windows.Forms.CheckBox();
            this.txtPoetSeparator = new System.Windows.Forms.TextBox();
            this.txtCatSeparator = new System.Windows.Forms.TextBox();
            this.chkCat = new System.Windows.Forms.CheckBox();
            this.txtPoemSeparator = new System.Windows.Forms.TextBox();
            this.chkPoem = new System.Windows.Forms.CheckBox();
            this.lblComment = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblExportLevel
            // 
            this.lblExportLevel.AutoSize = true;
            this.lblExportLevel.Location = new System.Drawing.Point(12, 9);
            this.lblExportLevel.Name = "lblExportLevel";
            this.lblExportLevel.Size = new System.Drawing.Size(71, 13);
            this.lblExportLevel.TabIndex = 0;
            this.lblExportLevel.Text = "سطح تفکیک:";
            // 
            // cmbTextExportLevel
            // 
            this.cmbTextExportLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTextExportLevel.FormattingEnabled = true;
            this.cmbTextExportLevel.Items.AddRange(new object[] {
            "شاعر",
            "بخش",
            "شعر"});
            this.cmbTextExportLevel.Location = new System.Drawing.Point(89, 6);
            this.cmbTextExportLevel.Name = "cmbTextExportLevel";
            this.cmbTextExportLevel.Size = new System.Drawing.Size(338, 21);
            this.cmbTextExportLevel.TabIndex = 1;
            // 
            // lblExportLevelComment
            // 
            this.lblExportLevelComment.Location = new System.Drawing.Point(93, 30);
            this.lblExportLevelComment.Name = "lblExportLevelComment";
            this.lblExportLevelComment.Size = new System.Drawing.Size(334, 48);
            this.lblExportLevelComment.TabIndex = 2;
            this.lblExportLevelComment.Text = "تمام اشعار شاعر در یک فایل متنی گنجانده می‌شود.";
            // 
            // chkPoet
            // 
            this.chkPoet.AutoSize = true;
            this.chkPoet.Location = new System.Drawing.Point(89, 81);
            this.chkPoet.Name = "chkPoet";
            this.chkPoet.Size = new System.Drawing.Size(221, 17);
            this.chkPoet.TabIndex = 3;
            this.chkPoet.Text = "نام شاعر در فایل سطح شاعر گنجانده شود";
            this.chkPoet.UseVisualStyleBackColor = true;
            // 
            // txtPoetSeparator
            // 
            this.txtPoetSeparator.Location = new System.Drawing.Point(380, 81);
            this.txtPoetSeparator.Name = "txtPoetSeparator";
            this.txtPoetSeparator.Size = new System.Drawing.Size(47, 21);
            this.txtPoetSeparator.TabIndex = 4;
            this.txtPoetSeparator.Text = "===";
            // 
            // txtCatSeparator
            // 
            this.txtCatSeparator.Location = new System.Drawing.Point(380, 104);
            this.txtCatSeparator.Name = "txtCatSeparator";
            this.txtCatSeparator.Size = new System.Drawing.Size(47, 21);
            this.txtCatSeparator.TabIndex = 6;
            this.txtCatSeparator.Text = "---";
            // 
            // chkCat
            // 
            this.chkCat.AutoSize = true;
            this.chkCat.Location = new System.Drawing.Point(89, 104);
            this.chkCat.Name = "chkCat";
            this.chkCat.Size = new System.Drawing.Size(254, 17);
            this.chkCat.TabIndex = 5;
            this.chkCat.Text = "نام بخش در فایل سطح بخش و شاعر گنجانده شود";
            this.chkCat.UseVisualStyleBackColor = true;
            // 
            // txtPoemSeparator
            // 
            this.txtPoemSeparator.Location = new System.Drawing.Point(380, 127);
            this.txtPoemSeparator.Name = "txtPoemSeparator";
            this.txtPoemSeparator.Size = new System.Drawing.Size(47, 21);
            this.txtPoemSeparator.TabIndex = 8;
            this.txtPoemSeparator.Text = "***";
            // 
            // chkPoem
            // 
            this.chkPoem.AutoSize = true;
            this.chkPoem.Location = new System.Drawing.Point(89, 127);
            this.chkPoem.Name = "chkPoem";
            this.chkPoem.Size = new System.Drawing.Size(137, 17);
            this.chkPoem.TabIndex = 7;
            this.chkPoem.Text = "عنوان شعر گنجانده شود";
            this.chkPoem.UseVisualStyleBackColor = true;
            // 
            // lblComment
            // 
            this.lblComment.Location = new System.Drawing.Point(93, 151);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(334, 43);
            this.lblComment.TabIndex = 9;
            this.lblComment.Text = "در صورت انتخاب هر یک از گزینه‌های فوق پیش و پس از نام/عنوان خطهایی با متن وارد شد" +
    "ه گذاشته می‌شود. اگر متن روبروی چک‌باکس را خالی کنید این اتفاق نمی‌افتد.";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(302, 207);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(123, 23);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "انصراف";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(175, 207);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(123, 23);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "انتخاب مسیر";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // TextExporter
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(439, 241);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.txtPoemSeparator);
            this.Controls.Add(this.chkPoem);
            this.Controls.Add(this.txtCatSeparator);
            this.Controls.Add(this.chkCat);
            this.Controls.Add(this.txtPoetSeparator);
            this.Controls.Add(this.chkPoet);
            this.Controls.Add(this.lblExportLevelComment);
            this.Controls.Add(this.cmbTextExportLevel);
            this.Controls.Add(this.lblExportLevel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextExporter";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ارسال به فایل متنی";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblExportLevel;
        private System.Windows.Forms.ComboBox cmbTextExportLevel;
        private System.Windows.Forms.Label lblExportLevelComment;
        private System.Windows.Forms.CheckBox chkPoet;
        private System.Windows.Forms.TextBox txtPoetSeparator;
        private System.Windows.Forms.TextBox txtCatSeparator;
        private System.Windows.Forms.CheckBox chkCat;
        private System.Windows.Forms.TextBox txtPoemSeparator;
        private System.Windows.Forms.CheckBox chkPoem;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}