using System;
using System.Windows.Forms;

namespace ganjoor.Audio_Support
{
    public partial class ConfirmAudioUpload : Form
    {
        public ConfirmAudioUpload(string profileName = "نام نمایه")
        {
            InitializeComponent();
            lblMessage.Text = $"آیا از ارسال خوانش انتخاب شده با نمایهٔ فعال «{profileName}» به سایت اطمینان دارید؟";
            chkCommentary.Checked = _Commentary;
        }

        private static bool _Commentary = false;

        public static bool Commentary 
        {
            get
            {
                return _Commentary;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _Commentary = chkCommentary.Checked;
            if (chkReplace.Checked)
            {
                DialogResult = DialogResult.Yes;
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
