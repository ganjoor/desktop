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
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
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
