using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Perptool.db
{

    public class FieldValuesStuff
    {
        public int FieldId { get; set; }
        public int ValueId { get; set; }
        public string  FieldName { get; set; }
        public decimal FieldValue { get; set; }
        public int FieldFormula { get; set; }
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
        /// saves a new record
        /// </summary>
        public void SaveNewRecord()
        {
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("Insert into aggregatevalues ");
                sqlCommand.Append("(`id`, `definition`, `field`, `value`) ");
                sqlCommand.Append(" Values ");
                sqlCommand.Append("(@id, @definition, @field, @value) ");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@id", this.id);
                command.Parameters.AddWithValue("@definition", this.definition);
                command.Parameters.AddWithValue("@field", this.field);
                command.Parameters.AddWithValue("@value", this.value);

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
                sqlCommand.Append("UPDATE aggregatevalues Set definition=@definition, field=@field, value=@value where id = @id");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@id", this.id);
                command.Parameters.AddWithValue("@definition", this.definition);
                command.Parameters.AddWithValue("@field", this.field);
                command.Parameters.AddWithValue("@value", this.value);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                string query = command.CommandText;
                foreach (SqlParameter p in command.Parameters)
                {
                    query = query.Replace(p.ParameterName, p.Value.ToString());
                }
                Console.Write(query);

                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + this.GetType().Name + ".sql",
                  command.ToString());
            }
        }


        public List<FieldValuesStuff> GetValuesForEntity(int EntityId)
        {
            List<FieldValuesStuff> list = new List<FieldValuesStuff>();
            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("SELECT aggregatefields.id as fieldid, aggregatevalues.id as valueid, aggregatefields.name, aggregatevalues.value, aggregatefields.formula from aggregatevalues join aggregatefields on (aggregatevalues.field = aggregatefields.id) Where definition=@id");
                    command.CommandText = sqlCommand.ToString();
                    command.Parameters.AddWithValue("@id", EntityId);
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FieldValuesStuff tmp = new FieldValuesStuff();
                            tmp.FieldId = Convert.ToInt32(reader["fieldid"]);
                            tmp.ValueId = Convert.ToInt32(reader["valueid"]);
                            tmp.FieldName = Convert.ToString(reader["name"]);
                            tmp.FieldValue = Convert.ToDecimal(reader["value"]);
                            tmp.FieldFormula = Convert.ToInt32(reader["formula"]);
                            list.Add(tmp);

                            //this.id = Convert.ToInt32(reader["id"]);
                            //this.definition = Convert.ToInt32(reader["definition"]);
                            //this.field = Convert.ToInt32(reader["field"]);
                            //this.value = Convert.ToDecimal(reader["value"]);
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
                    sqlCommand.Append("SELECT * from aggregatevalues Where definition=@id");
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