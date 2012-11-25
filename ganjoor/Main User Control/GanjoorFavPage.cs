using System;
using System.Collections.Generic;
using System.Text;

namespace ganjoor
{
    class GanjoorFavPage
    {
        public int _PageStart;
        public int _MaxItemsCount;
        public GanjoorFavPage(int PageStart, int MaxItemsCount)
        {
            _PageStart = PageStart;
            _MaxItemsCount = MaxItemsCount;
        }
    }
}
