using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class ItemEditor : Form
    {
        public ItemEditor() : this(EditItemType.Poet)
        {
        }
        public ItemEditor(EditItemType ItemType)
        {
            InitializeComponent();
            txtName.Text = this.ItemName;
            btnOK.Enabled = !string.IsNullOrEmpty(ItemName);
            if (ItemType == EditItemType.Category)
            {
                this.Text = "ویرایش مشخصات بخش";
                this.lblCat.Text = "نام بخش:";
            }
            else
                if (ItemType == EditItemType.Poem)
                {
                    this.Text = "ویرایش مشخصات شعر";
                    this.lblCat.Text = "نام شعر:";
                }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = !string.IsNullOrEmpty(ItemName);
        }

        public string ItemName
        {
            get { return txtName.Text.Trim(); }
            set { txtName.Text = value; }
        }


    }
}
