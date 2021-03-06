﻿using PerpTool.db;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perptool.db
{

    // don't hate!

    /// <summary>
    /// Table Class
    /// </summary>
    public class AggregateFields : INotifyPropertyChanged
    {
        #region privates
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
        private int privateformula;

        /// <summary>
        /// private field
        /// </summary>
        private string privatemeasurementunit;

        /// <summary>
        /// private field
        /// </summary>
        private decimal privatemeasurementmultiplier;

        /// <summary>
        /// private field
        /// </summary>
        private decimal privatemeasurementoffset;

        /// <summary>
        /// private field
        /// </summary>
        private int privatecategory;

        /// <summary>
        /// private field
        /// </summary>
        private int privatedigits;

        /// <summary>
        /// private field
        /// </summary>
        private int privatemoreisbetter;

        /// <summary>
        /// private field
        /// </summary>
        private int privateusedinconfig;

        /// <summary>
        /// private field
        /// </summary>
        private string privatenote;
        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref='aggregatefieldsTbl' /> class.
        /// </summary>
        /// <param name='connectionString'>pass the connection string to the database</param>
        public AggregateFields(string connectionString)
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
        /// Gets or sets public field formula
        /// </summary>
        public int formula
        {
            get
            {
                return this.privateformula;
            }

            set
            {
                this.privateformula = value;
                this.OnPropertyChanged("formula");
            }
        }

        /// <summary>
        /// Gets or sets public field measurementunit
        /// </summary>
        public string measurementunit
        {
            get
            {
                return this.privatemeasurementunit;
            }

            set
            {
                this.privatemeasurementunit = value;
                this.OnPropertyChanged("measurementunit");
            }
        }

        /// <summary>
        /// Gets or sets public field measurementmultiplier
        /// </summary>
        public decimal measurementmultiplier
        {
            get
            {
                return this.privatemeasurementmultiplier;
            }

            set
            {
                this.privatemeasurementmultiplier = value;
                this.OnPropertyChanged("measurementmultiplier");
            }
        }

        /// <summary>
        /// Gets or sets public field measurementoffset
        /// </summary>
        public decimal measurementoffset
        {
            get
            {
                return this.privatemeasurementoffset;
            }

            set
            {
                this.privatemeasurementoffset = value;
                this.OnPropertyChanged("measurementoffset");
            }
        }

        /// <summary>
        /// Gets or sets public field category
        /// </summary>
        public int category
        {
            get
            {
                return this.privatecategory;
            }

            set
            {
                this.privatecategory = value;
                this.OnPropertyChanged("category");
            }
        }

        /// <summary>
        /// Gets or sets public field digits
        /// </summary>
        public int digits
        {
            get
            {
                return this.privatedigits;
            }

            set
            {
                this.privatedigits = value;
                this.OnPropertyChanged("digits");
            }
        }

        /// <summary>
        /// Gets or sets public field moreisbetter
        /// </summary>
        public int moreisbetter
        {
            get
            {
                return this.privatemoreisbetter;
            }

            set
            {
                this.privatemoreisbetter = value;
                this.OnPropertyChanged("moreisbetter");
            }
        }

        /// <summary>
        /// Gets or sets public field usedinconfig
        /// </summary>
        public int usedinconfig
        {
            get
            {
                return this.privateusedinconfig;
            }

            set
            {
                this.privateusedinconfig = value;
                this.OnPropertyChanged("usedinconfig");
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
            this.formula = 0;
            this.measurementunit = string.Empty;
            this.measurementmultiplier = (decimal)0.00;
            this.measurementoffset = (decimal)0.00;
            this.privatecategory = 0;
            this.category = 0;
            this.privatedigits = 0;
            this.digits = 0;
            this.moreisbetter = 0;
            this.usedinconfig = 0;
            this.note = string.Empty;
        }


        public void GetById(int agFieldID)
        {
            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("SELECT * from aggregatefields Where id=@id;");
                    command.CommandText = sqlCommand.ToString();
                    command.Parameters.AddWithValue("@id", agFieldID);
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.id = Convert.ToInt32(reader["id"]);
                            this.name = Convert.ToString(reader["name"]);
                            this.formula = Convert.ToInt32(reader["formula"]);
                            this.measurementunit = Convert.ToString(reader["measurementunit"]);
                            this.measurementmultiplier = Convert.ToDecimal(reader["measurementmultiplier"]);
                            this.measurementoffset = Convert.ToDecimal(reader["measurementoffset"]);
                            this.category = Convert.ToInt32(reader["category"]);
                            this.digits = Convert.ToInt32(reader["digits"]);
                            this.moreisbetter = Utilities.handleNullableInt(reader["moreisbetter"]);
                            this.usedinconfig = Utilities.handleNullableInt(reader["usedinconfig"]);
                            this.note = Convert.ToString(reader["note"]);
                        }
                    }
                }
            }
        }

        public List<AggregateFields> GetAllFields()
        {
            List<AggregateFields> list = new List<AggregateFields>();
            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("SELECT * from aggregatefields;");
                    command.CommandText = sqlCommand.ToString();
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AggregateFields af = new AggregateFields(this.ConnString);
                            af.id = Convert.ToInt32(reader["id"]);
                            af.name = Convert.ToString(reader["name"]);
                            af.formula = Convert.ToInt32(reader["formula"]);
                            af.measurementunit = Convert.ToString(reader["measurementunit"]);
                            af.measurementmultiplier = Convert.ToDecimal(reader["measurementmultiplier"]);
                            af.measurementoffset = Convert.ToDecimal(reader["measurementoffset"]);
                            af.category = Convert.ToInt32(reader["category"]);
                            af.digits = Convert.ToInt32(reader["digits"]);
                            af.moreisbetter = Utilities.handleNullableInt(reader["moreisbetter"]);
                            af.usedinconfig = Utilities.handleNullableInt(reader["usedinconfig"]);
                            af.note = Convert.ToString(reader["note"]);
                            list.Add(af);
                        }
                    }
                }
            }
            return list;
        }

        public string Save(FieldValuesStuff data)
        {
            this.GetById(data.FieldId);
            string query = "";
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("UPDATE aggregatefields SET [name]=@SQLname, [formula]=@formula, [measurementunit]=@measurementunit, [measurementmultiplier]=@measurementmultiplier, [measurementoffset]=@measurementoffset, [category]=@category, [digits]=@digits, [moreisbetter]=@moreisbetter, [usedinconfig]=@usedinconfig, [note]=@note WHERE id ="+AggregateFields.IDkey+";");
                command.CommandText = sqlCommand.ToString();
                command.Parameters.AddWithValue(AggregateFields.IDkey, data.FieldId);
                command.Parameters.AddWithValue("@SQLname", data.FieldName);
                command.Parameters.AddWithValue("@formula", data.FieldFormula);
                command.Parameters.AddWithValue("@measurementunit", data.FieldUnits);
                command.Parameters.AddWithValue("@measurementmultiplier", data.FieldMultiplier);
                command.Parameters.AddWithValue("@measurementoffset", data.FieldOffset);
                command.Parameters.AddWithValue("@category", this.category);
                command.Parameters.AddWithValue("@digits", this.digits);
                command.Parameters.AddWithValue("@moreisbetter", Utilities.getNullableInt(this.moreisbetter));
                command.Parameters.AddWithValue("@usedinconfig", Utilities.getNullableInt(this.usedinconfig));
                if (this.note == null)
                {
                    command.Parameters.AddWithValue("@note", string.Empty);
                }
                else
                {
                    command.Parameters.AddWithValue("@note", this.note);
                }
                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();
                query = Utilities.parseCommandString(command, new List<string>(new string[] { AggregateFields.IDkey }));
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

        public static string IDkey = "@aggfieldID";

        public static string GetDeclStatement()
        {
            return "DECLARE "+ IDkey + " int;";
        }

        public string GetLookupStatement()
        {
            return "SET "+ IDkey + " = (SELECT TOP 1 id from aggregatefields WHERE [name] = '" + this.name + "' ORDER BY [name] DESC);";
        }
    }
}
