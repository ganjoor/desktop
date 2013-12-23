using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
