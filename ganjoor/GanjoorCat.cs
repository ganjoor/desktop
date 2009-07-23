using System;
using System.Collections.Generic;
using System.Text;

namespace ganjoor
{
    class GanjoorCat
    {
        public GanjoorCat(int ID, int PoetID, string Text, int ParentID, string Url)
        {
            _ID = ID;
            _PoetID = PoetID;
            _Text = Text;
            _ParentID = ParentID;
            _Url = Url;
        }
        public int _ID;
        public int _PoetID;
        public string _Text;
        public int _ParentID;
        public string _Url;
    }
}
