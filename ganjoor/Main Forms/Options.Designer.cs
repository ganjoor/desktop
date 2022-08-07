using System.ComponentModel;
using System.Windows.Forms;

namespace ganjoor
{
    partial class Options
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.label1 = new System.Windows.Forms.Label();
            this.lblFont = new System.Windows.Forms.Label();
            this.btnSelectFont = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkHighlightSearchResults = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numMaxPoems = new System.Windows.Forms.NumericUpDown();
            this.chkGradiantBk = new System.Windows.Forms.CheckBox();
            this.btnGradiantEnd = new System.Windows.Forms.Button();
            this.lblNormalBk = new System.Windows.Forms.Label();
            this.lblGradiantEnd = new System.Windows.Forms.Label();
            this.lblGradiantBegin = new System.Windows.Forms.Label();
            this.btnGradiantBegin = new System.Windows.Forms.Button();
            this.btnBackColor = new System.Windows.Forms.Button();
            this.lblImage = new System.Windows.Forms.Label();
            this.lblImagePath = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnTextColor = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
            this.chkCheckForUpdate = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnNoBkImage = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnLinkColor = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCurrentLinkColor = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnHighlightColor = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btnBandLinkColor = new System.Windows.Forms.Button();
            this.chkScrollToFaved = new System.Windows.Forms.CheckBox();
            this.chkCenteredViewMode = new System.Windows.Forms.CheckBox();
            this.numMaxFavs = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.lblRandomCat = new System.Windows.Forms.Label();
            this.btnSelectRandomCat = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtProxyServer = new System.Windows.Forms.TextBox();
            this.txtProxyPort = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.grpOptionalButtons = new System.Windows.Forms.GroupBox();
            this.chkDownloadButton = new System.Windows.Forms.CheckBox();
            this.chkEditorButton = new System.Windows.Forms.CheckBox();
            this.chkRandomButton = new System.Windows.Forms.CheckBox();
            this.chkHomeButton = new System.Windows.Forms.CheckBox();
            this.chkShowNumsButton = new System.Windows.Forms.CheckBox();
            this.chkPrintButton = new System.Windows.Forms.CheckBox();
            this.chkCopyButton = new System.Windows.Forms.CheckBox();
            this.chkCommentsButton = new System.Windows.Forms.CheckBox();
            this.chkBrowseButton = new System.Windows.Forms.CheckBox();
            this.btnBrowseDbPath = new System.Windows.Forms.Button();
            this.lblDbPath = new System.Windows.Forms.Label();
            this.txtDbPath = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPoems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxFavs)).BeginInit();
            this.grpOptionalButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "قلم:";
            // 
            // lblFont
            // 
            this.lblFont.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFont.Location = new System.Drawing.Point(49, 15);
            this.lblFont.Name = "lblFont";
            this.lblFont.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblFont.Size = new System.Drawing.Size(502, 23);
            this.lblFont.TabIndex = 4;
            this.lblFont.Text = "...";
            this.lblFont.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSelectFont
            // 
            this.btnSelectFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFont.Location = new System.Drawing.Point(557, 15);
            this.btnSelectFont.Name = "btnSelectFont";
            this.btnSelectFont.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFont.TabIndex = 5;
            this.btnSelectFont.Text = "گزینش";
            this.btnSelectFont.UseVisualStyleBackColor = true;
            this.btnSelectFont.Click += new System.EventHandler(this.btnSelectFont_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(15, 391);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "تأیید";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(96, 391);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "انصراف";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chkHighlightSearchResults
            // 
            this.chkHighlightSearchResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkHighlightSearchResults.AutoSize = true;
            this.chkHighlightSearchResults.Location = new System.Drawing.Point(15, 137);
            this.chkHighlightSearchResults.Name = "chkHighlightSearchResults";
            this.chkHighlightSearchResults.Size = new System.Drawing.Size(489, 17);
            this.chkHighlightSearchResults.TabIndex = 27;
            this.chkHighlightSearchResults.Text = "کلمهٔ جستجو شده را در شعری که پس از کلیک بر روی نتایج جستجو نمایش داده می‌شود برج" +
    "سته کن.";
            this.chkHighlightSearchResults.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 247);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(227, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "حداکثر تعداد عنوانها در فهرست اشعار یک بخش:";
            // 
            // numMaxPoems
            // 
            this.numMaxPoems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numMaxPoems.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numMaxPoems.Location = new System.Drawing.Point(253, 245);
            this.numMaxPoems.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.numMaxPoems.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numMaxPoems.Name = "numMaxPoems";
            this.numMaxPoems.Size = new System.Drawing.Size(51, 21);
            this.numMaxPoems.TabIndex = 31;
            this.numMaxPoems.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // chkGradiantBk
            // 
            this.chkGradiantBk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkGradiantBk.AutoSize = true;
            this.chkGradiantBk.Location = new System.Drawing.Point(15, 48);
            this.chkGradiantBk.Name = "chkGradiantBk";
            this.chkGradiantBk.Size = new System.Drawing.Size(102, 17);
            this.chkGradiantBk.TabIndex = 6;
            this.chkGradiantBk.Text = "رنگ زمینهٔ طیفی";
            this.chkGradiantBk.UseVisualStyleBackColor = true;
            this.chkGradiantBk.CheckedChanged += new System.EventHandler(this.chkGradiantBk_CheckedChanged);
            // 
            // btnGradiantEnd
            // 
            this.btnGradiantEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGradiantEnd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGradiantEnd.Location = new System.Drawing.Point(348, 44);
            this.btnGradiantEnd.Name = "btnGradiantEnd";
            this.btnGradiantEnd.Size = new System.Drawing.Size(28, 23);
            this.btnGradiantEnd.TabIndex = 10;
            this.btnGradiantEnd.UseVisualStyleBackColor = true;
            this.btnGradiantEnd.Click += new System.EventHandler(this.btnGradiantEnd_Click);
            // 
            // lblNormalBk
            // 
            this.lblNormalBk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNormalBk.AutoSize = true;
            this.lblNormalBk.Location = new System.Drawing.Point(15, 77);
            this.lblNormalBk.Name = "lblNormalBk";
            this.lblNormalBk.Size = new System.Drawing.Size(84, 13);
            this.lblNormalBk.TabIndex = 15;
            this.lblNormalBk.Text = "رنگ زمینهٔ عادی:";
            // 
            // lblGradiantEnd
            // 
            this.lblGradiantEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGradiantEnd.AutoSize = true;
            this.lblGradiantEnd.Location = new System.Drawing.Point(291, 49);
            this.lblGradiantEnd.Name = "lblGradiantEnd";
            this.lblGradiantEnd.Size = new System.Drawing.Size(51, 13);
            this.lblGradiantEnd.TabIndex = 9;
            this.lblGradiantEnd.Text = "رنگ پایان:";
            // 
            // lblGradiantBegin
            // 
            this.lblGradiantBegin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGradiantBegin.AutoSize = true;
            this.lblGradiantBegin.Location = new System.Drawing.Point(145, 49);
            this.lblGradiantBegin.Name = "lblGradiantBegin";
            this.lblGradiantBegin.Size = new System.Drawing.Size(60, 13);
            this.lblGradiantBegin.TabIndex = 7;
            this.lblGradiantBegin.Text = "رنگ شروع:";
            // 
            // btnGradiantBegin
            // 
            this.btnGradiantBegin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGradiantBegin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGradiantBegin.Location = new System.Drawing.Point(211, 44);
            this.btnGradiantBegin.Name = "btnGradiantBegin";
            this.btnGradiantBegin.Size = new System.Drawing.Size(28, 23);
            this.btnGradiantBegin.TabIndex = 8;
            this.btnGradiantBegin.UseVisualStyleBackColor = true;
            this.btnGradiantBegin.Click += new System.EventHandler(this.btnGradiantBegin_Click);
            // 
            // btnBackColor
            // 
            this.btnBackColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBackColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBackColor.Location = new System.Drawing.Point(109, 73);
            this.btnBackColor.Name = "btnBackColor";
            this.btnBackColor.Size = new System.Drawing.Size(28, 23);
            this.btnBackColor.TabIndex = 16;
            this.btnBackColor.UseVisualStyleBackColor = true;
            this.btnBackColor.Click += new System.EventHandler(this.btnBackColor_Click);
            // 
            // lblImage
            // 
            this.lblImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblImage.AutoSize = true;
            this.lblImage.Location = new System.Drawing.Point(15, 106);
            this.lblImage.Name = "lblImage";
            this.lblImage.Size = new System.Drawing.Size(63, 13);
            this.lblImage.TabIndex = 23;
            this.lblImage.Text = "تصویر زمینه:";
            // 
            // lblImagePath
            // 
            this.lblImagePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblImagePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblImagePath.Location = new System.Drawing.Point(79, 103);
            this.lblImagePath.Name = "lblImagePath";
            this.lblImagePath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblImagePath.Size = new System.Drawing.Size(485, 19);
            this.lblImagePath.TabIndex = 24;
            this.lblImagePath.Text = "label5";
            this.lblImagePath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Location = new System.Drawing.Point(570, 101);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(27, 23);
            this.btnSelect.TabIndex = 25;
            this.btnSelect.Text = "...";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(550, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "رنگ متن:";
            // 
            // btnTextColor
            // 
            this.btnTextColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTextColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTextColor.Location = new System.Drawing.Point(603, 43);
            this.btnTextColor.Name = "btnTextColor";
            this.btnTextColor.Size = new System.Drawing.Size(28, 23);
            this.btnTextColor.TabIndex = 14;
            this.btnTextColor.UseVisualStyleBackColor = true;
            this.btnTextColor.Click += new System.EventHandler(this.btnTextColor_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefault.Location = new System.Drawing.Point(476, 391);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(156, 23);
            this.btnDefault.TabIndex = 2;
            this.btnDefault.Text = "بازگشت پیکربندی پیش‌فرض";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // chkCheckForUpdate
            // 
            this.chkCheckForUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCheckForUpdate.AutoSize = true;
            this.chkCheckForUpdate.Location = new System.Drawing.Point(15, 272);
            this.chkCheckForUpdate.Name = "chkCheckForUpdate";
            this.chkCheckForUpdate.Size = new System.Drawing.Size(416, 17);
            this.chkCheckForUpdate.TabIndex = 34;
            this.chkCheckForUpdate.Text = "در هنگام شروع برنامه برای یافتن ویرایش جدیدتر و مجموعه اشعار جدید پرس و جو کن.";
            this.chkCheckForUpdate.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 322);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "دامنهٔ عملکرد دکمهٔ فال:";
            // 
            // btnNoBkImage
            // 
            this.btnNoBkImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNoBkImage.Location = new System.Drawing.Point(604, 101);
            this.btnNoBkImage.Name = "btnNoBkImage";
            this.btnNoBkImage.Size = new System.Drawing.Size(27, 23);
            this.btnNoBkImage.TabIndex = 26;
            this.btnNoBkImage.Text = "X";
            this.btnNoBkImage.UseVisualStyleBackColor = true;
            this.btnNoBkImage.Click += new System.EventHandler(this.btnNoBkImage_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(162, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "رنگ پیوندهای عادی:";
            // 
            // btnLinkColor
            // 
            this.btnLinkColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLinkColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLinkColor.Location = new System.Drawing.Point(266, 73);
            this.btnLinkColor.Name = "btnLinkColor";
            this.btnLinkColor.Size = new System.Drawing.Size(28, 23);
            this.btnLinkColor.TabIndex = 18;
            this.btnLinkColor.UseVisualStyleBackColor = true;
            this.btnLinkColor.Click += new System.EventHandler(this.btnLinkColor_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(315, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "رنگ پیوند جاری:";
            // 
            // btnCurrentLinkColor
            // 
            this.btnCurrentLinkColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCurrentLinkColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCurrentLinkColor.Location = new System.Drawing.Point(398, 72);
            this.btnCurrentLinkColor.Name = "btnCurrentLinkColor";
            this.btnCurrentLinkColor.Size = new System.Drawing.Size(28, 23);
            this.btnCurrentLinkColor.TabIndex = 20;
            this.btnCurrentLinkColor.UseVisualStyleBackColor = true;
            this.btnCurrentLinkColor.Click += new System.EventHandler(this.btnCurrentLinkColor_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(407, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "رنگ برجسته‌سازی:";
            // 
            // btnHighlightColor
            // 
            this.btnHighlightColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHighlightColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnHighlightColor.Location = new System.Drawing.Point(505, 44);
            this.btnHighlightColor.Name = "btnHighlightColor";
            this.btnHighlightColor.Size = new System.Drawing.Size(28, 23);
            this.btnHighlightColor.TabIndex = 12;
            this.btnHighlightColor.UseVisualStyleBackColor = true;
            this.btnHighlightColor.Click += new System.EventHandler(this.btnHighlightColor_Click);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(465, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "رنگ شمارهٔ ابیات بین بندها:";
            // 
            // btnBandLinkColor
            // 
            this.btnBandLinkColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBandLinkColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBandLinkColor.Location = new System.Drawing.Point(603, 72);
            this.btnBandLinkColor.Name = "btnBandLinkColor";
            this.btnBandLinkColor.Size = new System.Drawing.Size(28, 23);
            this.btnBandLinkColor.TabIndex = 22;
            this.btnBandLinkColor.UseVisualStyleBackColor = true;
            this.btnBandLinkColor.Click += new System.EventHandler(this.btnBandLinkColor_Click);
            // 
            // chkScrollToFaved
            // 
            this.chkScrollToFaved.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkScrollToFaved.AutoSize = true;
            this.chkScrollToFaved.Location = new System.Drawing.Point(15, 163);
            this.chkScrollToFaved.Name = "chkScrollToFaved";
            this.chkScrollToFaved.Size = new System.Drawing.Size(467, 17);
            this.chkScrollToFaved.TabIndex = 28;
            this.chkScrollToFaved.Text = "نوار لغزنده را به محل اولین بیت نشانه‌گذاری شده در شعر انتخاب شده از فهرست نشانه‌" +
    "ها بلغزان.";
            this.chkScrollToFaved.UseVisualStyleBackColor = true;
            // 
            // chkCenteredViewMode
            // 
            this.chkCenteredViewMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCenteredViewMode.AutoSize = true;
            this.chkCenteredViewMode.Location = new System.Drawing.Point(15, 347);
            this.chkCenteredViewMode.Name = "chkCenteredViewMode";
            this.chkCenteredViewMode.Size = new System.Drawing.Size(322, 17);
            this.chkCenteredViewMode.TabIndex = 42;
            this.chkCenteredViewMode.Text = "هر بیت را در یک خط نشان بده (نمایش ابیات به صورت وسط چین).";
            this.chkCenteredViewMode.UseVisualStyleBackColor = true;
            // 
            // numMaxFavs
            // 
            this.numMaxFavs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numMaxFavs.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numMaxFavs.Location = new System.Drawing.Point(581, 242);
            this.numMaxFavs.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numMaxFavs.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numMaxFavs.Name = "numMaxFavs";
            this.numMaxFavs.Size = new System.Drawing.Size(51, 21);
            this.numMaxFavs.TabIndex = 33;
            this.numMaxFavs.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(377, 247);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(198, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = "حداکثر تعداد نشانه‌ها در صفحات نشانه‌ها:";
            // 
            // lblRandomCat
            // 
            this.lblRandomCat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRandomCat.AutoEllipsis = true;
            this.lblRandomCat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRandomCat.ForeColor = System.Drawing.Color.Green;
            this.lblRandomCat.Location = new System.Drawing.Point(134, 322);
            this.lblRandomCat.Name = "lblRandomCat";
            this.lblRandomCat.Size = new System.Drawing.Size(373, 17);
            this.lblRandomCat.TabIndex = 40;
            // 
            // btnSelectRandomCat
            // 
            this.btnSelectRandomCat.Location = new System.Drawing.Point(516, 318);
            this.btnSelectRandomCat.Name = "btnSelectRandomCat";
            this.btnSelectRandomCat.Size = new System.Drawing.Size(75, 23);
            this.btnSelectRandomCat.TabIndex = 41;
            this.btnSelectRandomCat.Text = "انتخاب";
            this.btnSelectRandomCat.UseVisualStyleBackColor = true;
            this.btnSelectRandomCat.Click += new System.EventHandler(this.btnSelectRandomCat_Click);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 297);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(190, 13);
            this.label11.TabIndex = 35;
            this.label11.Text = "نشانی سرور پروکسی اتصال به اینترنت:";
            // 
            // txtProxyServer
            // 
            this.txtProxyServer.Location = new System.Drawing.Point(205, 294);
            this.txtProxyServer.Name = "txtProxyServer";
            this.txtProxyServer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtProxyServer.Size = new System.Drawing.Size(100, 21);
            this.txtProxyServer.TabIndex = 36;
            // 
            // txtProxyPort
            // 
            this.txtProxyPort.Location = new System.Drawing.Point(349, 294);
            this.txtProxyPort.Name = "txtProxyPort";
            this.txtProxyPort.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtProxyPort.Size = new System.Drawing.Size(52, 21);
            this.txtProxyPort.TabIndex = 38;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(311, 297);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 13);
            this.label12.TabIndex = 37;
            this.label12.Text = "پورت:";
            // 
            // grpOptionalButtons
            // 
            this.grpOptionalButtons.Controls.Add(this.chkDownloadButton);
            this.grpOptionalButtons.Controls.Add(this.chkEditorButton);
            this.grpOptionalButtons.Controls.Add(this.chkRandomButton);
            this.grpOptionalButtons.Controls.Add(this.chkHomeButton);
            this.grpOptionalButtons.Controls.Add(this.chkShowNumsButton);
            this.grpOptionalButtons.Controls.Add(this.chkPrintButton);
            this.grpOptionalButtons.Controls.Add(this.chkCopyButton);
            this.grpOptionalButtons.Controls.Add(this.chkCommentsButton);
            this.grpOptionalButtons.Controls.Add(this.chkBrowseButton);
            this.grpOptionalButtons.Location = new System.Drawing.Point(15, 186);
            this.grpOptionalButtons.Name = "grpOptionalButtons";
            this.grpOptionalButtons.Size = new System.Drawing.Size(616, 50);
            this.grpOptionalButtons.TabIndex = 29;
            this.grpOptionalButtons.TabStop = false;
            this.grpOptionalButtons.Text = "دکمه‌های اختیاری";
            // 
            // chkDownloadButton
            // 
            this.chkDownloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDownloadButton.AutoSize = true;
            this.chkDownloadButton.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDownloadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkDownloadButton.Location = new System.Drawing.Point(14, 25);
            this.chkDownloadButton.Name = "chkDownloadButton";
            this.chkDownloadButton.Size = new System.Drawing.Size(105, 17);
            this.chkDownloadButton.TabIndex = 8;
            this.chkDownloadButton.Text = "دریافت مجموعه‌ها";
            this.chkDownloadButton.UseVisualStyleBackColor = true;
            // 
            // chkEditorButton
            // 
            this.chkEditorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEditorButton.AutoSize = true;
            this.chkEditorButton.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkEditorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkEditorButton.Location = new System.Drawing.Point(124, 25);
            this.chkEditorButton.Name = "chkEditorButton";
            this.chkEditorButton.Size = new System.Drawing.Size(65, 17);
            this.chkEditorButton.TabIndex = 7;
            this.chkEditorButton.Text = "ویرایشگر";
            this.chkEditorButton.UseVisualStyleBackColor = true;
            // 
            // chkRandomButton
            // 
            this.chkRandomButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRandomButton.AutoSize = true;
            this.chkRandomButton.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRandomButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRandomButton.Location = new System.Drawing.Point(514, 25);
            this.chkRandomButton.Name = "chkRandomButton";
            this.chkRandomButton.Size = new System.Drawing.Size(38, 17);
            this.chkRandomButton.TabIndex = 1;
            this.chkRandomButton.Text = "فال";
            this.chkRandomButton.UseVisualStyleBackColor = true;
            // 
            // chkHomeButton
            // 
            this.chkHomeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkHomeButton.AutoSize = true;
            this.chkHomeButton.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkHomeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkHomeButton.Location = new System.Drawing.Point(557, 25);
            this.chkHomeButton.Name = "chkHomeButton";
            this.chkHomeButton.Size = new System.Drawing.Size(42, 17);
            this.chkHomeButton.TabIndex = 0;
            this.chkHomeButton.Text = "خانه";
            this.chkHomeButton.UseVisualStyleBackColor = true;
            // 
            // chkShowNumsButton
            // 
            this.chkShowNumsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowNumsButton.AutoSize = true;
            this.chkShowNumsButton.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkShowNumsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkShowNumsButton.Location = new System.Drawing.Point(194, 25);
            this.chkShowNumsButton.Name = "chkShowNumsButton";
            this.chkShowNumsButton.Size = new System.Drawing.Size(81, 17);
            this.chkShowNumsButton.TabIndex = 6;
            this.chkShowNumsButton.Text = "شماره‌گذاری";
            this.chkShowNumsButton.UseVisualStyleBackColor = true;
            // 
            // chkPrintButton
            // 
            this.chkPrintButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkPrintButton.AutoSize = true;
            this.chkPrintButton.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPrintButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkPrintButton.Location = new System.Drawing.Point(280, 25);
            this.chkPrintButton.Name = "chkPrintButton";
            this.chkPrintButton.Size = new System.Drawing.Size(42, 17);
            this.chkPrintButton.TabIndex = 5;
            this.chkPrintButton.Text = "چاپ";
            this.chkPrintButton.UseVisualStyleBackColor = true;
            // 
            // chkCopyButton
            // 
            this.chkCopyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCopyButton.AutoSize = true;
            this.chkCopyButton.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCopyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkCopyButton.Location = new System.Drawing.Point(327, 25);
            this.chkCopyButton.Name = "chkCopyButton";
            this.chkCopyButton.Size = new System.Drawing.Size(64, 17);
            this.chkCopyButton.TabIndex = 4;
            this.chkCopyButton.Text = "کپی متن";
            this.chkCopyButton.UseVisualStyleBackColor = true;
            // 
            // chkCommentsButton
            // 
            this.chkCommentsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCommentsButton.AutoSize = true;
            this.chkCommentsButton.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCommentsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkCommentsButton.Location = new System.Drawing.Point(396, 25);
            this.chkCommentsButton.Name = "chkCommentsButton";
            this.chkCommentsButton.Size = new System.Drawing.Size(65, 17);
            this.chkCommentsButton.TabIndex = 3;
            this.chkCommentsButton.Text = "حاشیه‌ها";
            this.chkCommentsButton.UseVisualStyleBackColor = true;
            // 
            // chkBrowseButton
            // 
            this.chkBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkBrowseButton.AutoSize = true;
            this.chkBrowseButton.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkBrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkBrowseButton.Location = new System.Drawing.Point(466, 25);
            this.chkBrowseButton.Name = "chkBrowseButton";
            this.chkBrowseButton.Size = new System.Drawing.Size(43, 17);
            this.chkBrowseButton.TabIndex = 2;
            this.chkBrowseButton.Text = "مرور";
            this.chkBrowseButton.UseVisualStyleBackColor = true;
            // 
            // btnBrowseDbPath
            // 
            this.btnBrowseDbPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseDbPath.Location = new System.Drawing.Point(597, 362);
            this.btnBrowseDbPath.Name = "btnBrowseDbPath";
            this.btnBrowseDbPath.Size = new System.Drawing.Size(35, 23);
            this.btnBrowseDbPath.TabIndex = 45;
            this.btnBrowseDbPath.Text = "مرور";
            this.btnBrowseDbPath.UseVisualStyleBackColor = true;
            this.btnBrowseDbPath.Click += new System.EventHandler(this.btnBrowseDbPath_Click);
            // 
            // lblDbPath
            // 
            this.lblDbPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDbPath.AutoSize = true;
            this.lblDbPath.Location = new System.Drawing.Point(15, 367);
            this.lblDbPath.Name = "lblDbPath";
            this.lblDbPath.Size = new System.Drawing.Size(96, 13);
            this.lblDbPath.TabIndex = 43;
            this.lblDbPath.Text = "مسیر پایگاه داده‌ها:";
            // 
            // txtDbPath
            // 
            this.txtDbPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDbPath.Location = new System.Drawing.Point(117, 363);
            this.txtDbPath.Name = "txtDbPath";
            this.txtDbPath.ReadOnly = true;
            this.txtDbPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDbPath.Size = new System.Drawing.Size(480, 21);
            this.txtDbPath.TabIndex = 46;
            // 
            // Options
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(644, 418);
            this.Controls.Add(this.txtDbPath);
            this.Controls.Add(this.btnBrowseDbPath);
            this.Controls.Add(this.lblDbPath);
            this.Controls.Add(this.grpOptionalButtons);
            this.Controls.Add(this.txtProxyPort);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtProxyServer);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnSelectRandomCat);
            this.Controls.Add(this.lblRandomCat);
            this.Controls.Add(this.numMaxFavs);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.chkCenteredViewMode);
            this.Controls.Add(this.chkScrollToFaved);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnBandLinkColor);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnHighlightColor);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnCurrentLinkColor);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnLinkColor);
            this.Controls.Add(this.btnNoBkImage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkCheckForUpdate);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnTextColor);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lblImagePath);
            this.Controls.Add(this.lblImage);
            this.Controls.Add(this.btnBackColor);
            this.Controls.Add(this.lblGradiantBegin);
            this.Controls.Add(this.btnGradiantBegin);
            this.Controls.Add(this.lblGradiantEnd);
            this.Controls.Add(this.lblNormalBk);
            this.Controls.Add(this.btnGradiantEnd);
            this.Controls.Add(this.chkGradiantBk);
            this.Controls.Add(this.numMaxPoems);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkHighlightSearchResults);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnSelectFont);
            this.Controls.Add(this.lblFont);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "پیکربندی";
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPoems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxFavs)).EndInit();
            this.grpOptionalButtons.ResumeLayout(false);
            this.grpOptionalButtons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label lblFont;
        private Button btnSelectFont;
        private Button btnOK;
        private Button btnCancel;
        private CheckBox chkHighlightSearchResults;
        private Label label3;
        private NumericUpDown numMaxPoems;
        private CheckBox chkGradiantBk;
        private Button btnGradiantEnd;
        private Label lblNormalBk;
        private Label lblGradiantEnd;
        private Label lblGradiantBegin;
        private Button btnGradiantBegin;
        private Button btnBackColor;
        private Label lblImage;
        private Label lblImagePath;
        private Button btnSelect;
        private Label label4;
        private Button btnTextColor;
        private Button btnDefault;
        private CheckBox chkCheckForUpdate;
        private Label label5;
        private Button btnNoBkImage;
        private Label label6;
        private Button btnLinkColor;
        private Label label7;
        private Button btnCurrentLinkColor;
        private Label label8;
        private Button btnHighlightColor;
        private Label label9;
        private Button btnBandLinkColor;
        private CheckBox chkScrollToFaved;
        private CheckBox chkCenteredViewMode;
        private NumericUpDown numMaxFavs;
        private Label label10;
        private Label lblRandomCat;
        private Button btnSelectRandomCat;
        private Label label11;
        private TextBox txtProxyServer;
        private TextBox txtProxyPort;
        private Label label12;
        private GroupBox grpOptionalButtons;
        private CheckBox chkDownloadButton;
        private CheckBox chkEditorButton;
        private CheckBox chkRandomButton;
        private CheckBox chkHomeButton;
        private CheckBox chkShowNumsButton;
        private CheckBox chkPrintButton;
        private CheckBox chkCopyButton;
        private CheckBox chkCommentsButton;
        private CheckBox chkBrowseButton;
        private Button btnBrowseDbPath;
        private Label lblDbPath;
        private TextBox txtDbPath;
    }
}