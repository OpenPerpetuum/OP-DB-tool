//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Perptool.db
//{
//    /// <summary>
//    /// Table Class
//    /// </summary>
//    class charactersTbl : INotifyPropertyChanged
//    {
//        /// <summary>
//        /// private field
//        /// </summary>
//        private int privatecharacterid;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private int privateaccountid;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private int privaterooteid;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private string privatenick;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private string privatemoodmessage;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private System.DateTime privatecreation;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private System.DateTime privatelastlogout;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private System.DateTime privatelastused;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private string privatecredit;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private string privateinuse;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private int privatetotalminsonline;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private int privateactivechassis;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private string privateactive;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private System.DateTime privatedeletedat;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private int privatebaseeid;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private int privatedefaultcorporationeid;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private int privatemajorid;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private int privateraceid;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private int privateschoolid;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private int privatesparkid;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private System.DateTime privatelastdocked;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private string privatedocked;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private System.DateTime privatelastteleported;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private int privatezoneid;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private string privatenickcorrected;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private string privateoffensivenick;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private string privatepositionx;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private string privatepositiony;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private int privatehomebaseeid;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private string privateblocktrades;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private string privateglobalmute;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private string privateavatar;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private string privatenote;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private int privatecorporationeid;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private int privateallianceeid;

//        /// <summary>
//        /// private field
//        /// </summary>
//        private int privatelanguage;

//        /// <summary>
//        /// Initializes a new instance of the <see cref='charactersTbl' /> class.
//        /// </summary>
//        /// <param name='connectionString'>pass the connection string to the database</param>
//        public charactersTbl(string connectionString)
//        {
//            this.ConnString = connectionString;
//        }


//        /// <summary>
//        /// Event handler for properties
//        /// </summary>
//        public event PropertyChangedEventHandler PropertyChanged;

//        #region Fields
//        /// <summary>
//        /// Gets or sets public field characterID
//        /// </summary>
//        public int characterID
//        {
//            get
//            {
//                return this.privatecharacterid;
//            }

//            set
//            {
//                this.privatecharacterid = value;
//                this.OnPropertyChanged("characterID");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field accountID
//        /// </summary>
//        public int accountID
//        {
//            get
//            {
//                return this.privateaccountid;
//            }

//            set
//            {
//                this.privateaccountid = value;
//                this.OnPropertyChanged("accountID");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field rootEID
//        /// </summary>
//        public int rootEID
//        {
//            get
//            {
//                return this.privaterooteid;
//            }

//            set
//            {
//                this.privaterooteid = value;
//                this.OnPropertyChanged("rootEID");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field nick
//        /// </summary>
//        public string nick
//        {
//            get
//            {
//                return this.privatenick;
//            }

//            set
//            {
//                this.privatenick = value;
//                this.OnPropertyChanged("nick");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field moodMessage
//        /// </summary>
//        public string moodMessage
//        {
//            get
//            {
//                return this.privatemoodmessage;
//            }

//            set
//            {
//                this.privatemoodmessage = value;
//                this.OnPropertyChanged("moodMessage");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field creation
//        /// </summary>
//        public System.DateTime creation
//        {
//            get
//            {
//                return this.privatecreation;
//            }

//            set
//            {
//                this.privatecreation = value;
//                this.OnPropertyChanged("creation");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field lastLogOut
//        /// </summary>
//        public System.DateTime lastLogOut
//        {
//            get
//            {
//                return this.privatelastlogout;
//            }

//            set
//            {
//                this.privatelastlogout = value;
//                this.OnPropertyChanged("lastLogOut");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field lastUsed
//        /// </summary>
//        public System.DateTime lastUsed
//        {
//            get
//            {
//                return this.privatelastused;
//            }

//            set
//            {
//                this.privatelastused = value;
//                this.OnPropertyChanged("lastUsed");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field credit
//        /// </summary>
//        public string credit
//        {
//            get
//            {
//                return this.privatecredit;
//            }

//            set
//            {
//                this.privatecredit = value;
//                this.OnPropertyChanged("credit");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field inUse
//        /// </summary>
//        public string inUse
//        {
//            get
//            {
//                return this.privateinuse;
//            }

//            set
//            {
//                this.privateinuse = value;
//                this.OnPropertyChanged("inUse");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field totalMinsOnline
//        /// </summary>
//        public int totalMinsOnline
//        {
//            get
//            {
//                return this.privatetotalminsonline;
//            }

