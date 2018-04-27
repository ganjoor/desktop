using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ganjoor.Properties;

namespace ganjoor
{
    public partial class TextExporter : Form
    {
        public TextExporter()
        {
            InitializeComponent();

            cmbTextExportLevel.SelectedIndex = Settings.Default.ExportLevel == "Poet" ? 0 : Settings.Default.ExportLevel == "Cat" ? 1 : 2;


            chkPoet.Checked = Settings.Default.ExportPoetName;
            txtPoetSeparator.Text = Settings.Default.ExportPoetSep;

            chkCat.Checked = Settings.Default.ExportCatName;
            txtCatSeparator.Text = Settings.Default.ExportCatSep;

            chkPoem.Checked = Settings.Default.ExportPoemName;
            txtPoemSeparator.Text = Settings.Default.ExportPoemSep;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            Settings.Default.ExportLevel  = cmbTextExportLevel.SelectedIndex == 0 ? "Poet" : cmbTextExportLevel.SelectedIndex == 1 ? "Cat"  : "Poem";

            Settings.Default.ExportPoetName = chkPoet.Checked;
            Settings.Default.ExportPoetSep = txtPoetSeparator.Text;

            Settings.Default.ExportCatName = chkCat.Checked;
            Settings.Default.ExportCatSep = txtCatSeparator.Text;

            Settings.Default.ExportPoemName = chkPoem.Checked;
            Settings.Default.ExportPoemSep = txtPoemSeparator.Text;

            Settings.Default.Save();

        }

        private void cmbTextExportLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cmbTextExportLevel.SelectedIndex)
            {
                case 0:
                    lblExportLevelComment.Text = "تمام اشعار شاعر در یک فایل متنی گنجانده می‌شود.";
                    break;
                case 1:
                    lblExportLevelComment.Text = "برای هر بخش یک پوشه ایجاد می‌شود و اگر شعری در خود آن بخش باشد در فایل متنی همنام در مسیر پوشه گنجانده می‌شود. این فرایند برای تمام زیربخشها تکرار می‌شود.";
                    break;
                default:
                    lblExportLevelComment.Text = "اشعار هر بخش در پوشه آن بخش در فایلهای جداگانه ذخیره می‌شود.";
                    break;
            }
        }
    }
}
