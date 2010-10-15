using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.IO;
using ganjoor.Properties;

namespace ganjoor
{
    public class DbBrowser
    {
        #region Constructor
        public DbBrowser()
            : this("ganjoor.s3db")
        {
        }
        public DbBrowser(string sqliteDatabaseNameFileName)
        {
            try
            {
                SQLiteConnectionStringBuilder conString = new SQLiteConnectionStringBuilder();
                conString.DataSource = sqliteDatabaseNameFileName;
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
                CachedMaxCatID = CachedMaxPoemID = 0;
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
                poets.Sort(ComparePoetsByName);
                return poets;
            }
        }
        private static int ComparePoetsByName(GanjoorPoet poet1, GanjoorPoet poet2)
        {
            return poet1._Name.CompareTo(poet2._Name);
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
        private static int CompareCategoriesByName(GanjoorCat cat1, GanjoorCat cat2)
        {
            return cat1._Text.CompareTo(cat2._Text);
        }
        public List<GanjoorCat> GetSubCategories(int CatID)
        {
            List<GanjoorCat> lst = new List<GanjoorCat>();
            if (Connected)
            {
                using (DataTable tbl = new DataTable())
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT poet_id, text, url, ID FROM cat WHERE parent_id = " + CatID.ToString() , _con))
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
                        if (CatID == 0)//home
                            lst.Sort(CompareCategoriesByName);
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
                while (Cat!=null && Cat._ParentID != 0)
                {
                    Cat = GetCategory(Cat._ParentID);
                    lst.Insert(0, Cat);
                }
                lst.Insert(0,new GanjoorCat(0, 0, "خانه", 0, ""));
            }
            return lst;
        }
        public bool HasAnyPoem(int CatID)
        {
            return GetPoems(CatID, 1).Count == 1;
        }
        public List<GanjoorPoem> GetPoems(int CatID)
        {
            return GetPoems(CatID, 0);
        }
        public List<GanjoorPoem> GetPoems(int CatID, int Count)
        {
            List<GanjoorPoem> lst = new List<GanjoorPoem>();
            if (Connected)
            {
                using (DataTable tbl = new DataTable())
                {
                    string strQuery = String.Format(
                        "SELECT ID, title, url FROM poem WHERE cat_id = {0} ORDER BY ID"
                        , CatID);
                    if (Count > 0)
                        strQuery += string.Format(" LIMIT {0}", Count);
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(
                        strQuery
                        , _con))
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
        public GanjoorPoet GetPoetForCat(int CatID)
        {
            if (CatID == 0)
                return new GanjoorPoet(0, "همه", 0);
            if (Connected)
            {
                using (DataTable tbl = new DataTable())
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT poet_id FROM cat WHERE id = " + CatID.ToString(), _con))
                    {
                        da.Fill(tbl);
                        System.Diagnostics.Debug.Assert(tbl.Rows.Count < 2);
                        if (1 == tbl.Rows.Count)
                            return
                                GetPoet(Convert.ToInt32(tbl.Rows[0].ItemArray[0]));

                    }
                }
            }
            return null;
        }
        public GanjoorPoet GetPoet(int PoetID)
        {
            if (PoetID == 0)
                return new GanjoorPoet(0, "همه", 0);
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
        public GanjoorPoet GetPoet(string PoetName)
        {
            if (Connected)
            {
                using (DataTable tbl = new DataTable())
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT id, name, cat_id FROM poet WHERE name = '" + PoetName + "'", _con))
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
        public DataTable FindPoemsContaingPhrase(string phrase, int PageStart, int Count, int PoetID)
        {
            if (Connected)
            {
                DataTable tbl = new DataTable();
                {
                    string strQuery = 
                        (PoetID == 0)
                        ?
                        "SELECT poem_id FROM verse WHERE text LIKE '%" + phrase + "%' GROUP BY poem_id LIMIT "+PageStart.ToString()+","+Count.ToString()
                        :
                        "SELECT poem_id FROM (verse INNER JOIN poem ON verse.poem_id=poem.id) INNER JOIN cat ON cat.id =cat_id WHERE verse.text LIKE '%" + phrase + "%' AND poet_id=" + PoetID.ToString() + " GROUP BY poem_id LIMIT " + PageStart.ToString() + "," + Count.ToString()
                        ;
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(strQuery, _con))
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
        public bool IsVerseFaved(int PoemID, int VerseID)
        {
            if (Connected)
            {
                using (DataTable tbl = new DataTable())
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT pos FROM fav WHERE poem_id = " + PoemID.ToString() + " AND verse_id=" + VerseID.ToString(), _con))
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
            Fav(PoemID, -1);
        }
        public void Fav(int PoemID, int VerseID)
        {
            if (Connected)
            {
                using (SQLiteCommand cmd = new SQLiteCommand(_con))
                {
                    cmd.CommandText = "INSERT INTO fav (poem_id, pos, verse_id) VALUES (" + PoemID.ToString() + ","+(MaxFavOrder+1).ToString()+","+VerseID.ToString()+");";
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void UnFav(int PoemID)
        {
            UnFav(PoemID, -1);
        }
        public void UnFav(int PoemID, int VerseID)
        {
            if (Connected)
            {
                using (SQLiteCommand cmd = new SQLiteCommand(_con))
                {
                    if(VerseID != -1)
                        cmd.CommandText = "DELETE FROM fav WHERE poem_id=" + PoemID.ToString()+" AND verse_id="+VerseID.ToString();
                    else
                        cmd.CommandText = "DELETE FROM fav WHERE poem_id=" + PoemID.ToString();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public bool ToggleFav(int PoemID, int VerseID)
        {
            bool faved = VerseID == -1 ? IsPoemFaved(PoemID) : IsVerseFaved(PoemID, VerseID);
            if (faved)
            {
                UnFav(PoemID, VerseID);
                return false;
            }
            else
            {
                Fav(PoemID, VerseID);
                return true;
            }
        }
        public DataTable GetFavs(int PageStart, int Count)
        {
            if (Connected)
            {
                DataTable tbl = new DataTable();
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT poem_id FROM fav GROUP BY poem_id ORDER BY pos LIMIT " + PageStart.ToString() + "," + Count.ToString(), _con))
                    {
                        da.Fill(tbl);
                    }
                    return tbl;
                }
            }
            return null;
        }
        public GanjoorVerse GetPreferablyAFavVerse(int PoemID)
        {
            List<GanjoorVerse> allVerses = GetVerses(PoemID);
            
            for (int v = 0; v < allVerses.Count; v++)
            {
                GanjoorVerse verse = allVerses[v];
                if (IsVerseFaved(verse._PoemID, verse._Order))
                {
                    return verse;
                }
            }
            if (allVerses.Count != 0)
                return allVerses[0];
            return null;
            
        }
        #endregion

        #region Random Poem
        public int GetRandomPoem(List<int> CatIDs)
        {
            if (CatIDs.Count == 0)
                return GetRandomPoem(0);
            else
                return GetRandomPoem(CatIDs[rnd.Next(CatIDs.Count - 1)]);
        }
        public int GetRandomPoem(int CatID)
        {
            if (_randomCatID != CatID)
            {
                if (Connected)
                {
                    using (DataTable tbl = new DataTable())
                    {
                        string strQuery = "SELECT MIN(id), MAX(id) FROM poem";
                        if (CatID != 0)
                            strQuery += " WHERE cat_id=" + CatID;
                        using (SQLiteDataAdapter da = new SQLiteDataAdapter(strQuery, _con))
                        {
                            da.Fill(tbl);
                            if (tbl.Rows.Count > 0)
                            {
                                try
                                {
                                    _randomCatIDMinPoem = Convert.ToInt32(tbl.Rows[0].ItemArray[0]);
                                    _randomCatIDMaxPoem = Convert.ToInt32(tbl.Rows[0].ItemArray[1]);
                                    _randomCatID = CatID;
                                }
                                catch
                                {
                                    return -1;//MIN(id) and MAX(id) are DBNull because CatID has been deleted from database
                                }
                            }
                        }
                    }
                }
            }
            if (_randomCatIDMinPoem != -1 && _randomCatIDMaxPoem != -1)
                return rnd.Next(_randomCatIDMinPoem, _randomCatIDMaxPoem);
            return 0;
        }
        private int _randomCatID = -1;
        private int _randomCatIDMinPoem = -1;
        private int _randomCatIDMaxPoem = -1;
        private Random rnd = new Random();
        #endregion

        #region Versioning
        private const int DatabaseVersion = 1;
        private void UpgradeOldDbs()
        {
            using (DataTable tbl = _con.GetSchema("Tables"))
            {
                #region fav table
                DataRow[] favTable = tbl.Select("Table_Name='fav'");
                
                if (favTable.Length == 0)
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(_con))
                    {
                        cmd.CommandText = "CREATE TABLE fav (poem_id INTEGER, verse_id INTEGER, pos INTEGER);";
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    try
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(_con))
                        {
                            cmd.CommandText = "SELECT verse_id FROM fav LIMIT 0,1";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception exp)
                    {                        
                        if (exp.Message.IndexOf("verse_id") != -1)
                        {
                            
                            using (SQLiteCommand cmd = new SQLiteCommand(_con))
                            {
                                cmd.CommandText = "CREATE TABLE favcpy (poem_id INTEGER, verse_id INTEGER, pos INTEGER);";
                                cmd.ExecuteNonQuery();
                            }
                            using (DataTable fav = new DataTable())
                            {
                                using (SQLiteDataAdapter favAd = new SQLiteDataAdapter("SELECT poem_id, pos FROM fav", _con))
                                {
                                    favAd.Fill(fav);
                                    foreach (DataRow row in fav.Rows)
                                    {
                                        using (SQLiteCommand cmd = new SQLiteCommand(_con))
                                        {
                                            cmd.CommandText = "INSERT INTO favcpy (poem_id, verse_id, pos) VALUES (" + row.ItemArray[0].ToString() + ", -1, " + row.ItemArray[1].ToString() + ")";
                                            cmd.ExecuteNonQuery();
                                        }                         
                                    }
                                }
                            }
                            using (SQLiteCommand cmd = new SQLiteCommand(_con))
                            {
                                cmd.CommandText = "DROP TABLE fav";
                                cmd.ExecuteNonQuery();
                            }
                            using (SQLiteCommand cmd = new SQLiteCommand(_con))
                            {
                                cmd.CommandText = "CREATE TABLE fav (poem_id INTEGER, verse_id INTEGER, pos INTEGER);";
                                cmd.ExecuteNonQuery();
                            }
                             
                            using (DataTable fav = new DataTable())
                            {
                                using (SQLiteDataAdapter favAd = new SQLiteDataAdapter("SELECT poem_id, pos, verse_id FROM favcpy", _con))
                                {
                                    favAd.Fill(fav);
                                    foreach (DataRow row in fav.Rows)
                                    {
                                        using (SQLiteCommand cmd = new SQLiteCommand(_con))
                                        {
                                            cmd.CommandText = "INSERT INTO fav (poem_id, verse_id, pos) VALUES (" + row.ItemArray[0].ToString() + ", -1, " + row.ItemArray[1].ToString() + ")";
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                            using (SQLiteCommand cmd = new SQLiteCommand(_con))
                            {
                                cmd.CommandText = "DROP TABLE favcpy";
                                cmd.ExecuteNonQuery();
                            }

                        }
                    }

                }
                #endregion
                #region version table
                DataRow[] verTable = tbl.Select("Table_Name='gver'");

                string vg3db = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "vg.s3db");
                if (File.Exists(vg3db))
                {
                    if (verTable.Length == 0)
                    {

                        //Version table does not exist, so our verse table
                        //position field values are incorrect,
                        //correct it using vg.s3db file and then create version table

                        SQLiteConnectionStringBuilder conString = new SQLiteConnectionStringBuilder();
                        conString.DataSource = "vg.s3db";
                        conString.DefaultTimeout = 5000;
                        conString.FailIfMissing = true;
                        conString.ReadOnly = true;
                        using (SQLiteConnection vgCon = new SQLiteConnection(conString.ConnectionString))
                        {
                            vgCon.Open();
                            using (SQLiteCommand cmd = new SQLiteCommand(_con))
                            {
                                cmd.CommandText = "BEGIN TRANSACTION;";
                                cmd.ExecuteNonQuery();
                                using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT poem_id, vorder, position FROM verse", vgCon))
                                {
                                    using (DataTable tblVerse = new DataTable())
                                    {
                                        da.Fill(tblVerse);
                                        int poemID = 0;
                                        int vOrder = 1;
                                        int position = 2;
                                        foreach (DataRow row in tblVerse.Rows)
                                        {
                                            cmd.CommandText = "UPDATE verse SET position=" + row.ItemArray[position].ToString() + " WHERE poem_id = " + row.ItemArray[poemID].ToString() + " AND vorder=" + row.ItemArray[vOrder].ToString() + ";";
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                                cmd.CommandText = "CREATE TABLE gver (curver INTEGER);";
                                cmd.ExecuteNonQuery();
                                cmd.CommandText = "INSERT INTO gver (curver) VALUES (" + DatabaseVersion.ToString() + ");";
                                cmd.ExecuteNonQuery();
                                cmd.CommandText = "COMMIT;";
                                cmd.ExecuteNonQuery();
                            }
                        }                        
                    }
                    File.Delete(vg3db);
                }
                #endregion
                #region new poets
                string newdb = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "new.s3db");
                if (File.Exists(newdb))
                {
                    ImportDb(newdb);
                    File.Delete(newdb);
                }
                #endregion
                #region gdb ignore list
                DataRow[] gilTable = tbl.Select("Table_Name='gil'");

                if (gilTable.Length == 0)
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(_con))
                    {
                        cmd.CommandText = "CREATE TABLE gil (cat_id INTEGER);";
                        cmd.ExecuteNonQuery();
                    }
                }
                #endregion
            }
        }
        #endregion

        #region Import Db
        /// <summary>
        /// TODO: This methods needs more checks (for id conflicts) and its performance can significantly
        ///       be improved using a temporary database.
        /// </summary>
        public bool ImportDb(string fileName)
        {
            SQLiteConnection newConnection;
            try
            {
                SQLiteConnectionStringBuilder conString = new SQLiteConnectionStringBuilder();
                conString.DataSource = fileName;
                conString.DefaultTimeout = 5000;
                conString.FailIfMissing = true;
                conString.ReadOnly = false;
                newConnection = new SQLiteConnection(conString.ConnectionString);
                newConnection.Open();
            }
            catch (Exception exp)
            {
                LastError = exp.ToString();
                return false;
            }

            try
            {
                Dictionary<int, int> dicPoets = new Dictionary<int, int>();
                Dictionary<int, int> dicCats = new Dictionary<int, int>();
                using (SQLiteCommand cmd = new SQLiteCommand(_con))
                {
                    cmd.CommandText = "BEGIN TRANSACTION;";
                    cmd.ExecuteNonQuery();                    

                    

                    using (DataTable tbl = new DataTable())
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT id, name, cat_id FROM poet", newConnection))
                    {
                        da.Fill(tbl);
                        foreach (DataRow row in tbl.Rows)
                        {
                            bool insertNewPoet = true;
                            int NewPoetID = Convert.ToInt32(row.ItemArray[0]);
                            string NewPoetName = row.ItemArray[1].ToString();
                            int NewCatID = Convert.ToInt32(row.ItemArray[2]);
                            GanjoorPoet poet = GetPoet(NewPoetName);
                            if (poet != null)
                            {
                                if (poet._ID == NewPoetID)
                                {
                                    insertNewPoet = false;
                                    dicPoets.Add(NewPoetID, NewPoetID);
                                    GanjoorCat NewPoetCat = GetCategory(NewCatID);
                                    if (NewPoetCat != null)
                                    {
                                        if (NewPoetCat._PoetID == NewPoetID)
                                        {
                                            dicCats.Add(NewCatID, NewCatID);
                                        }
                                        else
                                        {
                                            int RealyNewCatID = GenerateNewCatID();
                                            dicCats.Add(NewCatID, GenerateNewCatID());
                                            NewCatID = RealyNewCatID;
                                        }
                                    }
                                }
                                else
                                {
                                    dicPoets.Add(NewPoetID, poet._ID);
                                    dicPoets.Add(NewCatID, poet._CatID);
                                    NewCatID = poet._CatID;
                                }
                            }
                            else
                            {
                                if (GetPoet(NewPoetID) != null)//conflict on IDs
                                {
                                    int RealyNewPoetID = GenerateNewPoetID();
                                    dicPoets.Add(NewPoetID, RealyNewPoetID);
                                    NewPoetID = RealyNewPoetID;

                                    int RealyNewCatID = GenerateNewCatID();
                                    dicCats.Add(NewCatID, RealyNewCatID);
                                    NewCatID = RealyNewCatID;
                                }
                                else //no conflict, insertNew
                                {
                                    dicPoets.Add(NewPoetID, NewPoetID);
                                    GanjoorCat NewPoetCat = GetCategory(NewCatID);
                                    if (NewPoetCat == null)
                                        dicCats.Add(NewCatID, NewCatID);
                                    else
                                    {
                                        int RealyNewCatID = GenerateNewCatID();
                                        dicCats.Add(NewCatID, GenerateNewCatID());
                                        NewCatID = RealyNewCatID;
                                    }
                                }
                            }
                            
                            if(insertNewPoet)
                            {                                
                                cmd.CommandText = String.Format(
                                    "INSERT INTO poet (id, name, cat_id) VALUES ({0}, \"{1}\", {2});",
                                    NewPoetID, NewPoetName, NewCatID
                                    );
                                cmd.ExecuteNonQuery();
                            }                               
                        }
                    }

                    using (DataTable tbl = new DataTable())
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT id, poet_id, text, parent_id, url FROM cat", newConnection))
                    {
                        da.Fill(tbl);                        
                        foreach (DataRow row in tbl.Rows)
                        {
                            int PoetID = Convert.ToInt32(row.ItemArray[1]);
                            int MappedPoetID;
                            if(dicPoets.TryGetValue(PoetID, out MappedPoetID))
                                PoetID = MappedPoetID;
                            int NewCatID = Convert.ToInt32(row.ItemArray[0]);
                            int NewCatParentID = Convert.ToInt32(row.ItemArray[3]);
                            int MappedCatParentID;
                            if (dicCats.TryGetValue(NewCatParentID, out MappedCatParentID))
                            {
                                NewCatParentID = MappedCatParentID;
                            }
                            bool insertNewCategory = true;
                            GanjoorCat NewCat = GetCategory(NewCatID);
                            if (NewCat != null)
                            {
                                if (NewCat._PoetID == PoetID)
                                {
                                    insertNewCategory = false;
                                    int tmp;
                                    if (!dicCats.TryGetValue(NewCatID, out tmp))
                                        dicCats.Add(NewCatID, NewCatID);
                                }
                                else
                                {
                                    int RealyNewCatID = GenerateNewCatID();
                                    dicCats.Add(NewCatID, RealyNewCatID);
                                    NewCatID = RealyNewCatID;
                                }
                            }
                            else
                            {
                                int tmp;
                                if(!dicCats.TryGetValue(NewCatID, out tmp))
                                    dicCats.Add(NewCatID, NewCatID);
                            }
                            if (insertNewCategory)
                            {
                                cmd.CommandText = String.Format(
                                    "INSERT INTO cat (id, poet_id, text, parent_id, url) VALUES ({0}, {1}, \"{2}\", {3}, \"{4}\");",
                                    NewCatID, PoetID, row.ItemArray[2], NewCatParentID, row.ItemArray[4]
                                    );
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    Dictionary<int, int> dicPoemID = new Dictionary<int, int>();
                    using (DataTable tbl = new DataTable())
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT id, cat_id, title, url FROM poem", newConnection))
                    {
                        da.Fill(tbl);
                        foreach (DataRow row in tbl.Rows)
                        {
                            int poemID = Convert.ToInt32(row.ItemArray[0]);
                            if (GetPoem(poemID) != null)
                                poemID = GenerateNewPoemID();
                            int NewCat = dicCats[Convert.ToInt32(row.ItemArray[1])];
                            dicPoemID.Add(Convert.ToInt32(row.ItemArray[0]), poemID);
                            cmd.CommandText = String.Format(
                                "INSERT INTO poem (id, cat_id, title, url) VALUES ({0}, {1}, \"{2}\", \"{3}\");",
                                poemID, NewCat, row.ItemArray[2], row.ItemArray[3]
                                );
                            cmd.ExecuteNonQuery();
                        }
                    }

                    using (DataTable tbl = new DataTable())
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT poem_id, vorder, position, text FROM verse", newConnection))
                    {
                        da.Fill(tbl);
                        foreach (DataRow row in tbl.Rows)
                        {
                            int PoemID = dicPoemID[Convert.ToInt32(row.ItemArray[0])];
                            cmd.CommandText = String.Format(
                                "INSERT INTO verse (poem_id, vorder, position, text) VALUES ({0}, {1}, {2}, \"{3}\");",
                                PoemID, row.ItemArray[1], row.ItemArray[2], row.ItemArray[3]
                                );
                            cmd.ExecuteNonQuery();
                        }
                    }

                    cmd.CommandText = "COMMIT;";
                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception exp)
            {
                LastError = exp.ToString();//probably repeated data
                newConnection.Dispose();
                return false;
            }

            newConnection.Dispose();
            
            return true;
        }
        /// <summary>
        /// This is the fast old ImportDb function which can be used if user is sure that his/her new db
        /// does not conflict with main db
        /// </summary>
        public bool ImportDbFastUnsafe(string fileName)
        {
            SQLiteConnection newConnection;
            try
            {
                SQLiteConnectionStringBuilder conString = new SQLiteConnectionStringBuilder();
                conString.DataSource = fileName;
                conString.DefaultTimeout = 5000;
                conString.FailIfMissing = true;
                conString.ReadOnly = false;
                newConnection = new SQLiteConnection(conString.ConnectionString);
                newConnection.Open();
            }
            catch (Exception exp)
            {
                LastError = exp.ToString();
                return false;
            }

            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand(_con))
                {
                    cmd.CommandText = "BEGIN TRANSACTION;";
                    cmd.ExecuteNonQuery();

                    using (DataTable tbl = new DataTable())
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT id, poet_id, text, parent_id, url FROM cat", newConnection))
                    {
                        da.Fill(tbl);
                        foreach (DataRow row in tbl.Rows)
                        {
                            cmd.CommandText = String.Format(
                                "INSERT INTO cat (id, poet_id, text, parent_id, url) VALUES ({0}, {1}, \"{2}\", {3}, \"{4}\");",
                                row.ItemArray[0], row.ItemArray[1], row.ItemArray[2], row.ItemArray[3], row.ItemArray[4]
                                );
                            cmd.ExecuteNonQuery();
                        }
                    }

                    using (DataTable tbl = new DataTable())
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT id, name, cat_id FROM poet", newConnection))
                    {
                        da.Fill(tbl);
                        foreach (DataRow row in tbl.Rows)
                        {
                            cmd.CommandText = String.Format(
                                "INSERT INTO poet (id, name, cat_id) VALUES ({0}, \"{1}\", {2});",
                                row.ItemArray[0], row.ItemArray[1], row.ItemArray[2]
                                );
                            cmd.ExecuteNonQuery();
                        }
                    }

                    using (DataTable tbl = new DataTable())
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT id, cat_id, title, url FROM poem", newConnection))
                    {
                        da.Fill(tbl);
                        foreach (DataRow row in tbl.Rows)
                        {
                            cmd.CommandText = String.Format(
                                "INSERT INTO poem (id, cat_id, title, url) VALUES ({0}, {1}, \"{2}\", \"{3}\");",
                                row.ItemArray[0], row.ItemArray[1], row.ItemArray[2], row.ItemArray[3]
                                );
                            cmd.ExecuteNonQuery();
                        }
                    }

                    using (DataTable tbl = new DataTable())
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT poem_id, vorder, position, text FROM verse", newConnection))
                    {
                        da.Fill(tbl);
                        foreach (DataRow row in tbl.Rows)
                        {
                            cmd.CommandText = String.Format(
                                "INSERT INTO verse (poem_id, vorder, position, text) VALUES ({0}, {1}, {2}, \"{3}\");",
                                row.ItemArray[0], row.ItemArray[1], row.ItemArray[2], row.ItemArray[3]
                                );
                            cmd.ExecuteNonQuery();
                        }
                    }

                    cmd.CommandText = "COMMIT;";
                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception exp)
            {
                LastError = exp.ToString();//probable repeated data
                newConnection.Dispose();
                return false;
            }

            newConnection.Dispose();

            return true;
        }
        #endregion

        #region Import/Export Favs
        public bool ExportFavs(string fileName)
        {
            SQLiteConnectionStringBuilder conString = new SQLiteConnectionStringBuilder();
            conString.DataSource = fileName;
            conString.DefaultTimeout = 5000;
            conString.FailIfMissing = false;
            conString.ReadOnly = false;
            using (SQLiteConnection connection = new SQLiteConnection(conString.ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "CREATE TABLE fav (poem_id INTEGER, verse_id INTEGER, pos INTEGER);";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "BEGIN TRANSACTION;";
                    cmd.ExecuteNonQuery();

                    using (DataTable tbl = new DataTable())
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT poem_id, verse_id, pos FROM fav", _con))
                    {
                        da.Fill(tbl);
                        foreach (DataRow row in tbl.Rows)
                        {
                            cmd.CommandText = String.Format(
                                "INSERT INTO fav (poem_id, verse_id, pos) VALUES ({0}, {1}, {2});",
                                row.ItemArray[0], row.ItemArray[1], row.ItemArray[2]
                                );
                            cmd.ExecuteNonQuery();
                        }
                    }

                    cmd.CommandText = "COMMIT;";
                    cmd.ExecuteNonQuery();
                }


            }
            return true;
        }
        public int ImportMixFavs(string fileName, out int dupFavs)
        {
            int ImportedFavs = 0;
            dupFavs = 0;
            SQLiteConnectionStringBuilder conString = new SQLiteConnectionStringBuilder();
            conString.DataSource = fileName;
            conString.DefaultTimeout = 5000;
            conString.FailIfMissing = true;
            conString.ReadOnly = true;
            using (SQLiteConnection connection = new SQLiteConnection(conString.ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(_con))
                {
                    cmd.CommandText = "BEGIN TRANSACTION;";
                    cmd.ExecuteNonQuery();

                    using (DataTable tbl = new DataTable())
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT poem_id, verse_id, pos FROM fav", connection))
                    {
                        da.Fill(tbl);
                        foreach (DataRow row in tbl.Rows)
                        {
                            int poemID = Convert.ToInt32(row.ItemArray[0]);
                            int verseID = Convert.ToInt32(row.ItemArray[1]);
                            int pos = Convert.ToInt32(row.ItemArray[2]);
                            if (!this.IsVerseFaved(poemID, verseID))
                            {
                                cmd.CommandText = String.Format(
                                    "INSERT INTO fav (poem_id, verse_id, pos) VALUES ({0}, {1}, {2});",
                                    poemID, verseID, pos
                                    );
                                cmd.ExecuteNonQuery();
                                ImportedFavs++;
                            }
                            else
                                dupFavs++;
                        }
                    }

                    cmd.CommandText = "COMMIT;";
                    cmd.ExecuteNonQuery();
                }


            }

            return ImportedFavs;
        }
        #endregion

        #region Editing Options
        private int CachedMaxCatID;
        private int CachedMaxPoemID;
        /// <returns>PoetID</returns>
        public int NewPoet(string PoetName)
        {
            if (!Connected)
                return -1;
            foreach (GanjoorPoet Poet in this.Poets)
                if (Poet._Name == PoetName)
                    return -1;//نام تکراری
            int NewPoetID = GenerateNewPoetID();

            GanjoorCat poetCat = CreateNewCategory(PoetName, 0, NewPoetID);
            if (poetCat == null)
                return -1;
            using (SQLiteCommand cmd = new SQLiteCommand(_con))
            {
                cmd.CommandText = String.Format(
                    "INSERT INTO poet (id, name, cat_id) VALUES ({0}, \"{1}\", {2});",
                    NewPoetID, PoetName, poetCat._ID
                    );
                cmd.ExecuteNonQuery();
            }
            return NewPoetID;
        }

        private int GenerateNewPoetID()
        {
            int NewPoetID;
            using (DataTable tbl = new DataTable())
            {
                using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT MAX(id) FROM poet", _con))
                {
                    da.Fill(tbl);
                    if (tbl.Rows.Count == 1)
                    {
                        try
                        {
                            NewPoetID = Convert.ToInt32(tbl.Rows[0].ItemArray[0]);
                        }
                        catch
                        {
                            NewPoetID = Settings.Default.MinNewPoetID;
                        }
                        if (NewPoetID < Settings.Default.MinNewPoetID)
                            NewPoetID = Settings.Default.MinNewPoetID;
                        else
                            NewPoetID++;
                    }
                    else
                        NewPoetID = Settings.Default.MinNewPoetID;
                }
            }
            return NewPoetID;
        }
        public bool SetPoetName(int PoetID, string NewName)
        {
            if (!Connected)
                return false;
            foreach (GanjoorPoet Poet in this.Poets)
                if (Poet._ID != PoetID && Poet._Name == NewName)
                    return false;//نام تکراری
            GanjoorPoet poet = GetPoet(PoetID);
            NewName = NewName.Replace("\"", "\"\"");
            using (SQLiteCommand cmd = new SQLiteCommand(_con))
            {
                cmd.CommandText = String.Format(
                    "UPDATE poet SET name = \"{0}\" WHERE id="+poet._ID,
                    NewName
                    );
                cmd.ExecuteNonQuery();
                cmd.CommandText = String.Format(
                    "UPDATE cat SET text = \"{0}\" WHERE id=" + poet._CatID,
                    NewName
                    );
                cmd.ExecuteNonQuery();
            }
            return true;
        }
        public GanjoorCat CreateNewCategory(string CategoryName, int ParentCatID, int PoetID)
        {
            int NewCatID;
            using (DataTable tbl = new DataTable())
            {
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(String.Format("SELECT * FROM cat WHERE parent_id={0} AND text LIKE \"{1}\"", ParentCatID, CategoryName), _con))
                {
                    da.Fill(tbl);
                    if (tbl.Rows.Count > 0)
                        return null;
                }
            }

            NewCatID = GenerateNewCatID();

            using (SQLiteCommand cmd = new SQLiteCommand(_con))
            {
                cmd.CommandText = String.Format(
                    "INSERT INTO cat (id, poet_id, text, parent_id, url) VALUES ({0}, {1}, \"{2}\", {3}, \"{4}\");",
                    NewCatID, PoetID, CategoryName, ParentCatID, ""
                    );
                cmd.ExecuteNonQuery();

                return GetCategory(NewCatID);
            }

        }

        private int GenerateNewCatID()
        {
            int NewCatID;
            using (DataTable tbl = new DataTable())
            {
                if (CachedMaxCatID == 0)
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT MAX(id) FROM cat", _con))
                    {
                        da.Fill(tbl);
                        if (tbl.Rows.Count == 1)
                        {
                            try
                            {
                                NewCatID = Convert.ToInt32(tbl.Rows[0].ItemArray[0]);
                            }
                            catch
                            {
                                NewCatID = Settings.Default.MinNewCatID;
                            }
                            if (NewCatID < Settings.Default.MinNewCatID)
                                NewCatID = Settings.Default.MinNewCatID;
                            else
                                NewCatID++;
                        }
                        else
                            NewCatID = Settings.Default.MinNewCatID;
                        CachedMaxCatID = NewCatID;
                    }
                }
                else
                {
                    NewCatID = CachedMaxCatID + 1;
                    CachedMaxCatID++;
                }
            }
            return NewCatID;
        }
        public bool SetCatTitle(int CatID, string NewTitle)
        {
            if (!Connected)
                return false;
            GanjoorCat cat = GetCategory(CatID);
            if (null == cat)
                return false;
            if (cat._ParentID == 0)
                return false;
            using (DataTable tbl = new DataTable())
            {
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(String.Format("SELECT * FROM cat WHERE id<>{0} AND parent_id={1} AND text LIKE \"{2}\"", CatID, cat._ParentID, NewTitle), _con))
                {
                    da.Fill(tbl);
                    if (tbl.Rows.Count > 0)
                        return false;
                }
            }
            NewTitle = NewTitle.Replace("\"", "\"\"");
            using (SQLiteCommand cmd = new SQLiteCommand(_con))
            {
                cmd.CommandText = String.Format(
                    "UPDATE cat SET text = \"{0}\" WHERE id=" + CatID,
                    NewTitle
                    );
                cmd.ExecuteNonQuery();
            }
            return true;
        }
        public bool SetCatID(int CatID, int NewCatID)
        {
            if (!Connected)
                return false;
            using (SQLiteCommand cmd = new SQLiteCommand(_con))
            {
                cmd.CommandText = String.Format(
                    "UPDATE cat SET id = {0} WHERE id= {1}",
                    NewCatID, CatID
                    );
                cmd.ExecuteNonQuery();
                cmd.CommandText = String.Format(
                    "UPDATE poem SET cat_id = {0} WHERE cat_id= {1}",
                    NewCatID, CatID
                    );
                cmd.ExecuteNonQuery();
                cmd.CommandText = String.Format(
                    "UPDATE poet SET cat_id = {0} WHERE cat_id= {1}",
                    NewCatID, CatID
                    );
                cmd.ExecuteNonQuery();
            }
            return true;
        }
        public GanjoorPoem CreateNewPoem(string PoemTitle, int CatID)
        {
            
            int NewPoemID = GenerateNewPoemID();
            using (SQLiteCommand cmd = new SQLiteCommand(_con))
            {
                cmd.CommandText = String.Format(
                        "INSERT INTO poem (id, cat_id, title, url) VALUES ({0}, {1}, \"{2}\", \"{3}\");",
                        NewPoemID, CatID, PoemTitle, ""
                    );
                cmd.ExecuteNonQuery();

                return GetPoem(NewPoemID);
            }
        }

        private int GenerateNewPoemID()
        {
            int NewPoemID;
            if (CachedMaxPoemID == 0)
            {
                using (DataTable tbl = new DataTable())
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT MAX(id) FROM poem", _con))
                    {
                        da.Fill(tbl);
                        if (tbl.Rows.Count == 1)
                        {
                            try
                            {
                                NewPoemID = Convert.ToInt32(tbl.Rows[0].ItemArray[0]) + 1;
                            }
                            catch
                            {
                                NewPoemID = Settings.Default.MinNewPoemID;
                            }
                            if (NewPoemID < Settings.Default.MinNewPoemID)
                                NewPoemID = Settings.Default.MinNewPoemID;
                        }
                        else
                            NewPoemID = Settings.Default.MinNewPoemID;
                        CachedMaxPoemID = NewPoemID;
                    }
                }
            }
            else
            {
                CachedMaxPoemID++;
                NewPoemID = CachedMaxPoemID;
            }
            return NewPoemID;
        }
        public bool SetPoemTitle(int PoemID, string NewTitle)
        {
            if (!Connected)
                return false;
            GanjoorPoem poem = GetPoem(PoemID);
            if (null == poem)
                return false;
            NewTitle = NewTitle.Replace("\"", "\"\"");
            using (SQLiteCommand cmd = new SQLiteCommand(_con))
            {
                cmd.CommandText = String.Format(
                    "UPDATE poem SET title = \"{0}\" WHERE id=" + PoemID,
                    NewTitle
                    );
                cmd.ExecuteNonQuery();
            }
            return true;
        }
        public bool ChangePoemCategory(int PoemID, int CatID)
        {
            if (!Connected)
                return false;
            GanjoorPoem poem = GetPoem(PoemID);
            if (null == poem)
                return false;
            using (SQLiteCommand cmd = new SQLiteCommand(_con))
            {
                cmd.CommandText = String.Format(
                    "UPDATE poem SET cat_id = {0} WHERE id=" + PoemID,
                    CatID
                    );
                cmd.ExecuteNonQuery();
            }
            return true;
        }
        public bool DeletePoem(int PoemID)
        {
            if (!Connected)
                return false;
            GanjoorPoem poem = GetPoem(PoemID);
            if (null == poem)
                return false;
            using (SQLiteCommand cmd = new SQLiteCommand(_con))
            {
                cmd.CommandText = String.Format(
                    "DELETE FROM verse WHERE poem_id={0}",
                    PoemID
                    );
                cmd.ExecuteNonQuery();

                cmd.CommandText = String.Format(
                    "DELETE FROM poem WHERE id={0}" ,
                    PoemID
                    );
                cmd.ExecuteNonQuery();
            }
            return true;
        }
        public GanjoorVerse CreateNewVerse(int PoemID, int beforeVerseOrder, VersePosition Position)
        {
            if (!Connected)
                return null;
            List<string> updateCommands = new List<string>();
            using (DataTable tbl = new DataTable())
            {
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(
                    String.Format(
                    "SELECT vorder FROM verse WHERE poem_id={0} AND vorder>{1} ORDER BY vorder DESC",
                    PoemID, beforeVerseOrder
                    )
                    , _con))
                {
                    da.Fill(tbl);
                    foreach (DataRow Row in tbl.Rows)
                    {
                        int vorder = Convert.ToInt32(Row.ItemArray[0]);
                        updateCommands.Add(
                            String.Format("UPDATE verse SET vorder={0} WHERE poem_id={1} AND vorder={2}",
                            vorder + 1, PoemID, vorder));
                    }
                }
            }
            foreach (string updateCommand in updateCommands)
            {
                using (SQLiteCommand cmd = new SQLiteCommand(updateCommand, _con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            using (SQLiteCommand cmd = new SQLiteCommand(_con))
            {
                cmd.CommandText = String.Format(
                        "INSERT INTO verse (poem_id, vorder, position, text) VALUES ({0}, {1}, {2}, \"{3}\");",
                        PoemID, beforeVerseOrder+1, (int)Position, ""
                    );
                cmd.ExecuteNonQuery();
                return new GanjoorVerse(PoemID, beforeVerseOrder + 1, Position, "");
            }
        }
        public bool SetVerseText(int PoemID, int Order, string Text)
        {
            if (!Connected)
                return false;
            Text = Text.Replace("\"", "\"\"");
            using (SQLiteCommand cmd = new SQLiteCommand(_con))
            {
                cmd.CommandText = String.Format(
                    "UPDATE verse SET text = \"{0}\" WHERE poem_id={1} AND vorder={2}",
                    Text, PoemID, Order
                    );
                cmd.ExecuteNonQuery();
            }
            return true;
        }
        public bool DeleteVerses(int PoemID, List<int> VerseOrders)
        {
            if (!Connected)
                return false;
            GanjoorPoem poem = GetPoem(PoemID);
            if (null == poem)
                return false;
            BeginBatchOperation();
            foreach(int vorder in VerseOrders)
            using (SQLiteCommand cmd = new SQLiteCommand(_con))
            {
                cmd.CommandText = String.Format(
                    "DELETE FROM verse WHERE poem_id={0} AND vorder={1}",
                    PoemID, vorder
                    );
                cmd.ExecuteNonQuery();
            }
            List<GanjoorVerse> vs = GetVerses(PoemID);
            for(int v=0; v<vs.Count; v++)
            {
                using (SQLiteCommand cmd = new SQLiteCommand(_con))
                {
                    cmd.CommandText = String.Format(
                        "UPDATE verse SET vorder= {0} WHERE poem_id={1} AND vorder={2}",
                        -(1+v), PoemID, vs[v]._Order
                        );
                    cmd.ExecuteNonQuery();
                }
            }
            for (int v = 0; v < vs.Count; v++)
            {
                using (SQLiteCommand cmd = new SQLiteCommand(_con))
                {
                    cmd.CommandText = String.Format(
                        "UPDATE verse SET vorder= {0} WHERE poem_id={1} AND vorder={2}",
                        (1 + v), PoemID, -(1 + v)
                        );
                    cmd.ExecuteNonQuery();
                }
            }
            CommitBatchOperation();
            return true;
        }
        public void DeleteCat(int CatID)
        {
            BeginBatchOperation();
            DRY_DeleteCat(GetCategory(CatID));
            CommitBatchOperation();

        }        
        private void DRY_DeleteCat(GanjoorCat Cat)
        {
            List<GanjoorCat> SubCats = GetSubCategories(Cat._ID);
            foreach (GanjoorCat SubCat in SubCats)
                DRY_DeleteCat(SubCat);
            using (SQLiteCommand cmd = new SQLiteCommand(_con))
            {
                cmd.CommandText = String.Format("DELETE FROM verse WHERE poem_id IN (SELECT id FROM poem WHERE cat_id={0});", Cat._ID);
                cmd.ExecuteNonQuery();
                cmd.CommandText = String.Format("DELETE FROM poem WHERE cat_id={0};", Cat._ID);
                cmd.ExecuteNonQuery();
                cmd.CommandText = String.Format("DELETE FROM cat WHERE id={0};", Cat._ID);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeletePoet(int PoetID)
        {
            using (DataTable tbl = new DataTable())
            {
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(String.Format("SELECT cat_id FROM poet WHERE id={0}", PoetID), _con))
                {
                    da.Fill(tbl);
                    foreach (DataRow Row in tbl.Rows)
                        DeleteCat(Convert.ToInt32(Row.ItemArray[0]));
                }
            }
            using (SQLiteCommand cmd = new SQLiteCommand(_con))
            {
                cmd.CommandText = String.Format("DELETE FROM poet WHERE id={0};", PoetID);
                cmd.ExecuteNonQuery();
            }
        }
        public bool BeginBatchOperation()
        {
            if (!Connected)
                return false;
            using (SQLiteCommand cmd = new SQLiteCommand(_con))
            {
                cmd.CommandText = "BEGIN TRANSACTION";
                cmd.ExecuteNonQuery();
            }
            return true;
        }
        public bool CommitBatchOperation()
        {
            if (!Connected)
                return false;
            using (SQLiteCommand cmd = new SQLiteCommand(_con))
            {
                cmd.CommandText = "COMMIT";
                cmd.ExecuteNonQuery();
            }
            return true;
        }
        public bool SetPoemID(int PoemID, int NewPoemID)
        {
            if (!Connected)
                return false;
            using (SQLiteCommand cmd = new SQLiteCommand(_con))
            {
                cmd.CommandText = String.Format(
                    "UPDATE poem SET id = {0} WHERE id= {1}",
                    NewPoemID, PoemID
                    );
                cmd.ExecuteNonQuery();
                cmd.CommandText = String.Format(
                    "UPDATE verse SET poem_id = {0} WHERE poem_id= {1}",
                    NewPoemID, PoemID
                    );
                cmd.ExecuteNonQuery();
            }
            return true;
        }
        #endregion

        #region Export Poems
        public bool ExportCategory(string fileName, int CatID)
        {
            return Export(fileName, CatID, false);
        }
        public bool ExportPoet(string fileName, int PoetID)
        {
            GanjoorPoet poet = GetPoet(PoetID);
            if (null == poet)
                return false;
            return Export(fileName, poet._CatID, true);
        }
        private void CreateEmptyDB(SQLiteConnection connection)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(connection))
            {
                cmd.CommandText = "BEGIN TRANSACTION;" +
                            "CREATE TABLE [cat] ([id] INTEGER  PRIMARY KEY NOT NULL,[poet_id] INTEGER  NULL,[text] NVARCHAR(100)  NULL,[parent_id] INTEGER  NULL,[url] NVARCHAR(255)  NULL);"
                            +
                            "CREATE TABLE poem (id INTEGER PRIMARY KEY, cat_id INTEGER, title NVARCHAR(255), url NVARCHAR(255));"
                            +
                            "CREATE TABLE [poet] ([id] INTEGER  PRIMARY KEY NOT NULL,[name] NVARCHAR(20)  NULL,[cat_id] INTEGER  NULL  NULL);"
                            +
                            "CREATE TABLE [verse] ([poem_id] INTEGER  NULL,[vorder] INTEGER  NULL,[position] INTEGER  NULL,[text] TEXT  NULL);"
                            +
                            "COMMIT;";
                cmd.ExecuteNonQuery();
            }
        }
        private bool Export(string fileName, int CatID, bool ExportPoet)

        {
            SQLiteConnection newConnection;
            try
            {
                SQLiteConnectionStringBuilder conString = new SQLiteConnectionStringBuilder();
                conString.DataSource = fileName;
                conString.DefaultTimeout = 5000;
                conString.FailIfMissing = false;
                conString.ReadOnly = false;
                newConnection = new SQLiteConnection(conString.ConnectionString);
                newConnection.Open();
            }
            catch (Exception exp)
            {
                LastError = exp.ToString();
                return false;
            }

            try
            {
                CreateEmptyDB(newConnection);
                if (ExportPoet)
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(newConnection))
                    {
                        using (DataTable tbl = new DataTable())
                        using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT id, name, cat_id FROM poet WHERE cat_id=" + CatID, _con))
                        {
                            da.Fill(tbl);
                            foreach (DataRow row in tbl.Rows)
                            {
                                cmd.CommandText = String.Format(
                                    "INSERT INTO poet (id, name, cat_id) VALUES ({0}, \"{1}\", {2});",
                                    row.ItemArray[0], row.ItemArray[1], row.ItemArray[2]
                                    );
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                }
                ExportCat(CatID, newConnection);
            }
            catch (Exception exp)
            {
                LastError = exp.ToString();//probable repeated data
                newConnection.Dispose();
                return false;
            }

            newConnection.Dispose();
            
            return true;
        }
        private void ExportCat(int CatID, SQLiteConnection newConnection)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(newConnection))
            {
                cmd.CommandText = "BEGIN TRANSACTION;";
                cmd.ExecuteNonQuery();

                using (DataTable tbl = new DataTable())
                using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT id, poet_id, text, parent_id, url FROM cat WHERE id=" + CatID, _con))
                {
                    da.Fill(tbl);
                    foreach (DataRow row in tbl.Rows)
                    {
                        cmd.CommandText = String.Format(
                            "INSERT INTO cat (id, poet_id, text, parent_id, url) VALUES ({0}, {1}, \"{2}\", {3}, \"{4}\");",
                            row.ItemArray[0], row.ItemArray[1], row.ItemArray[2], row.ItemArray[3], row.ItemArray[4]
                            );
                        cmd.ExecuteNonQuery();
                    }
                }

                using (DataTable tbl = new DataTable())
                using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT id, cat_id, title, url FROM poem WHERE cat_id=" + CatID, _con))
                {
                    da.Fill(tbl);
                    foreach (DataRow row in tbl.Rows)
                    {
                        cmd.CommandText = String.Format(
                            "INSERT INTO poem (id, cat_id, title, url) VALUES ({0}, {1}, \"{2}\", \"{3}\");",
                            row.ItemArray[0], row.ItemArray[1], row.ItemArray[2], row.ItemArray[3]
                            );
                        cmd.ExecuteNonQuery();
                    }
                }

                using (DataTable tbl = new DataTable())
                using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT poem_id, vorder, position, text FROM verse WHERE poem_id IN (SELECT id FROM poem WHERE cat_id=" + CatID + ")", _con))
                {
                    da.Fill(tbl);
                    foreach (DataRow row in tbl.Rows)
                    {
                        cmd.CommandText = String.Format(
                            "INSERT INTO verse (poem_id, vorder, position, text) VALUES ({0}, {1}, {2}, \"{3}\");",
                            row.ItemArray[0], row.ItemArray[1], row.ItemArray[2], row.ItemArray[3]
                            );
                        cmd.ExecuteNonQuery();
                    }
                }

                cmd.CommandText = "COMMIT;";
                cmd.ExecuteNonQuery();

            }

            List<GanjoorCat> SubCats = GetSubCategories(CatID);
            foreach (GanjoorCat SubCat in SubCats)
                ExportCat(SubCat._ID, newConnection);
        }        
        #endregion

        #region Replace
        public void Replace(string searchterm, string replacement)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(_con))
            {
                cmd.CommandText = "UPDATE poet SET name = REPLACE(name, '" + searchterm + "', '" + replacement + "');";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "UPDATE cat SET text = REPLACE(text, '" + searchterm + "', '" + replacement + "');";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "UPDATE poem SET title = REPLACE(title, '" + searchterm + "', '" + replacement + "');";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "UPDATE verse SET text = REPLACE(text, '" + searchterm + "', '" + replacement + "');";
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Edit IDs
        public List<int> GetAllSubCats(int CatID)
        {
            List<int> subIDs = new List<int>();
            List<GanjoorCat> subs = GetSubCategories(CatID);
            foreach (GanjoorCat sub in subs)
            {
                subIDs.Add(sub._ID);
                List<int> subsubs = GetAllSubCats(sub._ID);
                subIDs.AddRange(subsubs);
            }
            return subIDs;
        }
        private int GetMainCatID(int PoetID)
        {
            GanjoorPoet poet = GetPoet(PoetID);
            if (poet == null)
                return -1;
            return poet._CatID;
        }
        private int GetMinPoemID(int MainCatID)
        {
            if (MainCatID != -1)
            {
                List<int> cats = GetAllSubCats(MainCatID);
                cats.Add(MainCatID);
                string strQuery = "SELECT MIN(id) FROM poem WHERE cat_id IN (";
                for(int i=0; i<cats.Count - 1 ; i++)
                {
                    strQuery += cats[i].ToString();
                    strQuery += ", ";
                }
                if (cats.Count - 1 >= 0)
                {
                    strQuery += cats[cats.Count - 1].ToString();
                }
                strQuery += ");";
                using (DataTable tbl = new DataTable())
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(strQuery, _con))
                    {
                        da.Fill(tbl);
                        if (tbl.Rows.Count > 0)
                        {
                            return Convert.ToInt32(tbl.Rows[0].ItemArray[0]);
                        }
                    }
                }

            }
            return -1;
        }
        public void GetMinIDs(int PoetID, out int MinCatID, out int MinPoemID)
        {
            MinCatID = GetMainCatID(PoetID);
            MinPoemID = GetMinPoemID(MinCatID);
        }
        public bool ChangePoetID(int PoetID, int NewID)
        {
            if (GetPoet(NewID) != null)
                return false;
            if (BeginBatchOperation())
            {
                using
                    (
                    SQLiteCommand cmd = new SQLiteCommand(
                    String.Format("UPDATE poet SET id = {0} WHERE id = {1}", NewID, PoetID),
                    _con)
                    )
                {
                    cmd.ExecuteNonQuery();
                }
                using
                    (
                    SQLiteCommand cmd = new SQLiteCommand(
                    String.Format("UPDATE cat SET poet_id = {0} WHERE poet_id = {1}", NewID, PoetID),
                    _con)
                    )
                {
                    cmd.ExecuteNonQuery();
                }
                return CommitBatchOperation();
            }
            return false;
        }
        public bool ChangeCatIDs(int PoetID, int NewStartCatID)
        {
            List<int> cats = GetAllSubCats(GetMainCatID(PoetID));
            cats.Insert(0, GetMainCatID(PoetID));
            if (BeginBatchOperation())
            {
                Dictionary<int, int> ids = new Dictionary<int, int>();
                int NewCatID = NewStartCatID;

                foreach (int CatID in cats)
                {
                    while (GetCategory(NewCatID) != null)
                        NewCatID++;                    
                    using
                        (
                        SQLiteCommand cmd = new SQLiteCommand(
                        String.Format("UPDATE cat SET id = {0} WHERE id = {1}", NewCatID, CatID),
                        _con)
                        )
                    {
                        cmd.ExecuteNonQuery();
                    }
                    using
                        (
                        SQLiteCommand cmd = new SQLiteCommand(
                        String.Format("UPDATE cat SET parent_id = {0} WHERE parent_id = {1}", NewCatID, CatID),
                        _con)
                        )
                    {
                        cmd.ExecuteNonQuery();
                    }
                    using
                        (
                        SQLiteCommand cmd = new SQLiteCommand(
                        String.Format("UPDATE poet SET cat_id = {0} WHERE cat_id = {1}", NewCatID, CatID),
                        _con)
                        )
                    {
                        cmd.ExecuteNonQuery();
                    }
                    using
                        (
                        SQLiteCommand cmd = new SQLiteCommand(
                        String.Format("UPDATE poem SET cat_id = {0} WHERE cat_id = {1}", NewCatID, CatID),
                        _con)
                        )
                    {
                        cmd.ExecuteNonQuery();
                    }
                    NewCatID++;
                }
                return CommitBatchOperation();
            }
            return false;
        }
        public bool ChangePoemIDs(int PoetID, int NewStartPoemID)
        {
            List<int> cats = GetAllSubCats(GetMainCatID(PoetID));
            cats.Insert(0, GetMainCatID(PoetID));
            if (BeginBatchOperation())
            {
                int NewPoemID = NewStartPoemID;

                foreach (int CatID in cats)
                {
                    List<GanjoorPoem> poems = GetPoems(CatID);
                    foreach (GanjoorPoem poem in poems)
                    {
                        while (GetPoem(NewPoemID) != null)
                            NewPoemID++;
                        using
                            (
                            SQLiteCommand cmd = new SQLiteCommand(
                            String.Format("UPDATE poem SET id = {0} WHERE id = {1}", NewPoemID, poem._ID),
                            _con)
                            )
                        {
                            cmd.ExecuteNonQuery();
                        }
                        using
                            (
                            SQLiteCommand cmd = new SQLiteCommand(
                            String.Format("UPDATE verse SET poem_id = {0} WHERE poem_id = {1}", NewPoemID, poem._ID),
                            _con)
                            )
                        {
                            cmd.ExecuteNonQuery();
                        }
                        NewPoemID++;
                    }
                }
                return CommitBatchOperation();
            }
            return false;
        }
        #endregion

        #region GDB Ignore List
        public bool IsInGDBIgnoreList(int CatID)
        {
            if (Connected)
            {
                using (DataTable tbl = new DataTable())
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(
                        string.Format("SELECT cat_id FROM gil WHERE cat_id = {0}", CatID)
                        , _con))
                    {
                        da.Fill(tbl);
                        return tbl.Rows.Count != 0;
                    }
                }

            }
            return false;
        }
        public void AddToGDBIgnoreList(int CatID)
        {
            if (Connected)
            {
                if(!IsInGDBIgnoreList(CatID))
                using (DataTable tbl = new DataTable())
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(
                        string.Format("INSERT INTO gil (cat_id) VALUES ({0})", CatID)
                        , _con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }            
        }
        #endregion
    }
}
