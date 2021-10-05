namespace ganjoor
{
    /// <summary>
    /// معنی فیلد position در جدول verse با توجه به مقادیر این ساختار داده مشخص می شود.
    /// </summary>
    public enum VersePosition
    {
        Right = 0,//مصرع اول
        Left = 1,// مصرع دوم
        CenteredVerse1 = 2,// مصرع اول یا تنهای ابیات ترجیع یا ترکیب
        CenteredVerse2 = 3,// مصرع دوم ابیات ترجیع یا ترکیب
        Single = 4, //مصرعهای شعرهای نیمایی یا آزاد
        Comment = 5, //پاراگرافهایی که حالت توضیحی دارند
        Paragraph = -1, //نثر
    }
}
