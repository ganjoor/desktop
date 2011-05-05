using System;
using System.Collections.Generic;
using System.Text;

namespace ganjoor
{
    public class GanjoorPoet
    {
        public GanjoorPoet(int ID, string Name, int CatID, string Bio)
        {
            _Name = Name;
            _ID = ID;
            _CatID = CatID;
            _Bio = Bio;
        }
        public string _Name;
        public int _ID;
        public int _CatID;
        public string _Bio;        
        public override bool Equals(object obj)
        {
            if (obj is GanjoorPoet)
            {
                GanjoorPoet otherPoet = obj as GanjoorPoet;
                return
                    this._ID == otherPoet._ID && this._Name == otherPoet._Name;
            }
             return false;
        }

    }
}
