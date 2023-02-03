namespace ganjoor
{
    partial class AboutForm
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
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.lblAppVersion = new System.Windows.Forms.Label();
            this.lnkGanjoorOnSFNet = new System.Windows.Forms.LinkLabel();
            this.btnOK = new System.Windows.Forms.Button();
            this.lnkIcons = new System.Windows.Forms.LinkLabel();
            this.lnkHamidReza = new System.Windows.Forms.LinkLabel();
            this.lnkSources = new System.Windows.Forms.LinkLabel();
            this.lnkIconsEditor = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.grdContributers = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.lnkBlog = new System.Windows.Forms.LinkLabel();
            this.lnkEditor = new System.Windows.Forms.LinkLabel();
            this.lblIcon = new System.Windows.Forms.Label();
            this.lblImanAbidi = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.grdContributers)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAppTitle
            // 
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Location = new System.Drawing.Point(75, 11);
            this.lblAppTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(273, 13);
            this.lblAppTitle.TabIndex = 0;
            this.lblAppTitle.Text = "گنجور رومیزی (نرم‌افزار رایگان و بازمتن مرور اشعار فارسی)";
            // 
            // lblAppVersion
            // 
            this.lblAppVersion.AutoSize = true;
            this.lblAppVersion.Location = new System.Drawing.Point(180, 30);
            this.lblAppVersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAppVersion.Name = "lblAppVersion";
            this.lblAppVersion.Size = new System.Drawing.Size(59, 13);
            this.lblAppVersion.TabIndex = 1;
            this.lblAppVersion.Text = "ویرایش 0.0";
            // 
            // lnkGanjoorOnSFNet
            // 
            this.lnkGanjoorOnSFNet.AutoSize = true;
            this.lnkGanjoorOnSFNet.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkGanjoorOnSFNet.Location = new System.Drawing.Point(171, 50);
            this.lnkGanjoorOnSFNet.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkGanjoorOnSFNet.Name = "lnkGanjoorOnSFNet";
            this.lnkGanjoorOnSFNet.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lnkGanjoorOnSFNet.Size = new System.Drawing.Size(80, 13);
            this.lnkGanjoorOnSFNet.TabIndex = 2;
            this.lnkGanjoorOnSFNet.TabStop = true;
            this.lnkGanjoorOnSFNet.Text = "dg.ganjoor.net";
            this.lnkGanjoorOnSFNet.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkGanjoorOnSFNet_LinkClicked);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(179, 434);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(60, 22);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "تأیید";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // lnkIcons
            // 
            this.lnkIcons.AutoSize = true;
            this.lnkIcons.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkIcons.Location = new System.Drawing.Point(160, 106);
            this.lnkIcons.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkIcons.Name = "lnkIcons";
            this.lnkIcons.Size = new System.Drawing.Size(98, 13);
            this.lnkIcons.TabIndex = 4;
            this.lnkIcons.TabStop = true;
            this.lnkIcons.Text = "منبع آیکونهای برنامه";
            this.lnkIcons.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkIcons_LinkClicked);
            // 
            // lnkHamidReza
            // 
            this.lnkHamidReza.AutoSize = true;
            this.lnkHamidReza.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkHamidReza.Location = new System.Drawing.Point(166, 69);
            this.lnkHamidReza.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkHamidReza.Name = "lnkHamidReza";
            this.lnkHamidReza.Size = new System.Drawing.Size(86, 13);
            this.lnkHamidReza.TabIndex = 3;
            this.lnkHamidReza.TabStop = true;
            this.lnkHamidReza.Text = "حمیدرضا محمدی";
            this.lnkHamidReza.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHamidReza_LinkClicked);
            // 
            // lnkSources
            // 
            this.lnkSources.AutoSize = true;
            this.lnkSources.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkSources.Location = new System.Drawing.Point(149, 146);
            this.lnkSources.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkSources.Name = "lnkSources";
            this.lnkSources.Size = new System.Drawing.Size(120, 13);
            this.lnkSources.TabIndex = 6;
            this.lnkSources.TabStop = true;
            this.lnkSources.Text = "منابع متون و اشعار گنجور";
            this.lnkSources.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSources_LinkClicked);
            // 
            // lnkIconsEditor
            // 
            this.lnkIconsEditor.AutoSize = true;
            this.lnkIconsEditor.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkIconsEditor.Location = new System.Drawing.Point(139, 126);
            this.lnkIconsEditor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkIconsEditor.Name = "lnkIconsEditor";
            this.lnkIconsEditor.Size = new System.Drawing.Size(141, 13);
            this.lnkIconsEditor.TabIndex = 5;
            this.lnkIconsEditor.TabStop = true;
            this.lnkIconsEditor.Text = "منبع آیکونهای بخش ویرایشگر";
            this.lnkIconsEditor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkIconsEditor_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 201);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(333, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "دوستانی که با تایپ آثار شاعران به گسترش گنجور رومیزی کمک کرده‌اند:";
            // 
            // grdContributers
            // 
            this.grdContributers.AllowUserToAddRows = false;
            this.grdContributers.AllowUserToDeleteRows = false;
            this.grdContributers.AllowUserToOrderColumns = true;
            this.grdContributers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdContributers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.grdContributers.Location = new System.Drawing.Point(12, 221);
            this.grdContributers.Name = "grdContributers";
            this.grdContributers.ReadOnly = true;
            this.grdContributers.RowHeadersWidth = 11;
            this.grdContributers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdContributers.Size = new System.Drawing.Size(395, 186);
            this.grdContributers.TabIndex = 9;
            this.grdContributers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdContributers_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "نام";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.Width = 110;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "بخش";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column3.HeaderText = "توضیح بیشتر";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 75;
            // 
            // lnkBlog
            // 
            this.lnkBlog.AutoSize = true;
            this.lnkBlog.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkBlog.Location = new System.Drawing.Point(176, 166);
            this.lnkBlog.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkBlog.Name = "lnkBlog";
            this.lnkBlog.Size = new System.Drawing.Size(71, 13);
            this.lnkBlog.TabIndex = 7;
            this.lnkBlog.TabStop = true;
            this.lnkBlog.Text = "تازه‌های گنجور";
            this.lnkBlog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkBlog_LinkClicked);
            // 
            // lnkEditor
            // 
            this.lnkEditor.AutoSize = true;
            this.lnkEditor.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkEditor.Location = new System.Drawing.Point(147, 414);
            this.lnkEditor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkEditor.Name = "lnkEditor";
            this.lnkEditor.Size = new System.Drawing.Size(121, 13);
            this.lnkEditor.TabIndex = 10;
            this.lnkEditor.TabStop = true;
            this.lnkEditor.Text = "چگونه اشعار را تایپ کنم؟";
            this.lnkEditor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEditor_LinkClicked);
            // 
            // lblIcon
            // 
            this.lblIcon.AutoSize = true;
            this.lblIcon.Location = new System.Drawing.Point(79, 184);
            this.lblIcon.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(257, 13);
            this.lblIcon.TabIndex = 11;
            this.lblIcon.Text = "طراحی آیکونهای برنامه: یوسف زمانی، ارسلان سفیدگر";
            // 
            // lblImanAbidi
            // 
            this.lblImanAbidi.AutoSize = true;
            this.lblImanAbidi.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lblImanAbidi.Location = new System.Drawing.Point(139, 88);
            this.lblImanAbidi.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblImanAbidi.Name = "lblImanAbidi";
            this.lblImanAbidi.Size = new System.Drawing.Size(144, 13);
            this.lblImanAbidi.TabIndex = 12;
            this.lblImanAbidi.TabStop = true;
            this.lblImanAbidi.Text = "به کمک ایمان عبیدی آشتیانی";
            this.lblImanAbidi.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblImanAbidi_LinkClicked);
            // 
            // AboutForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(419, 462);
            this.Controls.Add(this.lblImanAbidi);
            this.Controls.Add(this.lblIcon);
            this.Controls.Add(this.lnkEditor);
            this.Controls.Add(this.lnkBlog);
            this.Controls.Add(this.grdContributers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lnkIconsEditor);
            this.Controls.Add(this.lnkSources);
            this.Controls.Add(this.lnkHamidReza);
            this.Controls.Add(this.lnkIcons);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lnkGanjoorOnSFNet);
            this.Controls.Add(this.lblAppVersion);
            this.Controls.Add(this.lblAppTitle);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "دربارهٔ گنجور رومیزی";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdContributers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.Label lblAppVersion;
        private System.Windows.Forms.LinkLabel lnkGanjoorOnSFNet;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.LinkLabel lnkIcons;
        private System.Windows.Forms.LinkLabel lnkHamidReza;
        private System.Windows.Forms.LinkLabel lnkSources;
        private System.Windows.Forms.LinkLabel lnkIconsEditor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView grdContributers;
        private System.Windows.Forms.LinkLabel lnkBlog;
        private System.Windows.Forms.LinkLabel lnkEditor;
        private System.Windows.Forms.DataGridViewLinkColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewLinkColumn Column3;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.LinkLabel lblImanAbidi;
    }
}