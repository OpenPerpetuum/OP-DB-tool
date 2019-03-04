
using System;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using PerpTool.db;
using System.Collections.Generic;

namespace Perptool.db
{
    public class ArtifactLootItem
    {
        public int LootID { get; set; }
        public int ArtifactTypeID { get; set; }
        public string ArtifactTypeName { get; set; }
        public int LootDefinition { get; set; }
        public string LootDefinitionName { get; set; }
        public decimal LootProbability { get; set; }
        public int LootMaxQuantity { get; set; }
        public int LootMinQuantity { get; set; }
        public int LootRepackaged { get; set; }
        public DBAction dBAction { get; set; }

        public static string IDKey = "@artifactLootID";

        public static string GetLootDeclStatment()
        {
            return "DECLARE " + EntityDefaults.IDkey + " int;";
        }

        public static string GetDeclStatement()
        {
            return "DECLARE " + IDKey + " int;";
        }

        public string GetArtifactTypeDefinitionLokupStatement()
        {
            return "SET " + ArtifactTypesTable.IDKey + " = (SELECT TOP 1 id from artifacttypes WHERE [name] = '" + this.ArtifactTypeName + "');";
        }

        public string GetLootDefinitionLookupStatement()
        {
            return "SET " + EntityDefaults.IDkey + " = (SELECT TOP 1 definition from entitydefaults WHERE [definitionname] = '" + this.LootDefinitionName + "');";
        }

        public string GetLookupStatement()
        {
            return "SET " + ArtifactLootItem.IDKey + " = (SELECT TOP 1 id FROM artifactloot WHERE definition = " + EntityDefaults.IDkey + "  AND artifacttype = " + ArtifactTypesTable.IDKey + ");";
        }

        public static ArtifactLootItem CreateNewForArtifactType(ArtifactTypesTable _type, EntityDefaults entity)
        {
            ArtifactLootItem item = new ArtifactLootItem();
            item.ArtifactTypeID = _type.id;
            item.ArtifactTypeName = _type.name;
            item.dBAction = DBAction.INSERT;
            item.LootDefinition = entity.definition;
            item.LootDefinitionName = entity.definitionname;
            item.LootMaxQuantity = 1;
            item.LootProbability = 0.5M;
            item.LootRepackaged = 1;
            return item;
        }

    }

    public class ArtifactLoot : INotifyPropertyChanged
    {

        private int privateid;
        private int privateartifacttype;
        private int privatelootdefinition;
        private int privateminquantity;
        private int privatemaxquantity;
        private decimal privatechance;
        private int privaterepackaged;

        public ArtifactLoot(string connectionString)
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

