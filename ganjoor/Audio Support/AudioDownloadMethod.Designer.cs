
namespace ganjoor.Audio_Support
{
    partial class AudioDownloadMethod
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
            this.grp = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.rdAll = new System.Windows.Forms.RadioButton();
            this.rdSelected = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearchTerm = new System.Windows.Forms.TextBox();
            this.txtSelectedPoetOrCategory = new System.Windows.Forms.TextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.grp.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp
            // 
            this.grp.Controls.Add(this.btnSelect);
            this.grp.Controls.Add(this.txtSelectedPoetOrCategory);
            this.grp.Controls.Add(this.txtSearchTerm);
            this.grp.Controls.Add(this.label1);
            this.grp.Controls.Add(this.rdSelected);
            this.grp.Controls.Add(this.rdAll);
            this.grp.Location = new System.Drawing.Point(12, 12);
            this.grp.Name = "grp";
            this.grp.Size = new System.Drawing.Size(815, 166);
            this.grp.TabIndex = 0;
            this.grp.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(727, 194);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "انصراف";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(621, 194);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 30);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "تأیید";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // rdAll
            // 
            this.rdAll.Checked = true;
            this.rdAll.Location = new System.Drawing.Point(505, 35);
            this.rdAll.Name = "rdAll";
            this.rdAll.Size = new System.Drawing.Size(302, 24);
            this.rdAll.TabIndex = 0;
            this.rdAll.TabStop = true;
            this.rdAll.Text = "همهٔ خوانش‌ها به ترتیب نزولی انتشار";
            this.rdAll.UseVisualStyleBackColor = true;
            // 
            // rdSelected
            // 
            this.rdSelected.Location = new System.Drawing.Point(555, 75);
            this.rdSelected.Name = "rdSelected";
            this.rdSelected.Size = new System.Drawing.Size(252, 23);
            this.rdSelected.TabIndex = 1;
            this.rdSelected.Text = "خوانش‌های بخش انتخاب شده";
            this.rdSelected.UseVisualStyleBackColor = true;
            this.rdSelected.CheckedChanged += new System.EventHandler(this.rdSelected_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(459, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(334, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "نام خوانشگر یا متن شاعر حاوی این عبارت باشد:";
            // 
            // txtSearchTerm
            // 
            this.txtSearchTerm.Location = new System.Drawing.Point(6, 114);
            this.txtSearchTerm.Name = "txtSearchTerm";
            this.txtSearchTerm.Size = new System.Drawing.Size(447, 27);
            this.txtSearchTerm.TabIndex = 4;
            // 
            // txtSelectedPoetOrCategory
            // 
            this.txtSelectedPoetOrCategory.Location = new System.Drawing.Point(48, 73);
            this.txtSelectedPoetOrCategory.Name = "txtSelectedPoetOrCategory";
            this.txtSelectedPoetOrCategory.ReadOnly = true;
            this.txtSelectedPoetOrCategory.Size = new System.Drawing.Size(501, 27);
            this.txtSelectedPoetOrCategory.TabIndex = 2;
            // 
            // btnSelect
            // 
            this.btnSelect.Enabled = false;
            this.btnSelect.Location = new System.Drawing.Point(6, 71);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(40, 30);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = "...";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // AudioDownloadMethod
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(836, 241);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grp);
            this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AudioDownloadMethod";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "نحوهٔ دریافت فهرست خوانش‌ها";
            this.grp.ResumeLayout(false);
            this.grp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtSearchTerm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdSelected;
        private System.Windows.Forms.RadioButton rdAll;
        private System.Windows.Forms.TextBox txtSelectedPoetOrCategory;
        private System.Windows.Forms.Button btnSelect;
    }
}