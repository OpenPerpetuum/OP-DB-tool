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
    /// <summary>
    /// Table Class
    /// </summary>
    class AggregateModifiers : INotifyPropertyChanged
    {
        /// <summary>
        /// private field
        /// </summary>
        private int privatecategoryflag;

        /// <summary>
        /// private field
        /// </summary>
        private int privatebasefield;

        /// <summary>
        /// private field
        /// </summary>
        private int privatemodifierfield;

        /// <summary>
        /// Initializes a new instance of the <see cref='AggregateModifiers' /> class.
        /// </summary>
        /// <param name='connectionString'>pass the connection string to the database</param>
        public AggregateModifiers(string connectionString)
        {
            this.ConnString = connectionString;
        }

        /// <summary>
        /// Event handler for properties
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        #region Fields
        /// <summary>
        /// Gets or sets public field categoryflag
        /// </summary>
        public int categoryflag
        {
            get
            {
                return this.privatecategoryflag;
            }

            set
            {
                this.privatecategoryflag = value;
                this.OnPropertyChanged("categoryflag");
            }
        }

        /// <summary>
        /// Gets or sets public field basefield
        /// </summary>
        public int basefield
        {
            get
            {
                return this.privatebasefield;
            }

            set
            {
                this.privatebasefield = value;
                this.OnPropertyChanged("basefield");
            }
        }

        /// <summary>
        /// Gets or sets public field modifierfield
        /// </summary>
        public int modifierfield
        {
            get
            {
                return this.privatemodifierfield;
            }

            set
            {
                this.privatemodifierfield = value;
                this.OnPropertyChanged("modifierfield");
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
            this.privatecategoryflag = 0;
            this.categoryflag = 0;
            this.privatebasefield = 0;
            this.basefield = 0;
            this.privatemodifierfield = 0;
            this.modifierfield = 0;
        }

        /// <summary>
        /// saves a new record
        /// </summary>
        public void SaveNewRecord()
        {
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("Insert into aggregatemodifiers ");
                sqlCommand.Append("(`categoryflag`, `basefield`, `modifierfield`) ");
                sqlCommand.Append(" Values ");
                sqlCommand.Append("(@categoryflag, @basefield, @modifierfield) ");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@categoryflag", this.categoryflag);
                command.Parameters.AddWithValue("@basefield", this.basefield);
                command.Parameters.AddWithValue("@modifierfield", this.modifierfield);

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
                sqlCommand.Append("UPDATE aggregatemodifiers Set `basefield`=@basefield, `modifierfield`=@modifierfield where `categoryflag`=@categoryflag and `@basefield`=@basefield and `modifierfield`=@modifierfield");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@categoryflag", this.categoryflag);
                command.Parameters.AddWithValue("@basefield", this.basefield);
                command.Parameters.AddWithValue("@modifierfield", this.modifierfield);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        ///// <summary>
        ///// gets a record by its record id
        ///// </summary>
        ///// <param name='id number'>id number</param>
        //public void GetById(int categoryflag)
        //{
        //    SqlConnection conn = new SqlConnection(this.ConnString);
        //    using (SqlCommand command = new SqlCommand())
        //    {
        //        StringBuilder sqlCommand = new StringBuilder();
        //        sqlCommand.Append("SELECT * from aggregatemodifiers Where categoryflag=@categoryflag");
        //        command.CommandText = sqlCommand.ToString();
        //        command.Parameters.AddWithValue("@categoryflag", categoryflag);
        //        command.Connection = conn;
        //        conn.Open();
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                this.categoryflag = Convert.ToInt32(reader["categoryflag"]);
        //                this.basefield = Convert.ToInt32(reader["basefield"]);
        //                this.modifierfield = Convert.ToInt32(reader["modifierfield"]);
        //            }
        //        }

        //        conn.Dispose();
        //    }
        //}

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