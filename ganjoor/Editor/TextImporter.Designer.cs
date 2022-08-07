using System.ComponentModel;
using System.Windows.Forms;

namespace ganjoor
{
    partial class TextImporter
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
            this.lblComment = new System.Windows.Forms.Label();
            this.lblMainCatText = new System.Windows.Forms.Label();
            this.txtMainCat = new System.Windows.Forms.TextBox();
            this.lblSubCats = new System.Windows.Forms.Label();
            this.txtSubCats = new System.Windows.Forms.TextBox();
            this.lblParagraphs = new System.Windows.Forms.Label();
            this.lblV1 = new System.Windows.Forms.Label();
            this.lblV2 = new System.Windows.Forms.Label();
            this.lblMinVerseWords = new System.Windows.Forms.Label();
            this.numMaxVerse = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkTabularVerses = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxVerse)).BeginInit();
            this.SuspendLayout();
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(12, 9);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(461, 19);
            this.lblComment.TabIndex = 0;
            this.lblComment.Text = "این ابزار برای درون‌ریزی کتاب کشف الاسرار میبدی ایجاد شده است.";
            // 
            // lblMainCatText
            // 
            this.lblMainCatText.AutoSize = true;
            this.lblMainCatText.Location = new System.Drawing.Point(12, 48);
            this.lblMainCatText.Name = "lblMainCatText";
            this.lblMainCatText.Size = new System.Drawing.Size(161, 19);
            this.lblMainCatText.TabIndex = 1;
            this.lblMainCatText.Text = "متن سربخشها شامل:";
            // 
            // txtMainCat
            // 
            this.txtMainCat.Location = new System.Drawing.Point(180, 45);
            this.txtMainCat.Name = "txtMainCat";
            this.txtMainCat.Size = new System.Drawing.Size(201, 27);
            this.txtMainCat.TabIndex = 2;
            this.txtMainCat.Text = "- سورة";
            // 
            // lblSubCats
            // 
            this.lblSubCats.AutoSize = true;
            this.lblSubCats.Location = new System.Drawing.Point(12, 91);
            this.lblSubCats.Name = "lblSubCats";
            this.lblSubCats.Size = new System.Drawing.Size(172, 19);
            this.lblSubCats.TabIndex = 3;
            this.lblSubCats.Text = "عناوین زیربخشها شامل:";
            // 
            // txtSubCats
            // 
            this.txtSubCats.Location = new System.Drawing.Point(180, 91);
            this.txtSubCats.Multiline = true;
            this.txtSubCats.Name = "txtSubCats";
            this.txtSubCats.Size = new System.Drawing.Size(401, 188);
            this.txtSubCats.TabIndex = 4;
            this.txtSubCats.Text = "النوبة";
            // 
            // lblParagraphs
            // 
            this.lblParagraphs.AutoSize = true;
            this.lblParagraphs.Location = new System.Drawing.Point(12, 291);
            this.lblParagraphs.Name = "lblParagraphs";
            this.lblParagraphs.Size = new System.Drawing.Size(429, 19);
            this.lblParagraphs.TabIndex = 5;
            this.lblParagraphs.Text = "خطوط متن به طور پیش فرض پاراگراف متنی محسوب می‌شوند.";
            // 
            // lblV1
            // 
            this.lblV1.AutoSize = true;
            this.lblV1.Location = new System.Drawing.Point(12, 327);
            this.lblV1.Name = "lblV1";
            this.lblV1.Size = new System.Drawing.Size(509, 19);
            this.lblV1.TabIndex = 6;
            this.lblV1.Text = "اگر دو خط متوالی دارای کلمات کمتر از حداکثر کلمات مصرع را داشته باشند";
            // 
            // lblV2
            // 
            this.lblV2.AutoSize = true;
            this.lblV2.Location = new System.Drawing.Point(12, 361);
            this.lblV2.Name = "lblV2";
            this.lblV2.Size = new System.Drawing.Size(189, 19);
            this.lblV2.TabIndex = 7;
            this.lblV2.Text = "یک بیت محسوب می‌شوند.";
            // 
            // lblMinVerseWords
            // 
            this.lblMinVerseWords.AutoSize = true;
            this.lblMinVerseWords.Location = new System.Drawing.Point(12, 399);
            this.lblMinVerseWords.Name = "lblMinVerseWords";
            this.lblMinVerseWords.Size = new System.Drawing.Size(210, 19);
            this.lblMinVerseWords.TabIndex = 8;
            this.lblMinVerseWords.Text = "حداکثر تعداد کلمات یک مصرع:";
            // 
            // numMaxVerse
            // 
            this.numMaxVerse.Location = new System.Drawing.Point(228, 399);
            this.numMaxVerse.Name = "numMaxVerse";
            this.numMaxVerse.Size = new System.Drawing.Size(120, 27);
            this.numMaxVerse.TabIndex = 9;
            this.numMaxVerse.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(16, 472);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(185, 35);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "انتخاب فایل";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(207, 472);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(185, 35);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "انصراف";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chkTabularVerses
            // 
            this.chkTabularVerses.AutoSize = true;
            this.chkTabularVerses.Checked = true;
            this.chkTabularVerses.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTabularVerses.Location = new System.Drawing.Point(16, 432);
            this.chkTabularVerses.Name = "chkTabularVerses";
            this.chkTabularVerses.Size = new System.Drawing.Size(302, 23);
            this.chkTabularVerses.TabIndex = 12;
            this.chkTabularVerses.Text = "نیاز به بازچینی مصرعها (چینش جدولی)";
            this.chkTabularVerses.UseVisualStyleBackColor = true;
            // 
            // TextImporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(593, 539);
            this.Controls.Add(this.chkTabularVerses);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.numMaxVerse);
            this.Controls.Add(this.lblMinVerseWords);
            this.Controls.Add(this.lblV2);
            this.Controls.Add(this.lblV1);
            this.Controls.Add(this.lblParagraphs);
            this.Controls.Add(this.txtSubCats);
            this.Controls.Add(this.lblSubCats);
            this.Controls.Add(this.txtMainCat);
            this.Controls.Add(this.lblMainCatText);
            this.Controls.Add(this.lblComment);
            this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextImporter";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "درون ریزی از فایل متنی";
            ((System.ComponentModel.ISupportInitialize)(this.numMaxVerse)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblComment;
        private Label lblMainCatText;
        private TextBox txtMainCat;
        private Label lblSubCats;
        private TextBox txtSubCats;
        private Label lblParagraphs;
        private Label lblV1;
        private Label lblV2;
        private Label lblMinVerseWords;
        private NumericUpDown numMaxVerse;
        private Button btnOK;
        private Button btnCancel;
        private CheckBox chkTabularVerses;
    }
}