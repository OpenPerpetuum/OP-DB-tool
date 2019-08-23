using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerpTool.db
{
    public class RelicTypeRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RaceId { get; set; }
        public int Level { get; set; }
        public int Ep { get; set; }
        public DBAction dBAction { get; set; }

        public RelicTypeRecord()
        {
        }

        public static string IDKey = "@relicTypeID";

        public static string GetDeclStatement()
        {
            return "DECLARE " + IDKey + " int;";
        }

        public string GetRelicTypeLookupStatement()
        {
            return "SET " + IDKey + " = (SELECT id FROM relictypes WHERE name='" + this.Name + "');";
        }

        public RelicTypesTable ToTable(string connstring)
        {
            RelicTypesTable a = new RelicTypesTable(connstring);
            a.id = this.Id;
            a.name = this.Name;
            a.raceid = this.RaceId;
            a.level = this.Level;
            a.ep = this.Ep;
            return a;
        }

    }
    public class RelicTypesTable
    {
        private int _id;
        private string _name;
        private int _raceid;
        private int _level;
        private int _ep;
        public static string IDKey = "@relicTypeID";

        public static string GetDeclStatement()
        {
            return "DECLARE " + IDKey + " int;";
        }

        public string GetRelicTypeLookupStatement()
        {
            return "SET " + IDKey + " = (SELECT id FROM relictypes WHERE name='" + this.name + "');";
        }

        private string ConnString { get; set; }

        public RelicTypesTable(string connectionString)
        {
            this.ConnString = connectionString;
        }

        #region Fields
        public event PropertyChangedEventHandler PropertyChanged;

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

        public int raceid
        {
            get
            {
                return this._raceid;
            }

            set
            {
                this._raceid = value;
                this.OnPropertyChanged("raceid");
            }
        }

        public int level
        {
            get
            {
                return this._level;
            }

            set
            {
                this._level = value;
                this.OnPropertyChanged("level");
            }
        }

        public int ep
        {
            get
            {
                return this._ep;
            }

            set
            {
                this._ep = value;
                this.OnPropertyChanged("ep");
            }
        }
        #endregion




        public void Clear()
        {
            this.id = 0;
            this.name = "";
            this.raceid = 0;
            this.level = 0;
            this.ep = 0;
        }


        public void GetbyID(int id)
        {
            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("SELECT id, name, raceid, level, ep FROM relictypes WHERE id=@id;");
                command.CommandText = sqlCommand.ToString();
                command.Parameters.AddWithValue("@id", id);
                command.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.id = Convert.ToInt32(reader["id"]);
                        this.name = Convert.ToString(reader["name"]);
                        this.raceid = Convert.ToInt32(reader["raceid"]);
                        this.level = Convert.ToInt32(reader["level"]);
                        this.ep = Convert.ToInt32(reader["ep"]);
                    }
                }
                conn.Dispose();
            }
        }


        public ObservableCollection<RelicTypeRecord> GetAllTypes()
        {
            ObservableCollection<RelicTypeRecord> types = new ObservableCollection<RelicTypeRecord>();
            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("SELECT id, name, raceid, level, ep FROM relictypes;");
                command.CommandText = sqlCommand.ToString();
                command.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RelicTypeRecord a = new RelicTypeRecord();
                        a.Id = Convert.ToInt32(reader["id"]);
                        a.Name = Convert.ToString(reader["name"]);
                        a.RaceId = Convert.ToInt32(reader["raceid"]);
                        a.Level = Convert.ToInt32(reader["level"]);
                        a.Ep = Convert.ToInt32(reader["ep"]);
                        types.Add(a);
                    }
                }
                conn.Dispose();
            }
            return types;
        }

        public List<RelicTypesTable> GetAll()
        {
            List<RelicTypesTable> types = new List<RelicTypesTable>();
            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("SELECT id, name, raceid, level, ep FROM relictypes;");
                command.CommandText = sqlCommand.ToString();
                command.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RelicTypesTable a = new RelicTypesTable(this.ConnString);
                        a.id = Convert.ToInt32(reader["id"]);
                        a.name = Convert.ToString(reader["name"]);
                        a.raceid = Convert.ToInt32(reader["raceid"]);
                        a.level = Convert.ToInt32(reader["level"]);
                        a.ep = Convert.ToInt32(reader["ep"]);
                        types.Add(a);
                    }
                }
                conn.Dispose();
            }
            return types;
        }


        public string Save(RelicTypeRecord a, string connstring)
        {
            RelicTypesTable table = a.ToTable(connstring);
            return table.Save();
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
                sqlCommand.Append(@"UPDATE [dbo].[relictypes]
               SET [name] = @name
                  ,[raceid] = @raceid
                  ,[ep] = @ep
                  ,[level] = @level
                WHERE id=@id");
                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@id", this.id);
                command.Parameters.AddWithValue("@name", this.name);
                command.Parameters.AddWithValue("@raceid", this.raceid);
                command.Parameters.AddWithValue("@ep", this.ep);
                command.Parameters.AddWithValue("@level", this.level);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                query = Utilities.parseCommandString(command, new List<string>(new string[] { IDKey }));//TODO
            }
            return query;
        }

        public string Insert(ArtifactTypeRecord a, string connstring)
        {
            ArtifactTypesTable table = a.toTable(connstring);
            return table.Insert();
        }

        public string Insert()
        {
            string query = "";
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append(@"INSERT INTO [dbo].[relictypes] (name, raceid, ep level) 
                VALUES (@name,@raceid,@ep,@level);");
                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@name", this.id);
                command.Parameters.AddWithValue("@raceid", this.raceid);
                command.Parameters.AddWithValue("@ep", this.ep);
                command.Parameters.AddWithValue("@level", this.level);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

            }
            return query;
        }

        public string Delete(ArtifactTypeRecord a, string connstring)
        {
            ArtifactTypesTable table = a.toTable(connstring);
            return table.Delete();
        }

        public string Delete()
        {
            string query = "";
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("DELETE FROM [dbo].[relicspawninfo] WHERE relictypeid=@id;");
                sqlCommand.Append("DELETE FROM [dbo].[relicloot] WHERE relictypeid=@id;");
                sqlCommand.Append("DELETE FROM [dbo].[relictypes] WHERE id=@id;");  //TODO test me
                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@id", this.id);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                query = command.CommandText;
                query = Utilities.parseCommandString(command, new List<string>(new string[] { IDKey }));//TODO
            }
            return query;
        }


        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
