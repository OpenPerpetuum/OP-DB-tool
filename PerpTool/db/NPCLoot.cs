
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace Perptool.db
{
    public enum DBAction
    {
        UPDATE,
        INSERT,
        DELETE
    }


    public class LootItem
    {
        public int NPCDefinition { get; set; }
        public int NPCLootID { get; set; }
        public int LootDefinition { get; set; }
        public string LootDefinitionName { get; set; }
        public decimal LootProbability { get; set; }
        public int LootQuantity { get; set; }
        public int LootMinQuantity { get; set; }
        public int LootRepackaged { get; set; }
        public int LootDontDamage { get; set; }
        public DBAction recordAction { get; set; }
    }

    public class NPCLoot : INotifyPropertyChanged
    {

        private int privateid;
        private int privatedefinition;
        private int privatelootdefinition;
        private int privatequantity;
        private decimal privateprobability;
        private int privaterepackaged;
        private int privatedontdamage;
        private int privateminquantity;

        public NPCLoot(string connectionString)
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

        public int lootdefinition
        {
            get
            {
                return this.privatelootdefinition;
            }

            set
            {
                this.privatelootdefinition = value;
                this.OnPropertyChanged("lootdefinition");
            }
        }

        public int quantity
        {
            get
            {
                return this.privatequantity;
            }

            set
            {
                this.privatequantity = value;
                this.OnPropertyChanged("quantity");
            }
        }

        public decimal probability
        {
            get
            {
                return this.privateprobability;
            }

            set
            {
                this.privateprobability = value;
                this.OnPropertyChanged("probability");
            }
        }

        public int repackaged
        {
            get
            {
                return this.privaterepackaged;
            }

            set
            {
                this.privaterepackaged = value;
                this.OnPropertyChanged("repackaged");
            }
        }
        public int dontdamage
        {
            get
            {
                return this.privatedontdamage;
            }

            set
            {
                this.privatedontdamage = value;
                this.OnPropertyChanged("dontdamage");
            }
        }
        public int minquantity
        {
            get
            {
                return this.privateminquantity;
            }

            set
            {
                this.privateminquantity = value;
                this.OnPropertyChanged("minquantity");
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
            this.definition = 0;
            this.lootdefinition = 0;
            this.quantity = 0;
            this.minquantity = 0;
            this.probability = 0;
            this.repackaged = 0;
            this.dontdamage = 0;
        }



        public List<NPCLoot> GetLootsById(int id)
        {
            List<NPCLoot> looties = new List<NPCLoot>();
            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("SELECT * FROM npcloot WHERE id=@id;");
                command.CommandText = sqlCommand.ToString();
                command.Parameters.AddWithValue("@id", id);
                command.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        NPCLoot temp = new NPCLoot(this.ConnString);
                        temp.id = Convert.ToInt32(reader["id"]);
                        temp.definition = Convert.ToInt32(reader["definition"]);
                        temp.lootdefinition = Convert.ToInt32(reader["lootdefinition"]);
                        temp.quantity = Convert.ToInt32(reader["quantity"]);
                        temp.probability = Convert.ToDecimal(reader["probability"]);
                        temp.repackaged = Convert.ToInt32(reader["repackaged"]);
                        temp.dontdamage = Convert.ToInt32(reader["dontdamage"]);
                        temp.minquantity = Convert.ToInt32(reader["minquantity"]);
                        looties.Add(temp);
                    }
                }

                conn.Dispose();
            }
            return looties;
        }

        public void updateSelf(LootItem loot)
        {
            this.id = loot.NPCLootID;
            this.definition = loot.NPCDefinition;
            this.lootdefinition = loot.LootDefinition;
            this.quantity = loot.LootQuantity;
            this.probability = loot.LootProbability;
            this.repackaged = loot.LootRepackaged;
            this.dontdamage = loot.LootDontDamage;
            this.minquantity = loot.LootMinQuantity;
        }

        public void GetbyID(int id)
        {
            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("SELECT * FROM npcloot WHERE id=@id;");
                command.CommandText = sqlCommand.ToString();
                command.Parameters.AddWithValue("@id", id);
                command.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        NPCLoot temp = new NPCLoot(this.ConnString);
                        this.id = Convert.ToInt32(reader["id"]);
                        this.definition = Convert.ToInt32(reader["definition"]);
                        this.lootdefinition = Convert.ToInt32(reader["lootdefinition"]);
                        this.quantity = Convert.ToInt32(reader["quantity"]);
                        this.probability = Convert.ToDecimal(reader["probability"]);
                        this.repackaged = Convert.ToInt32(reader["repackaged"]);
                        this.dontdamage = Convert.ToInt32(reader["dontdamage"]);
                        this.minquantity = Convert.ToInt32(reader["minquantity"]);
                    }
                }
                conn.Dispose();
            }
        }

        /// <summary>
        /// saves a new record
        /// </summary>
        public void SaveNewRecord()
        {
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("Insert into npcloot ");
                sqlCommand.Append("(id, definition, lootdefinition, quantity, probability, repackaged, dontdamage, minquantity) ");
                sqlCommand.Append(" Values ");
                sqlCommand.Append("(@id, @definition, @lootdefinition, @quantity, @probability, @repackaged, @dontdamage, @minquantity) ;");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@id", this.id);
                command.Parameters.AddWithValue("@definition", this.definition);
                command.Parameters.AddWithValue("@lootdefinition", this.lootdefinition);
                command.Parameters.AddWithValue("@quantity", this.quantity);
                command.Parameters.AddWithValue("@probability", this.probability);
                command.Parameters.AddWithValue("@repackaged", this.repackaged);
                command.Parameters.AddWithValue("@dontdamage", this.dontdamage);
                command.Parameters.AddWithValue("@minquantity", this.minquantity);


                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                Object Id = command.ExecuteScalar();
                if (Id != DBNull.Value)
                {
                    this.id = Convert.ToInt32(Id);
                }
                conn.Close();
            }
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
                sqlCommand.Append("UPDATE npcloot SET [definition]=@definition, [lootdefinition]=@lootdefinition, [quantity]=@quantity, [probability]=@probability, [repackaged]=@repackaged, [dontdamage]=@dontdamage, [minquantity]=@minquantity WHERE [id]=@id;");
                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@id", this.id);
                command.Parameters.AddWithValue("@definition", this.definition);
                command.Parameters.AddWithValue("@lootdefinition", this.lootdefinition);
                command.Parameters.AddWithValue("@quantity", this.quantity);
                command.Parameters.AddWithValue("@probability", this.probability);
                command.Parameters.AddWithValue("@repackaged", this.repackaged);
                command.Parameters.AddWithValue("@dontdamage", this.dontdamage);
                command.Parameters.AddWithValue("@minquantity", this.minquantity);

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

        public string Insert()
        {
            string query = "";
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("INSERT INTO [dbo].[npcloot] ([definition],[lootdefinition],[quantity],[probability],[repackaged],[dontdamage],[minquantity]) VALUES(@definition, @lootdefinition, @quantity, @probability, @repackaged,@dontdamage, @minquantity);");
                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@definition", this.definition);
                command.Parameters.AddWithValue("@lootdefinition", this.lootdefinition);
                command.Parameters.AddWithValue("@quantity", this.quantity);
                command.Parameters.AddWithValue("@probability", this.probability);
                command.Parameters.AddWithValue("@repackaged", this.repackaged);
                command.Parameters.AddWithValue("@dontdamage", this.dontdamage);
                command.Parameters.AddWithValue("@minquantity", this.minquantity);

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

        public ObservableCollection<LootItem> GetLootByDefinition(int npcBotDef)
        {
            ObservableCollection<LootItem> list = new ObservableCollection<LootItem>();

            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("SELECT dbo.entitydefaults.definitionname, npcloot.id as npclootID, npcloot.* FROM dbo.npcloot JOIN dbo.entitydefaults on npcloot.lootdefinition = entitydefaults.definition WHERE npcloot.definition=@definition ;");
                    command.Parameters.AddWithValue("@definition", npcBotDef);
                    command.CommandText = sqlCommand.ToString();
                    command.Connection = conn;
                    conn.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LootItem tmp = new LootItem();
                            tmp.NPCDefinition = npcBotDef;
                            tmp.NPCLootID = Convert.ToInt32(reader["npclootID"]);
                            tmp.LootDefinitionName = Convert.ToString(reader["definitionname"]);
                            tmp.LootDefinition = Convert.ToInt32(reader["lootdefinition"]);
                            tmp.LootQuantity = Convert.ToInt32(reader["quantity"]);
                            tmp.LootProbability = Convert.ToDecimal(reader["probability"]);
                            tmp.LootRepackaged = Convert.ToInt32(reader["repackaged"]);
                            tmp.LootDontDamage = Convert.ToInt32(reader["dontdamage"]);
                            tmp.LootMinQuantity = Convert.ToInt32(reader["minquantity"]);
                            list.Add(tmp);
                        }
                    }
                    conn.Dispose();
                }
            }
            return list;
        }


        public LootItem CreateNewLootForBot(EntityItems npcBotDef, EntityItems item)
        {
            LootItem tmp = new LootItem();
            tmp.NPCDefinition = npcBotDef.Definition;
            tmp.NPCLootID = -1;
            tmp.LootDefinitionName = item.Name;
            tmp.LootDefinition = item.Definition;
            tmp.LootQuantity = 1;
            tmp.LootProbability = 0.5M;
            tmp.LootRepackaged = 1;
            tmp.LootDontDamage = 1;
            tmp.LootMinQuantity = 0;
            return tmp;
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

