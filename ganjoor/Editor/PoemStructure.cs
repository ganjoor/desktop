using System.Windows.Forms;

namespace ganjoor
{
    public partial class PoemStructure : Form
    {
        public PoemStructure()
        {
            InitializeComponent();
        }
        public int LinesCount
        {
            get
            {
                return (int)numLineCount.Value;
            }
        }
        public bool FullLine
        {
            get
            {
                return chkFullLine.Checked;
            }
        }
    }
}
