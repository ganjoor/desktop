using System;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class ItemEditor : Form
    {
        public ItemEditor() : this(EditItemType.Poet)
        {
        }
        public ItemEditor(EditItemType ItemType)
            : this(ItemType, "", "")
        {
        }
        public ItemEditor(EditItemType ItemType, string caption, string itemname)
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
            else
                    if (ItemType == EditItemType.General)
            {
                this.Text = caption;
                this.lblCat.Text = itemname;
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = !string.IsNullOrEmpty(ItemName);
        }

        public string ItemName
        {
            get => txtName.Text.Trim();
            set => txtName.Text = value;
        }


    }
}
