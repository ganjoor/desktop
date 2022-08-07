using System.ComponentModel;
using System.Windows.Forms;

namespace ganjoor
{
    partial class GeneralTextImporter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralTextImporter));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtNextPoemStartText = new System.Windows.Forms.TextBox();
            this.lblMainCatText = new System.Windows.Forms.Label();
            this.lblComment = new System.Windows.Forms.Label();
            this.chkStartShort = new System.Windows.Forms.CheckBox();
            this.lblNextPoemStartTextComment = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(262, 131);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(123, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "انصراف";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(135, 131);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(123, 23);
            this.btnOK.TabIndex = 15;
            this.btnOK.Text = "انتخاب فایل";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtNextPoemStartText
            // 
            this.txtNextPoemStartText.Location = new System.Drawing.Point(123, 65);
            this.txtNextPoemStartText.Margin = new System.Windows.Forms.Padding(2);
            this.txtNextPoemStartText.Name = "txtNextPoemStartText";
            this.txtNextPoemStartText.Size = new System.Drawing.Size(135, 21);
            this.txtNextPoemStartText.TabIndex = 14;
            this.txtNextPoemStartText.Text = "غزل";
            // 
            // lblMainCatText
            // 
            this.lblMainCatText.AutoSize = true;
            this.lblMainCatText.Location = new System.Drawing.Point(11, 67);
            this.lblMainCatText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMainCatText.Name = "lblMainCatText";
            this.lblMainCatText.Size = new System.Drawing.Size(103, 13);
            this.lblMainCatText.TabIndex = 13;
            this.lblMainCatText.Text = "متن آغازین شعر بعد:";
            // 
            // lblComment
            // 
            this.lblComment.Location = new System.Drawing.Point(11, 9);
            this.lblComment.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(378, 71);
            this.lblComment.TabIndex = 12;
            this.lblComment.Text = resources.GetString("lblComment.Text");
            // 
            // chkStartShort
            // 
            this.chkStartShort.AutoSize = true;
            this.chkStartShort.Location = new System.Drawing.Point(12, 110);
            this.chkStartShort.Name = "chkStartShort";
            this.chkStartShort.Size = new System.Drawing.Size(296, 17);
            this.chkStartShort.TabIndex = 17;
            this.chkStartShort.Text = "خطوط با طول کمتر از ۱۰ نویسه را آغاز شعر بعد محسوب کن";
            this.chkStartShort.UseVisualStyleBackColor = true;
            // 
            // lblNextPoemStartTextComment
            // 
            this.lblNextPoemStartTextComment.AutoSize = true;
            this.lblNextPoemStartTextComment.Location = new System.Drawing.Point(127, 88);
            this.lblNextPoemStartTextComment.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNextPoemStartTextComment.Name = "lblNextPoemStartTextComment";
            this.lblNextPoemStartTextComment.Size = new System.Drawing.Size(212, 13);
            this.lblNextPoemStartTextComment.TabIndex = 18;
            this.lblNextPoemStartTextComment.Text = "متون متعدد را با سمیکالن (;) از هم جدا کنید.";
            // 
            // GeneralTextImporter
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(397, 158);
            this.Controls.Add(this.lblNextPoemStartTextComment);
            this.Controls.Add(this.chkStartShort);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtNextPoemStartText);
            this.Controls.Add(this.lblMainCatText);
            this.Controls.Add(this.lblComment);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GeneralTextImporter";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ورود اشعار از فایل متنی";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnCancel;
        private Button btnOK;
        private TextBox txtNextPoemStartText;
        private Label lblMainCatText;
        private Label lblComment;
        private CheckBox chkStartShort;
        private Label lblNextPoemStartTextComment;
    }
}