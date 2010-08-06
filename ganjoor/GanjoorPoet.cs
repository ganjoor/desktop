using System;
using System.Collections.Generic;
using System.Text;

namespace ganjoor
{
    public class GanjoorPoet
    {
        public GanjoorPoet(int ID, string Name, int CatID)
        {
            _Name = Name;
            _ID = ID;
            _CatID = CatID;
        }
        public string _Name;
        public int _ID;
        public int _CatID;
    }
}
