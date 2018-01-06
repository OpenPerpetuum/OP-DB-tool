using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Perptool.db;
using System.ComponentModel;
using System.IO;
using System.Collections.ObjectModel;
using PerpTool.Types;
using System.Linq;
using PerpTool.db;

namespace PerpTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private string Connstr = "Server=localhost\\PERPSQL;Database=perpetuumsa;Trusted_Connection=True;Pooling=True;Connection Timeout=30;Connection Lifetime=260;Connection Reset=True;Min Pool Size=20;Max Pool Size=60;";

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public MainWindow()
        {
            InitializeComponent();

            AgModifiers = new AggregateModifiers(Connstr);
            AgFields = new AggregateFields(Connstr);
            Entities = new EntityDefaults(Connstr);
            AgValues = new AggregateValues(Connstr);
            PerpAccounts = new Accounts(Connstr);
            PerpChars = new Characters(Connstr);
            ZoneTbl = new Zones(Connstr);
            Spawn = new NPCSpawn(Connstr);
            Loot = new NPCLoot(Connstr);
            BotBonus = new ChassisBonus(Connstr);
            NPCBotTemplates = new RobotTemplatesTable(Connstr);
            NPCTemplateRelations = new RobotTemplateRelation(Connstr);
            NPCPresenceTable = new NPCPresence(Connstr);
            NPCFlockTable = new NPCFlock(Connstr);

            EntityItems = Entities.GetEntitiesWithFields();
            ZoneList = ZoneTbl.GetAllZones();
            SpawnList = Spawn.GetAllSpawns();
            LootableBots = Entities.GetAllNPCLootableBots();
            LootableEntityDefaults = Entities.GetLootableEntities();
            BotItems = Entities.GetAllDistinctBotItems();
            NPCTemplates = NPCBotTemplates.getAll();
            AllNPCPresences = NPCPresenceTable.getAll();

            this.CatFlags = this.GetAllCategoryFlags();
            this.AllAggregateFields = AgFields.GetAllFields();
            this.AmmoList = Entities.GetAllAmmo();
            this.ModuleList = Entities.GetAllModules();
            this.NPCEntities = Entities.GetAllNPCEntities();
            this.NPCTemplateRelationList = new ObservableCollection<BotTemplateRelation>();
            this.SelectedNPCPresence = new ObservableCollection<NPCPresenceData>();
            this.NPCFlockList = new ObservableCollection<NPCFlockData>();
            this.BotTemplate = new ObservableCollection<RobotTemplate>();
            this.DataContext = this;
        }

        public IEnumerable<CategoryFlags> CatFlags { get; set; }
        private EntityItems currentSelection { get; set; }
        private AggregateModifiers AgModifiers { get; set; }
        private AggregateFields AgFields { get; set; }
        private EntityDefaults Entities { get; set; }
        private AggregateValues AgValues { get; set; }
        private Accounts PerpAccounts { get; set; }
        private Characters PerpChars { get; set; }
        public Zones ZoneTbl { get; set; }
        public NPCSpawn Spawn { get; set; }
        private RobotTemplateRelation NPCTemplateRelations { get; set; }
        private NPCPresence NPCPresenceTable { get; set; }
        private NPCFlock NPCFlockTable { get; set; }

        public List<EntityItems> AmmoList { get; set; }
        public List<EntityItems> ModuleList { get; set; }
        public List<EntityItems> BotItems { get; set; }
        public List<EntityItems> NPCEntities { get; set; }
        public List<NPCPresenceData> AllNPCPresences { get; set; }
        public List<AggregateFields> AllAggregateFields { get; set; }



        #region EntityDefaults

        // New methods to filter by CF
        private List<EntityItems> _privlist;
        public List<EntityItems> EntityItems
        {
            get
            {
                return this._privlist;
            }
            set
            {
                this._privlist = value;
                this.OnPropertyChanged("EntityItems");
            }
        }
        //private ObservableCollection<EntityDefaults> _privEntity;
        //public ObservableCollection<EntityDefaults> selectedEntity
        //{
        //    get
        //    {
        //        return this._privEntity;
        //    }
        //    set
        //    {
        //        this._privEntity = value;
        //        this.OnPropertyChanged("selectedEntity");
        //    }
        //}

        private EntityDefaults _privateSelectedEntity;
        public EntityDefaults SelectedEntity
        {
            get
            {
                return this._privateSelectedEntity;
            }
            set
            {
                this._privateSelectedEntity = value;
                OnPropertyChanged("SelectedEntity");
            }
        }

        private ObservableCollection<FieldValuesStuff> _valstuffs;
        public ObservableCollection<FieldValuesStuff> FieldValuesList
        {
            get
            {
                return _valstuffs;
            }
            set
            {
                _valstuffs = value;
                OnPropertyChanged("FieldValuesList");
            }
        }

        public IEnumerable<CategoryFlags> GetAllCategoryFlags()
        {
            return Enum.GetValues(typeof(CategoryFlags)).Cast<CategoryFlags>();
        }

        private CategoryFlags _privateCategoryFlag;
        public CategoryFlags SelectedCategoryFlag
        {
            get
            {
                return this._privateCategoryFlag;
            }
            set
            {
                this._privateCategoryFlag = value;
                OnPropertyChanged("SelectedCategoryFlag");
            }
        }

        private List<EntityDefaults> _EntitiesList;
        public List<EntityDefaults> EntitiesList
        {
            get
            {
                return this._EntitiesList;
            }
            set
            {
                this._EntitiesList = value;
                OnPropertyChanged("EntitiesList");
            }
        }
        private void ComboBox_DropDownClosed_CatFlag(object sender, EventArgs e)
        {
            //Still needs to perform query to grab entities by catflag at selection-confirm
            //Could be pre-populated... its a lot though
            this.EntitiesList = Entities.GetEntityItemsByCategory(SelectedCategoryFlag);
        }

        private AggregateFields selectedAggField;
        private void ComboBox_DropDownClosed_AggField(object sender, EventArgs e)
        {
            this.selectedAggField = (AggregateFields)AggFieldCombo.SelectedItem;
            System.Console.WriteLine(this.selectedAggField);
        }

        private void AggFieldAdd_Click(object sender, EventArgs e)
        {
            if (this.selectedAggField == null || this.SelectedEntity == null) { return; }
            FieldValuesStuff f = new FieldValuesStuff();
            f.dBAction = DBAction.INSERT;
            f.Definition = this.SelectedEntity.definition;
            f.FieldFormula = this.selectedAggField.formula;
            f.FieldId = this.selectedAggField.id;
            f.FieldMultiplier = this.selectedAggField.measurementmultiplier;
            f.FieldName = this.selectedAggField.name;
            f.FieldOffset = this.selectedAggField.measurementoffset;
            f.FieldUnits = this.selectedAggField.measurementunit;
            f.FieldValue = 0;
            f.ValueId = -1;
            this.FieldValuesList.Add(f);
        }

        private void AggFieldRemove_Click(object sender, EventArgs e)
        {
            if (this.FieldValuesList.Count <= 0) { return; }
            this.FieldValuesList.RemoveAt(this.FieldValuesList.Count - 1);
        }

        //Save/Insert
        private void EntityDefault_Save_New_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SelectedEntity != null)
                {
                    EntityDefaults entity = this.SelectedEntity;
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(handleAggregateFieldValuesSave(FieldValuesList));
                    sb.AppendLine(entity.SaveNewRecord());
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + entity.definitionname + ".sql", sb.ToString());
                    MessageBox.Show("Saved NEW Record!", "Info", 0, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }
        }

        //Save/update
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.SelectedEntity != null)
                {
                    EntityDefaults entity = this.SelectedEntity;
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(handleAggregateFieldValuesSave(FieldValuesList));
                    sb.AppendLine(entity.Save());
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + entity.definitionname + ".sql", sb.ToString());
                    MessageBox.Show("Saved Record!", "Info", 0, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }
        }

        private string handleAggregateFieldValuesSave(ObservableCollection<FieldValuesStuff> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (FieldValuesStuff item in list)
            {
                if (item.dBAction == DBAction.UPDATE)
                {
                    AgValues.GetById(item.ValueId);
                    if (AgValues.value != item.FieldValue)
                    {
                        AgValues.value = item.FieldValue;
                        sb.AppendLine(AgValues.Save());
                    }

                    AgFields.GetById(item.FieldId);
                    if (AgFields.formula != item.FieldFormula)
                    {
                        AgFields.formula = item.FieldFormula;
                        sb.AppendLine(AgFields.Save());
                    }
                }
                else if (item.dBAction == DBAction.INSERT)
                {
                    sb.AppendLine(AgValues.Insert(item));
                    item.dBAction = DBAction.UPDATE;

                    AgFields.GetById(item.FieldId);
                    if (AgFields.formula != item.FieldFormula)
                    {
                        AgFields.formula = item.FieldFormula;
                        sb.AppendLine(AgFields.Save()); //New AggValues use old AggFields -- this remains an update iff changed
                    }
                }
            }
            return sb.ToString();
        }

        private ObservableCollection<EntityOptions> _privOptions;
        public ObservableCollection<EntityOptions> selectedEntityOptions
        {
            get
            {
                return _privOptions;
            }
            set
            {
                _privOptions = value;
                OnPropertyChanged("selectedEntityOptions");
            }
        }

        public List<FieldValuesStuff> copyOfFields = new List<FieldValuesStuff>();
        private void CopyFields_click(object sender, EventArgs e)
        {
            copyOfFields = new List<FieldValuesStuff>(this.FieldValuesList);
        }

        private void PasteFields_Click(object sender, EventArgs e)
        {
            foreach (FieldValuesStuff fields in this.copyOfFields)
            {
                EntityDefaults entity = this.SelectedEntity;
                fields.Definition = entity.definition;
                fields.dBAction = DBAction.INSERT;
                this.FieldValuesList.Add(fields);
            }
        }

        #endregion

        #region Zones

        private List<NPCSpawn> _spawns;
        public List<NPCSpawn> SpawnList
        {
            get
            {
                return _spawns;
            }
            set
            {
                _spawns = value;
                OnPropertyChanged("SpawnList");
            }
        }

        private List<Zones> _zones;
        public List<Zones> ZoneList
        {
            get
            {
                return _zones;
            }
            set
            {
                _zones = value;
                OnPropertyChanged("ZoneList");
            }
        }

        private Zones _selzone;
        public Zones SelectedZone
        {
            get
            {
                return _selzone;
            }
            set
            {
                _selzone = value;
                OnPropertyChanged("SelectedZone");
            }
        }

        List<Accounts> _accts;
        public List<Accounts> AccountsList
        {
            get
            {
                return _accts;
            }
            set
            {
                _accts = value;
                OnPropertyChanged("AccountsList");
            }
        }
        private Accounts _selacct;
        public Accounts SelectedAcct
        {
            get
            {
                return _selacct;
            }
            set
            {
                _selacct = value;
                CharactersList = PerpChars.GetCharactersOnAccount(value.accountID);
            }
        }

        List<Characters> _chars;
        public List<Characters> CharactersList
        {
            get
            {
                return _chars;
            }
            set
            {
                _chars = value;
                OnPropertyChanged("CharactersList");
            }
        }

        private Characters _selchar;
        public Characters SelectedChar
        {
            get
            {
                return _selchar;
            }
            set
            {
                _selchar = value;
                OnPropertyChanged("SelectedChar");
            }
        }

        private int _eptoinject;
        public int EPToInject
        {
            get
            {
                return _eptoinject;
            }
            set
            {
                _eptoinject = value;
                OnPropertyChanged("EPToInject");
            }
        }

        private void GetAccounts_Click(object sender, RoutedEventArgs e)
        {
            AccountsList = PerpAccounts.GetAllAccounts();
        }

        private void SaveCharBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectedChar != null)
            {
                try
                {
                    this.SelectedChar.Save();
                    MessageBox.Show("Saved!", "Info", 0, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to save character!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SelectedAcct == null || SelectedAcct.accountID == 0) { return; }
            if (EPToInject <= 0)
            {
                MessageBox.Show("Really? No EP? Enter EP to inject!", "How much??", 0, MessageBoxImage.Error);
                return;
            }
            this.SelectedAcct.InsertEP(SelectedAcct.accountID, EPToInject);
            MessageBox.Show("Saved!", "Info", 0, MessageBoxImage.Information);
        }

        private void ZoneSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectedZone != null)
            {
                try
                {
                    SelectedZone.Save();
                    MessageBox.Show("Saved!", "Info", 0, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to Save!\n" + ex.Message, "Error!", 0, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No zone selected", "Error!", 0, MessageBoxImage.Warning);
            }
        }

        #endregion

        #region NPCTemplates

        private RobotTemplatesTable NPCBotTemplates { get; set; }
        //HACK
        private ObservableCollection<RobotTemplate> _temp;
        public ObservableCollection<RobotTemplate> BotTemplate
        {
            get
            {
                return _temp;
            }
            set
            {
                _temp = value;
                OnPropertyChanged("BotTemplate");
            }
        }


        private List<BotTemplateDropdownItem> _NPCTemplates;
        public List<BotTemplateDropdownItem> NPCTemplates
        {
            get
            {
                return _NPCTemplates;
            }
            set
            {
                _NPCTemplates = value;
                OnPropertyChanged("NPCTemplate");
            }
        }

        public BotTemplateDropdownItem currentBotTemplateSelection;
        private void ComboBox_DropDownClosed_NPCTemplates(object sender, EventArgs e)
        {
            BotTemplateDropdownItem item = (BotTemplateDropdownItem)npctemplatecombo.SelectedItem;
            this.currentBotTemplateSelection = item;
            if (item == null) { return; }
            this.BotTemplate.Clear();
            RobotTemplate robotTemp = NPCBotTemplates.GetById(item.id);
            robotTemp.robotName = Entities.GetEntityByID(robotTemp.robotID).Name;
            robotTemp.headName = Entities.GetEntityByID(robotTemp.headID).Name;
            robotTemp.chassisName = Entities.GetEntityByID(robotTemp.chassisID).Name;
            robotTemp.legName = Entities.GetEntityByID(robotTemp.legID).Name;
            foreach (ModuleTemplate mod in robotTemp.headModules)
            {
                mod.definitionName = Entities.GetEntityByID(mod.definition).Name;
                if (mod.ammoDefinition > 0)
                {
                    mod.ammoDefinitionName = Entities.GetEntityByID(mod.ammoDefinition).Name;
                }
            }
            foreach (ModuleTemplate mod in robotTemp.chassisModules)
            {
                mod.definitionName = Entities.GetEntityByID(mod.definition).Name;
                if (mod.ammoDefinition > 0)
                {
                    mod.ammoDefinitionName = Entities.GetEntityByID(mod.ammoDefinition).Name;
                }
            }
            foreach (ModuleTemplate mod in robotTemp.legModules)
            {
                mod.definitionName = Entities.GetEntityByID(mod.definition).Name;
                if (mod.ammoDefinition > 0)
                {
                    mod.ammoDefinitionName = Entities.GetEntityByID(mod.ammoDefinition).Name;
                }
            }
            foreach (ItemTemplate iTemp in robotTemp.items)
            {
                iTemp.definitionName = Entities.GetEntityByID(iTemp.definition).Name;
            }

            this.BotTemplate.Add(robotTemp);
        }

        private void NPCTemplate_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                foreach (RobotTemplate temp in this.BotTemplate)
                {
                    sb.AppendLine(NPCBotTemplates.SaveBotTemplate(temp));
                }
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + this.currentBotTemplateSelection.name + ".sql", sb.ToString());
                MessageBox.Show("Saved!", "Info", 0, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }

        }

        public EntityItems selectedModule;
        private void ComboBox_DropDownClosed_ModuleDef(object sender, EventArgs e)
        {
            EntityItems item = (EntityItems)moduledropdown.SelectedItem;
            this.selectedModule = item;
        }

        public EntityItems selectedAmmo;
        private void ComboBox_DropDownClosed_AmmoDef(object sender, EventArgs e)
        {
            EntityItems item = (EntityItems)ammodropdown.SelectedItem;
            this.selectedAmmo = item;
        }

        private void Add_To_Head_Click(object sender, RoutedEventArgs e)
        {
            if (BotTemplate.Count == 1)
            {
                RobotTemplate temp = BotTemplate[0];
                ModuleTemplate mod;
                if (selectedAmmo == null)
                {
                    mod = new ModuleTemplate(selectedModule.Definition, temp.chassisModules.Count + 1, 0, 0, selectedModule.Name);
                }
                else
                {
                    mod = new ModuleTemplate(selectedModule.Definition, temp.chassisModules.Count + 1, selectedAmmo.Definition, 10, selectedModule.Name, selectedAmmo.Name);
                }
                temp.headModules.Add(mod);
            }
        }

        private void Add_To_Chassis_Click(object sender, RoutedEventArgs e)
        {
            if (BotTemplate.Count == 1)
            {
                RobotTemplate temp = BotTemplate[0];
                ModuleTemplate mod;
                if (selectedAmmo == null)
                {
                    mod = new ModuleTemplate(selectedModule.Definition, temp.chassisModules.Count + 1, 0, 0, selectedModule.Name);
                }
                else
                {
                    mod = new ModuleTemplate(selectedModule.Definition, temp.chassisModules.Count + 1, selectedAmmo.Definition, 10, selectedModule.Name, selectedAmmo.Name);
                }
                temp.chassisModules.Add(mod);
            }
        }

        private void Add_To_Leg_Click(object sender, RoutedEventArgs e)
        {
            if (BotTemplate.Count == 1)
            {
                RobotTemplate temp = BotTemplate[0];
                ModuleTemplate mod;
                if (selectedAmmo == null)
                {
                    mod = new ModuleTemplate(selectedModule.Definition, temp.legModules.Count + 1, 0, 0, selectedModule.Name);
                }
                else
                {
                    mod = new ModuleTemplate(selectedModule.Definition, temp.legModules.Count + 1, selectedAmmo.Definition, 10, selectedModule.Name, selectedAmmo.Name);
                }
                temp.legModules.Add(mod);
            }
        }


        private void Remove_To_Head_Click(object sender, RoutedEventArgs e)
        {
            if (BotTemplate.Count == 1)
            {
                RobotTemplate temp = BotTemplate[0];
                if (temp.headModules.Count > 0)
                {
                    temp.headModules.RemoveAt(temp.headModules.Count - 1);
                }
            }
        }

        private void Remove_To_Chassis_Click(object sender, RoutedEventArgs e)
        {
            if (BotTemplate.Count == 1)
            {
                RobotTemplate temp = BotTemplate[0];
                if (temp.chassisModules.Count > 0)
                {
                    temp.chassisModules.RemoveAt(temp.chassisModules.Count - 1);
                }
            }
        }

        private void Remove_To_Leg_Click(object sender, RoutedEventArgs e)
        {
            if (BotTemplate.Count == 1)
            {
                RobotTemplate temp = BotTemplate[0];
                if (temp.legModules.Count > 0)
                {
                    temp.legModules.RemoveAt(temp.legModules.Count - 1);
                }
            }
        }

        #endregion

        #region NPCLoot
        public NPCLoot Loot { get; set; }

        private ObservableCollection<LootItem> _lootdata;
        public ObservableCollection<LootItem> loots
        {
            get
            {
                return _lootdata;
            }
            set
            {
                _lootdata = value;
                OnPropertyChanged("loots");
            }
        }

        private List<EntityItems> _itemdata;
        public List<EntityItems> LootableBots
        {
            get
            {
                return _itemdata;
            }
            set
            {
                _itemdata = value;
                OnPropertyChanged("LootableBots");
            }
        }

        private List<EntityItems> _lootables;
        public List<EntityItems> LootableEntityDefaults
        {
            get
            {
                return _lootables;
            }
            set
            {
                _lootables = value;
                OnPropertyChanged("LootableEntityDefaults");
            }
        }

        public EntityItems currentNPCLootableBot;
        private void ComboBox_DropDownClosed_LootableNPCs(object sender, EventArgs e)
        {
            EntityItems item = (EntityItems)npclootcombo.SelectedItem;
            this.currentNPCLootableBot = item;
            if (item == null) { return; }
            this.loots = Loot.GetLootByDefinition(item.Definition);
        }

        public EntityItems currentRowAddItem;
        private void ComboBox_DropDownClosed_NPCLootableDefs(object sender, EventArgs e)
        {
            EntityItems item = (EntityItems)npcloot.SelectedItem;
            this.currentRowAddItem = item;
        }

        private void NPC_Loot_Save_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (LootItem item in this.loots)
                {
                    if (item.recordAction == DBAction.UPDATE)
                    {
                        Loot.updateSelf(item);
                        sb.Append(Loot.Save());
                        sb.AppendLine();
                    }
                    else if (item.recordAction == DBAction.INSERT)
                    {
                        Loot.updateSelf(item);
                        sb.Append(Loot.Insert());
                        sb.AppendLine();
                    }
                    else if (item.recordAction == DBAction.DELETE)
                    {
                        //TODO
                    }
                }
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + currentNPCLootableBot.Name + ".sql", sb.ToString());
                MessageBox.Show("Saved!", "Info", 0, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }

        }

        private void NPC_Loot_Add_Row_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LootItem loot = Loot.CreateNewLootForBot(this.currentNPCLootableBot, this.currentRowAddItem);
                loot.recordAction = DBAction.INSERT;
                this.loots.Add(loot);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }

        }

        #endregion

        #region BotBonuses

        private ChassisBonus BotBonus { get; set; }

        private ObservableCollection<BotBonusObj> _botbonuslist;
        public ObservableCollection<BotBonusObj> BotBonusList
        {
            get
            {
                return _botbonuslist;
            }
            set
            {
                _botbonuslist = value;
                OnPropertyChanged("BotBonusList");
            }
        }

        public EntityItems currentBotComponentSelection;
        private void Bot_ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            EntityItems item = (EntityItems)bot_combo_dropdown.SelectedItem;
            this.currentBotComponentSelection = item;
            if (item == null) { return; }
            this.BotBonusList = BotBonus.getByEntity(item.Definition);
        }

        private void Bot_Bonus_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (BotBonusObj bonus in this.BotBonusList)
                {
                    sb.AppendLine(this.BotBonus.Save(bonus));
                }
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + currentBotComponentSelection.Name + ".sql", sb.ToString());
                MessageBox.Show("Saved!", "Info", 0, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }

        }

        #endregion

        #region NPCTemplateRelation
        //TODO hack, using observable collection for one item BAD
        private ObservableCollection<BotTemplateRelation> _selectedTempRelation;
        public ObservableCollection<BotTemplateRelation> NPCTemplateRelationList
        {
            get
            {
                return _selectedTempRelation;
            }
            set
            {
                _selectedTempRelation = value;
                OnPropertyChanged("NPCTemplateRelationList");
            }
        }

        public EntityItems selectedNPCTemplateRelation;
        private void ComboBox_DropDownClosed_NPCTemplateRelations(object sender, EventArgs e)
        {
            EntityItems item = (EntityItems)npctemplaterelation.SelectedItem;
            this.selectedNPCTemplateRelation = item;
            if (item == null) { return; }
            this.NPCTemplateRelationList.Clear();
            NPCTemplateRelationList.Add(this.NPCTemplateRelations.GetById(item.Definition));
        }

        public BotTemplateDropdownItem currentBotTemplateSelection_forRelation;
        private void ComboBox_DropDownClosed_NPCTemplateRelationEdit(object sender, EventArgs e)
        {
            BotTemplateDropdownItem item = (BotTemplateDropdownItem)npctemplaterelation_change.SelectedItem;
            this.currentBotTemplateSelection_forRelation = item;
            if (item == null) { return; }
        }

        private void ChangeTemplateClick(object sender, RoutedEventArgs e)
        {
            if (NPCTemplateRelationList.Count == 1)
            {
                BotTemplateRelation temp = NPCTemplateRelationList[0];
                temp.templateid = currentBotTemplateSelection_forRelation.id;
                temp.templatename = currentBotTemplateSelection_forRelation.name;
                NPCTemplateRelationList.Clear();
                NPCTemplateRelationList.Add(temp);
            }
        }

        private void NPCTemplateRelation_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (BotTemplateRelation relation in this.NPCTemplateRelationList)
                {
                    sb.AppendLine(NPCTemplateRelations.Save(relation));
                }

                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + selectedNPCTemplateRelation.Name + "_template_relation.sql", sb.ToString());
                MessageBox.Show("Saved!", "Info", 0, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }

        }


        private void NPCTemplateRelation_Save_New_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //TODO Insert on relation table.. requires unique def or templ?
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }

        }
        #endregion

        #region NPCGroup


        //TODO hack, using observable collection for one item BAD
        private ObservableCollection<NPCPresenceData> _selectedNPCPresence;
        public ObservableCollection<NPCPresenceData> SelectedNPCPresence
        {
            get
            {
                return _selectedNPCPresence;
            }
            set
            {
                _selectedNPCPresence = value;
                OnPropertyChanged("SelectedNPCPresence");
            }
        }

        //TODO hack, using observable collection for one item BAD
        private ObservableCollection<NPCFlockData> _flockList;
        public ObservableCollection<NPCFlockData> NPCFlockList
        {
            get
            {
                return _flockList;
            }
            set
            {
                _flockList = value;
                OnPropertyChanged("NPCFlockList");
            }
        }


        public NPCPresenceData selectedPresence;
        private void ComboBox_DropDownClosed_NPCPresence(object sender, EventArgs e)
        {
            NPCPresenceData item = (NPCPresenceData)npcgroupcombo.SelectedItem;
            this.selectedPresence = item;
            if (item == null) { return; }
            this.NPCFlockList.Clear();
            List<NPCFlockData> flocks = NPCFlockTable.getByPresenceID(item.id);
            foreach (NPCFlockData flock in flocks)
            {
                this.NPCFlockList.Add(flock);
            }
            SelectedNPCPresence.Clear();
            SelectedNPCPresence.Add(item);
        }

        public EntityItems selectedNPC;
        private void ComboBox_DropDownClosed_NPCDef_forFlock(object sender, EventArgs e)
        {
            EntityItems item = (EntityItems)editflockNPCdef.SelectedItem;
            this.selectedNPC = item;
            if (item == null) { return; }
        }

        private void NPCFlock_Change_NPC(object sender, RoutedEventArgs e)
        {
            NPCFlockData flock = (NPCFlockData)flockgrid.SelectedItem;

            if (selectedNPC == null || flock == null)
            {
                MessageBox.Show("Select NPC from dropdown, click on the flock you want to change, then press button.", "Warn", 0, MessageBoxImage.Error);
                return;
            }
            int index = this.NPCFlockList.IndexOf(flock);
            if (index == -1)
            {
                MessageBox.Show("Invalid Flock Selection?!", "Error", 0, MessageBoxImage.Error);
                return;
            }
            flock.definition = this.selectedNPC.Definition;
            flock.NPCDefinitionName = this.selectedNPC.Name;
            this.NPCFlockList[index] = flock;


        }

        private void NPCPresence_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (NPCFlockData flock in this.NPCFlockList)
                {
                    sb.AppendLine(NPCFlockTable.Save(flock));
                }
                foreach (NPCPresenceData pres in this.SelectedNPCPresence)
                {
                    sb.AppendLine(NPCPresenceTable.Save(pres));
                }
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + selectedPresence.name + "_NPCPresence_flocks.sql", sb.ToString());
                MessageBox.Show("Saved!", "Info", 0, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }
        }
        #endregion

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Dialogs.CreateRobotTemplate dlg = new Dialogs.CreateRobotTemplate();
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                // FIXME: load the record here for the user to edit it.
                dlg.Close();
            }

        }
    }
}