//            set
//            {
//                this.privatetotalminsonline = value;
//                this.OnPropertyChanged("totalMinsOnline");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field activeChassis
//        /// </summary>
//        public int activeChassis
//        {
//            get
//            {
//                return this.privateactivechassis;
//            }

//            set
//            {
//                this.privateactivechassis = value;
//                this.OnPropertyChanged("activeChassis");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field active
//        /// </summary>
//        public string active
//        {
//            get
//            {
//                return this.privateactive;
//            }

//            set
//            {
//                this.privateactive = value;
//                this.OnPropertyChanged("active");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field deletedAt
//        /// </summary>
//        public System.DateTime deletedAt
//        {
//            get
//            {
//                return this.privatedeletedat;
//            }

//            set
//            {
//                this.privatedeletedat = value;
//                this.OnPropertyChanged("deletedAt");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field baseEID
//        /// </summary>
//        public int baseEID
//        {
//            get
//            {
//                return this.privatebaseeid;
//            }

//            set
//            {
//                this.privatebaseeid = value;
//                this.OnPropertyChanged("baseEID");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field defaultcorporationEID
//        /// </summary>
//        public int defaultcorporationEID
//        {
//            get
//            {
//                return this.privatedefaultcorporationeid;
//            }

//            set
//            {
//                this.privatedefaultcorporationeid = value;
//                this.OnPropertyChanged("defaultcorporationEID");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field majorID
//        /// </summary>
//        public int majorID
//        {
//            get
//            {
//                return this.privatemajorid;
//            }

//            set
//            {
//                this.privatemajorid = value;
//                this.OnPropertyChanged("majorID");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field raceID
//        /// </summary>
//        public int raceID
//        {
//            get
//            {
//                return this.privateraceid;
//            }

//            set
//            {
//                this.privateraceid = value;
//                this.OnPropertyChanged("raceID");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field schoolID
//        /// </summary>
//        public int schoolID
//        {
//            get
//            {
//                return this.privateschoolid;
//            }

//            set
//            {
//                this.privateschoolid = value;
//                this.OnPropertyChanged("schoolID");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field sparkID
//        /// </summary>
//        public int sparkID
//        {
//            get
//            {
//                return this.privatesparkid;
//            }

//            set
//            {
//                this.privatesparkid = value;
//                this.OnPropertyChanged("sparkID");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field lastdocked
//        /// </summary>
//        public System.DateTime lastdocked
//        {
//            get
//            {
//                return this.privatelastdocked;
//            }

//            set
//            {
//                this.privatelastdocked = value;
//                this.OnPropertyChanged("lastdocked");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field docked
//        /// </summary>
//        public string docked
//        {
//            get
//            {
//                return this.privatedocked;
//            }

//            set
//            {
//                this.privatedocked = value;
//                this.OnPropertyChanged("docked");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field lastteleported
//        /// </summary>
//        public System.DateTime lastteleported
//        {
//            get
//            {
//                return this.privatelastteleported;
//            }

//            set
//            {
//                this.privatelastteleported = value;
//                this.OnPropertyChanged("lastteleported");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field zoneID
//        /// </summary>
//        public int zoneID
//        {
//            get
//            {
//                return this.privatezoneid;
//            }

//            set
//            {
//                this.privatezoneid = value;
//                this.OnPropertyChanged("zoneID");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field nickcorrected
//        /// </summary>
//        public string nickcorrected
//        {
//            get
//            {
//                return this.privatenickcorrected;
//            }

//            set
//            {
//                this.privatenickcorrected = value;
//                this.OnPropertyChanged("nickcorrected");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field offensivenick
//        /// </summary>
//        public string offensivenick
//        {
//            get
//            {
//                return this.privateoffensivenick;
//            }

//            set
//            {
//                this.privateoffensivenick = value;
//                this.OnPropertyChanged("offensivenick");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field positionX
//        /// </summary>
//        public string positionX
//        {
//            get
//            {
//                return this.privatepositionx;
//            }

//            set
//            {
//                this.privatepositionx = value;
//                this.OnPropertyChanged("positionX");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field positionY
//        /// </summary>
//        public string positionY
//        {
//            get
//            {
//                return this.privatepositiony;
//            }

//            set
//            {
//                this.privatepositiony = value;
//                this.OnPropertyChanged("positionY");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field homeBaseEID
//        /// </summary>
//        public int homeBaseEID
//        {
//            get
//            {
//                return this.privatehomebaseeid;
//            }

