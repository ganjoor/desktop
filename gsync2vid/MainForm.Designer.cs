namespace gsync2vid
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.grpConnection = new System.Windows.Forms.GroupBox();
            this.lblDbComment = new System.Windows.Forms.Label();
            this.btnSelDb = new System.Windows.Forms.Button();
            this.txtSrcDb = new System.Windows.Forms.TextBox();
            this.lblSrcDb = new System.Windows.Forms.Label();
            this.grpPoem = new System.Windows.Forms.GroupBox();
            this.txtSyncId = new System.Windows.Forms.TextBox();
            this.lblSync = new System.Windows.Forms.Label();
            this.txtPoemId = new System.Windows.Forms.TextBox();
            this.btnSelPoem = new System.Windows.Forms.Button();
            this.txtPoem = new System.Windows.Forms.TextBox();
            this.lblPoem = new System.Windows.Forms.Label();
            this.grpVerses = new System.Windows.Forms.GroupBox();
            this.btnPreview = new System.Windows.Forms.Button();
            this.pbxPreview = new System.Windows.Forms.PictureBox();
            this.txtFont = new System.Windows.Forms.TextBox();
            this.btnSelectFont = new System.Windows.Forms.Button();
            this.lblFont = new System.Windows.Forms.Label();
            this.trckAlpha = new System.Windows.Forms.TrackBar();
            this.lblTextBackAlpha = new System.Windows.Forms.Label();
            this.lblTextBackColor = new System.Windows.Forms.Label();
            this.btnTextColor = new System.Windows.Forms.Button();
            this.lblTextColor = new System.Windows.Forms.Label();
            this.btnTextBackColor = new System.Windows.Forms.Button();
            this.btnBackColor = new System.Windows.Forms.Button();
            this.lblBackColor = new System.Windows.Forms.Label();
            this.btnSelBackgroundImage = new System.Windows.Forms.Button();
            this.txtBackgroundImage = new System.Windows.Forms.TextBox();
            this.lblBackgroundImage = new System.Windows.Forms.Label();
            this.txtDurationInMiliseconds = new System.Windows.Forms.TextBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.chkAudioBound = new System.Windows.Forms.CheckBox();
            this.cmbVerses = new System.Windows.Forms.ComboBox();
            this.lblWidth = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.NumericUpDown();
            this.txtHeight = new System.Windows.Forms.NumericUpDown();
            this.lblHeight = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.trckHPosition = new System.Windows.Forms.TrackBar();
            this.trckMaxTextWidth = new System.Windows.Forms.TrackBar();
            this.lblMaxTextWidth = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.grpConnection.SuspendLayout();
            this.grpPoem.SuspendLayout();
            this.grpVerses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trckAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trckHPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trckMaxTextWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // grpConnection
            // 
            this.grpConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpConnection.Controls.Add(this.lblDbComment);
            this.grpConnection.Controls.Add(this.btnSelDb);
            this.grpConnection.Controls.Add(this.txtSrcDb);
            this.grpConnection.Controls.Add(this.lblSrcDb);
            this.grpConnection.Location = new System.Drawing.Point(8, 12);
            this.grpConnection.Name = "grpConnection";
            this.grpConnection.Size = new System.Drawing.Size(842, 76);
            this.grpConnection.TabIndex = 0;
            this.grpConnection.TabStop = false;
            this.grpConnection.Text = "فایل پایگاه داده‌های گنجور رومیزی:";
            // 
            // lblDbComment
            // 
            this.lblDbComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDbComment.Location = new System.Drawing.Point(41, 48);
            this.lblDbComment.Name = "lblDbComment";
            this.lblDbComment.Size = new System.Drawing.Size(753, 17);
            this.lblDbComment.TabIndex = 3;
            this.lblDbComment.Text = "فایل در مسیر وجود ندارد یا امکان اتصال به آن وجود ندارد. لطفا اطمینان حاصل کنید گ" +
    "نجور رومیزی نصب شده و مسیر فایل درست انتخاب شده است.";
            // 
            // btnSelDb
            // 
            this.btnSelDb.Location = new System.Drawing.Point(7, 22);
            this.btnSelDb.Name = "btnSelDb";
            this.btnSelDb.Size = new System.Drawing.Size(29, 23);
            this.btnSelDb.TabIndex = 2;
            this.btnSelDb.Text = "...";
            this.btnSelDb.UseVisualStyleBackColor = true;
            this.btnSelDb.Click += new System.EventHandler(this.btnSelDb_Click);
            // 
            // txtSrcDb
            // 
            this.txtSrcDb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSrcDb.Location = new System.Drawing.Point(41, 24);
            this.txtSrcDb.Name = "txtSrcDb";
            this.txtSrcDb.ReadOnly = true;
            this.txtSrcDb.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSrcDb.Size = new System.Drawing.Size(750, 21);
            this.txtSrcDb.TabIndex = 1;
            // 
            // lblSrcDb
            // 
            this.lblSrcDb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSrcDb.AutoSize = true;
            this.lblSrcDb.Location = new System.Drawing.Point(797, 27);
            this.lblSrcDb.Name = "lblSrcDb";
            this.lblSrcDb.Size = new System.Drawing.Size(38, 13);
            this.lblSrcDb.TabIndex = 0;
            this.lblSrcDb.Text = "مسیر:";
            // 
            // grpPoem
            // 
            this.grpPoem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPoem.Controls.Add(this.btnReset);
            this.grpPoem.Controls.Add(this.txtHeight);
            this.grpPoem.Controls.Add(this.lblHeight);
            this.grpPoem.Controls.Add(this.txtWidth);
            this.grpPoem.Controls.Add(this.lblWidth);
            this.grpPoem.Controls.Add(this.txtSyncId);
            this.grpPoem.Controls.Add(this.lblSync);
            this.grpPoem.Controls.Add(this.txtPoemId);
            this.grpPoem.Controls.Add(this.btnSelPoem);
            this.grpPoem.Controls.Add(this.txtPoem);
            this.grpPoem.Controls.Add(this.lblPoem);
            this.grpPoem.Location = new System.Drawing.Point(7, 98);
            this.grpPoem.Name = "grpPoem";
            this.grpPoem.Size = new System.Drawing.Size(842, 77);
            this.grpPoem.TabIndex = 1;
            this.grpPoem.TabStop = false;
            this.grpPoem.Text = "شعر، خوانش و مشخصات کلی:";
            // 
            // txtSyncId
            // 
            this.txtSyncId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSyncId.Location = new System.Drawing.Point(465, 49);
            this.txtSyncId.Name = "txtSyncId";
            this.txtSyncId.ReadOnly = true;
            this.txtSyncId.Size = new System.Drawing.Size(100, 21);
            this.txtSyncId.TabIndex = 5;
            // 
            // lblSync
            // 
            this.lblSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSync.AutoSize = true;
            this.lblSync.Location = new System.Drawing.Point(571, 51);
            this.lblSync.Name = "lblSync";
            this.lblSync.Size = new System.Drawing.Size(57, 13);
            this.lblSync.TabIndex = 4;
            this.lblSync.Text = "کد خوانش:";
            // 
            // txtPoemId
            // 
            this.txtPoemId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPoemId.Location = new System.Drawing.Point(635, 49);
            this.txtPoemId.Name = "txtPoemId";
            this.txtPoemId.ReadOnly = true;
            this.txtPoemId.Size = new System.Drawing.Size(100, 21);
            this.txtPoemId.TabIndex = 3;
            // 
            // btnSelPoem
            // 
            this.btnSelPoem.Location = new System.Drawing.Point(7, 22);
            this.btnSelPoem.Name = "btnSelPoem";
            this.btnSelPoem.Size = new System.Drawing.Size(29, 23);
            this.btnSelPoem.TabIndex = 2;
            this.btnSelPoem.Text = "...";
            this.btnSelPoem.UseVisualStyleBackColor = true;
            this.btnSelPoem.Click += new System.EventHandler(this.btnSelPoem_Click);
            // 
            // txtPoem
            // 
            this.txtPoem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPoem.Location = new System.Drawing.Point(41, 24);
            this.txtPoem.Name = "txtPoem";
            this.txtPoem.ReadOnly = true;
            this.txtPoem.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPoem.Size = new System.Drawing.Size(750, 21);
            this.txtPoem.TabIndex = 1;
            // 
            // lblPoem
            // 
            this.lblPoem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPoem.AutoSize = true;
            this.lblPoem.Location = new System.Drawing.Point(741, 51);
            this.lblPoem.Name = "lblPoem";
            this.lblPoem.Size = new System.Drawing.Size(47, 13);
            this.lblPoem.TabIndex = 0;
            this.lblPoem.Text = "کد شعر:";
            // 
            // grpVerses
            // 
            this.grpVerses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpVerses.Controls.Add(this.txtComment);
            this.grpVerses.Controls.Add(this.trckMaxTextWidth);
            this.grpVerses.Controls.Add(this.lblMaxTextWidth);
            this.grpVerses.Controls.Add(this.trckHPosition);
            this.grpVerses.Controls.Add(this.btnPreview);
            this.grpVerses.Controls.Add(this.pbxPreview);
            this.grpVerses.Controls.Add(this.txtFont);
            this.grpVerses.Controls.Add(this.btnSelectFont);
            this.grpVerses.Controls.Add(this.lblFont);
            this.grpVerses.Controls.Add(this.trckAlpha);
            this.grpVerses.Controls.Add(this.lblTextBackAlpha);
            this.grpVerses.Controls.Add(this.lblTextBackColor);
            this.grpVerses.Controls.Add(this.btnTextColor);
            this.grpVerses.Controls.Add(this.lblTextColor);
            this.grpVerses.Controls.Add(this.btnTextBackColor);
            this.grpVerses.Controls.Add(this.btnBackColor);
            this.grpVerses.Controls.Add(this.lblBackColor);
            this.grpVerses.Controls.Add(this.btnSelBackgroundImage);
            this.grpVerses.Controls.Add(this.txtBackgroundImage);
            this.grpVerses.Controls.Add(this.lblBackgroundImage);
            this.grpVerses.Controls.Add(this.txtDurationInMiliseconds);
            this.grpVerses.Controls.Add(this.lblDuration);
            this.grpVerses.Controls.Add(this.chkAudioBound);
            this.grpVerses.Controls.Add(this.cmbVerses);
            this.grpVerses.Location = new System.Drawing.Point(7, 181);
            this.grpVerses.Name = "grpVerses";
            this.grpVerses.Size = new System.Drawing.Size(842, 426);
            this.grpVerses.TabIndex = 2;
            this.grpVerses.TabStop = false;
            this.grpVerses.Text = "قابها:";
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(2, 20);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 23);
            this.btnPreview.TabIndex = 34;
            this.btnPreview.Text = "پیش‌نمایش";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // pbxPreview
            // 
            this.pbxPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxPreview.Location = new System.Drawing.Point(7, 131);
            this.pbxPreview.Name = "pbxPreview";
            this.pbxPreview.Size = new System.Drawing.Size(300, 225);
            this.pbxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxPreview.TabIndex = 33;
            this.pbxPreview.TabStop = false;
            this.pbxPreview.Click += new System.EventHandler(this.pbxPreview_Click);
            // 
            // txtFont
            // 
            this.txtFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFont.Location = new System.Drawing.Point(40, 102);
            this.txtFont.Name = "txtFont";
            this.txtFont.ReadOnly = true;
            this.txtFont.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFont.Size = new System.Drawing.Size(664, 21);
            this.txtFont.TabIndex = 32;
            // 
            // btnSelectFont
            // 
            this.btnSelectFont.Location = new System.Drawing.Point(7, 102);
            this.btnSelectFont.Name = "btnSelectFont";
            this.btnSelectFont.Size = new System.Drawing.Size(29, 23);
            this.btnSelectFont.TabIndex = 31;
            this.btnSelectFont.Text = "...";
            this.btnSelectFont.UseVisualStyleBackColor = true;
            this.btnSelectFont.Click += new System.EventHandler(this.btnSelectFont_Click);
            // 
            // lblFont
            // 
            this.lblFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFont.AutoSize = true;
            this.lblFont.Location = new System.Drawing.Point(760, 107);
            this.lblFont.Name = "lblFont";
            this.lblFont.Size = new System.Drawing.Size(28, 13);
            this.lblFont.TabIndex = 29;
            this.lblFont.Text = "قلم:";
            // 
            // trckAlpha
            // 
            this.trckAlpha.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trckAlpha.LargeChange = 15;
            this.trckAlpha.Location = new System.Drawing.Point(406, 160);
            this.trckAlpha.Maximum = 255;
            this.trckAlpha.Name = "trckAlpha";
            this.trckAlpha.RightToLeftLayout = true;
            this.trckAlpha.Size = new System.Drawing.Size(253, 45);
            this.trckAlpha.TabIndex = 28;
            this.trckAlpha.TickFrequency = 15;
            this.trckAlpha.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trckAlpha.Scroll += new System.EventHandler(this.trckAlpha_Scroll);
            // 
            // lblTextBackAlpha
            // 
            this.lblTextBackAlpha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTextBackAlpha.AutoSize = true;
            this.lblTextBackAlpha.Location = new System.Drawing.Point(668, 171);
            this.lblTextBackAlpha.Name = "lblTextBackAlpha";
            this.lblTextBackAlpha.Size = new System.Drawing.Size(119, 13);
            this.lblTextBackAlpha.TabIndex = 27;
            this.lblTextBackAlpha.Text = "شفافیت رنگ زمینه متن:";
            // 
            // lblTextBackColor
            // 
            this.lblTextBackColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTextBackColor.AutoSize = true;
            this.lblTextBackColor.Location = new System.Drawing.Point(455, 135);
            this.lblTextBackColor.Name = "lblTextBackColor";
            this.lblTextBackColor.Size = new System.Drawing.Size(77, 13);
            this.lblTextBackColor.TabIndex = 25;
            this.lblTextBackColor.Text = "رنگ زمینه متن:";
            // 
            // btnTextColor
            // 
            this.btnTextColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTextColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTextColor.Location = new System.Drawing.Point(564, 131);
            this.btnTextColor.Name = "btnTextColor";
            this.btnTextColor.Size = new System.Drawing.Size(28, 23);
            this.btnTextColor.TabIndex = 26;
            this.btnTextColor.UseVisualStyleBackColor = true;
            this.btnTextColor.Click += new System.EventHandler(this.btnTextColor_Click);
            // 
            // lblTextColor
            // 
            this.lblTextColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTextColor.AutoSize = true;
            this.lblTextColor.Location = new System.Drawing.Point(598, 135);
            this.lblTextColor.Name = "lblTextColor";
            this.lblTextColor.Size = new System.Drawing.Size(50, 13);
            this.lblTextColor.TabIndex = 23;
            this.lblTextColor.Text = "رنگ متن:";
            // 
            // btnTextBackColor
            // 
            this.btnTextBackColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTextBackColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTextBackColor.Location = new System.Drawing.Point(406, 131);
            this.btnTextBackColor.Name = "btnTextBackColor";
            this.btnTextBackColor.Size = new System.Drawing.Size(28, 23);
            this.btnTextBackColor.TabIndex = 24;
            this.btnTextBackColor.UseVisualStyleBackColor = true;
            this.btnTextBackColor.Click += new System.EventHandler(this.btnTextBackColor_Click);
            // 
            // btnBackColor
            // 
            this.btnBackColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBackColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBackColor.Location = new System.Drawing.Point(670, 131);
            this.btnBackColor.Name = "btnBackColor";
            this.btnBackColor.Size = new System.Drawing.Size(28, 23);
            this.btnBackColor.TabIndex = 22;
            this.btnBackColor.UseVisualStyleBackColor = true;
            this.btnBackColor.Click += new System.EventHandler(this.btnBackColor_Click);
            // 
            // lblBackColor
            // 
            this.lblBackColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBackColor.AutoSize = true;
            this.lblBackColor.Location = new System.Drawing.Point(731, 135);
            this.lblBackColor.Name = "lblBackColor";
            this.lblBackColor.Size = new System.Drawing.Size(56, 13);
            this.lblBackColor.TabIndex = 21;
            this.lblBackColor.Text = "رنگ زمینه:";
            // 
            // btnSelBackgroundImage
            // 
            this.btnSelBackgroundImage.Location = new System.Drawing.Point(7, 73);
            this.btnSelBackgroundImage.Name = "btnSelBackgroundImage";
            this.btnSelBackgroundImage.Size = new System.Drawing.Size(29, 23);
            this.btnSelBackgroundImage.TabIndex = 8;
            this.btnSelBackgroundImage.Text = "...";
            this.btnSelBackgroundImage.UseVisualStyleBackColor = true;
            this.btnSelBackgroundImage.Click += new System.EventHandler(this.btnSelBackgroundImage_Click);
            // 
            // txtBackgroundImage
            // 
            this.txtBackgroundImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBackgroundImage.Location = new System.Drawing.Point(41, 75);
            this.txtBackgroundImage.Name = "txtBackgroundImage";
            this.txtBackgroundImage.ReadOnly = true;
            this.txtBackgroundImage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtBackgroundImage.Size = new System.Drawing.Size(664, 21);
            this.txtBackgroundImage.TabIndex = 7;
            // 
            // lblBackgroundImage
            // 
            this.lblBackgroundImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBackgroundImage.AutoSize = true;
            this.lblBackgroundImage.Location = new System.Drawing.Point(730, 78);
            this.lblBackgroundImage.Name = "lblBackgroundImage";
            this.lblBackgroundImage.Size = new System.Drawing.Size(63, 13);
            this.lblBackgroundImage.TabIndex = 6;
            this.lblBackgroundImage.Text = "تصویر زمینه:";
            // 
            // txtDurationInMiliseconds
            // 
            this.txtDurationInMiliseconds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDurationInMiliseconds.Location = new System.Drawing.Point(389, 45);
            this.txtDurationInMiliseconds.Name = "txtDurationInMiliseconds";
            this.txtDurationInMiliseconds.Size = new System.Drawing.Size(100, 21);
            this.txtDurationInMiliseconds.TabIndex = 5;
            // 
            // lblDuration
            // 
            this.lblDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(495, 48);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(124, 13);
            this.lblDuration.TabIndex = 4;
            this.lblDuration.Text = "زمان بر حسب میلی ثانیه:";
            // 
            // chkAudioBound
            // 
            this.chkAudioBound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAudioBound.AutoSize = true;
            this.chkAudioBound.Enabled = false;
            this.chkAudioBound.Location = new System.Drawing.Point(692, 47);
            this.chkAudioBound.Name = "chkAudioBound";
            this.chkAudioBound.Size = new System.Drawing.Size(99, 17);
            this.chkAudioBound.TabIndex = 1;
            this.chkAudioBound.Text = "مربوط به خوانش";
            this.chkAudioBound.UseVisualStyleBackColor = true;
            // 
            // cmbVerses
            // 
            this.cmbVerses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbVerses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVerses.FormattingEnabled = true;
            this.cmbVerses.Location = new System.Drawing.Point(83, 20);
            this.cmbVerses.Name = "cmbVerses";
            this.cmbVerses.Size = new System.Drawing.Size(708, 21);
            this.cmbVerses.TabIndex = 0;
            this.cmbVerses.SelectedIndexChanged += new System.EventHandler(this.cmbVerses_SelectedIndexChanged);
            // 
            // lblWidth
            // 
            this.lblWidth.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(288, 52);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(74, 13);
            this.lblWidth.TabIndex = 6;
            this.lblWidth.Text = "عرض خروجی:";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(223, 49);
            this.txtWidth.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.txtWidth.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(59, 21);
            this.txtWidth.TabIndex = 8;
            this.txtWidth.Value = new decimal(new int[] {
            960,
            0,
            0,
            0});
            this.txtWidth.ValueChanged += new System.EventHandler(this.txtWidth_ValueChanged);
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(78, 50);
            this.txtHeight.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.txtHeight.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(59, 21);
            this.txtHeight.TabIndex = 10;
            this.txtHeight.Value = new decimal(new int[] {
            720,
            0,
            0,
            0});
            this.txtHeight.ValueChanged += new System.EventHandler(this.txtHeight_ValueChanged);
            // 
            // lblHeight
            // 
            this.lblHeight.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(143, 53);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(68, 13);
            this.lblHeight.TabIndex = 9;
            this.lblHeight.Text = "طول خروجی:";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(7, 49);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(70, 23);
            this.btnReset.TabIndex = 11;
            this.btnReset.Text = "پیش‌فرض";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // trckHPosition
            // 
            this.trckHPosition.LargeChange = 15;
            this.trckHPosition.Location = new System.Drawing.Point(313, 129);
            this.trckHPosition.Maximum = 20;
            this.trckHPosition.Name = "trckHPosition";
            this.trckHPosition.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trckHPosition.Size = new System.Drawing.Size(45, 227);
            this.trckHPosition.TabIndex = 36;
            this.trckHPosition.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trckHPosition.Value = 10;
            this.trckHPosition.Scroll += new System.EventHandler(this.trckHPosition_Scroll);
            // 
            // trckMaxTextWidth
            // 
            this.trckMaxTextWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trckMaxTextWidth.LargeChange = 15;
            this.trckMaxTextWidth.Location = new System.Drawing.Point(406, 211);
            this.trckMaxTextWidth.Maximum = 255;
            this.trckMaxTextWidth.Name = "trckMaxTextWidth";
            this.trckMaxTextWidth.RightToLeftLayout = true;
            this.trckMaxTextWidth.Size = new System.Drawing.Size(253, 45);
            this.trckMaxTextWidth.TabIndex = 38;
            this.trckMaxTextWidth.TickFrequency = 15;
            this.trckMaxTextWidth.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trckMaxTextWidth.Scroll += new System.EventHandler(this.trckMaxTextWidth_Scroll);
            // 
            // lblMaxTextWidth
            // 
            this.lblMaxTextWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMaxTextWidth.AutoSize = true;
            this.lblMaxTextWidth.Location = new System.Drawing.Point(697, 222);
            this.lblMaxTextWidth.Name = "lblMaxTextWidth";
            this.lblMaxTextWidth.Size = new System.Drawing.Size(90, 13);
            this.lblMaxTextWidth.TabIndex = 37;
            this.lblMaxTextWidth.Text = "حداکثر عرض متن:";
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtComment.Location = new System.Drawing.Point(406, 259);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ReadOnly = true;
            this.txtComment.Size = new System.Drawing.Size(379, 62);
            this.txtComment.TabIndex = 39;
            this.txtComment.Text = resources.GetString("txtComment.Text");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 619);
            this.Controls.Add(this.grpVerses);
            this.Controls.Add(this.grpPoem);
            this.Controls.Add(this.grpConnection);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تولید فیلم از خوانش شعر گنجور رومیزی";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.grpConnection.ResumeLayout(false);
            this.grpConnection.PerformLayout();
            this.grpPoem.ResumeLayout(false);
            this.grpPoem.PerformLayout();
            this.grpVerses.ResumeLayout(false);
            this.grpVerses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trckAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trckHPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trckMaxTextWidth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpConnection;
        private System.Windows.Forms.Button btnSelDb;
        private System.Windows.Forms.TextBox txtSrcDb;
        private System.Windows.Forms.Label lblSrcDb;
        private System.Windows.Forms.Label lblDbComment;
        private System.Windows.Forms.GroupBox grpPoem;
        private System.Windows.Forms.Button btnSelPoem;
        private System.Windows.Forms.TextBox txtPoem;
        private System.Windows.Forms.Label lblPoem;
        private System.Windows.Forms.TextBox txtPoemId;
        private System.Windows.Forms.TextBox txtSyncId;
        private System.Windows.Forms.Label lblSync;
        private System.Windows.Forms.GroupBox grpVerses;
        private System.Windows.Forms.ComboBox cmbVerses;
        private System.Windows.Forms.TextBox txtDurationInMiliseconds;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.CheckBox chkAudioBound;
        private System.Windows.Forms.Button btnSelBackgroundImage;
        private System.Windows.Forms.TextBox txtBackgroundImage;
        private System.Windows.Forms.Label lblBackgroundImage;
        private System.Windows.Forms.Label lblTextBackColor;
        private System.Windows.Forms.Button btnTextColor;
        private System.Windows.Forms.Label lblTextColor;
        private System.Windows.Forms.Button btnTextBackColor;
        private System.Windows.Forms.Button btnBackColor;
        private System.Windows.Forms.Label lblBackColor;
        private System.Windows.Forms.TrackBar trckAlpha;
        private System.Windows.Forms.Label lblTextBackAlpha;
        private System.Windows.Forms.TextBox txtFont;
        private System.Windows.Forms.Button btnSelectFont;
        private System.Windows.Forms.Label lblFont;
        private System.Windows.Forms.PictureBox pbxPreview;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.NumericUpDown txtHeight;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.NumericUpDown txtWidth;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TrackBar trckHPosition;
        private System.Windows.Forms.TrackBar trckMaxTextWidth;
        private System.Windows.Forms.Label lblMaxTextWidth;
        private System.Windows.Forms.TextBox txtComment;
    }
}

