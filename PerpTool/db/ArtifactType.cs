using System;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using PerpTool.db;
using System.Collections.Generic;
using Perptool.db;

namespace PerpTool.db
{

    public class ArtifactTypeRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GoalRange { get; set; }
        public int NpcPresenceID { get; set; }
        public string NpcPresenceName { get; set; }
        public int Persistent { get; set; }
        public int MinimumLoot { get; set; }
        public int Dynamic { get; set; }

        public ArtifactTypeRecord()
        {


        }

        public static string IDKey = "@artifactTypeID";

        public static string GetDeclStatement()
        {
            return "DECLARE " + IDKey + " int;";
        }

        public string GetArtifactTypeLookupStatement()
        {
            return "SET " + IDKey + " = (SELECT id FROM artifacttypes WHERE name='" + this.Name + "');";
        }

        public ArtifactTypesTable toTable(string connstring)
        {
            ArtifactTypesTable a = new ArtifactTypesTable(connstring);
            a.id = this.Id;
            a.name = this.Name;
            a.goalrange = this.GoalRange;
            a.npcpresence = this.NpcPresenceID;
            a.persistent = this.Persistent;
            a.minimumloot = this.MinimumLoot;
            a.dynamic = this.Dynamic;
            return a;
        }

    }

    public class ArtifactTypesTable : INotifyPropertyChanged
    {

        private int privateid;
        private string privatename;
        private int privategoalrange;
        private int privatenpcpresenceid;
        private int privatepersistent;
        private int privateminimumloot;
        private int privatedynamic;

        public static string IDKey = "@artifactTypeID";


        //SQL helpers
        public static string GetArtifactTypeDeclStatement()
        {
            return "DECLARE " + IDKey + " int;";
        }

        public string GetArtifactTypeDefinitionLokupStatement()
        {
            return "SET " + IDKey + " = (SELECT TOP 1 id from artifacttypes WHERE [name] = '" + this.name + "');";
        }

        public ArtifactTypesTable(string connectionString)
        {
            this.ConnString = connectionString;
        }

        #region Fields
        public event PropertyChangedEventHandler PropertyChanged;

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

        public int goalrange
        {
            get
            {
                return this.privategoalrange;
            }

            set
            {
                this.privategoalrange = value;
                this.OnPropertyChanged("goalrange");
            }
        }

        public int npcpresence
        {
            get
            {
                return this.privatenpcpresenceid;
            }

            set
            {
                this.privatenpcpresenceid = value;
                this.OnPropertyChanged("npcpresenceid");
            }
        }

        public int persistent
        {
            get
            {
                return this.privatepersistent;
            }

            set
            {
                this.privatepersistent = value;
                this.OnPropertyChanged("persistent");
            }
        }

        public int minimumloot
        {
            get
            {
                return this.privateminimumloot;
            }

            set
            {
                this.privateminimumloot = value;
                this.OnPropertyChanged("maxquantity");
            }
        }

        public int dynamic
        {
            get
            {
                return this.privatedynamic;
            }

            set
            {
                this.privatedynamic = value;
                this.OnPropertyChanged("dynamic");
            }
        }





        /// <summary>
        /// Gets or sets connection string
        /// </summary>
        private string ConnString { get; set; }
        #endregion



        public void Clear()
        {
            this.id = 0;
            this.name = "";
            this.goalrange = 0;
            this.npcpresence = 0;
            this.persistent = 0;
            this.minimumloot = 0;
            this.dynamic = 0;
        }


        public void GetbyID(int id)
        {
            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("SELECT * FROM artifacttypes WHERE id=@id;");
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
                        this.goalrange = Convert.ToInt32(reader["goalrange"]);
                        this.npcpresence = Utilities.handleNullableInt(reader["npcpresenceid"]);
                        this.persistent = Convert.ToInt32(reader["persistent"]);
                        this.minimumloot = Convert.ToInt32(reader["minimumloot"]);
                        this.dynamic = Convert.ToInt32(reader["dynamic"]);
                    }
                }
                conn.Dispose();
            }
        }


        public ObservableCollection<ArtifactTypeRecord> GetAllTypes()
        {
            ObservableCollection<ArtifactTypeRecord> types = new ObservableCollection<ArtifactTypeRecord>();
            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append(@"SELECT artifacttypes.*, npcpresence.name AS npcpresencename FROM artifacttypes
                LEFT JOIN npcpresence ON(npcpresence.id = artifacttypes.npcpresenceid);");
                command.CommandText = sqlCommand.ToString();
                command.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ArtifactTypeRecord a = new ArtifactTypeRecord();
                        a.Id = Convert.ToInt32(reader["id"]);
                        a.Name = Convert.ToString(reader["name"]);
                        a.GoalRange = Convert.ToInt32(reader["goalrange"]);
                        a.NpcPresenceID = Utilities.handleNullableInt(reader["npcpresenceid"]);
                        a.NpcPresenceName = Utilities.handleNullableString(reader["npcpresencename"]);
                        a.Persistent = Convert.ToInt32(reader["persistent"]);
                        a.MinimumLoot = Convert.ToInt32(reader["minimumloot"]);
                        a.Dynamic = Convert.ToInt32(reader["dynamic"]);
                        types.Add(a);
                    }
                }
                conn.Dispose();
            }
            return types;
        }

        public List<ArtifactTypesTable> GetAll()
        {
            List<ArtifactTypesTable> types = new List<ArtifactTypesTable>();
            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("SELECT * FROM artifacttypes");
                command.CommandText = sqlCommand.ToString();
                command.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ArtifactTypesTable a = new ArtifactTypesTable(this.ConnString);
                        a.id = Convert.ToInt32(reader["id"]);
                        a.name = Convert.ToString(reader["name"]);
                        a.goalrange = Convert.ToInt32(reader["goalrange"]);
                        a.npcpresence = Utilities.handleNullableInt(reader["npcpresenceid"]);
                        a.persistent = Convert.ToInt32(reader["persistent"]);
                        a.minimumloot = Convert.ToInt32(reader["minimumloot"]);
                        a.dynamic = Convert.ToInt32(reader["dynamic"]);
                        types.Add(a);
                    }
                }
                conn.Dispose();
            }
            return types;
        }


        public string Save(ArtifactTypeRecord a, string connstring)
        {
            ArtifactTypesTable table = a.toTable(connstring);
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
                sqlCommand.Append(@"UPDATE [dbo].[artifacttypes]
               SET [name] = @name
                  ,[goalrange] = @goalrange
                  ,[npcpresenceid] = @npcpresenceid
                  ,[persistent] = @persistent
                  ,[minimumloot] = @minimumloot
                  ,[dynamic] = @dynamic
                WHERE id=@id");
                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@id", this.id);
                command.Parameters.AddWithValue("@name", this.name);
                command.Parameters.AddWithValue("@goalrange", this.goalrange);
                command.Parameters.AddWithValue("@npcpresenceid", Utilities.getNullableInt(this.npcpresence));
                command.Parameters.AddWithValue("@persistent", this.persistent);
                command.Parameters.AddWithValue("@minimumloot", this.minimumloot);
                command.Parameters.AddWithValue("@dynamic", this.dynamic);

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
                sqlCommand.Append(@"INSERT INTO [dbo].[artifacttypes] ([name],[goalrange],[npcpresenceid],[persistent],[minimumloot],[dynamic]) 
                VALUES (@name,@goalrange,@npcpresenceid,@persistent,@minimumloot,@dynamic);");
                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@name", this.id);
                command.Parameters.AddWithValue("@goalrange", this.goalrange);
                command.Parameters.AddWithValue("@npcpresenceid", Utilities.getNullableInt(this.npcpresence));
                command.Parameters.AddWithValue("@persistent", this.persistent);
                command.Parameters.AddWithValue("@minimumloot", this.minimumloot);
                command.Parameters.AddWithValue("@dynamic", this.dynamic);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                query = Utilities.parseCommandString(command, new List<string>(new string[] { }));//TODO
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
                sqlCommand.Append("DELETE FROM [dbo].[artifacttypes] WHERE id=@id;");
                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@id", this.id);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                query = command.CommandText;
                query = Utilities.parseCommandString(command, new List<string>(new string[] {  }));//TODO
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
