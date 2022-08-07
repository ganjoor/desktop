using System.Windows.Forms;

namespace ganjoor
{
    public partial class WaitMsg : Form
    {
        public WaitMsg(string strMsg)
        {
            InitializeComponent();
            lblMsg.Text = strMsg;
        }
    }
}