//            set
//            {
//                this.privatehomebaseeid = value;
//                this.OnPropertyChanged("homeBaseEID");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field blockTrades
//        /// </summary>
//        public string blockTrades
//        {
//            get
//            {
//                return this.privateblocktrades;
//            }

//            set
//            {
//                this.privateblocktrades = value;
//                this.OnPropertyChanged("blockTrades");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field globalMute
//        /// </summary>
//        public string globalMute
//        {
//            get
//            {
//                return this.privateglobalmute;
//            }

//            set
//            {
//                this.privateglobalmute = value;
//                this.OnPropertyChanged("globalMute");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field avatar
//        /// </summary>
//        public string avatar
//        {
//            get
//            {
//                return this.privateavatar;
//            }

//            set
//            {
//                this.privateavatar = value;
//                this.OnPropertyChanged("avatar");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field note
//        /// </summary>
//        public string note
//        {
//            get
//            {
//                return this.privatenote;
//            }

//            set
//            {
//                this.privatenote = value;
//                this.OnPropertyChanged("note");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field corporationeid
//        /// </summary>
//        public int corporationeid
//        {
//            get
//            {
//                return this.privatecorporationeid;
//            }

//            set
//            {
//                this.privatecorporationeid = value;
//                this.OnPropertyChanged("corporationeid");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field allianceeid
//        /// </summary>
//        public int allianceeid
//        {
//            get
//            {
//                return this.privateallianceeid;
//            }

//            set
//            {
//                this.privateallianceeid = value;
//                this.OnPropertyChanged("allianceeid");
//            }
//        }

//        /// <summary>
//        /// Gets or sets public field language
//        /// </summary>
//        public int language
//        {
//            get
//            {
//                return this.privatelanguage;
//            }

//            set
//            {
//                this.privatelanguage = value;
//                this.OnPropertyChanged("language");
//            }
//        }

//        /// <summary>
//        /// Gets or sets connection string
//        /// </summary>
//        private string ConnString { get; set; }

//        #endregion

//        /// <summary>
//        /// Clears and sets defaults on variables.
//        /// </summary>
//        public void Clear()
//        {
//            this.privatecharacterid = 0;
//            this.characterID = 0;
//            this.privateaccountid = 0;
//            this.accountID = 0;
//            this.privaterooteid = 0;
//            this.rootEID = 0;
//            this.privatenick = string.Empty;
//            this.nick = string.Empty;
//            this.privatemoodmessage = string.Empty;
//            this.moodMessage = string.Empty;
//            this.privatecreation = DateTime.MinValue;
//            this.creation = DateTime.MinValue;
//            this.privatelastlogout = DateTime.MinValue;
//            this.lastLogOut = DateTime.MinValue;
//            this.privatelastused = DateTime.MinValue;
//            this.lastUsed = DateTime.MinValue;
//            this.privatecredit
//            this.credit
//            this.privateinuse
//            this.inUse
//            this.privatetotalminsonline = 0;
//            this.totalMinsOnline = 0;
//            this.privateactivechassis = 0;
//            this.activeChassis = 0;
//            this.privateactive
//            this.active
//            this.privatedeletedat = DateTime.MinValue;
//            this.deletedAt = DateTime.MinValue;
//            this.privatebaseeid = 0;
//            this.baseEID = 0;
//            this.privatedefaultcorporationeid = 0;
//            this.defaultcorporationEID = 0;
//            this.privatemajorid = 0;
//            this.majorID = 0;
//            this.privateraceid = 0;
//            this.raceID = 0;
//            this.privateschoolid = 0;
//            this.schoolID = 0;
//            this.privatesparkid = 0;
//            this.sparkID = 0;
//            this.privatelastdocked = DateTime.MinValue;
//            this.lastdocked = DateTime.MinValue;
//            this.privatedocked
//            this.docked
//            this.privatelastteleported = DateTime.MinValue;
//            this.lastteleported = DateTime.MinValue;
//            this.privatezoneid = 0;
//            this.zoneID = 0;
//            this.privatenickcorrected
//            this.nickcorrected
//            this.privateoffensivenick
//            this.offensivenick
//            this.privatepositionx
//            this.positionX
//            this.privatepositiony
//            this.positionY
//            this.privatehomebaseeid = 0;
//            this.homeBaseEID = 0;
//            this.privateblocktrades
//            this.blockTrades
//            this.privateglobalmute
//            this.globalMute
//            this.privateavatar = string.Empty;
//            this.avatar = string.Empty;
//            this.privatenote = string.Empty;
//            this.note = string.Empty;
//            this.privatecorporationeid = 0;
//            this.corporationeid = 0;
//            this.privateallianceeid = 0;
//            this.allianceeid = 0;
//            this.privatelanguage = 0;
//            this.language = 0;
//        }

