using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ganjoor
{
    class GarnjoorBrowsingHistory
    {
        public int _CatID;
        public int _CatPageStart;
        public int _PoemID;
        public string _SearchPhrase;
        public int _PoetID;
        public int _SearchStart;
        public int _PageItemsCount;
        public bool _FavsPage;
        public Point _AutoScrollPosition;

        public GarnjoorBrowsingHistory(int CatID, int PoemID, int CatPageStart, Point AutoScrollPosition)
        {
            _CatID = CatID;
            _PoemID = PoemID;
            _CatPageStart = CatPageStart;
            _AutoScrollPosition = AutoScrollPosition;
        }
        public GarnjoorBrowsingHistory(string SearchPhrase, int PoetID, int SearchStart, int PageItemsCount, bool FavsPage, Point AutoScrollPosition)
        {
            _CatID = _PoemID = 0;
            _SearchPhrase = SearchPhrase;
            _SearchStart = SearchStart;
            _PageItemsCount = PageItemsCount;
            _FavsPage = FavsPage;
            _PoetID = PoetID;
            _AutoScrollPosition = AutoScrollPosition;
        }
    }
}
