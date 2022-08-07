using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ganjoor
{
    public class DbBrowser
    {
        #region Constructor
        /// <summary>
        /// Default constructor searchers for ganjoor.s3db file in path provided
        /// by ganjoor.ini configuration file in application executable path,
        /// if it does not exist it searches LocalApplicationData evv. variable path,
        /// and if there is no ganjoor.s3db in there, it searches application path
        /// </summary>
        public DbBrowser()
        {
            _dbfilepath = DefaultDbPath;
            if (!File.Exists(_dbfilepath))
            {
                _dbfilepath = "ganjoor.s3db";
            }

            try
            {
                Init(_dbfilepath);
            }
            catch (BadImageFormatException exp)
            {
                GAdvisor.AdviseOnSQLiteDllNotfound();
                throw exp;
            }
            catch (FileNotFoundException exp)//this is where incorrect version of System.Data.SQLite.DLL causes problems
            {
                if (exp.FileName.IndexOf("System.Data.SQLite", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    GAdvisor.AdviseOnSQLiteDllNotfound();
                }
                //throw exp; //92-04-14
            }
        }

        /// <summary>
        /// opens EXISTING sqlite poem database
        /// </summary>
        public DbBrowser(string sqliteDatabaseNameFileName)
        {
            Init(sqliteDatabaseNameFileName);
        }

        private int _MinNewPoetID = 1001;
        private int _MinNewCatID = 10001;
        private int _MinNewPoemID = 100001;

        /// <summary>
        /// Default Db Path
        /// </summary>
        public static string DefaultDbPath
        {
            get
            {
                var iniFilePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "ganjoor.ini");
                if (!File.Exists(iniFilePath))
                    iniFilePath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ganjoor"), "ganjoor.ini");
                var dbfilepath = string.Empty;
                if (File.Exists(iniFilePath))
                {
                    var gParaser = new GINIParser(iniFilePath);
                    try
                    {
                        dbfilepath = gParaser.Values["Database"]["Path"];
                        dbfilepath = Path.Combine(dbfilepath, "ganjoor.s3db");
                    }
                    catch
                    {
                    }
                }
                if (string.IsNullOrEmpty(dbfilepath))
                    dbfilepath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ganjoor"), "ganjoor.s3db");
                return dbfilepath;
            }
        }

        /// <summary>
        /// creates a new poem database
        /// </summary>
        /// <param name="failIfExists">
        /// if true returns null for existing databases and does not modify them,
        /// if false opens existing database (it is not overwrited and its data remains unchanged)
        /// </param>
        public static DbBrowser CreateNewPoemDatabase(string fileName, bool failIfExists)
        {

            try
            {
                if (File.Exists(fileName))
                {
                    if (failIfExists)
                        return null;
                }
                else
                {
                    var conString = new SQLiteConnectionStringBuilder {
                        DataSource = fileName,
                        DefaultTimeout = 5000,
                        FailIfMissing = false,
                        ReadOnly = false
                    };
                    using var newConnection = new SQLiteConnection(conString.ConnectionString);
                    newConnection.Open();
                    CreateEmptyDB(newConnection);
                }
                return new DbBrowser(fileName);
            }
            catch
            {
                return null;
            }
        }
        private void Init(string sqliteDatabaseNameFileName)
        {
            try
            {
                _dbfilepath = sqliteDatabaseNameFileName;
                var conString = new SQLiteConnectionStringBuilder {
                    DataSource = sqliteDatabaseNameFileName,
                    DefaultTimeout = 5000,
                    FailIfMissing = true,
                    ReadOnly = false
                };
                _con = new SQLiteConnection(conString.ConnectionString);
                _con.Open();
            }
            catch (Exception exp)
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
            CloseDb();
        }

        public void CloseDb()
        {
            if (_con != null)
            {
                _con.Dispose();
                _con = null;
            }
        }
        #endregion

        #region Variables
        private SQLiteConnection _con;
        public string LastError = string.Empty;
        private string _dbfilepath = string.Empty;
        #endregion 

        #region Properties & Methods
        public string DbFilePath => _dbfilepath;

        public bool Failed => null == _con;

        public bool Connected => null != _con;

        public List<GanjoorPoet> Poets
        {
            get
            {
                var poets = new List<GanjoorPoet>();
                if (Connected) {
                    using var tbl = new DataTable();
                    using var da = new SQLiteDataAdapter("SELECT id, name, cat_id, description FROM poet", _con);
                    da.Fill(tbl);
                    foreach (DataRow row in tbl.Rows)
                    {
                        poets.Add(new GanjoorPoet(Convert.ToInt32(row.ItemArray[0]), row.ItemArray[1].ToString(), Convert.ToInt32(row.ItemArray[2]), row.ItemArray[3].ToString()));
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
            if (Connected) {
                using var tbl = new DataTable();
                using var da = new SQLiteDataAdapter("SELECT poet_id, text, parent_id, url FROM cat WHERE id = " + CatID, _con);
                da.Fill(tbl);
                Debug.Assert(tbl.Rows.Count < 2);
                if (tbl.Rows.Count == 1)
                    return new GanjoorCat(
                        CatID,
                        Convert.ToInt32(tbl.Rows[0].ItemArray[0]),
                        tbl.Rows[0].ItemArray[1].ToString(),
                        Convert.ToInt32(tbl.Rows[0].ItemArray[2]),
                        tbl.Rows[0].ItemArray[3].ToString()
                    );
            }

            return null;
        }
        private static int CompareCategoriesByName(GanjoorCat cat1, GanjoorCat cat2)
        {
            return cat1._Text.CompareTo(cat2._Text);
        }
        public List<GanjoorCat> GetSubCategories(int CatID)
        {
            var lst = new List<GanjoorCat>();
            if (Connected) {
                using var tbl = new DataTable();
                using var da = new SQLiteDataAdapter("SELECT poet_id, text, url, ID FROM cat WHERE parent_id = " + CatID, _con);
                da.Fill(tbl);
                foreach (DataRow row in tbl.Rows)
                    lst.Add(new GanjoorCat(
                        Convert.ToInt32(row.ItemArray[3]),
                        Convert.ToInt32(row.ItemArray[0]),
                        row.ItemArray[1].ToString(),
                        CatID,
                        row.ItemArray[2].ToString()
                    ));
                if (CatID == 0)//home
                    lst.Sort(CompareCategoriesByName);
            }
            return lst;
        }
        public List<GanjoorCat> GetParentCategories(GanjoorCat Cat)
        {
            var lst = new List<GanjoorCat>();
            if (Connected)
            {
                while (Cat != null && Cat._ParentID != 0)
                {
                    Cat = GetCategory(Cat._ParentID);
                    lst.Insert(0, Cat);
                }
                lst.Insert(0, new GanjoorCat(0, 0, "خانه", 0, ""));
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
            var lst = new List<GanjoorPoem>();
            if (Connected) {
                using var tbl = new DataTable();
                var strQuery = String.Format(
                    "SELECT ID, title, url FROM poem WHERE cat_id = {0} ORDER BY ID"
                    , CatID);
                if (Count > 0)
                    strQuery += string.Format(" LIMIT {0}", Count);
                using var da = new SQLiteDataAdapter(
                    strQuery
                    , _con);
                da.Fill(tbl);
                foreach (DataRow row in tbl.Rows)
                {
                    var PID = Convert.ToInt32(row.ItemArray[0]);
                    lst.Add(new GanjoorPoem(
                        PID,
                        CatID,
                        row.ItemArray[1].ToString(),
                        row.ItemArray[2].ToString(),
                        IsPoemFaved(PID)
                    ));
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
            var lst = new List<GanjoorVerse>();
            if (Connected) {
                using var tbl = new DataTable();
                using var da = new SQLiteDataAdapter("SELECT vorder, position, text FROM verse WHERE poem_id = " + PoemID + " order by vorder" + (Count > 0 ? " LIMIT " + Count : ""), _con);
                da.Fill(tbl);
                foreach (DataRow row in tbl.Rows)
                    lst.Add(new GanjoorVerse(
                        PoemID,
                        Convert.ToInt32(row.ItemArray[0]),
                        (VersePosition)Convert.ToInt32(row.ItemArray[1]),
                        row.ItemArray[2].ToString()
                    ));
            }
            return lst;
        }
        public GanjoorPoem GetPoem(int PoemID)
        {
            if (Connected) {
                using var tbl = new DataTable();
                using var da = new SQLiteDataAdapter("SELECT cat_id, title, url FROM poem WHERE ID = " + PoemID, _con);
                da.Fill(tbl);
                Debug.Assert(tbl.Rows.Count < 2);
                if (1 == tbl.Rows.Count)
                    return
                        new GanjoorPoem(
                            PoemID,
                            Convert.ToInt32(tbl.Rows[0].ItemArray[0]),
                            tbl.Rows[0].ItemArray[1].ToString(),
                            tbl.Rows[0].ItemArray[2].ToString(),
                            IsPoemFaved(PoemID)
                        );
            }
            return null;
        }
        public GanjoorPoem GetNextPoem(int PoemID, int CatID)
        {
            if (Connected) {
                using var tbl = new DataTable();
                using var da = new SQLiteDataAdapter(
                    String.Format(
                        "SELECT ID FROM poem WHERE cat_id = {0} AND id>{1} LIMIT 1"
                        ,
                        CatID, PoemID)
                    , _con);
                da.Fill(tbl);
                if (1 == tbl.Rows.Count)
                    return
                        GetPoem(Convert.ToInt32(tbl.Rows[0].ItemArray[0]));
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
            if (Connected) {
                using var tbl = new DataTable();
                using var da = new SQLiteDataAdapter(
                    String.Format(
                        "SELECT ID FROM poem WHERE cat_id = {0} AND id<{1} ORDER BY ID DESC LIMIT 1"
                        ,
                        CatID, PoemID)
                    , _con);
                da.Fill(tbl);
                if (1 == tbl.Rows.Count)
                    return
                        GetPoem(Convert.ToInt32(tbl.Rows[0].ItemArray[0]));
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
                return new GanjoorPoet(0, "همه", 0, "");
            if (Connected) {
                using var tbl = new DataTable();
                using var da = new SQLiteDataAdapter("SELECT poet_id FROM cat WHERE id = " + CatID, _con);
                da.Fill(tbl);
                Debug.Assert(tbl.Rows.Count < 2);
                if (1 == tbl.Rows.Count)
                    return
                        GetPoet(Convert.ToInt32(tbl.Rows[0].ItemArray[0]));
            }
            return null;
        }
        public GanjoorPoet GetPoet(int PoetID)
        {
            if (PoetID == 0)
                return new GanjoorPoet(0, "همه", 0, "");
            if (Connected) {
                using var tbl = new DataTable();
                using var da = new SQLiteDataAdapter("SELECT id, name, cat_id, description FROM poet WHERE id = " + PoetID, _con);
                da.Fill(tbl);
                Debug.Assert(tbl.Rows.Count < 2);
                if (1 == tbl.Rows.Count)
                    return
                        new GanjoorPoet(Convert.ToInt32(tbl.Rows[0].ItemArray[0]), tbl.Rows[0].ItemArray[1].ToString(), Convert.ToInt32(tbl.Rows[0].ItemArray[2]), tbl.Rows[0].ItemArray[3].ToString());
            }
            return null;
        }
        public GanjoorPoet GetPoet(string PoetName)
        {
            if (Connected) {
                using var tbl = new DataTable();
                using var da = new SQLiteDataAdapter("SELECT id, name, cat_id, description FROM poet WHERE name = '" + PoetName + "'", _con);
                da.Fill(tbl);
                Debug.Assert(tbl.Rows.Count < 2);
                if (1 == tbl.Rows.Count)
                    return
                        new GanjoorPoet(Convert.ToInt32(tbl.Rows[0].ItemArray[0]), tbl.Rows[0].ItemArray[1].ToString(), Convert.ToInt32(tbl.Rows[0].ItemArray[2]), tbl.Rows[0].ItemArray[3].ToString());
            }
            return null;
        }
        #endregion

        #region Search
        public DataTable FindPoemsContaingPhrase(string phrase, int pageStart, int count, int poetID, int searchType, int searchLocation)
        {
            if (Connected)
            {
                var tbl = new DataTable();
                {

                    var versePosition = FindVersePosition(searchType);

                    var searchFilterLocationPattern = searchLocation == 0 ? $"%{phrase}%" : searchLocation == 1 ? $"{phrase}%" : $"%{phrase}";
                    var additionalFilterPattern = searchType == 0 ? "" : $" AND position={versePosition}";

                    var strQuery = poetID == 0
                        ? $"SELECT poem_id,vorder,position,text FROM verse WHERE text LIKE '{searchFilterLocationPattern}' {additionalFilterPattern} GROUP BY poem_id LIMIT {pageStart},{count}"
                        :
                        //کوئری جایگزین توسط آقای سیدرضی علوی زاده برنامه نویس ساغز پیشنهاد شده که از کوئری پیشین سریع تر است
                        $"SELECT poem_id,vorder,position,text FROM verse WHERE poem_id IN (SELECT id FROM poem WHERE cat_id IN (SELECT id FROM cat WHERE poet_id={poetID})) " +
                        $"AND text LIKE '{searchFilterLocationPattern}' {additionalFilterPattern} GROUP BY poem_id LIMIT {pageStart}, {count}"
                        ;
                    using var da = new SQLiteDataAdapter(strQuery, _con);
                    da.Fill(tbl);
                    return tbl;
                }
            }
            return null;
        }

        private int? FindVersePosition(int searchType)
        {
            switch (searchType)
            {
                case 1: return 0;
                case 2: return 1;
                case 3: return 2;
                case 4: return 3;
                case 5: return 4;
                case 6: return -1;  // نثر
                default: return null;
            }
        }

        public DataTable FindFirstVerseContaingPhrase(int PoemID, string phrase, int searchType)
        {
            if (Connected)
            {
                var tbl = new DataTable();
                {
                    var searchFilterPattern = searchType == 0 ? $"%{phrase}%" : $"{phrase}%";
                    var additionalFilterPattern = searchType == 0 ? "" : " AND position=0";

                    using var da = new SQLiteDataAdapter("SELECT text FROM verse WHERE poem_id=" + PoemID + " AND text LIKE'%" + phrase + "%' LIMIT 0,1", _con);
                    da.Fill(tbl);
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
                if (Connected) {
                    using var tbl = new DataTable();
                    using var da = new SQLiteDataAdapter("SELECT MAX(pos) FROM fav", _con);
                    da.Fill(tbl);
                    if (tbl.Rows.Count != 0)
                        if (tbl.Rows[0].ItemArray[0] is DBNull)
                            return -1;
                        else
                            return Convert.ToInt32(tbl.Rows[0].ItemArray[0]);
                    return -1;
                }
                return -1;
            }
        }
        public bool IsPoemFaved(int PoemID)
        {
            if (Connected) {
                using var tbl = new DataTable();
                using var da = new SQLiteDataAdapter("SELECT pos FROM fav WHERE poem_id = " + PoemID, _con);
                da.Fill(tbl);
                return tbl.Rows.Count != 0;
            }
            return false;
        }
        public bool IsVerseFaved(int PoemID, int VerseID)
        {
            if (Connected) {
                using var tbl = new DataTable();
                using var da = new SQLiteDataAdapter("SELECT pos FROM fav WHERE poem_id = " + PoemID + " AND verse_id=" + VerseID, _con);
                da.Fill(tbl);
                return tbl.Rows.Count != 0;
            }
            return false;
        }
        public void Fav(int PoemID)
        {
            Fav(PoemID, -1);
        }
        public void Fav(int PoemID, int VerseID)
        {
            if (Connected) {
                using var cmd = new SQLiteCommand(_con);
                cmd.CommandText = "INSERT INTO fav (poem_id, pos, verse_id) VALUES (" + PoemID + "," + (MaxFavOrder + 1) + "," + VerseID + ");";
                cmd.ExecuteNonQuery();
            }
        }
        public void UnFav(int PoemID)
        {
            UnFav(PoemID, -1);
        }
        public void UnFav(int PoemID, int VerseID)
        {
            if (Connected) {
                using var cmd = new SQLiteCommand(_con);
                if (VerseID != -1)
                    cmd.CommandText = "DELETE FROM fav WHERE poem_id=" + PoemID + " AND verse_id=" + VerseID;
                else
                    cmd.CommandText = "DELETE FROM fav WHERE poem_id=" + PoemID;
                cmd.ExecuteNonQuery();
            }
        }
        public bool ToggleFav(int PoemID, int VerseID)
        {
            var faved = VerseID == -1 ? IsPoemFaved(PoemID) : IsVerseFaved(PoemID, VerseID);
            if (faved)
            {
                UnFav(PoemID, VerseID);
                return false;
            }

            Fav(PoemID, VerseID);
            return true;
        }
        public DataTable GetFavs(int PageStart, int Count)
        {
            if (Connected)
            {
                var tbl = new DataTable();
                {
                    using var da = new SQLiteDataAdapter("SELECT poem_id FROM fav GROUP BY poem_id ORDER BY pos DESC LIMIT " + PageStart + "," + Count, _con);
                    da.Fill(tbl);
                    return tbl;
                }
            }
            return null;
        }
        public GanjoorVerse GetPreferablyAFavVerse(int PoemID)
        {
            var allVerses = GetVerses(PoemID);

            for (var v = 0; v < allVerses.Count; v++)
            {
                var verse = allVerses[v];
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
        public int GetRandomPoem(List<int> CatIDs) {
            if (CatIDs.Count == 0)
                return GetRandomPoem(0);
            return GetRandomPoem(CatIDs[rnd.Next(CatIDs.Count)]);
        }
        public int GetRandomPoem(int CatID)
        {
            if (_randomCatID != CatID)
            {
                if (Connected) {
                    using var tbl = new DataTable();
                    var strQuery = "SELECT MIN(id), MAX(id) FROM poem";
                    if (CatID != 0)
                        strQuery += " WHERE cat_id=" + CatID;
                    using var da = new SQLiteDataAdapter(strQuery, _con);
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
            if (_randomCatIDMinPoem != -1 && _randomCatIDMaxPoem != -1)
                return rnd.Next(_randomCatIDMinPoem, _randomCatIDMaxPoem);
            return 0;
        }
        private int _randomCatID = -1;
        private int _randomCatIDMinPoem = -1;
        private int _randomCatIDMaxPoem = -1;
        private Random rnd = new();
        #endregion

        #region Versioning
        private const int DatabaseVersion = 3;
        private void UpgradeOldDbs() {
            using var tbl = _con.GetSchema("Tables");

            #region fav table
            var favTable = tbl.Select("Table_Name='fav'");

            if (favTable.Length == 0) {
                using var cmd = new SQLiteCommand(_con);
                cmd.CommandText = "CREATE TABLE fav (poem_id INTEGER, verse_id INTEGER, pos INTEGER);";
                cmd.ExecuteNonQuery();
            }
            else
            {
                try {
                    using var cmd = new SQLiteCommand(_con);
                    cmd.CommandText = "SELECT verse_id FROM fav LIMIT 0,1";
                    cmd.ExecuteNonQuery();
                }
                catch (Exception exp)
                {
                    if (exp.Message.IndexOf("verse_id") != -1)
                    {

                        using (var cmd = new SQLiteCommand(_con))
                        {
                            cmd.CommandText = "CREATE TABLE favcpy (poem_id INTEGER, verse_id INTEGER, pos INTEGER);";
                            cmd.ExecuteNonQuery();
                        }
                        using (var fav = new DataTable())
                        {
                            using (var favAd = new SQLiteDataAdapter("SELECT poem_id, pos FROM fav", _con))
                            {
                                favAd.Fill(fav);
                                foreach (DataRow row in fav.Rows) {
                                    using var cmd = new SQLiteCommand(_con);
                                    cmd.CommandText = "INSERT INTO favcpy (poem_id, verse_id, pos) VALUES (" + row.ItemArray[0] + ", -1, " + row.ItemArray[1] + ")";
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        using (var cmd = new SQLiteCommand(_con))
                        {
                            cmd.CommandText = "DROP TABLE fav";
                            cmd.ExecuteNonQuery();
                        }
                        using (var cmd = new SQLiteCommand(_con))
                        {
                            cmd.CommandText = "CREATE TABLE fav (poem_id INTEGER, verse_id INTEGER, pos INTEGER);";
                            cmd.ExecuteNonQuery();
                        }

                        using (var fav = new DataTable())
                        {
                            using (var favAd = new SQLiteDataAdapter("SELECT poem_id, pos, verse_id FROM favcpy", _con))
                            {
                                favAd.Fill(fav);
                                foreach (DataRow row in fav.Rows) {
                                    using var cmd = new SQLiteCommand(_con);
                                    cmd.CommandText = "INSERT INTO fav (poem_id, verse_id, pos) VALUES (" + row.ItemArray[0] + ", -1, " + row.ItemArray[1] + ")";
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        using (var cmd = new SQLiteCommand(_con))
                        {
                            cmd.CommandText = "DROP TABLE favcpy";
                            cmd.ExecuteNonQuery();
                        }

                    }
                }

            }
            #endregion
            #region version table
            var verTable = tbl.Select("Table_Name='gver'");

            var vg3db = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "vg.s3db");
            if (verTable.Length == 0 && !File.Exists(vg3db)) {
                using var cmd = new SQLiteCommand(_con);
                cmd.CommandText = "CREATE TABLE gver (curver INTEGER);";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO gver (curver) VALUES (" + DatabaseVersion + ");";
                cmd.ExecuteNonQuery();
            }
            else
            {
                if (verTable.Length == 0)
                {

                    //Version table does not exist, so our verse table
                    //position field values are incorrect,
                    //correct it using vg.s3db file and then create version table

                    var conString = new SQLiteConnectionStringBuilder {
                        DataSource = "vg.s3db",
                        DefaultTimeout = 5000,
                        FailIfMissing = true,
                        ReadOnly = true
                    };
                    using var vgCon = new SQLiteConnection(conString.ConnectionString);
                    vgCon.Open();
                    BeginBatchOperation();
                    using (var cmd = new SQLiteCommand(_con))
                    {
                        using (var da = new SQLiteDataAdapter("SELECT poem_id, vorder, position FROM verse", vgCon))
                        {
                            using (var tblVerse = new DataTable())
                            {
                                da.Fill(tblVerse);
                                var poemID = 0;
                                var vOrder = 1;
                                var position = 2;
                                foreach (DataRow row in tblVerse.Rows)
                                {
                                    cmd.CommandText = "UPDATE verse SET position=" + row.ItemArray[position] + " WHERE poem_id = " + row.ItemArray[poemID] + " AND vorder=" + row.ItemArray[vOrder] + ";";
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        cmd.CommandText = "CREATE TABLE gver (curver INTEGER);";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "INSERT INTO gver (curver) VALUES (" + DatabaseVersion + ");";
                        cmd.ExecuteNonQuery();
                    }
                    CommitBatchOperation();
                }
                File.Delete(vg3db);
            }
            #endregion
            #region new poets
            var newdb = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "new.s3db");
            if (File.Exists(newdb))
            {
                ImportDb(newdb);
                File.Delete(newdb);
            }
            #endregion
            #region gdb ignore list
            var gilTable = tbl.Select("Table_Name='gil'");

            if (gilTable.Length == 0) {
                using var cmd = new SQLiteCommand(_con);
                cmd.CommandText = "CREATE TABLE gil (cat_id INTEGER);";
                cmd.ExecuteNonQuery();
            }
            #endregion
            #region poet description field
            using (var poetTableChecker = new SQLiteDataAdapter("PRAGMA table_info('poet')", _con))
            {
                using (var poetTableInfo = new DataTable())
                {
                    poetTableChecker.Fill(poetTableInfo);
                    if (poetTableInfo.Rows.Count == 3)//old poet table
                    {
                        using (var cmd = new SQLiteCommand(_con))
                        {
                            cmd.CommandText = "ALTER TABLE poet ADD description TEXT;";
                            cmd.ExecuteNonQuery();
                        }
                        try {
                            using var cmd = new SQLiteCommand(_con);
                            cmd.CommandText = "DELETE FROM gver";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "INSERT INTO gver (curver) VALUES (" + DatabaseVersion + ");";//update database version information
                            cmd.ExecuteNonQuery();
                        }
                        catch
                        {
                            //this is normal, gdbs do not have gver table
                        }
                    }
                }
            }
            if (Path.GetFileNameWithoutExtension(_dbfilepath).ToLower() != "poetinfo")
            {
                var poetInfoDb = Path.Combine(Path.GetDirectoryName(_dbfilepath), "poetinfo.s3db");
                if (File.Exists(poetInfoDb))
                {
                    var poetInfo = new DbBrowser(poetInfoDb);
                    BeginBatchOperation();
                    foreach (var imPoet in poetInfo.Poets)
                    {
                        var correspondingPoet = GetPoet(imPoet._ID);
                        if (correspondingPoet != null)
                            ModifyPoetBio(correspondingPoet._ID, imPoet._Bio);
                    }
                    CommitBatchOperation();
                    poetInfo.CloseDb();
                    File.Delete(poetInfoDb);
                }
            }
            #endregion
            #region poemsnd (2.61+)
            var poemsndTable = tbl.Select("Table_Name='poemsnd'");
            if (poemsndTable.Length == 0)
            {
                using (var cmd = new SQLiteCommand(_con))
                {
                    cmd.CommandText = "CREATE TABLE [poemsnd] ([poem_id] INTEGER NOT NULL, [id] INTEGER NOT NULL, [filepath] TEXT, [description] TEXT, " +
                                      "[dnldurl] TEXT, [isdirect] INTEGER, [syncguid] TEXT, [fchksum] TEXT, isuploaded INTEGER" +
                                      ");";
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SQLiteCommand(_con))
                {
                    cmd.CommandText = "CREATE TABLE [sndsync] ([poem_id] INTEGER NOT NULL, [snd_id] INTEGER NOT NULL, [verse_order] INTEGER NOT NULL, [milisec] INTEGER NOT NULL);";
                    cmd.ExecuteNonQuery();
                }

                try {
                    using var cmd = new SQLiteCommand(_con);
                    cmd.CommandText = "DELETE FROM gver";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO gver (curver) VALUES (" + DatabaseVersion + ");";//update database version information
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    //this is normal, gdbs do not have gver table
                }

            }
            else {
                //upgrade older than 2.72 table structure
                using var sndTableChecker = new SQLiteDataAdapter("PRAGMA table_info('poemsnd')", _con);
                using var sndTableInfo = new DataTable();
                sndTableChecker.Fill(sndTableInfo);
                if (sndTableInfo.Rows.Count == 4)//old poemsnd table
                {
                    using (var cmd = new SQLiteCommand(_con))
                    {
                        cmd.CommandText = "ALTER TABLE poemsnd ADD dnldurl TEXT;";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE poemsnd ADD isdirect INTEGER;";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE poemsnd ADD syncguid TEXT;";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE poemsnd ADD fchksum TEXT;";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE poemsnd ADD isuploaded INTEGER;";
                        cmd.ExecuteNonQuery();

                    }

                    //update existing data:
                    using (var tblPoemSnd = new DataTable())
                    {
                        var strQuery = "SELECT poem_id, id, filepath FROM poemsnd";
                        using (var da = new SQLiteDataAdapter(strQuery, _con))
                        {
                            da.Fill(tblPoemSnd);
                            var nIdxPoemId = 0;
                            var nIdxId = 1;
                            var nIdxFilePath = 2;
                            BeginBatchOperation();
                            foreach (DataRow row in tblPoemSnd.Rows)
                            {
                                if (row.ItemArray[nIdxFilePath] != null)
                                {
                                    strQuery = String.Format("update poemsnd SET dnldurl = '', isdirect = 0, syncguid = '{0}', fchksum = '{1}', isuploaded = 0 " +
                                                             "WHERE poem_id = {2} AND id = {3}",
                                        Guid.NewGuid(), PoemAudio.ComputeCheckSum(row.ItemArray[nIdxFilePath].ToString()),
                                        row.ItemArray[nIdxPoemId], row.ItemArray[nIdxId]
                                    );
                                    using var cmd = new SQLiteCommand(_con);
                                    cmd.CommandText = strQuery;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            CommitBatchOperation();
                        }
                    }

                    try {
                        using var cmd = new SQLiteCommand(_con);
                        cmd.CommandText = "DELETE FROM gver";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "INSERT INTO gver (curver) VALUES (" + DatabaseVersion + ");";//update database version information
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        //this is normal, gdbs do not have gver table
                    }


                }
            }
            #endregion
        }
        #endregion

        #region Import Db
        /// <summary>
        /// TODO: This methods needs more checks (for id conflicts) and its performance can significantly
        ///       be improved using a temporary database.
        /// </summary>
        public bool ImportDb(string fileName)
        {
            try
            {
                //this causes openning db to be upgraded
                var dbUpgrader = new DbBrowser(fileName);
                dbUpgrader.CloseDb();
            }
            catch
                (Exception exp)
            {
                LastError = exp.ToString();
                return false;
            }
            SQLiteConnection newConnection;
            try
            {
                var conString = new SQLiteConnectionStringBuilder {
                    DataSource = fileName,
                    DefaultTimeout = 5000,
                    FailIfMissing = true,
                    ReadOnly = false
                };
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
                var dicPoets = new Dictionary<int, int>();
                var dicCats = new Dictionary<int, int>();
                BeginBatchOperation();
                using var cmd = new SQLiteCommand(_con);
                using (var tbl = new DataTable())
                using (var da = new SQLiteDataAdapter("SELECT id, name, cat_id, description FROM poet", newConnection))
                {
                    da.Fill(tbl);
                    foreach (DataRow row in tbl.Rows)
                    {
                        var insertNewPoet = true;
                        var NewPoetID = Convert.ToInt32(row.ItemArray[0]);
                        var NewPoetName = row.ItemArray[1].ToString();
                        var NewCatID = Convert.ToInt32(row.ItemArray[2]);
                        var NewPoetBio = row.ItemArray[3].ToString();
                        var poet = GetPoet(NewPoetName);
                        if (poet != null)
                        {
                            if (poet._ID == NewPoetID)
                            {
                                insertNewPoet = false;
                                dicPoets.Add(NewPoetID, NewPoetID);
                                var NewPoetCat = GetCategory(NewCatID);
                                if (NewPoetCat != null)
                                {
                                    if (NewPoetCat._PoetID == NewPoetID)
                                    {
                                        dicCats.Add(NewCatID, NewCatID);
                                    }
                                    else
                                    {
                                        var RealyNewCatID = GenerateNewCatID();
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
                                var RealyNewPoetID = GenerateNewPoetID();
                                dicPoets.Add(NewPoetID, RealyNewPoetID);
                                NewPoetID = RealyNewPoetID;

                                var RealyNewCatID = GenerateNewCatID();
                                dicCats.Add(NewCatID, RealyNewCatID);
                                NewCatID = RealyNewCatID;
                            }
                            else //no conflict, insertNew
                            {
                                dicPoets.Add(NewPoetID, NewPoetID);
                                var NewPoetCat = GetCategory(NewCatID);
                                if (NewPoetCat == null)
                                    dicCats.Add(NewCatID, NewCatID);
                                else
                                {
                                    var RealyNewCatID = GenerateNewCatID();
                                    dicCats.Add(NewCatID, GenerateNewCatID());
                                    NewCatID = RealyNewCatID;
                                }
                            }
                        }

                        if (insertNewPoet)
                        {
                            cmd.CommandText = String.Format(
                                "INSERT INTO poet (id, name, cat_id, description) VALUES ({0}, \"{1}\", {2}, \"{3}\");",
                                NewPoetID, NewPoetName, NewCatID, NewPoetBio
                            );
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                using (var tbl = new DataTable())
                using (var da = new SQLiteDataAdapter("SELECT id, poet_id, text, parent_id, url FROM cat", newConnection))
                {
                    da.Fill(tbl);
                    foreach (DataRow row in tbl.Rows)
                    {
                        var PoetID = Convert.ToInt32(row.ItemArray[1]);
                        int MappedPoetID;
                        if (dicPoets.TryGetValue(PoetID, out MappedPoetID))
                            PoetID = MappedPoetID;
                        var NewCatID = Convert.ToInt32(row.ItemArray[0]);
                        var NewCatParentID = Convert.ToInt32(row.ItemArray[3]);
                        int MappedCatParentID;
                        if (dicCats.TryGetValue(NewCatParentID, out MappedCatParentID))
                        {
                            NewCatParentID = MappedCatParentID;
                        }
                        var insertNewCategory = true;
                        var NewCat = GetCategory(NewCatID);
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
                                var RealyNewCatID = GenerateNewCatID();
                                dicCats.Add(NewCatID, RealyNewCatID);
                                NewCatID = RealyNewCatID;
                            }
                        }
                        else
                        {
                            int tmp;
                            if (!dicCats.TryGetValue(NewCatID, out tmp))
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

                        if (GetPoet(PoetID) == null)
                        {
                            //missing poet
                            int poetCat;
                            if (NewCatParentID == 0)
                                poetCat = NewCatID;
                            else
                            {
                                //this is not good:
                                poetCat = NewCatParentID;
                            }
                            NewPoet("شاعر " + PoetID, PoetID, poetCat);
                        }

                    }
                }

                var dicPoemID = new Dictionary<int, int>();
                using (var tbl = new DataTable())
                using (var da = new SQLiteDataAdapter("SELECT id, cat_id, title, url FROM poem", newConnection))
                {
                    da.Fill(tbl);
                    foreach (DataRow row in tbl.Rows)
                    {
                        var poemID = Convert.ToInt32(row.ItemArray[0]);
                        if (GetPoem(poemID) != null)
                            poemID = GenerateNewPoemID();
                        var NewCat = dicCats[Convert.ToInt32(row.ItemArray[1])];
                        dicPoemID.Add(Convert.ToInt32(row.ItemArray[0]), poemID);
                        cmd.CommandText = String.Format(
                            "INSERT INTO poem (id, cat_id, title, url) VALUES ({0}, {1}, \"{2}\", \"{3}\");",
                            poemID, NewCat, row.ItemArray[2], row.ItemArray[3]
                        );
                        cmd.ExecuteNonQuery();
                    }
                }

                using (var tbl = new DataTable())
                using (var da = new SQLiteDataAdapter("SELECT poem_id, vorder, position, text FROM verse", newConnection))
                {
                    da.Fill(tbl);
                    foreach (DataRow row in tbl.Rows)
                    {
                        var PoemID = dicPoemID[Convert.ToInt32(row.ItemArray[0])];
                        cmd.CommandText = String.Format(
                            "INSERT INTO verse (poem_id, vorder, position, text) VALUES ({0}, {1}, {2}, \"{3}\");",
                            PoemID, row.ItemArray[1], row.ItemArray[2], row.ItemArray[3].ToString().Replace("\"", "\"\"")
                        );
                        cmd.ExecuteNonQuery();
                    }
                }

                CommitBatchOperation();
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
        public GanjoorPoet[] GetDbPoets(string fileName)
        {
            var newConnection = OpenConnectionForDb(fileName);
            var dbPoets = new List<GanjoorPoet>();
            if (newConnection != null)
            {
                using (var tbl = new DataTable())
                using (var da = new SQLiteDataAdapter("SELECT id, name, cat_id, description FROM poet", newConnection))
                {
                    da.Fill(tbl);
                    foreach (DataRow row in tbl.Rows)
                    {
                        dbPoets.Add(new GanjoorPoet(Convert.ToInt32(row[0]), row[1].ToString(), Convert.ToInt32(row[2]), row.ItemArray[3].ToString()));
                    }
                }

                newConnection.Dispose();
            }

            return dbPoets.ToArray();

        }

        private static SQLiteConnection OpenConnectionForDb(string fileName)
        {
            SQLiteConnection newConnection = null;
            try
            {
                var dbUpgrader = new DbBrowser(fileName);
                dbUpgrader.CloseDb();
            }
            catch
            {
            }
            try
            {
                var conString = new SQLiteConnectionStringBuilder {
                    DataSource = fileName,
                    DefaultTimeout = 5000,
                    FailIfMissing = true,
                    ReadOnly = false
                };
                newConnection = new SQLiteConnection(conString.ConnectionString);
                newConnection.Open();
            }
            catch
            {
                return null;
            }
            return newConnection;
        }
        public GanjoorPoet[] GetConflictingPoets(string fileName)
        {
            var conficts = new List<GanjoorPoet>();
            if (Connected)
            {
                var dbPoets = GetDbPoets(fileName);
                foreach (var MyPoet in Poets)
                    foreach (var dbPoet in dbPoets)
                        if (MyPoet.Equals(dbPoet))
                        {
                            conficts.Add(MyPoet);
                        }

            }
            return conficts.ToArray();
        }
        public GanjoorCat[] GetDbCats(string fileName)
        {
            var newConnection = OpenConnectionForDb(fileName);

            var dbCats = new List<GanjoorCat>();
            if (newConnection != null)
            {
                using (var tbl = new DataTable())
                using (var da = new SQLiteDataAdapter("SELECT id, poet_id, text, parent_id, url FROM cat", newConnection))
                {
                    da.Fill(tbl);
                    foreach (DataRow row in tbl.Rows)
                    {
                        dbCats.Add(new GanjoorCat(Convert.ToInt32(row[0]), Convert.ToInt32(row[1]), row[2].ToString(), Convert.ToInt32(row[3]), row[4].ToString()));
                    }
                }

                newConnection.Dispose();
            }


            return dbCats.ToArray();

        }
        public GanjoorCat[] GetCategoriesWithMissingPoet(string fileName)
        {
            var missingPoetCats = new List<GanjoorCat>();
            if (Connected)
            {
                var cats = GetDbCats(fileName);
                var dbPoets = GetDbPoets(fileName);
                foreach (var cat in cats)
                {
                    var PoetFoundInTheDb = false;
                    foreach (var dbPoet in dbPoets)
                        if (cat._PoetID == dbPoet._ID)
                        {
                            PoetFoundInTheDb = true;
                            break;
                        }
                    if (!PoetFoundInTheDb)
                    {
                        var PoetFound = false;
                        foreach (var MyPoet in Poets)
                            if (cat._PoetID == MyPoet._ID)
                            {
                                PoetFound = true;
                                break;
                            }
                        if (!PoetFound)
                            missingPoetCats.Add(cat);
                    }
                }
            }
            return missingPoetCats.ToArray();
        }
        public GanjoorCat[] GetConflictingCats(string fileName)
        {
            var conficts = new List<GanjoorCat>();
            if (Connected)
            {
                var cats = GetDbCats(fileName);
                foreach (var cat in cats)
                    if (GetCategory(cat._ID) != null)
                        conficts.Add(cat);
            }
            return conficts.ToArray();
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
                var conString = new SQLiteConnectionStringBuilder {
                    DataSource = fileName,
                    DefaultTimeout = 5000,
                    FailIfMissing = true,
                    ReadOnly = false
                };
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
                BeginBatchOperation();
                using (var cmd = new SQLiteCommand(_con))
                {

                    using (var tbl = new DataTable())
                    using (var da = new SQLiteDataAdapter("SELECT id, poet_id, text, parent_id, url FROM cat", newConnection))
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

                    using (var tbl = new DataTable())
                    using (var da = new SQLiteDataAdapter("SELECT id, name, cat_id, description FROM poet", newConnection))
                    {
                        da.Fill(tbl);
                        foreach (DataRow row in tbl.Rows)
                        {
                            cmd.CommandText = String.Format(
                                "INSERT INTO poet (id, name, cat_id, description) VALUES ({0}, \"{1}\", {2}, \"{3}\");",
                                row.ItemArray[0], row.ItemArray[1], row.ItemArray[2], row.ItemArray[3]
                                );
                            cmd.ExecuteNonQuery();
                        }
                    }

                    using (var tbl = new DataTable())
                    using (var da = new SQLiteDataAdapter("SELECT id, cat_id, title, url FROM poem", newConnection))
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

                    using (var tbl = new DataTable())
                    using (var da = new SQLiteDataAdapter("SELECT poem_id, vorder, position, text FROM verse", newConnection))
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


                }
                CommitBatchOperation();
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

        /// <summary>
        /// a special utility for importing poet bio texts
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool ImportDbPoetBioText(string fileName)
        {

            try
            {
                var dbSrc = new DbBrowser(fileName);

                var srcPoets = dbSrc.Poets;

                foreach (var targetPoet in Poets)
                {
                    foreach (var srcPoet in srcPoets)
                    {
                        if (srcPoet._ID == targetPoet._ID)
                        {
                            if (!ModifyPoetBio(targetPoet._ID, srcPoet._Bio))
                            {
                                return false;
                            }
                            break;
                        }
                    }
                }

                dbSrc.CloseDb();

                return true;
            }
            catch
                (Exception exp)
            {
                LastError = exp.ToString();
                return false;
            }
        }
        #endregion

        #region Import/Export Favs
        public bool ExportFavs(string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
            var conString = new SQLiteConnectionStringBuilder {
                DataSource = fileName,
                DefaultTimeout = 5000,
                FailIfMissing = false,
                ReadOnly = false
            };
            using var connection = new SQLiteConnection(conString.ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "CREATE TABLE fav (poem_id INTEGER, verse_id INTEGER, pos INTEGER);";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "BEGIN TRANSACTION;";
            cmd.ExecuteNonQuery();


            using (var tbl = new DataTable())
            using (var da = new SQLiteDataAdapter("SELECT poem_id, verse_id, pos FROM fav", _con))
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

            return true;
        }
        public int ImportMixFavs(string fileName, out int dupFavs, out int errFavs)
        {
            var ImportedFavs = 0;
            dupFavs = errFavs = 0;
            var conString = new SQLiteConnectionStringBuilder {
                DataSource = fileName,
                DefaultTimeout = 5000,
                FailIfMissing = true,
                ReadOnly = true
            };
            using var connection = new SQLiteConnection(conString.ConnectionString);
            connection.Open();

            BeginBatchOperation();
            using (var cmd = new SQLiteCommand(_con))
            {



                using (var tbl = new DataTable())
                using (var da = new SQLiteDataAdapter("SELECT poem_id, verse_id, pos FROM fav", connection))
                {
                    da.Fill(tbl);
                    foreach (DataRow row in tbl.Rows)
                    {
                        var poemID = Convert.ToInt32(row.ItemArray[0]);
                        var verseID = Convert.ToInt32(row.ItemArray[1]);
                        var pos = Convert.ToInt32(row.ItemArray[2]);
                        if (GetPoem(poemID) != null)//new check 2011/05/05
                        {
                            if (!IsVerseFaved(poemID, verseID))
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
                        else
                            errFavs++;
                    }
                }

            }

            CommitBatchOperation();

            return ImportedFavs;
        }
        #endregion

        #region Editing Options
        private int CachedMaxCatID;
        private int CachedMaxPoemID;
        /// <returns>PoetID</returns>
        public int NewPoet(string PoetName)
        {
            return NewPoet(PoetName, -1, -1);
        }
        public int NewPoet(string PoetName, int NewPoetID, int NewPoetCatID)
        {
            if (!Connected)
                return -1;
            foreach (var Poet in Poets)
                if (Poet._Name == PoetName)
                    return -1;//نام تکراری
            if (NewPoetID == -1 || GetPoet(NewPoetID) != null)
                NewPoetID = GenerateNewPoetID();

            var poetCat = CreateNewCategory(PoetName, 0, NewPoetID, NewPoetCatID);
            if (poetCat == null)
                return -1;
            using var cmd = new SQLiteCommand(_con);
            cmd.CommandText = String.Format(
                "INSERT INTO poet (id, name, cat_id, description) VALUES ({0}, \"{1}\", {2}, \"{3}\");",
                NewPoetID, PoetName, poetCat._ID, ""
            );
            cmd.ExecuteNonQuery();
            return NewPoetID;
        }

        private int GenerateNewPoetID()
        {
            int NewPoetID;
            using var tbl = new DataTable();
            using var da = new SQLiteDataAdapter("SELECT MAX(id) FROM poet", _con);
            da.Fill(tbl);
            if (tbl.Rows.Count == 1)
            {
                try
                {
                    NewPoetID = Convert.ToInt32(tbl.Rows[0].ItemArray[0]);
                }
                catch
                {
                    NewPoetID = _MinNewPoetID;
                }
                if (NewPoetID < _MinNewPoetID)
                    NewPoetID = _MinNewPoetID;
                else
                    NewPoetID++;
            }
            else
                NewPoetID = _MinNewPoetID;

            return NewPoetID;
        }
        public bool SetPoetName(int PoetID, string NewName)
        {
            if (!Connected)
                return false;
            foreach (var Poet in Poets)
                if (Poet._ID != PoetID && Poet._Name == NewName)
                    return false;//نام تکراری
            var poet = GetPoet(PoetID);
            NewName = NewName.Replace("\"", "\"\"");
            using var cmd = new SQLiteCommand(_con);
            cmd.CommandText = String.Format(
                "UPDATE poet SET name = \"{0}\" WHERE id=" + poet._ID,
                NewName
            );
            cmd.ExecuteNonQuery();
            cmd.CommandText = String.Format(
                "UPDATE cat SET text = \"{0}\" WHERE id=" + poet._CatID,
                NewName
            );
            cmd.ExecuteNonQuery();
            return true;
        }
        public bool ModifyPoetBio(int PoetID, string Bio)
        {
            if (!Connected)
                return false;
            var poet = GetPoet(PoetID);
            Bio = Bio.Replace("\"", "\"\"");
            using var cmd = new SQLiteCommand(_con);
            cmd.CommandText = String.Format(
                "UPDATE poet SET description = \"{0}\" WHERE id=" + poet._ID,
                Bio
            );
            cmd.ExecuteNonQuery();
            return true;
        }
        public GanjoorCat CreateNewCategory(string CategoryName, int ParentCatID, int PoetID)
        {
            return CreateNewCategory(CategoryName, ParentCatID, PoetID, -1);
        }
        public GanjoorCat CreateNewCategory(string CategoryName, int ParentCatID, int PoetID, int NewCatID)
        {
            using (var tbl = new DataTable())
            {
                using (var da = new SQLiteDataAdapter(String.Format("SELECT * FROM cat WHERE parent_id={0} AND text LIKE \"{1}\"", ParentCatID, CategoryName), _con))
                {
                    da.Fill(tbl);
                    if (tbl.Rows.Count > 0)
                        return null;
                }
            }
            if (NewCatID == -1 || GetCategory(NewCatID) != null)
                NewCatID = GenerateNewCatID();

            using (var cmd = new SQLiteCommand(_con))
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
            using var tbl = new DataTable();
            if (CachedMaxCatID == 0) {
                using var da = new SQLiteDataAdapter("SELECT MAX(id) FROM cat", _con);
                da.Fill(tbl);
                if (tbl.Rows.Count == 1)
                {
                    try
                    {
                        NewCatID = Convert.ToInt32(tbl.Rows[0].ItemArray[0]);
                    }
                    catch
                    {
                        NewCatID = _MinNewCatID;
                    }
                    if (NewCatID < _MinNewCatID)
                        NewCatID = _MinNewCatID;
                    else
                        NewCatID++;
                }
                else
                    NewCatID = _MinNewCatID;
                CachedMaxCatID = NewCatID;
            }
            else
            {
                NewCatID = CachedMaxCatID + 1;
                CachedMaxCatID++;
            }

            return NewCatID;
        }
        public bool SetCatTitle(int CatID, string NewTitle)
        {
            if (!Connected)
                return false;
            var cat = GetCategory(CatID);
            if (null == cat)
                return false;
            if (cat._ParentID == 0)
                return false;
            using (var tbl = new DataTable())
            {
                using (var da = new SQLiteDataAdapter(String.Format("SELECT * FROM cat WHERE id<>{0} AND parent_id={1} AND text LIKE \"{2}\"", CatID, cat._ParentID, NewTitle), _con))
                {
                    da.Fill(tbl);
                    if (tbl.Rows.Count > 0)
                        return false;
                }
            }
            NewTitle = NewTitle.Replace("\"", "\"\"");
            using (var cmd = new SQLiteCommand(_con))
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
            using var cmd = new SQLiteCommand(_con);
            cmd.CommandText = String.Format(
                "UPDATE cat SET id = {0} WHERE id= {1}",
                NewCatID, CatID
            );
            cmd.ExecuteNonQuery();
            cmd.CommandText = String.Format(
                "UPDATE cat SET parent_id = {0} WHERE parent_id= {1}",
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
            return true;
        }
        public bool SetCatParentID(int CatID, int ParentCatID)
        {
            if (!Connected)
                return false;
            using var cmd = new SQLiteCommand(_con);
            cmd.CommandText = String.Format(
                "UPDATE cat SET parent_id = {0} WHERE id= {1}",
                ParentCatID, CatID
            );
            cmd.ExecuteNonQuery();
            return true;
        }
        public GanjoorPoem CreateNewPoem(string PoemTitle, int CatID)
        {

            var NewPoemID = GenerateNewPoemID();
            using var cmd = new SQLiteCommand(_con);
            cmd.CommandText = String.Format(
                "INSERT INTO poem (id, cat_id, title, url) VALUES ({0}, {1}, \"{2}\", \"{3}\");",
                NewPoemID, CatID, PoemTitle, ""
            );
            cmd.ExecuteNonQuery();

            return GetPoem(NewPoemID);
        }

        private int GenerateNewPoemID()
        {
            int NewPoemID;
            if (CachedMaxPoemID == 0) {
                using var tbl = new DataTable();
                using var da = new SQLiteDataAdapter("SELECT MAX(id) FROM poem", _con);
                da.Fill(tbl);
                if (tbl.Rows.Count == 1)
                {
                    try
                    {
                        NewPoemID = Convert.ToInt32(tbl.Rows[0].ItemArray[0]) + 1;
                    }
                    catch
                    {
                        NewPoemID = _MinNewPoemID;
                    }
                    if (NewPoemID < _MinNewPoemID)
                        NewPoemID = _MinNewPoemID;
                }
                else
                    NewPoemID = _MinNewPoemID;
                CachedMaxPoemID = NewPoemID;
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
            var poem = GetPoem(PoemID);
            if (null == poem)
                return false;
            NewTitle = NewTitle.Replace("\"", "\"\"");
            using var cmd = new SQLiteCommand(_con);
            cmd.CommandText = String.Format(
                "UPDATE poem SET title = \"{0}\" WHERE id=" + PoemID,
                NewTitle
            );
            cmd.ExecuteNonQuery();
            return true;
        }
        public bool ChangePoemCategory(int PoemID, int CatID)
        {
            if (!Connected)
                return false;
            var poem = GetPoem(PoemID);
            if (null == poem)
                return false;
            using var cmd = new SQLiteCommand(_con);
            cmd.CommandText = String.Format(
                "UPDATE poem SET cat_id = {0} WHERE id=" + PoemID,
                CatID
            );
            cmd.ExecuteNonQuery();
            return true;
        }
        public bool DeletePoem(int PoemID)
        {
            if (!Connected)
                return false;
            var poem = GetPoem(PoemID);
            if (null == poem)
                return false;
            using var cmd = new SQLiteCommand(_con);
            cmd.CommandText = String.Format(
                "DELETE FROM verse WHERE poem_id={0}",
                PoemID
            );
            cmd.ExecuteNonQuery();

            cmd.CommandText = String.Format(
                "DELETE FROM poem WHERE id={0}",
                PoemID
            );
            cmd.ExecuteNonQuery();
            return true;
        }
        /// <summary>
        /// Insert a new verse into a poem,
        /// the new verse will have no TEXT, use SetVerseText to fill its TEXT
        /// </summary>
        /// <param name="PoemID">corresponding poem id</param>
        /// <param name="beforeVerseOrder">
        /// VORDER field value in VERSE table for the verse you want to insert this new verse after it,
        /// VORDERs are started from 1 (normaly), order for new verse becomes beforeVerseOrder+1, so
        /// if this is the first versse of a poem set this parameter to 0, for inserting a verse
        /// after first verse set it to 1 and ...
        /// VORDERs for next verses are updated automatically in this function
        /// </param>
        public GanjoorVerse CreateNewVerse(int PoemID, int beforeVerseOrder, VersePosition Position)
        {
            if (!Connected)
                return null;
            var updateCommands = new List<string>();
            using (var tbl = new DataTable())
            {
                using (var da = new SQLiteDataAdapter(
                    String.Format(
                    "SELECT vorder FROM verse WHERE poem_id={0} AND vorder>{1} ORDER BY vorder DESC",
                    PoemID, beforeVerseOrder
                    )
                    , _con))
                {
                    da.Fill(tbl);
                    foreach (DataRow Row in tbl.Rows)
                    {
                        var vorder = Convert.ToInt32(Row.ItemArray[0]);
                        updateCommands.Add(
                            String.Format("UPDATE verse SET vorder={0} WHERE poem_id={1} AND vorder={2}",
                            vorder + 1, PoemID, vorder));
                    }
                }
            }
            foreach (var updateCommand in updateCommands) {
                using var cmd = new SQLiteCommand(updateCommand, _con);
                cmd.ExecuteNonQuery();
            }
            using (var cmd = new SQLiteCommand(_con))
            {
                cmd.CommandText = String.Format(
                        "INSERT INTO verse (poem_id, vorder, position, text) VALUES ({0}, {1}, {2}, \"{3}\");",
                        PoemID, beforeVerseOrder + 1, (int)Position, ""
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
            using var cmd = new SQLiteCommand(_con);
            cmd.CommandText = String.Format(
                "UPDATE verse SET text = \"{0}\" WHERE poem_id={1} AND vorder={2}",
                Text.Trim(), PoemID, Order
            );
            cmd.ExecuteNonQuery();
            return true;
        }
        public bool SetVersePosition(int PoemID, int Order, VersePosition Position)
        {
            if (!Connected)
                return false;
            using var cmd = new SQLiteCommand(_con);
            cmd.CommandText = String.Format(
                "UPDATE verse SET position = {0} WHERE poem_id={1} AND vorder={2}",
                (int)Position, PoemID, Order
            );
            cmd.ExecuteNonQuery();
            return true;
        }
        public bool DeleteVerses(int PoemID, List<int> VerseOrders)
        {
            if (!Connected)
                return false;
            var poem = GetPoem(PoemID);
            if (null == poem)
                return false;
            BeginBatchOperation();
            foreach (var vorder in VerseOrders) {
                using var cmd = new SQLiteCommand(_con);
                cmd.CommandText = String.Format(
                    "DELETE FROM verse WHERE poem_id={0} AND vorder={1}",
                    PoemID, vorder
                );
                cmd.ExecuteNonQuery();
            }

            var vs = GetVerses(PoemID);
            for (var v = 0; v < vs.Count; v++) {
                using var cmd = new SQLiteCommand(_con);
                cmd.CommandText = String.Format(
                    "UPDATE verse SET vorder= {0} WHERE poem_id={1} AND vorder={2}",
                    -(1 + v), PoemID, vs[v]._Order
                );
                cmd.ExecuteNonQuery();
            }
            for (var v = 0; v < vs.Count; v++) {
                using var cmd = new SQLiteCommand(_con);
                cmd.CommandText = String.Format(
                    "UPDATE verse SET vorder= {0} WHERE poem_id={1} AND vorder={2}",
                    1 + v, PoemID, -(1 + v)
                );
                cmd.ExecuteNonQuery();
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
            if (null == Cat)//sorry!
                return;
            var SubCats = GetSubCategories(Cat._ID);
            foreach (var SubCat in SubCats)
                DRY_DeleteCat(SubCat);
            using var cmd = new SQLiteCommand(_con);
            cmd.CommandText = String.Format("DELETE FROM verse WHERE poem_id IN (SELECT id FROM poem WHERE cat_id={0});", Cat._ID);
            cmd.ExecuteNonQuery();
            cmd.CommandText = String.Format("DELETE FROM poem WHERE cat_id={0};", Cat._ID);
            cmd.ExecuteNonQuery();
            cmd.CommandText = String.Format("DELETE FROM cat WHERE id={0};", Cat._ID);
            cmd.ExecuteNonQuery();
        }
        public void DeletePoet(int PoetID)
        {
            DeleteCat(GetPoet(PoetID)._CatID);
            /*
            using (DataTable tbl = new DataTable())
            {
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(String.Format("SELECT cat_id FROM poet WHERE id={0}", PoetID), _con))
                {
                    da.Fill(tbl);
                    foreach (DataRow Row in tbl.Rows)
                        DeleteCat(Convert.ToInt32(Row.ItemArray[0]));
                }
            }*/
            using var cmd = new SQLiteCommand(_con);
            cmd.CommandText = String.Format("DELETE FROM poet WHERE id={0};", PoetID);
            cmd.ExecuteNonQuery();
        }
        public bool BeginBatchOperation()
        {
            if (!Connected)
                return false;
            using var cmd = new SQLiteCommand(_con);
            cmd.CommandText = "BEGIN TRANSACTION";
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                //BeginBatchOperation already called
                CommitBatchOperation();
                cmd.ExecuteNonQuery();
            }

            return true;
        }
        public bool CommitBatchOperation()
        {
            if (!Connected)
                return false;
            using var cmd = new SQLiteCommand(_con);
            cmd.CommandText = "COMMIT";
            cmd.ExecuteNonQuery();
            return true;
        }
        public bool SetPoemID(int PoemID, int NewPoemID)
        {
            if (!Connected)
                return false;
            using var cmd = new SQLiteCommand(_con);
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
            return true;
        }
        public bool SetPoemCatID(int PoemID, int NewCatID)
        {
            if (!Connected)
                return false;
            using var cmd = new SQLiteCommand(_con);
            cmd.CommandText = String.Format(
                "UPDATE poem SET cat_id = {0} WHERE id= {1}",
                NewCatID, PoemID
            );
            cmd.ExecuteNonQuery();
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
            var poet = GetPoet(PoetID);
            if (null == poet)
                return false;
            return Export(fileName, poet._CatID, true);
        }
        private static void CreateEmptyDB(SQLiteConnection connection) {
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "BEGIN TRANSACTION;" +
                              "CREATE TABLE [cat] ([id] INTEGER  PRIMARY KEY NOT NULL,[poet_id] INTEGER  NULL,[text] NVARCHAR(100)  NULL,[parent_id] INTEGER  NULL,[url] NVARCHAR(255)  NULL);"
                              +
                              "CREATE TABLE poem (id INTEGER PRIMARY KEY, cat_id INTEGER, title NVARCHAR(255), url NVARCHAR(255));"
                              +
                              "CREATE TABLE [poet] ([id] INTEGER  PRIMARY KEY NOT NULL,[name] NVARCHAR(20)  NULL,[cat_id] INTEGER  NULL  NULL, [description] TEXT);"
                              +
                              "CREATE TABLE [verse] ([poem_id] INTEGER  NULL,[vorder] INTEGER  NULL,[position] INTEGER  NULL,[text] TEXT  NULL);"
                              +
                              "COMMIT;";
            cmd.ExecuteNonQuery();
        }

        private bool Export(string fileName, int CatID, bool ExportPoet)
        {
            SQLiteConnection newConnection;
            try
            {
                var conString = new SQLiteConnectionStringBuilder {
                    DataSource = fileName,
                    DefaultTimeout = 5000,
                    FailIfMissing = false,
                    ReadOnly = false
                };
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
                if (ExportPoet) {
                    using var cmd = new SQLiteCommand(newConnection);
                    using var tbl = new DataTable();
                    using var da = new SQLiteDataAdapter("SELECT id, name, cat_id, description FROM poet WHERE cat_id=" + CatID, _con);
                    da.Fill(tbl);
                    foreach (DataRow row in tbl.Rows)
                    {
                        cmd.CommandText = String.Format(
                            "INSERT INTO poet (id, name, cat_id, description) VALUES ({0}, \"{1}\", {2}, \"{3}\");",
                            row.ItemArray[0], row.ItemArray[1], row.ItemArray[2], row.ItemArray[3]
                        );
                        cmd.ExecuteNonQuery();
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
            using (var cmd = new SQLiteCommand(newConnection))
            {
                cmd.CommandText = "BEGIN TRANSACTION;";
                cmd.ExecuteNonQuery();

                using (var tbl = new DataTable())
                using (var da = new SQLiteDataAdapter("SELECT id, poet_id, text, parent_id, url FROM cat WHERE id=" + CatID, _con))
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

                using (var tbl = new DataTable())
                using (var da = new SQLiteDataAdapter("SELECT id, cat_id, title, url FROM poem WHERE cat_id=" + CatID, _con))
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

                using (var tbl = new DataTable())
                using (var da = new SQLiteDataAdapter("SELECT poem_id, vorder, position, text FROM verse WHERE poem_id IN (SELECT id FROM poem WHERE cat_id=" + CatID + ")", _con))
                {
                    da.Fill(tbl);
                    foreach (DataRow row in tbl.Rows)
                    {
                        cmd.CommandText = String.Format(
                            "INSERT INTO verse (poem_id, vorder, position, text) VALUES ({0}, {1}, {2}, \"{3}\");",
                            row.ItemArray[0], row.ItemArray[1], row.ItemArray[2], row.ItemArray[3].ToString().Replace("\"", "\"\"")
                            );
                        cmd.ExecuteNonQuery();
                    }
                }

                cmd.CommandText = "COMMIT;";
                cmd.ExecuteNonQuery();

            }

            var SubCats = GetSubCategories(CatID);
            foreach (var SubCat in SubCats)
                ExportCat(SubCat._ID, newConnection);
        }
        #endregion

        #region Replace
        public void Replace(string searchterm, string replacement, bool onlyInPoemTitles) {
            using var cmd = new SQLiteCommand(_con);
            cmd.CommandText = "UPDATE poem SET title = REPLACE(title, '" + searchterm + "', '" + replacement + "');";
            cmd.ExecuteNonQuery();

            if (!onlyInPoemTitles)
            {
                cmd.CommandText = "UPDATE poet SET name = REPLACE(name, '" + searchterm + "', '" + replacement + "');";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "UPDATE poet SET description = REPLACE(description, '" + searchterm + "', '" + replacement + "');";
                cmd.ExecuteNonQuery();


                cmd.CommandText = "UPDATE cat SET text = REPLACE(text, '" + searchterm + "', '" + replacement + "');";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "UPDATE verse SET text = REPLACE(text, '" + searchterm + "', '" + replacement + "');";
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Edit IDs
        public List<int> GetAllSubCats(int CatID)
        {
            var subIDs = new List<int>();
            var subs = GetSubCategories(CatID);
            foreach (var sub in subs)
            {
                subIDs.Add(sub._ID);
                var subsubs = GetAllSubCats(sub._ID);
                subIDs.AddRange(subsubs);
            }
            return subIDs;
        }
        private int GetMainCatID(int PoetID)
        {
            var poet = GetPoet(PoetID);
            if (poet == null)
                return -1;
            return poet._CatID;
        }
        private int GetMinPoemID(int MainCatID)
        {
            if (MainCatID != -1)
            {
                var cats = GetAllSubCats(MainCatID);
                cats.Add(MainCatID);
                var strQuery = "SELECT MIN(id) FROM poem WHERE cat_id IN (";
                for (var i = 0; i < cats.Count - 1; i++)
                {
                    strQuery += cats[i].ToString();
                    strQuery += ", ";
                }
                if (cats.Count - 1 >= 0)
                {
                    strQuery += cats[cats.Count - 1].ToString();
                }
                strQuery += ");";
                using var tbl = new DataTable();
                using var da = new SQLiteDataAdapter(strQuery, _con);
                da.Fill(tbl);
                if (tbl.Rows.Count > 0)
                {
                    return Convert.ToInt32(tbl.Rows[0].ItemArray[0]);
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
            if (PoetID == NewID)
                return true;
            if (GetPoet(NewID) != null)
                return false;
            if (BeginBatchOperation())
            {
                using
                    (
                    var cmd = new SQLiteCommand(
                    String.Format("UPDATE poet SET id = {0} WHERE id = {1}", NewID, PoetID),
                    _con)
                    )
                {
                    cmd.ExecuteNonQuery();
                }
                using
                    (
                    var cmd = new SQLiteCommand(
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
            var minCatID = GetMainCatID(PoetID);
            if (minCatID == NewStartCatID)
                return true;
            var cats = GetAllSubCats(minCatID);
            cats.Insert(0, minCatID);
            if (BeginBatchOperation())
            {
                var ids = new Dictionary<int, int>();
                var NewCatID = NewStartCatID;

                foreach (var CatID in cats)
                {
                    while (GetCategory(NewCatID) != null)
                        NewCatID++;
                    using
                        (
                        var cmd = new SQLiteCommand(
                        String.Format("UPDATE cat SET id = {0} WHERE id = {1}", NewCatID, CatID),
                        _con)
                        )
                    {
                        cmd.ExecuteNonQuery();
                    }
                    using
                        (
                        var cmd = new SQLiteCommand(
                        String.Format("UPDATE cat SET parent_id = {0} WHERE parent_id = {1}", NewCatID, CatID),
                        _con)
                        )
                    {
                        cmd.ExecuteNonQuery();
                    }
                    using
                        (
                        var cmd = new SQLiteCommand(
                        String.Format("UPDATE poet SET cat_id = {0} WHERE cat_id = {1}", NewCatID, CatID),
                        _con)
                        )
                    {
                        cmd.ExecuteNonQuery();
                    }
                    using
                        (
                        var cmd = new SQLiteCommand(
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
            var minCatID = GetMainCatID(PoetID);
            var cats = GetAllSubCats(minCatID);
            cats.Insert(0, minCatID);
            if (BeginBatchOperation())
            {
                var NewPoemID = NewStartPoemID;

                //========== 92-09-22: this is wrong, but fix needs more work, for now I should repeat changing IDs one time more
                /*
                int MinPoemID= NewStartPoemID+1;
                foreach (int CatID in cats)
                {
                    List<GanjoorPoem> poems = GetPoems(CatID);
                    foreach (GanjoorPoem poem in poems)
                        if (poem._ID < MinPoemID)
                            MinPoemID = poem._ID;
                }

                if (MinPoemID == NewStartPoemID)
                {
                    CommitBatchOperation();
                    return true;
                }
                */

                foreach (var CatID in cats)
                {
                    var poems = GetPoems(CatID);
                    foreach (var poem in poems)
                    {
                        while (GetPoem(NewPoemID) != null)
                            NewPoemID++;
                        using
                            (
                            var cmd = new SQLiteCommand(
                            String.Format("UPDATE poem SET id = {0} WHERE id = {1}", NewPoemID, poem._ID),
                            _con)
                            )
                        {
                            cmd.ExecuteNonQuery();
                        }
                        using
                            (
                            var cmd = new SQLiteCommand(
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
            if (Connected) {
                using var tbl = new DataTable();
                using var da = new SQLiteDataAdapter(
                    string.Format("SELECT cat_id FROM gil WHERE cat_id = {0}", CatID)
                    , _con);
                da.Fill(tbl);
                return tbl.Rows.Count != 0;
            }
            return false;
        }
        public void AddToGDBIgnoreList(int CatID)
        {
            if (Connected)
            {
                if (!IsInGDBIgnoreList(CatID)) {
                    using var tbl = new DataTable();
                    using var cmd = new SQLiteCommand(
                        string.Format("INSERT INTO gil (cat_id) VALUES ({0})", CatID)
                        , _con);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region INDEXing
        public bool CreateIndexes()
        {
            if (_con == null)
            {//92-08-30
                return false;
            }
            //cat_id in poem:
            using (var cmd = new SQLiteCommand(
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_poem_catid ON poem(id ASC, cat_id ASC)", _con))
            {
                cmd.ExecuteNonQuery();
            }
            //title in poem:
            using (var cmd = new SQLiteCommand(
                "CREATE INDEX IF NOT EXISTS idx_poem_title ON poem(id ASC, title ASC)", _con))
            {
                cmd.ExecuteNonQuery();
            }
            //verse relation indexes:
            using (var cmd = new SQLiteCommand(
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_verse ON verse(poem_id ASC, vorder ASC)", _con))
            {
                cmd.ExecuteNonQuery();
            }
            return true;
        }
        #endregion

        #region Audio File Management
        /// <summary>
        /// اطلاعات اولین فایل صتی مرتبط را بر می گرداند
        /// </summary>
        /// <param name="nPoemId"></param>
        /// <returns></returns>
        public PoemAudio GetMainPoemAudio(int nPoemId)
        {
            var pa = GetPoemAudioFiles(nPoemId, true);
            if (pa.Length > 0)
            {
                return pa[0];
            }

            return null;
        }

        /// <summary>
        /// آیا فایل صوتی شعر همگامسازی شده؟
        /// </summary>
        /// <param name="nPoemId"></param>
        /// <param name="nAudioId"></param>
        /// <returns></returns>
        public bool PoemAudioHasSyncData(int nPoemId, int nAudioId)
        {
            if (Connected) {
                using var tbl = new DataTable();
                using var da = new SQLiteDataAdapter(
                    String.Format("SELECT milisec FROM sndsync WHERE poem_id = {0} AND snd_id = {1} LIMIT 1",
                        nPoemId, nAudioId), _con
                );
                da.Fill(tbl);
                return tbl.Rows.Count > 0;
            }
            return false;
        }

        /// <summary>
        ///اطلاعات  فایلهای صوتی منتسب به یک شعر را به ترتیب تنظیم شده بر می گرداند
        /// </summary>
        /// <param name="nPoemId">شناسۀ شعر</param>
        /// <returns>لیست فایلهای صوتی</returns>
        public PoemAudio[] GetPoemAudioFiles(int nPoemId, bool bOnlyFirst = false)
        {
            var lstAudio = new List<PoemAudio>();
            if (Connected) {
                using var tbl = new DataTable();
                var strQuery = String.Format("SELECT * FROM poemsnd WHERE poem_id = {0} ORDER BY id", nPoemId);
                if (bOnlyFirst)
                    strQuery += " LIMIT 1";

                using var da = new SQLiteDataAdapter(strQuery, _con);
                da.Fill(tbl);
                foreach (DataRow row in tbl.Rows)
                {
                    lstAudio.Add(
                        new PoemAudio {
                            PoemId = nPoemId,
                            Id = Convert.ToInt32(row["id"]),
                            FilePath = row["filepath"].ToString(),
                            Description = row["description"].ToString(),
                            DownloadUrl = row["dnldurl"].ToString(),
                            IsDirectlyDownloadable = Convert.ToInt32(row["isdirect"]) == 1,
                            SyncGuid = Guid.Parse(row["syncguid"].ToString()),
                            FileCheckSum = row["fchksum"].ToString(),
                            IsUploaded = Convert.ToInt32(row["isuploaded"]) == 1
                        }
                    );
                    lstAudio[lstAudio.Count - 1].SyncArray = GetPoemSync(lstAudio[lstAudio.Count - 1]);

                }
            }
            return lstAudio.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Warning PoemAudio[] is incomplete
        /// </remarks>
        /// <returns></returns>
        public PoemAudio[] GetAllPoemAudioFiles()
        {
            var lstAudio = new List<PoemAudio>();
            if (Connected) {
                using var tbl = new DataTable();
                var strQuery = "SELECT s.poem_id as [poem_id], s.id as [id], s.filepath as [filepath], s.description as [description], s.dnldurl as [dnldurl],  s.isdirect as [isdirect], s.syncguid as [syncguid], s.fchksum as [fchksum], s.isuploaded as [isuploaded], p.title || ('::' || (SELECT v.text FROM verse v  WHERE v.poem_id = s.poem_id AND v.vorder=1 )) as [poemtitle], e.name as [poetname] FROM poemsnd s INNER JOIN poem p ON s.poem_id = p.id INNER JOIN cat c ON p.cat_id = c.id INNER JOIN poet e ON e.id= c.poet_id ORDER BY s.poem_id, s.id";

                using var da = new SQLiteDataAdapter(strQuery, _con);
                da.Fill(tbl);
                foreach (DataRow row in tbl.Rows)
                {
                    lstAudio.Add(
                        new PoemAudio {
                            PoemId = Convert.ToInt32(row["poem_id"]),
                            Id = Convert.ToInt32(row["id"]),
                            FilePath = row["filepath"].ToString(),
                            Description = row["description"].ToString(),
                            DownloadUrl = row["dnldurl"].ToString(),
                            IsDirectlyDownloadable = Convert.ToInt32(row["isdirect"]) == 1,
                            SyncGuid = Guid.Parse(row["syncguid"].ToString()),
                            FileCheckSum = row["fchksum"].ToString(),
                            IsUploaded = Convert.ToInt32(row["isuploaded"]) == 1,
                            PoemTitle = row["poemtitle"].ToString(),
                            PoetName = row["poetname"].ToString()
                        }
                    );

                }
            }
            return lstAudio.ToArray();
        }


        /// <summary>
        /// new id for a poemsnd
        /// </summary>
        /// <param name="nPoemId"></param>
        /// <returns></returns>
        private int GeneratetNewAudioId(int nPoemId)
        {
            int nAudioID;
            using var tbl = new DataTable();
            using var da = new SQLiteDataAdapter(String.Format("SELECT MAX(id) FROM poemsnd WHERE poem_id = {0}", nPoemId), _con);
            da.Fill(tbl);
            if (tbl.Rows.Count == 1)
            {
                try
                {
                    nAudioID = Convert.ToInt32(tbl.Rows[0].ItemArray[0]);
                }
                catch
                {
                    nAudioID = 0;
                }
                nAudioID++;
            }
            else
                nAudioID = 1;

            return nAudioID;

        }

        private bool DoesPoemAudioExistsWithId(int nPoemId, int nId)
        {
            using var tbl = new DataTable();
            using var da = new SQLiteDataAdapter(String.Format("SELECT * FROM poemsnd WHERE poem_id = {0} AND id = {1}", nPoemId, nId), _con);
            da.Fill(tbl);
            if (tbl.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }


        public PoemAudio AddAudio(string filePath, PoemAudio srcAudio, int nDefId = -1)
        {
            if (Connected)
            {
                var nPoemId = srcAudio.PoemId;
                using var cmd = new SQLiteCommand(_con);
                var nId = nDefId;
                if (nId == -1)
                    nId = GeneratetNewAudioId(nPoemId);
                else
                {
                    if (DoesPoemAudioExistsWithId(nPoemId, nId))
                        nId = GeneratetNewAudioId(nPoemId);
                }

                var newPoemAudio = new PoemAudio {
                    PoemId = nPoemId,
                    Id = nId,
                    FilePath = filePath,
                    Description = srcAudio.Description,
                    FileCheckSum = PoemAudio.ComputeCheckSum(filePath),
                    DownloadUrl = srcAudio.DownloadUrl,
                    IsDirectlyDownloadable = srcAudio.IsDirectlyDownloadable,
                    SyncGuid = srcAudio.SyncGuid,
                    IsUploaded = srcAudio.IsUploaded
                };

                cmd.CommandText = String.Format(
                    "INSERT INTO poemsnd (poem_id, id, filepath, description, dnldurl, isdirect, syncguid, fchksum, isuploaded) " +
                    "VALUES ({0}, {1}, \"{2}\", \"{3}\", \"{4}\", {5}, \"{6}\", \"{7}\", {8});",
                    newPoemAudio.PoemId, newPoemAudio.Id, newPoemAudio.FilePath, newPoemAudio.Description,
                    newPoemAudio.DownloadUrl, newPoemAudio.IsDirectlyDownloadable ? 1 : 0, newPoemAudio.SyncGuid.ToString(), newPoemAudio.FileCheckSum, newPoemAudio.IsUploaded ? 1 : 0
                );
                cmd.ExecuteNonQuery();

                newPoemAudio.SyncArray = srcAudio.SyncArray;

                SavePoemSync(newPoemAudio, newPoemAudio.SyncArray, false);

                DeleteAudioWithSync(newPoemAudio.PoemId, newPoemAudio.SyncGuid, newPoemAudio.Id);

                return newPoemAudio;
            }
            return null;
        }

        /// <summary>
        /// اضافه کردن فایل صوتی برای شعر
        /// </summary>
        /// <param name="nPoemId"></param>
        /// <param name="filePath"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public PoemAudio AddAudio(int nPoemId, string filePath, string desc, int nDefId = -1)
        {
            if (Connected) {
                using var cmd = new SQLiteCommand(_con);
                var nId = nDefId;
                if (nId == -1)
                    nId = GeneratetNewAudioId(nPoemId);
                else
                {
                    if (DoesPoemAudioExistsWithId(nPoemId, nId))
                        nId = GeneratetNewAudioId(nPoemId);
                }

                var newPoemAudio = new PoemAudio {
                    PoemId = nPoemId,
                    Id = nId,
                    FilePath = filePath,
                    Description = desc,
                    FileCheckSum = PoemAudio.ComputeCheckSum(filePath),
                    DownloadUrl = "",
                    IsDirectlyDownloadable = false,
                    SyncGuid = Guid.NewGuid(),
                    IsUploaded = false
                };

                cmd.CommandText = String.Format(
                    "INSERT INTO poemsnd (poem_id, id, filepath, description, dnldurl, isdirect, syncguid, fchksum, isuploaded) " +
                    "VALUES ({0}, {1}, \"{2}\", \"{3}\", \"{4}\", {5}, \"{6}\", \"{7}\", {8});",
                    newPoemAudio.PoemId, newPoemAudio.Id, newPoemAudio.FilePath, newPoemAudio.Description,
                    newPoemAudio.DownloadUrl, newPoemAudio.IsDirectlyDownloadable ? 1 : 0, newPoemAudio.SyncGuid.ToString(), newPoemAudio.FileCheckSum, newPoemAudio.IsUploaded ? 1 : 0
                );
                cmd.ExecuteNonQuery();
                return newPoemAudio;
            }
            return null;
        }

        /// <summary>
        /// حذف ارتباط با فایل صوتی
        /// </summary>
        /// <param name="audio"></param>
        /// <returns></returns>
        public bool DeleteAudio(PoemAudio audio)
        {
            if (Connected)
            {
                if (audio != null)
                {
                    SavePoemSync(audio, null, false);
                    using var cmd = new SQLiteCommand(_con);
                    cmd.CommandText = String.Format(
                        "DELETE FROM poemsnd WHERE poem_id = {0} AND id = {1};",
                        audio.PoemId, audio.Id
                    );
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            return false;
        }

        /// <summary>
        /// حذف سطور تکراری خوانش پیش از دریافت
        /// </summary>
        /// <param name="nPoemId"></param>
        /// <param name="guidSync"></param>
        /// <returns></returns>
        public bool DeleteAudioWithSync(int nPoemId, Guid guidSync, int nExceptFor)
        {
            if (Connected) {
                using var cmd = new SQLiteCommand(_con);
                cmd.CommandText = String.Format(
                    "DELETE FROM poemsnd WHERE poem_id = {0} AND syncguid = '{1}' AND id <> {2};",
                    nPoemId, guidSync.ToString(), nExceptFor
                );
                cmd.ExecuteNonQuery();
                return true;
            }
            return false;
        }

        /// <summary>
        /// ذخیرۀ همگامسازی
        /// </summary>
        /// <param name="audio"></param>
        /// <param name="verseMilisecPositions"></param>
        /// <returns></returns>
        public bool SavePoemSync(PoemAudio audio, PoemAudio.SyncInfo[] verseMilisecPositions, bool bUpdateGuid)
        {
            if (Connected && audio != null)
            {
                using (var cmd = new SQLiteCommand(_con))
                {
                    cmd.CommandText = String.Format(
                        "DELETE FROM sndsync WHERE poem_id = {0} AND snd_id = {1};",
                        audio.PoemId, audio.Id
                        );
                    cmd.ExecuteNonQuery();
                }
                if (verseMilisecPositions != null)
                {
                    BeginBatchOperation();
                    for (var i = 0; i < verseMilisecPositions.Length; i++) {
                        using var cmd = new SQLiteCommand(_con);
                        cmd.CommandText = String.Format(
                            "INSERT INTO sndsync (poem_id, snd_id, verse_order, milisec) VALUES ({0}, {1}, {2}, {3});",
                            audio.PoemId, audio.Id, verseMilisecPositions[i].VerseOrder, verseMilisecPositions[i].AudioMiliseconds
                        );
                        cmd.ExecuteNonQuery();
                    }

                    if (bUpdateGuid) {
                        using var cmd = new SQLiteCommand(_con);
                        cmd.CommandText = String.Format("UPDATE poemsnd SET syncguid = \"{0}\" WHERE poem_id = {1} AND id = {2}",
                            Guid.NewGuid().ToString(), audio.PoemId, audio.Id);
                        cmd.ExecuteNonQuery();
                    }

                    CommitBatchOperation();
                }
            }

            return false;
        }

        /// <summary>
        /// آپدیت Guid 
        /// </summary>
        /// <param name="poemAudio"></param>
        public bool ReadPoemAudioGuid(ref PoemAudio poemAudio)
        {
            if (Connected) {
                using var tbl = new DataTable();
                var strQuery = String.Format("SELECT syncguid FROM poemsnd WHERE poem_id = {0} AND id = {1}", poemAudio.PoemId, poemAudio.Id);

                using var da = new SQLiteDataAdapter(strQuery, _con);
                da.Fill(tbl);
                foreach (DataRow row in tbl.Rows)
                {
                    poemAudio.SyncGuid = Guid.Parse(row["syncguid"].ToString());//only one row
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// ذخیره Guid خوانش
        /// </summary>
        /// <param name="audio"></param>
        /// <returns></returns>
        public bool WritePoemAudioGuid(PoemAudio audio)
        {
            if (Connected) {
                using var cmd = new SQLiteCommand(_con);
                cmd.CommandText = String.Format("UPDATE poemsnd SET syncguid = \"{0}\" WHERE poem_id = {1} AND id = {2}",
                    audio.SyncGuid.ToString(), audio.PoemId, audio.Id);
                cmd.ExecuteNonQuery();
                return true;
            }
            return false;

        }



        /// <summary>
        /// اطلاعات همگامسازی فایل صوتی شعر
        /// </summary>
        /// <param name="audio"></param>
        /// <returns></returns>
        public PoemAudio.SyncInfo[] GetPoemSync(PoemAudio audio)
        {
            var lstVersePosInMilisec = new List<PoemAudio.SyncInfo>();

            if (Connected && audio != null) {
                using var tbl = new DataTable();
                var strQuery = String.Format("SELECT verse_order, milisec FROM sndsync WHERE poem_id = {0} AND snd_id = {1} ORDER BY milisec", audio.PoemId, audio.Id);

                using var da = new SQLiteDataAdapter(strQuery, _con);
                da.Fill(tbl);
                foreach (DataRow row in tbl.Rows)
                {
                    lstVersePosInMilisec.Add
                    (
                        new PoemAudio.SyncInfo {
                            VerseOrder = Convert.ToInt32(row.ItemArray[0]),
                            AudioMiliseconds = Convert.ToInt32(row.ItemArray[1])
                        }

                    );
                }
            }


            return lstVersePosInMilisec.Count > 0 ? lstVersePosInMilisec.ToArray() : null;
        }


        /// <summary>
        /// فایل صوتی، فایل صوتی اصلی شعر بشود
        /// </summary>
        /// <param name="audio"></param>
        /// <returns></returns>
        public bool MoveToTop(PoemAudio audio)
        {
            if (Connected)
            {
                if (audio != null)
                {
                    if (audio.Id == 1)
                        return false;

                    using (var cmd = new SQLiteCommand(_con))
                    {
                        cmd.CommandText = String.Format(
                            "UPDATE poemsnd SET id = 0 WHERE poem_id = {0} AND id = {1};",
                            audio.PoemId, audio.Id
                            );
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SQLiteCommand(_con))
                    {
                        cmd.CommandText = String.Format(
                            "UPDATE sndsync SET snd_id = 0 WHERE poem_id = {0} AND snd_id = {1};",
                            audio.PoemId, audio.Id
                            );
                        cmd.ExecuteNonQuery();
                    }


                    using (var cmd = new SQLiteCommand(_con))
                    {
                        cmd.CommandText = String.Format(
                            "UPDATE poemsnd SET id = {0} WHERE poem_id = {1} AND id = 1;",
                            audio.Id, audio.PoemId
                            );
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SQLiteCommand(_con))
                    {
                        cmd.CommandText = String.Format(
                            "UPDATE sndsync SET snd_id = {0} WHERE poem_id = {1} AND snd_id = 1;",
                            audio.Id, audio.PoemId
                            );
                        cmd.ExecuteNonQuery();
                    }


                    using (var cmd = new SQLiteCommand(_con))
                    {
                        cmd.CommandText = String.Format(
                            "UPDATE poemsnd SET id = 1 WHERE poem_id = {0} AND id = 0;",
                            audio.PoemId
                            );
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SQLiteCommand(_con))
                    {
                        cmd.CommandText = String.Format(
                            "UPDATE sndsync SET snd_id = 1 WHERE poem_id = {0} AND snd_id = 0;",
                            audio.PoemId
                            );
                        cmd.ExecuteNonQuery();
                    }

                    return true;


                }
            }
            return false;
        }

        /// <summary>
        /// آیا خوانش وجود دارد -  برای دریافت
        /// </summary>
        /// <param name="nPoemId">شناسه شعر</param>
        /// <param name="SyncGuid">شناسه خوانش</param>
        /// <returns></returns>
        public bool PoemAudioExists(int nPoemId, string guidSync)
        {
            if (Connected) {
                using var tbl = new DataTable();
                var strQuery = String.Format("SELECT * FROM poemsnd WHERE poem_id = {0} AND syncguid = '{1}'", nPoemId, guidSync);

                using var da = new SQLiteDataAdapter(strQuery, _con);
                da.Fill(tbl);
                foreach (DataRow row in tbl.Rows)
                {
                    return true;
                }
            }
            return false;
        }


        #endregion

        #region Technical Problems
        public List<GanjoorVerse> GetVersesWithTechnicalProblems(bool top1)
        {
            var lst = new List<GanjoorVerse>();
            if (Connected) {
                using var tbl = new DataTable();
                var sql = "SELECT v2.vorder, v2.position, v2.text, v2.poem_id FROM verse v2 WHERE v2.position = 1 AND NOT EXISTS (SELECT * FROM verse v1 WHERE v2.poem_id = v1.poem_id AND v1.vorder = (v2.vorder - 1) AND v1.position = 0 )";
                if (top1)
                    sql += " LIMIT 1";
                using var da = new SQLiteDataAdapter(sql, _con);
                da.Fill(tbl);
                foreach (DataRow row in tbl.Rows)
                    lst.Add(new GanjoorVerse(
                        Convert.ToInt32(row.ItemArray[3]),
                        Convert.ToInt32(row.ItemArray[0]),
                        (VersePosition)Convert.ToInt32(row.ItemArray[1]),
                        row.ItemArray[2].ToString()
                    ));
            }
            return lst;
        }
        #endregion
    }
}
