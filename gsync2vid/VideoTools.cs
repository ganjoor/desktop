using ganjoor.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gsync2vid
{
    public partial class VideoTools : Form
    {
        public VideoTools()
        {
            InitializeComponent();
        }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        private void btnCut_Click(object sender, EventArgs e)
        {
            StartTime = txtStart.Text;
            EndTime = txtEnd.Text;

            DateTime dtStartTime;
            if (!DateTime.TryParse(StartTime, out dtStartTime))
            {
                GMessageBox.SayError("زمان شروع نامعتبر است.");
                DialogResult = DialogResult.None;
                return;
            }
            
            DateTime dtEndTime;
            if (!DateTime.TryParse(EndTime, out dtEndTime))
            {
                GMessageBox.SayError("زمان پایان نامعتبر است.");
                DialogResult = DialogResult.None;
                return;
            }

            if(dtEndTime <= dtStartTime)
            {
                GMessageBox.SayError("زمان پایان باید بعد از زمان شروع باشد.");
                DialogResult = DialogResult.None;
                return;
            }

        }
    }
}
