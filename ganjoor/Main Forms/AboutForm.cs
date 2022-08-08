using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

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
            LaunchUrl("http://dg.ganjoor.net");
        }

        private const int DTCLMN_NAME = 0;
        private const int DTCLMN_LINK = 1;
        private const int DTCLMN_SEC = 2;
        private const int DTCLMN_MORE = 3;
        private static string[][] Contributers = {
            //1-نام
            //2-لینک نام
            //3- بخش
            //4- لینک بخش
            new[] {"سایت رسمی شاملو", "http://www.shamlou.org", "احمد شاملو", "https://blog.ganjoor.net/1389/02/24/dg-1-83/"},
            new[] {"علی پی‌سپار", "", "سهراب سپهری", "https://blog.ganjoor.net/1389/05/06/dg-1-86/"},
            new[] {"علی پی‌سپار", "", "امام خمینی", "https://blog.ganjoor.net/1389/05/12/dg-emam/"},
            new[] {"مهرداد بیات", "http://eshghast.persianblog.ir", "کسایی مروزی", "https://blog.ganjoor.net/1389/05/14/kesayee-marvzi/"},
            new[] {"آزاده رستمی", "http://asmanii.blogfa.com", "فروغ فرخزاد", "https://blog.ganjoor.net/1389/05/27/foroogh-dg/"},
            new[] {"الف. رسته", "", "سیمین بهبهانی", "https://blog.ganjoor.net/1389/06/31/dg-simin/"},
            new[] {"الف. رسته", "", "عرفی شیرازی", "https://blog.ganjoor.net/1389/07/14/orfi-shirazi/"},
            new[] {"رضا رنجبر", "", "رضی‌الدین آرتیمانی", "https://blog.ganjoor.net/1389/07/16/artimani/"},
            new[] {"علی پی‌سپار", "", "ملاهادی سبزواری", "https://blog.ganjoor.net/1389/07/23/dg-2-1/"},
            new[] {"مهران صمدنژاد", "", "نجمه زارع", "https://blog.ganjoor.net/1389/08/26/najmezare/"},
            new[] {"سجاد مهرابی", "", "سیدحمیدرضا برقعی", "https://blog.ganjoor.net/1389/10/10/borghaee/"},
            new[] {"مهران صمدنژاد", "", "خلیل‌الله خلیلی", "https://blog.ganjoor.net/1389/10/17/khalili-robaee/"},
            new[] {"علی پی‌سپار", "", "هلالی جغتایی", "https://blog.ganjoor.net/1389/10/28/helali/"},
            new[] {"کامران مشایخی", "", "اشعار ترکی شهریار", "https://blog.ganjoor.net/1390/02/02/shahriar-turki/"},
            new[] {"کامران مشایخی", "", "گزیدهٔ اشعار ترکی شاه اسماعیل صفوی", "https://blog.ganjoor.net/1390/02/11/shah-esmaeel-torki/"},
            new[] {"الف. رسته", "", "قسمت دوم حیدربابای استاد شهریار", "https://blog.ganjoor.net/1391/02/16/heydarbaba2/"},
            new[] {"سیاوش جعفری", "", "گرشاسپ‌نامهٔ اسدی توسی", "https://blog.ganjoor.net/1391/02/16/asadi-garshaspname/"},
            new[] {"محمودرضا رجایی", "", "حدیقهٔ سنایی", "https://blog.ganjoor.net/1391/07/03/hadighe/"},
            new[] {"محمودرضا رجایی", "", "کشف‌المحجوب هجویری", "https://blog.ganjoor.net/1391/07/08/kashf-ol-mahjoob/"},
            new[] {"عباس معاذاللهی", "", "شاطر عباس صبوحی", "https://ganjoor.net/sources/shaterabbas/"},
            new[] {"علی پی‌سپار", "", "رباعیات مهستی گنجوی", "https://blog.ganjoor.net/1391/08/26/mahsati/"},
            new[] {"محمودرضا رجایی", "", "ترانه‌های خیام به اهتمام صادق هدایت", "https://blog.ganjoor.net/1391/09/03/khayyam-hedayat/"},
            new[] {"محمد نصیری", "", "قرآن کریم", "https://blog.ganjoor.net/1391/09/10/dg-2-54/"},
            new[] {"مهران صمدنژاد", "", "محمدعلی بهمنی", "https://blog.ganjoor.net/1391/09/10/dg-2-54/"},
            new[] {"مهران صمدنژاد", "", "مهدی سهیلی", "https://blog.ganjoor.net/1391/09/10/dg-2-54/"},
            new[] {"بهروز ثروتی", "", "عبدالقهار عاصی", "https://blog.ganjoor.net/1391/09/10/abdulqahar-aasee/"},
            new[] {"سعید رستگار", "", "باقر فداغی لارستانی", "https://blog.ganjoor.net/1392/01/03/bagher-fedaghi/"},
            new[] {"الف. رسته", "", "بابا افضل کاشانی", "https://blog.ganjoor.net/1392/02/02/baba-afzal-kashani/"},
            new[] {"اسماعیل ابراهیمی", "", "جبار محمدی (الیار)", "https://blog.ganjoor.net/1392/05/08/elyaar/"},
            new[] {"الف. رسته", "", "صادق سرمد", "https://blog.ganjoor.net/1392/05/25/saadegh-sarmad/"},
            new[] {"کامران مشایخی", "", "گزیدۀ اشعار مصطفی مجیدی", "https://blog.ganjoor.net/1392/05/28/mostafa-majidi/"},
            new[] {"ابوالفضل فتحی آزاد", "http://afasoft.ir/", "گزیدۀ اشعار فریدون مشیری", "https://blog.ganjoor.net/1392/08/24/new-gdbs/"},
            new[] {"ابوالفضل فتحی آزاد", "http://afasoft.ir/", "اشعار ترانه‌های سیاوش قمیشی", "https://blog.ganjoor.net/1392/08/24/new-gdbs/"},
            new[] {"اسحاق فروزنده", "", "گزیدۀ اشعار پژمان بختیاری", "https://blog.ganjoor.net/1392/08/24/new-gdbs/"},
            new[] {"محمد ملکشاهی و همراهانشان", "https://t.me/ebnhesam", "غزلیات ابن حسام خوسفی", "https://blog.ganjoor.net/1394/12/29/ebnehesam-khusfi/"},
            new[] {"سیاوش جعفری", "", "فیه ما فیه و مجالس سبعه مولانا", "https://blog.ganjoor.net/1395/11/15/vtv-mlv/"},
            new[] {"سیاوش جعفری", "", "رشیدالدین وطواط", "https://blog.ganjoor.net/1395/11/15/vtv-mlv/"},
            new[] {"امیرحسین موسوی", "", "فایز دشتستانی", "https://blog.ganjoor.net/1396/01/27/fayez/"},
            new[] {"سیاوش جعفری", "", "عنصری", "https://blog.ganjoor.net/1396/04/16/onsori-azraghi/"},
            new[] {"سیاوش جعفری", "", "ازرقی هروی", "https://blog.ganjoor.net/1396/04/16/onsori-azraghi/"},
            new[] {"مصطفی علیزاده و همراهانشان", "https://t.me/hakimnazaribirjandi", "اشعار نزاری قهستانی", "https://blog.ganjoor.net/1396/05/26/nezari-1-100/"},
            new[] {"حمید زارعی مرودشت", "", "ترجیع بند بیدل", "https://blog.ganjoor.net/1396/09/03/bidel-tarjee/"},
            new[] { "سیاوش جعفری", "", "مرزبان‌نامهٔ سعدالدین وراوینی", "https://blog.ganjoor.net/1396/12/21/marzbanname/"},
            new[] { "سیاوش جعفری", "", "دیوان کمال‌الدین اسماعیل", "https://blog.ganjoor.net/1396/12/25/kamal/"},
            new[] { "سیاوش جعفری", "", "دیوان ظهیرالدین فاریابی", "https://blog.ganjoor.net/1397/01/09/zahir/"},
            new[] { "سید جابر موسوی صالحی", "", "غزلیات عبدالقادر گیلانی", "https://blog.ganjoor.net/1397/01/14/abdolghader-gilani/"},
            new[] { "سید جابر موسوی صالحی", "", "دیوان مولانا خالد نقشبندی", "https://blog.ganjoor.net/1397/03/26/naghshbandi/"},
            new[] { "محمد عظیم", "", "غزلیات سلطان باهو", "https://blog.ganjoor.net/1397/03/18/sultan-bahoo/"},
            new[] {"امیرحسین موسوی", "", "قدسی مشهدی", "https://blog.ganjoor.net/1397/04/08/ghodsi-mashhadi/"},
            new[] { "سید جابر موسوی صالحی", "", "مناجات نامهٔ خواجه عبدالله انصاری", "https://blog.ganjoor.net/1397/06/02/monajatname/"},
            new[] { "سید جابر موسوی صالحی", "", "کمال خجندی", "https://blog.ganjoor.net/1397/10/06/kamal-khojandi/"},
            new[] { "سید جابر موسوی صالحی", "", "همام تبریزی", "https://blog.ganjoor.net/1398/08/02/homam/"},
            new[] { "جواد احشامیان", "", "شیوا فرازمند", "https://blog.ganjoor.net/1398/10/16/shivafarazmand/"},
            new[] { "سید صادق هاشمی", "", "دیوان شوق مهدی فیض کاشانی", "https://blog.ganjoor.net/1398/11/22/feyz-ebnh/"},
            new[] { "سید صادق هاشمی", "", "موش و گربهٔ شیخ بهایی", "https://blog.ganjoor.net/1399/04/31/sheykh-bahaee-mg/"},
        };

        private const int GRDCLMN_NAME = 0;
        private const int GRDCLMN_SEC = 1;
        private const int GRDCLMN_MORE = 2;


        private void AboutForm_Load(object sender, EventArgs e)
        {
            lblAppVersion.Text =
                "ویرایش " + Assembly.GetExecutingAssembly().GetName().Version.Major + "." + Assembly.GetExecutingAssembly().GetName().Version.Minor;


            var num = Contributers.GetLength(0);
            for (var i = 0; i < num; i++)
            {
                var RowIndex = grdContributers.Rows.Add();
                grdContributers.Rows[RowIndex].Cells[GRDCLMN_NAME].Value = Contributers[i][DTCLMN_NAME];
                grdContributers.Rows[RowIndex].Cells[GRDCLMN_SEC].Value = Contributers[i][DTCLMN_SEC];
                grdContributers.Rows[RowIndex].Cells[GRDCLMN_MORE].Value = "کلیک کنید";
            }

        }

        private void LaunchUrl(string url)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString(), "خطا");
            }
            Cursor.Current = Cursors.Default;
        }

        private void lnkIcons_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LaunchUrl("http://dryicons.com/free-icons/preview/aesthetica/");
        }

        private void lnkHamidReza_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LaunchUrl("https://www.linkedin.com/in/hrmoh/");
        }

        private void lnkSources_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LaunchUrl("https://ganjoor.net/sources/");
        }

        private void lnkIconsEditor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LaunchUrl("http://dryicons.com/free-icons/preview/grace-icons-set/");
        }

        private void grdContributers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == GRDCLMN_NAME)
            {
                var link = Contributers[e.RowIndex][DTCLMN_LINK];
                if (!string.IsNullOrEmpty(link))
                    LaunchUrl(link);
            }
            else
                if (e.ColumnIndex == GRDCLMN_MORE)
            {
                var link = Contributers[e.RowIndex][DTCLMN_MORE];
                if (!string.IsNullOrEmpty(link))
                    LaunchUrl(link);
            }
        }

        private void lnkBlog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LaunchUrl("https://blog.ganjoor.net");
        }

        private void lnkEditor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LaunchUrl("https://blog.ganjoor.net/1389/04/27/dg-editor/");
        }

        private void lblImanAbidi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LaunchUrl("https://www.linkedin.com/in/imanabidi/");
        }

        private void lblHrBDev_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LaunchUrl("https://github.com/HrBDev/");
        }
    }
}
