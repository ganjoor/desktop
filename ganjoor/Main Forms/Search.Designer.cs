using System.ComponentModel;
using System.Windows.Forms;

namespace ganjoor
{
    partial class Search
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
            this.txtPhrase = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numItemsInPage = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbPoets = new System.Windows.Forms.ComboBox();
            this.labelSearchType = new System.Windows.Forms.Label();
            this.comboBoxSearchType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxSearchLocationType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numItemsInPage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "متن:";
            // 
            // txtPhrase
            // 
            this.txtPhrase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhrase.Location = new System.Drawing.Point(59, 7);
            this.txtPhrase.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPhrase.Name = "txtPhrase";
            this.txtPhrase.Size = new System.Drawing.Size(274, 21);
            this.txtPhrase.TabIndex = 1;
            this.txtPhrase.TextChanged += new System.EventHandler(this.txtPhrase_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(9, 150);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 26);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "بگرد";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(74, 150);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 26);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "انصراف";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 120);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "حداکثر تعداد نتایج در هر صفحه:";
            // 
            // numItemsInPage
            // 
            this.numItemsInPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numItemsInPage.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numItemsInPage.Location = new System.Drawing.Point(299, 120);
            this.numItemsInPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numItemsInPage.Name = "numItemsInPage";
            this.numItemsInPage.Size = new System.Drawing.Size(34, 21);
            this.numItemsInPage.TabIndex = 9;
            this.numItemsInPage.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 91);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "محدودهٔ جستجو:";
            // 
            // cmbPoets
            // 
            this.cmbPoets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPoets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPoets.FormattingEnabled = true;
            this.cmbPoets.Location = new System.Drawing.Point(93, 91);
            this.cmbPoets.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbPoets.Name = "cmbPoets";
            this.cmbPoets.Size = new System.Drawing.Size(240, 21);
            this.cmbPoets.TabIndex = 7;
            // 
            // labelSearchType
            // 
            this.labelSearchType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSearchType.AutoSize = true;
            this.labelSearchType.Location = new System.Drawing.Point(6, 33);
            this.labelSearchType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSearchType.Name = "labelSearchType";
            this.labelSearchType.Size = new System.Drawing.Size(47, 13);
            this.labelSearchType.TabIndex = 2;
            this.labelSearchType.Text = "نوع متن:";
            // 
            // comboBoxSearchType
            // 
            this.comboBoxSearchType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSearchType.FormattingEnabled = true;
            this.comboBoxSearchType.Items.AddRange(new object[] {
            "هر نوع",
            "مصرع اول",
            "مصرع دوم",
            "مصرع اول یا تنهای ابیات ترجیع یا ترکیب",
            "مصرع دوم ابیات ترجیع یا ترکیب",
            "شعرهای نیمایی یا آزاد",
            "نثر"});
            this.comboBoxSearchType.Location = new System.Drawing.Point(59, 33);
            this.comboBoxSearchType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBoxSearchType.Name = "comboBoxSearchType";
            this.comboBoxSearchType.Size = new System.Drawing.Size(274, 21);
            this.comboBoxSearchType.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 62);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "روش جستجو:";
            // 
            // comboBoxSearchLocationType
            // 
            this.comboBoxSearchLocationType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSearchLocationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSearchLocationType.FormattingEnabled = true;
            this.comboBoxSearchLocationType.Items.AddRange(new object[] {
            "هر جا",
            "شروع",
            "پایان"});
            this.comboBoxSearchLocationType.Location = new System.Drawing.Point(81, 62);
            this.comboBoxSearchLocationType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBoxSearchLocationType.Name = "comboBoxSearchLocationType";
            this.comboBoxSearchLocationType.Size = new System.Drawing.Size(252, 21);
            this.comboBoxSearchLocationType.TabIndex = 5;
            // 
            // Search
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(339, 189);
            this.Controls.Add(this.comboBoxSearchLocationType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxSearchType);
            this.Controls.Add(this.labelSearchType);
            this.Controls.Add(this.cmbPoets);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numItemsInPage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtPhrase);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Search";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "جستجو";
            this.Load += new System.EventHandler(this.Search_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numItemsInPage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox txtPhrase;
        private Button btnSearch;
        private Button btnCancel;
        private Label label2;
        private NumericUpDown numItemsInPage;
        private Label label3;
        private ComboBox cmbPoets;
        private Label labelSearchType;
        private ComboBox comboBoxSearchType;
        private Label label4;
        private ComboBox comboBoxSearchLocationType;
    }
}