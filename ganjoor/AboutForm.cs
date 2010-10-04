using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace ganjoor
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void lnkGanjoorOnSFNet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://ganjoor.sourceforge.net");
        }

        private const int DTCLMN_NAME = 0;
        private const int DTCLMN_LINK = 1;
        private const int DTCLMN_SEC = 2;
        private const int DTCLMN_MORE = 3;
        private static string[][] Contributers = new string[][]
        {
            //1-نام
            //2-لینک نام
            //3- بخش
            //4- لینک بخش

            new string[] {"علی پی‌سپار", "", "سهراب سپهری", "http://blog.ganjoor.net/1389/05/06/dg-1-86/"},
            new string[] {"علی پی‌سپار", "", "امام خمینی", "http://blog.ganjoor.net/1389/05/12/dg-emam/"},
            new string[] {"مهرداد بیات", "http://eshghast.persianblog.ir", "کسایی مروزی", "http://blog.ganjoor.net/1389/05/14/kesayee-marvzi/"},
            new string[] {"آزاده رستمی", "http://asmanii.blogfa.com", "فروغ فرخزاد", "http://blog.ganjoor.net/1389/05/27/foroogh-dg/"},
            new string[] {"الف. رسته", "", "سیمین بهبهانی", "http://blog.ganjoor.net/1389/06/31/dg-simin/"},

        };

        private const int GRDCLMN_NAME = 0;
        private const int GRDCLMN_SEC = 1;
        private const int GRDCLMN_MORE = 2;


        private void AboutForm_Load(object sender, EventArgs e)
        {
            lblAppVersion.Text =
                "ویرایش " + Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." + Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();


            int num = Contributers.GetLength(0);
            for (int i = 0; i < num; i++)
            {
                int RowIndex = grdContributers.Rows.Add();
                grdContributers.Rows[RowIndex].Cells[GRDCLMN_NAME].Value = Contributers[i][DTCLMN_NAME];
                grdContributers.Rows[RowIndex].Cells[GRDCLMN_SEC].Value = Contributers[i][DTCLMN_SEC];
                grdContributers.Rows[RowIndex].Cells[GRDCLMN_MORE].Value = "کلیک کنید";
            }
            
        }

        private void lnkIcons_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://dryicons.com/free-icons/preview/aesthetica/");            
        }

        private void lnkHamidReza_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.gozir.com/contact/");            
        }

        private void lnkSources_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://ganjoor.net/sources/");                        
        }

        private void lnkIconsEditor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://dryicons.com/free-icons/preview/grace-icons-set/");            
        }

        private void grdContributers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == GRDCLMN_NAME)
            {
                string link = Contributers[e.RowIndex][DTCLMN_LINK];
                if (!string.IsNullOrEmpty(link))
                    Process.Start(link);
            }
            else
                if (e.ColumnIndex == GRDCLMN_MORE)
                {
                    string link = Contributers[e.RowIndex][DTCLMN_MORE];
                    if (!string.IsNullOrEmpty(link))
                        Process.Start(link);
                }
        }
    }
}
