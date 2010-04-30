using System;
using System.Collections.Generic;
using System.Text;

namespace ganjoor
{    
    enum VersePosition
    {
        Right = 0,//مصرع اول
        Left = 1,// مصرع دوم
        CenteredVerse1 = 2,// مصرع اول یا تنهای ابیات ترجیع یا ترکیب
        CenteredVerse2 = 3,// مصرع دوم ابیات ترجیع یا ترکیب
        Single = 4, //مصرعهای شعرهای نیمایی یا آزاد
    }
    class GanjoorVerse
    {
        public int _PoemID;
        public int _Order;
        public VersePosition _Position;
        public string _Text;
        public GanjoorVerse(int PoemID, int Order, VersePosition Position, string Text)
        {
            _PoemID = PoemID;
            _Order = Order;
            _Position = Position;
            _Text = Text;
        }
    }
}
