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
                conString.ReadOnly = false;
                _con = new SQLiteConnection(conString.ConnectionString);
                _con.Open();
            }
            catch(Exception exp)
            {
                LastError = exp.ToString();
                _con = null;
            }
            if (_con != null)
            {
                UpgradeOldDbs();
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
                        {
                            int PID = Convert.ToInt32(row.ItemArray[0]);
                            lst.Add(new GanjoorPoem(
                                PID,
                                CatID,
                                row.ItemArray[1].ToString(),
                                row.ItemArray[2].ToString().ToString(),
                                IsPoemFaved(PID)
                                ));
                        }
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
                                tbl.Rows[0].ItemArray[2].ToString().ToString(),
                                IsPoemFaved(PoemID)
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

        #region Fav/UnFav
        private int MaxFavOrder
        {
            get
            {
                if (Connected)
                {
                    using (DataTable tbl = new DataTable())
                    {
                        using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT MAX(pos) FROM fav", _con))
                        {
                            da.Fill(tbl);
                            if (tbl.Rows.Count != 0)
                                if (tbl.Rows[0].ItemArray[0] is DBNull)
                                    return -1;
                                else
                                    return Convert.ToInt32(tbl.Rows[0].ItemArray[0]);
                            return -1;
                        }
                    }
                }
                return -1;
            }
        }
        public bool IsPoemFaved(int PoemID)
        {
            if (Connected)
            {
                using (DataTable tbl = new DataTable())
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT pos FROM fav WHERE poem_id = " + PoemID.ToString(), _con))
                    {
                        da.Fill(tbl);
                        return tbl.Rows.Count != 0;
                    }
                }

            }
            return false;
        }
        public void Fav(int PoemID)
        {
            if (Connected)
            {
                using (SQLiteCommand cmd = new SQLiteCommand(_con))
                {
                    cmd.CommandText = "INSERT INTO fav (poem_id, pos) VALUES (" + PoemID.ToString() + ","+(MaxFavOrder+1).ToString()+");";
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void UnFav(int PoemID)
        {
            if (Connected)
            {
                using (SQLiteCommand cmd = new SQLiteCommand(_con))
                {
                    cmd.CommandText = "DELETE FROM fav WHERE poem_id=" + PoemID.ToString();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public bool ToggleFav(int PoemID)
        {
            if (IsPoemFaved(PoemID))
            {
                UnFav(PoemID);
                return false;
            }
            else
            {
                Fav(PoemID);
                return true;
            }
        }
        public DataTable GetFavs(int PageStart, int Count)
        {
            if (Connected)
            {
                DataTable tbl = new DataTable();
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT poem_id FROM fav ORDER BY pos LIMIT " + PageStart.ToString() + "," + Count.ToString(), _con))
                    {
                        da.Fill(tbl);
                    }
                    return tbl;
                }
            }
            return null;
        }
        #endregion

        #region Versioning
        private void UpgradeOldDbs()
        {
            DataRow[] favTable = _con.GetSchema("Tables").Select("Table_Name='fav'");
            if (favTable.Length == 0)
            {
                using (SQLiteCommand cmd = new SQLiteCommand(_con))
                {
                    cmd.CommandText = "CREATE TABLE fav (poem_id INTEGER PRIMARY KEY, pos INTEGER);";
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

    }
}
