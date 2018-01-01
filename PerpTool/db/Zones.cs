using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Perptool.db
{
    /// <summary>
    /// Table Class
    /// </summary>
    public class Zones : INotifyPropertyChanged
    {
        /// <summary>
        /// private field
        /// </summary>
        private int privateid;

        /// <summary>
        /// private field
        /// </summary>
        private int privatex;

        /// <summary>
        /// private field
        /// </summary>
        private int privatey;

        /// <summary>
        /// private field
        /// </summary>
        private string privatename;

        /// <summary>
        /// private field
        /// </summary>
        private string privatedescription;

        /// <summary>
        /// private field
        /// </summary>
        private string privatenote;

        /// <summary>
        /// private field
        /// </summary>
        private int privatefertility;

        /// <summary>
        /// private field
        /// </summary>
        private string privatezoneplugin;

        /// <summary>
        /// private field
        /// </summary>
        private string privatezoneip;

        /// <summary>
        /// private field
        /// </summary>
        private int privatezoneport;

        /// <summary>
        /// private field
        /// </summary>
        private int privateisinstance;

        /// <summary>
        /// private field
        /// </summary>
        private int privateenabled;

        /// <summary>
        /// private field
        /// </summary>
        private int privatespawnid;

        /// <summary>
        /// private field
        /// </summary>
        private int privateplantruleset;

        /// <summary>
        /// private field
        /// </summary>
        private int privateprotected;

        /// <summary>
        /// private field
        /// </summary>
        private int privateraceid;

        /// <summary>
        /// private field
        /// </summary>
        private int privatewidth;

        /// <summary>
        /// private field
        /// </summary>
        private int privateheight;

        /// <summary>
        /// private field
        /// </summary>
        private int privateterraformable;

        /// <summary>
        /// private field
        /// </summary>
        private int privatezonetype;

        /// <summary>
        /// private field
        /// </summary>
        private int privatesparkcost;

        /// <summary>
        /// private field
        /// </summary>
        private int privatemaxdockingbase;

        /// <summary>
        /// private field
        /// </summary>
        private int privatesleeping;

        /// <summary>
        /// private field
        /// </summary>
        private decimal privateplantaltitudescale;

        /// <summary>
        /// private field
        /// </summary>
        private string privatehost;

        /// <summary>
        /// private field
        /// </summary>
        private int privateactive;

        /// <summary>
        /// private dataset
        /// </summary>
        private DataSet privateDSzones;

        /// <summary>
        /// Initializes a new instance of the <see cref='zonesTbl' /> class.
        /// </summary>
        /// <param name='connectionString'>pass the connection string to the database</param>
        public Zones(string connectionString)
        {
             this.ConnString = connectionString;
        }

        /// <summary>
        /// Event handler for properties
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets public DataSet for this table
        /// </summary>
        public DataSet DSzones { get; set; }

        #region Fields
        /// <summary>
        /// Gets or sets public field id
        /// </summary>
        public int id
        {
            get
            {
                return this.privateid;
            }

            set
            {
                this.privateid = value;
                this.OnPropertyChanged("id");
            }
        }

        /// <summary>
        /// Gets or sets public field x
        /// </summary>
        public int x
        {
            get
            {
                return this.privatex;
            }

            set
            {
                this.privatex = value;
                this.OnPropertyChanged("x");
            }
        }

        /// <summary>
        /// Gets or sets public field y
        /// </summary>
        public int y
        {
            get
            {
                return this.privatey;
            }

            set
            {
                this.privatey = value;
                this.OnPropertyChanged("y");
            }
        }

        /// <summary>
        /// Gets or sets public field name
        /// </summary>
        public string name
        {
            get
            {
                return this.privatename;
            }

            set
            {
                this.privatename = value;
                this.OnPropertyChanged("name");
            }
        }

        /// <summary>
        /// Gets or sets public field description
        /// </summary>
        public string description
        {
            get
            {
                return this.privatedescription;
            }

            set
            {
                this.privatedescription = value;
                this.OnPropertyChanged("description");
            }
        }

        /// <summary>
        /// Gets or sets public field note
        /// </summary>
        public string note
        {
            get
            {
                return this.privatenote;
            }

            set
            {
                this.privatenote = value;
                this.OnPropertyChanged("note");
            }
        }

        /// <summary>
        /// Gets or sets public field fertility
        /// </summary>
        public int fertility
        {
            get
            {
                return this.privatefertility;
            }

            set
            {
                this.privatefertility = value;
                this.OnPropertyChanged("fertility");
            }
        }

        /// <summary>
        /// Gets or sets public field zoneplugin
        /// </summary>
        public string zoneplugin
        {
            get
            {
                return this.privatezoneplugin;
            }

            set
            {
                this.privatezoneplugin = value;
                this.OnPropertyChanged("zoneplugin");
            }
        }

        /// <summary>
        /// Gets or sets public field zoneip
        /// </summary>
        public string zoneip
        {
            get
            {
                return this.privatezoneip;
            }

            set
            {
                this.privatezoneip = value;
                this.OnPropertyChanged("zoneip");
            }
        }

        /// <summary>
        /// Gets or sets public field zoneport
        /// </summary>
        public int zoneport
        {
            get
            {
                return this.privatezoneport;
            }

            set
            {
                this.privatezoneport = value;
                this.OnPropertyChanged("zoneport");
            }
        }

        /// <summary>
        /// Gets or sets public field isinstance
        /// </summary>
        public int isinstance
        {
            get
            {
                return this.privateisinstance;
            }

            set
            {
                this.privateisinstance = value;
                this.OnPropertyChanged("isinstance");
            }
        }

        /// <summary>
        /// Gets or sets public field enabled
        /// </summary>
        public int enabled
        {
            get
            {
                return this.privateenabled;
            }

            set
            {
                this.privateenabled = value;
                this.OnPropertyChanged("enabled");
            }
        }

        /// <summary>
        /// Gets or sets public field spawnid
        /// </summary>
        public int spawnid
        {
            get
            {
                return this.privatespawnid;
            }

            set
            {
                this.privatespawnid = value;
                this.OnPropertyChanged("spawnid");
            }
        }

        /// <summary>
        /// Gets or sets public field plantruleset
        /// </summary>
        public int plantruleset
        {
            get
            {
                return this.privateplantruleset;
            }

            set
            {
                this.privateplantruleset = value;
                this.OnPropertyChanged("plantruleset");
            }
        }

        /// <summary>
        /// Gets or sets public field protected
        /// </summary>
        public int pprotected
        {
            get
            {
                return this.privateprotected;
            }

            set
            {
                this.privateprotected = value;
                this.OnPropertyChanged("protected");
            }
        }

        /// <summary>
        /// Gets or sets public field raceid
        /// </summary>
        public int raceid
        {
            get
            {
                return this.privateraceid;
            }

            set
            {
                this.privateraceid = value;
                this.OnPropertyChanged("raceid");
            }
        }

        /// <summary>
        /// Gets or sets public field width
        /// </summary>
        public int width
        {
            get
            {
                return this.privatewidth;
            }

            set
            {
                this.privatewidth = value;
                this.OnPropertyChanged("width");
            }
        }

        /// <summary>
        /// Gets or sets public field height
        /// </summary>
        public int height
        {
            get
            {
                return this.privateheight;
            }

            set
            {
                this.privateheight = value;
                this.OnPropertyChanged("height");
            }
        }

        /// <summary>
        /// Gets or sets public field terraformable
        /// </summary>
        public int terraformable
        {
            get
            {
                return this.privateterraformable;
            }

            set
            {
                this.privateterraformable = value;
                this.OnPropertyChanged("terraformable");
            }
        }

        /// <summary>
        /// Gets or sets public field zonetype
        /// </summary>
        public int zonetype
        {
            get
            {
                return this.privatezonetype;
            }

            set
            {
                this.privatezonetype = value;
                this.OnPropertyChanged("zonetype");
            }
        }

        /// <summary>
        /// Gets or sets public field sparkcost
        /// </summary>
        public int sparkcost
        {
            get
            {
                return this.privatesparkcost;
            }

            set
            {
                this.privatesparkcost = value;
                this.OnPropertyChanged("sparkcost");
            }
        }

        /// <summary>
        /// Gets or sets public field maxdockingbase
        /// </summary>
        public int maxdockingbase
        {
            get
            {
                return this.privatemaxdockingbase;
            }

            set
            {
                this.privatemaxdockingbase = value;
                this.OnPropertyChanged("maxdockingbase");
            }
        }

        /// <summary>
        /// Gets or sets public field sleeping
        /// </summary>
        public int sleeping
        {
            get
            {
                return this.privatesleeping;
            }

            set
            {
                this.privatesleeping = value;
                this.OnPropertyChanged("sleeping");
            }
        }

        /// <summary>
        /// Gets or sets public field plantaltitudescale
        /// </summary>
        public decimal plantaltitudescale
        {
            get
            {
                return this.privateplantaltitudescale;
            }

            set
            {
                this.privateplantaltitudescale = value;
                this.OnPropertyChanged("plantaltitudescale");
            }
        }

        /// <summary>
        /// Gets or sets public field host
        /// </summary>
        public string host
        {
            get
            {
                return this.privatehost;
            }

            set
            {
                this.privatehost = value;
                this.OnPropertyChanged("host");
            }
        }

        /// <summary>
        /// Gets or sets public field active
        /// </summary>
        public int active
        {
            get
            {
                return this.privateactive;
            }

            set
            {
                this.privateactive = value;
                this.OnPropertyChanged("active");
            }
        }

        /// <summary>
        /// Gets or sets connection string
        /// </summary>
        private string ConnString { get; set; }

        public string ConcatZoneIDName
        {
            get
            {
                return string.Format("{0} - {1} - {2}", this.id, this.name, this.note);
            }
        }

        #endregion

        // <summary>
        // Clears and sets defaults on variables.
        // </summary>
        public void Clear()
        {
            this.id = 0;
            this.x = 0;
            this.y = 0;
            this.name = string.Empty;
            this.description = string.Empty;
            this.note = string.Empty;
            this.fertility = 0;
            this.zoneplugin = string.Empty;
            this.zoneip = string.Empty;
            this.zoneport = 0;
            this.isinstance = 0;
            this.enabled = 0;
            this.spawnid = 0;
            this.plantruleset = 0;
            this.pprotected = 0;
            this.raceid = 0;
            this.width = 0;
            this.height = 0;
            this.terraformable = 0;
            this.zonetype = 0;
            this.sparkcost = 0;
            this.maxdockingbase = 0;
            this.sleeping = 0;
            this.plantaltitudescale = 0;
            this.host = string.Empty;
            this.active = 0;
        }

        /// <summary>
        /// saves a new record
        /// </summary>
        public void SaveNewRecord()
        {
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("Insert into zones ");
                sqlCommand.Append("(id, x, y, name, description, note, fertility,  zoneplugin ,  zoneip ,  zoneport ,  isinstance ,  enabled ,  spawnid ,  plantruleset ,  protected ,  raceid ,  width ,  height ,  terraformable ,  zonetype ,  sparkcost ,  maxdockingbase ,  sleeping ,  plantaltitudescale ,  host ,  active ) ");
                sqlCommand.Append(" Values ");
                sqlCommand.Append("(@id, @x, @y, @name, @description, @note, @fertility, @zoneplugin, @zoneip, @zoneport, @isinstance, @enabled, @spawnid, @plantruleset, @protected, @raceid, @width, @height, @terraformable, @zonetype, @sparkcost, @maxdockingbase, @sleeping, @plantaltitudescale, @host, @active) ");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@id", this.id);
                command.Parameters.AddWithValue("@x", this.x);
                command.Parameters.AddWithValue("@y", this.y);
                command.Parameters.AddWithValue("@name", this.name);
                command.Parameters.AddWithValue("@description", this.description);
                command.Parameters.AddWithValue("@note", this.note);
                command.Parameters.AddWithValue("@fertility", this.fertility);
                command.Parameters.AddWithValue("@zoneplugin", this.zoneplugin);
                command.Parameters.AddWithValue("@zoneip", this.zoneip);
                command.Parameters.AddWithValue("@zoneport", this.zoneport);
                command.Parameters.AddWithValue("@isinstance", this.isinstance);
                command.Parameters.AddWithValue("@enabled", this.enabled);
                command.Parameters.AddWithValue("@spawnid", this.spawnid);
                command.Parameters.AddWithValue("@plantruleset", this.plantruleset);
                command.Parameters.AddWithValue("@protected", this.pprotected);
                command.Parameters.AddWithValue("@raceid", this.raceid);
                command.Parameters.AddWithValue("@width", this.width);
                command.Parameters.AddWithValue("@height", this.height);
                command.Parameters.AddWithValue("@terraformable", this.terraformable);
                command.Parameters.AddWithValue("@zonetype", this.zonetype);
                command.Parameters.AddWithValue("@sparkcost", this.sparkcost);
                command.Parameters.AddWithValue("@maxdockingbase", this.maxdockingbase);
                command.Parameters.AddWithValue("@sleeping", this.sleeping);
                command.Parameters.AddWithValue("@plantaltitudescale", this.plantaltitudescale);
                command.Parameters.AddWithValue("@host", this.host);
                command.Parameters.AddWithValue("@active", this.active);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        /// <summary>
        /// saves existing record
        /// </summary>
        public void Save()
        {
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("UPDATE zones Set  x = @x,  y = @y,  name = @name,  description = @description,  note = @note,  fertility = @fertility,  zoneplugin = @zoneplugin,  zoneip = @zoneip,  zoneport = @zoneport,  isinstance = @isinstance,  enabled = @enabled,  spawnid = @spawnid,  plantruleset = @plantruleset,  protected =@protected,  raceid =@raceid,  width =@width,  height =@height,  terraformable =@terraformable,  zonetype =@zonetype,  sparkcost =@sparkcost,  maxdockingbase =@maxdockingbase,  sleeping =@sleeping,  plantaltitudescale =@plantaltitudescale,  host =@host,  active =@active where id=@id");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@id", this.id);
                command.Parameters.AddWithValue("@x", this.x);
                command.Parameters.AddWithValue("@y", this.y);
                command.Parameters.AddWithValue("@name", this.name);
                command.Parameters.AddWithValue("@description", this.description);
                command.Parameters.AddWithValue("@note", this.note);
                command.Parameters.AddWithValue("@fertility", this.fertility);
                command.Parameters.AddWithValue("@zoneplugin", this.zoneplugin);
                command.Parameters.AddWithValue("@zoneip", this.zoneip);
                command.Parameters.AddWithValue("@zoneport", this.zoneport);
                command.Parameters.AddWithValue("@isinstance", this.isinstance);
                command.Parameters.AddWithValue("@enabled", this.enabled);
                command.Parameters.AddWithValue("@spawnid", this.spawnid);
                command.Parameters.AddWithValue("@plantruleset", this.plantruleset);
                command.Parameters.AddWithValue("@protected", this.pprotected);
                command.Parameters.AddWithValue("@raceid", this.raceid);
                command.Parameters.AddWithValue("@width", this.width);
                command.Parameters.AddWithValue("@height", this.height);
                command.Parameters.AddWithValue("@terraformable", this.terraformable);
                command.Parameters.AddWithValue("@zonetype", this.zonetype);
                command.Parameters.AddWithValue("@sparkcost", this.sparkcost);
                command.Parameters.AddWithValue("@maxdockingbase", this.maxdockingbase);
                command.Parameters.AddWithValue("@sleeping", this.sleeping);
                command.Parameters.AddWithValue("@plantaltitudescale", this.plantaltitudescale);
                command.Parameters.AddWithValue("@host", this.host);
                command.Parameters.AddWithValue("@active", this.active);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        // <summary>
        // gets a record by its record id
        // </summary>
        // <param name='recnum'>record number</param>
        public List<Zones> GetAllZones()
        {
            List<Zones> list = new List<Zones>();

            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("SELECT * from zones");
                    command.CommandText = sqlCommand.ToString();
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Zones tmp = new Zones(this.ConnString);

                            tmp.id = Convert.ToInt32(reader["id"]);
                            tmp.x = Convert.ToInt32(reader["x"]);
                            tmp.y = Convert.ToInt32(reader["y"]);
                            tmp.name = Convert.ToString(reader["name"]);
                            tmp.description = Convert.ToString(reader["description"]);
                            tmp.note = Convert.ToString(reader["note"]);
                            tmp.fertility = Convert.ToInt32(reader["fertility"]);
                            tmp.zoneplugin = Convert.ToString(reader["zoneplugin"]);
                            tmp.zoneip = Convert.ToString(reader["zoneip"]);
                            tmp.zoneport = Convert.ToInt32(reader["zoneport"]);
                            tmp.isinstance = Convert.ToInt32(reader["isinstance"]);
                            tmp.enabled = Convert.ToInt32(reader["enabled"]);
                            if (reader["spawnid"] != DBNull.Value) { tmp.spawnid = Convert.ToInt32(reader["spawnid"]); }
                            tmp.plantruleset = Convert.ToInt32(reader["plantruleset"]);
                            tmp.pprotected = Convert.ToInt32(reader["protected"]);
                            tmp.raceid = Convert.ToInt32(reader["raceid"]);
                            tmp.width = Convert.ToInt32(reader["width"]);
                            tmp.height = Convert.ToInt32(reader["height"]);
                            tmp.terraformable = Convert.ToInt32(reader["terraformable"]);
                            tmp.zonetype = Convert.ToInt32(reader["zonetype"]);
                            tmp.sparkcost = Convert.ToInt32(reader["sparkcost"]);
                            tmp.maxdockingbase = Convert.ToInt32(reader["maxdockingbase"]);
                            tmp.sleeping = Convert.ToInt32(reader["sleeping"]);
                            tmp.plantaltitudescale = Convert.ToDecimal(reader["plantaltitudescale"]);
                            tmp.host = Convert.ToString(reader["host"]);
                            tmp.active = Convert.ToInt32(reader["active"]);

                            list.Add(tmp);
                        }
                    }
                    conn.Dispose();
                }
            }
            return list;
        }


        /// <summary>
        /// fires when properties are set.
        /// </summary>
        /// <param name='name'>name of property being changed</param>
        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

