using System;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class SpaceSeparatedPoem : Form
    {
        public SpaceSeparatedPoem()
        {
            InitializeComponent();
            mainText.Focus();
        }

        public string ResulText
        {
            get
            {
                return result.Text;
            }
        }

        private void result_TextChanged(object sender, EventArgs e)
        {
            btnInsert.Enabled = !string.IsNullOrEmpty(result.Text.Trim());
        }

        private void mainText_TextChanged(object sender, EventArgs e)
        {
            btnConvert.Enabled = !string.IsNullOrEmpty(mainText.Text.Trim());
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            var bTab = chkTab.Checked;
            var bSpace = chkSpace.Checked;
            var txtLines = mainText.Text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var resultText = "";
            foreach (var txtLine in txtLines)
            {
                var txt = txtLine.Trim();
                if (txt.Length == 0)
                    continue;
                var bTabDoneIt = false;
                if (bTab)
                {
                    var tabSep = txt.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (tabSep.Length == 2)
                    {
                        resultText += tabSep[0].Trim() + "\r\n";
                        resultText += tabSep[1].Trim() + "\r\n";
                        bTabDoneIt = true;
                    }
                    else
                    {
                        var nIdx = txt.IndexOf('\t');
                        if (nIdx > 0)
                        {
                            resultText += txt.Substring(0, nIdx).Trim() + "\r\n";
                            resultText += txt.Substring(nIdx + 1).Trim() + "\r\n";
                            bTabDoneIt = true;
                        }
                    }
                }

                var bDone = bTabDoneIt;
                if (!bDone)
                {
                    var nIdx = txt.IndexOf("   ");
                    if (nIdx > 0)
                    {
                        resultText += txt.Substring(0, nIdx).Trim() + "\r\n";
                        resultText += txt.Substring(nIdx + 1).Trim() + "\r\n";
                        bDone = true;
                    }
                }

                if (!bDone)
                {
                    resultText += txt + "\r\n";
                    resultText += "\r\n";
                }
            }
            result.Text = resultText;
        }


    }
}
