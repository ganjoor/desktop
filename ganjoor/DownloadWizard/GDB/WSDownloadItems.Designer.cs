using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ganjoor
{
    partial class WSDownloadItems
    {
        private void InitializeComponent()
        {
            this.backgroundWorker = new BackgroundWorker();
            this.btnStop = new Button();
            this.lblMsg = new Label();
            this.pnlList = new Panel();
            this.SuspendLayout();
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // btnStop
            // 
            this.btnStop.Location = new Point(9, 9);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new Size(75, 23);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "توقف";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new EventHandler(this.btnStop_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = SystemColors.Window;
            this.lblMsg.BorderStyle = BorderStyle.FixedSingle;
            this.lblMsg.Dock = DockStyle.Top;
            this.lblMsg.Location = new Point(0, 0);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new Size(585, 40);
            this.lblMsg.TabIndex = 6;
            this.lblMsg.Text = "شروع دریافت ممکن است خیلی طول بکشد. لطفاً صبور باشید.";
            this.lblMsg.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlList
            // 
            this.pnlList.AutoScroll = true;
            this.pnlList.Dock = DockStyle.Fill;
            this.pnlList.Location = new Point(0, 40);
            this.pnlList.Name = "pnlList";
            this.pnlList.Padding = new Padding(25);
            this.pnlList.Size = new Size(585, 325);
            this.pnlList.TabIndex = 7;
            // 
            // WSDownloadItems
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.Controls.Add(this.pnlList);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblMsg);
            this.Name = "WSDownloadItems";
            this.ResumeLayout(false);

        }

        private BackgroundWorker backgroundWorker;
        private Button btnStop;
        private Label lblMsg;
        private Panel pnlList;
    }
}