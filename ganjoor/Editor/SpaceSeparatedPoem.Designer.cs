namespace ganjoor
{
    partial class SpaceSeparatedPoem
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkTab = new System.Windows.Forms.CheckBox();
            this.chkSpace = new System.Windows.Forms.CheckBox();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.grpResult = new System.Windows.Forms.GroupBox();
            this.result = new System.Windows.Forms.TextBox();
            this.grpUnFormattedText = new System.Windows.Forms.GroupBox();
            this.mainText = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.grpResult.SuspendLayout();
            this.grpUnFormattedText.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkTab);
            this.panel1.Controls.Add(this.chkSpace);
            this.panel1.Controls.Add(this.btnInsert);
            this.panel1.Controls.Add(this.btnConvert);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(735, 33);
            this.panel1.TabIndex = 2;
            // 
            // chkTab
            // 
            this.chkTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTab.AutoSize = true;
            this.chkTab.Checked = true;
            this.chkTab.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTab.Location = new System.Drawing.Point(406, 8);
            this.chkTab.Name = "chkTab";
            this.chkTab.Size = new System.Drawing.Size(44, 17);
            this.chkTab.TabIndex = 3;
            this.chkTab.Text = "Tab";
            this.chkTab.UseVisualStyleBackColor = true;
            // 
            // chkSpace
            // 
            this.chkSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSpace.AutoSize = true;
            this.chkSpace.Checked = true;
            this.chkSpace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSpace.Location = new System.Drawing.Point(522, 9);
            this.chkSpace.Name = "chkSpace";
            this.chkSpace.Size = new System.Drawing.Size(96, 17);
            this.chkSpace.TabIndex = 2;
            this.chkSpace.Text = "بیش از ۲ فاصله";
            this.chkSpace.UseVisualStyleBackColor = true;
            // 
            // btnInsert
            // 
            this.btnInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInsert.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnInsert.Enabled = false;
            this.btnInsert.Location = new System.Drawing.Point(186, 3);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(133, 23);
            this.btnInsert.TabIndex = 1;
            this.btnInsert.Text = "درج نتیجه در ویرایشگر";
            this.btnInsert.UseVisualStyleBackColor = true;
            // 
            // btnConvert
            // 
            this.btnConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConvert.Enabled = false;
            this.btnConvert.Location = new System.Drawing.Point(624, 6);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(99, 23);
            this.btnConvert.TabIndex = 0;
            this.btnConvert.Text = "تبدیل";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // grpResult
            // 
            this.grpResult.Controls.Add(this.result);
            this.grpResult.Dock = System.Windows.Forms.DockStyle.Right;
            this.grpResult.Location = new System.Drawing.Point(490, 33);
            this.grpResult.Name = "grpResult";
            this.grpResult.Size = new System.Drawing.Size(245, 493);
            this.grpResult.TabIndex = 5;
            this.grpResult.TabStop = false;
            this.grpResult.Text = "نتیجه:";
            // 
            // result
            // 
            this.result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.result.Location = new System.Drawing.Point(3, 17);
            this.result.Multiline = true;
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(239, 473);
            this.result.TabIndex = 0;
            this.result.TextChanged += new System.EventHandler(this.result_TextChanged);
            // 
            // grpUnFormattedText
            // 
            this.grpUnFormattedText.Controls.Add(this.mainText);
            this.grpUnFormattedText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpUnFormattedText.Location = new System.Drawing.Point(0, 33);
            this.grpUnFormattedText.Name = "grpUnFormattedText";
            this.grpUnFormattedText.Size = new System.Drawing.Size(490, 493);
            this.grpUnFormattedText.TabIndex = 3;
            this.grpUnFormattedText.TabStop = false;
            this.grpUnFormattedText.Text = "متن:";
            // 
            // mainText
            // 
            this.mainText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainText.Location = new System.Drawing.Point(3, 17);
            this.mainText.Multiline = true;
            this.mainText.Name = "mainText";
            this.mainText.Size = new System.Drawing.Size(484, 473);
            this.mainText.TabIndex = 0;
            this.mainText.TextChanged += new System.EventHandler(this.mainText_TextChanged);
            // 
            // SpaceSeparatedPoem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 526);
            this.Controls.Add(this.grpUnFormattedText);
            this.Controls.Add(this.grpResult);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Name = "SpaceSeparatedPoem";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تبدیل شعر با مصرعهای جدا شده با بیش از ۲ فضای خالی یا Tab";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpResult.ResumeLayout(false);
            this.grpResult.PerformLayout();
            this.grpUnFormattedText.ResumeLayout(false);
            this.grpUnFormattedText.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkTab;
        private System.Windows.Forms.CheckBox chkSpace;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.GroupBox grpResult;
        private System.Windows.Forms.TextBox result;
        private System.Windows.Forms.GroupBox grpUnFormattedText;
        private System.Windows.Forms.TextBox mainText;
    }
}