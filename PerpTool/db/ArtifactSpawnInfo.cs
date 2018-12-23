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

    public class ArtifactSpawnInfo : INotifyPropertyChanged
    {

        private int privateid;
        private int privateartifacttype;
        private int privatezoneid;
        private int privaterate;

        public ArtifactSpawnInfo(string connectionString)
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

        public int artifacttype
        {
            get
            {
                return this.privateartifacttype;
            }

            set
            {
                this.privateartifacttype = value;
                this.OnPropertyChanged("artifacttype");
            }
        }

        public int zoneid
        {
            get
            {
                return this.privatezoneid;
            }

            set
            {
                this.privatezoneid = value;
                this.OnPropertyChanged("zoneid");
            }
        }

        public int rate
        {
            get
            {
                return this.privaterate;
            }

            set
            {
                this.privaterate = value;
                this.OnPropertyChanged("rate");
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
            this.artifacttype = 0;
            this.zoneid = 0;
            this.rate = 0;
        }


        public void GetbyID(int id)
        {
            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("SELECT * FROM artifactspawninfo WHERE id=@id;");
                command.CommandText = sqlCommand.ToString();
                command.Parameters.AddWithValue("@id", id);
                command.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.id = Convert.ToInt32(reader["id"]);
                        this.artifacttype = Convert.ToInt32(reader["artifacttype"]);
                        this.zoneid = Convert.ToInt32(reader["zoneid"]);
                        this.rate = Convert.ToInt32(reader["rate"]);
                    }
                }
                conn.Dispose();
            }
        }

        public List<ArtifactSpawnInfo> GetAll()
        {
            List<ArtifactSpawnInfo> spawns = new List<ArtifactSpawnInfo>();
            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("SELECT * FROM artifactspawninfo");
                command.CommandText = sqlCommand.ToString();
                command.Parameters.AddWithValue("@id", id);
                command.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ArtifactSpawnInfo a = new ArtifactSpawnInfo(this.ConnString);
                        a.id = Convert.ToInt32(reader["id"]);
                        a.artifacttype = Convert.ToInt32(reader["artifacttype"]);
                        a.zoneid = Convert.ToInt32(reader["zoneid"]);
                        a.rate = Convert.ToInt32(reader["rate"]);
                        spawns.Add(a);
                    }
                }
                conn.Dispose();
            }
            return spawns;
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
                sqlCommand.Append(@"UPDATE [dbo].[artifactspawninfo]
                SET [artifacttype] = @artifacttype
                ,[zoneid] = @zoneid
                ,[rate] = @rate
                WHERE id=@id; ");
                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@id", this.id);
                command.Parameters.AddWithValue("@artifacttype", this.artifacttype);
                command.Parameters.AddWithValue("@zoneid", this.zoneid);
                command.Parameters.AddWithValue("@rate", this.rate);

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
                sqlCommand.Append(@"INSERT INTO [dbo].[artifactspawninfo] ([artifacttype],[zoneid],[rate])
                 VALUES (@artifacttype,@zoneid,@rate);");
                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@name", this.id);
                command.Parameters.AddWithValue("@artifacttype", this.artifacttype);
                command.Parameters.AddWithValue("@zoneid", this.zoneid);
                command.Parameters.AddWithValue("@rate", this.rate);

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

        public string Delete()
        {
            string query = "";
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("DELETE FROM [dbo].[artifactspawninfo] WHERE id=@id;");
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
