using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perptool.db
{
    /// <summary>
    /// Table Class
    /// </summary>
    public class Characters : INotifyPropertyChanged
    {
        /// <summary>
        /// private field
        /// </summary>
        private int privatecharacterid;

        /// <summary>
        /// private field
        /// </summary>
        private int privateaccountid;

        /// <summary>
        /// private field
        /// </summary>
        private Int64 privaterooteid;

        /// <summary>
        /// private field
        /// </summary>
        private string privatenick;

        /// <summary>
        /// private field
        /// </summary>
        private string privatemoodmessage;

        /// <summary>
        /// private field
        /// </summary>
        private SqlDateTime? privatecreation;

        /// <summary>
        /// private field
        /// </summary>
        private SqlDateTime? privatelastlogout;

        /// <summary>
        /// private field
        /// </summary>
        private SqlDateTime? privatelastused;

        /// <summary>
        /// private field
        /// </summary>
        private decimal privatecredit;

        /// <summary>
        /// private field
        /// </summary>
        private int privateinuse;

        /// <summary>
        /// private field
        /// </summary>
        private int privatetotalminsonline;

        /// <summary>
        /// private field
        /// </summary>
        private Int64 privateactivechassis;

        /// <summary>
        /// private field
        /// </summary>
        private int privateactive;

        /// <summary>
        /// private field
        /// </summary>
        private SqlDateTime? privatedeletedat;

        /// <summary>
        /// private field
        /// </summary>
        private int privatebaseeid;

        /// <summary>
        /// private field
        /// </summary>
        private int privatedefaultcorporationeid;

        /// <summary>
        /// private field
        /// </summary>
        private int privatemajorid;

        /// <summary>
        /// private field
        /// </summary>
        private int privateraceid;

        /// <summary>
        /// private field
        /// </summary>
        private int privateschoolid;

        /// <summary>
        /// private field
        /// </summary>
        private int privatesparkid;

        /// <summary>
        /// private field
        /// </summary>
        private SqlDateTime? privatelastdocked;

        /// <summary>
        /// private field
        /// </summary>
        private int privatedocked;

        /// <summary>
        /// private field
        /// </summary>
        private SqlDateTime? privatelastteleported;

        /// <summary>
        /// private field
        /// </summary>
        private int privatezoneid;

        /// <summary>
        /// private field
        /// </summary>
        private int privatenickcorrected;

        /// <summary>
        /// private field
        /// </summary>
        private int privateoffensivenick;

        /// <summary>
        /// private field
        /// </summary>
        private decimal privatepositionx;

        /// <summary>
        /// private field
        /// </summary>
        private decimal privatepositiony;

        /// <summary>
        /// private field
        /// </summary>
        private int privatehomebaseeid;

        /// <summary>
        /// private field
        /// </summary>
        private int privateblocktrades;

        /// <summary>
        /// private field
        /// </summary>
        private int privateglobalmute;

        /// <summary>
        /// private field
        /// </summary>
        private string privateavatar;

        /// <summary>
        /// private field
        /// </summary>
        private string privatenote;

        /// <summary>
        /// private field
        /// </summary>
        private int privatecorporationeid;

        /// <summary>
        /// private field
        /// </summary>
        private int privateallianceeid;

        /// <summary>
        /// private field
        /// </summary>
        private int privatelanguage;

        /// <summary>
        /// Initializes a new instance of the <see cref='charactersTbl' /> class.
        /// </summary>
        /// <param name='connectionString'>pass the connection string to the database</param>
        public Characters(string connectionString)
        {
            this.ConnString = connectionString;
        }


        /// <summary>
        /// Event handler for properties
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #region Fields
        /// <summary>
        /// Gets or sets public field characterID
        /// </summary>
        public int characterID
        {
            get
            {
                return this.privatecharacterid;
            }

            set
            {
                this.privatecharacterid = value;
                this.OnPropertyChanged("characterID");
            }
        }

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
        /// Gets or sets public field rootEID
        /// </summary>
        public Int64 rootEID
        {
            get
            {
                return this.privaterooteid;
            }

            set
            {
                this.privaterooteid = value;
                this.OnPropertyChanged("rootEID");
            }
        }

        /// <summary>
        /// Gets or sets public field nick
        /// </summary>
        public string nick
        {
            get
            {
                return this.privatenick;
            }

            set
            {
                this.privatenick = value;
                this.OnPropertyChanged("nick");
            }
        }

        /// <summary>
        /// Gets or sets public field moodMessage
        /// </summary>
        public string moodMessage
        {
            get
            {
                return this.privatemoodmessage;
            }

            set
            {
                this.privatemoodmessage = value;
                this.OnPropertyChanged("moodMessage");
            }
        }

        /// <summary>
        /// Gets or sets public field creation
        /// </summary>
        public DateTime creation
        {
            get
            {
                return (DateTime)(this.privatecreation ?? SqlDateTime.MinValue);
            }

            set
            {
                if (value == null || value < (DateTime)SqlDateTime.MinValue)
                {
                    this.privatecreation = SqlDateTime.MinValue;
                }
                else
                {
                    this.privatecreation = (SqlDateTime)value;
                }
                this.OnPropertyChanged("creation");
            }
        }

        /// <summary>
        /// Gets or sets public field lastLogOut
        /// </summary>
        public DateTime lastLogOut
        {
            get
            {
                return (DateTime)(this.privatelastlogout ?? SqlDateTime.MinValue);
            }

            set
            {
                if (value == null || value < (DateTime)SqlDateTime.MinValue)
                {
                    this.privatelastlogout = SqlDateTime.MinValue;
                }
                else
                {
                    this.privatelastlogout = (SqlDateTime)value;
                }
                this.OnPropertyChanged("lastLogOut");
            }
        }

        /// <summary>
        /// Gets or sets public field lastUsed
        /// </summary>
        public DateTime lastUsed
        {
            get
            {
                return (DateTime)(this.privatelastused ?? SqlDateTime.MinValue);
            }

            set
            {
                if (value == null || value < (DateTime)SqlDateTime.MinValue)
                {
                    this.privatelastused = SqlDateTime.MinValue;
                }
                else
                {
                    this.privatelastused = (SqlDateTime)value;
                }
                this.OnPropertyChanged("lastUsed");
            }
        }

        /// <summary>
        /// Gets or sets public field credit
        /// </summary>
        public decimal credit
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
        /// Gets or sets public field inUse
        /// </summary>
        public int inUse
        {
            get
            {
                return this.privateinuse;
            }

            set
            {
                this.privateinuse = value;
                this.OnPropertyChanged("inUse");
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
        /// Gets or sets public field activeChassis
        /// </summary>
        public Int64 activeChassis
        {
            get
            {
                return this.privateactivechassis;
            }

            set
            {
                this.privateactivechassis = value;
                this.OnPropertyChanged("activeChassis");
            }
        }

        /// <summary>
        /// Gets or sets public field active
        /// </summary>
        public int active
        {
            get
            {
                return this.privateactive;
            }

            set
            {
                this.privateactive = value;
                this.OnPropertyChanged("active");
            }
        }

        /// <summary>
        /// Gets or sets public field deletedAt
        /// </summary>
        public DateTime deletedAt
        {
            get
            {
                return (DateTime)(this.privatedeletedat ?? SqlDateTime.MinValue);
            }

            set
            {
                if (value == null || value < (DateTime)SqlDateTime.MinValue)
                {
                    this.privatedeletedat = SqlDateTime.MinValue;
                }
                else
                {
                    this.privatedeletedat = (SqlDateTime)value;
                }
                this.OnPropertyChanged("deletedAt");
            }
        }

        /// <summary>
        /// Gets or sets public field baseEID
        /// </summary>
        public int baseEID
        {
            get
            {
                return this.privatebaseeid;
            }

            set
            {
                this.privatebaseeid = value;
                this.OnPropertyChanged("baseEID");
            }
        }

        /// <summary>
        /// Gets or sets public field defaultcorporationEID
        /// </summary>
        public int defaultcorporationEID
        {
            get
            {
                return this.privatedefaultcorporationeid;
            }

            set
            {
                this.privatedefaultcorporationeid = value;
                this.OnPropertyChanged("defaultcorporationEID");
            }
        }

        /// <summary>
        /// Gets or sets public field majorID
        /// </summary>
        public int majorID
        {
            get
            {
                return this.privatemajorid;
            }

            set
            {
                this.privatemajorid = value;
                this.OnPropertyChanged("majorID");
            }
        }

        /// <summary>
        /// Gets or sets public field raceID
        /// </summary>
        public int raceID
        {
            get
            {
                return this.privateraceid;
            }

            set
            {
                this.privateraceid = value;
                this.OnPropertyChanged("raceID");
            }
        }

        /// <summary>
        /// Gets or sets public field schoolID
        /// </summary>
        public int schoolID
        {
            get
            {
                return this.privateschoolid;
            }

            set
            {
                this.privateschoolid = value;
                this.OnPropertyChanged("schoolID");
            }
        }

        /// <summary>
        /// Gets or sets public field sparkID
        /// </summary>
        public int sparkID
        {
            get
            {
                return this.privatesparkid;
            }

            set
            {
                this.privatesparkid = value;
                this.OnPropertyChanged("sparkID");
            }
        }

        /// <summary>
        /// Gets or sets public field lastdocked
        /// </summary>
        public DateTime lastdocked
        {
            get
            {
                return (DateTime)(this.privatelastdocked ?? SqlDateTime.MinValue);
            }

            set
            {
                if (value == null || value < (DateTime)SqlDateTime.MinValue)
                {
                    this.privatelastdocked = SqlDateTime.MinValue;
                }
                else
                {
                    this.privatelastdocked = (SqlDateTime)value;
                }
                this.OnPropertyChanged("lastdocked");
            }
        }

        /// <summary>
        /// Gets or sets public field docked
        /// </summary>
        public int docked
        {
            get
            {
                return this.privatedocked;
            }

            set
            {
                this.privatedocked = value;
                this.OnPropertyChanged("docked");
            }
        }

        /// <summary>
        /// Gets or sets public field lastteleported
        /// </summary>
        public virtual DateTime lastteleported
        {
            get
            {
                return (DateTime)(privatelastteleported ?? SqlDateTime.MinValue);
            }

            set
            {
                if (value == null || value < (DateTime)SqlDateTime.MinValue)
                {
                    privatelastteleported = SqlDateTime.MinValue;
                }
                else
                {
                    privatelastteleported = (SqlDateTime)value;
                }
                this.OnPropertyChanged("lastteleported");
            }
        }

        /// <summary>
        /// Gets or sets public field zoneID
        /// </summary>
        public int zoneID
        {
            get
            {
                return this.privatezoneid;
            }

            set
            {
                this.privatezoneid = value;
                this.OnPropertyChanged("zoneID");
            }
        }

        /// <summary>
        /// Gets or sets public field nickcorrected
        /// </summary>
        public int nickcorrected
        {
            get
            {
                return this.privatenickcorrected;
            }

            set
            {
                this.privatenickcorrected = value;
                this.OnPropertyChanged("nickcorrected");
            }
        }

        /// <summary>
        /// Gets or sets public field offensivenick
        /// </summary>
        public int offensivenick
        {
            get
            {
                return this.privateoffensivenick;
            }

            set
            {
                this.privateoffensivenick = value;
                this.OnPropertyChanged("offensivenick");
            }
        }

        /// <summary>
        /// Gets or sets public field positionX
        /// </summary>
        public decimal positionX
        {
            get
            {
                return this.privatepositionx;
            }

            set
            {
                this.privatepositionx = value;
                this.OnPropertyChanged("positionX");
            }
        }

        /// <summary>
        /// Gets or sets public field positionY
        /// </summary>
        public decimal positionY
        {
            get
            {
                return this.privatepositiony;
            }

            set
            {
                this.privatepositiony = value;
                this.OnPropertyChanged("positionY");
            }
        }

        /// <summary>
        /// Gets or sets public field homeBaseEID
        /// </summary>
        public int homeBaseEID
        {
            get
            {
                return this.privatehomebaseeid;
            }

            set
            {
                this.privatehomebaseeid = value;
                this.OnPropertyChanged("homeBaseEID");
            }
        }

        /// <summary>
        /// Gets or sets public field blockTrades
        /// </summary>
        public int blockTrades
        {
            get
            {
                return this.privateblocktrades;
            }

            set
            {
                this.privateblocktrades = value;
                this.OnPropertyChanged("blockTrades");
            }
        }

        /// <summary>
        /// Gets or sets public field globalMute
        /// </summary>
        public int globalMute
        {
            get
            {
                return this.privateglobalmute;
            }

            set
            {
                this.privateglobalmute = value;
                this.OnPropertyChanged("globalMute");
            }
        }

        /// <summary>
        /// Gets or sets public field avatar
        /// </summary>
        public string avatar
        {
            get
            {
                return this.privateavatar;
            }

            set
            {
                this.privateavatar = value;
                this.OnPropertyChanged("avatar");
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
        /// Gets or sets public field corporationeid
        /// </summary>
        public int corporationeid
        {
            get
            {
                return this.privatecorporationeid;
            }

            set
            {
                this.privatecorporationeid = value;
                this.OnPropertyChanged("corporationeid");
            }
        }

        /// <summary>
        /// Gets or sets public field allianceeid
        /// </summary>
        public int allianceeid
        {
            get
            {
                return this.privateallianceeid;
            }

            set
            {
                this.privateallianceeid = value;
                this.OnPropertyChanged("allianceeid");
            }
        }

        /// <summary>
        /// Gets or sets public field language
        /// </summary>
        public int language
        {
            get
            {
                return this.privatelanguage;
            }

            set
            {
                this.privatelanguage = value;
                this.OnPropertyChanged("language");
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
            this.characterID = 0;
            this.accountID = 0;
            this.rootEID = 0;
            this.nick = string.Empty;
            this.moodMessage = string.Empty;
            this.creation = DateTime.MinValue;
            this.lastLogOut = DateTime.MinValue;
            this.lastUsed = DateTime.MinValue;
            this.credit = 0;
            this.inUse = 0;
            this.totalMinsOnline = 0;
            this.activeChassis = 0;
            this.active = 0;
            this.deletedAt = DateTime.MinValue;
            this.baseEID = 0;
            this.defaultcorporationEID = 0;
            this.majorID = 0;
            this.raceID = 0;
            this.schoolID = 0;
            this.sparkID = 0;
            this.lastdocked = DateTime.MinValue;
            this.docked = 0;
            this.lastteleported = DateTime.MinValue;
            this.zoneID = 0;
            this.nickcorrected = 0;
            this.offensivenick = 0;
            this.positionX = 0;
            this.positionY = 0;
            this.homeBaseEID = 0;
            this.blockTrades = 0;
            this.globalMute = 0;
            this.avatar = string.Empty;
            this.note = string.Empty;
            this.corporationeid = 0;
            this.allianceeid = 0;
            this.privatelanguage = 0;
            this.language = 0;
        }

        /// <summary>
        /// saves a new record
        /// </summary>
        public void SaveNewRecord()
        {
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("Insert into characters ");
                sqlCommand.Append("(characterID, accountID, rootEID, nick, moodMessage, creation, lastLogOut, lastUsed, credit, inUse, totalMinsOnline, activeChassis, active, deletedAt, baseEID, defaultcorporationEID, majorID, raceID, schoolID, sparkID, lastdocked, docked, lastteleported, zoneID, nickcorrected, offensivenick, positionX, positionY, homeBaseEID, blockTrades, globalMute, avatar, note, corporationeid, allianceeid, language) ");
                sqlCommand.Append(" Values ");
                sqlCommand.Append("(@characterID, @accountID, @rootEID, @nick, @moodMessage, @creation, @lastLogOut, @lastUsed, @credit, @inUse, @totalMinsOnline, @activeChassis, @active, @deletedAt, @baseEID, @defaultcorporationEID, @majorID, @raceID, @schoolID, @sparkID, @lastdocked, @docked, @lastteleported, @zoneID, @nickcorrected, @offensivenick, @positionX, @positionY, @homeBaseEID, @blockTrades, @globalMute, @avatar, @note, @corporationeid, @allianceeid, @language) ");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@characterID", this.characterID);
                command.Parameters.AddWithValue("@accountID", this.accountID);
                command.Parameters.AddWithValue("@rootEID", this.rootEID);
                command.Parameters.AddWithValue("@nick", this.nick);
                command.Parameters.AddWithValue("@moodMessage", this.moodMessage);
                command.Parameters.AddWithValue("@creation", this.creation);
                command.Parameters.AddWithValue("@lastLogOut", this.lastLogOut);
                command.Parameters.AddWithValue("@lastUsed", this.lastUsed);
                command.Parameters.AddWithValue("@credit", this.credit);
                command.Parameters.AddWithValue("@inUse", this.inUse);
                command.Parameters.AddWithValue("@totalMinsOnline", this.totalMinsOnline);
                command.Parameters.AddWithValue("@activeChassis", this.activeChassis);
                command.Parameters.AddWithValue("@active", this.active);
                command.Parameters.AddWithValue("@deletedAt", this.deletedAt);
                command.Parameters.AddWithValue("@baseEID", this.baseEID);
                command.Parameters.AddWithValue("@defaultcorporationEID", this.defaultcorporationEID);
                command.Parameters.AddWithValue("@majorID", this.majorID);
                command.Parameters.AddWithValue("@raceID", this.raceID);
                command.Parameters.AddWithValue("@schoolID", this.schoolID);
                command.Parameters.AddWithValue("@sparkID", this.sparkID);
                command.Parameters.AddWithValue("@lastdocked", this.lastdocked);
                command.Parameters.AddWithValue("@docked", this.docked);
                command.Parameters.AddWithValue("@lastteleported", this.lastteleported);
                command.Parameters.AddWithValue("@zoneID", this.zoneID);
                command.Parameters.AddWithValue("@nickcorrected", this.nickcorrected);
                command.Parameters.AddWithValue("@offensivenick", this.offensivenick);
                command.Parameters.AddWithValue("@positionX", this.positionX);
                command.Parameters.AddWithValue("@positionY", this.positionY);
                command.Parameters.AddWithValue("@homeBaseEID", this.homeBaseEID);
                command.Parameters.AddWithValue("@blockTrades", this.blockTrades);
                command.Parameters.AddWithValue("@globalMute", this.globalMute);
                command.Parameters.AddWithValue("@avatar", this.avatar);
                command.Parameters.AddWithValue("@note", this.note);
                command.Parameters.AddWithValue("@corporationeid", this.corporationeid);
                command.Parameters.AddWithValue("@allianceeid", this.allianceeid);
                command.Parameters.AddWithValue("@language", this.language);

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
                //sqlCommand.Append("UPDATE characters Set accountID= @accountID, rootEID= @rootEID, nick= @nick, moodMessage= @moodMessage, creation= @creation, lastLogOut= @lastLogOut, lastUsed= @lastUsed, credit= @credit, inUse= @inUse, totalMinsOnline= @totalMinsOnline, activeChassis= @activeChassis, active= @active, deletedAt= @deletedAt, baseEID= @baseEID, defaultcorporationEID= @defaultcorporationEID, majorID= @majorID, raceID= @raceID, schoolID= @schoolID, sparkID= @sparkID, lastdocked= @lastdocked, docked= @docked, lastteleported= @lastteleported, zoneID= @zoneID, nickcorrected= @nickcorrected, offensivenick= @offensivenick, positionX= @positionX, positionY= @positionY, homeBaseEID= @homeBaseEID, blockTrades= @blockTrades, globalMute= @globalMute, avatar= @avatar, note= @note, corporationeid= @corporationeid, allianceeid= @allianceeid, language= @language where characterID = @characterID");

                sqlCommand.Append("UPDATE characters Set accountID= @accountID, rootEID= @rootEID, nick= @nick, moodMessage= @moodMessage, creation= @creation, lastLogOut= @lastLogOut, lastUsed= @lastUsed, credit= @credit, inUse= @inUse, totalMinsOnline= @totalMinsOnline, activeChassis= @activeChassis, active= @active, baseEID= @baseEID, defaultcorporationEID= @defaultcorporationEID, majorID= @majorID, raceID= @raceID, schoolID= @schoolID, sparkID= @sparkID, lastdocked= @lastdocked, docked= @docked, lastteleported= @lastteleported, zoneID= @zoneID, nickcorrected= @nickcorrected, offensivenick= @offensivenick, positionX= @positionX, positionY= @positionY, homeBaseEID= @homeBaseEID, blockTrades= @blockTrades, globalMute= @globalMute, avatar= @avatar, note= @note, corporationeid= @corporationeid, allianceeid= @allianceeid, language= @language where characterID = @characterID");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@characterID", this.characterID);
                command.Parameters.AddWithValue("@accountID", this.accountID);
                command.Parameters.AddWithValue("@rootEID", this.rootEID);
                command.Parameters.AddWithValue("@nick", this.nick);
                command.Parameters.AddWithValue("@moodMessage", this.moodMessage);
                command.Parameters.AddWithValue("@creation", this.creation);
                command.Parameters.AddWithValue("@lastLogOut", this.lastLogOut);
                command.Parameters.AddWithValue("@lastUsed", this.lastUsed);
                command.Parameters.AddWithValue("@credit", this.credit);
                command.Parameters.AddWithValue("@inUse", this.inUse);
                command.Parameters.AddWithValue("@totalMinsOnline", this.totalMinsOnline);
                command.Parameters.AddWithValue("@activeChassis", this.activeChassis);
                command.Parameters.AddWithValue("@active", this.active);
                //command.Parameters.AddWithValue("@deletedAt", this.deletedAt); // FIXME. 
                command.Parameters.AddWithValue("@baseEID", this.baseEID);
                command.Parameters.AddWithValue("@defaultcorporationEID", this.defaultcorporationEID);
                command.Parameters.AddWithValue("@majorID", this.majorID);
                command.Parameters.AddWithValue("@raceID", this.raceID);
                command.Parameters.AddWithValue("@schoolID", this.schoolID);
                command.Parameters.AddWithValue("@sparkID", this.sparkID);
                command.Parameters.AddWithValue("@lastdocked", this.lastdocked);
                command.Parameters.AddWithValue("@docked", this.docked);
                command.Parameters.AddWithValue("@lastteleported", this.lastteleported);
                command.Parameters.AddWithValue("@zoneID", this.zoneID);
                command.Parameters.AddWithValue("@nickcorrected", this.nickcorrected);
                command.Parameters.AddWithValue("@offensivenick", this.offensivenick);
                command.Parameters.AddWithValue("@positionX", this.positionX);
                command.Parameters.AddWithValue("@positionY", this.positionY);
                command.Parameters.AddWithValue("@homeBaseEID", this.homeBaseEID);
                command.Parameters.AddWithValue("@blockTrades", this.blockTrades);
                command.Parameters.AddWithValue("@globalMute", this.globalMute);
                command.Parameters.AddWithValue("@avatar", this.avatar);
                command.Parameters.AddWithValue("@note", this.note);
                command.Parameters.AddWithValue("@corporationeid", this.corporationeid);
                command.Parameters.AddWithValue("@allianceeid", this.allianceeid);
                command.Parameters.AddWithValue("@language", this.language);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public List<Characters> GetCharactersOnAccount(int AccountIdNumber)
        {
            List<Characters> list = new List<Characters>();

            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("SELECT * from characters Where AccountID=@acctid");
                    command.CommandText = sqlCommand.ToString();
                    command.Parameters.AddWithValue("@acctid", AccountIdNumber);
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Characters tmp = new Characters(this.ConnString);

                            tmp.characterID = Convert.ToInt32(reader["characterID"]);
                            tmp.accountID = Convert.ToInt32(reader["accountID"]);
                            tmp.rootEID = Convert.ToInt64(reader["rootEID"]);
                            tmp.nick = Convert.ToString(reader["nick"]);
                            tmp.moodMessage = Convert.ToString(reader["moodMessage"]);
                            tmp.creation = Convert.ToDateTime(reader["creation"]);
                            tmp.lastLogOut = Convert.ToDateTime(reader["lastLogOut"]);
                            tmp.lastUsed = Convert.ToDateTime(reader["lastUsed"]);
                            tmp.credit = Convert.ToDecimal(reader["credit"]);
                            tmp.inUse = Convert.ToInt32(reader["inUse"]);
                            tmp.totalMinsOnline = Convert.ToInt32(reader["totalMinsOnline"]);
                            tmp.activeChassis = Convert.ToInt64(reader["activeChassis"]);
                            tmp.active = Convert.ToInt32(reader["active"]);
                            if (reader["deletedAt"] != DBNull.Value) { tmp.deletedAt = Convert.ToDateTime(reader["deletedAt"]); }
                            tmp.baseEID = Convert.ToInt32(reader["baseEID"]);
                            tmp.defaultcorporationEID = Convert.ToInt32(reader["defaultcorporationEID"]);
                            tmp.majorID = Convert.ToInt32(reader["majorID"]);
                            tmp.raceID = Convert.ToInt32(reader["raceID"]);
                            tmp.schoolID = Convert.ToInt32(reader["schoolID"]);
                            tmp.sparkID = Convert.ToInt32(reader["sparkID"]);
                            if (reader["lastdocked"] != DBNull.Value) { tmp.lastdocked = Convert.ToDateTime(reader["lastdocked"]); }
                            tmp.docked = Convert.ToInt32(reader["docked"]);

                            if (reader["lastteleported"] != DBNull.Value) { tmp.lastteleported = Convert.ToDateTime(reader["lastteleported"]); }

                            if (reader["zoneID"] != DBNull.Value) { tmp.zoneID = Convert.ToInt32(reader["zoneID"]); }
                            tmp.nickcorrected = Convert.ToInt32(reader["nickcorrected"]);
                            tmp.offensivenick = Convert.ToInt32(reader["offensivenick"]);
                            if (reader["positionX"] != DBNull.Value) { tmp.positionX = Convert.ToDecimal(reader["positionX"]); }
                            if (reader["positionY"] != DBNull.Value) { tmp.positionY = Convert.ToDecimal(reader["positionY"]); }
                            if (reader["homeBaseEID"] != DBNull.Value) { tmp.homeBaseEID = Convert.ToInt32(reader["homeBaseEID"]); }
                            tmp.blockTrades = Convert.ToInt32(reader["blockTrades"]);
                            tmp.globalMute = Convert.ToInt32(reader["globalMute"]);
                            tmp.avatar = Convert.ToString(reader["avatar"]);
                            tmp.note = Convert.ToString(reader["note"]);
                            tmp.corporationeid = Convert.ToInt32(reader["corporationeid"]);
                            tmp.allianceeid = Convert.ToInt32(reader["allianceeid"]);
                            tmp.language = Convert.ToInt32(reader["language"]);

                            list.Add(tmp);
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// fires when properties are set.
        /// </summary>
        /// <param name='name'>name of property being changed</param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
