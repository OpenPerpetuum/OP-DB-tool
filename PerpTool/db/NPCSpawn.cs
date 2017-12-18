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
    public class NPCSpawn : INotifyPropertyChanged
    {
        /// <summary>
        /// private field
        /// </summary>
        private int privateid;

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
        /// Initializes a new instance of the <see cref='npcspawnTbl' /> class.
        /// </summary>
        /// <param name='connectionString'>pass the connection string to the database</param>
        public NPCSpawn(string connectionString)
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

        public string ConcatSpawnIDName
        {
            get
            {
                return string.Format("{0} - {1} - {2}", this.name, this.description, this.note);
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
            this.name = string.Empty;
            this.description = string.Empty;
            this.note = string.Empty;
        }

        /// <summary>
        /// saves a new record
        /// </summary>
        public void SaveNewRecord()
        {
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("Insert into npcspawn ");
                sqlCommand.Append("(`id`, `name`, `description`, `note`) ");
                sqlCommand.Append(" Values ");
                sqlCommand.Append("(@id, @name, @description, @note); SELECT SCOPE_IDENTITY(); ");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@id", this.id);
                command.Parameters.AddWithValue("@name", this.name);
                command.Parameters.AddWithValue("@description", this.description);
                command.Parameters.AddWithValue("@note", this.note);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                Object Id = command.ExecuteScalar();
                if (Id != DBNull.Value)
                {
                    this.id = Convert.ToInt32(Id);
                }
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
                sqlCommand.Append("UPDATE npcspawn Set `name`= @name, `description`= @description, `note`= @note where id = @id");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@id", this.id);
                command.Parameters.AddWithValue("@name", this.name);
                command.Parameters.AddWithValue("@description", this.description);
                command.Parameters.AddWithValue("@note", this.note);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public List<NPCSpawn> GetAllSpawns()
        {
            List<NPCSpawn> list = new List<NPCSpawn>();

            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("SELECT * from npcspawn");
                    command.CommandText = sqlCommand.ToString();
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NPCSpawn tmp = new NPCSpawn(this.ConnString);
                            tmp.id = Convert.ToInt32(reader["id"]);
                            tmp.name = Convert.ToString(reader["name"]);
                            tmp.description = Convert.ToString(reader["description"]);
                            tmp.note = Convert.ToString(reader["note"]);
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