using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Perpetuum;
using Perpetuum.GenXY;

namespace Perptool.db
{
    public class RobotTemplate : INotifyPropertyChanged
    {
        public int recordID { get; set; }
        public string recordName { get; set; }
        public string recordDescription { get; set; }
        public string recordNote { get; set; }
        public int robotID { get; set; }
        public int headID { get; set; }
        public int chassisID { get; set; }
        public int legID { get; set; }
        public int containerID { get; set; }
        public string robotName { get; set; }
        public string headName { get; set; }
        public string chassisName { get; set; }
        public string legName { get; set; }
        public string containerName { get; set; }
        public ObservableCollection<ModuleTemplate> headModules { get; set; }
        public ObservableCollection<ModuleTemplate> chassisModules { get; set; }
        public ObservableCollection<ModuleTemplate> legModules { get; set; }
        public ObservableCollection<ItemTemplate> items { get; set; }

        public RobotTemplate()
        {
            headModules = new ObservableCollection<ModuleTemplate>();
            chassisModules = new ObservableCollection<ModuleTemplate>();
            legModules = new ObservableCollection<ModuleTemplate>();
            items = new ObservableCollection<ItemTemplate>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public static RobotTemplate CreateFromRecord(int id, string name, string descGenXY, string note)
        {
            Dictionary<string, object> dict = GenxyConverter.Deserialize(descGenXY);
            RobotTemplate botTemp = RobotTemplate.GetRobotTemplateFromXY(dict);
            botTemp.recordNote = note;
            botTemp.recordID = id;
            botTemp.recordName = name;
            botTemp.recordDescription = descGenXY;
            return botTemp;
        }

        private static RobotTemplate GetRobotTemplateFromXY(IDictionary<string, object> d)
        {
            RobotTemplate robotTemplate = new RobotTemplate();
            bool success = d.TryGetValue("robot", out object rob);
            robotTemplate.robotID = Convert.ToInt32(rob);

            success = d.TryGetValue("head", out object heado) && success;
            robotTemplate.headID = Convert.ToInt32(heado);

            success = d.TryGetValue("chassis", out object chasso) && success;
            robotTemplate.chassisID = Convert.ToInt32(chasso);

            success = d.TryGetValue("leg", out object lego) && success;
            robotTemplate.legID = Convert.ToInt32(lego);

            success = d.TryGetValue("container", out object cono) && success;
            robotTemplate.legID = Convert.ToInt32(cono);

            success = d.TryGetValue("headModules", out object hmod) && success;
            Dictionary<string, object> headmods = (Dictionary<string, object>)hmod;
            robotTemplate.headModules = ModulesFromDictionary((IDictionary<string, object>)headmods);

            success = d.TryGetValue("chassisModules", out object cmod) && success;
            Dictionary<string, object> chassMods = (Dictionary<string, object>)cmod;
            robotTemplate.chassisModules = ModulesFromDictionary((IDictionary<string, object>)chassMods);

            success = d.TryGetValue("legModules", out object lmod) && success;
            Dictionary<string, object> legMods = (Dictionary<string, object>)lmod;
            robotTemplate.legModules = ModulesFromDictionary((IDictionary<string, object>)legMods);

            success = d.TryGetValue("items", out object imod) && success;
            Dictionary<string, object> items = (Dictionary<string, object>)imod;
            robotTemplate.items = ItemsFromDictionary((IDictionary<string, object>)items);

            return robotTemplate;
        }

        private static ObservableCollection<ModuleTemplate> ModulesFromDictionary(IDictionary<string, object> d)
        {
            ObservableCollection<ModuleTemplate> mods = new ObservableCollection<ModuleTemplate>();
            if (d == null)
            {
                return mods;
            }
            foreach (IDictionary<string, object> dictionary in (IEnumerable<object>)d.Values)
            {
                ModuleTemplate fromDictionary = ModuleTemplate.CreateFromDictionary(dictionary);
                mods.Add(fromDictionary);
            }
            return mods;
        }


        private static ObservableCollection<ItemTemplate> ItemsFromDictionary(IDictionary<string, object> d)
        {
            ObservableCollection<ItemTemplate> items = new ObservableCollection<ItemTemplate>();
            if (d == null)
            {
                return items;
            }
            foreach (IDictionary<string, object> dictionary in (IEnumerable<object>)d.Values)
            {
                ItemTemplate fromDictionary = ItemTemplate.CreateFromDictionary(dictionary);
                items.Add(fromDictionary);
            }
            return items;
        }

        private Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary["robot"] = this.robotID;
            dictionary["head"] = this.headID;
            dictionary["chassis"] = this.chassisID;
            dictionary["leg"] = this.legID;
            dictionary["container"] = this.containerID;
            dictionary["headModules"] = (object)((IEnumerable<ModuleTemplate>)this.headModules).ToDictionary<ModuleTemplate>("m", (Converter<ModuleTemplate, object>)(m => (object)m.ToDictionary()));
            dictionary["chassisModules"] = (object)((IEnumerable<ModuleTemplate>)this.chassisModules).ToDictionary<ModuleTemplate>("m", (Converter<ModuleTemplate, object>)(m => (object)m.ToDictionary()));
            dictionary["legModules"] = (object)((IEnumerable<ModuleTemplate>)this.legModules).ToDictionary<ModuleTemplate>("m", (Converter<ModuleTemplate, object>)(m => (object)m.ToDictionary()));
            dictionary["items"] = (object)((IEnumerable<ItemTemplate>)this.items).ToDictionary<ItemTemplate>("i", (Converter<ItemTemplate, object>)(i => (object)i.ToDictionary()));
            return dictionary;
        }

