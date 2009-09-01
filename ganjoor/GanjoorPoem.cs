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
        public bool _Faved;

        public GanjoorPoem(int ID, int CatID, string Title, string Url, bool Faved)
            : this(ID, CatID, Title, Url, Faved, string.Empty)
        {           

        }
        public GanjoorPoem(int ID, int CatID, string Title, string Url, bool Faved, string HighlightText)
        {
            _ID = ID;
            _CatID = CatID;
            _Title = Title;
            _Url = Url;
            _Faved = Faved;
            _HighlightText = HighlightText;
        }
    }
}
