using System;
using System.Collections.Generic;
using System.Text;

namespace ganjoor
{
    class GanjoorPoem
    {
        public int _ID;
        public int _CatID;
        public string _Title;
        public string _Url;
        public string _HighlightText;

        public GanjoorPoem(int ID, int CatID, string Title, string Url)
            : this(ID, CatID, Title, Url, string.Empty)
        {           

        }
        public GanjoorPoem(int ID, int CatID, string Title, string Url, string HighlightText)
        {
            _ID = ID;
            _CatID = CatID;
            _Title = Title;
            _Url = Url;
            _HighlightText = HighlightText;
        }
    }
}
