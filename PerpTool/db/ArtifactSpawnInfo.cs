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

    public class ArtifactSpawnInfoRecord : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public int Id { get; set; }
        public int ArtifactType { get; set; }
        public string ArtifactTypeName { get; set; }
        public int ZoneId { get; set; }
        public string ZoneName { get; set; }
        public double Rate { get; set; }
        public DBAction dBAction { get; set; }

        public ArtifactSpawnInfoRecord()
        {

        }

        public static string IDKey = "@artifactSpawnInfo";

        public static string GetDeclStatement()
        {
            return "DECLARE " + IDKey + " int;";
        }

        public string GetArtifactSpawnInfoLookupStatement()
        {
            return "SET " + IDKey + " = (SELECT id FROM artifactspawninfo WHERE zoneid=" + Zones.IDKey + " AND artifacttype=" + ArtifactTypesTable.IDKey + ");"; ;
        }


        public static ArtifactSpawnInfoRecord CreateNewForZone(ArtifactTypesTable artifactType, Zones zone)
        {
            ArtifactSpawnInfoRecord a = new ArtifactSpawnInfoRecord
            {
                ArtifactType = artifactType.id,
                ArtifactTypeName = artifactType.name,
                ZoneId = zone.id,
                ZoneName = zone.ConcatZoneIDName,
                Rate = 1.0,
                dBAction = DBAction.INSERT
            };
            return a;
        }

        public ArtifactSpawnInfo ToTable(string connString)
        {
            ArtifactSpawnInfo info = new ArtifactSpawnInfo(connString);
            info.id = this.Id;
            info.zoneid = this.ZoneId;
            info.artifacttype = this.ArtifactType;
            info.rate = this.Rate;
            return info;
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

    public class ArtifactSpawnInfo : INotifyPropertyChanged
    {

        private int privateid;
        private int privateartifacttype;
        private int privatezoneid;
        private double privaterate;

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

        public double rate
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
                        this.rate = Convert.ToDouble(reader["rate"]);
                    }
                }
                conn.Dispose();
            }
        }

        public ObservableCollection<ArtifactSpawnInfoRecord> GetAllByZone(Zones z)
        {
            ObservableCollection<ArtifactSpawnInfoRecord> spawns = new ObservableCollection<ArtifactSpawnInfoRecord>();
            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append(@"SELECT artifactspawninfo.id, artifactspawninfo.artifacttype, artifacttypes.name as ArtifactTypeName, artifactspawninfo.rate,artifactspawninfo.zoneid, CONCAT(zones.name, ' - ', zones.note) as ZoneName from artifactspawninfo
                JOIN zones ON artifactspawninfo.zoneid=zones.id
                JOIN artifacttypes ON artifacttypes.id = artifactspawninfo.artifacttype
                WHERE artifactspawninfo.zoneid=@zoneID; ");
                command.CommandText = sqlCommand.ToString();
                command.Parameters.AddWithValue("@zoneID", z.id);
                command.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ArtifactSpawnInfoRecord a = new ArtifactSpawnInfoRecord();
                        a.Id = Convert.ToInt32(reader["id"]);
                        a.ArtifactType = Convert.ToInt32(reader["artifacttype"]);
                        a.ArtifactTypeName = Convert.ToString(reader["ArtifactTypeName"]);
                        a.ZoneId = Convert.ToInt32(reader["zoneid"]);
                        a.ZoneName = Convert.ToString(reader["ZoneName"]);
                        a.Rate = Convert.ToDouble(reader["rate"]);
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
                SET [artifacttype] = " + ArtifactTypeRecord.IDKey + ",[zoneid] = " + Zones.IDKey + ",[rate] = @rate WHERE id=" + ArtifactSpawnInfoRecord.IDKey + "; ");
                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue(ArtifactSpawnInfoRecord.IDKey, this.id);
                command.Parameters.AddWithValue(ArtifactTypeRecord.IDKey, this.artifacttype);
                command.Parameters.AddWithValue(Zones.IDKey, this.zoneid);
                command.Parameters.AddWithValue("@rate", this.rate);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                query = Utilities.parseCommandString(command, new List<string>(new string[] { ArtifactSpawnInfoRecord.IDKey, Zones.IDKey }));
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
                 VALUES (" + ArtifactSpawnInfoRecord.IDKey + "," + Zones.IDKey + ",@rate);");
                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue(ArtifactSpawnInfoRecord.IDKey, this.artifacttype);
                command.Parameters.AddWithValue(Zones.IDKey, this.zoneid);
                command.Parameters.AddWithValue("@rate", this.rate);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                query = Utilities.parseCommandString(command, new List<string>(new string[] { ArtifactSpawnInfoRecord.IDKey, Zones.IDKey }));
            }
            return query;
        }

        public string Delete()
        {
            string query = "";
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("DELETE FROM [dbo].[artifactspawninfo] WHERE id=" + ArtifactSpawnInfoRecord.IDKey + ";");
                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue(ArtifactSpawnInfoRecord.IDKey, this.id);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                query = Utilities.parseCommandString(command, new List<string>(new string[] { ArtifactSpawnInfoRecord.IDKey }));
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
