using System;
using System.Collections.Generic;
using System.Text;

namespace ganjoor
{
    class GanjoorSearchPage
    {
        public string _SearchPhrase;
        public int _PageStart;
        public int _MaxItemsCount;
        public GanjoorSearchPage(string SearchPhrase, int PageStart, int MaxItemsCount)
        {
            _SearchPhrase = SearchPhrase;
            _PageStart = PageStart;
            _MaxItemsCount = MaxItemsCount;
        }
    }
}
