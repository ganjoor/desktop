using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
            get
            {
                return this.txtMemo.Text;
            }
            set
            {
                this.txtMemo.Text = value;
            }
        }
    }
}
