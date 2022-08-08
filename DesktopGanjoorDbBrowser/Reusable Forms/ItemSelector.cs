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
            Text = strCaption;
            if (items != null)
                lstItems.Items.AddRange(items);
            if (selectedItem != null)
                lstItems.SelectedItem = selectedItem;
            else
                if (lstItems.Items.Count > 0)
                lstItems.SelectedIndex = 0;
        }


        public object SelectedItem => lstItems.SelectedItem;

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("لطفاً ردیفی را انتخاب کنید.");
                DialogResult = DialogResult.None;
            }
        }

        private void lstItems_DoubleClick(object sender, EventArgs e)
        {
            if (SelectedItem != null)
                DialogResult = DialogResult.OK;
        }

    }
}
