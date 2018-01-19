using PerpTool.db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perptool.db
{

    public class NPCGroupData : INotifyPropertyChanged
    {
        public string ZoneName { get; set; }
        public string ZoneNote { get; set; }
        public string NPCDefinitionName { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class NPCFlockData : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private int _presenceid;
        private int _flockmembercount;
        private int _definition;
        private int _spawnoriginX;
        private int _spawnoriginY;
        private int _spawnrangeMin;
        private int _spawnrangeMax;
        private int _respawnseconds;
        private int _totalspawncount;
        private int _homerange;
        private string _note;
        private decimal _respawnmultiplierlow;
        private int _enabled;
        private int _iscallforhelp;
        private int _behaviorType;

        public DBAction dBAction;
        private int _def;
        public int definition
        {
            get
            {
                return this._def;
            }
            set
            {
                this._def = value;
                OnPropertyChanged("definition");
            }
        }
        private string _defname;
        public string NPCDefinitionName
        {
            get
            {
                return this._defname;
            }
            set
            {
                this._defname = value;
                OnPropertyChanged("NPCDefinitionName");
            }
        }
        public int id { get { return this._id; } set { this._id = value; OnPropertyChanged("id"); } }
        public string name { get { return this._name; } set { this._name = value; OnPropertyChanged("name"); } }
        public int presenceid { get { return this._presenceid; } set { this._presenceid = value; OnPropertyChanged("presenceid"); } }
        public int flockmembercount { get { return this._flockmembercount; } set { this._flockmembercount = value; OnPropertyChanged("flockmembercount"); } }
        public int spawnoriginX { get { return this._spawnoriginX; } set { this._spawnoriginX = value; OnPropertyChanged("spawnoriginX"); } }
        public int spawnoriginY { get { return this._spawnoriginY; } set { this._spawnoriginY = value; OnPropertyChanged("spawnoriginY"); } }
        public int spawnrangeMin { get { return this._spawnrangeMin; } set { this._spawnrangeMin = value; OnPropertyChanged("spawnrangeMin"); } }
        public int spawnrangeMax { get { return this._spawnrangeMax; } set { this._spawnrangeMax = value; OnPropertyChanged("spawnrangeMax"); } }
        public int respawnseconds { get { return this._respawnseconds; } set { this._respawnseconds = value; OnPropertyChanged("respawnseconds"); } }
        public int totalspawncount { get { return this._totalspawncount; } set { this._totalspawncount = value; OnPropertyChanged("totalspawncount"); } }
        public int homerange { get { return this._homerange; } set { this._homerange = value; OnPropertyChanged("homerange"); } }
        public string note { get { return this._note; } set { this._note = value; OnPropertyChanged("note"); } }
        public decimal respawnmultiplierlow { get { return this._respawnmultiplierlow; } set { this._respawnmultiplierlow = value; OnPropertyChanged("respawnmultiplierlow"); } }
        public int enabled { get { return this._enabled; } set { this._enabled = value; OnPropertyChanged("enabled"); } }
        public int iscallforhelp { get { return this._iscallforhelp; } set { this._iscallforhelp = value; OnPropertyChanged("iscallforhelp"); } }
        public int behaviorType { get { return this._behaviorType; } set { this._behaviorType = value; OnPropertyChanged("behaviorType"); } }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public static string IDkey = "@flockID";

        public static string GetDeclStatement()
        {
            return "DECLARE "+ IDkey + " int;";
        }

        public string GetLookupStatement()
        {
            return "SET "+ IDkey + " = (SELECT TOP 1 id from npcflock WHERE [name] = '" + this.name + "' ORDER BY id DESC);";
        }

        public NPCFlockData copy()
        {
            NPCFlockData d = new NPCFlockData();
            d.id = this.id;
            d.behaviorType = this.behaviorType;
            d.dBAction = this.dBAction;
            d.definition = this.definition;
            d.enabled = this.enabled;
            d.flockmembercount = this.flockmembercount;
            d.homerange = this.homerange;
            d.iscallforhelp = this.iscallforhelp;
            d.name = this.name;
            d.note = this.note;
            d.NPCDefinitionName = this.NPCDefinitionName;
            d.presenceid = this.presenceid;
            d.respawnmultiplierlow = this.respawnmultiplierlow;
            d.respawnseconds = this.respawnseconds;
            d.spawnoriginX = this.spawnoriginX;
            d.spawnoriginY = this.spawnoriginY;
            d.spawnrangeMax = this.spawnrangeMax;
            d.spawnrangeMin = this.spawnrangeMin;
            d.totalspawncount = this.totalspawncount;
            return d;
        }
    }

    class NPCFlock : INotifyPropertyChanged
    {

        private int _id;
        private string _name;
        private int _presenceid;
        private int _flockmembercount;
        private int _definition;
        private int _spawnoriginX;
        private int _spawnoriginY;
        private int _spawnrangeMin;
        private int _spawnrangeMax;
        private int _respawnseconds;
        private int _totalspawncount;
        private int _homerange;
        private string _note;
        private decimal _respawnmultiplierlow;
        private int _enabled;
        private int _iscallforhelp;
        private int _behaviorType;

        public NPCFlock(string connectionString)
        {
            this.ConnString = connectionString;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        #region Fields
        public int id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
                this.OnPropertyChanged("id");
            }
        }
        public string name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
                this.OnPropertyChanged("name");
            }
        }
        public int presenceid
        {
            get
            {
                return this._presenceid;
            }
            set
            {
                this._presenceid = value;
                this.OnPropertyChanged("presenceid");
            }
        }
        public int flockmembercount
        {
            get
            {
                return this._flockmembercount;
            }
            set
            {
                this._flockmembercount = value;
                this.OnPropertyChanged("flockmembercount");
            }
        }
        public int definition
        {
            get
            {
                return this._definition;
            }
            set
            {
                this._definition = value;
                this.OnPropertyChanged("definition");
            }
        }
        public int spawnoriginX
        {
            get
            {
                return this._spawnoriginX;
            }
            set
            {
                this._spawnoriginX = value;
                this.OnPropertyChanged("spawnoriginX");
            }
        }
        public int spawnoriginY
        {
            get
            {
                return this._spawnoriginY;
            }
            set
            {
                this._spawnoriginY = value;
                this.OnPropertyChanged("spawnoriginY");
            }
        }
        public int spawnrangeMin
        {
            get
            {
                return this._spawnrangeMin;
            }
            set
            {
                this._spawnrangeMin = value;
                this.OnPropertyChanged("spawnrangeMin");
            }
        }
        public int spawnrangeMax
        {
            get
            {
                return this._spawnrangeMax;
            }
            set
            {
                this._spawnrangeMax = value;
                this.OnPropertyChanged("spawnrangeMax");
            }
        }
        public int respawnseconds
        {
            get
            {
                return this._respawnseconds;
            }
            set
            {
                this._respawnseconds = value;
                this.OnPropertyChanged("respawnseconds");
            }
        }
        public int totalspawncount
        {
            get
            {
                return this._totalspawncount;
            }
            set
            {
                this._totalspawncount = value;
                this.OnPropertyChanged("totalspawncount");
            }
        }
        public int homerange
        {
            get
            {
                return this._homerange;
            }
            set
            {
                this._homerange = value;
                this.OnPropertyChanged("homerange");
            }
        }
        public string note
        {
            get
            {
                return this._note;
            }
            set
            {
                this._note = value;
                this.OnPropertyChanged("note");
            }
        }
        public decimal respawnmultiplierlow
        {
            get
            {
                return this._respawnmultiplierlow;
            }
            set
            {
                this._respawnmultiplierlow = value;
                this.OnPropertyChanged("respawnmultiplierlow");
            }
        }
        public int enabled
        {
            get
            {
                return this._enabled;
            }
            set
            {
                this._enabled = value;
                this.OnPropertyChanged("enabled");
            }
        }
        public int iscallforhelp
        {
            get
            {
                return this._iscallforhelp;
            }
            set
            {
                this._iscallforhelp = value;
                this.OnPropertyChanged("iscallforhelp");
            }
        }
        public int behaviorType
        {
            get
            {
                return this._behaviorType;
            }
            set
            {
                this._behaviorType = value;
                this.OnPropertyChanged("behaviorType");
            }
        }

        private string ConnString { get; set; }

        #endregion

        public void Clear()
        {
            this.id = 0;
            this.name = "";
            this.presenceid = 0;
            this.flockmembercount = 0;
            this.definition = 0;
            this.spawnoriginX = 0;
            this.spawnoriginY = 0;
            this.spawnrangeMin = 0;
            this.spawnrangeMax = 0;
            this.respawnseconds = 0;
            this.totalspawncount = 0;
            this.homerange = 0;
            this.note = "";
            this.respawnmultiplierlow = 0;
            this.enabled = 0;
            this.iscallforhelp = 0;
            this.behaviorType = 0;
        }

        public List<NPCFlockData> GetAllFlocks()
        {
            List<NPCFlockData> list = new List<NPCFlockData>();
            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append(@"SELECT entitydefaults.definitionname, npcflock.* FROM npcflock 
                    JOIN entitydefaults on entitydefaults.definition=npcflock.definition");
                    command.CommandText = sqlCommand.ToString();
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NPCFlockData flock = new NPCFlockData();
                            flock.behaviorType = Convert.ToInt32(reader["behaviorType"]);
                            flock.definition = Convert.ToInt32(reader["definition"]);
                            flock.enabled = Convert.ToInt32(reader["enabled"]);
                            flock.flockmembercount = Convert.ToInt32(reader["flockmembercount"]);
                            flock.homerange = Convert.ToInt32(reader["homerange"]);
                            flock.id = Convert.ToInt32(reader["id"]);
                            flock.iscallforhelp = Convert.ToInt32(reader["iscallforhelp"]);
                            flock.name = Convert.ToString(reader["name"]);
                            flock.note = Convert.ToString(reader["note"]);
                            flock.presenceid = Convert.ToInt32(reader["presenceid"]);
                            flock.respawnmultiplierlow = Convert.ToInt32(reader["respawnmultiplierlow"]);
                            flock.respawnseconds = Convert.ToInt32(reader["respawnseconds"]);
                            flock.spawnoriginX = Convert.ToInt32(reader["spawnoriginX"]);
                            flock.spawnoriginY = Convert.ToInt32(reader["spawnoriginY"]);
                            flock.spawnrangeMax = Convert.ToInt32(reader["spawnrangeMax"]);
                            flock.spawnrangeMin = Convert.ToInt32(reader["spawnrangeMin"]);
                            flock.totalspawncount = Convert.ToInt32(reader["totalspawncount"]);
                            flock.NPCDefinitionName = Convert.ToString(reader["definitionname"]);
                            flock.dBAction = DBAction.UPDATE;
                            list.Add(flock);
                        }

                    }
                }
            }
            return list;
        }

        public List<NPCFlockData> getByPresenceID(int presID)
        {
            List<NPCFlockData> list = new List<NPCFlockData>();
            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append(@"SELECT entitydefaults.definitionname, npcflock.* FROM npcflock 
                    JOIN entitydefaults on entitydefaults.definition=npcflock.definition
                    WHERE presenceid=@id;");
                    command.Parameters.AddWithValue("@id", presID);
                    command.CommandText = sqlCommand.ToString();
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NPCFlockData flock = new NPCFlockData();
                            flock.behaviorType = Convert.ToInt32(reader["behaviorType"]);
                            flock.definition = Convert.ToInt32(reader["definition"]);
                            flock.enabled = Convert.ToInt32(reader["enabled"]);
                            flock.flockmembercount = Convert.ToInt32(reader["flockmembercount"]);
                            flock.homerange = Convert.ToInt32(reader["homerange"]);
                            flock.id = Convert.ToInt32(reader["id"]);
                            flock.iscallforhelp = Convert.ToInt32(reader["iscallforhelp"]);
                            flock.name = Convert.ToString(reader["name"]);
                            flock.note = Convert.ToString(reader["note"]);
                            flock.presenceid = Convert.ToInt32(reader["presenceid"]);
                            flock.respawnmultiplierlow = Convert.ToInt32(reader["respawnmultiplierlow"]);
                            flock.respawnseconds = Convert.ToInt32(reader["respawnseconds"]);
                            flock.spawnoriginX = Convert.ToInt32(reader["spawnoriginX"]);
                            flock.spawnoriginY = Convert.ToInt32(reader["spawnoriginY"]);
                            flock.spawnrangeMax = Convert.ToInt32(reader["spawnrangeMax"]);
                            flock.spawnrangeMin = Convert.ToInt32(reader["spawnrangeMin"]);
                            flock.totalspawncount = Convert.ToInt32(reader["totalspawncount"]);

                            flock.NPCDefinitionName = Convert.ToString(reader["definitionname"]);
                            flock.dBAction = DBAction.UPDATE;
                            list.Add(flock);
                        }

                    }
                }
            }
            return list;
        }


        public string Save(NPCFlockData item)
        {
            string query = "";
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append(@"UPDATE [dbo].[npcflock] SET [name] = @name ,[presenceid] = "+ NPCPresence.IDkey + ", [flockmembercount] = @flockmembercount, [definition] = @definitionID, " +
                    "[spawnoriginX] = @spawnoriginX, [spawnoriginY] = @spawnoriginY ,[spawnrangeMin] = @spawnrangeMin, [spawnrangeMax] = @spawnrangeMax,[respawnseconds] = @respawnseconds, " +
                    "[totalspawncount] = @totalspawncount, [homerange] = @homerange ,[note] = @note, [respawnmultiplierlow] = @respawnmultiplierlow, [enabled] = @enabled, " +
                    "[iscallforhelp] = @iscallforhelp, [behaviorType] = @behaviorType WHERE id="+NPCFlock.IDkey+";");
                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue(NPCFlock.IDkey, item.id);
                command.Parameters.AddWithValue("@name", item.name);
                command.Parameters.AddWithValue(NPCPresence.IDkey, item.presenceid);
                command.Parameters.AddWithValue("@flockmembercount", item.flockmembercount);
                command.Parameters.AddWithValue("@definitionID", item.definition);
                command.Parameters.AddWithValue("@spawnoriginX", item.spawnoriginX);
                command.Parameters.AddWithValue("@spawnoriginY", item.spawnoriginY);
                command.Parameters.AddWithValue("@spawnrangeMin", item.spawnrangeMin);
                command.Parameters.AddWithValue("@spawnrangeMax", item.spawnrangeMax);
                command.Parameters.AddWithValue("@respawnseconds", item.respawnseconds);
                command.Parameters.AddWithValue("@respawnmultiplierlow", item.respawnmultiplierlow);
                command.Parameters.AddWithValue("@totalspawncount", item.totalspawncount);
                command.Parameters.AddWithValue("@homerange", item.homerange);
                command.Parameters.AddWithValue("@note", item.note);
                command.Parameters.AddWithValue("@enabled", item.enabled);
                command.Parameters.AddWithValue("@iscallforhelp", item.iscallforhelp);
                command.Parameters.AddWithValue("@behaviorType", item.behaviorType);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                query = Utilities.parseCommandString(command, new List<string>(new string[] { NPCFlock.IDkey, NPCPresence.IDkey }));
            }
            return query;
        }


        public string Insert(NPCFlockData item)
        {
            string query = "";
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append(@"INSERT INTO[dbo].[npcflock]([name],[presenceid],[flockmembercount],[definition],[spawnoriginX],[spawnoriginY],[spawnrangeMin],[spawnrangeMax],[respawnseconds]
                ,[totalspawncount],[homerange],[note],[respawnmultiplierlow],[enabled],[iscallforhelp],[behaviorType]) VALUES
                (@name, @presenceID, @flockmembercount, @definitionID, @spawnoriginX, @spawnoriginY, @spawnrangeMin, @spawnrangeMax, @respawnseconds, @totalspawncount,
                 @homerange, @note, @respawnmultiplierlow, @enabled, @iscallforhelp, @behaviorType);");
                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@name", item.name);
                command.Parameters.AddWithValue(NPCPresence.IDkey, item.presenceid);
                command.Parameters.AddWithValue("@flockmembercount", item.flockmembercount);
                command.Parameters.AddWithValue("@definitionID", item.definition);
                command.Parameters.AddWithValue("@spawnoriginX", item.spawnoriginX);
                command.Parameters.AddWithValue("@spawnoriginY", item.spawnoriginY);
                command.Parameters.AddWithValue("@spawnrangeMin", item.spawnrangeMin);
                command.Parameters.AddWithValue("@spawnrangeMax", item.spawnrangeMax);
                command.Parameters.AddWithValue("@respawnseconds", item.respawnseconds);
                command.Parameters.AddWithValue("@respawnmultiplierlow", item.respawnmultiplierlow);
                command.Parameters.AddWithValue("@totalspawncount", item.totalspawncount);
                command.Parameters.AddWithValue("@homerange", item.homerange);
                command.Parameters.AddWithValue("@note", item.note);
                command.Parameters.AddWithValue("@enabled", item.enabled);
                command.Parameters.AddWithValue("@iscallforhelp", item.iscallforhelp);
                command.Parameters.AddWithValue("@behaviorType", item.behaviorType);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                query = Utilities.parseCommandString(command, new List<string>(new string[] { NPCPresence.IDkey }));
            }
            return query;
        }

        public string Delete(NPCFlockData data)
        {
            string query = "";
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append(@"" + data.GetLookupStatement() + "\nDELETE FROM [dbo].[npcflock] WHERE id="+NPCFlockData.IDkey+";");
                command.CommandText = sqlCommand.ToString();
                SqlConnection conn = new SqlConnection(this.ConnString);

                command.Parameters.AddWithValue(NPCFlock.IDkey, data.id);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                query = Utilities.parseCommandString(command, new List<string>(new string[] { NPCFlockData.IDkey }));
            }
            return query;

        }


        public static string IDkey = "@flockID";

        public static string GetDeclStatement()
        {
            return "DECLARE " + IDkey + " int;";
        }

        public string GetLookupStatement()
        {
            return "SET " + IDkey + " = (SELECT TOP 1 id from npcflock WHERE [name] = '" + this.name + "' ORDER BY id DESC);";
        }



        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }


    public class NPCPresenceData : INotifyPropertyChanged
    {
        public int id { get; set; }
        public string name { get; set; }
        public int topx { get; set; }
        public int topy { get; set; }
        public int bottomx { get; set; }
        public int bottomy { get; set; }
        public int spawnid { get; set; }
        public int enabled { get; set; }
        public int roaming { get; set; }
        public int roamingrespawnseconds { get; set; }
        public int presencetype { get; set; }
        public int maxrandomflock { get; set; }
        public int randomcenterx { get; set; }
        public int randomcentery { get; set; }
        public int randomradius { get; set; }
        public int dynamiclifetime { get; set; }
        public int isbodypull { get; set; }
        public int isrespawnallowed { get; set; }
        public int safebodypull { get; set; }
        public string note { get; set; }

        public object getNullableInt(int v)
        {
            if (v < 0)
            {
                return (object)DBNull.Value;
            }
            return v;
        }

        public static string IDkey = "@presenceID";

        public static string GetDeclStatement()
        {
            return "DECLARE " + IDkey + " int";
        }

        public string GetLookupStatement()
        {
            return "SET " + IDkey + " = (SELECT TOP 1 id from npcpresence WHERE [name] = '" + this.name + "' ORDER BY id DESC)";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    class NPCPresence : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private int _topx;
        private int _topy;
        private int _bottomx;
        private int _bottomy;
        private int _spawnid;
        private int _enabled;
        private int _roaming;
        private int _roamingrespawnseconds;
        private int _presencetype;
        private int _maxrandomflock;
        private int _randomcenterx;
        private int _randomcentery;
        private int _randomradius;
        private int _dynamiclifetime;
        private int _isbodypull;
        private int _isrespawnallowed;
        private int _safebodypull;
        private string _note;

        public NPCPresence(string connectionString)
        {
            this.ConnString = connectionString;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        #region Fields
        public int id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
                this.OnPropertyChanged("id");
            }
        }
        public string name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
                this.OnPropertyChanged("name");
            }
        }
        public int topx
        {
            get
            {
                return this._topx;
            }
            set
            {
                this._topx = value;
                this.OnPropertyChanged("topx");
            }
        }
        public int topy
        {
            get
            {
                return this._topy;
            }
            set
            {
                this._topy = value;
                this.OnPropertyChanged("topy");
            }
        }
        public int bottomx
        {
            get
            {
                return this._bottomx;
            }
            set
            {
                this._bottomx = value;
                this.OnPropertyChanged("bottomx");
            }
        }
        public int bottomy
        {
            get
            {
                return this._bottomy;
            }
            set
            {
                this._bottomy = value;
                this.OnPropertyChanged("bottomy");
            }
        }
        public int spawnid
        {
            get
            {
                return this._spawnid;
            }
            set
            {
                this._spawnid = value;
                this.OnPropertyChanged("spawnid");
            }
        }
        public int enabled
        {
            get
            {
                return this._enabled;
            }
            set
            {
                this._enabled = value;
                this.OnPropertyChanged("enabled");
            }
        }
        public int roaming
        {
            get
            {
                return this._roaming;
            }
            set
            {
                this._roaming = value;
                this.OnPropertyChanged("roaming");
            }
        }
        public int roamingrespawnseconds
        {
            get
            {
                return this._roamingrespawnseconds;
            }
            set
            {
                this._roamingrespawnseconds = value;
                this.OnPropertyChanged("roamingrespawnseconds");
            }
        }
        public int presencetype
        {
            get
            {
                return this._presencetype;
            }
            set
            {
                this._presencetype = value;
                this.OnPropertyChanged("presencetype");
            }
        }
        public int maxrandomflock
        {
            get
            {
                return this._maxrandomflock;
            }
            set
            {
                this._maxrandomflock = value;
                this.OnPropertyChanged("maxrandomflock");
            }
        }
        public string note
        {
            get
            {
                return this._note;
            }
            set
            {
                this._note = value;
                this.OnPropertyChanged("note");
            }
        }
        public int randomcenterx
        {
            get
            {
                return this._randomcenterx;
            }
            set
            {
                this._randomcenterx = value;
                this.OnPropertyChanged("randomcenterx");
            }
        }
        public int randomcentery
        {
            get
            {
                return this._randomcentery;
            }
            set
            {
                this._randomcentery = value;
                this.OnPropertyChanged("randomcentery");
            }
        }
        public int randomradius
        {
            get
            {
                return this._randomradius;
            }
            set
            {
                this._randomradius = value;
                this.OnPropertyChanged("randomradius");
            }
        }
        public int dynamiclifetime
        {
            get
            {
                return this._dynamiclifetime;
            }
            set
            {
                this._dynamiclifetime = value;
                this.OnPropertyChanged("dynamiclifetime");
            }
        }
        public int isbodypull
        {
            get
            {
                return this._isbodypull;
            }
            set
            {
                this._isbodypull = value;
                this.OnPropertyChanged("isbodypull");
            }
        }
        public int isrespawnallowed
        {
            get
            {
                return this._isrespawnallowed;
            }
            set
            {
                this._isrespawnallowed = value;
                this.OnPropertyChanged("isrespawnallowed");
            }
        }
        public int safebodypull
        {
            get
            {
                return this._safebodypull;
            }
            set
            {
                this._safebodypull = value;
                this.OnPropertyChanged("safebodypull");
            }
        }

        private string ConnString { get; set; }

        #endregion

        public void Clear()
        {
            this.id = 0;
            this.name = "";
            this.topx = 0;
            this.topy = 0;
            this.bottomx = 0;
            this.bottomy = 0;
            this.spawnid = 0;
            this.enabled = 0;
            this.roaming = 0;
            this.roamingrespawnseconds = 0;
            this.presencetype = 0;
            this.maxrandomflock = 0;
            this.note = "";
            this.randomcenterx = 0;
            this.randomcentery = 0;
            this.randomradius = 0;
            this.dynamiclifetime = 0;
            this.isbodypull = 0;
            this.isrespawnallowed = 0;
            this.safebodypull = 0;
        }


        public List<NPCPresenceData> getAll()
        {
            List<NPCPresenceData> list = new List<NPCPresenceData>();
            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append(@"SELECT DISTINCT * FROM [perpetuumsa].[dbo].[npcpresence]");
                    command.CommandText = sqlCommand.ToString();
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NPCPresenceData pres = new NPCPresenceData();
                            pres.id = Convert.ToInt32(reader["id"]);
                            pres.isbodypull = Convert.ToInt32(reader["isbodypull"]);
                            pres.isrespawnallowed = Convert.ToInt32(reader["isrespawnallowed"]);
                            pres.maxrandomflock = Utilities.handleNullableInt(reader["maxrandomflock"]);
                            pres.name = Convert.ToString(reader["name"]);
                            pres.note = Convert.ToString(reader["note"]);
                            pres.presencetype = Convert.ToInt32(reader["presencetype"]);
                            pres.randomcenterx = Utilities.handleNullableInt(reader["randomcenterx"]);
                            pres.randomcentery = Utilities.handleNullableInt(reader["randomcentery"]);
                            pres.randomradius = Utilities.handleNullableInt(reader["randomradius"]);
                            pres.roaming = Convert.ToInt32(reader["roaming"]);
                            pres.roamingrespawnseconds = Convert.ToInt32(reader["roamingrespawnseconds"]);
                            pres.safebodypull = Convert.ToInt32(reader["safebodypull"]);
                            pres.spawnid = Utilities.handleNullableInt(reader["spawnid"]);
                            pres.topx = Convert.ToInt32(reader["topx"]);
                            pres.topy = Convert.ToInt32(reader["topy"]);
                            pres.bottomx = Convert.ToInt32(reader["bottomx"]);
                            pres.bottomy = Convert.ToInt32(reader["bottomy"]);
                            pres.dynamiclifetime = Utilities.handleNullableInt(reader["dynamiclifetime"]);
                            pres.enabled = Convert.ToInt32(reader["enabled"]);
                            list.Add(pres);
                        }
                    }
                }
            }
            return list;
        }

        public string Save(NPCPresenceData item)
        {
            string query = "";
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append(@"UPDATE [dbo].[npcpresence]
                SET [name] = @name,[topx] = @topx,[topy] = @topy,[bottomx] = @bottomx,[bottomy] = @bottomy,[note] = @note,[spawnid] = @spawnid,[enabled] = @enabled,[roaming] = @roaming
                ,[roamingrespawnseconds] = @roamingrespawnseconds,[presencetype] = @presencetype,[maxrandomflock] = @maxrandomflock,[randomcenterx] = @randomcenterx,[randomcentery] = @randomcentery
                ,[randomradius] = @randomradius,[dynamiclifetime] = @dynamiclifetime ,[isbodypull] = @isbodypull,[isrespawnallowed] = @isrespawnallowed,[safebodypull] = @safebodypull
                WHERE id="+NPCPresence.IDkey+";");
                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue(NPCPresence.IDkey, item.id);
                command.Parameters.AddWithValue("@name", item.name);
                command.Parameters.AddWithValue("@note", item.note);
                command.Parameters.AddWithValue("@topx", item.topx);
                command.Parameters.AddWithValue("@topy", item.topy);
                command.Parameters.AddWithValue("@bottomx", item.bottomx);
                command.Parameters.AddWithValue("@bottomy", item.bottomy);
                command.Parameters.AddWithValue("@spawnid", item.getNullableInt(item.spawnid));
                command.Parameters.AddWithValue("@enabled", item.enabled);
                command.Parameters.AddWithValue("@roamingrespawnseconds", item.roamingrespawnseconds);
                command.Parameters.AddWithValue("@roaming", item.roaming);
                command.Parameters.AddWithValue("@presencetype", item.presencetype);
                command.Parameters.AddWithValue("@maxrandomflock", item.getNullableInt(item.maxrandomflock));
                command.Parameters.AddWithValue("@randomcenterx", item.getNullableInt(item.randomcenterx));
                command.Parameters.AddWithValue("@randomcentery", item.getNullableInt(item.randomcentery));
                command.Parameters.AddWithValue("@randomradius", item.getNullableInt(item.randomradius));
                command.Parameters.AddWithValue("@dynamiclifetime", item.getNullableInt(item.dynamiclifetime));
                command.Parameters.AddWithValue("@isbodypull", item.isbodypull);
                command.Parameters.AddWithValue("@isrespawnallowed", item.isrespawnallowed);
                command.Parameters.AddWithValue("@safebodypull", item.safebodypull);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                query = Utilities.parseCommandString(command, new List<string>(new string[] { NPCPresence.IDkey }));
            }
            return query;
        }

        public string Insert(NPCPresenceData item)
        {
            string query = "";
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append(@"INSERT INTO [dbo].[npcpresence] ([name],[topx],[topy],[bottomx],[bottomy],[note],[spawnid],[enabled],[roaming],[roamingrespawnseconds]
                ,[presencetype],[maxrandomflock],[randomcenterx],[randomcentery],[randomradius],[dynamiclifetime],[isbodypull],[isrespawnallowed],[safebodypull])
                VALUES (@name,@topx,@topy,@bottomx,@bottomy,@note,@spawnid,@enabled,@roaming,@roamingrespawnseconds,@presencetype,@maxrandomflock,@randomcenterx
			    ,@randomcentery,@randomradius,@dynamiclifetime,@isbodypull,@isrespawnallowed,@safebodypull);");
                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@name", item.name);
                command.Parameters.AddWithValue("@note", item.note);
                command.Parameters.AddWithValue("@topx", item.topx);
                command.Parameters.AddWithValue("@topy", item.topy);
                command.Parameters.AddWithValue("@bottomx", item.bottomx);
                command.Parameters.AddWithValue("@bottomy", item.bottomy);
                command.Parameters.AddWithValue("@spawnid", item.getNullableInt(item.spawnid));
                command.Parameters.AddWithValue("@enabled", item.enabled);
                command.Parameters.AddWithValue("@roamingrespawnseconds", item.roamingrespawnseconds);
                command.Parameters.AddWithValue("@roaming", item.roaming);
                command.Parameters.AddWithValue("@presencetype", item.presencetype);
                command.Parameters.AddWithValue("@maxrandomflock", item.getNullableInt(item.maxrandomflock));
                command.Parameters.AddWithValue("@randomcenterx", item.getNullableInt(item.randomcenterx));
                command.Parameters.AddWithValue("@randomcentery", item.getNullableInt(item.randomcentery));
                command.Parameters.AddWithValue("@randomradius", item.getNullableInt(item.randomradius));
                command.Parameters.AddWithValue("@dynamiclifetime", item.getNullableInt(item.dynamiclifetime));
                command.Parameters.AddWithValue("@isbodypull", item.isbodypull);
                command.Parameters.AddWithValue("@isrespawnallowed", item.isrespawnallowed);
                command.Parameters.AddWithValue("@safebodypull", item.safebodypull);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                query = Utilities.parseCommandString(command, new List<string>(new string[] { NPCPresence.IDkey }));
            }
            return query;
        }

        public static string IDkey = "@presenceID";

        public static string GetDeclStatement()
        {
            return "DECLARE "+IDkey+" int";
        }

        public string GetLookupStatement()
        {
            return "SET "+ IDkey + " = (SELECT TOP 1 id from npcpresence WHERE [name] = '" + this.name + "' ORDER BY id DESC)";
        }

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}