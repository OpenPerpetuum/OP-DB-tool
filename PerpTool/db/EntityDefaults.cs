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

    // THI IS SHIT. don't hate!

    public class EntityItems
    {
        public int Definition { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// Table Class
    /// </summary>
    class EntityDefaults : INotifyPropertyChanged
    {

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
        private int privateattributeflags;

        /// <summary>
        /// private field
        /// </summary>
        private int privatecategoryflags;

        /// <summary>
        /// private field
        /// </summary>
        private string privateoptions;

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

        /// <summary>
        /// Initializes a new instance of the <see cref='entitydefaultsTbl' /> class.
        /// </summary>
        /// <param name='connectionString'>pass the connection string to the database</param>
        public EntityDefaults(string connectionString)
        {
            this.ConnString = connectionString;
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
        public int attributeflags
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
        public int categoryflags
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
        public string options
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
            this.options = string.Empty;
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
        public void SaveNewRecord()
        {
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("Insert into entitydefaults ");
                sqlCommand.Append("(`definition`, `definitionname`, `quantity`, `attributeflags`, `categoryflags`, `options`, `note`, `enabled`, `volume`, `mass`, `hidden`, `health`, `descriptiontoken`, `purchasable`, `tiertype`, `tierlevel`) ");
                sqlCommand.Append(" Values ");
                sqlCommand.Append("(@definition, @definitionname, @quantity, @attributeflags, @categoryflags, @options, @note, @enabled, @volume, @mass, @hidden, @health, @descriptiontoken, @purchasable, @tiertype, @tierlevel) ");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@definition", this.definition);
                command.Parameters.AddWithValue("@definitionname", this.definitionname);
                command.Parameters.AddWithValue("@quantity", this.quantity);
                command.Parameters.AddWithValue("@attributeflags", this.attributeflags);
                command.Parameters.AddWithValue("@categoryflags", this.categoryflags);
                command.Parameters.AddWithValue("@options", this.options);
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
                sqlCommand.Append("UPDATE entitydefaults Set `definitionname`= @definitionname, `quantity`= @quantity, `attributeflags`= @attributeflags, `categoryflags`= @categoryflags, `options`= @options, `note`= @note, `enabled`= @enabled, `volume`= @volume, `mass`= @mass, `hidden`= @hidden, `health`= @health, `descriptiontoken`= @descriptiontoken, `purchasable`= @purchasable, `tiertype`= @tiertype, `tierlevel`= @tierlevel where definition = @definition");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@definition", this.definition);
                command.Parameters.AddWithValue("@definitionname", this.definitionname);
                command.Parameters.AddWithValue("@quantity", this.quantity);
                command.Parameters.AddWithValue("@attributeflags", this.attributeflags);
                command.Parameters.AddWithValue("@categoryflags", this.categoryflags);
                command.Parameters.AddWithValue("@options", this.options);
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
            }
        }

        /// <summary>
        /// gets a record by its record id
        /// </summary>
        /// <param name='recnum'>record number</param>
        public void GetById(int definition)
        {
            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("SELECT * from entitydefaults Where definition=@definition");
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
                        this.options = Convert.ToString(reader["options"]);
                        this.note = Convert.ToString(reader["note"]);
                        this.enabled = Convert.ToInt32(reader["enabled"]);
                        this.volume = Convert.ToDecimal(reader["volume"]);
                        this.mass = Convert.ToDecimal(reader["mass"]);
                        this.hidden = Convert.ToString(reader["hidden"]);
                        this.health = Convert.ToDecimal(reader["health"]);
                        this.descriptiontoken = Convert.ToString(reader["descriptiontoken"]);
                        this.purchasable = Convert.ToInt32(reader["purchasable"]);
                        this.tiertype = Convert.ToInt32(reader["tiertype"]);
                        this.tierlevel = Convert.ToInt32(reader["tierlevel"]);
                    }
                }

                conn.Dispose();
            }
        }

        public List<EntityItems> GetEntitiesWithFields()
        {
            List<EntityItems> list = new List<EntityItems>();

            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("SELECT entitydefaults.definition, entitydefaults.definitionname FROM entitydefaults JOIN aggregatevalues ON (aggregatevalues.definition = entitydefaults.definition) GROUP BY entitydefaults.definition,  entitydefaults.definitionname");
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


        public List<EntityItems> GetAllNPCLootableBots()
        {
            List<EntityItems> list = new List<EntityItems>();

            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("SELECT DISTINCT dbo.entitydefaults.definitionname, dbo.entitydefaults.definition FROM dbo.entitydefaults JOIN dbo.npcloot on dbo.npcloot.definition = dbo.entitydefaults.definition GROUP BY entitydefaults.definition,  entitydefaults.definitionname");
                //sqlCommand.Append("");
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
                sqlCommand.Append("SELECT DISTINCT entitydefaults.definition, definitionname FROM dbo.entitydefaults JOIN npcloot ON npcloot.lootdefinition = entitydefaults.definition GROUP BY entitydefaults.definition, entitydefaults.definitionname");
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
