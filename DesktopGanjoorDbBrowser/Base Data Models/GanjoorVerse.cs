using System;
using System.Collections.Generic;
using System.Text;

namespace ganjoor
{
    /// <summary>
    /// اطلاعات مصرعهای اشعار، همینطور خطوط شعر نیمایی یا آزاد یا پاراگرافهای نثر در این ساختار نگهداری می شود.
    /// </summary>
    /// <dbtable>
    /// verse
    /// </dbtable>
   
    public class GanjoorVerse
    {
        #region database fields
        /// <summary>
        /// شناسۀ شعر مرتبط
        /// </summary>
        /// <dbfield>
        /// poem_id
        /// </dbfield>
        public int _PoemID;
        /// <summary>
        /// ترتیب مصرع در کل شعر بدون توجه به _Position
        /// </summary>
        /// <dbfield>
        /// vorder
        /// </dbfield>
        public int _Order;
        /// <summary>
        /// نوع و جایگاه مصرع
        /// </summary>
        /// <dbfield>
        /// position
        /// </dbfield>
        public VersePosition _Position;
        /// <summary>
        /// متن مصرع
        /// </summary>
        /// <dbfield>
        /// text
        /// </dbfield>
        public string _Text;
        /// <summary>
        /// سازندۀ پیش فرض : یک نمونه از کلاس را با مقادیر ورودی می سازد
        /// </summary>
        #endregion
        #region Constructors

        public GanjoorVerse()
        {
            
        }
        public GanjoorVerse(int PoemID, int Order, VersePosition Position, string Text)
        {
            _PoemID = PoemID;
            _Order = Order;
            _Position = Position;
            _Text = Text;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return _Text;
        }
        #endregion
    }
}
