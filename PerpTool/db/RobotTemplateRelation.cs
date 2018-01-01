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

    public class BotTemplateRelation : INotifyPropertyChanged
    {
        public int definition { get; set; }
        public string definitionname { get; set; }
        public int templateid { get; set; }
        public string templatename { get; set; }
        public int itemscoresum { get; set; }
        public int raceid { get; set; }
        public int missionlevel { get; set; }
        public int missionleveloverride { get; set; }
        public int killep { get; set; }
        public string note { get; set; }
        //NULLABLE INTS FLAGGED AS -1
        //CHECK ON SAVE/UPDATE - SAVE DB.NULL

        public object getNullableInt(int v)
        {
            if (v < 0)
            {
                return (object)DBNull.Value;
            }
            return v;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    class RobotTemplateRelation : INotifyPropertyChanged
    {

        private int privatedefinition;
        private int privatetemplateid;
        private int privateitemscoresum;
        private int privateraceid;
        private int privatemissionlevel;
        private int privatemissionleveloverride;
        private int privatekillep;
        private string privatenote;
        public RobotTemplateRelation(string connectionString)
        {
            this.ConnString = connectionString;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        #region Fields
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
        public int templateid
        {
            get
            {
                return this.privatetemplateid;
            }

            set
            {
                this.privatetemplateid = value;
                this.OnPropertyChanged("templateid");
            }
        }
        public int itemscoresum
        {
            get
            {
                return this.privateitemscoresum;
            }

            set
            {
                this.privateitemscoresum = value;
                this.OnPropertyChanged("itemscoresum");
            }
        }
        public int raceid
        {
            get
            {
                return this.privateraceid;
            }

            set
            {
                this.privateraceid = value;
                this.OnPropertyChanged("raceid");
            }
        }
        public int missionlevel
        {
            get
            {
                return this.privatemissionlevel;
            }

            set
            {
                this.privatemissionlevel = value;
                this.OnPropertyChanged("missionlevel");
            }
        }
        public int missionleveloverride
        {
            get
            {
                return this.privatemissionleveloverride;
            }

            set
            {
                this.privatemissionleveloverride = value;
                this.OnPropertyChanged("missionleveloverride");
            }
        }
        public int killep
        {
            get
            {
                return this.privatekillep;
            }

            set
            {
                this.privatekillep = value;
                this.OnPropertyChanged("killep");
            }
        }
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

        private string ConnString { get; set; }

        #endregion

        public void Clear()
        {
            this.privatedefinition = 0;
            this.privatetemplateid = 0;
            this.privateitemscoresum = 0;
            this.privateraceid = 0;
            this.privatemissionlevel = 0;
            this.privatemissionleveloverride = 0;
            this.privatekillep = 0;
            this.privatenote = "";
        }

        public string Save(BotTemplateRelation item)
        {
            string query = "";
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append(@"UPDATE [dbo].[robottemplaterelation] SET [templateid] = @templateid,[itemscoresum] = @itemscoresum,[raceid] = @raceid,
                [missionlevel] = @missionlevel,[missionleveloverride] = @levelOverride,[killep] = @killep ,[note] = @note WHERE [definition] = @definition;");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@definition", item.definition);
                command.Parameters.AddWithValue("@templateid", item.templateid);
                command.Parameters.AddWithValue("@itemscoresum", item.itemscoresum);
                command.Parameters.AddWithValue("@raceid", item.raceid);
                command.Parameters.AddWithValue("@missionlevel", item.getNullableInt(item.missionlevel));
                command.Parameters.AddWithValue("@levelOverride", item.getNullableInt(item.missionleveloverride));
                command.Parameters.AddWithValue("@killep", item.getNullableInt(item.killep));
                command.Parameters.AddWithValue("@note", item.note);

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

        public BotTemplateRelation GetById(int id)
        {
            BotTemplateRelation temprelation = new BotTemplateRelation();
            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append(@"SELECT robottemplaterelation.definition,entitydefaults.definitionname,templateid
	                ,robottemplates.name,itemscoresum,raceid,missionlevel,missionleveloverride,killep,robottemplaterelation.note 
	                FROM perpetuumsa.dbo.robottemplaterelation
                    join robottemplates on robottemplates.id=robottemplaterelation.templateid
                    join entitydefaults on entitydefaults.definition=robottemplaterelation.definition
                    WHERE robottemplaterelation.definition=@id;");
                    command.CommandText = sqlCommand.ToString();
                    command.Parameters.AddWithValue("@id", id);
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            temprelation.definition = Convert.ToInt32(reader["definition"]);
                            temprelation.definitionname = Convert.ToString(reader["definitionname"]);
                            temprelation.templateid = Convert.ToInt32(reader["templateid"]);
                            temprelation.templatename = Convert.ToString(reader["name"]);
                            temprelation.itemscoresum = Convert.ToInt32(reader["itemscoresum"]);
                            temprelation.raceid = Convert.ToInt32(reader["raceid"]);
                            temprelation.missionlevel = handleNullableInt(reader["missionlevel"]);
                            temprelation.missionleveloverride = handleNullableInt(reader["missionleveloverride"]);
                            temprelation.killep = handleNullableInt(reader["killep"]);
                            temprelation.note = Convert.ToString(reader["note"]);
                        }
                    }
                }
            }
            return temprelation;
        }

        private int handleNullableInt(object readValue)
        {
            if (DBNull.Value==readValue)
            {
                return -1;
            }
            return Convert.ToInt32(readValue);
        }

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}