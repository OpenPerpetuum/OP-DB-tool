using Perpetuum.GenXY;
using PerpTool.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perptool.db
{

    // THI IS SHIT. don't hate!

    public class EntityItems : INotifyPropertyChanged
    {
        public int Definition { get; set; }
        public string Name { get; set; }
        public EntityOptions Options { get; set; }
        public decimal Volume { get; set; }
        public decimal Mass { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }


    public class SlotFlagWrapper : INotifyPropertyChanged
    {
        private bool _head;
        private bool _chassis;
        private bool _leg;
        private bool _small;
        private bool _medium;
        private bool _large;
        private bool _industrial;
        private bool _ew_Eng;
        private bool _turret;
        private bool _missile;
        private bool _melee;
        private SlotFlags _flag;

        public SlotFlagWrapper(int index, SlotFlags slotFlags)
        {
            this.Index = index;
            this.SlotFlag = slotFlags;
            this.setFlags();
        }
        public int Index { get; set; }
        public SlotFlags SlotFlag { get; set; }
        public bool Head
        {
            get { return this._head; }
            set
            {
                OnPropertyChanged("Head");
                this._head = value;
                updateFlag(SlotFlags.head, value);
            }
        }
        public bool Chassis
        {
            get { return this._chassis; }
            set
            {
                OnPropertyChanged("Chassis");
                this._chassis = value;
                updateFlag(SlotFlags.chassis, value);
            }
        }
        public bool Leg
        {
            get { return this._leg; }
            set
            {
                OnPropertyChanged("Leg");
                this._leg = value;
                updateFlag(SlotFlags.leg, value);
            }
        }
        public bool Small
        {
            get { return this._small; }
            set
            {
                OnPropertyChanged("Small");
                this._small = value;
                updateFlag(SlotFlags.small, value);
            }
        }
        public bool Medium
        {
            get { return this._medium; }
            set
            {
                OnPropertyChanged("Medium");
                this._medium = value;
                updateFlag(SlotFlags.medium, value);
            }
        }
        public bool Large
        {
            get { return this._large; }
            set
            {
                OnPropertyChanged("Large");
                this._large = value;
                updateFlag(SlotFlags.large, value);
            }
        }
        public bool Industrial
        {
            get { return this._industrial; }
            set
            {
                OnPropertyChanged("Industrial");
                this._industrial = value;
                updateFlag(SlotFlags.industrial, value);
            }
        }
        public bool EW_Eng
        {
            get { return this._ew_Eng; }
            set
            {
                OnPropertyChanged("EW_Eng");
                this._ew_Eng = value;
                updateFlag(SlotFlags.ew_and_engineering, value);
            }
        }
        public bool Turret
        {
            get { return this._turret; }
            set
            {
                OnPropertyChanged("Turret");
                this._turret = value;
                updateFlag(SlotFlags.turret, value);
            }
        }
        public bool Missile
        {
            get { return this._missile; }
            set
            {
                OnPropertyChanged("Missile");
                this._missile = value;
                updateFlag(SlotFlags.missile, value);
            }
        }
        public bool Melee
        {
            get { return this._melee; }
            set
            {
                OnPropertyChanged("Melee");
                this._melee = value;
                updateFlag(SlotFlags.melee, value);
            }
        }

        private void updateFlag(SlotFlags attribute, bool isAdd)
        {
            if (isAdd)
            {
                this.addFlag(attribute);
            }
            else
            {
                this.removeFlag(attribute);
            }
        }

        private void addFlag(SlotFlags flag)
        {
            this.SlotFlag |= flag;
        }

        private void removeFlag(SlotFlags flag)
        {
            this.SlotFlag &= ~flag;
        }

        private void setFlags()
        {
            this.Head = SlotFlag.HasFlag(SlotFlags.head);
            this.Chassis = SlotFlag.HasFlag(SlotFlags.chassis);
            this.Leg = SlotFlag.HasFlag(SlotFlags.leg);
            this.Small = SlotFlag.HasFlag(SlotFlags.small);
            this.Medium = SlotFlag.HasFlag(SlotFlags.medium);
            this.Large = SlotFlag.HasFlag(SlotFlags.large);
            this.Industrial = SlotFlag.HasFlag(SlotFlags.industrial);
            this.EW_Eng = SlotFlag.HasFlag(SlotFlags.ew_and_engineering);
            this.Turret = SlotFlag.HasFlag(SlotFlags.turret);
            this.Missile = SlotFlag.HasFlag(SlotFlags.missile);
            this.Melee = SlotFlag.HasFlag(SlotFlags.melee);
        }

        public SlotFlags getFlags()
        {
            System.Console.WriteLine(SlotFlag);
            SlotFlag = 0;
            this.updateFlag(SlotFlags.head, this.Head);
            this.updateFlag(SlotFlags.chassis, this.Chassis);
            this.updateFlag(SlotFlags.leg, this.Leg);
            this.updateFlag(SlotFlags.small, this.Small);
            this.updateFlag(SlotFlags.medium, this.Medium);
            this.updateFlag(SlotFlags.large, this.Large);
            this.updateFlag(SlotFlags.industrial, this.Industrial);
            this.updateFlag(SlotFlags.ew_and_engineering, this.EW_Eng);
            this.updateFlag(SlotFlags.turret, this.Turret);
            this.updateFlag(SlotFlags.missile, this.Missile);
            this.updateFlag(SlotFlags.melee, this.Melee);
            System.Console.WriteLine(SlotFlag);
            return SlotFlag;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class EntityOptions : INotifyPropertyChanged
    {
        #region fields
        private int pHead;
        private int pChassis;
        private int pLeg;
        private int pInventory;
        private SlotFlags[] pslotFlags;
        private decimal pheight;
        private int pmax_locked_targets;
        private decimal pmax_targeting_range;
        private decimal psensor_strength;
        private decimal pcpu;
        private SlotFlags pmoduleFlag;
        private int pammoCapacity;
        private long pammoType;

        public SlotFlags[] getSlotArrary()
        {
            SlotFlags[] arr = new SlotFlags[Slots.Count];
            for (int i = 0; i < Slots.Count; i++)
            {
                arr[i] = Slots[i].getFlags();
            }
            return arr;
        }

        private ObservableCollection<SlotFlagWrapper> _pslots;
        public ObservableCollection<SlotFlagWrapper> Slots
        {
            get
            {
                return this._pslots;
            }
            set
            {
                this._pslots = value;
                OnPropertyChanged("Slots");
            }
        }

        public int Head
        {
            get
            {
                return this.pHead;
            }
            set
            {
                this.pHead = value;
                OnPropertyChanged("Head");
            }
        }
        public int Chassis
        {
            get
            {
                return this.pChassis;
            }
            set
            {
                this.pChassis = value;
                OnPropertyChanged("Chassis");
            }
        }
        public int Leg
        {
            get
            {
                return this.pLeg;
            }
            set
            {
                this.pLeg = value;
                OnPropertyChanged("Leg");
            }
        }
        public int Inventory
        {
            get
            {
                return this.pInventory;
            }
            set
            {
                this.pInventory = value;
                OnPropertyChanged("Inventory");
            }
        }
        public SlotFlags[] slotFlags
        {
            get
            {
                return getSlotArrary();
            }
            set
            {
                this.pslotFlags = value;
                this.Slots = new ObservableCollection<SlotFlagWrapper>();
                for (int i = 0; i < this.pslotFlags.Length; i++)
                {
                    this.Slots.Add(new SlotFlagWrapper(i, pslotFlags[i]));
                }
                OnPropertyChanged("slotFlags");
            }
        }
        public decimal height
        {
            get
            {
                return this.pheight;
            }
            set
            {
                this.pheight = value;
                OnPropertyChanged("height");
            }
        }
        public int max_locked_targets
        {
            get
            {
                return this.pmax_locked_targets;
            }
            set
            {
                this.pmax_locked_targets = value;
                OnPropertyChanged("max_locked_targets");
            }
        }
        public decimal max_targeting_range
        {
            get
            {
                return this.pmax_targeting_range;
            }
            set
            {
                this.pmax_targeting_range = value;
                OnPropertyChanged("max_targeting_range");
            }
        }
        public decimal sensor_strength
        {
            get
            {
                return this.psensor_strength;
            }
            set
            {
                this.psensor_strength = value;
                OnPropertyChanged("sensor_strength");
            }
        }
        public decimal cpu
        {
            get
            {
                return this.pcpu;
            }
            set
            {
                this.pcpu = value;
                OnPropertyChanged("cpu");
            }
        }
        public SlotFlags moduleFlag
        {
            get
            {
                return this.pmoduleFlag;
            }
            set
            {
                this.pmoduleFlag = value;
                OnPropertyChanged("moduleFlag");
            }
        }
        public int ammoCapacity
        {
            get
            {
                return this.pammoCapacity;
            }
            set
            {
                this.pammoCapacity = value;
                OnPropertyChanged("ammoCapacity");
            }
        }
        public long ammoType
        {
            get
            {
                return this.pammoType;
            }
            set
            {
                this.pammoType = value;
                OnPropertyChanged("ammoType");
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (this.Head > 0)
            {
                dictionary["head"] = this.Head;
            }
            if (this.Chassis > 0)
            {
                dictionary["chassis"] = this.Chassis;
            }
            if (this.Leg > 0)
            {
                dictionary["leg"] = this.Leg;
            }
            if (this.Inventory > 0)
            {
                dictionary["container"] = this.Inventory;
            }
            if (this.slotFlags != null && this.Slots.Count > 0)
            {
                dictionary["slotFlags"] = getSlotArrary();
            }
            if (this.moduleFlag > 0)
            {
                dictionary["moduleFlag"] = this.moduleFlag;
            }
            if (this.ammoCapacity > 0)
            {
                dictionary["ammoCapacity"] = this.ammoCapacity;
            }
            if (this.ammoCapacity > 0)
            {
                dictionary["ammoCapacity"] = this.ammoCapacity;
            }
            if (this.ammoType > 0)
            {
                dictionary["ammoType"] = this.ammoType;
            }
            return dictionary;
        }

        public string ToGenXY()
        {
            Dictionary<string, object> d = this.ToDictionary();
            if (d.Count > 0)
            {
                return GenxyConverter.Serialize(d);
            }
            return "";

        }
    }


    /// <summary>
    /// Table Class
    /// </summary>
    public class EntityDefaults : INotifyPropertyChanged
    {
        #region privates
        /// <summary>
        /// private field
        /// </summary>
        private int privatedefinition;

        /// <summary>
        /// private field
        /// </summary>
        private string privatedefinitionname;

        /// <summary>
        /// private field
        /// </summary>
        private int privatequantity;

        /// <summary>
        /// private field
        /// </summary>
        private Int64 privateattributeflags;

        /// <summary>
        /// private field
        /// </summary>
        private Int64 privatecategoryflags;

        /// <summary>
        /// private field
        /// </summary>
        private EntityOptions privateoptions;

        /// <summary>
        /// private field
        /// </summary>
        private string privatenote;

        /// <summary>
        /// private field
        /// </summary>
        private int privateenabled;

        /// <summary>
        /// private field
        /// </summary>
        private decimal privatevolume;

        /// <summary>
        /// private field
        /// </summary>
        private decimal privatemass;

        /// <summary>
        /// private field
        /// </summary>
        private string privatehidden;

        /// <summary>
        /// private field
        /// </summary>
        private decimal privatehealth;

        /// <summary>
        /// private field
        /// </summary>
        private string privatedescriptiontoken;

        /// <summary>
        /// private field
        /// </summary>
        private int privatepurchasable;

        /// <summary>
        /// private field
        /// </summary>
        private int privatetiertype;

        /// <summary>
        /// private field
        /// </summary>
        private int privatetierlevel;
        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref='entitydefaultsTbl' /> class.
        /// </summary>
        /// <param name='connectionString'>pass the connection string to the database</param>
        public EntityDefaults(string connectionString)
        {
            this.ConnString = connectionString;
        }

        //Constructor overload to get Entity on creation
        public EntityDefaults(string connString, int definition)
        {
            this.ConnString = connString;
            this.GetById(definition);
        }

        /// <summary>
        /// Event handler for properties
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #region Fields
        /// <summary>
        /// Gets or sets public field definition
        /// </summary>
        public int definition
        {
            get
            {
                return this.privatedefinition;
            }

            set
            {
                this.privatedefinition = value;
                this.OnPropertyChanged("definition");
            }
        }

        /// <summary>
        /// Gets or sets public field definitionname
        /// </summary>
        public string definitionname
        {
            get
            {
                return this.privatedefinitionname;
            }

            set
            {
                this.privatedefinitionname = value;
                this.OnPropertyChanged("definitionname");
            }
        }

        /// <summary>
        /// Gets or sets public field quantity
        /// </summary>
        public int quantity
        {
            get
            {
                return this.privatequantity;
            }

            set
            {
                this.privatequantity = value;
                this.OnPropertyChanged("quantity");
            }
        }

        /// <summary>
        /// Gets or sets public field attributeflags
        /// </summary>
        public Int64 attributeflags
        {
            get
            {
                return this.privateattributeflags;
            }

            set
            {
                this.privateattributeflags = value;
                this.OnPropertyChanged("attributeflags");
            }
        }

        /// <summary>
        /// Gets or sets public field categoryflags
        /// </summary>
        public Int64 categoryflags
        {
            get
            {
                return this.privatecategoryflags;
            }

            set
            {
                this.privatecategoryflags = value;
                this.OnPropertyChanged("categoryflags");
            }
        }

        /// <summary>
        /// Gets or sets public field options
        /// </summary>
        public EntityOptions options
        {
            get
            {
                return this.privateoptions;
            }

            set
            {
                this.privateoptions = value;
                this.OnPropertyChanged("options");
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
        /// Gets or sets public field volume
        /// </summary>
        public decimal volume
        {
            get
            {
                return this.privatevolume;
            }

            set
            {
                this.privatevolume = value;
                this.OnPropertyChanged("volume");
            }
        }

        /// <summary>
        /// Gets or sets public field mass
        /// </summary>
        public decimal mass
        {
            get
            {
                return this.privatemass;
            }

            set
            {
                this.privatemass = value;
                this.OnPropertyChanged("mass");
            }
        }

        /// <summary>
        /// Gets or sets public field hidden
        /// </summary>
        public string hidden
        {
            get
            {
                return this.privatehidden;
            }

            set
            {
                this.privatehidden = value;
                this.OnPropertyChanged("hidden");
            }
        }

        /// <summary>
        /// Gets or sets public field health
        /// </summary>
        public decimal health
        {
            get
            {
                return this.privatehealth;
            }

            set
            {
                this.privatehealth = value;
                this.OnPropertyChanged("health");
            }
        }

        /// <summary>
        /// Gets or sets public field descriptiontoken
        /// </summary>
        public string descriptiontoken
        {
            get
            {
                return this.privatedescriptiontoken;
            }

            set
            {
                this.privatedescriptiontoken = value;
                this.OnPropertyChanged("descriptiontoken");
            }
        }

        /// <summary>
        /// Gets or sets public field purchasable
        /// </summary>
        public int purchasable
        {
            get
            {
                return this.privatepurchasable;
            }

            set
            {
                this.privatepurchasable = value;
                this.OnPropertyChanged("purchasable");
            }
        }

        /// <summary>
        /// Gets or sets public field tiertype
        /// </summary>
        public int tiertype
        {
            get
            {
                return this.privatetiertype;
            }

            set
            {
                this.privatetiertype = value;
                this.OnPropertyChanged("tiertype");
            }
        }

        /// <summary>
        /// Gets or sets public field tierlevel
        /// </summary>
        public int tierlevel
        {
            get
            {
                return this.privatetierlevel;
            }

            set
            {
                this.privatetierlevel = value;
                this.OnPropertyChanged("tierlevel");
            }
        }

        /// <summary>
        /// Gets or sets connection string
        /// </summary>
        private string ConnString { get; set; }

        #endregion

        /// <summary>
        /// Clears and sets defaults on variables.
        /// </summary>
        public void Clear()
        {
            this.definition = 0;
            this.definitionname = string.Empty;
            this.quantity = 0;
            this.attributeflags = 0;
            this.categoryflags = 0;
            this.options = null;
            this.note = string.Empty;
            this.enabled = 0;
            this.volume = 0;
            this.mass = 0;
            this.hidden = string.Empty;
            this.health = 0;
            this.descriptiontoken = string.Empty;
            this.purchasable = 0;
            this.tiertype = 0;
            this.tierlevel = 0;
        }

        /// <summary>
        /// saves a new record
        /// </summary>
        public string SaveNewRecord()
        {
            String query = "";
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append(@"INSERT INTO entitydefaults ( definitionname ,  quantity ,  attributeflags ,  categoryflags ,  options ,  note ,  enabled ,  volume ,  mass ,  hidden , 
                health ,  descriptiontoken ,  purchasable ,  tiertype ,  tierlevel ) 
                VALUES ( @defname, @quantity, @attributeflags, @categoryflags, @options, @note, @enabled, @volume, @mass, @hidden, @health, @descriptiontoken, @purchasable, @tiertype, @tierlevel); ");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@defname", this.definitionname);
                command.Parameters.AddWithValue("@quantity", this.quantity);
                command.Parameters.AddWithValue("@attributeflags", this.attributeflags);
                command.Parameters.AddWithValue("@categoryflags", this.categoryflags);
                command.Parameters.AddWithValue("@options", this.options.ToGenXY());
                command.Parameters.AddWithValue("@note", this.note);
                command.Parameters.AddWithValue("@enabled", this.enabled);
                command.Parameters.AddWithValue("@volume", this.volume);
                command.Parameters.AddWithValue("@mass", this.mass);
                command.Parameters.AddWithValue("@hidden", this.hidden);
                command.Parameters.AddWithValue("@health", this.health);
                command.Parameters.AddWithValue("@descriptiontoken", this.descriptiontoken);
                command.Parameters.AddWithValue("@purchasable", this.purchasable);
                command.Parameters.AddWithValue("@tiertype", this.tiertype);
                command.Parameters.AddWithValue("@tierlevel", this.tierlevel);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                if (this.note == null)
                {
                    command.Parameters.AddWithValue("@note", string.Empty);
                }
                else
                {
                    command.Parameters.AddWithValue("@note", this.note);
                }
                query = command.CommandText;
                foreach (SqlParameter p in command.Parameters)
                {
                    if (SqlDbType.NVarChar.Equals(p.SqlDbType) || SqlDbType.VarChar.Equals(p.SqlDbType))
                    {
                        query = query.Replace(p.ParameterName, "'" + p.Value.ToString() + "'");
                    }
                    else
                    {
                        query = query.Replace(p.ParameterName, p.Value.ToString());
                    }
                }
            }
            return query;
        }



        /// <summary>
        /// saves existing record
        /// </summary>
        public string Save()
        {
            string query = "";
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("UPDATE entitydefaults Set definitionname=@defname, quantity=@quantity, attributeflags=@attributeflags, categoryflags=@categoryflags, options=@options, note=@note, enabled=@enabled, volume=@volume, mass=@mass, hidden=@hidden, health=@health, descriptiontoken=@descriptiontoken, purchasable=@purchasable, tiertype=@tiertype, tierlevel=@tierlevel where definition=@definition;");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@definition", this.definition);
                command.Parameters.AddWithValue("@defname", this.definitionname);
                command.Parameters.AddWithValue("@quantity", this.quantity);
                command.Parameters.AddWithValue("@attributeflags", this.attributeflags);
                command.Parameters.AddWithValue("@categoryflags", this.categoryflags);
                command.Parameters.AddWithValue("@options", this.options.ToGenXY());
                command.Parameters.AddWithValue("@note", this.note);
                command.Parameters.AddWithValue("@enabled", this.enabled);
                command.Parameters.AddWithValue("@volume", this.volume);
                command.Parameters.AddWithValue("@mass", this.mass);
                command.Parameters.AddWithValue("@hidden", this.hidden);
                command.Parameters.AddWithValue("@health", this.health);
                command.Parameters.AddWithValue("@descriptiontoken", this.descriptiontoken);
                command.Parameters.AddWithValue("@purchasable", this.purchasable);
                command.Parameters.AddWithValue("@tiertype", this.tiertype);
                command.Parameters.AddWithValue("@tierlevel", this.tierlevel);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                if (this.note == null)
                {
                    command.Parameters.AddWithValue("@note", string.Empty);
                }
                else
                {
                    command.Parameters.AddWithValue("@note", this.note);
                }
                query = command.CommandText;
                foreach (SqlParameter p in command.Parameters)
                {
                    if (SqlDbType.NVarChar.Equals(p.SqlDbType) || SqlDbType.VarChar.Equals(p.SqlDbType))
                    {
                        query = query.Replace(p.ParameterName, "'" + p.Value.ToString() + "'");
                    }
                    else
                    {
                        query = query.Replace(p.ParameterName, p.Value.ToString());
                    }
                }
            }
            return query;
        }

        /// <summary>
        /// gets a record by its record id
        /// </summary>
        /// <param name='recnum'>record number</param>
        public EntityDefaults GetById(int definition)
        {
            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("SELECT * from entitydefaults Where definition=@definition;");
                command.CommandText = sqlCommand.ToString();
                command.Parameters.AddWithValue("@definition", definition);
                command.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.definition = Convert.ToInt32(reader["definition"]);
                        this.definitionname = Convert.ToString(reader["definitionname"]);
                        this.quantity = Convert.ToInt32(reader["quantity"]);
                        this.attributeflags = Convert.ToInt32(reader["attributeflags"]);
                        this.categoryflags = Convert.ToInt32(reader["categoryflags"]);
                        this.options = CreateFromOptions(Convert.ToString(reader["options"]));
                        this.note = Convert.ToString(reader["note"]);
                        this.enabled = Convert.ToInt32(reader["enabled"]);
                        this.volume = Convert.ToDecimal(reader["volume"]);
                        this.mass = Convert.ToDecimal(reader["mass"]);
                        this.hidden = Convert.ToString(reader["hidden"]);
                        this.health = Convert.ToDecimal(reader["health"]);
                        this.descriptiontoken = Convert.ToString(reader["descriptiontoken"]);
                        this.purchasable = Convert.ToInt32(reader["purchasable"]);
                        if (reader["tiertype"] != DBNull.Value) { this.tiertype = Convert.ToInt32(reader["tiertype"]); }
                        if (reader["tierlevel"] != DBNull.Value) { this.tierlevel = Convert.ToInt32(reader["tierlevel"]); }
                    }
                }
                conn.Dispose();
            }
            return this;
        }

        public EntityItems GetEntityByID(int definition)
        {
            EntityItems item = new EntityItems();
            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("SELECT * from entitydefaults Where definition=@definition;");
                command.CommandText = sqlCommand.ToString();
                command.Parameters.AddWithValue("@definition", definition);
                command.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        item.Definition = Convert.ToInt32(reader["definition"]);
                        item.Name = Convert.ToString(reader["definitionname"]);
                        item.Options = CreateFromOptions(Convert.ToString(reader["options"]));
                        item.Volume = Convert.ToDecimal(reader["volume"]);
                        item.Mass = Convert.ToDecimal(reader["mass"]);
                    }
                }
                conn.Dispose();
            }
            return item;
        }

        public List<EntityItems> GetAllModules()
        {
            List<EntityItems> list = new List<EntityItems>();
            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("SELECT * FROM entitydefaults WHERE options like '%#module%';");
                command.CommandText = sqlCommand.ToString();
                command.Parameters.AddWithValue("@definition", definition);
                command.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EntityItems item = new EntityItems();
                        item.Definition = Convert.ToInt32(reader["definition"]);
                        item.Name = Convert.ToString(reader["definitionname"]);
                        item.Options = CreateFromOptions(Convert.ToString(reader["options"]));
                        item.Volume = Convert.ToDecimal(reader["volume"]);
                        item.Mass = Convert.ToDecimal(reader["mass"]);
                        list.Add(item);
                    }
                }
                conn.Dispose();
            }
            return list;
        }

        public List<EntityItems> GetAllAmmo()
        {
            List<EntityItems> list = new List<EntityItems>();
            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("SELECT * FROM entitydefaults WHERE definitionname like '%ammo%';");
                command.CommandText = sqlCommand.ToString();
                command.Parameters.AddWithValue("@definition", definition);
                command.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EntityItems item = new EntityItems();
                        item.Definition = Convert.ToInt32(reader["definition"]);
                        item.Name = Convert.ToString(reader["definitionname"]);
                        item.Options = CreateFromOptions(Convert.ToString(reader["options"]));
                        item.Volume = Convert.ToDecimal(reader["volume"]);
                        item.Mass = Convert.ToDecimal(reader["mass"]);
                        list.Add(item);
                    }
                }
                conn.Dispose();
            }
            return list;
        }

        public string SaveWithEntityItemChange(EntityItems item)
        {
            string query = "";
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("UPDATE entitydefaults SET options=@options, volume=@volume, mass=@mass WHERE definition=@definition;");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@definition", item.Definition);
                command.Parameters.AddWithValue("@options", item.Options);
                command.Parameters.AddWithValue("@volume", item.Volume);
                command.Parameters.AddWithValue("@mass", item.Mass);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                if (this.note == null)
                {
                    command.Parameters.AddWithValue("@note", string.Empty);
                }
                else
                {
                    command.Parameters.AddWithValue("@note", this.note);
                }
                query = command.CommandText;
                foreach (SqlParameter p in command.Parameters)
                {
                    if (SqlDbType.NVarChar.Equals(p.SqlDbType) || SqlDbType.VarChar.Equals(p.SqlDbType))
                    {
                        query = query.Replace(p.ParameterName, "'" + p.Value.ToString() + "'");
                    }
                    else
                    {
                        query = query.Replace(p.ParameterName, p.Value.ToString());
                    }
                }
            }
            return query;
        }

        public string InsertWithEntityItemChange(EntityItems item)
        {
            string query = "";
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("UPDATE entitydefaults SET options=@options, volume=@volume, mass=@mass WHERE definition=@definition;");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@definition", item.Definition);
                command.Parameters.AddWithValue("@options", item.Options);
                command.Parameters.AddWithValue("@volume", item.Volume);
                command.Parameters.AddWithValue("@mass", item.Mass);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                if (this.note == null)
                {
                    command.Parameters.AddWithValue("@note", string.Empty);
                }
                else
                {
                    command.Parameters.AddWithValue("@note", this.note);
                }
                query = command.CommandText;
                foreach (SqlParameter p in command.Parameters)
                {
                    if (SqlDbType.NVarChar.Equals(p.SqlDbType) || SqlDbType.VarChar.Equals(p.SqlDbType))
                    {
                        query = query.Replace(p.ParameterName, "'" + p.Value.ToString() + "'");
                    }
                    else
                    {
                        query = query.Replace(p.ParameterName, p.Value.ToString());
                    }
                }
            }
            return query;
        }

        public List<EntityItems> GetEntitiesWithFields()
        {
            List<EntityItems> list = new List<EntityItems>();

            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append(@"SELECT entitydefaults.definition, entitydefaults.definitionname, entitydefaults.mass, entitydefaults.volume, entitydefaults.options
                FROM entitydefaults JOIN aggregatevalues ON (aggregatevalues.definition = entitydefaults.definition) 
                GROUP BY entitydefaults.definition, entitydefaults.definitionname, entitydefaults.mass, entitydefaults.volume, entitydefaults.options;");
                command.CommandText = sqlCommand.ToString();
                command.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EntityItems item = new EntityItems();
                        item.Name = Convert.ToString(reader["definitionname"]);
                        item.Definition = Convert.ToInt32(reader["definition"]);
                        item.Options = CreateFromOptions(Convert.ToString(reader["options"]));
                        item.Volume = Convert.ToInt32(reader["volume"]);
                        item.Mass = Convert.ToInt32(reader["mass"]);
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public List<EntityItems> GetAllNPCLootableBots()
        {
            List<EntityItems> list = new List<EntityItems>();

            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("SELECT DISTINCT dbo.entitydefaults.definitionname, dbo.entitydefaults.definition FROM dbo.entitydefaults JOIN dbo.npcloot on dbo.npcloot.definition = dbo.entitydefaults.definition GROUP BY entitydefaults.definition,  entitydefaults.definitionname;");
                command.CommandText = sqlCommand.ToString();
                command.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EntityItems item = new EntityItems();
                        item.Name = Convert.ToString(reader["definitionname"]);
                        item.Definition = Convert.ToInt32(reader["definition"]);
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public List<EntityItems> GetLootableEntities()
        {
            List<EntityItems> list = new List<EntityItems>();

            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("SELECT DISTINCT entitydefaults.definition, definitionname FROM dbo.entitydefaults JOIN npcloot ON npcloot.lootdefinition = entitydefaults.definition GROUP BY entitydefaults.definition, entitydefaults.definitionname;");
                command.CommandText = sqlCommand.ToString();
                command.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EntityItems item = new EntityItems();
                        item.Name = Convert.ToString(reader["definitionname"]);
                        item.Definition = Convert.ToInt32(reader["definition"]);
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public List<EntityItems> GetAllDistinctBotItems()
        {
            List<EntityItems> list = new List<EntityItems>();

            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("SELECT DISTINCT dbo.entitydefaults.definitionname, dbo.entitydefaults.definition FROM dbo.entitydefaults JOIN dbo.chassisbonus on dbo.chassisbonus.definition = dbo.entitydefaults.definition GROUP BY entitydefaults.definition,  entitydefaults.definitionname;");
                command.CommandText = sqlCommand.ToString();
                command.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EntityItems item = new EntityItems();
                        item.Name = Convert.ToString(reader["definitionname"]);
                        item.Definition = Convert.ToInt32(reader["definition"]);
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public List<EntityItems> GetAllNPCEntities()
        {
            List<EntityItems> list = new List<EntityItems>();
            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append(@"SELECT entitydefaults.* FROM entitydefaults
	                JOIN robottemplaterelation ON robottemplaterelation.definition=entitydefaults.definition;");
                    command.CommandText = sqlCommand.ToString();
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EntityItems item = new EntityItems();
                            item.Name = Convert.ToString(reader["definitionname"]);
                            item.Definition = Convert.ToInt32(reader["definition"]);
                            item.Options = CreateFromOptions(Convert.ToString(reader["options"]));
                            item.Volume = Convert.ToInt32(reader["volume"]);
                            item.Mass = Convert.ToInt32(reader["mass"]);
                            list.Add(item);
                        }
                    }
                }
            }
            return list;
        }


        public static EntityItems GetEntityItem(EntityDefaults entity)
        {
            EntityItems item = new EntityItems();
            item.Definition = entity.definition;
            item.Mass = entity.mass;
            item.Name = entity.definitionname;
            item.Options = entity.options;
            item.Volume = entity.volume;
            return item;
        }

        public List<EntityDefaults> GetEntityItemsByCategory(CategoryFlags flag)
        {
            List<EntityDefaults> list = this.GetEntitiesByCategory(flag);
            return list;
        }


        private CategoryFlags GetCategoryFlagsMask(CategoryFlags categoryFlags)
        {
            ulong num = ulong.MaxValue;
            while ((categoryFlags & (CategoryFlags)num) > CategoryFlags.undefined)
            {
                num <<= 8;
            }
            return (CategoryFlags)(~(CategoryFlags)num);
        }



        public List<EntityDefaults> GetEntitiesByCategory(CategoryFlags CategoryFlag)
        {
            List<EntityDefaults> list = new List<EntityDefaults>();
            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("select * from entitydefaults where (categoryflags & CAST(@mask as BIGINT)) = @flag");
                    command.Parameters.AddWithValue("@mask", GetCategoryFlagsMask(CategoryFlag));
                    command.Parameters.AddWithValue("@flag", CategoryFlag);

                    command.CommandText = sqlCommand.ToString();
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EntityDefaults tmp = new EntityDefaults(this.ConnString);

                            tmp.definition = Convert.ToInt32(reader["definition"]);
                            tmp.definitionname = Convert.ToString(reader["definitionname"]);
                            tmp.quantity = Convert.ToInt32(reader["quantity"]);
                            tmp.attributeflags = Convert.ToInt64(reader["attributeflags"]);
                            tmp.categoryflags = Convert.ToInt64(reader["categoryflags"]);
                            tmp.options = CreateFromOptions(Convert.ToString(reader["options"]));
                            tmp.note = Convert.ToString(reader["note"]);
                            tmp.enabled = Convert.ToInt32(reader["enabled"]);
                            tmp.volume = Convert.ToDecimal(reader["volume"]);
                            tmp.mass = Convert.ToDecimal(reader["mass"]);
                            tmp.hidden = Convert.ToString(reader["hidden"]);
                            tmp.health = Convert.ToDecimal(reader["health"]);
                            tmp.descriptiontoken = Convert.ToString(reader["descriptiontoken"]);
                            tmp.purchasable = Convert.ToInt32(reader["purchasable"]);
                            if (reader["tiertype"] != DBNull.Value) { tmp.tiertype = Convert.ToInt32(reader["tiertype"]); }
                            if (reader["tierlevel"] != DBNull.Value) { tmp.tierlevel = Convert.ToInt32(reader["tierlevel"]); }

                            list.Add(tmp);
                        }
                    }
                }
            }
            return list;
        }

        // this sort of works.
        public List<EntityDefaults> GetEntitiesByAttribute(AttributeFlags AttributeFlag)
        {
            List<EntityDefaults> list = new List<EntityDefaults>();
            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("select * from entitydefaults where (attributeflags & @flag) = @flag ");
                    command.Parameters.AddWithValue("@flag", AttributeFlag);

                    command.CommandText = sqlCommand.ToString();
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EntityDefaults tmp = new EntityDefaults(this.ConnString);

                            tmp.definition = Convert.ToInt32(reader["definition"]);
                            tmp.definitionname = Convert.ToString(reader["definitionname"]);
                            tmp.quantity = Convert.ToInt32(reader["quantity"]);
                            tmp.attributeflags = Convert.ToInt64(reader["attributeflags"]);
                            tmp.categoryflags = Convert.ToInt64(reader["categoryflags"]);
                            tmp.options = CreateFromOptions(Convert.ToString(reader["options"]));
                            tmp.note = Convert.ToString(reader["note"]);
                            tmp.enabled = Convert.ToInt32(reader["enabled"]);
                            tmp.volume = Convert.ToDecimal(reader["volume"]);
                            tmp.mass = Convert.ToDecimal(reader["mass"]);
                            tmp.hidden = Convert.ToString(reader["hidden"]);
                            tmp.health = Convert.ToDecimal(reader["health"]);
                            tmp.descriptiontoken = Convert.ToString(reader["descriptiontoken"]);
                            tmp.purchasable = Convert.ToInt32(reader["purchasable"]);
                            if (reader["tiertype"] != DBNull.Value) { tmp.tiertype = Convert.ToInt32(reader["tiertype"]); }
                            if (reader["tierlevel"] != DBNull.Value) { tmp.tierlevel = Convert.ToInt32(reader["tierlevel"]); }

                            list.Add(tmp);
                        }
                    }
                }
            }
            return list;
        }


        // NOT SMART!
        // Got a better idea?
        // Yeah. Don't do it?
        // ok, let's do it.
        public List<EntityDefaults> GetAllEntities()
        {
            List<EntityDefaults> list = new List<EntityDefaults>();
            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("select * from entitydefaults;");

                    command.CommandText = sqlCommand.ToString();
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EntityDefaults tmp = new EntityDefaults(this.ConnString);

                            tmp.definition = Convert.ToInt32(reader["definition"]);
                            tmp.definitionname = Convert.ToString(reader["definitionname"]);
                            tmp.quantity = Convert.ToInt32(reader["quantity"]);
                            tmp.attributeflags = Convert.ToInt64(reader["attributeflags"]);
                            tmp.categoryflags = Convert.ToInt64(reader["categoryflags"]);
                            tmp.options = CreateFromOptions(Convert.ToString(reader["options"]));
                            tmp.note = Convert.ToString(reader["note"]);
                            tmp.enabled = Convert.ToInt32(reader["enabled"]);
                            tmp.volume = Convert.ToDecimal(reader["volume"]);
                            tmp.mass = Convert.ToDecimal(reader["mass"]);
                            tmp.hidden = Convert.ToString(reader["hidden"]);
                            tmp.health = Convert.ToDecimal(reader["health"]);
                            tmp.descriptiontoken = Convert.ToString(reader["descriptiontoken"]);
                            tmp.purchasable = Convert.ToInt32(reader["purchasable"]);
                            if (reader["tiertype"] != DBNull.Value) { tmp.tiertype = Convert.ToInt32(reader["tiertype"]); }
                            if (reader["tierlevel"] != DBNull.Value) { tmp.tierlevel = Convert.ToInt32(reader["tierlevel"]); }

                            list.Add(tmp);
                        }
                    }
                }
            }
            return list;
        }


        private EntityOptions CreateFromOptions(string descGenXY)
        {
            Dictionary<string, object> dict = GenxyConverter.Deserialize(descGenXY);
            EntityOptions tmp = GetEntityOptionsFromXY(dict);
            return tmp;
        }


        private static EntityOptions GetEntityOptionsFromXY(IDictionary<string, object> d)
        {

            EntityOptions EntityOpts = new EntityOptions();
            bool success = d.TryGetValue("head", out object head);
            EntityOpts.Head = (Convert.ToInt32(head));

            success = d.TryGetValue("chassis", out object chassis) && success;
            EntityOpts.Chassis = Convert.ToInt32(chassis);

            success = d.TryGetValue("leg", out object leg) && success;
            EntityOpts.Leg = Convert.ToInt32(leg);

            success = d.TryGetValue("inventory", out object inv) && success;
            EntityOpts.Inventory = Convert.ToInt32(inv);

            if (d.TryGetValue("slotFlags", out object slotflags))
            {
                EntityOpts.slotFlags = (SlotFlags[])slotflags;
            }

            if (d.TryGetValue("moduleFlag", out object moduleflag))
            {
                EntityOpts.moduleFlag = (SlotFlags)moduleflag;
            }

            if (d.TryGetValue("ammoCapacity", out object ammocap))
            {
                EntityOpts.ammoCapacity = (int)ammocap;
            }

            if (d.TryGetValue("ammoType", out object ammotype))
            {
                EntityOpts.ammoType = (long)ammotype;
            }

            return EntityOpts;
        }

        /// <summary>
        /// fires when properties are set.
        /// </summary>
        /// <param name='name'>name of property being changed</param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
