using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class IDChanger : Form
    {
        public IDChanger()
        {
            InitializeComponent();
        }
        public int PoetID
        {
            set
            {
                ictlPoet.Value = value;
            }
            get
            {
                return (int)ictlPoet.Value;
            }
        }
        public int StartCatID
        {
            set
            {
                ictlCat.Value = value;
            }
            get
            {
                return (int)ictlCat.Value;
            }
        }
        public int StartPoemID
        {
            set
            {
                ictlPoem.Value = value;
            }
            get
            {
                return (int)ictlPoem.Value;
            }
        }

    }
}
