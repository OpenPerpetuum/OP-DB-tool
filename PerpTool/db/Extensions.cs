using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerpTool.db;

namespace PerpTool.db
{
    public class Extensions : INotifyPropertyChanged
    {
        private int _extensionid;
        private string _extensionname;
        private int _category;
        private int _rank;
        private string _targetlearningattribute;
        private string _learningattributeprimary;
        private string _learningattributesecondary;
        private float _bonus;
        private string _note;
        private int _price;
        private int _active;
        private string _description;
        private int _targetpropertyID;
        private int _effectenhancer;
        private int _hidden;
        private int _freezelimit;

        public int extensionid { get { return this._extensionid; } set { this._extensionid = value; OnPropertyChanged("extensionid"); } }
        public string extensionname { get { return this._extensionname; } set { this._extensionname = value; OnPropertyChanged("extensionname"); } }
        public int category { get { return this._category; } set { this._category = value; OnPropertyChanged("category"); } }
        public int rank { get { return this._rank; } set { this._rank = value; OnPropertyChanged("rank"); } }
        public string targetlearningattribute { get { return this._targetlearningattribute; } set { this._targetlearningattribute = value; OnPropertyChanged("targetlearningattribute"); } }
        public string learningattributeprimary { get { return this._learningattributeprimary; } set { this._learningattributeprimary = value; OnPropertyChanged("learningattributeprimary"); } }
        public string learningattributesecondary { get { return this._learningattributesecondary; } set { this._learningattributesecondary = value; OnPropertyChanged("learningattributesecondary"); } }
        public float bonus { get { return this._bonus; } set { this._bonus = value; OnPropertyChanged("bonus"); } }
        public string note { get { return this._note; } set { this._note = value; OnPropertyChanged("note"); } }
        public int price { get { return this._price; } set { this._price = value; OnPropertyChanged("price"); } }
        public int active { get { return this._active; } set { this._active = value; OnPropertyChanged("active"); } }
        public string description { get { return this._description; } set { this._description = value; OnPropertyChanged("description"); } }
        public int targetpropertyID { get { return this._targetpropertyID; } set { this._targetpropertyID = value; OnPropertyChanged("targetpropertyID"); } }
        public int effectenhancer { get { return this._effectenhancer; } set { this._effectenhancer = value; OnPropertyChanged("effectenhancer"); } }
        public int hidden { get { return this._hidden; } set { this._hidden = value; OnPropertyChanged("hidden"); } }
        public int freezelimit { get { return this._freezelimit; } set { this._freezelimit = value; OnPropertyChanged("freezelimit"); } }

        private string ConnString;


        public Extensions(string connstr)
        {
            this.ConnString = connstr;
        }

        public void Clear()
        {
            this.active = 0;
            this.bonus = 0;
            this.category = 0;
            this.extensionid = 0;
            this.extensionname = string.Empty;
            this.freezelimit = 0;
            this.hidden = 0;
            this.learningattributeprimary = string.Empty;
            this.learningattributesecondary = string.Empty;
            this.note = string.Empty;
            this.price = 0;
            this.rank = 0;
            this.targetlearningattribute = string.Empty;
            this.targetpropertyID = 0;
        }

        public Extensions GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("SELECT * FROM extensions WHERE extensionid=@id;");
                    command.CommandText = sqlCommand.ToString();
                    command.Parameters.AddWithValue("@id", id);
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.extensionid = Convert.ToInt32(reader["extensionid"]);
                            this.active = Convert.ToInt32(reader["active"]);
                            this.bonus = Convert.ToSingle(reader["bonus"]);
                            this.category = Convert.ToInt32(reader["category"]);
                            this.description = Convert.ToString(reader["description"]);
                            this.effectenhancer = Convert.ToInt32(reader["effectenhancer"]);
                            this.extensionname = Convert.ToString(reader["extensionname"]);
                            this.freezelimit = Convert.ToInt32(reader["freezelimit"]);
                            this.hidden = Convert.ToInt32(reader["hidden"]);
                            this.learningattributeprimary = Convert.ToString(reader["learningattributeprimary"]);
                            this.learningattributesecondary = Convert.ToString(reader["learningattributesecondary"]);
                            this.note = Convert.ToString(reader["note"]);
                            this.price = Convert.ToInt32(reader["price"]);
                            this.rank = Convert.ToInt32(reader["rank"]);
                            this.targetlearningattribute = Convert.ToString(reader["targetlearningattribute"]);
                            this.targetpropertyID = Convert.ToInt32(reader["targetpropertyID"]);
                            return this;

                        }
                    }
                }
            }
            return null;
        }


        public List<Extensions> GetAll()
        {
            List<Extensions> allExtensions = new List<Extensions>();
            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("SELECT * FROM extensions;");
                    command.CommandText = sqlCommand.ToString();
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Extensions ext = new Extensions(this.ConnString);
                            ext.extensionid = Convert.ToInt32(reader["extensionid"]);
                            ext.active = Convert.ToInt32(reader["active"]);
                            ext.bonus = Convert.ToSingle(reader["bonus"]);
                            ext.category = Convert.ToInt32(reader["category"]);
                            ext.description = Convert.ToString(reader["description"]);
                            ext.effectenhancer = Convert.ToInt32(reader["effectenhancer"]);
                            ext.extensionname = Convert.ToString(reader["extensionname"]);
                            ext.freezelimit = Utilities.handleNullableInt(reader["freezelimit"]);
                            ext.hidden = Convert.ToInt32(reader["hidden"]);
                            ext.learningattributeprimary = Convert.ToString(reader["learningattributeprimary"]);
                            ext.learningattributesecondary = Convert.ToString(reader["learningattributesecondary"]);
                            ext.note = Convert.ToString(reader["note"]);
                            ext.price = Convert.ToInt32(reader["price"]);
                            ext.rank = Convert.ToInt32(reader["rank"]);
                            ext.targetlearningattribute = Convert.ToString(reader["targetlearningattribute"]);
                            ext.targetpropertyID = Utilities.handleNullableInt(reader["targetpropertyID"]);
                            allExtensions.Add(ext);
                        }
                    }
                }
            }
            return allExtensions;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public static string IDkey = "@extensionID";

        public static string GetDeclStatement()
        {
            return "DECLARE "+IDkey+" int;";
        }

        public string GetLookupStatement()
        {
            return "SET " + IDkey + " = (SELECT TOP 1 extensionid from extensions WHERE [extensionname] = '" + this.extensionname+ "' ORDER BY [extensionname] DESC);";
        }
    }
}
