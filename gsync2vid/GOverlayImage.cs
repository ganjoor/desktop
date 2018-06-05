using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace gsync2vid
{
    /// <summary>
    /// تصویر روی قاب
    /// </summary>
    public class GOverlayImage
    {
        /// <summary>
        /// عنوان
        /// </summary>
        [DisplayName("عنوان")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// مسیر تصویر
        /// </summary>
        [DisplayName("مسیر")]
        public string ImagePath
        {
            get;
            set;
        }

        /// <summary>
        /// مکان تصویر مثلا اگر وسط صفحه است این باید ۱ باشد
        /// </summary>
        [DisplayName("صورت کسر موقعیت عمودی مرکز")]
        [DefaultValue(1)]
        public int VerticalPosRatioPortion
        {
            get;
            set;
        }

        /// <summary>
        /// مکان تصویر مثلا اگر وسط صفحه است این باید ۲ باشد
        /// </summary>
        [DisplayName("مخرج کسر موقعیت عمودی مرکز")]
        [DefaultValue(1)]
        public int VerticalPosRatioPortionFrom
        {
            get;
            set;
        }



        /// <summary>
        /// مکان تصویر مثلا اگر وسط صفحه است این باید ۱ باشد
        /// </summary>
        [DisplayName("صورت کسر موقعیت افقی مرکز")]
        [DefaultValue(1)]
        public int HorizontalPosRatioPortion
        {
            get;
            set;
        }

        /// <summary>
        /// مکان تصویر مثلا اگر وسط صفحه است این باید ۲ باشد
        /// </summary>
        [DisplayName("مخرج کسر موقعیت افقی مرکز")]
        [DefaultValue(1)]
        public int HorizontalPosRatioPortionFrom
        {
            get;
            set;
        }



        /// <summary>
        /// صورت کسر اندازه برای عدم تغییر  1 باشد
        /// </summary>
        [DisplayName("صورت کسر اندازه")]
        [DefaultValue(1)]
        public int ScaleRatioPortion
        {
            get;
            set;
        }

        /// <summary>
        /// مخرج کسر اندازه
        /// </summary>
        [DisplayName("مخرج کسر اندازه")]
        [DefaultValue(1)]
        public int ScaleRatioPortionFrom
        {
            get;
            set;
        }


        public override string ToString()
        {
            return Name; 
        }


    }
}