//        /// <summary>
//        /// saves a new record
//        /// </summary>
//        public void SaveNewRecord()
//        {
//            using (SqlCommand command = new SqlCommand())
//            {
//                StringBuilder sqlCommand = new StringBuilder();
//                sqlCommand.Append("Insert into characters ");
//                sqlCommand.Append("(`characterID`, `accountID`, `rootEID`, `nick`, `moodMessage`, `creation`, `lastLogOut`, `lastUsed`, `credit`, `inUse`, `totalMinsOnline`, `activeChassis`, `active`, `deletedAt`, `baseEID`, `defaultcorporationEID`, `majorID`, `raceID`, `schoolID`, `sparkID`, `lastdocked`, `docked`, `lastteleported`, `zoneID`, `nickcorrected`, `offensivenick`, `positionX`, `positionY`, `homeBaseEID`, `blockTrades`, `globalMute`, `avatar`, `note`, `corporationeid`, `allianceeid`, `language`) ");
//                sqlCommand.Append(" Values ");
//                sqlCommand.Append("(@characterID, @accountID, @rootEID, @nick, @moodMessage, @creation, @lastLogOut, @lastUsed, @credit, @inUse, @totalMinsOnline, @activeChassis, @active, @deletedAt, @baseEID, @defaultcorporationEID, @majorID, @raceID, @schoolID, @sparkID, @lastdocked, @docked, @lastteleported, @zoneID, @nickcorrected, @offensivenick, @positionX, @positionY, @homeBaseEID, @blockTrades, @globalMute, @avatar, @note, @corporationeid, @allianceeid, @language) ");

//                command.CommandText = sqlCommand.ToString();

//                command.Parameters.AddWithValue("@characterID", this.characterID);
//                command.Parameters.AddWithValue("@accountID", this.accountID);
//                command.Parameters.AddWithValue("@rootEID", this.rootEID);
//                command.Parameters.AddWithValue("@nick", this.nick);
//                command.Parameters.AddWithValue("@moodMessage", this.moodMessage);
//                command.Parameters.AddWithValue("@creation", this.creation);
//                command.Parameters.AddWithValue("@lastLogOut", this.lastLogOut);
//                command.Parameters.AddWithValue("@lastUsed", this.lastUsed);
//                command.Parameters.AddWithValue("@credit", this.credit);
//                command.Parameters.AddWithValue("@inUse", this.inUse);
//                command.Parameters.AddWithValue("@totalMinsOnline", this.totalMinsOnline);
//                command.Parameters.AddWithValue("@activeChassis", this.activeChassis);
//                command.Parameters.AddWithValue("@active", this.active);
//                command.Parameters.AddWithValue("@deletedAt", this.deletedAt);
//                command.Parameters.AddWithValue("@baseEID", this.baseEID);
//                command.Parameters.AddWithValue("@defaultcorporationEID", this.defaultcorporationEID);
//                command.Parameters.AddWithValue("@majorID", this.majorID);
//                command.Parameters.AddWithValue("@raceID", this.raceID);
//                command.Parameters.AddWithValue("@schoolID", this.schoolID);
//                command.Parameters.AddWithValue("@sparkID", this.sparkID);
//                command.Parameters.AddWithValue("@lastdocked", this.lastdocked);
//                command.Parameters.AddWithValue("@docked", this.docked);
//                command.Parameters.AddWithValue("@lastteleported", this.lastteleported);
//                command.Parameters.AddWithValue("@zoneID", this.zoneID);
//                command.Parameters.AddWithValue("@nickcorrected", this.nickcorrected);
//                command.Parameters.AddWithValue("@offensivenick", this.offensivenick);
//                command.Parameters.AddWithValue("@positionX", this.positionX);
//                command.Parameters.AddWithValue("@positionY", this.positionY);
//                command.Parameters.AddWithValue("@homeBaseEID", this.homeBaseEID);
//                command.Parameters.AddWithValue("@blockTrades", this.blockTrades);
//                command.Parameters.AddWithValue("@globalMute", this.globalMute);
//                command.Parameters.AddWithValue("@avatar", this.avatar);
//                command.Parameters.AddWithValue("@note", this.note);
//                command.Parameters.AddWithValue("@corporationeid", this.corporationeid);
//                command.Parameters.AddWithValue("@allianceeid", this.allianceeid);
//                command.Parameters.AddWithValue("@language", this.language);

