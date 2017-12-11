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
    public class Accounts : INotifyPropertyChanged
    {
        /// <summary>
        /// private field
        /// </summary>
        private int privateaccountid;

        /// <summary>
        /// private field
        /// </summary>
        private string privateemail;

        /// <summary>
        /// private field
        /// </summary>
        private string privatepassword;

        /// <summary>
        /// private field
        /// </summary>
        private string privatefirstname;

        /// <summary>
        /// private field
        /// </summary>
        private string privatelastname;

        /// <summary>
        /// private field
        /// </summary>
        private System.DateTime privateborn;

        /// <summary>
        /// private field
        /// </summary>
        private int privatestate;

        /// <summary>
        /// private field
        /// </summary>
        private int privateacclevel;

        /// <summary>
        /// private field
        /// </summary>
        private int privatetotalminsonline;

        /// <summary>
        /// private field
        /// </summary>
        private System.DateTime privatelastloggedin;

        /// <summary>
        /// private field
        /// </summary>
        private System.DateTime privatecreation;

        /// <summary>
        /// private field
        /// </summary>
        private int privateclienttype;

        /// <summary>
        /// private field
        /// </summary>
        private int privateisloggedin;

        /// <summary>
        /// private field
        /// </summary>
        private DateTime privatebantime;

        /// <summary>
        /// private field
        /// </summary>
        private int privatebanlength;

        /// <summary>
        /// private field
        /// </summary>
        private string privatebannote;

        /// <summary>
        /// private field
        /// </summary>
        private int privateemailconfirmed;

        /// <summary>
        /// private field
        /// </summary>
        private DateTime privatefirstcharacter;

        /// <summary>
        /// private field
        /// </summary>
        private string privatenote;

        /// <summary>
        /// private field
        /// </summary>
        private string privatesteamid;

        /// <summary>
        /// private field
        /// </summary>
        private string privatetwitchauthtoken;

        /// <summary>
        /// private field
        /// </summary>
        private int privatecredit;

        /// <summary>
        /// private field
        /// </summary>
        private int privateisactive;

        /// <summary>
        /// private field
        /// </summary>
        private int privateresetcount;

        /// <summary>
        /// private field
        /// </summary>
        private int privatewasreset;

        /// <summary>
        /// private field
        /// </summary>
        private DateTime privatevaliduntil;

        /// <summary>
        /// private field
        /// </summary>
        private int privatepayingcustomer;

        /// <summary>
        /// private field
        /// </summary>
        private string privatecampaignid;

        /// <summary>
        /// Initializes a new instance of the <see cref='Accounts' /> class.
        /// </summary>
        /// <param name='connectionString'>pass the connection string to the database</param>
        public Accounts(string connectionString)
        {
            this.ConnString = connectionString;
        }

        /// <summary>
        /// Event handler for properties
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #region Fields
        /// <summary>
        /// Gets or sets public field accountID
        /// </summary>
        public int accountID
        {
            get
            {
                return this.privateaccountid;
            }

            set
            {
                this.privateaccountid = value;
                this.OnPropertyChanged("accountID");
            }
        }

        /// <summary>
        /// Gets or sets public field email
        /// </summary>
        public string email
        {
            get
            {
                return this.privateemail;
            }

            set
            {
                this.privateemail = value;
                this.OnPropertyChanged("email");
            }
        }

        /// <summary>
        /// Gets or sets public field password
        /// </summary>
        public string password
        {
            get
            {
                return this.privatepassword;
            }

            set
            {
                this.privatepassword = value;
                this.OnPropertyChanged("password");
            }
        }

        /// <summary>
        /// Gets or sets public field firstName
        /// </summary>
        public string firstName
        {
            get
            {
                return this.privatefirstname;
            }

            set
            {
                this.privatefirstname = value;
                this.OnPropertyChanged("firstName");
            }
        }

        /// <summary>
        /// Gets or sets public field lastName
        /// </summary>
        public string lastName
        {
            get
            {
                return this.privatelastname;
            }

            set
            {
                this.privatelastname = value;
                this.OnPropertyChanged("lastName");
            }
        }

        /// <summary>
        /// Gets or sets public field born
        /// </summary>
        public System.DateTime born
        {
            get
            {
                return this.privateborn;
            }

            set
            {
                this.privateborn = value;
                this.OnPropertyChanged("born");
            }
        }

        /// <summary>
        /// Gets or sets public field state
        /// </summary>
        public int state
        {
            get
            {
                return this.privatestate;
            }

            set
            {
                this.privatestate = value;
                this.OnPropertyChanged("state");
            }
        }

        /// <summary>
        /// Gets or sets public field accLevel
        /// </summary>
        public int accLevel
        {
            get
            {
                return this.privateacclevel;
            }

            set
            {
                this.privateacclevel = value;
                this.OnPropertyChanged("accLevel");
            }
        }

        /// <summary>
        /// Gets or sets public field totalMinsOnline
        /// </summary>
        public int totalMinsOnline
        {
            get
            {
                return this.privatetotalminsonline;
            }

            set
            {
                this.privatetotalminsonline = value;
                this.OnPropertyChanged("totalMinsOnline");
            }
        }

        /// <summary>
        /// Gets or sets public field lastLoggedIn
        /// </summary>
        public System.DateTime lastLoggedIn
        {
            get
            {
                return this.privatelastloggedin;
            }

            set
            {
                this.privatelastloggedin = value;
                this.OnPropertyChanged("lastLoggedIn");
            }
        }

        /// <summary>
        /// Gets or sets public field creation
        /// </summary>
        public System.DateTime creation
        {
            get
            {
                return this.privatecreation;
            }

            set
            {
                this.privatecreation = value;
                this.OnPropertyChanged("creation");
            }
        }

        /// <summary>
        /// Gets or sets public field clientType
        /// </summary>
        public int clientType
        {
            get
            {
                return this.privateclienttype;
            }

            set
            {
                this.privateclienttype = value;
                this.OnPropertyChanged("clientType");
            }
        }

        /// <summary>
        /// Gets or sets public field isLoggedIn
        /// </summary>
        public int isLoggedIn
        {
            get
            {
                return this.privateisloggedin;
            }

            set
            {
                this.privateisloggedin = value;
                this.OnPropertyChanged("isLoggedIn");
            }
        }

        /// <summary>
        /// Gets or sets public field bantime
        /// </summary>
        public System.DateTime bantime
        {
            get
            {
                return this.privatebantime;
            }

            set
            {
                this.privatebantime = value;
                this.OnPropertyChanged("bantime");
            }
        }

        /// <summary>
        /// Gets or sets public field banlength
        /// </summary>
        public int banlength
        {
            get
            {
                return this.privatebanlength;
            }

            set
            {
                this.privatebanlength = value;
                this.OnPropertyChanged("banlength");
            }
        }

        /// <summary>
        /// Gets or sets public field bannote
        /// </summary>
        public string bannote
        {
            get
            {
                return this.privatebannote;
            }

            set
            {
                this.privatebannote = value;
                this.OnPropertyChanged("bannote");
            }
        }

        /// <summary>
        /// Gets or sets public field emailConfirmed
        /// </summary>
        public int emailConfirmed
        {
            get
            {
                return this.privateemailconfirmed;
            }

            set
            {
                this.privateemailconfirmed = value;
                this.OnPropertyChanged("emailConfirmed");
            }
        }

        /// <summary>
        /// Gets or sets public field firstcharacter
        /// </summary>
        public System.DateTime firstcharacter
        {
            get
            {
                return this.privatefirstcharacter;
            }

            set
            {
                this.privatefirstcharacter = value;
                this.OnPropertyChanged("firstcharacter");
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
        /// Gets or sets public field steamID
        /// </summary>
        public string steamID
        {
            get
            {
                return this.privatesteamid;
            }

            set
            {
                this.privatesteamid = value;
                this.OnPropertyChanged("steamID");
            }
        }

        /// <summary>
        /// Gets or sets public field twitchAuthToken
        /// </summary>
        public string twitchAuthToken
        {
            get
            {
                return this.privatetwitchauthtoken;
            }

            set
            {
                this.privatetwitchauthtoken = value;
                this.OnPropertyChanged("twitchAuthToken");
            }
        }

        /// <summary>
        /// Gets or sets public field credit
        /// </summary>
        public int credit
        {
            get
            {
                return this.privatecredit;
            }

            set
            {
                this.privatecredit = value;
                this.OnPropertyChanged("credit");
            }
        }

        /// <summary>
        /// Gets or sets public field isactive
        /// </summary>
        public int isactive
        {
            get
            {
                return this.privateisactive;
            }

            set
            {
                this.privateisactive = value;
                this.OnPropertyChanged("isactive");
            }
        }

        /// <summary>
        /// Gets or sets public field resetcount
        /// </summary>
        public int resetcount
        {
            get
            {
                return this.privateresetcount;
            }

            set
            {
                this.privateresetcount = value;
                this.OnPropertyChanged("resetcount");
            }
        }

        /// <summary>
        /// Gets or sets public field wasreset
        /// </summary>
        public int wasreset
        {
            get
            {
                return this.privatewasreset;
            }

            set
            {
                this.privatewasreset = value;
                this.OnPropertyChanged("wasreset");
            }
        }

        /// <summary>
        /// Gets or sets public field validUntil
        /// </summary>
        public System.DateTime validUntil
        {
            get
            {
                return this.privatevaliduntil;
            }

            set
            {
                this.privatevaliduntil = value;
                this.OnPropertyChanged("validUntil");
            }
        }

        /// <summary>
        /// Gets or sets public field payingcustomer
        /// </summary>
        public int payingcustomer
        {
            get
            {
                return this.privatepayingcustomer;
            }

            set
            {
                this.privatepayingcustomer = value;
                this.OnPropertyChanged("payingcustomer");
            }
        }

        /// <summary>
        /// Gets or sets public field campaignid
        /// </summary>
        public string campaignid
        {
            get
            {
                return this.privatecampaignid;
            }

            set
            {
                this.privatecampaignid = value;
                this.OnPropertyChanged("campaignid");
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
            this.accountID = 0;
            this.email = string.Empty;
            this.password = string.Empty;
            this.firstName = string.Empty;
            this.lastName = string.Empty;
            this.born = DateTime.MinValue;
            this.state = 0;
            this.accLevel = 0;
            this.totalMinsOnline = 0;
            this.lastLoggedIn = DateTime.MinValue;
            this.creation = DateTime.MinValue;
            this.clientType = 0;
            this.isLoggedIn = 0;
            this.bantime = DateTime.MinValue;
            this.banlength = 0;
            this.bannote = string.Empty;
            this.emailConfirmed = 0;
            this.firstcharacter = DateTime.MinValue;
            this.note = string.Empty;
            this.steamID = string.Empty;
            this.twitchAuthToken = string.Empty;
            this.credit = 0;
            this.isactive = 0;
            this.resetcount = 0;
            this.wasreset = 0;
            this.validUntil = DateTime.MinValue;
            this.payingcustomer = 0;
            this.campaignid = string.Empty;
        }

        /// <summary>
        /// saves a new record
        /// </summary>
        public void SaveNewRecord()
        {
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("Insert into accounts ");
                sqlCommand.Append("(`accountID`, `email`, `password`, `firstName`, `lastName`, `born`, `state`, `accLevel`, `totalMinsOnline`, `lastLoggedIn`, `creation`, `clientType`, `isLoggedIn`, `bantime`, `banlength`, `bannote`, `emailConfirmed`, `firstcharacter`, `note`, `steamID`, `twitchAuthToken`, `credit`, `isactive`, `resetcount`, `wasreset`, `validUntil`, `payingcustomer`, `campaignid`) ");
                sqlCommand.Append(" Values ");
                sqlCommand.Append("(@accountID, @email, @password, @firstName, @lastName, @born, @state, @accLevel, @totalMinsOnline, @lastLoggedIn, @creation, @clientType, @isLoggedIn, @bantime, @banlength, @bannote, @emailConfirmed, @firstcharacter, @note, @steamID, @twitchAuthToken, @credit, @isactive, @resetcount, @wasreset, @validUntil, @payingcustomer, @campaignid) ");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@accountID", this.accountID);
                command.Parameters.AddWithValue("@email", this.email);
                command.Parameters.AddWithValue("@password", this.password);
                command.Parameters.AddWithValue("@firstName", this.firstName);
                command.Parameters.AddWithValue("@lastName", this.lastName);
                command.Parameters.AddWithValue("@born", this.born);
                command.Parameters.AddWithValue("@state", this.state);
                command.Parameters.AddWithValue("@accLevel", this.accLevel);
                command.Parameters.AddWithValue("@totalMinsOnline", this.totalMinsOnline);
                command.Parameters.AddWithValue("@lastLoggedIn", this.lastLoggedIn);
                command.Parameters.AddWithValue("@creation", this.creation);
                command.Parameters.AddWithValue("@clientType", this.clientType);
                command.Parameters.AddWithValue("@isLoggedIn", this.isLoggedIn);
                command.Parameters.AddWithValue("@bantime", this.bantime);
                command.Parameters.AddWithValue("@banlength", this.banlength);
                command.Parameters.AddWithValue("@bannote", this.bannote);
                command.Parameters.AddWithValue("@emailConfirmed", this.emailConfirmed);
                command.Parameters.AddWithValue("@firstcharacter", this.firstcharacter);
                command.Parameters.AddWithValue("@note", this.note);
                command.Parameters.AddWithValue("@steamID", this.steamID);
                command.Parameters.AddWithValue("@twitchAuthToken", this.twitchAuthToken);
                command.Parameters.AddWithValue("@credit", this.credit);
                command.Parameters.AddWithValue("@isactive", this.isactive);
                command.Parameters.AddWithValue("@resetcount", this.resetcount);
                command.Parameters.AddWithValue("@wasreset", this.wasreset);
                command.Parameters.AddWithValue("@validUntil", this.validUntil);
                command.Parameters.AddWithValue("@payingcustomer", this.payingcustomer);
                command.Parameters.AddWithValue("@campaignid", this.campaignid);

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
                sqlCommand.Append("UPDATE accounts Set `email`= @email, `password`= @password, `firstName`= @firstName, `lastName`= @lastName, `born`= @born, `state`= @state, `accLevel`= @accLevel, `totalMinsOnline`= @totalMinsOnline, `lastLoggedIn`= @lastLoggedIn, `creation`= @creation, `clientType`= @clientType, `isLoggedIn`= @isLoggedIn, `bantime`= @bantime, `banlength`= @banlength, `bannote`= @bannote, `emailConfirmed`= @emailConfirmed, `firstcharacter`= @firstcharacter, `note`= @note, `steamID`= @steamID, `twitchAuthToken`= @twitchAuthToken, `credit`= @credit, `isactive`= @isactive, `resetcount`= @resetcount, `wasreset`= @wasreset, `validUntil`= @validUntil, `payingcustomer`= @payingcustomer, `campaignid`= @campaignid where accountID = @accountID");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@accountID", this.accountID);
                command.Parameters.AddWithValue("@email", this.email);
                command.Parameters.AddWithValue("@password", this.password);
                command.Parameters.AddWithValue("@firstName", this.firstName);
                command.Parameters.AddWithValue("@lastName", this.lastName);
                command.Parameters.AddWithValue("@born", this.born);
                command.Parameters.AddWithValue("@state", this.state);
                command.Parameters.AddWithValue("@accLevel", this.accLevel);
                command.Parameters.AddWithValue("@totalMinsOnline", this.totalMinsOnline);
                command.Parameters.AddWithValue("@lastLoggedIn", this.lastLoggedIn);
                command.Parameters.AddWithValue("@creation", this.creation);
                command.Parameters.AddWithValue("@clientType", this.clientType);
                command.Parameters.AddWithValue("@isLoggedIn", this.isLoggedIn);
                command.Parameters.AddWithValue("@bantime", this.bantime);
                command.Parameters.AddWithValue("@banlength", this.banlength);
                command.Parameters.AddWithValue("@bannote", this.bannote);
                command.Parameters.AddWithValue("@emailConfirmed", this.emailConfirmed);
                command.Parameters.AddWithValue("@firstcharacter", this.firstcharacter);
                command.Parameters.AddWithValue("@note", this.note);
                command.Parameters.AddWithValue("@steamID", this.steamID);
                command.Parameters.AddWithValue("@twitchAuthToken", this.twitchAuthToken);
                command.Parameters.AddWithValue("@credit", this.credit);
                command.Parameters.AddWithValue("@isactive", this.isactive);
                command.Parameters.AddWithValue("@resetcount", this.resetcount);
                command.Parameters.AddWithValue("@wasreset", this.wasreset);
                command.Parameters.AddWithValue("@validUntil", this.validUntil);
                command.Parameters.AddWithValue("@payingcustomer", this.payingcustomer);
                command.Parameters.AddWithValue("@campaignid", this.campaignid);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public List<Accounts> GetAllAccounts()
        {
            List<Accounts> List = new List<Accounts>();

            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("SELECT * from accounts");
                    command.CommandText = sqlCommand.ToString();
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Accounts tmp = new Accounts(this.ConnString);

                            tmp.accountID = Convert.ToInt32(reader["accountID"]);
                            tmp.email = Convert.ToString(reader["email"]);
                            tmp.password = Convert.ToString(reader["password"]);
                            tmp.firstName = Convert.ToString(reader["firstName"]);
                            tmp.lastName = Convert.ToString(reader["lastName"]);
                            if (reader["born"] != DBNull.Value) { tmp.born = Convert.ToDateTime(reader["born"]); }
                            tmp.state = Convert.ToInt32(reader["state"]);
                            tmp.accLevel = Convert.ToInt32(reader["accLevel"]);
                            tmp.totalMinsOnline = Convert.ToInt32(reader["totalMinsOnline"]);
                            tmp.lastLoggedIn = Convert.ToDateTime(reader["lastLoggedIn"]);
                            tmp.creation = Convert.ToDateTime(reader["creation"]);
                            tmp.clientType = Convert.ToInt32(reader["clientType"]);
                            tmp.isLoggedIn = Convert.ToInt32(reader["isLoggedIn"]);
                            tmp.bantime = Convert.ToDateTime(reader["bantime"]);
                            tmp.banlength = Convert.ToInt32(reader["banlength"]);
                            tmp.bannote = Convert.ToString(reader["bannote"]);
                            tmp.emailConfirmed = Convert.ToInt32(reader["emailConfirmed"]);
                            if (reader["firstcharacter"] != DBNull.Value) { tmp.firstcharacter = Convert.ToDateTime(reader["firstcharacter"]); }
                            tmp.note = Convert.ToString(reader["note"]);
                            tmp.steamID = Convert.ToString(reader["steamID"]);
                            tmp.twitchAuthToken = Convert.ToString(reader["twitchAuthToken"]);
                            tmp.credit = Convert.ToInt32(reader["credit"]);
                            tmp.isactive = Convert.ToInt32(reader["isactive"]);
                            tmp.resetcount = Convert.ToInt32(reader["resetcount"]);
                            tmp.wasreset = Convert.ToInt32(reader["wasreset"]);
                            if (reader["validUntil"] != DBNull.Value) { tmp.validUntil = Convert.ToDateTime(reader["validUntil"]); }
                            tmp.payingcustomer = Convert.ToInt32(reader["payingcustomer"]);
                            tmp.campaignid = Convert.ToString(reader["campaignid"]);

                            List.Add(tmp);
                        }
                    }
                }
            }
            return List;
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