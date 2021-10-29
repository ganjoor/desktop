namespace ganjoor
{
    partial class ReplaceInDb
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReplaceInDb));
            this.label1 = new System.Windows.Forms.Label();
            this.btnYaKaf = new System.Windows.Forms.Button();
            this.btnHeye = new System.Windows.Forms.Button();
            this.btnNumbers = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReplace = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRep = new System.Windows.Forms.TextBox();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.btnOther = new System.Windows.Forms.Button();
            this.chkOnlyPoemTitles = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(600, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "تذکر مهم: لطفاً قبل از استفاده از این ابزار از پایگاه داده‌های برنامه پشتیبان تهی" +
    "ه کنید. محدودهٔ عملکرد شامل تمام پایگاه داده‌هاست.";
            // 
            // btnYaKaf
            // 
            this.btnYaKaf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnYaKaf.Location = new System.Drawing.Point(15, 36);
            this.btnYaKaf.Name = "btnYaKaf";
            this.btnYaKaf.Size = new System.Drawing.Size(597, 23);
            this.btnYaKaf.TabIndex = 1;
            this.btnYaKaf.Text = "جایگزینی «ي» و «ك» با «ی» و «ک»";
            this.btnYaKaf.UseVisualStyleBackColor = true;
            this.btnYaKaf.Click += new System.EventHandler(this.btnYaKaf_Click);
            // 
            // btnHeye
            // 
            this.btnHeye.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHeye.Location = new System.Drawing.Point(15, 65);
            this.btnHeye.Name = "btnHeye";
            this.btnHeye.Size = new System.Drawing.Size(597, 23);
            this.btnHeye.TabIndex = 2;
            this.btnHeye.Text = "جایگزینی «هٔ» و «ه‌ی» با «هٔ» ";
            this.btnHeye.UseVisualStyleBackColor = true;
            this.btnHeye.Click += new System.EventHandler(this.btnHeye_Click);
            // 
            // btnNumbers
            // 
            this.btnNumbers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNumbers.Location = new System.Drawing.Point(15, 94);
            this.btnNumbers.Name = "btnNumbers";
            this.btnNumbers.Size = new System.Drawing.Size(597, 23);
            this.btnNumbers.TabIndex = 3;
            this.btnNumbers.Text = "جایگزینی اعداد انگلیسی و عربی با شکل فارسی آنها";
            this.btnNumbers.UseVisualStyleBackColor = true;
            this.btnNumbers.Click += new System.EventHandler(this.btnNumbers_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnReplace);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtRep);
            this.groupBox1.Controls.Add(this.txtFind);
            this.groupBox1.Location = new System.Drawing.Point(15, 171);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(597, 117);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "جایگزینی دلخواه:";
            // 
            // btnReplace
            // 
            this.btnReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReplace.Location = new System.Drawing.Point(387, 82);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(98, 23);
            this.btnReplace.TabIndex = 4;
            this.btnReplace.Text = "جایگزین شود";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(491, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "متن جایگزین:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(491, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "متن مورد جستجو:";
            // 
            // txtRep
            // 
            this.txtRep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRep.Location = new System.Drawing.Point(6, 53);
            this.txtRep.Name = "txtRep";
            this.txtRep.Size = new System.Drawing.Size(479, 21);
            this.txtRep.TabIndex = 3;
            // 
            // txtFind
            // 
            this.txtFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFind.Location = new System.Drawing.Point(6, 23);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(479, 21);
            this.txtFind.TabIndex = 1;
            // 
            // btnOther
            // 
            this.btnOther.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOther.Location = new System.Drawing.Point(15, 124);
            this.btnOther.Name = "btnOther";
            this.btnOther.Size = new System.Drawing.Size(597, 23);
            this.btnOther.TabIndex = 4;
            this.btnOther.Text = "حذف کشیده، خالی‌های متوالی، خالی قبل از علامتهای سجاوندی";
            this.btnOther.UseVisualStyleBackColor = true;
            this.btnOther.Click += new System.EventHandler(this.btnOther_Click);
            // 
            // chkOnlyPoemTitles
            // 
            this.chkOnlyPoemTitles.AutoSize = true;
            this.chkOnlyPoemTitles.Location = new System.Drawing.Point(20, 295);
            this.chkOnlyPoemTitles.Name = "chkOnlyPoemTitles";
            this.chkOnlyPoemTitles.Size = new System.Drawing.Size(213, 17);
            this.chkOnlyPoemTitles.TabIndex = 5;
            this.chkOnlyPoemTitles.Text = "فقط در عنوان اشعار جایگزینی صورت گیرد";
            this.chkOnlyPoemTitles.UseVisualStyleBackColor = true;
            // 
            // ReplaceInDb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 329);
            this.Controls.Add(this.chkOnlyPoemTitles);
            this.Controls.Add(this.btnOther);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnNumbers);
            this.Controls.Add(this.btnHeye);
            this.Controls.Add(this.btnYaKaf);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReplaceInDb";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "جایگزینی در پایگاه داده‌ها";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnYaKaf;
        private System.Windows.Forms.Button btnHeye;
        private System.Windows.Forms.Button btnNumbers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRep;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.Button btnOther;
        private System.Windows.Forms.CheckBox chkOnlyPoemTitles;
    }
}