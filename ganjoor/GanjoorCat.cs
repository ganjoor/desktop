using System;
using System.Collections.Generic;
using System.Text;

namespace ganjoor
{
    class GanjoorCat
    {
        public GanjoorCat(int ID, int PoetID, string Text, int ParentID, string Url) : this(ID, PoetID, Text, ParentID, Url, 0)
        {
        }
        public GanjoorCat(int ID, int PoetID, string Text, int ParentID, string Url, int StartPoem)
        {
            _ID = ID;
            _PoetID = PoetID;
            _Text = Text;
            _ParentID = ParentID;
            _Url = Url;
            _StartPoem = StartPoem;
        }
        public GanjoorCat(GanjoorCat baseCat, int StartPoem) : this(baseCat._ID, baseCat._PoetID, baseCat._Text, baseCat._ParentID, baseCat._Url, StartPoem)
        {
        }
        public int _ID;
        public int _PoetID;
        public string _Text;
        public int _ParentID;
        public string _Url;
        public int _StartPoem;
    }
}
