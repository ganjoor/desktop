using System;
using System.Collections.Generic;
using System.Text;

namespace ganjoor
{
    /// <summary>
    /// اطلاعات شعر : مصاریع یا خطوط شعر با لیستی از نمونه های کلاس GanjoorVerse که _PoemId همه آنها برابر _ID نمونۀ این کلاس است مشخص می شود.
    /// </summary>
    /// <dbtable>
    /// poem
    /// </dbtable>
    public class GanjoorPoem
    {
        #region public fields
        #region database fields
        /// <summary>
        /// شناسۀ رکورد شعر
        /// </summary>
        /// <dbfield>
        /// id
        /// </dbfield>
        public int _ID;
        /// <summary>
        /// شناسۀ رکورد بخشی که شعر به آن تعلق دارد
        /// </summary>
        /// <dbfield>
        /// cat_id
        /// </dbfield>
        public int _CatID;
        /// <summary>
        /// عنوان شعر
        /// </summary>
        /// <dbfield>
        /// title
        /// </dbfield>
        public string _Title;
        /// <summary>
        /// نشانی شعر در سایت گنجور ganjoor.net
        /// </summary>
        /// <dbfield>
        /// url
        /// </dbfield>
        public string _Url;
        /// <summary>
        /// در نمایش شعر در صورتی که قرار است متنی در کل شعر برجسته و متفاوت نشان داده شود مقدار این متن در این فیلد نگهداری می شود
        /// </summary>
        #endregion
        #region UI properties
        public string _HighlightText;
        /// <summary>
        /// آیا کل شعر نشانه گذاری شده یا خیر (وضعیت دکمۀ نشانه گذاری - حذف نشانه از روی این فیلد تعیین می شود)
        /// </summary>
        public bool _Faved;
        #endregion
        #endregion

        #region Constructors
        /// <summary>
        /// سازندۀ پیش فرض : یک نمونه از کلاس را با مقادیر ورودی می سازد
        /// </summary>
        public GanjoorPoem(int ID, int CatID, string Title, string Url, bool Faved)
            : this(ID, CatID, Title, Url, Faved, string.Empty)
        {           

        }
        /// <summary>
        /// سازندۀ پیش فرض : یک نمونه از کلاس را با مقادیر ورودی می سازد
        /// </summary>
        public GanjoorPoem(int ID, int CatID, string Title, string Url, bool Faved, string HighlightText)
        {
            _ID = ID;
            _CatID = CatID;
            _Title = Title;
            _Url = Url;
            _Faved = Faved;
            _HighlightText = HighlightText;
        }
        #endregion
    }
}
