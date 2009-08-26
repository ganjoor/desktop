using System;
using System.Collections.Generic;
using System.Text;

namespace ganjoor
{
    class GarnjoorBrowsingHistory
    {
        public int _CatID;
        public int _PoemID;
        public string _SearchPhrase;
        public int _SearchStart;
        public int _PageItemsCount;

        public GarnjoorBrowsingHistory(int CatID, int PoemID)
        {
            _CatID = CatID;
            _PoemID = PoemID;
        }
        public GarnjoorBrowsingHistory(string SearchPhrase, int SearchStart, int PageItemsCount)
        {
            _CatID = _PoemID = 0;
            _SearchPhrase = SearchPhrase;
            _SearchStart = SearchStart;
            _PageItemsCount = PageItemsCount;
        }
    }
}