        public int artifactType
        {
            get
            {
                return this.privateartifacttype;
            }

            set
            {
                this.privateartifacttype = value;
                this.OnPropertyChanged("artifacttype");
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

        public int maxquantity
        {
            get
            {
                return this.privatemaxquantity;
            }

            set
            {
                this.privatemaxquantity = value;
                this.OnPropertyChanged("maxquantity");
            }
        }

        public decimal chance
        {
            get
            {
                return this.privatechance;
            }

            set
            {
                this.privatechance = value;
                this.OnPropertyChanged("chance");
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





        /// <summary>
        /// Gets or sets connection string
        /// </summary>
        private string ConnString { get; set; }
        #endregion



        public void Clear()
        {
            this.id = 0;
            this.artifactType = 0;
            this.lootdefinition = 0;
            this.minquantity = 0;
            this.maxquantity = 0;
            this.chance = 0;
            this.repackaged = 0;
        }

        public void updateSelf(ArtifactLootItem loot)
        {
            this.id = loot.LootID;
            this.artifactType = loot.ArtifactTypeID;
            this.lootdefinition = loot.LootDefinition;
            this.minquantity = loot.LootMinQuantity;
            this.maxquantity = loot.LootMaxQuantity;
            this.chance = loot.LootProbability;
            this.repackaged = loot.LootRepackaged;
        }

        public void GetbyID(int id)
        {
            SqlConnection conn = new SqlConnection(this.ConnString);
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("SELECT * FROM artifactloot WHERE id=@id;");
                command.CommandText = sqlCommand.ToString();
                command.Parameters.AddWithValue("@id", id);
                command.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.id = Convert.ToInt32(reader["id"]);
                        this.artifactType = Convert.ToInt32(reader["artifacttype"]);
                        this.lootdefinition = Convert.ToInt32(reader["definition"]);
                        this.lootdefinition = Convert.ToInt32(reader["lootdefinition"]);
                        this.minquantity = Convert.ToInt32(reader["minquantity"]);
                        this.maxquantity = Convert.ToInt32(reader["maxquantity"]);
                        this.chance = Convert.ToDecimal(reader["chance"]);
                        this.repackaged = Convert.ToInt32(reader["packed"]);
                    }
                }
                conn.Dispose();
            }
        }


        public ObservableCollection<ArtifactLootItem> GetLootByArtifactTypeID(int artifactType)
        {
            ObservableCollection<ArtifactLootItem> list = new ObservableCollection<ArtifactLootItem>();

            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append(@"
                    SELECT entitydefaults.definitionname, artifacttypes.name, artifactloot.* FROM artifactloot
                    JOIN entitydefaults ON entitydefaults.definition = artifactloot.definition
                    JOIN artifacttypes ON artifactloot.artifacttype = artifacttypes.id
                    WHERE artifacttype = @artifacttype;");
                    command.Parameters.AddWithValue("@artifacttype", artifactType);
                    command.CommandText = sqlCommand.ToString();
                    command.Connection = conn;
                    conn.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ArtifactLootItem tmp = new ArtifactLootItem();
                            tmp.ArtifactTypeID = artifactType;
                            tmp.LootID = Convert.ToInt32(reader["id"]);
                            tmp.LootDefinitionName = Convert.ToString(reader["definitionname"]);
                            tmp.ArtifactTypeName = Convert.ToString(reader["name"]);
                            tmp.LootDefinition = Convert.ToInt32(reader["definition"]);
                            tmp.LootMinQuantity = Convert.ToInt32(reader["minquantity"]);
                            tmp.LootMaxQuantity = Convert.ToInt32(reader["maxquantity"]);
                            tmp.LootProbability = Convert.ToDecimal(reader["chance"]);
                            tmp.LootRepackaged = Convert.ToInt32(reader["packed"]);
                            list.Add(tmp);
                        }
                    }
                    conn.Dispose();
                }
            }
            return list;
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
                sqlCommand.Append(@"UPDATE artifactloot SET 
                [definition]="+ EntityDefaults.IDkey+",[artifacttype]="+ ArtifactTypesTable.IDKey+",[minquantity]=@minquantity,[maxquantity]=@maxquantity,[chance]=@chance,[packed]=@packed " +
                "WHERE [id]="+ ArtifactLootItem.IDKey + ";");
                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue(ArtifactLootItem.IDKey, this.id);
                command.Parameters.AddWithValue(EntityDefaults.IDkey, this.lootdefinition);
                command.Parameters.AddWithValue(ArtifactTypesTable.IDKey, this.artifactType);
                command.Parameters.AddWithValue("@minquantity", this.minquantity);
                command.Parameters.AddWithValue("@maxquantity", this.maxquantity);
                command.Parameters.AddWithValue("@chance", this.chance);
                command.Parameters.AddWithValue("@packed", this.repackaged);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                query = Utilities.parseCommandString(command, new List<string>(new string[] { EntityDefaults.IDkey, ArtifactLootItem.IDKey, ArtifactTypesTable.IDKey}));
            }
            return query;
        }

        public string Insert()
        {
            string query = "";
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append(@"INSERT INTO[dbo].[artifactloot] ([artifacttype],[definition],[minquantity],[maxquantity],[chance],[packed])
                VALUES ("+ ArtifactTypesTable.IDKey + ", "+ EntityDefaults.IDkey + ", @minquantity, @maxquantity, @chance, @packed)");
                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue(EntityDefaults.IDkey, this.lootdefinition);
                command.Parameters.AddWithValue(ArtifactTypesTable.IDKey, this.artifactType);
                command.Parameters.AddWithValue("@minquantity", this.minquantity);
                command.Parameters.AddWithValue("@maxquantity", this.maxquantity);
                command.Parameters.AddWithValue("@chance", this.chance);
                command.Parameters.AddWithValue("@packed", this.repackaged);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                query = Utilities.parseCommandString(command, new List<string>(new string[] { EntityDefaults.IDkey, ArtifactTypesTable.IDKey }));
            }
            return query;
        }

        public string Delete()
        {
            string query = "";
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("DELETE FROM [dbo].[artifactloot] WHERE id="+ ArtifactLootItem.IDKey+";");
                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue(ArtifactLootItem.IDKey, this.id);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                query = Utilities.parseCommandString(command, new List<string>(new string[] { ArtifactLootItem.IDKey}));
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
    }
}

