using System;
using System.Drawing;
using System.Windows.Forms;

namespace ganjoor
{

    partial class WSSelectList
    {

        private ComboBox cmbListUrl;
        private Label lblListName;
        private Label lblListDescription;
        private Label lblUrl;

        private void InitializeComponent()
        {
            this.lblUrl = new Label();
            this.cmbListUrl = new ComboBox();
            this.lblListName = new Label();
            this.lblListDescription = new Label();
            this.lblDesc = new Label();
            this.lnkMoreInfo = new LinkLabel();
            this.SuspendLayout();
            // 
            // lblUrl
            // 
            this.lblUrl.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.lblUrl.AutoSize = true;
            this.lblUrl.BackColor = Color.Transparent;
            this.lblUrl.Location = new Point(463, 73);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.RightToLeft = RightToLeft.Yes;
            this.lblUrl.Size = new Size(83, 13);
            this.lblUrl.TabIndex = 0;
            this.lblUrl.Text = "نشانی فهرست:";
            // 
            // cmbListUrl
            // 
            this.cmbListUrl.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left)
                                                      | AnchorStyles.Right)));
            this.cmbListUrl.FormattingEnabled = true;
            this.cmbListUrl.Location = new Point(63, 70);
            this.cmbListUrl.Name = "cmbListUrl";
            this.cmbListUrl.RightToLeft = RightToLeft.No;
            this.cmbListUrl.Size = new Size(397, 21);
            this.cmbListUrl.TabIndex = 1;
            this.cmbListUrl.SelectedIndexChanged += new EventHandler(this.cmbListUrl_SelectedIndexChanged);
            this.cmbListUrl.TextChanged += new EventHandler(this.cmbListUrl_TextChanged);
            // 
            // lblListName
            // 
            this.lblListName.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left)
                                                       | AnchorStyles.Right)));
            this.lblListName.BackColor = SystemColors.Info;
            this.lblListName.BorderStyle = BorderStyle.FixedSingle;
            this.lblListName.Location = new Point(63, 102);
            this.lblListName.Name = "lblListName";
            this.lblListName.RightToLeft = RightToLeft.Yes;
            this.lblListName.Size = new Size(397, 23);
            this.lblListName.TabIndex = 3;
            this.lblListName.Text = "برای بروزآوری نام و شرح فهرست کلیک کنید.";
            this.lblListName.TextAlign = ContentAlignment.MiddleLeft;
            this.lblListName.Click += new EventHandler(this.RetriveNameDesc);
            // 
            // lblListDescription
            // 
            this.lblListDescription.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                                                               | AnchorStyles.Left)
                                                              | AnchorStyles.Right)));
            this.lblListDescription.BackColor = SystemColors.Info;
            this.lblListDescription.BorderStyle = BorderStyle.FixedSingle;
            this.lblListDescription.Location = new Point(63, 136);
            this.lblListDescription.Name = "lblListDescription";
            this.lblListDescription.RightToLeft = RightToLeft.Yes;
            this.lblListDescription.Size = new Size(397, 142);
            this.lblListDescription.TabIndex = 5;
            this.lblListDescription.Text = "برای بروزآوری نام و شرح فهرست کلیک کنید.";
            this.lblListDescription.Click += new EventHandler(this.RetriveNameDesc);
            // 
            // lblDesc
            // 
            this.lblDesc.BackColor = SystemColors.Window;
            this.lblDesc.BorderStyle = BorderStyle.FixedSingle;
            this.lblDesc.Dock = DockStyle.Top;
            this.lblDesc.Location = new Point(0, 0);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new Size(585, 60);
            this.lblDesc.TabIndex = 6;
            this.lblDesc.Text = "به کمک این ابزار می‌توانید مجموعه‌های در دسترس از طریق اینترنت را دریافت کنید و ب" +
                "ه داده‌های برنامهٔ خود بیفزایید. برای دریافت فهرست مجموعه‌های در دسترس روی «ادام" +
                "ه» کلیک کنید.";
            this.lblDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lnkMoreInfo
            // 
            this.lnkMoreInfo.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.lnkMoreInfo.AutoSize = true;
            this.lnkMoreInfo.Location = new Point(382, 284);
            this.lnkMoreInfo.Name = "lnkMoreInfo";
            this.lnkMoreInfo.Size = new Size(78, 13);
            this.lnkMoreInfo.TabIndex = 7;
            this.lnkMoreInfo.TabStop = true;
            this.lnkMoreInfo.Text = "توضیحات بیشتر";
            this.lnkMoreInfo.Visible = false;
            this.lnkMoreInfo.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnkMoreInfo_LinkClicked);
            // 
            // WSSelectList
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
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
        private Label lblDesc;
        private LinkLabel lnkMoreInfo;

    }
}