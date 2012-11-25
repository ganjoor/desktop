namespace ganjoor
{

    partial class WSSelectList
    {

        private System.Windows.Forms.ComboBox cmbListUrl;
        private System.Windows.Forms.Label lblListName;
        private System.Windows.Forms.Label lblListDescription;
        private System.Windows.Forms.Label lblUrl;

        private void InitializeComponent()
        {
            this.lblUrl = new System.Windows.Forms.Label();
            this.cmbListUrl = new System.Windows.Forms.ComboBox();
            this.lblListName = new System.Windows.Forms.Label();
            this.lblListDescription = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lnkMoreInfo = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblUrl
            // 
            this.lblUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUrl.AutoSize = true;
            this.lblUrl.BackColor = System.Drawing.Color.Transparent;
            this.lblUrl.Location = new System.Drawing.Point(463, 73);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblUrl.Size = new System.Drawing.Size(83, 13);
            this.lblUrl.TabIndex = 0;
            this.lblUrl.Text = "نشانی فهرست:";
            // 
            // cmbListUrl
            // 
            this.cmbListUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbListUrl.FormattingEnabled = true;
            this.cmbListUrl.Location = new System.Drawing.Point(63, 70);
            this.cmbListUrl.Name = "cmbListUrl";
            this.cmbListUrl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbListUrl.Size = new System.Drawing.Size(397, 21);
            this.cmbListUrl.TabIndex = 1;
            this.cmbListUrl.SelectedIndexChanged += new System.EventHandler(this.cmbListUrl_SelectedIndexChanged);
            this.cmbListUrl.TextChanged += new System.EventHandler(this.cmbListUrl_TextChanged);
            // 
            // lblListName
            // 
            this.lblListName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblListName.BackColor = System.Drawing.SystemColors.Info;
            this.lblListName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblListName.Location = new System.Drawing.Point(63, 102);
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
            this.lblListDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblListDescription.BackColor = System.Drawing.SystemColors.Info;
            this.lblListDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblListDescription.Location = new System.Drawing.Point(63, 136);
            this.lblListDescription.Name = "lblListDescription";
            this.lblListDescription.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblListDescription.Size = new System.Drawing.Size(397, 142);
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
                "ه داده‌های برنامهٔ خود بیفزایید. برای دریافت فهرست مجموعه‌های در دسترس روی «ادام" +
                "ه» کلیک کنید.";
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lnkMoreInfo
            // 
            this.lnkMoreInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkMoreInfo.AutoSize = true;
            this.lnkMoreInfo.Location = new System.Drawing.Point(382, 284);
            this.lnkMoreInfo.Name = "lnkMoreInfo";
            this.lnkMoreInfo.Size = new System.Drawing.Size(78, 13);
            this.lnkMoreInfo.TabIndex = 7;
            this.lnkMoreInfo.TabStop = true;
            this.lnkMoreInfo.Text = "توضیحات بیشتر";
            this.lnkMoreInfo.Visible = false;
            this.lnkMoreInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkMoreInfo_LinkClicked);
            // 
            // WSSelectList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.lnkMoreInfo);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.lblListDescription);
            this.Controls.Add(this.lblListName);
            this.Controls.Add(this.cmbListUrl);
            this.Controls.Add(this.lblUrl);
            this.Name = "WSSelectList";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.LinkLabel lnkMoreInfo;

    }
}