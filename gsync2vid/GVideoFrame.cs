using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

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
        public bool AudioBound
        {
            get;
            set;
        }

        /// <summary>
        /// فقط برای قابهای قبل یا بعد از خوانش طول زمان نمایش قاب بر حسب میلی ثانیه
        /// </summary>
        public int DurationInMiliseconds
        {
            get;
            set;
        }


        /// <summary>
        /// متن اصلی
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// رنگ زمینه
        /// </summary>
        public Color BackColor
        {
            get;
            set;
        }

        
        /// <summary>
        /// مسیر تصویر زمینه
        /// </summary>
        public string BackgroundImagePath
        {
            get;
            set;
        }

        /// <summary>
        /// فونت متن اصلی
        /// </summary>
        public Font Font
        {
            get;
            set;
        }

        /// <summary>
        /// جای فونت اصلی مثلا اگر وسط صفحه است این باید ۱ باشد
        /// </summary>
        public int MainTextPosRatioPortion
        {
            get;
            set;
        }

        /// <summary>
        /// جای فونت اصلی مثلا اگر وسط صفحه است این باید ۲ باشد
        /// </summary>
        public int MainTextPosRatioPortionFrom
        {
            get;
            set;
        }

        /// <summary>
        /// حداکثر عرض متن صورت کسر
        /// </summary>
        public int MaxTextWidthRatioPortion
        {
            get;
            set;
        }

        /// <summary>
        /// حداکثر عرض متن مخرج کسر
        /// </summary>
        public int MaxTextWidthRatioPortionFrom
        {
            get;
            set;
        }


        /// <summary>
        /// رنگ متن اصلی
        /// </summary>
        public Color TextColor
        {
            get;
            set;
        }

        /// <summary>
        /// رنگ زمینه متن اصلی
        /// </summary>
        public Color TextBackColor
        {
            get;
            set;
        }

        /// <summary>
        /// میزان شفافیت رنگ زمینه متن اصلی
        /// </summary>
        public int TextBackColorAlpha
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
