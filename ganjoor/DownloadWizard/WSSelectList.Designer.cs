namespace ganjoor
{

    partial class WSSelectList
    {

        private System.Windows.Forms.ComboBox cmbListUrl;
        private System.Windows.Forms.Label lblListName;
        private System.Windows.Forms.Label lblListDescription;
        private System.Windows.Forms.Label label1;

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cmbListUrl = new System.Windows.Forms.ComboBox();
            this.lblListName = new System.Windows.Forms.Label();
            this.lblListDescription = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(463, 73);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "نشانی فهرست:";
            // 
            // cmbListUrl
            // 
            this.cmbListUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbListUrl.FormattingEnabled = true;
            this.cmbListUrl.Location = new System.Drawing.Point(60, 70);
            this.cmbListUrl.Name = "cmbListUrl";
            this.cmbListUrl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbListUrl.Size = new System.Drawing.Size(397, 21);
            this.cmbListUrl.TabIndex = 1;
            this.cmbListUrl.SelectedIndexChanged += new System.EventHandler(this.cmbListUrl_SelectedIndexChanged);
            this.cmbListUrl.TextChanged += new System.EventHandler(this.cmbListUrl_TextChanged);
            // 
            // lblListName
            // 
            this.lblListName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblListName.BackColor = System.Drawing.SystemColors.Info;
            this.lblListName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblListName.Location = new System.Drawing.Point(60, 102);
            this.lblListName.Name = "lblListName";
            this.lblListName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblListName.Size = new System.Drawing.Size(397, 23);
            this.lblListName.TabIndex = 3;
            this.lblListName.Text = "برای بروزآوری نام و شرح فهرست کلیک کنید.";
            this.lblListName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblListName.Click += new System.EventHandler(this.RetriveNameDesc);
            // 
            // lblListDescription
            // 
            this.lblListDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblListDescription.BackColor = System.Drawing.SystemColors.Info;
            this.lblListDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblListDescription.Location = new System.Drawing.Point(60, 136);
            this.lblListDescription.Name = "lblListDescription";
            this.lblListDescription.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblListDescription.Size = new System.Drawing.Size(397, 58);
            this.lblListDescription.TabIndex = 5;
            this.lblListDescription.Text = "برای بروزآوری نام و شرح فهرست کلیک کنید.";
            this.lblListDescription.Click += new System.EventHandler(this.RetriveNameDesc);
            // 
            // lblDesc
            // 
            this.lblDesc.BackColor = System.Drawing.SystemColors.Window;
            this.lblDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDesc.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDesc.Location = new System.Drawing.Point(0, 0);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(585, 60);
            this.lblDesc.TabIndex = 6;
            this.lblDesc.Text = "به کمک این ابزار می‌توانید مجموعه‌های در دسترس از طریق اینترنت را دریافت کنید و ب" +
                "ه داده‌های برنامۀ خود بیفزایید. برای دریافت فهرست مجموعه‌های در دسترس روی «ادامه" +
                "» کلیک کنید.";
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WSSelectList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.lblListDescription);
            this.Controls.Add(this.lblListName);
            this.Controls.Add(this.cmbListUrl);
            this.Controls.Add(this.label1);
            this.Name = "WSSelectList";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label lblDesc;

    }
}