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
            txtName.Text = ItemName;
            btnOK.Enabled = !string.IsNullOrEmpty(ItemName);
            if (ItemType == EditItemType.Category)
            {
                Text = "ویرایش مشخصات بخش";
                lblCat.Text = "نام بخش:";
            }
            else
                if (ItemType == EditItemType.Poem)
            {
                Text = "ویرایش مشخصات شعر";
                lblCat.Text = "نام شعر:";
            }
            else
                    if (ItemType == EditItemType.General)
            {
                Text = caption;
                lblCat.Text = itemname;
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
