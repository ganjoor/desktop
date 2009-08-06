using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace ganjoor
{
    class DbBrowser
    {
        #region Constructor
        public DbBrowser()
        {
            try
            {
                SQLiteConnectionStringBuilder conString = new SQLiteConnectionStringBuilder();
                conString.DataSource = "ganjoor.s3db";
                conString.DefaultTimeout = 5000;
                conString.FailIfMissing = true;
                conString.ReadOnly = true;
                _con = new SQLiteConnection(conString.ConnectionString);
                _con.Open();
            }
            catch(Exception exp)
            {
                LastError = exp.ToString();
                _con = null;
            }
        }
        ~DbBrowser()
        {
            if (_con != null)
                _con.Close();
        }
        #endregion

        #region Variables
        private SQLiteConnection _con;
        public string LastError = string.Empty;
        #endregion 

        #region Properties & Methods
        public bool Failed
        {
            get
            {
                return (null == _con);
            }
        }
        public bool Connected
        {
            get
            {
                return (null != _con);
            }
        }
        public List<GanjoorPoet> Poets
        {
            get
            {
                List<GanjoorPoet> poets = new List<GanjoorPoet>();
                if (Connected)
                {
                    using (DataTable tbl = new DataTable())
                    {
                        using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT id, name, cat_id FROM poet", _con))
                        {
                            da.Fill(tbl);
                            foreach (DataRow row in tbl.Rows)
                            {
                                poets.Add(new GanjoorPoet(Convert.ToInt32(row.ItemArray[0]), row.ItemArray[1].ToString(), Convert.ToInt32(row.ItemArray[2])));
                            }
                        }
                    }
                }
                return poets;
            }
        }
        public GanjoorCat GetCategory(int CatID)
        {
            if (Connected)                
            using (DataTable tbl = new DataTable())
            {
                using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT poet_id, text, parent_id, url FROM cat WHERE id = " + CatID.ToString(), _con))
                {
                    da.Fill(tbl);
                    System.Diagnostics.Debug.Assert(tbl.Rows.Count < 2);
                    if (tbl.Rows.Count == 1)
                        return new GanjoorCat(
                            CatID,
                            Convert.ToInt32(tbl.Rows[0].ItemArray[0]),
                            tbl.Rows[0].ItemArray[1].ToString(),
                            Convert.ToInt32(tbl.Rows[0].ItemArray[2]),
                            tbl.Rows[0].ItemArray[3].ToString().ToString()
                            );
                }
            }
            return null;
        }
        public List<GanjoorCat> GetSubCategories(int CatID)
        {
            List<GanjoorCat> lst = new List<GanjoorCat>();
            if (Connected)
            {
                using (DataTable tbl = new DataTable())
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT poet_id, text, url, ID FROM cat WHERE parent_id = " + CatID.ToString(), _con))
                    {
                        da.Fill(tbl);
                        foreach (DataRow row in tbl.Rows)
                            lst.Add(new GanjoorCat(
                                Convert.ToInt32(row.ItemArray[3]),
                                Convert.ToInt32(row.ItemArray[0]),
                                row.ItemArray[1].ToString(),
                                CatID,
                                row.ItemArray[2].ToString().ToString()
                                ));
                    }
                }
            }
            return lst;
        }
        public List<GanjoorCat> GetParentCategories(GanjoorCat Cat)
        {
            List<GanjoorCat> lst = new List<GanjoorCat>();
            if (Connected)
            {
                while (Cat._ParentID != 0)
                {
                    Cat = GetCategory(Cat._ParentID);
                    lst.Insert(0, Cat);
                }
                lst.Insert(0,new GanjoorCat(0, 0, "خانه", 0, ""));
            }
            return lst;
        }
        public List<GanjoorPoem> GetPoems(int CatID)
        {
            List<GanjoorPoem> lst = new List<GanjoorPoem>();
            if (Connected)
            {
                using (DataTable tbl = new DataTable())
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT ID, title, url FROM poem WHERE cat_id = " + CatID.ToString(), _con))
                    {
                        da.Fill(tbl);
                        foreach (DataRow row in tbl.Rows)
                            lst.Add(new GanjoorPoem(
                                Convert.ToInt32(row.ItemArray[0]),
                                CatID,
                                row.ItemArray[1].ToString(),
                                row.ItemArray[2].ToString().ToString()
                                ));
                    }
                }
            }
            return lst;
        }
        public List<GanjoorVerse> GetVerses(int PoemID)
        {
            return GetVerses(PoemID, 0);
        }
        public List<GanjoorVerse> GetVerses(int PoemID, int Count)
        {
            List<GanjoorVerse> lst = new List<GanjoorVerse>();
            if (Connected)
            {
                using (DataTable tbl = new DataTable())
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT vorder, position, text FROM verse WHERE poem_id = " + PoemID.ToString() + " order by vorder" + (Count>0 ? " LIMIT "+Count.ToString() : ""), _con))
                    {
                        da.Fill(tbl);
                        foreach (DataRow row in tbl.Rows)
                            lst.Add(new GanjoorVerse(
                                PoemID,                                
                                Convert.ToInt32(row.ItemArray[0]),
                                (VersePosition)Convert.ToInt32(row.ItemArray[1]),
                                row.ItemArray[2].ToString()
                                ));
                    }
                }
            }
            return lst;
        }
        public GanjoorPoem GetPoem(int PoemID)
        {
            if (Connected)
            {
                using (DataTable tbl = new DataTable())
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT cat_id, title, url FROM poem WHERE ID = " + PoemID.ToString(), _con))
                    {
                        da.Fill(tbl);
                        System.Diagnostics.Debug.Assert(tbl.Rows.Count < 2);
                        if(1 == tbl.Rows.Count)
                            return
                                new GanjoorPoem(
                                PoemID,
                                Convert.ToInt32(tbl.Rows[0].ItemArray[0]),
                                tbl.Rows[0].ItemArray[1].ToString(),
                                tbl.Rows[0].ItemArray[2].ToString().ToString()
                                );
                    }
                }
            }
            return null;
        }
        public GanjoorPoem GetNextPoem(int PoemID, int CatID)
        {
            if (Connected)
            {
                using (DataTable tbl = new DataTable())
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(
                        String.Format(
                        "SELECT ID FROM poem WHERE cat_id = {0} AND id>{1} LIMIT 1"
                        ,
                        CatID, PoemID)
                    , _con))
                    {
                        da.Fill(tbl);
                        if (1 == tbl.Rows.Count)
                            return
                                GetPoem(Convert.ToInt32(tbl.Rows[0].ItemArray[0]));
                    }
                }
            }
            return null;
        }
        public GanjoorPoem GetNextPoem(GanjoorPoem poem)
        {
            if (null != poem)
            {
                return GetNextPoem(poem._ID, poem._CatID);
            }
            return null;
        }
        public GanjoorPoem GetPreviousPoem(int PoemID, int CatID)
        {
            if (Connected)
            {
                using (DataTable tbl = new DataTable())
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(
                        String.Format(
                        "SELECT ID FROM poem WHERE cat_id = {0} AND id<{1} ORDER BY ID DESC LIMIT 1"
                        ,
                        CatID, PoemID)
                    , _con))
                    {
                        da.Fill(tbl);
                        if (1 == tbl.Rows.Count)
                            return
                                GetPoem(Convert.ToInt32(tbl.Rows[0].ItemArray[0]));
                    }
                }
            }
            return null;
        }
        public GanjoorPoem GetPreviousPoem(GanjoorPoem poem)
        {
            if (null != poem)
            {
                return GetPreviousPoem(poem._ID, poem._CatID);
            }
            return null;
        }
        public GanjoorPoet GetPoet(int PoetID)
        {
            if (Connected)
            {
                using (DataTable tbl = new DataTable())
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT id, name, cat_id FROM poet WHERE id = "+PoetID.ToString(), _con))
                    {
                        da.Fill(tbl);
                        System.Diagnostics.Debug.Assert(tbl.Rows.Count < 2);
                        if (1 == tbl.Rows.Count)
                            return
                                new GanjoorPoet(Convert.ToInt32(tbl.Rows[0].ItemArray[0]), tbl.Rows[0].ItemArray[1].ToString(), Convert.ToInt32(tbl.Rows[0].ItemArray[2]));
                        
                    }
                }
            }
            return null;
        }
        #endregion

        #region Search
        public DataTable FindPoemsContaingPhrase(string phrase, int PageStart, int Count)
        {
            if (Connected)
            {
                DataTable tbl = new DataTable();
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT poem_id FROM verse WHERE text LIKE '%" + phrase + "%' GROUP BY poem_id LIMIT "+PageStart.ToString()+","+Count.ToString(), _con))
                    {
                        da.Fill(tbl);                        
                    }
                    return tbl;
                }
            }
            return null;
        }
        public DataTable FindFirstVerseContaingPhrase(int PoemID, string phrase)
        {
            if (Connected)
            {
                DataTable tbl = new DataTable();
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT text FROM verse WHERE poem_id="+PoemID+" AND text LIKE'%"+phrase+"%' LIMIT 0,1", _con))
                    {
                        da.Fill(tbl);
                    }
                    return tbl;
                }
            }
            return null;
        }
        #endregion

    }
}
