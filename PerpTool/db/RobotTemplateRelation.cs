using PerpTool.db;
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
        private int _definition;
        private string _definitionname;
        private int _templateid;
        private string _templatename;
        private int _itemscoresum;
        private int _raceid;
        private int _missionlevel;
        private int _missionleveloverride;
        private int _killep;
        private string _note;

        public int definition { get { return _definition; } set { _definition = value; OnPropertyChanged("definition"); } }
        public string definitionname { get { return _definitionname; } set { _definitionname = value; OnPropertyChanged("definitionname"); } }
        public int templateid { get { return _templateid; } set { _templateid = value; OnPropertyChanged("templateid"); } }
        public string templatename { get { return _templatename; } set { _templatename = value; OnPropertyChanged("templatename"); } }
        public int itemscoresum { get { return _itemscoresum; } set { _itemscoresum = value; OnPropertyChanged("itemscoresum"); } }
        public int raceid { get { return _raceid; } set { _raceid = value; OnPropertyChanged("raceid"); } }
        public int missionlevel { get { return _missionlevel; } set { _missionlevel = value; OnPropertyChanged("missionlevel"); } }
        public int missionleveloverride { get { return _missionleveloverride; } set { _missionleveloverride = value; OnPropertyChanged("missionleveloverride"); } }
        public int killep { get { return _killep; } set { _killep = value; OnPropertyChanged("killep"); } }
        public string note { get { return _note; } set { _note = value; OnPropertyChanged("note"); } }
        public DBAction dBAction;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public bool isEmpty()
        {
            bool e = this.definition == 0;
            e = this.definitionname == null && e;
            e = this.templateid == 0 && e;
            e = this.templatename == null && e;
            e = this.itemscoresum == 0 && e;
            e = this.raceid == 0 && e;
            e = this.missionlevel == 0 && e;
            e = this.missionleveloverride == 0 && e;
            e = this.killep == 0 && e;
            e = this.note == null && e;
            return e;
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
                sqlCommand.Append(@"UPDATE [dbo].[robottemplaterelation] SET [templateid] = @templateID,[itemscoresum] = @itemscoresum,[raceid] = @raceid,
                [missionlevel] = @missionlevel,[missionleveloverride] = @levelOverride,[killep] = @killep ,[note] = @note WHERE [definition] = @definitionID;");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@definitionID", item.definition);
                command.Parameters.AddWithValue("@templateID", item.templateid);
                command.Parameters.AddWithValue("@itemscoresum", item.itemscoresum);
                command.Parameters.AddWithValue("@raceid", item.raceid);
                command.Parameters.AddWithValue("@missionlevel", Utilities.getNullableInt(item.missionlevel));
                command.Parameters.AddWithValue("@levelOverride", Utilities.getNullableInt(item.missionleveloverride));
                command.Parameters.AddWithValue("@killep", Utilities.getNullableInt(item.killep));
                command.Parameters.AddWithValue("@note", item.note);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                query = command.CommandText;
                foreach (SqlParameter p in command.Parameters)
                {
                    if (p.ParameterName == "@definitionID" || p.ParameterName == "@templateID")
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

        public string Insert(BotTemplateRelation item)
        {
            string query = "";
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append(@"INSERT INTO [dbo].[robottemplaterelation] ([definition],[templateid],[itemscoresum],[raceid],[missionlevel],[missionleveloverride],[killep],[note])
                VALUES (@definitionID,@templateID,@itemscoresum,@raceid,@missionlevel,@levelOverride,@killep,@note);");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@definitionID", item.definition);
                command.Parameters.AddWithValue("@templateID", item.templateid);
                command.Parameters.AddWithValue("@itemscoresum", item.itemscoresum);
                command.Parameters.AddWithValue("@raceid", item.raceid);
                command.Parameters.AddWithValue("@missionlevel", Utilities.getNullableInt(item.missionlevel));
                command.Parameters.AddWithValue("@levelOverride", Utilities.getNullableInt(item.missionleveloverride));
                command.Parameters.AddWithValue("@killep", Utilities.getNullableInt(item.killep));
                command.Parameters.AddWithValue("@note", item.note);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                query = command.CommandText;
                foreach (SqlParameter p in command.Parameters)
                {
                    if (p.ParameterName != "@definitionID" && p.ParameterName != "@templateID")
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
                            temprelation.missionlevel = Utilities.handleNullableInt(reader["missionlevel"]);
                            temprelation.missionleveloverride = Utilities.handleNullableInt(reader["missionleveloverride"]);
                            temprelation.killep = Utilities.handleNullableInt(reader["killep"]);
                            temprelation.note = Convert.ToString(reader["note"]);
                        }
                    }
                }
            }
            return temprelation;
        }


        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}