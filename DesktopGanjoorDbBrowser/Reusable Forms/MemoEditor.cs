using System.Windows.Forms;

namespace ganjoor
{
    public partial class MemoEditor : Form
    {
        public MemoEditor()
        {
            InitializeComponent();
        }
        public MemoEditor(string memoText)
            : this()
        {
            txtMemo.Text = memoText;
        }
        public string MemoText
        {
            get => txtMemo.Text;
            set => txtMemo.Text = value;
        }
    }
}
