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
        /// جای فونت اصلی مثلا اگر وسط صفحه است این باید ۱ باشد
        /// </summary>
        [DisplayName("صورت کسر موقعیت عمودی متن")]
        public int MainTextPosRatioPortion
        {
            get;
            set;
        }

        /// <summary>
        /// جای فونت اصلی مثلا اگر وسط صفحه است این باید ۲ باشد
        /// </summary>
        [DisplayName("صورت کسر موقعیت افقی متن")]
        public int MainTextPosRatioPortionFrom
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
        /// رنگ زمینه متن اصلی
        /// </summary>
        [DisplayName("رنگ زمینه متن")]
        public Color TextBackColor
        {
            get;
            set;
        }

        /// <summary>
        /// میزان شفافیت رنگ زمینه متن اصلی
        /// </summary>
        [DisplayName("شفافیت رنگ زمینه متن")]
        public int TextBackColorAlpha
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


        public override string ToString()
        {
            return this.Text;
        }

    }
}
