using System.ComponentModel;
using System.Windows.Forms;

namespace ganjoor
{
    partial class MrgTwoClmns
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clmn1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.clmn2 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.result = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkAutomaticFocus = new System.Windows.Forms.CheckBox();
            this.chkIgnoreBlankLines = new System.Windows.Forms.CheckBox();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnMerge = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.clmn1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 50);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(368, 718);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ستون اول:";
            // 
            // clmn1
            // 
            this.clmn1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clmn1.Location = new System.Drawing.Point(4, 24);
            this.clmn1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clmn1.Multiline = true;
            this.clmn1.Name = "clmn1";
            this.clmn1.Size = new System.Drawing.Size(360, 690);
            this.clmn1.TabIndex = 0;
            this.clmn1.TextChanged += new System.EventHandler(this.clmn1_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.clmn2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(368, 50);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(368, 718);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ستون دوم:";
            // 
            // clmn2
            // 
            this.clmn2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clmn2.Location = new System.Drawing.Point(4, 24);
            this.clmn2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clmn2.Multiline = true;
            this.clmn2.Name = "clmn2";
            this.clmn2.Size = new System.Drawing.Size(360, 690);
            this.clmn2.TabIndex = 0;
            this.clmn2.TextChanged += new System.EventHandler(this.clmn1_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.result);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(736, 50);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(368, 718);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "نتیجه:";
            // 
            // result
            // 
            this.result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.result.Location = new System.Drawing.Point(4, 24);
            this.result.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.result.Multiline = true;
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(360, 690);
            this.result.TabIndex = 0;
            this.result.TextChanged += new System.EventHandler(this.result_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkAutomaticFocus);
            this.panel1.Controls.Add(this.chkIgnoreBlankLines);
            this.panel1.Controls.Add(this.btnInsert);
            this.panel1.Controls.Add(this.btnMerge);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1101, 50);
            this.panel1.TabIndex = 1;
            // 
            // chkAutomaticFocus
            // 
            this.chkAutomaticFocus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAutomaticFocus.AutoSize = true;
            this.chkAutomaticFocus.Checked = true;
            this.chkAutomaticFocus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutomaticFocus.Location = new System.Drawing.Point(493, 12);
            this.chkAutomaticFocus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkAutomaticFocus.Name = "chkAutomaticFocus";
            this.chkAutomaticFocus.Size = new System.Drawing.Size(181, 25);
            this.chkAutomaticFocus.TabIndex = 3;
            this.chkAutomaticFocus.Text = "تغییر خودکار فوکوس ";
            this.chkAutomaticFocus.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreBlankLines
            // 
            this.chkIgnoreBlankLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIgnoreBlankLines.AutoSize = true;
            this.chkIgnoreBlankLines.Checked = true;
            this.chkIgnoreBlankLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIgnoreBlankLines.Location = new System.Drawing.Point(673, 14);
            this.chkIgnoreBlankLines.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkIgnoreBlankLines.Name = "chkIgnoreBlankLines";
            this.chkIgnoreBlankLines.Size = new System.Drawing.Size(253, 25);
            this.chkIgnoreBlankLines.TabIndex = 2;
            this.chkIgnoreBlankLines.Text = "خطوط خالی نادیده گرفته شوند";
            this.chkIgnoreBlankLines.UseVisualStyleBackColor = true;
            // 
            // btnInsert
            // 
            this.btnInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInsert.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnInsert.Enabled = false;
            this.btnInsert.Location = new System.Drawing.Point(278, 4);
            this.btnInsert.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(200, 34);
            this.btnInsert.TabIndex = 1;
            this.btnInsert.Text = "درج نتیجه در ویرایشگر";
            this.btnInsert.UseVisualStyleBackColor = true;
            // 
            // btnMerge
            // 
            this.btnMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMerge.Enabled = false;
            this.btnMerge.Location = new System.Drawing.Point(934, 9);
            this.btnMerge.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(148, 34);
            this.btnMerge.TabIndex = 0;
            this.btnMerge.Text = "یک در میان چیدن";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // MrgTwoClmns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1101, 768);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MrgTwoClmns";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "یک در میان چیدن خطوط دو ستون متنی";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private TextBox clmn1;
        private GroupBox groupBox2;
        private TextBox clmn2;
        private GroupBox groupBox3;
        private TextBox result;
        private Panel panel1;
        private CheckBox chkIgnoreBlankLines;
        private Button btnInsert;
        private Button btnMerge;
        private CheckBox chkAutomaticFocus;
    }
}