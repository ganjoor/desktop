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
            this.txtMemo.Text = memoText;
        }
        public string MemoText
        {
            get => this.txtMemo.Text;
            set => this.txtMemo.Text = value;
        }
    }
}
