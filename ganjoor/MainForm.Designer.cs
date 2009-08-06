namespace ganjoor
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
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.btnHome = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPreviousPoem = new System.Windows.Forms.ToolStripButton();
            this.btnNextPoem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnHistoryBack = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnViewInSite = new System.Windows.Forms.ToolStripButton();
            this.btnComments = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbout = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblDummy = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCurrentPage = new System.Windows.Forms.ToolStripStatusLabel();
            this.ganjoorView = new ganjoor.GanjoorViewer();
            this.btnCopyText = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMain.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnHome,
            this.toolStripSeparator1,
            this.btnPreviousPoem,
            this.btnNextPoem,
            this.toolStripSeparator2,
            this.btnHistoryBack,
            this.toolStripSeparator4,
            this.btnViewInSite,
            this.btnComments,
            this.toolStripSeparator3,
            this.btnPrint,
            this.toolStripSeparator5,
            this.btnSearch,
            this.toolStripSeparator7,
            this.btnAbout});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(672, 54);
            this.toolStripMain.TabIndex = 1;
            this.toolStripMain.Text = "نوار ابزار";
            // 
            // btnHome
            // 
            this.btnHome.Image = global::ganjoor.Properties.Resources.home_next;
            this.btnHome.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(36, 51);
            this.btnHome.Text = "خانه";
            this.btnHome.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 54);
            // 
            // btnPreviousPoem
            // 
            this.btnPreviousPoem.Enabled = false;
            this.btnPreviousPoem.Image = global::ganjoor.Properties.Resources.fast_forward;
            this.btnPreviousPoem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPreviousPoem.Name = "btnPreviousPoem";
            this.btnPreviousPoem.Size = new System.Drawing.Size(49, 51);
            this.btnPreviousPoem.Text = "شعر قبل";
            this.btnPreviousPoem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPreviousPoem.Click += new System.EventHandler(this.btnPreviousPoem_Click);
            // 
            // btnNextPoem
            // 
            this.btnNextPoem.Enabled = false;
            this.btnNextPoem.Image = global::ganjoor.Properties.Resources.rewind;
            this.btnNextPoem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNextPoem.Name = "btnNextPoem";
            this.btnNextPoem.Size = new System.Drawing.Size(48, 51);
            this.btnNextPoem.Text = "شعر بعد";
            this.btnNextPoem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNextPoem.Click += new System.EventHandler(this.btnNextPoem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 54);
            // 
            // btnHistoryBack
            // 
            this.btnHistoryBack.Image = global::ganjoor.Properties.Resources.repeat;
            this.btnHistoryBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHistoryBack.Name = "btnHistoryBack";
            this.btnHistoryBack.Size = new System.Drawing.Size(44, 51);
            this.btnHistoryBack.Text = "برگشت";
            this.btnHistoryBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnHistoryBack.Click += new System.EventHandler(this.btnHistoryBack_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 54);
            // 
            // btnViewInSite
            // 
            this.btnViewInSite.Image = global::ganjoor.Properties.Resources.firefox_alt;
            this.btnViewInSite.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnViewInSite.Name = "btnViewInSite";
            this.btnViewInSite.Size = new System.Drawing.Size(36, 51);
            this.btnViewInSite.Text = "مرور";
            this.btnViewInSite.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnViewInSite.Click += new System.EventHandler(this.btnViewInSite_Click);
            // 
            // btnComments
            // 
            this.btnComments.Enabled = false;
            this.btnComments.Image = global::ganjoor.Properties.Resources.comments;
            this.btnComments.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnComments.Name = "btnComments";
            this.btnComments.Size = new System.Drawing.Size(51, 51);
            this.btnComments.Text = "حاشیه‌ها";
            this.btnComments.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnComments.Click += new System.EventHandler(this.btnComments_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 54);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = global::ganjoor.Properties.Resources.printer;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(36, 51);
            this.btnPrint.Text = "چاپ";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 54);
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::ganjoor.Properties.Resources.database_search;
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(43, 51);
            this.btnSearch.Text = "جستجو";
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 54);
            // 
            // btnAbout
            // 
            this.btnAbout.Image = global::ganjoor.Properties.Resources.help;
            this.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.RightToLeftAutoMirrorImage = true;
            this.btnAbout.Size = new System.Drawing.Size(41, 51);
            this.btnAbout.Text = "درباره";
            this.btnAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCopyText,
            this.lblDummy,
            this.lblCurrentPage});
            this.statusStrip.Location = new System.Drawing.Point(0, 320);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip.Size = new System.Drawing.Size(672, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "نوار وضعیت";
            // 
            // lblDummy
            // 
            this.lblDummy.Name = "lblDummy";
            this.lblDummy.Size = new System.Drawing.Size(541, 17);
            this.lblDummy.Spring = true;
            // 
            // lblCurrentPage
            // 
            this.lblCurrentPage.Name = "lblCurrentPage";
            this.lblCurrentPage.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCurrentPage.Size = new System.Drawing.Size(29, 17);
            this.lblCurrentPage.Text = "خانه";
            // 
            // ganjoorView
            // 
            this.ganjoorView.AutoScroll = true;
            this.ganjoorView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ganjoorView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ganjoorView.Location = new System.Drawing.Point(0, 54);
            this.ganjoorView.Name = "ganjoorView";
            this.ganjoorView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ganjoorView.Size = new System.Drawing.Size(672, 266);
            this.ganjoorView.TabIndex = 0;
            this.ganjoorView.OnPageChanged += new ganjoor.PageChangedEvent(this.ganjoorView_OnPageChanged);
            // 
            // btnCopyText
            // 
            this.btnCopyText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCopyText.Image = ((System.Drawing.Image)(resources.GetObject("btnCopyText.Image")));
            this.btnCopyText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopyText.Name = "btnCopyText";
            this.btnCopyText.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnCopyText.RightToLeftAutoMirrorImage = true;
            this.btnCopyText.ShowDropDownArrow = false;
            this.btnCopyText.Size = new System.Drawing.Size(56, 20);
            this.btnCopyText.Text = "کپی متن";
            this.btnCopyText.Click += new System.EventHandler(this.btnCopyText_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 342);
            this.Controls.Add(this.ganjoorView);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStripMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "گنجور رومیزی";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GanjoorViewer ganjoorView;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton btnHome;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnPreviousPoem;
        private System.Windows.Forms.ToolStripButton btnNextPoem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnViewInSite;
        private System.Windows.Forms.ToolStripButton btnComments;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripStatusLabel lblDummy;
        private System.Windows.Forms.ToolStripStatusLabel lblCurrentPage;
        private System.Windows.Forms.ToolStripButton btnHistoryBack;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnAbout;
        private System.Windows.Forms.ToolStripButton btnSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripDropDownButton btnCopyText;





    }
}

