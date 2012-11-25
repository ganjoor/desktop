using System;
using System.Collections.Generic;
using System.Text;

namespace ganjoor
{
    /// <summary>
    /// اطلاعات بخشها
    /// </summary>
    /// <dbtable>
    /// cat
    /// </dbtable>
    public class GanjoorCat
    {
        #region Constructors
        /// <summary>
        /// سازندۀ پیش فرض : یک نمونه از کلاس را با مقادیر ورودی می سازد
        /// </summary>
        public GanjoorCat(int ID, int PoetID, string Text, int ParentID, string Url) : this(ID, PoetID, Text, ParentID, Url, 0)
        {
        }
        /// <summary>
        /// سازندۀ پیش فرض : یک نمونه از کلاس را با مقادیر ورودی می سازد
        /// </summary>
        public GanjoorCat(int ID, int PoetID, string Text, int ParentID, string Url, int StartPoem)
        {
            _ID = ID;
            _PoetID = PoetID;
            _Text = Text;
            _ParentID = ParentID;
            _Url = Url;
            _StartPoem = StartPoem;
        }
        /// <summary>
        /// سازندۀ پیش فرض : یک نمونه از کلاس را با مقادیر ورودی می سازد
        /// </summary>
        public GanjoorCat(GanjoorCat baseCat, int StartPoem) : this(baseCat._ID, baseCat._PoetID, baseCat._Text, baseCat._ParentID, baseCat._Url, StartPoem)
        {
        }
        #endregion
        #region public properties
        #region database fields
        /// <summary>
        /// شناسۀ رکورد بخش
        /// </summary>
        /// <dbfield>
        /// id
        /// </dbfield>
        public int _ID;
        /// <summary>
        /// شناسۀ رکورد شاعر
        /// </summary>
        /// <dbfield>
        /// poet_id
        /// </dbfield>
        public int _PoetID;
        /// <summary>
        /// عنوان بخش
        /// </summary>
        /// <dbfield>
        /// text
        /// </dbfield>
        public string _Text;
        /// <summary>
        /// شناسۀ رکورد بخش والد
        /// id از رکورد دیگری از جدول cat
        /// اگر صفر باشد یعنی مربوط به بخشهای ریشه (شاعران) است
        /// </summary>
        /// <dbfield>
        /// parent_id
        /// </dbfield>
        public int _ParentID;
        /// <summary>
        /// نشانی بخش در سایت گنجور ganjoor.net
        /// </summary>
        /// <dbfield>
        /// url
        /// </dbfield>
        public string _Url;
        #endregion
        #region UI propertis
        /// <summary>
        /// در صورتی که تعداد شعرهای بخش بیشتر از «حداکثر تعداد عنوانها در فهرست اشعار یک بخش» باشد مقدار غیر صفر برای این
        /// فیلد نشان می دهد فهرست اشعار باید نه از ابتدا که از این عدد به بعد تا همان حداکثر نمایش داده شود.
        /// </summary>
        public int _StartPoem;
        #endregion
        #endregion
    }
}
