namespace ganjoor
{
    partial class Editor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.btnPreviousPoem = new System.Windows.Forms.ToolStripButton();
            this.btnNextPoem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNewPoet = new System.Windows.Forms.ToolStripSplitButton();
            this.btnNewPoetSub = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEditPoet = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDeletePoet = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportPoet = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNewCat = new System.Windows.Forms.ToolStripSplitButton();
            this.btnNewCatSub = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEditCat = new System.Windows.Forms.ToolStripMenuItem();
            this.btnReOrderCat = new System.Windows.Forms.ToolStripMenuItem();
            this.btnReOrderSubCat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDeleteCat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportCat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNewPoem = new System.Windows.Forms.ToolStripSplitButton();
            this.btnNewPoemSub = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEditPoem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImportFromTextFile = new System.Windows.Forms.ToolStripMenuItem();
            this.btnImportFromClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.btnImportFromClipboadStructuredPoem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.chkEachlineOneverse = new System.Windows.Forms.ToolStripMenuItem();
            this.chkIgnoreBlankLines = new System.Windows.Forms.ToolStripMenuItem();
            this.chkIgnoreShortLines = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDeletePoem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNewLine = new System.Windows.Forms.ToolStripSplitButton();
            this.btnNewNormalLine = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNewBandLine = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNewBandVerse = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNewSingleVerse = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDeleteLine = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeleteAllLine = new System.Windows.Forms.ToolStripMenuItem();
            this.btnTools = new System.Windows.Forms.ToolStripSplitButton();
            this.btnMergeTwoTextColumns = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ganjoorView = new ganjoor.GanjoorViewer();
            this.btnReplaceInDb = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPreviousPoem,
            this.btnNextPoem,
            this.toolStripSeparator1,
            this.btnNewPoet,
            this.btnNewCat,
            this.toolStripSeparator2,
            this.btnNewPoem,
            this.toolStripSeparator3,
            this.btnNewLine,
            this.btnTools});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(757, 69);
            this.toolStripMain.TabIndex = 0;
            this.toolStripMain.Text = "نوار ابزار";
            // 
            // btnPreviousPoem
            // 
            this.btnPreviousPoem.Image = global::ganjoor.Properties.Resources.next;
            this.btnPreviousPoem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPreviousPoem.Name = "btnPreviousPoem";
            this.btnPreviousPoem.Size = new System.Drawing.Size(56, 66);
            this.btnPreviousPoem.Text = "شعر قبل";
            this.btnPreviousPoem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPreviousPoem.Click += new System.EventHandler(this.btnPreviousPoem_Click);
            // 
            // btnNextPoem
            // 
            this.btnNextPoem.Image = global::ganjoor.Properties.Resources.back;
            this.btnNextPoem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNextPoem.Name = "btnNextPoem";
            this.btnNextPoem.Size = new System.Drawing.Size(54, 66);
            this.btnNextPoem.Text = "شعر بعد";
            this.btnNextPoem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNextPoem.Click += new System.EventHandler(this.btnNextPoem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 69);
            // 
            // btnNewPoet
            // 
            this.btnNewPoet.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewPoetSub,
            this.toolStripSeparator6,
            this.btnEditPoet,
            this.toolStripSeparator7,
            this.btnDeletePoet,
            this.toolStripSeparator12,
            this.btnExportPoet});
            this.btnNewPoet.Image = global::ganjoor.Properties.Resources.user;
            this.btnNewPoet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewPoet.Name = "btnNewPoet";
            this.btnNewPoet.Size = new System.Drawing.Size(80, 66);
            this.btnNewPoet.Text = "شاعر جدید";
            this.btnNewPoet.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNewPoet.ButtonClick += new System.EventHandler(this.btnNewPoet_ButtonClick);
            // 
            // btnNewPoetSub
            // 
            this.btnNewPoetSub.Name = "btnNewPoetSub";
            this.btnNewPoetSub.Size = new System.Drawing.Size(199, 22);
            this.btnNewPoetSub.Text = "شاعر جدید";
            this.btnNewPoetSub.Click += new System.EventHandler(this.btnNewPoet_ButtonClick);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(196, 6);
            // 
            // btnEditPoet
            // 
            this.btnEditPoet.Name = "btnEditPoet";
            this.btnEditPoet.Size = new System.Drawing.Size(199, 22);
            this.btnEditPoet.Text = "ویرایش نام شاعر";
            this.btnEditPoet.Click += new System.EventHandler(this.btnEditPoet_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(196, 6);
            // 
            // btnDeletePoet
            // 
            this.btnDeletePoet.Name = "btnDeletePoet";
            this.btnDeletePoet.Size = new System.Drawing.Size(199, 22);
            this.btnDeletePoet.Text = "حذف شاعر";
            this.btnDeletePoet.Click += new System.EventHandler(this.btnDeletePoet_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(196, 6);
            // 
            // btnExportPoet
            // 
            this.btnExportPoet.Name = "btnExportPoet";
            this.btnExportPoet.Size = new System.Drawing.Size(199, 22);
            this.btnExportPoet.Text = "تولید خروجی از آثار شاعر";
            this.btnExportPoet.Click += new System.EventHandler(this.btnExportPoet_Click);
            // 
            // btnNewCat
            // 
            this.btnNewCat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewCatSub,
            this.toolStripSeparator8,
            this.btnEditCat,
            this.btnReOrderCat,
            this.btnReOrderSubCat,
            this.toolStripSeparator9,
            this.btnDeleteCat,
            this.toolStripSeparator11,
            this.btnExportCat});
            this.btnNewCat.Image = global::ganjoor.Properties.Resources.folder;
            this.btnNewCat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewCat.Name = "btnNewCat";
            this.btnNewCat.Size = new System.Drawing.Size(79, 66);
            this.btnNewCat.Text = "بخش جدید";
            this.btnNewCat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNewCat.ButtonClick += new System.EventHandler(this.btnNewCat_ButtonClick);
            // 
            // btnNewCatSub
            // 
            this.btnNewCatSub.Name = "btnNewCatSub";
            this.btnNewCatSub.Size = new System.Drawing.Size(220, 22);
            this.btnNewCatSub.Text = "بخش جدید";
            this.btnNewCatSub.Click += new System.EventHandler(this.btnNewCat_ButtonClick);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(217, 6);
            // 
            // btnEditCat
            // 
            this.btnEditCat.Name = "btnEditCat";
            this.btnEditCat.Size = new System.Drawing.Size(220, 22);
            this.btnEditCat.Text = "ویرایش عنوان بخش";
            this.btnEditCat.Click += new System.EventHandler(this.btnEditCat_Click);
            // 
            // btnReOrderCat
            // 
            this.btnReOrderCat.Name = "btnReOrderCat";
            this.btnReOrderCat.Size = new System.Drawing.Size(220, 22);
            this.btnReOrderCat.Text = "تغییر ترتیب اشعار بخش";
            this.btnReOrderCat.Click += new System.EventHandler(this.btnReOrderCat_Click);
            // 
            // btnReOrderSubCat
            // 
            this.btnReOrderSubCat.Name = "btnReOrderSubCat";
            this.btnReOrderSubCat.Size = new System.Drawing.Size(220, 22);
            this.btnReOrderSubCat.Text = "تغییر ترتیب زیربخشها";
            this.btnReOrderSubCat.Click += new System.EventHandler(this.btnReOrderSubCat_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(217, 6);
            // 
            // btnDeleteCat
            // 
            this.btnDeleteCat.Name = "btnDeleteCat";
            this.btnDeleteCat.Size = new System.Drawing.Size(220, 22);
            this.btnDeleteCat.Text = "حذف بخش";
            this.btnDeleteCat.Click += new System.EventHandler(this.btnDeleteCat_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(217, 6);
            // 
            // btnExportCat
            // 
            this.btnExportCat.Name = "btnExportCat";
            this.btnExportCat.Size = new System.Drawing.Size(220, 22);
            this.btnExportCat.Text = "تولید خروجی از محتوای بخش";
            this.btnExportCat.Click += new System.EventHandler(this.btnExportCat_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 69);
            // 
            // btnNewPoem
            // 
            this.btnNewPoem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewPoemSub,
            this.toolStripSeparator4,
            this.btnEditPoem,
            this.toolStripSeparator13,
            this.btnImportFromTextFile,
            this.btnImportFromClipboard,
            this.btnImportFromClipboadStructuredPoem,
            this.toolStripSeparator14,
            this.chkEachlineOneverse,
            this.chkIgnoreBlankLines,
            this.chkIgnoreShortLines,
            this.toolStripSeparator5,
            this.btnDeletePoem});
            this.btnNewPoem.Image = global::ganjoor.Properties.Resources.add;
            this.btnNewPoem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewPoem.Name = "btnNewPoem";
            this.btnNewPoem.Size = new System.Drawing.Size(75, 66);
            this.btnNewPoem.Text = "شعر جدید";
            this.btnNewPoem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNewPoem.ButtonClick += new System.EventHandler(this.btnNewPoem_ButtonClick);
            // 
            // btnNewPoemSub
            // 
            this.btnNewPoemSub.Name = "btnNewPoemSub";
            this.btnNewPoemSub.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.btnNewPoemSub.Size = new System.Drawing.Size(383, 22);
            this.btnNewPoemSub.Text = "شعر جدید";
            this.btnNewPoemSub.Click += new System.EventHandler(this.btnNewPoem_ButtonClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(380, 6);
            // 
            // btnEditPoem
            // 
            this.btnEditPoem.Name = "btnEditPoem";
            this.btnEditPoem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.btnEditPoem.Size = new System.Drawing.Size(383, 22);
            this.btnEditPoem.Text = "ویرایش عنوان شعر";
            this.btnEditPoem.Click += new System.EventHandler(this.btnEditPoem_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(380, 6);
            // 
            // btnImportFromTextFile
            // 
            this.btnImportFromTextFile.Name = "btnImportFromTextFile";
            this.btnImportFromTextFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.btnImportFromTextFile.Size = new System.Drawing.Size(383, 22);
            this.btnImportFromTextFile.Text = "درج محتوی از فایل متنی با خطوط جدا شده با ENTER";
            this.btnImportFromTextFile.Click += new System.EventHandler(this.btnImportFromTextFile_Click);
            // 
            // btnImportFromClipboard
            // 
            this.btnImportFromClipboard.Name = "btnImportFromClipboard";
            this.btnImportFromClipboard.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.V)));
            this.btnImportFromClipboard.Size = new System.Drawing.Size(383, 22);
            this.btnImportFromClipboard.Text = "درج محتوی از کلیپ بورد";
            this.btnImportFromClipboard.Click += new System.EventHandler(this.btnImportFromClipboard_Click);
            // 
            // btnImportFromClipboadStructuredPoem
            // 
            this.btnImportFromClipboadStructuredPoem.Name = "btnImportFromClipboadStructuredPoem";
            this.btnImportFromClipboadStructuredPoem.Size = new System.Drawing.Size(383, 22);
            this.btnImportFromClipboadStructuredPoem.Text = "درج شعر چند بندی از کلیپ بورد";
            this.btnImportFromClipboadStructuredPoem.Click += new System.EventHandler(this.btnImportFromClipboadStructuredPoem_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(380, 6);
            // 
            // chkEachlineOneverse
            // 
            this.chkEachlineOneverse.CheckOnClick = true;
            this.chkEachlineOneverse.Name = "chkEachlineOneverse";
            this.chkEachlineOneverse.Size = new System.Drawing.Size(383, 22);
            this.chkEachlineOneverse.Text = "شعری که درج می‌شود نیمایی یا آزاد است";
            // 
            // chkIgnoreBlankLines
            // 
            this.chkIgnoreBlankLines.CheckOnClick = true;
            this.chkIgnoreBlankLines.Name = "chkIgnoreBlankLines";
            this.chkIgnoreBlankLines.Size = new System.Drawing.Size(383, 22);
            this.chkIgnoreBlankLines.Text = "خطهای خالی نادیده گرفته شوند";
            // 
            // chkIgnoreShortLines
            // 
            this.chkIgnoreShortLines.CheckOnClick = true;
            this.chkIgnoreShortLines.Name = "chkIgnoreShortLines";
            this.chkIgnoreShortLines.Size = new System.Drawing.Size(383, 22);
            this.chkIgnoreShortLines.Text = "خطهای حاوی کمتر از 4 حرف نادیده گرفته شوند";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(380, 6);
            // 
            // btnDeletePoem
            // 
            this.btnDeletePoem.Name = "btnDeletePoem";
            this.btnDeletePoem.Size = new System.Drawing.Size(383, 22);
            this.btnDeletePoem.Text = "حذف شعر";
            this.btnDeletePoem.Click += new System.EventHandler(this.btnDeletePoem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 69);
            // 
            // btnNewLine
            // 
            this.btnNewLine.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewNormalLine,
            this.btnNewBandLine,
            this.btnNewBandVerse,
            this.btnNewSingleVerse,
            this.toolStripSeparator10,
            this.btnDeleteLine,
            this.btnDeleteAllLine});
            this.btnNewLine.Image = ((System.Drawing.Image)(resources.GetObject("btnNewLine.Image")));
            this.btnNewLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewLine.Name = "btnNewLine";
            this.btnNewLine.Size = new System.Drawing.Size(70, 66);
            this.btnNewLine.Text = "بیت جدید";
            this.btnNewLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNewLine.ButtonClick += new System.EventHandler(this.btnNewLine_Click);
            // 
            // btnNewNormalLine
            // 
            this.btnNewNormalLine.Name = "btnNewNormalLine";
            this.btnNewNormalLine.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.btnNewNormalLine.Size = new System.Drawing.Size(250, 22);
            this.btnNewNormalLine.Text = "بیت معمولی جدید";
            this.btnNewNormalLine.Click += new System.EventHandler(this.btnNewLine_Click);
            // 
            // btnNewBandLine
            // 
            this.btnNewBandLine.Name = "btnNewBandLine";
            this.btnNewBandLine.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.btnNewBandLine.Size = new System.Drawing.Size(250, 22);
            this.btnNewBandLine.Text = "بیت بند جدید";
            this.btnNewBandLine.Click += new System.EventHandler(this.btnNewBandLine_Click);
            // 
            // btnNewBandVerse
            // 
            this.btnNewBandVerse.Name = "btnNewBandVerse";
            this.btnNewBandVerse.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.btnNewBandVerse.Size = new System.Drawing.Size(250, 22);
            this.btnNewBandVerse.Text = "مصرع بند جدید";
            this.btnNewBandVerse.Click += new System.EventHandler(this.btnNewBandVerse_Click);
            // 
            // btnNewSingleVerse
            // 
            this.btnNewSingleVerse.Name = "btnNewSingleVerse";
            this.btnNewSingleVerse.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.btnNewSingleVerse.Size = new System.Drawing.Size(250, 22);
            this.btnNewSingleVerse.Text = "مصرع تنهای جدید (نیمایی/آزاد)";
            this.btnNewSingleVerse.Click += new System.EventHandler(this.btnNewSingleVerse_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(247, 6);
            // 
            // btnDeleteLine
            // 
            this.btnDeleteLine.Name = "btnDeleteLine";
            this.btnDeleteLine.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)));
            this.btnDeleteLine.Size = new System.Drawing.Size(250, 22);
            this.btnDeleteLine.Text = "حذف بیت جاری";
            this.btnDeleteLine.Click += new System.EventHandler(this.btnDeleteLine_Click);
            // 
            // btnDeleteAllLine
            // 
            this.btnDeleteAllLine.Name = "btnDeleteAllLine";
            this.btnDeleteAllLine.Size = new System.Drawing.Size(250, 22);
            this.btnDeleteAllLine.Text = "حذف تمام ابیات";
            this.btnDeleteAllLine.Click += new System.EventHandler(this.btnDeleteAllLine_Click);
            // 
            // btnTools
            // 
            this.btnTools.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMergeTwoTextColumns,
            this.btnReplaceInDb});
            this.btnTools.Image = global::ganjoor.Properties.Resources.tools;
            this.btnTools.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTools.Name = "btnTools";
            this.btnTools.Size = new System.Drawing.Size(64, 66);
            this.btnTools.Text = "ابزارها";
            this.btnTools.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnMergeTwoTextColumns
            // 
            this.btnMergeTwoTextColumns.Name = "btnMergeTwoTextColumns";
            this.btnMergeTwoTextColumns.Size = new System.Drawing.Size(271, 22);
            this.btnMergeTwoTextColumns.Text = "یک در میان چیدن خطوط دو ستون متنی";
            this.btnMergeTwoTextColumns.Click += new System.EventHandler(this.btnMergeTwoTextColumns_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Location = new System.Drawing.Point(0, 378);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip1.Size = new System.Drawing.Size(757, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "نوار وضعیت";
            // 
            // ganjoorView
            // 
            this.ganjoorView.AutoScroll = true;
            this.ganjoorView.BackColor = System.Drawing.SystemColors.Window;
            this.ganjoorView.Cursor = System.Windows.Forms.Cursors.Default;
            this.ganjoorView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ganjoorView.EditMode = true;
            this.ganjoorView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ganjoorView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ganjoorView.Location = new System.Drawing.Point(0, 69);
            this.ganjoorView.Margin = new System.Windows.Forms.Padding(48, 22, 48, 22);
            this.ganjoorView.MesraWidth = 0;
            this.ganjoorView.Name = "ganjoorView";
            this.ganjoorView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ganjoorView.Size = new System.Drawing.Size(757, 309);
            this.ganjoorView.TabIndex = 1;
            this.ganjoorView.OnPageChanged += new ganjoor.PageChangedEvent(this.ganjoorView_OnPageChanged);
            // 
            // btnReplaceInDb
            // 
            this.btnReplaceInDb.Name = "btnReplaceInDb";
            this.btnReplaceInDb.Size = new System.Drawing.Size(271, 22);
            this.btnReplaceInDb.Text = "جایگزینی در پایگاه داده‌ها";
            this.btnReplaceInDb.Click += new System.EventHandler(this.btnReplaceInDb_Click);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(757, 400);
            this.Controls.Add(this.ganjoorView);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStripMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Editor";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "ویرایشگر شعر";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.Editor_Activated);
            this.Deactivate += new System.EventHandler(this.Editor_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Editor_FormClosing);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMain;
        private GanjoorViewer ganjoorView;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripSplitButton btnNewCat;
        private System.Windows.Forms.ToolStripMenuItem btnEditCat;
        private System.Windows.Forms.ToolStripSplitButton btnNewPoem;
        private System.Windows.Forms.ToolStripMenuItem btnEditPoem;
        private System.Windows.Forms.ToolStripButton btnPreviousPoem;
        private System.Windows.Forms.ToolStripButton btnNextPoem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem btnDeletePoem;
        private System.Windows.Forms.ToolStripSplitButton btnNewPoet;
        private System.Windows.Forms.ToolStripMenuItem btnEditPoet;
        private System.Windows.Forms.ToolStripSplitButton btnNewLine;
        private System.Windows.Forms.ToolStripMenuItem btnNewNormalLine;
        private System.Windows.Forms.ToolStripMenuItem btnNewBandLine;
        private System.Windows.Forms.ToolStripMenuItem btnNewBandVerse;
        private System.Windows.Forms.ToolStripMenuItem btnNewCatSub;
        private System.Windows.Forms.ToolStripMenuItem btnDeleteCat;
        private System.Windows.Forms.ToolStripMenuItem btnNewPoemSub;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem btnNewPoetSub;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem btnDeletePoet;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem btnDeleteLine;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem btnExportCat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem btnExportPoet;
        private System.Windows.Forms.ToolStripMenuItem btnReOrderCat;
        private System.Windows.Forms.ToolStripMenuItem btnReOrderSubCat;
        private System.Windows.Forms.ToolStripMenuItem btnNewSingleVerse;
        private System.Windows.Forms.ToolStripMenuItem btnImportFromTextFile;
        private System.Windows.Forms.ToolStripMenuItem chkEachlineOneverse;
        private System.Windows.Forms.ToolStripMenuItem btnImportFromClipboard;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripMenuItem btnImportFromClipboadStructuredPoem;
        private System.Windows.Forms.ToolStripMenuItem chkIgnoreBlankLines;
        private System.Windows.Forms.ToolStripMenuItem chkIgnoreShortLines;
        private System.Windows.Forms.ToolStripMenuItem btnDeleteAllLine;
        private System.Windows.Forms.ToolStripSplitButton btnTools;
        private System.Windows.Forms.ToolStripMenuItem btnMergeTwoTextColumns;
        private System.Windows.Forms.ToolStripMenuItem btnReplaceInDb;
    }
}