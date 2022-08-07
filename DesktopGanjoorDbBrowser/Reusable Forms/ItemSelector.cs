using System;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class ItemSelector : Form
    {
        public ItemSelector()
            :
            this("انتخاب ...", null, null)
        {
        }

        public ItemSelector(string strCaption, object[] items, object selectedItem)
        {
            InitializeComponent();
            this.Text = strCaption;
            if (items != null)
                lstItems.Items.AddRange(items);
            if (selectedItem != null)
                lstItems.SelectedItem = selectedItem;
            else
                if (lstItems.Items.Count > 0)
                lstItems.SelectedIndex = 0;
        }


        public object SelectedItem
        {
            get
            {
                return lstItems.SelectedItem;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.SelectedItem == null)
            {
                MessageBox.Show("لطفاً ردیفی را انتخاب کنید.");
                DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }

        private void lstItems_DoubleClick(object sender, EventArgs e)
        {
            if (this.SelectedItem != null)
                DialogResult = System.Windows.Forms.DialogResult.OK;
        }

    }
}
