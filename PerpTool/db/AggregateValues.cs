using PerpTool.db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Perptool.db
{

    public class FieldValuesStuff : INotifyPropertyChanged
    {
        private int _FieldId;
        private int _ValueId;
        private int _Definition;
        private string _FieldName;
        private decimal _FieldValue;
        private string _FieldUnits;
        private decimal _FieldOffset;
        private decimal _FieldMultiplier;
        private int _FieldFormula;
        private DBAction _dBAction;


        public int FieldId
        {
            get
            {
                return this._FieldId;
            }
            set
            {
                this._FieldId = value;
                OnPropertyChanged("FieldId");
            }
        }
        public int ValueId
        {
            get
            {
                return this._ValueId;
            }
            set
            {
                this._ValueId = value;
                OnPropertyChanged("ValueId");
            }
        }
        public int Definition
        {
            get
            {
                return this._Definition;
            }
            set
            {
                this._Definition = value;
                OnPropertyChanged("Definition");
            }
        }
        public string FieldName
        {
            get
            {
                return this._FieldName;
            }
            set
            {
                this._FieldName = value;
                OnPropertyChanged("FieldName");
            }
        }
        public decimal FieldValue
        {
            get
            {
                return this._FieldValue;
            }
            set
            {
                this._FieldValue = value;
                OnPropertyChanged("FieldValue");
            }
        }
        public string FieldUnits
        {
            get
            {
                return this._FieldUnits;
            }
            set
            {
                this._FieldUnits = value;
                OnPropertyChanged("FieldUnits");
            }
        }
        public decimal FieldOffset
        {
            get
            {
                return this._FieldOffset;
            }
            set
            {
                this._FieldOffset = value;
                OnPropertyChanged("FieldOffset");
            }
        }
        public decimal FieldMultiplier
        {
            get
            {
                return this._FieldMultiplier;
            }
            set
            {
                this._FieldMultiplier = value;
                OnPropertyChanged("FieldMultiplier");
            }
        }
        public int FieldFormula
        {
            get
            {
                return this._FieldFormula;
            }
            set
            {
                this._FieldFormula = value;
                OnPropertyChanged("FieldFormula");
            }
        }
        public DBAction dBAction
        {
            get
            {
                return this._dBAction;
            }
            set
            {
                this._dBAction = value;
                OnPropertyChanged("dBAction");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }


    /// <summary>
    /// Table Class
    /// </summary>
    class AggregateValues : INotifyPropertyChanged
    {
        /// <summary>
        /// private field
        /// </summary>
        private int privateid;

        /// <summary>
        /// private field
        /// </summary>
        private int privatedefinition;

        /// <summary>
        /// private field
        /// </summary>
        private int privatefield;

        /// <summary>
        /// private field
        /// </summary>
        private decimal privatevalue;

        /// <summary>
        /// Initializes a new instance of the <see cref='AggregateValues' /> class.
        /// </summary>
        /// <param name='connectionString'>pass the connection string to the database</param>
        public AggregateValues(string connectionString)
        {
            this.ConnString = connectionString;
        }

        /// <summary>
        /// Event handler for properties
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

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
        /// Gets or sets public field field
        /// </summary>
        public int field
        {
            get
            {
                return this.privatefield;
            }

            set
            {
                this.privatefield = value;
                this.OnPropertyChanged("field");
            }
        }

        /// <summary>
        /// Gets or sets public field value
        /// </summary>
        public decimal value
        {
            get
            {
                return this.privatevalue;
            }

            set
            {
                this.privatevalue = value;
                this.OnPropertyChanged("value");
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
            this.id = 0;
            this.definition = 0;
            this.field = 0;
            this.value = 0;
        }


        /// <summary>
        /// saves existing record
        /// And returns query as string for recording SQL updates to file
        /// </summary>
        public string Save(FieldValuesStuff data)
        {
            string query = "";
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("UPDATE aggregatevalues SET definition=@definition, field=@field, value=@value WHERE id = @id;");
                command.CommandText = sqlCommand.ToString();
                command.Parameters.AddWithValue("@id", data.ValueId);
                command.Parameters.AddWithValue("@definition", data.Definition);
                command.Parameters.AddWithValue("@field", data.FieldId);
                command.Parameters.AddWithValue("@value", data.FieldValue);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();
                query = command.CommandText;
                foreach (SqlParameter p in command.Parameters)
                {
                    query = query.Replace(p.ParameterName, p.Value.ToString());
                }
            }
            return query;
        }


        public ObservableCollection<FieldValuesStuff> GetValuesForEntity(int EntityId)
        {
            ObservableCollection<FieldValuesStuff> list = new ObservableCollection<FieldValuesStuff>();
            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append(@"SELECT aggregatefields.id as fieldid, aggregatevalues.id as valueid, aggregatefields.name as fieldname, aggregatevalues.value, 
                    aggregatefields.formula, aggregatefields.measurementunit, aggregatefields.measurementoffset, aggregatefields.measurementmultiplier
                    from aggregatevalues join aggregatefields on (aggregatevalues.field = aggregatefields.id) Where definition=@id;");
                    command.CommandText = sqlCommand.ToString();
                    command.Parameters.AddWithValue("@id", EntityId);
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FieldValuesStuff tmp = new FieldValuesStuff();
                            tmp.dBAction = DBAction.UPDATE;
                            tmp.Definition = EntityId;
                            tmp.FieldId = Convert.ToInt32(reader["fieldid"]);
                            tmp.ValueId = Convert.ToInt32(reader["valueid"]);
                            tmp.FieldName = Convert.ToString(reader["fieldname"]);
                            tmp.FieldValue = Convert.ToDecimal(reader["value"]);
                            tmp.FieldFormula = Convert.ToInt32(reader["formula"]);
                            tmp.FieldUnits = Convert.ToString(reader["measurementunit"]);
                            tmp.FieldOffset = Convert.ToDecimal(reader["measurementoffset"]);
                            tmp.FieldMultiplier = Convert.ToDecimal(reader["measurementmultiplier"]);
                            list.Add(tmp);
                        }
                    }
                }
            }
            return list;
        }

        public void GetById(int agValId)
        {
            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("SELECT * from aggregatevalues Where id=@id");
                    command.CommandText = sqlCommand.ToString();
                    command.Parameters.AddWithValue("@id", agValId);
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.id = Convert.ToInt32(reader["id"]);
                            this.definition = Convert.ToInt32(reader["definition"]);
                            this.field = Convert.ToInt32(reader["field"]);
                            this.value = Convert.ToDecimal(reader["value"]);
                        }
                    }
                }
            }
        }

        public string Insert(FieldValuesStuff data)
        {
            string query = "";

            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("INSERT INTO [dbo].[aggregatevalues] ([definition],[field],[value]) VALUES (@definition, @field, @value);");
                command.CommandText = sqlCommand.ToString();
                command.Parameters.AddWithValue("@definition", data.Definition);
                command.Parameters.AddWithValue("@field", data.FieldId);
                command.Parameters.AddWithValue("@value", data.FieldValue);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();
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
        /// fires when properties are set.
        /// </summary>
        /// <param name='name'>name of property being changed</param>
        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}