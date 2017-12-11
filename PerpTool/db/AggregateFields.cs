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

    // don't hate!

    public class AgFieldsValues
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal value { get; set; }
    }

    /// <summary>
    /// Table Class
    /// </summary>
    class AggregateFields : INotifyPropertyChanged
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

        /// <summary>
        /// saves a new record
        /// </summary>
        public void SaveNewRecord()
        {
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("Insert into aggregatefields ");
                sqlCommand.Append("(`id`, `name`, `formula`, `measurementunit`, `measurementmultiplier`, `measurementoffset`, `category`, `digits`, `moreisbetter`, `usedinconfig`, `note`) ");
                sqlCommand.Append(" Values ");
                sqlCommand.Append("(@id, @name, @formula, @measurementunit, @measurementmultiplier, @measurementoffset, @category, @digits, @moreisbetter, @usedinconfig, @note) ");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@id", this.id);
                command.Parameters.AddWithValue("@name", this.name);
                command.Parameters.AddWithValue("@formula", this.formula);
                command.Parameters.AddWithValue("@measurementunit", this.measurementunit);
                command.Parameters.AddWithValue("@measurementmultiplier", this.measurementmultiplier);
                command.Parameters.AddWithValue("@measurementoffset", this.measurementoffset);
                command.Parameters.AddWithValue("@category", this.category);
                command.Parameters.AddWithValue("@digits", this.digits);
                command.Parameters.AddWithValue("@moreisbetter", this.moreisbetter);
                command.Parameters.AddWithValue("@usedinconfig", this.usedinconfig);
                command.Parameters.AddWithValue("@note", this.note);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
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
                sqlCommand.Append("UPDATE aggregatefields Set `name`= @name, `formula`= @formula, `measurementunit`= @measurementunit, `measurementmultiplier`= @measurementmultiplier, `measurementoffset`= @measurementoffset, `category`= @category, `digits`= @digits, `moreisbetter`= @moreisbetter, `usedinconfig`= @usedinconfig, `note`= @note where id = @id");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@id", this.id);
                command.Parameters.AddWithValue("@name", this.name);
                command.Parameters.AddWithValue("@formula", this.formula);
                command.Parameters.AddWithValue("@measurementunit", this.measurementunit);
                command.Parameters.AddWithValue("@measurementmultiplier", this.measurementmultiplier);
                command.Parameters.AddWithValue("@measurementoffset", this.measurementoffset);
                command.Parameters.AddWithValue("@category", this.category);
                command.Parameters.AddWithValue("@digits", this.digits);
                command.Parameters.AddWithValue("@moreisbetter", this.moreisbetter);
                command.Parameters.AddWithValue("@usedinconfig", this.usedinconfig);
                command.Parameters.AddWithValue("@note", this.note);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();
            }
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
