namespace ganjoor
{
    class GanjoorSearchPage
    {
        public string _SearchPhrase;
        public int _SearchType;
        public int _PageStart;
        public int _MaxItemsCount;
        public int _PoetID;
        public GanjoorSearchPage(string SearchPhrase, int PageStart, int MaxItemsCount, int PoetID, int SearchType)
        {
            _SearchPhrase = SearchPhrase;
            _PageStart = PageStart;
            _MaxItemsCount = MaxItemsCount;
            _PoetID = PoetID;
            _SearchType = SearchType;
        }
    }
}