//                SqlConnection conn = new SqlConnection(this.ConnString);
//                conn.Open();
//                command.Connection = conn;
//                command.ExecuteNonQuery();
//                conn.Close();
//            }
//        }

//        /// <summary>
//        /// saves existing record
//        /// </summary>
//        public void Save()
//        {
//            using (SqlCommand command = new SqlCommand())
//            {
//                StringBuilder sqlCommand = new StringBuilder();
//                sqlCommand.Append("UPDATE characters Set `accountID`= @accountID, `rootEID`= @rootEID, `nick`= @nick, `moodMessage`= @moodMessage, `creation`= @creation, `lastLogOut`= @lastLogOut, `lastUsed`= @lastUsed, `credit`= @credit, `inUse`= @inUse, `totalMinsOnline`= @totalMinsOnline, `activeChassis`= @activeChassis, `active`= @active, `deletedAt`= @deletedAt, `baseEID`= @baseEID, `defaultcorporationEID`= @defaultcorporationEID, `majorID`= @majorID, `raceID`= @raceID, `schoolID`= @schoolID, `sparkID`= @sparkID, `lastdocked`= @lastdocked, `docked`= @docked, `lastteleported`= @lastteleported, `zoneID`= @zoneID, `nickcorrected`= @nickcorrected, `offensivenick`= @offensivenick, `positionX`= @positionX, `positionY`= @positionY, `homeBaseEID`= @homeBaseEID, `blockTrades`= @blockTrades, `globalMute`= @globalMute, `avatar`= @avatar, `note`= @note, `corporationeid`= @corporationeid, `allianceeid`= @allianceeid, `language`= @language where characterID = @characterID");

//                command.CommandText = sqlCommand.ToString();

//                command.Parameters.AddWithValue("@characterID", this.characterID);
//                command.Parameters.AddWithValue("@accountID", this.accountID);
//                command.Parameters.AddWithValue("@rootEID", this.rootEID);
//                command.Parameters.AddWithValue("@nick", this.nick);
//                command.Parameters.AddWithValue("@moodMessage", this.moodMessage);
//                command.Parameters.AddWithValue("@creation", this.creation);
//                command.Parameters.AddWithValue("@lastLogOut", this.lastLogOut);
//                command.Parameters.AddWithValue("@lastUsed", this.lastUsed);
//                command.Parameters.AddWithValue("@credit", this.credit);
//                command.Parameters.AddWithValue("@inUse", this.inUse);
//                command.Parameters.AddWithValue("@totalMinsOnline", this.totalMinsOnline);
//                command.Parameters.AddWithValue("@activeChassis", this.activeChassis);
//                command.Parameters.AddWithValue("@active", this.active);
//                command.Parameters.AddWithValue("@deletedAt", this.deletedAt);
//                command.Parameters.AddWithValue("@baseEID", this.baseEID);
//                command.Parameters.AddWithValue("@defaultcorporationEID", this.defaultcorporationEID);
//                command.Parameters.AddWithValue("@majorID", this.majorID);
//                command.Parameters.AddWithValue("@raceID", this.raceID);
//                command.Parameters.AddWithValue("@schoolID", this.schoolID);
//                command.Parameters.AddWithValue("@sparkID", this.sparkID);
//                command.Parameters.AddWithValue("@lastdocked", this.lastdocked);
//                command.Parameters.AddWithValue("@docked", this.docked);
//                command.Parameters.AddWithValue("@lastteleported", this.lastteleported);
//                command.Parameters.AddWithValue("@zoneID", this.zoneID);
//                command.Parameters.AddWithValue("@nickcorrected", this.nickcorrected);
//                command.Parameters.AddWithValue("@offensivenick", this.offensivenick);
//                command.Parameters.AddWithValue("@positionX", this.positionX);
//                command.Parameters.AddWithValue("@positionY", this.positionY);
//                command.Parameters.AddWithValue("@homeBaseEID", this.homeBaseEID);
//                command.Parameters.AddWithValue("@blockTrades", this.blockTrades);
//                command.Parameters.AddWithValue("@globalMute", this.globalMute);
//                command.Parameters.AddWithValue("@avatar", this.avatar);
//                command.Parameters.AddWithValue("@note", this.note);
//                command.Parameters.AddWithValue("@corporationeid", this.corporationeid);
//                command.Parameters.AddWithValue("@allianceeid", this.allianceeid);
//                command.Parameters.AddWithValue("@language", this.language);

