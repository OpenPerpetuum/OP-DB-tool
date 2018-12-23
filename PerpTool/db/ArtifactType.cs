using System;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using PerpTool.db;
using System.Collections.Generic;

namespace PerpTool.db
{

    public class ArtifactType : INotifyPropertyChanged
    {

        private int privateid;
        private string privatename;
        private int privategoalrange;
        private int privatenpcpresenceid;
        private int privatepersistent;
        private int privateminimumloot;
        private int privatedynamic;

        public ArtifactType(string connectionString)
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

        public List<ArtifactType> GetAll()
        {
            List<ArtifactType> types = new List<ArtifactType>();
            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("SELECT * FROM artifacttypes");
                command.CommandText = sqlCommand.ToString();
                command.Parameters.AddWithValue("@id", id);
                command.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ArtifactType a = new ArtifactType(this.ConnString);
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

                query = command.CommandText;
                foreach (SqlParameter p in command.Parameters)
                {
                    if (p.ParameterName == "@lootdefinitionID" || p.ParameterName == "@artifacttype")
                    {
                        continue;
                    }
                    else if (SqlDbType.NVarChar.Equals(p.SqlDbType) || SqlDbType.VarChar.Equals(p.SqlDbType))
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