        public string ToGenXY()
        {
            return GenxyConverter.Serialize(this.ToDictionary());
        }
    }

    public class ModuleTemplate : INotifyPropertyChanged
    {
        public int definition { get; set; }
        public string definitionName { get; set; }
        public int slot { get; set; }
        public int ammoDefinition { get; set; }
        public string ammoDefinitionName { get; set; }
        public int ammoQuantity { get; set; }
        public ModuleTemplate(int definition, int slot, int ammoDefinition = 0, int ammoQuantity = 0, string defName = "", string ammoName = "")
        {
            this.definition = definition;
            this.slot = slot;
            this.ammoDefinition = ammoDefinition;
            this.ammoQuantity = ammoQuantity;
            this.definitionName = defName;
            this.ammoDefinitionName = ammoName;
        }

        public ModuleTemplate()
        {
        }

        public static ModuleTemplate CreateFromDictionary(IDictionary<string, object> d)
        {
            bool success = d.TryGetValue("definition", out object o);
            int definition = Convert.ToInt32(o);
            success = d.TryGetValue("slot", out o) && success;
            int slot = Convert.ToInt32(o);
            success = d.TryGetValue("ammoDefinition", out o) && success;
            if (success)
            {
                int ammoDef = Convert.ToInt32(o);
                success = d.TryGetValue("ammoQuantity", out o) && success;
                int ammoQuan = Convert.ToInt32(o);
                return new ModuleTemplate(definition, slot, ammoDef, ammoQuan);
            }
            else
            {
                return new ModuleTemplate(definition, slot);
            }
        }

        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary["definition"] = this.definition;
            dictionary["slot"] = this.slot;
            if (this.ammoDefinition>0)
            {
                dictionary["ammoDefinition"] = this.ammoDefinition;
                dictionary["ammoQuantity"] = this.ammoQuantity;
            }
            return dictionary;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }

    public class ItemTemplate : INotifyPropertyChanged
    {
        public int definition { get; set; }
        public string definitionName { get; set; }
        public int quantity { get; set; }
        public int repackaged { get; set; }

        ItemTemplate(int definition, int quantity, int repackaged)
        {
            this.definition = definition;
            this.quantity = quantity;
            this.repackaged = repackaged;
        }

        public static ItemTemplate CreateFromDictionary(IDictionary<string, object> d)
        {
            bool success = d.TryGetValue("definition", out object o);
            int definition = Convert.ToInt32(o);
            success = d.TryGetValue("quantity", out o) && success;
            int quantity = Convert.ToInt32(o);
            success = d.TryGetValue("repackaged", out o) && success;
            int repackaged = Convert.ToInt32(o);
            return new ItemTemplate(definition, quantity, repackaged);
        }

        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary["definition"] = this.definition;
            dictionary["quantity"] = this.quantity;
            dictionary["repackaged"] = this.repackaged;
            return dictionary;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }


    public class BotTemplateDropdownItem
    {
        public int id { get; set; }
        public string name { get; set; }
    }


    public class RobotTemplatesTable : INotifyPropertyChanged
    {
        private int privateid;
        private string privatename;
        private GenxyString privategenxy;
        private string privatenote;

        public RobotTemplatesTable(string connectionString)
        {
            this.ConnString = connectionString;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #region Fields
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

        public string description
        {
            get
            {
                return this.privategenxy;
            }

            set
            {
                this.privategenxy = value;
                this.OnPropertyChanged("description");
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
            this.id = 0;
            this.name = string.Empty;
            this.description = string.Empty;
            this.note = string.Empty;
        }

        public string SaveBotTemplate(RobotTemplate bot)
        {
            string query = string.Empty;
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("UPDATE robottemplates SET name=@name, description=@description, note=@note WHERE id=@id;");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@id", bot.recordID);
                command.Parameters.AddWithValue("@name", bot.recordName);
                command.Parameters.AddWithValue("@description", bot.ToGenXY());
                command.Parameters.AddWithValue("@note", bot.recordNote);

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

        public string SaveNewBotTemplate()
        {
            string query = string.Empty;
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("INSERT INTO robottemplates ([name], [description], [note]) VALUES (@name, @description, @note)");

                command.CommandText = sqlCommand.ToString();

                command.Parameters.AddWithValue("@name", this.name);
                command.Parameters.AddWithValue("@description", this.description);
                command.Parameters.AddWithValue("@note", this.note);

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

        public RobotTemplate GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("SELECT * FROM robottemplates WHERE id=@id;");
                    command.CommandText = sqlCommand.ToString();
                    command.Parameters.AddWithValue("@id", id);
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.id = Convert.ToInt32(reader["id"]);
                            this.name = Convert.ToString(reader["name"]);
                            this.description = Convert.ToString(reader["description"]);
                            this.note = Convert.ToString(reader["note"]);
                            return RobotTemplate.CreateFromRecord(this.id, this.name, this.description, this.note);
                        }
                    }
                }
            }
            return null;
        }

        public List<BotTemplateDropdownItem> getAll()
        {
            List<BotTemplateDropdownItem> temps = new List<BotTemplateDropdownItem>();
            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("SELECT * FROM robottemplates");
                    command.CommandText = sqlCommand.ToString();
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BotTemplateDropdownItem tmp = new BotTemplateDropdownItem();
                            tmp.id = Convert.ToInt32(reader["id"]);
                            tmp.name = Convert.ToString(reader["name"]);
                            temps.Add(tmp);
                        }
                    }
                }
            }
            return temps;
        }

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
