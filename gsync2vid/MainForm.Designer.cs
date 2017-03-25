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
            this.btnApply = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.txtHeight = new System.Windows.Forms.NumericUpDown();
            this.lblHeight = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.NumericUpDown();
            this.lblWidth = new System.Windows.Forms.Label();
            this.txtSyncId = new System.Windows.Forms.TextBox();
            this.lblSync = new System.Windows.Forms.Label();
            this.txtPoemId = new System.Windows.Forms.TextBox();
            this.btnSelPoem = new System.Windows.Forms.Button();
            this.txtPoem = new System.Windows.Forms.TextBox();
            this.lblPoem = new System.Windows.Forms.Label();
            this.grpVerses = new System.Windows.Forms.GroupBox();
            this.btnPairNext = new System.Windows.Forms.Button();
            this.btnBorderColor = new System.Windows.Forms.Button();
            this.lblBorderColor = new System.Windows.Forms.Label();
            this.txtThickness = new System.Windows.Forms.NumericUpDown();
            this.lblThickness = new System.Windows.Forms.Label();
            this.trckHPosition = new System.Windows.Forms.TrackBar();
            this.btnProperties = new System.Windows.Forms.Button();
            this.btnRandomImage = new System.Windows.Forms.Button();
            this.chkShowLogo = new System.Windows.Forms.CheckBox();
            this.btnEditText = new System.Windows.Forms.Button();
            this.lblMinus = new System.Windows.Forms.Label();
            this.chkSlaveFrame = new System.Windows.Forms.CheckBox();
            this.txtStartInMiliseconds = new System.Windows.Forms.NumericUpDown();
            this.btnResetImage = new System.Windows.Forms.Button();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.trckMaxTextWidth = new System.Windows.Forms.TrackBar();
            this.lblMaxTextWidth = new System.Windows.Forms.Label();
            this.trckVPosition = new System.Windows.Forms.TrackBar();
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
            this.lblStart = new System.Windows.Forms.Label();
            this.chkAudioBound = new System.Windows.Forms.CheckBox();
            this.cmbVerses = new System.Windows.Forms.ComboBox();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.prgrss = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnProduce = new System.Windows.Forms.Button();
            this.btnSubtitle = new System.Windows.Forms.Button();
            this.cmbTransitionEffect = new System.Windows.Forms.ComboBox();
            this.lblTransitionEffect = new System.Windows.Forms.Label();
            this.chkAAC = new System.Windows.Forms.CheckBox();
            this.grpConnection.SuspendLayout();
            this.grpPoem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth)).BeginInit();
            this.grpVerses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtThickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trckHPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartInMiliseconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trckMaxTextWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trckVPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trckAlpha)).BeginInit();
            this.statusStripMain.SuspendLayout();
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
            this.grpConnection.Location = new System.Drawing.Point(7, 12);
            this.grpConnection.Name = "grpConnection";
            this.grpConnection.Size = new System.Drawing.Size(1197, 108);
            this.grpConnection.TabIndex = 0;
            this.grpConnection.TabStop = false;
            this.grpConnection.Text = "فایل پایگاه داده‌های گنجور رومیزی:";
            // 
            // lblDbComment
            // 
            this.lblDbComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDbComment.Location = new System.Drawing.Point(41, 61);
            this.lblDbComment.Name = "lblDbComment";
            this.lblDbComment.Size = new System.Drawing.Size(1069, 29);
            this.lblDbComment.TabIndex = 3;
            this.lblDbComment.Text = "فایل در مسیر وجود ندارد یا امکان اتصال به آن وجود ندارد. لطفا اطمینان حاصل کنید گ" +
    "نجور رومیزی نصب شده و مسیر فایل درست انتخاب شده است.";
            // 
            // btnSelDb
            // 
            this.btnSelDb.Location = new System.Drawing.Point(7, 22);
            this.btnSelDb.Name = "btnSelDb";
            this.btnSelDb.Size = new System.Drawing.Size(45, 36);
            this.btnSelDb.TabIndex = 2;
            this.btnSelDb.Text = "...";
            this.btnSelDb.UseVisualStyleBackColor = true;
            this.btnSelDb.Click += new System.EventHandler(this.btnSelDb_Click);
            // 
            // txtSrcDb
            // 
            this.txtSrcDb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSrcDb.Location = new System.Drawing.Point(58, 24);
            this.txtSrcDb.Name = "txtSrcDb";
            this.txtSrcDb.ReadOnly = true;
            this.txtSrcDb.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSrcDb.Size = new System.Drawing.Size(1055, 27);
            this.txtSrcDb.TabIndex = 1;
            // 
            // lblSrcDb
            // 
            this.lblSrcDb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSrcDb.AutoSize = true;
            this.lblSrcDb.Location = new System.Drawing.Point(1125, 27);
            this.lblSrcDb.Name = "lblSrcDb";
            this.lblSrcDb.Size = new System.Drawing.Size(56, 21);
            this.lblSrcDb.TabIndex = 0;
            this.lblSrcDb.Text = "مسیر:";
            // 
            // grpPoem
            // 
            this.grpPoem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPoem.Controls.Add(this.btnApply);
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
            this.grpPoem.Location = new System.Drawing.Point(7, 131);
            this.grpPoem.Name = "grpPoem";
            this.grpPoem.Size = new System.Drawing.Size(1197, 105);
            this.grpPoem.TabIndex = 1;
            this.grpPoem.TabStop = false;
            this.grpPoem.Text = "شعر، خوانش و مشخصات کلی:";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(114, 57);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(77, 42);
            this.btnApply.TabIndex = 12;
            this.btnApply.Text = "اعمال";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(7, 57);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(103, 42);
            this.btnReset.TabIndex = 11;
            this.btnReset.Text = "پیش‌فرض";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(204, 64);
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
            this.txtHeight.Size = new System.Drawing.Size(59, 27);
            this.txtHeight.TabIndex = 10;
            this.txtHeight.Value = new decimal(new int[] {
            720,
            0,
            0,
            0});
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(269, 67);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(104, 21);
            this.lblHeight.TabIndex = 9;
            this.lblHeight.Text = "طول خروجی:";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(376, 63);
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
            this.txtWidth.Size = new System.Drawing.Size(59, 27);
            this.txtWidth.TabIndex = 8;
            this.txtWidth.Value = new decimal(new int[] {
            960,
            0,
            0,
            0});
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(441, 66);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(111, 21);
            this.lblWidth.TabIndex = 6;
            this.lblWidth.Text = "عرض خروجی:";
            // 
            // txtSyncId
            // 
            this.txtSyncId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSyncId.Location = new System.Drawing.Point(723, 63);
            this.txtSyncId.Name = "txtSyncId";
            this.txtSyncId.ReadOnly = true;
            this.txtSyncId.Size = new System.Drawing.Size(100, 27);
            this.txtSyncId.TabIndex = 5;
            // 
            // lblSync
            // 
            this.lblSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSync.AutoSize = true;
            this.lblSync.Location = new System.Drawing.Point(829, 65);
            this.lblSync.Name = "lblSync";
            this.lblSync.Size = new System.Drawing.Size(88, 21);
            this.lblSync.TabIndex = 4;
            this.lblSync.Text = "کد خوانش:";
            // 
            // txtPoemId
            // 
            this.txtPoemId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPoemId.Location = new System.Drawing.Point(931, 61);
            this.txtPoemId.Name = "txtPoemId";
            this.txtPoemId.ReadOnly = true;
            this.txtPoemId.Size = new System.Drawing.Size(100, 27);
            this.txtPoemId.TabIndex = 3;
            // 
            // btnSelPoem
            // 
            this.btnSelPoem.Location = new System.Drawing.Point(7, 22);
            this.btnSelPoem.Name = "btnSelPoem";
            this.btnSelPoem.Size = new System.Drawing.Size(45, 36);
            this.btnSelPoem.TabIndex = 2;
            this.btnSelPoem.Text = "...";
            this.btnSelPoem.UseVisualStyleBackColor = true;
            this.btnSelPoem.Click += new System.EventHandler(this.btnSelPoem_Click);
            // 
            // txtPoem
            // 
            this.txtPoem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPoem.Location = new System.Drawing.Point(58, 24);
            this.txtPoem.Name = "txtPoem";
            this.txtPoem.ReadOnly = true;
            this.txtPoem.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPoem.Size = new System.Drawing.Size(1055, 27);
            this.txtPoem.TabIndex = 1;
            // 
            // lblPoem
            // 
            this.lblPoem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPoem.AutoSize = true;
            this.lblPoem.Location = new System.Drawing.Point(1037, 63);
            this.lblPoem.Name = "lblPoem";
            this.lblPoem.Size = new System.Drawing.Size(70, 21);
            this.lblPoem.TabIndex = 0;
            this.lblPoem.Text = "کد شعر:";
            // 
            // grpVerses
            // 
            this.grpVerses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpVerses.Controls.Add(this.btnPairNext);
            this.grpVerses.Controls.Add(this.btnBorderColor);
            this.grpVerses.Controls.Add(this.lblBorderColor);
            this.grpVerses.Controls.Add(this.txtThickness);
            this.grpVerses.Controls.Add(this.lblThickness);
            this.grpVerses.Controls.Add(this.trckHPosition);
            this.grpVerses.Controls.Add(this.btnProperties);
            this.grpVerses.Controls.Add(this.btnRandomImage);
            this.grpVerses.Controls.Add(this.chkShowLogo);
            this.grpVerses.Controls.Add(this.btnEditText);
            this.grpVerses.Controls.Add(this.lblMinus);
            this.grpVerses.Controls.Add(this.chkSlaveFrame);
            this.grpVerses.Controls.Add(this.txtStartInMiliseconds);
            this.grpVerses.Controls.Add(this.btnResetImage);
            this.grpVerses.Controls.Add(this.txtComment);
            this.grpVerses.Controls.Add(this.trckMaxTextWidth);
            this.grpVerses.Controls.Add(this.lblMaxTextWidth);
            this.grpVerses.Controls.Add(this.trckVPosition);
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
            this.grpVerses.Controls.Add(this.lblStart);
            this.grpVerses.Controls.Add(this.chkAudioBound);
            this.grpVerses.Controls.Add(this.cmbVerses);
            this.grpVerses.Location = new System.Drawing.Point(7, 251);
            this.grpVerses.Name = "grpVerses";
            this.grpVerses.Size = new System.Drawing.Size(1197, 576);
            this.grpVerses.TabIndex = 2;
            this.grpVerses.TabStop = false;
            this.grpVerses.Text = "قابها:";
            // 
            // btnPairNext
            // 
            this.btnPairNext.Location = new System.Drawing.Point(7, 59);
            this.btnPairNext.Name = "btnPairNext";
            this.btnPairNext.Size = new System.Drawing.Size(34, 31);
            this.btnPairNext.TabIndex = 54;
            this.btnPairNext.Text = "O";
            this.btnPairNext.UseVisualStyleBackColor = true;
            this.btnPairNext.Click += new System.EventHandler(this.btnPairNext_Click);
            // 
            // btnBorderColor
            // 
            this.btnBorderColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBorderColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBorderColor.Location = new System.Drawing.Point(638, 341);
            this.btnBorderColor.Name = "btnBorderColor";
            this.btnBorderColor.Size = new System.Drawing.Size(45, 37);
            this.btnBorderColor.TabIndex = 53;
            this.btnBorderColor.UseVisualStyleBackColor = true;
            this.btnBorderColor.Click += new System.EventHandler(this.btnBorderColor_Click);
            // 
            // lblBorderColor
            // 
            this.lblBorderColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBorderColor.AutoSize = true;
            this.lblBorderColor.Location = new System.Drawing.Point(704, 349);
            this.lblBorderColor.Name = "lblBorderColor";
            this.lblBorderColor.Size = new System.Drawing.Size(70, 21);
            this.lblBorderColor.TabIndex = 52;
            this.lblBorderColor.Text = "رنگ مرز:";
            // 
            // txtThickness
            // 
            this.txtThickness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtThickness.Location = new System.Drawing.Point(894, 347);
            this.txtThickness.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.txtThickness.Name = "txtThickness";
            this.txtThickness.Size = new System.Drawing.Size(57, 27);
            this.txtThickness.TabIndex = 51;
            this.txtThickness.ValueChanged += new System.EventHandler(this.txtThickness_ValueChanged);
            // 
            // lblThickness
            // 
            this.lblThickness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblThickness.AutoSize = true;
            this.lblThickness.Location = new System.Drawing.Point(954, 349);
            this.lblThickness.Name = "lblThickness";
            this.lblThickness.Size = new System.Drawing.Size(192, 21);
            this.lblThickness.TabIndex = 50;
            this.lblThickness.Text = "ضخامت مرز محیطی متن:";
            // 
            // trckHPosition
            // 
            this.trckHPosition.LargeChange = 15;
            this.trckHPosition.Location = new System.Drawing.Point(7, 426);
            this.trckHPosition.Maximum = 20;
            this.trckHPosition.Name = "trckHPosition";
            this.trckHPosition.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trckHPosition.Size = new System.Drawing.Size(300, 69);
            this.trckHPosition.TabIndex = 48;
            this.trckHPosition.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trckHPosition.Value = 10;
            this.trckHPosition.Scroll += new System.EventHandler(this.trckHPosition_Scroll);
            // 
            // btnProperties
            // 
            this.btnProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProperties.Location = new System.Drawing.Point(880, 505);
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(262, 39);
            this.btnProperties.TabIndex = 47;
            this.btnProperties.Text = "تنظیم دستی ویژگیهای قاب";
            this.btnProperties.UseVisualStyleBackColor = true;
            this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click);
            // 
            // btnRandomImage
            // 
            this.btnRandomImage.Location = new System.Drawing.Point(101, 95);
            this.btnRandomImage.Name = "btnRandomImage";
            this.btnRandomImage.Size = new System.Drawing.Size(73, 37);
            this.btnRandomImage.TabIndex = 46;
            this.btnRandomImage.Text = "rnd";
            this.btnRandomImage.UseVisualStyleBackColor = true;
            this.btnRandomImage.Click += new System.EventHandler(this.btnRandomImage_Click);
            // 
            // chkShowLogo
            // 
            this.chkShowLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowLogo.AutoSize = true;
            this.chkShowLogo.Location = new System.Drawing.Point(965, 474);
            this.chkShowLogo.Name = "chkShowLogo";
            this.chkShowLogo.Size = new System.Drawing.Size(177, 25);
            this.chkShowLogo.TabIndex = 45;
            this.chkShowLogo.Text = "نمایش لوگوی گنجور";
            this.chkShowLogo.UseVisualStyleBackColor = true;
            this.chkShowLogo.CheckedChanged += new System.EventHandler(this.chkShowLogo_CheckedChanged);
            // 
            // btnEditText
            // 
            this.btnEditText.Location = new System.Drawing.Point(111, 20);
            this.btnEditText.Name = "btnEditText";
            this.btnEditText.Size = new System.Drawing.Size(37, 36);
            this.btnEditText.TabIndex = 44;
            this.btnEditText.Text = "T";
            this.btnEditText.UseVisualStyleBackColor = true;
            this.btnEditText.Click += new System.EventHandler(this.btnEditText_Click);
            // 
            // lblMinus
            // 
            this.lblMinus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMinus.AutoSize = true;
            this.lblMinus.Location = new System.Drawing.Point(648, 62);
            this.lblMinus.Name = "lblMinus";
            this.lblMinus.Size = new System.Drawing.Size(16, 21);
            this.lblMinus.TabIndex = 43;
            this.lblMinus.Text = "-";
            // 
            // chkSlaveFrame
            // 
            this.chkSlaveFrame.AutoSize = true;
            this.chkSlaveFrame.Location = new System.Drawing.Point(44, 62);
            this.chkSlaveFrame.Name = "chkSlaveFrame";
            this.chkSlaveFrame.Size = new System.Drawing.Size(181, 25);
            this.chkSlaveFrame.TabIndex = 42;
            this.chkSlaveFrame.Text = "یکی با با قاب پیشین";
            this.chkSlaveFrame.UseVisualStyleBackColor = true;
            this.chkSlaveFrame.CheckedChanged += new System.EventHandler(this.chkSlaveFrame_CheckedChanged);
            // 
            // txtStartInMiliseconds
            // 
            this.txtStartInMiliseconds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartInMiliseconds.Enabled = false;
            this.txtStartInMiliseconds.Location = new System.Drawing.Point(686, 60);
            this.txtStartInMiliseconds.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.txtStartInMiliseconds.Name = "txtStartInMiliseconds";
            this.txtStartInMiliseconds.Size = new System.Drawing.Size(84, 27);
            this.txtStartInMiliseconds.TabIndex = 41;
            this.txtStartInMiliseconds.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // btnResetImage
            // 
            this.btnResetImage.Location = new System.Drawing.Point(58, 96);
            this.btnResetImage.Name = "btnResetImage";
            this.btnResetImage.Size = new System.Drawing.Size(39, 36);
            this.btnResetImage.TabIndex = 40;
            this.btnResetImage.Text = "x";
            this.btnResetImage.UseVisualStyleBackColor = true;
            this.btnResetImage.Click += new System.EventHandler(this.btnResetImage_Click);
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtComment.Location = new System.Drawing.Point(412, 394);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ReadOnly = true;
            this.txtComment.Size = new System.Drawing.Size(734, 74);
            this.txtComment.TabIndex = 39;
            this.txtComment.Text = resources.GetString("txtComment.Text");
            // 
            // trckMaxTextWidth
            // 
            this.trckMaxTextWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trckMaxTextWidth.LargeChange = 15;
            this.trckMaxTextWidth.Location = new System.Drawing.Point(406, 278);
            this.trckMaxTextWidth.Maximum = 255;
            this.trckMaxTextWidth.Name = "trckMaxTextWidth";
            this.trckMaxTextWidth.RightToLeftLayout = true;
            this.trckMaxTextWidth.Size = new System.Drawing.Size(565, 69);
            this.trckMaxTextWidth.TabIndex = 38;
            this.trckMaxTextWidth.TickFrequency = 15;
            this.trckMaxTextWidth.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trckMaxTextWidth.Scroll += new System.EventHandler(this.trckMaxTextWidth_Scroll);
            // 
            // lblMaxTextWidth
            // 
            this.lblMaxTextWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMaxTextWidth.AutoSize = true;
            this.lblMaxTextWidth.Location = new System.Drawing.Point(1014, 289);
            this.lblMaxTextWidth.Name = "lblMaxTextWidth";
            this.lblMaxTextWidth.Size = new System.Drawing.Size(136, 21);
            this.lblMaxTextWidth.TabIndex = 37;
            this.lblMaxTextWidth.Text = "حداکثر عرض متن:";
            // 
            // trckVPosition
            // 
            this.trckVPosition.LargeChange = 15;
            this.trckVPosition.Location = new System.Drawing.Point(313, 193);
            this.trckVPosition.Maximum = 20;
            this.trckVPosition.Name = "trckVPosition";
            this.trckVPosition.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trckVPosition.Size = new System.Drawing.Size(69, 227);
            this.trckVPosition.TabIndex = 36;
            this.trckVPosition.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trckVPosition.Value = 10;
            this.trckVPosition.Scroll += new System.EventHandler(this.trckVPosition_Scroll);
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(2, 20);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(108, 36);
            this.btnPreview.TabIndex = 34;
            this.btnPreview.Text = "پیش‌نمایش";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // pbxPreview
            // 
            this.pbxPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxPreview.Location = new System.Drawing.Point(7, 195);
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
            this.txtFont.Location = new System.Drawing.Point(59, 137);
            this.txtFont.Name = "txtFont";
            this.txtFont.ReadOnly = true;
            this.txtFont.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFont.Size = new System.Drawing.Size(972, 27);
            this.txtFont.TabIndex = 32;
            // 
            // btnSelectFont
            // 
            this.btnSelectFont.Location = new System.Drawing.Point(7, 137);
            this.btnSelectFont.Name = "btnSelectFont";
            this.btnSelectFont.Size = new System.Drawing.Size(45, 38);
            this.btnSelectFont.TabIndex = 31;
            this.btnSelectFont.Text = "...";
            this.btnSelectFont.UseVisualStyleBackColor = true;
            this.btnSelectFont.Click += new System.EventHandler(this.btnSelectFont_Click);
            // 
            // lblFont
            // 
            this.lblFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFont.AutoSize = true;
            this.lblFont.Location = new System.Drawing.Point(1109, 141);
            this.lblFont.Name = "lblFont";
            this.lblFont.Size = new System.Drawing.Size(41, 21);
            this.lblFont.TabIndex = 29;
            this.lblFont.Text = "قلم:";
            // 
            // trckAlpha
            // 
            this.trckAlpha.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trckAlpha.LargeChange = 15;
            this.trckAlpha.Location = new System.Drawing.Point(406, 224);
            this.trckAlpha.Maximum = 255;
            this.trckAlpha.Name = "trckAlpha";
            this.trckAlpha.RightToLeftLayout = true;
            this.trckAlpha.Size = new System.Drawing.Size(565, 69);
            this.trckAlpha.TabIndex = 28;
            this.trckAlpha.TickFrequency = 15;
            this.trckAlpha.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trckAlpha.Scroll += new System.EventHandler(this.trckAlpha_Scroll);
            // 
            // lblTextBackAlpha
            // 
            this.lblTextBackAlpha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTextBackAlpha.AutoSize = true;
            this.lblTextBackAlpha.Location = new System.Drawing.Point(967, 238);
            this.lblTextBackAlpha.Name = "lblTextBackAlpha";
            this.lblTextBackAlpha.Size = new System.Drawing.Size(183, 21);
            this.lblTextBackAlpha.TabIndex = 27;
            this.lblTextBackAlpha.Text = "شفافیت رنگ زمینه متن:";
            // 
            // lblTextBackColor
            // 
            this.lblTextBackColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTextBackColor.AutoSize = true;
            this.lblTextBackColor.Location = new System.Drawing.Point(686, 177);
            this.lblTextBackColor.Name = "lblTextBackColor";
            this.lblTextBackColor.Size = new System.Drawing.Size(118, 21);
            this.lblTextBackColor.TabIndex = 25;
            this.lblTextBackColor.Text = "رنگ زمینه متن:";
            // 
            // btnTextColor
            // 
            this.btnTextColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTextColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTextColor.Location = new System.Drawing.Point(846, 170);
            this.btnTextColor.Name = "btnTextColor";
            this.btnTextColor.Size = new System.Drawing.Size(45, 40);
            this.btnTextColor.TabIndex = 26;
            this.btnTextColor.UseVisualStyleBackColor = true;
            this.btnTextColor.Click += new System.EventHandler(this.btnTextColor_Click);
            // 
            // lblTextColor
            // 
            this.lblTextColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTextColor.AutoSize = true;
            this.lblTextColor.Location = new System.Drawing.Point(901, 177);
            this.lblTextColor.Name = "lblTextColor";
            this.lblTextColor.Size = new System.Drawing.Size(76, 21);
            this.lblTextColor.TabIndex = 23;
            this.lblTextColor.Text = "رنگ متن:";
            // 
            // btnTextBackColor
            // 
            this.btnTextBackColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTextBackColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTextBackColor.Location = new System.Drawing.Point(638, 170);
            this.btnTextBackColor.Name = "btnTextBackColor";
            this.btnTextBackColor.Size = new System.Drawing.Size(42, 40);
            this.btnTextBackColor.TabIndex = 24;
            this.btnTextBackColor.UseVisualStyleBackColor = true;
            this.btnTextBackColor.Click += new System.EventHandler(this.btnTextBackColor_Click);
            // 
            // btnBackColor
            // 
            this.btnBackColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBackColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBackColor.Location = new System.Drawing.Point(981, 170);
            this.btnBackColor.Name = "btnBackColor";
            this.btnBackColor.Size = new System.Drawing.Size(50, 40);
            this.btnBackColor.TabIndex = 22;
            this.btnBackColor.UseVisualStyleBackColor = true;
            this.btnBackColor.Click += new System.EventHandler(this.btnBackColor_Click);
            // 
            // lblBackColor
            // 
            this.lblBackColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBackColor.AutoSize = true;
            this.lblBackColor.Location = new System.Drawing.Point(1065, 177);
            this.lblBackColor.Name = "lblBackColor";
            this.lblBackColor.Size = new System.Drawing.Size(85, 21);
            this.lblBackColor.TabIndex = 21;
            this.lblBackColor.Text = "رنگ زمینه:";
            // 
            // btnSelBackgroundImage
            // 
            this.btnSelBackgroundImage.Location = new System.Drawing.Point(7, 96);
            this.btnSelBackgroundImage.Name = "btnSelBackgroundImage";
            this.btnSelBackgroundImage.Size = new System.Drawing.Size(45, 36);
            this.btnSelBackgroundImage.TabIndex = 8;
            this.btnSelBackgroundImage.Text = "...";
            this.btnSelBackgroundImage.UseVisualStyleBackColor = true;
            this.btnSelBackgroundImage.Click += new System.EventHandler(this.btnSelBackgroundImage_Click);
            // 
            // txtBackgroundImage
            // 
            this.txtBackgroundImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBackgroundImage.Location = new System.Drawing.Point(182, 98);
            this.txtBackgroundImage.Name = "txtBackgroundImage";
            this.txtBackgroundImage.ReadOnly = true;
            this.txtBackgroundImage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtBackgroundImage.Size = new System.Drawing.Size(849, 27);
            this.txtBackgroundImage.TabIndex = 7;
            // 
            // lblBackgroundImage
            // 
            this.lblBackgroundImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBackgroundImage.AutoSize = true;
            this.lblBackgroundImage.Location = new System.Drawing.Point(1054, 100);
            this.lblBackgroundImage.Name = "lblBackgroundImage";
            this.lblBackgroundImage.Size = new System.Drawing.Size(96, 21);
            this.lblBackgroundImage.TabIndex = 6;
            this.lblBackgroundImage.Text = "تصویر زمینه:";
            // 
            // lblStart
            // 
            this.lblStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(776, 60);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(203, 21);
            this.lblStart.TabIndex = 4;
            this.lblStart.Text = "شروع بر حسب میلی ثانیه:";
            // 
            // chkAudioBound
            // 
            this.chkAudioBound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAudioBound.AutoSize = true;
            this.chkAudioBound.Enabled = false;
            this.chkAudioBound.Location = new System.Drawing.Point(995, 59);
            this.chkAudioBound.Name = "chkAudioBound";
            this.chkAudioBound.Size = new System.Drawing.Size(151, 25);
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
            this.cmbVerses.Location = new System.Drawing.Point(154, 20);
            this.cmbVerses.Name = "cmbVerses";
            this.cmbVerses.Size = new System.Drawing.Size(992, 29);
            this.cmbVerses.TabIndex = 0;
            this.cmbVerses.SelectedIndexChanged += new System.EventHandler(this.cmbVerses_SelectedIndexChanged);
            // 
            // statusStripMain
            // 
            this.statusStripMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStripMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prgrss,
            this.lblStatus});
            this.statusStripMain.Location = new System.Drawing.Point(0, 888);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(1211, 28);
            this.statusStripMain.SizingGrip = false;
            this.statusStripMain.TabIndex = 3;
            // 
            // prgrss
            // 
            this.prgrss.Name = "prgrss";
            this.prgrss.Size = new System.Drawing.Size(100, 22);
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(1094, 23);
            this.lblStatus.Spring = true;
            this.lblStatus.Text = "آماده";
            // 
            // btnProduce
            // 
            this.btnProduce.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnProduce.Location = new System.Drawing.Point(7, 833);
            this.btnProduce.Name = "btnProduce";
            this.btnProduce.Size = new System.Drawing.Size(183, 43);
            this.btnProduce.TabIndex = 12;
            this.btnProduce.Text = "تولید خروجی";
            this.btnProduce.UseVisualStyleBackColor = true;
            this.btnProduce.Click += new System.EventHandler(this.btnProduce_Click);
            // 
            // btnSubtitle
            // 
            this.btnSubtitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSubtitle.Location = new System.Drawing.Point(198, 833);
            this.btnSubtitle.Name = "btnSubtitle";
            this.btnSubtitle.Size = new System.Drawing.Size(163, 43);
            this.btnSubtitle.TabIndex = 14;
            this.btnSubtitle.Text = "تولید زیرنویس";
            this.btnSubtitle.UseVisualStyleBackColor = true;
            this.btnSubtitle.Click += new System.EventHandler(this.btnSubtitle_Click);
            // 
            // cmbTransitionEffect
            // 
            this.cmbTransitionEffect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbTransitionEffect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransitionEffect.FormattingEnabled = true;
            this.cmbTransitionEffect.Items.AddRange(new object[] {
            "هیچکدام",
            "انتقال قاب قبل به راست",
            "انتقال قاب قبل به چپ",
            "انتقال قاب قبل به بالا",
            "انتقال قاب قبل به پایین"});
            this.cmbTransitionEffect.Location = new System.Drawing.Point(701, 838);
            this.cmbTransitionEffect.Name = "cmbTransitionEffect";
            this.cmbTransitionEffect.Size = new System.Drawing.Size(299, 29);
            this.cmbTransitionEffect.TabIndex = 47;
            this.cmbTransitionEffect.SelectedIndexChanged += new System.EventHandler(this.cmbTransitionEffect_SelectedIndexChanged);
            // 
            // lblTransitionEffect
            // 
            this.lblTransitionEffect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTransitionEffect.AutoSize = true;
            this.lblTransitionEffect.Location = new System.Drawing.Point(367, 841);
            this.lblTransitionEffect.Name = "lblTransitionEffect";
            this.lblTransitionEffect.Size = new System.Drawing.Size(333, 21);
            this.lblTransitionEffect.TabIndex = 53;
            this.lblTransitionEffect.Text = "پویانمایی انتقال بین قابها (فقط خروجی mp4):";
            // 
            // chkAAC
            // 
            this.chkAAC.AutoSize = true;
            this.chkAAC.Location = new System.Drawing.Point(1006, 840);
            this.chkAAC.Name = "chkAAC";
            this.chkAAC.Size = new System.Drawing.Size(108, 25);
            this.chkAAC.TabIndex = 54;
            this.chkAAC.Text = "صدای aac";
            this.chkAAC.UseVisualStyleBackColor = true;
            this.chkAAC.CheckedChanged += new System.EventHandler(this.chkAAC_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1211, 916);
            this.Controls.Add(this.chkAAC);
            this.Controls.Add(this.lblTransitionEffect);
            this.Controls.Add(this.cmbTransitionEffect);
            this.Controls.Add(this.btnSubtitle);
            this.Controls.Add(this.btnProduce);
            this.Controls.Add(this.grpConnection);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.grpVerses);
            this.Controls.Add(this.grpPoem);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "خوانشگر :: تولید فیلم از خوانش شعر گنجور رومیزی";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.grpConnection.ResumeLayout(false);
            this.grpConnection.PerformLayout();
            this.grpPoem.ResumeLayout(false);
            this.grpPoem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth)).EndInit();
            this.grpVerses.ResumeLayout(false);
            this.grpVerses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtThickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trckHPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartInMiliseconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trckMaxTextWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trckVPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trckAlpha)).EndInit();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Label lblStart;
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
        private System.Windows.Forms.TrackBar trckVPosition;
        private System.Windows.Forms.TrackBar trckMaxTextWidth;
        private System.Windows.Forms.Label lblMaxTextWidth;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnResetImage;
        private System.Windows.Forms.NumericUpDown txtStartInMiliseconds;
        private System.Windows.Forms.CheckBox chkSlaveFrame;
        private System.Windows.Forms.Label lblMinus;
        private System.Windows.Forms.Button btnEditText;
        private System.Windows.Forms.CheckBox chkShowLogo;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.Button btnProduce;
        private System.Windows.Forms.ToolStripProgressBar prgrss;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Button btnRandomImage;
        private System.Windows.Forms.Button btnSubtitle;
        private System.Windows.Forms.Button btnProperties;
        private System.Windows.Forms.TrackBar trckHPosition;
        private System.Windows.Forms.NumericUpDown txtThickness;
        private System.Windows.Forms.Label lblThickness;
        private System.Windows.Forms.Button btnBorderColor;
        private System.Windows.Forms.Label lblBorderColor;
        private System.Windows.Forms.Button btnPairNext;
        private System.Windows.Forms.ComboBox cmbTransitionEffect;
        private System.Windows.Forms.Label lblTransitionEffect;
        private System.Windows.Forms.CheckBox chkAAC;
    }
}

