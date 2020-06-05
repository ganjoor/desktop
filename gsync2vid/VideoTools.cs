using ganjoor.Utilities;
using gsync2vid.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "MP4 Files (*.mp4)|*.mp4";
                if(dlg.ShowDialog(this) == DialogResult.OK)
                {
                    foreach(string file in dlg.FileNames)
                    {
                        lstVideos.Items.Add(file);
                    }
                }
            }
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "MP4 Files (*.mp4)|*.mp4";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    List<string> lines = new List<string>();
                    foreach (string path in lstVideos.Items)
                    {
                        lines.Add($"file '{path}'");
                    }

                    string strTempList = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".txt");
                    File.WriteAllLines(strTempList, lines.ToArray());

                    string outFileName = dlg.FileName;

                    string ffmpegPath = Settings.Default.FFmpegPath;

                    string cmdArgs = $"-f concat -safe 0 -i \"{strTempList}\" -c copy \"{outFileName}\"";

                    ProcessStartInfo ps = new ProcessStartInfo
                        (
                        Path.Combine(ffmpegPath, "ffmpeg.exe")
                        ,
                        cmdArgs
                        );

                    ps.UseShellExecute = false;

                    var ffmpegPs = Process.Start(ps);

                    ffmpegPs.WaitForExit();

                    File.Delete(strTempList);

                    if (File.Exists(outFileName))
                    {
                        if (MessageBox.Show("آیا مایلید فایل خروجی را مشاهده کنید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign) == DialogResult.Yes)
                        {
                            Process.Start(outFileName);
                        }
                    }
                    else
                    {
                        MessageBox.Show("فایل خروجی ایجاد نشد.", "خطا", MessageBoxButtons.OK);
                    }

                }
            }
        }
    }
}
