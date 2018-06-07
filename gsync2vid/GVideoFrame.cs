using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace gsync2vid
{
    /// <summary>
    /// قاب ویدیو
    /// </summary>
    public class GVideoFrame
    {

        public GVideoFrame()
        {
            OverlayImages = new GOverlayImage[]
            {
                new GOverlayImage()
                {
                    Name = "متن اصلی",
                    ImagePath = string.Empty
                }
            };

            TextBorderValue = 0;
            TextBorderColor = Color.Black;
        }

        /// <summary>
        /// مربوط به خوانش
        /// </summary>
        [DisplayName("مربوط به خوانش")]
        public bool AudioBound
        {
            get;
            set;
        }

        /// <summary>
        /// شروع قاب بر حسب میلی ثانیه
        /// </summary>
        [DisplayName("شروع بر حسب میلی ثانیه")]
        public int StartInMiliseconds
        {
            get;
            set;
        }


        /// <summary>
        /// متن اصلی
        /// </summary>
        [DisplayName("متن")]
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// رنگ زمینه
        /// </summary>
        [DisplayName("رنگ زمینه")]
        public Color BackColor
        {
            get;
            set;
        }

        
        /// <summary>
        /// مسیر تصویر زمینه
        /// </summary>
        [DisplayName("مسیر تصویر زمینه")]
        public string BackgroundImagePath
        {
            get;
            set;
        }

        /// <summary>
        /// فونت متن اصلی
        /// </summary>
        [DisplayName("فونت")]
        public Font Font
        {
            get;
            set;
        }

        /// <summary>
        /// جای متن اصلی مثلا اگر وسط صفحه است این باید ۱ باشد
        /// </summary>
        [DisplayName("صورت کسر موقعیت عمودی متن")]
        public int TextVerticalPosRatioPortion
        {
            get;
            set;
        }

        /// <summary>
        /// جای متن اصلی مثلا اگر وسط صفحه است این باید ۲ باشد
        /// </summary>
        [DisplayName("مخرج کسر موقعیت عمودی متن")]
        public int TextVerticalPosRatioPortionFrom
        {
            get;
            set;
        }



        /// <summary>
        /// جای متن اصلی مثلا اگر وسط صفحه است این باید ۱ باشد
        /// </summary>
        [DisplayName("صورت کسر موقعیت افقی متن")]
        public int TextHorizontalPosRatioPortion
        {
            get;
            set;
        }

        /// <summary>
        /// جای متن اصلی مثلا اگر وسط صفحه است این باید ۲ باشد
        /// </summary>
        [DisplayName("مخرج کسر موقعیت افقی متن")]
        public int TextHorizontalPosRatioPortionFrom
        {
            get;
            set;
        }



        /// <summary>
        /// حداکثر عرض متن صورت کسر
        /// </summary>
        [DisplayName("صورت کسر حداکثر عرض متن")]
        public int MaxTextWidthRatioPortion
        {
            get;
            set;
        }

        /// <summary>
        /// حداکثر عرض متن مخرج کسر
        /// </summary>
        [DisplayName("مخرج کسر حداکثر عرض متن")]
        public int MaxTextWidthRatioPortionFrom
        {
            get;
            set;
        }


        /// <summary>
        /// رنگ متن اصلی
        /// </summary>
        [DisplayName("رنگ متن")]
        public Color TextColor
        {
            get;
            set;
        }


        /// <summary>
        /// قاب صاحب
        /// </summary>
        [DisplayName("قاب صاحب")]
        public GVideoFrame MasterFrame
        {
            get;
            set;
        }

        /// <summary>
        /// نمایش لوگو
        /// </summary>
        [DisplayName("نمایش لوگو")]
        public bool ShowLogo
        {
            get;
            set;
        }

        [DisplayName("شکل محیطی متن")]
        [TypeConverter(typeof(GTextBoxShapeConvertor))]
        public GTextBoxShape Shape
        {
            get;
            set;
        }

        /// <summary>
        /// رنگ زمینه متن اصلی
        /// </summary>
        [DisplayName("رنگ زمینه محیطی متن")]
        public Color TextBackColor
        {
            get;
            set;
        }

        /// <summary>
        /// میزان شفافیت رنگ زمینه متن اصلی
        /// </summary>
        [DisplayName("شفافیت رنگ زمینه محیطی متن")]
        public int TextBackColorAlpha
        {
            get;
            set;
        }

        /// <summary>
        /// ضخامت مرز مستطیل زمینه:
        /// </summary>
        [DisplayName("ضخامت مرز محیطی زمینه")]
        public int TextBackRectThickness
        {
            get;
            set;
        }

        /// <summary>
        /// رنگ مرز محیطی متن
        /// </summary>
        [DisplayName("رنگ مرز محیطی متن")]
        public Color BorderColor
        {
            get;
            set;
        }

        /// <summary>
        /// تصاویر لایه زیر متن
        /// </summary>
        [DisplayName("تصاویر لایه زیر متن")]
        public GOverlayImage[] OverlayImages
        {
            get;
            set;
        }

        /// <summary>
        /// میزان مرز متن = ۰ ندارد
        /// </summary>
        [DisplayName("میزان مرز متن")]
        public int TextBorderValue
        {
            get;
            set;
        }

        /// <summary>
        /// رنگ مرز متن
        /// </summary>
        [DisplayName("رنگ مرز متن")]
        public Color TextBorderColor
        {
            get;
            set;
        }



        public override string ToString()
        {
            return this.Text;
        }

    }
}
