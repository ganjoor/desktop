namespace ganjoor
{
    /// <summary>
    /// اطلاعات شاعر
    /// </summary>
    /// <dbtable>
    /// poet
    /// </dbtable>
    public class GanjoorPoet
    {
        #region Constructor
        /// <summary>
        ///  سازندۀ پیش فرض : یک نمونه از کلاس را با مقادیر ورودی می سازد
        /// </summary>
        public GanjoorPoet(int ID, string Name, int CatID, string Bio)
        {
            _Name = Name;
            _ID = ID;
            _CatID = CatID;
            _Bio = Bio;
        }
        #endregion
        #region public fields
        /// <summary>
        /// نام شاعر
        /// </summary>
        /// <dbfield>
        /// name
        /// </dbfield>
        public string _Name;
        /// <summary>
        /// شناسۀ رکورد شاعر
        /// </summary>
        /// <dbfield>
        /// id
        /// </dbfield>
        public int _ID;
        /// <summary>
        /// شناسۀ بخش مرتبط با شاعر
        /// id در جدول cat
        /// هر شاعر یک بخش مختص به خودش دارد که عموماً parent_id آن صفر است.
        /// </summary>
        /// <dbfield>
        /// cat_id
        /// </dbfield>
        public int _CatID;
        /// <summary>
        /// زندگینامه یا توضیحات دربارۀ شاعر
        /// این فیلد از ساغر وارد گنجور رومیزی شده است.
        /// </summary>
        /// <dbfield>
        /// description
        /// </dbfield>
        public string _Bio;
        #endregion
        #region Comparison
        /// <summary>
        /// دو نمونه از کلاس را مقایسه کرده نتیجه را باز می گرداند.
        /// </summary>
        /// <returns>true if equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is GanjoorPoet)
            {
                var otherPoet = obj as GanjoorPoet;
                return
                    _ID == otherPoet._ID && _Name == otherPoet._Name;
            }
            return false;
        }

        public override int GetHashCode() => _ID;

        #endregion

        #region ToString
        public override string ToString() => _Name;
        #endregion

    }
}