//                SqlConnection conn = new SqlConnection(this.ConnString);
//                conn.Open();
//                command.Connection = conn;
//                command.ExecuteNonQuery();
//                conn.Close();
//            }
//        }

//        /// <summary>
//        /// gets a record by its record id
//        /// </summary>
//        /// <param name='recnum'>record number</param>
//        public void GetById(int characterid)
//        {
//            SqlConnection conn = new SqlConnection(this.ConnString);
//            using (SqlCommand command = new SqlCommand())
//            {
//                StringBuilder sqlCommand = new StringBuilder();
//                sqlCommand.Append("SELECT * from characters Where characterID=@characterID");
//                command.CommandText = sqlCommand.ToString();
//                command.Parameters.AddWithValue("@characterID", characterID);
//                command.Connection = conn;
//                conn.Open();
//                using (SqlDataReader reader = command.ExecuteReader())
//                {
//                    while (reader.Read())
//                    {
//                        this.characterID = Convert.ToInt32(reader["characterID"]);
//                        this.accountID = Convert.ToInt32(reader["accountID"]);
//                        this.rootEID = Convert.ToInt32(reader["rootEID"]);
//                        this.nick = Convert.ToString(reader["nick"]);
//                        this.moodMessage = Convert.ToString(reader["moodMessage"]);
//                        this.creation = Convert.ToDateTime(reader["creation"]);
//                        this.lastLogOut = Convert.ToDateTime(reader["lastLogOut"]);
//                        this.lastUsed = Convert.ToDateTime(reader["lastUsed"]);
//                        this.credit = Convert.ToSingle(reader["credit"]);
//                        this.inUse = Convert.Tostring(reader["inUse"]);
//                        this.totalMinsOnline = Convert.ToInt32(reader["totalMinsOnline"]);
//                        this.activeChassis = Convert.ToInt32(reader["activeChassis"]);
//                        this.active = Convert.Tostring(reader["active"]);
//                        this.deletedAt = Convert.ToDateTime(reader["deletedAt"]);
//                        this.baseEID = Convert.ToInt32(reader["baseEID"]);
//                        this.defaultcorporationEID = Convert.ToInt32(reader["defaultcorporationEID"]);
//                        this.majorID = Convert.ToInt32(reader["majorID"]);
//                        this.raceID = Convert.ToInt32(reader["raceID"]);
//                        this.schoolID = Convert.ToInt32(reader["schoolID"]);
//                        this.sparkID = Convert.ToInt32(reader["sparkID"]);
//                        this.lastdocked = Convert.ToDateTime(reader["lastdocked"]);
//                        this.docked = Convert.Tostring(reader["docked"]);
//                        this.lastteleported = Convert.ToDateTime(reader["lastteleported"]);
//                        this.zoneID = Convert.ToInt32(reader["zoneID"]);
//                        this.nickcorrected = Convert.Tostring(reader["nickcorrected"]);
//                        this.offensivenick = Convert.Tostring(reader["offensivenick"]);
//                        this.positionX = Convert.ToSingle(reader["positionX"]);
//                        this.positionY = Convert.ToSingle(reader["positionY"]);
//                        this.homeBaseEID = Convert.ToInt32(reader["homeBaseEID"]);
//                        this.blockTrades = Convert.Tostring(reader["blockTrades"]);
//                        this.globalMute = Convert.Tostring(reader["globalMute"]);
//                        this.avatar = Convert.ToString(reader["avatar"]);
//                        this.note = Convert.ToString(reader["note"]);
//                        this.corporationeid = Convert.ToInt32(reader["corporationeid"]);
//                        this.allianceeid = Convert.ToInt32(reader["allianceeid"]);
//                        this.language = Convert.ToInt32(reader["language"]);
//                    }
//                }

//                conn.Dispose();
//            }
//        }

//        /// <summary>
//        /// fires when properties are set.
//        /// </summary>
//        /// <param name='name'>name of property being changed</param>
//        protected void OnPropertyChanged(string name)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
//        }
//    }
//}
