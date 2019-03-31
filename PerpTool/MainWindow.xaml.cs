﻿using System;
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
using System.Globalization;
using System.Threading;

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
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
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
            ExtensionsTable = new Extensions(Connstr);
            ArtifactLoot = new ArtifactLoot(Connstr);
            ArtifactType = new ArtifactTypesTable(Connstr);
            ArtifactSpawnInfoTable = new ArtifactSpawnInfo(Connstr);

            EntityItems = Entities.GetEntitiesWithFields();
            ZoneList = ZoneTbl.GetAllZones();
            SpawnList = Spawn.GetAllSpawns();
            LootableBots = Entities.GetEntitiesByCategory(CategoryFlags.cf_npc);
            LootableEntityDefaults = Entities.GetPurchasableEntities();
            AllRobotComponents = Entities.GetEntitiesByCategory(CategoryFlags.cf_robot_components);
            NPCTemplates = NPCBotTemplates.getAll();
            AllNPCPresences = NPCPresenceTable.getAll();
            AllNPCFlocks = NPCFlockTable.GetAllFlocks();
            AllExtensions = ExtensionsTable.GetAll();
            ArtifactTypes = ArtifactType.GetAll();


            this.CatFlags = this.GetAllCategoryFlags();
            this.AllAggregateFields = AgFields.GetAllFields();
            this.AmmoList = Entities.GetEntitiesByCategory(CategoryFlags.cf_ammo);
            this.ModuleList = Entities.GetEntitiesByCategory(CategoryFlags.cf_robot_equipment);
            this.NPCEntities = Entities.GetEntitiesByCategory(CategoryFlags.cf_npc);
            this.SelectedNPCPresence = new NPCPresenceData();
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
        private Extensions ExtensionsTable { get; set; }
        private ArtifactSpawnInfo ArtifactSpawnInfoTable { get; set; }

        public List<EntityDefaults> AmmoList { get; set; }
        public List<EntityDefaults> ModuleList { get; set; }
        public List<EntityDefaults> AllRobotComponents { get; set; }
        public List<EntityDefaults> NPCEntities { get; set; }
        public List<NPCPresenceData> AllNPCPresences { get; set; }
        public List<AggregateFields> AllAggregateFields { get; set; }
        public List<NPCFlockData> AllNPCFlocks { get; set; }
        public List<Extensions> AllExtensions { get; set; }



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

        private FieldValuesStuff _toRemoveFieldValueStuff;
        public FieldValuesStuff SelectedFieldValuesThing
        {
            get
            {
                return this._toRemoveFieldValueStuff;
            }
            set
            {
                this._toRemoveFieldValueStuff = value;
                OnPropertyChanged("SelectedFieldValuesThing");
            }
        }

        private void ComboBox_DropDownClosed_CatFlag(object sender, EventArgs e)
        {
            //Still needs to perform query to grab entities by catflag at selection-confirm
            //Could be pre-populated... its a lot though
            this.EntitiesList = Entities.GetEntitiesByCategory(SelectedCategoryFlag);
        }

        private void ComboBox_DropDownClosed_EntitySelect(object sender, EventArgs e)
        {
            EntityDefaults entity = (EntityDefaults)combo.SelectedItem;
            if (entity == null || entity != this.SelectedEntity) { return; }
            this.FieldValuesList = new ObservableCollection<FieldValuesStuff>();
            this.FieldValuesList = AgValues.GetValuesForEntity(this.SelectedEntity.definition);
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
        public List<FieldValuesStuff> fieldValuesToDelete = new List<FieldValuesStuff>();
        private void AggFieldRemove_Click(object sender, EventArgs e)
        {
            if (this.FieldValuesList.Count > 0)
            {
                int index = this.FieldValuesList.Count - 1;
                FieldValuesStuff data = this.FieldValuesList.Last<FieldValuesStuff>();
                if (this.SelectedFieldValuesThing != null)
                {
                    index = this.FieldValuesList.IndexOf(SelectedFieldValuesThing);
                    if (index >= 0)
                    {
                        data = this.FieldValuesList[index];
                    }
                }
                if (data.dBAction != DBAction.INSERT)
                {
                    data.dBAction = DBAction.DELETE;
                    this.fieldValuesToDelete.Add(data);
                }
                this.FieldValuesList.RemoveAt(index);
            }
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
                    sb.AppendLine(entity.SaveNewRecord());
                    sb.AppendLine();
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + entity.definitionname + "INSERT" + Utilities.timestamp() + ".sql", sb.ToString());
                    MessageBox.Show("Saved NEW EntityDefault Record!", "Info", 0, MessageBoxImage.Information);
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
                    sb.AppendLine(EntityDefaults.GetDeclStatement());
                    sb.AppendLine(entity.GetLookupStatement());
                    sb.AppendLine();
                    sb.AppendLine(entity.Save());
                    sb.AppendLine();
                    sb.AppendLine(handleAggregateFieldValuesSave(FieldValuesList));
                    sb.AppendLine();
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + entity.definitionname + Utilities.timestamp() + ".sql", sb.ToString());
                    MessageBox.Show("Saved Entity and FieldValues!", "Info", 0, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }
        }


        private string handleAggregateFieldValuesSave(ObservableCollection<FieldValuesStuff> list)
        {
            //TODO sql parameterization for outputs needed
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FieldValuesStuff.GetDeclStatement());
            sb.AppendLine(AggregateFields.GetDeclStatement());
            foreach (FieldValuesStuff item in list)
            {
                AgFields.GetById(item.FieldId);
                AgValues.GetById(item.ValueId);
                StringBuilder sb2 = new StringBuilder();
                sb2.AppendLine(AgFields.GetLookupStatement());
                sb2.AppendLine(item.GetLookupStatement());
                sb2.AppendLine();
                bool changed = item.FieldValue != AgValues.value || item.FieldFormula != AgFields.formula;

                if (item.dBAction == DBAction.UPDATE)
                {
                    if (changed)
                    {
                        sb.AppendLine(sb2.ToString());
                    }
                    if (item.FieldValue != AgValues.value)
                    {
                        AgValues.value = item.FieldValue;
                        sb.AppendLine(AgValues.Save(item));
                        sb.AppendLine();
                    }
                    if (item.FieldFormula != AgFields.formula || item.FieldMultiplier != AgFields.measurementmultiplier || item.FieldOffset != AgFields.measurementoffset)
                    {
                        sb.AppendLine(sb2.ToString());
                        sb.AppendLine(AgFields.Save(item));
                        sb.AppendLine();
                    }
                }
                else if (item.dBAction == DBAction.INSERT)
                {
                    sb.AppendLine(sb2.ToString());
                    sb.AppendLine(AgValues.Insert(item));
                    item.dBAction = DBAction.UPDATE;
                    if (item.FieldFormula != AgFields.formula)
                    {
                        sb.AppendLine(AgFields.Save(item));
                    }   //New AggValues use old AggFields -- this remains an update iff changed
                }
            }
            foreach (FieldValuesStuff item in fieldValuesToDelete)
            {
                if (item.dBAction == DBAction.DELETE)
                {
                    AgFields.GetById(item.FieldId);
                    AgValues.GetById(item.ValueId);
                    sb.AppendLine(AgFields.GetLookupStatement());
                    sb.AppendLine(item.GetLookupStatement());
                    sb.AppendLine();
                    sb.AppendLine(AgValues.Delete(item));
                    sb.AppendLine();
                }
            }
            this.fieldValuesToDelete.Clear();
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

        private bool checkNullForSlots()
        {
            return this.SelectedEntity == null || this.SelectedEntity.options == null || this.SelectedEntity.options.Slots == null;
        }

        private void AddSlot_Click(object sender, EventArgs e)
        {
            if (!checkNullForSlots())
            {
                this.SelectedEntity.options.Slots.Add(new SlotFlagWrapper(this.SelectedEntity.options.Slots.Count, SlotFlags.chassis));
            }
            else
            {
                MessageBox.Show("Hey Jerk, you can't add a slot to that!", "Wrong Category", 0, MessageBoxImage.Warning);
            }
        }

        private void RemoveSlot_Click(object sender, EventArgs e)
        {
            if (!checkNullForSlots() && this.SelectedEntity.options.Slots.Count > 0)
            {
                this.SelectedEntity.options.Slots.RemoveAt(this.SelectedEntity.options.Slots.Count - 1);
            }
            else
            {
                MessageBox.Show("Hey Jerk, you can't remove a slot from that!", "Wrong Category or No more slots!", 0, MessageBoxImage.Warning);
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

        private BotTemplateDropdownItem _currentBotTemplateSelection;
        public BotTemplateDropdownItem currentBotTemplateSelection
        {
            get
            {
                return this._currentBotTemplateSelection;
            }
            set
            {
                this._currentBotTemplateSelection = value;
                OnPropertyChanged("currentBotTemplateSelection");
            }
        }
        private void ComboBox_DropDownClosed_NPCTemplates(object sender, EventArgs e)
        {
            BotTemplateDropdownItem item = (BotTemplateDropdownItem)npctemplatecombo.SelectedItem;
            this.currentBotTemplateSelection = item;
            if (item == null) { return; }
            this.BotTemplate.Clear();
            RobotTemplate robotTemp = NPCBotTemplates.GetById(item.id);
            var bot = Entities.GetEntityByID(robotTemp.robotID);
            var head = Entities.GetEntityByID(robotTemp.headID);
            var chassis = Entities.GetEntityByID(robotTemp.chassisID);
            var leg = Entities.GetEntityByID(robotTemp.legID);
            var cargo = Entities.GetEntityByID(robotTemp.containerID);
            robotTemp.robotName = bot.Name;
            robotTemp.robotID = bot.Definition;
            robotTemp.headName = head.Name;
            robotTemp.headID = head.Definition;
            robotTemp.chassisName = chassis.Name;
            robotTemp.chassisID = chassis.Definition;
            robotTemp.legName = leg.Name;
            robotTemp.legID = leg.Definition;
            robotTemp.containerName = cargo.Name;
            robotTemp.containerID = cargo.Definition;
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
                sb.AppendLine(RobotTemplatesTable.GetDeclStatement());
                foreach (RobotTemplate temp in this.BotTemplate)
                {
                    sb.AppendLine(RobotTemplatesTable.GetLookupStatement(temp));
                    sb.AppendLine(NPCBotTemplates.SaveBotTemplate(temp));
                    sb.AppendLine();
                }
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + this.currentBotTemplateSelection.name + Utilities.timestamp() + ".sql", sb.ToString());
                MessageBox.Show("Saved!", "Info", 0, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }

        }

        private EntityDefaults _selectedModule;
        public EntityDefaults SelectedModule
        {
            get
            {
                return this._selectedModule;
            }
            set
            {
                this._selectedModule = value;
                OnPropertyChanged("SelectedModule");
            }
        }

        private EntityDefaults _selectedAmmo;
        public EntityDefaults SelectedAmmo
        {
            get
            {
                return this._selectedAmmo;
            }
            set
            {
                this._selectedAmmo = value;
                OnPropertyChanged("SelectedAmmo");
            }
        }

        private void Add_To_Head_Click(object sender, RoutedEventArgs e)
        {
            if (BotTemplate.Count == 1)
            {
                RobotTemplate temp = BotTemplate[0];
                ModuleTemplate mod;
                if (SelectedAmmo == null)
                {
                    mod = new ModuleTemplate(SelectedModule.definition, temp.chassisModules.Count + 1, 0, 0, SelectedModule.definitionname);
                }
                else
                {
                    mod = new ModuleTemplate(SelectedModule.definition, temp.chassisModules.Count + 1, SelectedAmmo.definition, 10, SelectedModule.definitionname, SelectedAmmo.definitionname);
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
                if (SelectedAmmo == null)
                {
                    mod = new ModuleTemplate(SelectedModule.definition, temp.chassisModules.Count + 1, 0, 0, SelectedModule.definitionname);
                }
                else
                {
                    mod = new ModuleTemplate(SelectedModule.definition, temp.chassisModules.Count + 1, SelectedAmmo.definition, 10, SelectedModule.definitionname, SelectedAmmo.definitionname);
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
                if (SelectedAmmo == null)
                {
                    mod = new ModuleTemplate(SelectedModule.definition, temp.legModules.Count + 1, 0, 0, SelectedModule.definitionname);
                }
                else
                {
                    mod = new ModuleTemplate(SelectedModule.definition, temp.legModules.Count + 1, SelectedAmmo.definition, 10, SelectedModule.definitionname, SelectedAmmo.definitionname);
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

        private List<EntityDefaults> _itemdata;
        public List<EntityDefaults> LootableBots
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

        private List<EntityDefaults> _lootables;
        public List<EntityDefaults> LootableEntityDefaults
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

        private EntityDefaults _selectedLootableBot;
        public EntityDefaults SelectedLootableBot
        {
            get
            {
                return this._selectedLootableBot;
            }
            set
            {
                this._selectedLootableBot = value;
                OnPropertyChanged("SelectedLootableBot");
            }
        }

        private void ComboBox_DropDownClosed_LootableNPCs(object sender, EventArgs e)
        {
            if (SelectedLootableBot == null) { return; }
            this.loots = Loot.GetLootByDefinition(SelectedLootableBot.definition);
            this.removeLoot.Clear();
        }

        private EntityDefaults _currentRowAddItem;
        public EntityDefaults currentRowAddItem
        {
            get
            {
                return this._currentRowAddItem;
            }
            set
            {
                this._currentRowAddItem = value;
                OnPropertyChanged("currentRowAddItem");
            }
        }
        private void ComboBox_DropDownClosed_NPCLootableDefs(object sender, EventArgs e)
        {
            System.Console.WriteLine(currentRowAddItem);
        }

        private void NPC_Loot_Save_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(EntityDefaults.GetDeclStatement());
                sb.AppendLine(SelectedLootableBot.GetLookupStatement());
                sb.AppendLine(LootItem.GetLootDeclStatment());
                sb.AppendLine(LootItem.GetDeclStatement());
                foreach (LootItem item in this.loots)
                {
                    if (item.dBAction == DBAction.UPDATE)
                    {
                        Loot.updateSelf(item);
                        sb.AppendLine(item.GetLootDefinitionLookupStatement());
                        sb.AppendLine(item.GetLookupStatement());
                        sb.AppendLine(Loot.Save());
                        sb.AppendLine();
                    }
                    else if (item.dBAction == DBAction.INSERT)
                    {
                        Loot.updateSelf(item);
                        sb.AppendLine(item.GetLootDefinitionLookupStatement());
                        sb.AppendLine(Loot.Insert());
                        sb.AppendLine();
                    }
                }

                foreach (LootItem item in removeLoot)
                {
                    if (item.dBAction == DBAction.DELETE)
                    {
                        sb.AppendLine(item.GetLootDefinitionLookupStatement());
                        sb.AppendLine(item.GetLookupStatement());
                        Loot.updateSelf(item);
                        sb.AppendLine(Loot.Delete());
                        sb.AppendLine();
                    }
                }
                this.removeLoot.Clear();
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + SelectedLootableBot.definitionname + "_loot" + Utilities.timestamp() + ".sql", sb.ToString());
                MessageBox.Show("Saved!", "Info", 0, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }

        }

        public List<LootItem> copyLoots = new List<LootItem>();
        private void Copy_Loots(object sender, EventArgs e)
        {
            copyLoots = new List<LootItem>(this.loots);
        }

        private void Paste_Loots(object sender, EventArgs e)
        {
            foreach (LootItem item in this.copyLoots)
            {
                item.dBAction = DBAction.INSERT;
                this.loots.Add(item);
            }
        }

        private LootItem _privateSelectedLoot;
        public LootItem SelectedLoot
        {
            get
            {
                return _privateSelectedLoot;
            }
            set
            {
                _privateSelectedLoot = value;
                OnPropertyChanged("SelectedLoot");
            }
        }

        public List<LootItem> removeLoot = new List<LootItem>();
        private void NPC_Loot_Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.loots.Count > 0)
            {
                LootItem lootToRemove = this.loots.Last<LootItem>();
                if (this.SelectedLoot != null)
                {
                    lootToRemove = SelectedLoot;
                }
                if (lootToRemove.dBAction != DBAction.INSERT)
                {
                    lootToRemove.dBAction = DBAction.DELETE;
                    removeLoot.Add(lootToRemove);
                }
                this.loots.Remove(lootToRemove);
            }
        }

        private void NPC_Loot_Add_Row_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LootItem loot = Loot.CreateNewLootForBot(this.SelectedLootableBot, this.currentRowAddItem);
                loot.dBAction = DBAction.INSERT;
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


        private EntityDefaults _selectedBot;
        public EntityDefaults SelectedBot
        {
            get
            {
                return this._selectedBot;
            }
            set
            {
                this._selectedBot = value;
                OnPropertyChanged("SelectedBot");
            }
        }

        private Extensions _SelectedExtension;
        public Extensions SelectedExtension
        {
            get
            {
                return this._SelectedExtension;
            }
            set
            {
                this._SelectedExtension = value;
                OnPropertyChanged("SelectedExtension");
            }
        }

        private AggregateFields _SelectedAggregateField;
        public AggregateFields SelectedAggregateField
        {
            get
            {
                return this._SelectedAggregateField;

            }
            set
            {
                this._SelectedAggregateField = value;
                OnPropertyChanged("SelectedAggregateField");
            }
        }

        private BotBonusObj _selectedBonus;
        public BotBonusObj SelectedBonus
        {
            get
            {
                return this._selectedBonus;
            }
            set
            {
                this._selectedBonus = value;
                OnPropertyChanged("SelectedBonus");
            }
        }

        private void Bot_ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (SelectedBot == null) { return; }
            this.BotBonusList = BotBonus.getByEntity(SelectedBot.definition);
            this.removeBonuses.Clear();
        }

        private void Bot_Bonus_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(EntityDefaults.GetDeclStatement());
                sb.AppendLine(Extensions.GetDeclStatement());
                sb.AppendLine(AggregateFields.GetDeclStatement());
                sb.AppendLine(BotBonusObj.GetDeclStatement());
                sb.AppendLine();
                foreach (BotBonusObj bonus in this.BotBonusList)
                {
                    if (bonus.dBAction == DBAction.UPDATE)
                    {
                        sb.AppendLine(bonus.GetBonusStatement());
                        sb.AppendLine(this.BotBonus.Save(bonus));
                        sb.AppendLine();
                    }
                    else if (bonus.dBAction == DBAction.INSERT)
                    {
                        sb.AppendLine(bonus.GetBonusStatement());
                        sb.AppendLine(this.BotBonus.Insert(bonus));
                        sb.AppendLine();
                    }
                }

                foreach (BotBonusObj bonus in this.removeBonuses)
                {
                    if (bonus.dBAction == DBAction.DELETE)
                    {
                        sb.AppendLine(bonus.GetBonusStatement());
                        sb.AppendLine(this.BotBonus.Delete(bonus));
                        sb.AppendLine();
                    }
                }
                this.removeBonuses.Clear();
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + SelectedBot.definitionname + "_bonuses" + Utilities.timestamp() + ".sql", sb.ToString());
                MessageBox.Show("Saved!", "Info", 0, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }
            this.removeBonuses.Clear();
        }

        public List<BotBonusObj> bonusCopy = new List<BotBonusObj>();
        private void Copy_Bonuses(object sender, EventArgs e)
        {
            bonusCopy = new List<BotBonusObj>(this.BotBonusList);
        }

        private void Paste_Bonuses(object sender, EventArgs e)
        {
            foreach (BotBonusObj item in this.bonusCopy)
            {
                item.dBAction = DBAction.INSERT;
                this.BotBonusList.Add(item);
            }
        }

        public List<BotBonusObj> removeBonuses = new List<BotBonusObj>();
        private void Bot_Bonus_Remove_Click(object sender, RoutedEventArgs e)
        {
            if (this.BotBonusList.Count > 0)
            {
                int index = this.BotBonusList.Count - 1;
                BotBonusObj bonus = this.BotBonusList.Last<BotBonusObj>();
                if (this.SelectedBonus != null)
                {
                    index = this.BotBonusList.IndexOf(SelectedBonus);
                    if (index > -1)
                    {
                        bonus = this.BotBonusList[index];
                    }
                }
                if (bonus.dBAction != DBAction.INSERT)
                {
                    bonus.dBAction = DBAction.DELETE;
                    removeBonuses.Add(bonus);

                }
                this.BotBonusList.RemoveAt(index);
            }
        }

        private void Bot_Bonus_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BotBonusObj bonus = new BotBonusObj();
                bonus.aggFieldName = this.SelectedAggregateField.name;
                bonus.bonus = 0;
                bonus.dBAction = DBAction.INSERT;
                bonus.definition = this.SelectedBot.definition;
                bonus.definitionName = this.SelectedBot.definitionname;
                bonus.effectenhancer = 0;
                bonus.extension = this.SelectedExtension.extensionid;
                bonus.extensionName = this.SelectedExtension.extensionname;
                bonus.id = 0;
                bonus.targetpropertyID = this.SelectedAggregateField.id;
                this.BotBonusList.Add(bonus);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Make sure you Select a Skill and a Field from the drop-downs!!!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }
        }

        #endregion

        #region NPCTemplateRelation
        private EntityDefaults _selectedBotForRelation;
        public EntityDefaults SelectedBotForRelation
        {
            get
            {
                return this._selectedBotForRelation;
            }
            set
            {
                _selectedBotForRelation = value;
                OnPropertyChanged("SelectedBotForRelation");
            }
        }


        private BotTemplateRelation _selectedTempRelation;
        public BotTemplateRelation NPCTemplateRelation
        {
            get
            {
                return _selectedTempRelation;
            }
            set
            {
                _selectedTempRelation = value;
                OnPropertyChanged("NPCTemplateRelation");
            }
        }

        private void ComboBox_DropDownClosed_NPCTemplateRelations(object sender, EventArgs e)
        {
            if (SelectedBotForRelation == null) { return; }
            NPCTemplateRelation = this.NPCTemplateRelations.GetById(SelectedBotForRelation.definition);
            NPCTemplateRelation.dBAction = DBAction.UPDATE;
            if (NPCTemplateRelation.isEmpty())
            {
                NPCTemplateRelation.definition = SelectedBotForRelation.definition;
                NPCTemplateRelation.definitionname = SelectedBotForRelation.definitionname;
                NPCTemplateRelation.note = "NEW TEMPLATE EDIT ME AND SAVE AS NEW!";
                NPCTemplateRelation.dBAction = DBAction.INSERT;
            }
        }
        private BotTemplateDropdownItem _botTemplateForRelation;
        public BotTemplateDropdownItem SelectedNPCTemplateForRelation
        {
            get
            {
                return this._botTemplateForRelation;
            }
            set
            {
                this._botTemplateForRelation = value;
                OnPropertyChanged("SelectedNPCTemplateForRelation");
            }
        }

        private void ChangeTemplateClick(object sender, RoutedEventArgs e)
        {
            if (this.SelectedNPCTemplateForRelation != null)
            {
                NPCTemplateRelation.templateid = SelectedNPCTemplateForRelation.id;
                NPCTemplateRelation.templatename = SelectedNPCTemplateForRelation.name;
            }
        }

        private void NPCTemplateRelation_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string message = "Did NOT save =(";
                string appendStr = NPCTemplateRelation.dBAction.ToString();
                sb.AppendLine(RobotTemplatesTable.GetDeclStatement());
                sb.AppendLine(RobotTemplatesTable.GetLookupStatement(NPCTemplateRelation.templatename));
                sb.AppendLine(EntityDefaults.GetDeclStatement());
                sb.AppendLine(SelectedBotForRelation.GetLookupStatement());
                sb.AppendLine();
                if (NPCTemplateRelation.dBAction == DBAction.UPDATE)
                {
                    sb.AppendLine(NPCTemplateRelations.Save(NPCTemplateRelation));
                    sb.AppendLine();
                    message = "Saved Updated TemplateRelation!";
                }
                else if (NPCTemplateRelation.dBAction == DBAction.INSERT)
                {
                    sb.AppendLine(NPCTemplateRelations.Insert(NPCTemplateRelation));
                    sb.AppendLine();
                    message = "Saved New TemplateRelation!";
                }
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + NPCTemplateRelation.definitionname + "_template_relation_" + appendStr + Utilities.timestamp() + ".sql", sb.ToString());
                MessageBox.Show(message, "Info", 0, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }
        }

        private void NPCTemplateRelation_Insert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string message = "Did NOT save =(";
                string appendStr = NPCTemplateRelation.dBAction.ToString();
                sb.AppendLine(RobotTemplatesTable.GetDeclStatement());
                sb.AppendLine(RobotTemplatesTable.GetLookupStatement(NPCTemplateRelation.templatename));
                sb.AppendLine(EntityDefaults.GetDeclStatement());
                sb.AppendLine(SelectedBotForRelation.GetLookupStatement());
                sb.AppendLine();
                sb.AppendLine(NPCTemplateRelations.Insert(NPCTemplateRelation));
                sb.AppendLine();
                message = "Saved New TemplateRelation!";
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + NPCTemplateRelation.definitionname + "_template_relation_INSERT_" + appendStr + Utilities.timestamp() + ".sql", sb.ToString());
                MessageBox.Show(message, "Info", 0, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }
        }

        #endregion

        #region NPCGroup



        private NPCPresenceData _selectedNPCPresence;
        public NPCPresenceData SelectedNPCPresence
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

        private void ComboBox_DropDownClosed_NPCPresence(object sender, EventArgs e)
        {
            if (SelectedNPCPresence == null) { return; }
            this.NPCFlockList.Clear();
            List<NPCFlockData> flocks = NPCFlockTable.getByPresenceID(SelectedNPCPresence.id);
            foreach (NPCFlockData flock in flocks)
            {
                this.NPCFlockList.Add(flock);
            }
        }

        private EntityDefaults _selectedNPCForFlock;
        public EntityDefaults selectedNPC
        {
            get
            {
                return this._selectedNPCForFlock;
            }
            set
            {
                this._selectedNPCForFlock = value;
                OnPropertyChanged("selectedNPC");
            }
        }
        private NPCFlockData _editFlock;
        public NPCFlockData FlockToEdit
        {
            get
            {
                return this._editFlock;
            }
            set
            {
                this._editFlock = value;
                OnPropertyChanged("FlockToEdit");
            }
        }

        private NPCFlockData _selectedFlock;
        public NPCFlockData SelectedFlock
        {
            get
            {
                return this._selectedFlock;
            }
            set
            {
                this._selectedFlock = value;
                OnPropertyChanged("SelectedFlock");
            }
        }

        private void NPCFlock_Change_NPC(object sender, RoutedEventArgs e)
        {
            if (selectedNPC == null || FlockToEdit == null)
            {
                MessageBox.Show("Select NPC from dropdown, click on the flock you want to change, then press button.", "Warn", 0, MessageBoxImage.Error);
                return;
            }
            int index = this.NPCFlockList.IndexOf(FlockToEdit);
            if (index == -1)
            {
                MessageBox.Show("Invalid Flock Selection?!", "Error", 0, MessageBoxImage.Error);
                return;
            }
            FlockToEdit.definition = this.selectedNPC.definition;
            FlockToEdit.NPCDefinitionName = this.selectedNPC.definitionname;
            this.NPCFlockList[index] = FlockToEdit;
        }

        private void NPCPresence_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(NPCPresenceData.GetDeclStatement());
                sb.AppendLine(SelectedNPCPresence.GetLookupStatement());
                sb.AppendLine(NPCPresenceTable.Save(SelectedNPCPresence));
                sb.AppendLine();
                sb.AppendLine(flockSaver());
                sb.AppendLine();
                string filename = SelectedNPCPresence.name + "_NPCPresence_flocksUPDATE";
                if (this.removeFlocks.Count > 0)
                {
                    filename += "_DELETE_";
                }
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + filename + Utilities.timestamp() + ".sql", sb.ToString());
                MessageBox.Show("Saved!", "Info", 0, MessageBoxImage.Information);
                this.removeFlocks.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }
        }

        private void NPCPresence_Save_New(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(NPCPresenceTable.Insert(SelectedNPCPresence));
                sb.AppendLine();
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + SelectedNPCPresence.name + "_NPCPresence_flocksINSERT" + Utilities.timestamp() + ".sql", sb.ToString());
                MessageBox.Show("Saved!", "Info", 0, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }
        }

        private string flockSaver()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(NPCFlockData.GetDeclStatement());
            sb.AppendLine(EntityDefaults.GetDeclStatement());
            sb.AppendLine();
            foreach (NPCFlockData flock in this.NPCFlockList)
            {
                sb.AppendLine(EntityDefaults.GetLookupStatement(flock.NPCDefinitionName));
                sb.AppendLine(flock.GetLookupStatement());
                if (flock.dBAction == DBAction.UPDATE)
                {
                    sb.AppendLine(NPCFlockTable.Save(flock));
                    sb.AppendLine();
                }
                else if (flock.dBAction == DBAction.INSERT)
                {
                    sb.AppendLine(NPCFlockTable.Insert(flock));
                    sb.AppendLine();
                    flock.dBAction = DBAction.UPDATE;
                }
            }
            foreach (NPCFlockData flock in this.removeFlocks)
            {
                if (flock.dBAction == DBAction.DELETE)
                {
                    sb.AppendLine(flock.GetLookupStatement());
                    sb.AppendLine(EntityDefaults.GetLookupStatement(flock.NPCDefinitionName));
                    sb.AppendLine(NPCFlockTable.Delete(flock));
                    sb.AppendLine();
                }
            }
            this.removeFlocks.Clear();
            return sb.ToString();
        }

        public List<NPCFlockData> removeFlocks = new List<NPCFlockData>();
        private void NPCPresence_RemoveFlock(object sender, RoutedEventArgs e)
        {
            if (this.NPCFlockList.Count > 0)
            {
                NPCFlockData flock = this.NPCFlockList.Last<NPCFlockData>();
                if (flock.dBAction == DBAction.UPDATE)
                {
                    flock.dBAction = DBAction.DELETE;
                    removeFlocks.Add(flock);
                }
                this.NPCFlockList.RemoveAt(this.NPCFlockList.Count - 1);
            }
        }


        private void NPCPresence_AddFlock(object sender, RoutedEventArgs e)
        {
            NPCFlockData data = this.SelectedFlock.copy();
            data.dBAction = DBAction.INSERT;
            data.presenceid = this.SelectedNPCPresence.id;
            data.name = "RENAME ME__UNIQUE__ REQD";
            data.note = "NEW FLOCK, WRITE NOTE!";
            this.NPCFlockList.Add(data);
        }

        #endregion


        #region ArtifactLoot
        public ArtifactLoot ArtifactLoot { get; set; }
        public ArtifactTypesTable ArtifactType { get; set; }

        private ObservableCollection<ArtifactLootItem> _artilootdata;
        public ObservableCollection<ArtifactLootItem> ArtifactLoots
        {
            get
            {
                return _artilootdata;
            }
            set
            {
                _artilootdata = value;
                OnPropertyChanged("ArtifactLoots");
            }
        }

        private List<ArtifactTypesTable> _artifactTypes;
        public List<ArtifactTypesTable> ArtifactTypes
        {
            get
            {
                return _artifactTypes;
            }
            set
            {
                _artifactTypes = value;
                OnPropertyChanged("ArtifactTypes");
            }
        }


        private ArtifactTypesTable _selectedArtifactType;
        public ArtifactTypesTable SelectedArtifactType
        {
            get
            {
                return this._selectedArtifactType;
            }
            set
            {
                this._selectedArtifactType = value;
                OnPropertyChanged("SelectedArtifactType");
            }
        }

        private void ComboBox_DropDownClosed_ArtifactType(object sender, EventArgs e)
        {
            if (SelectedArtifactType == null) { return; }
            this.ArtifactLoots = ArtifactLoot.GetLootByArtifactTypeID(SelectedArtifactType.id);
            this.removeArtifactLoots.Clear();
        }


        private void ComboBox_DropDownClosed_ArtifactLoots(object sender, EventArgs e)
        {
            System.Console.WriteLine(currentRowAddItem);
        }

        private void ArtifactLoots_Save_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                //Set the artifacttype = same for all updates on one 'page'
                sb.AppendLine(ArtifactLootItem.GetLootDeclStatment());
                sb.AppendLine(ArtifactLootItem.GetDeclStatement());
                sb.AppendLine(db.ArtifactTypesTable.GetArtifactTypeDeclStatement());
                sb.AppendLine(SelectedArtifactType.GetArtifactTypeDefinitionLokupStatement());

                foreach (ArtifactLootItem item in this.ArtifactLoots)
                {
                    if (item.dBAction == DBAction.UPDATE)
                    {
                        ArtifactLoot.updateSelf(item);
                        sb.AppendLine(item.GetLootDefinitionLookupStatement());
                        sb.AppendLine(item.GetLookupStatement());
                        sb.AppendLine(ArtifactLoot.Save());
                        sb.AppendLine();
                    }
                    else if (item.dBAction == DBAction.INSERT)
                    {
                        ArtifactLoot.updateSelf(item);
                        sb.AppendLine(item.GetLootDefinitionLookupStatement());
                        sb.AppendLine(ArtifactLoot.Insert());
                        sb.AppendLine();
                    }
                }

                foreach (ArtifactLootItem item in this.removeArtifactLoots)
                {
                    if (item.dBAction == DBAction.DELETE)
                    {
                        sb.AppendLine(item.GetLootDefinitionLookupStatement());
                        sb.AppendLine(item.GetLookupStatement());
                        ArtifactLoot.updateSelf(item);
                        sb.AppendLine(ArtifactLoot.Delete());
                        sb.AppendLine();
                    }
                }
                this.removeArtifactLoots.Clear();
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + SelectedArtifactType.name + "_loot" + Utilities.timestamp() + ".sql", sb.ToString());
                MessageBox.Show("Saved!", "Info", 0, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }

        }

        public List<ArtifactLootItem> copyArtifactLoots = new List<ArtifactLootItem>();
        private void Copy_ArtifactLoots(object sender, EventArgs e)
        {
            copyArtifactLoots = new List<ArtifactLootItem>(this.ArtifactLoots);
        }

        private void Paste_ArtifactLoots(object sender, EventArgs e)
        {
            foreach (ArtifactLootItem item in this.copyArtifactLoots)
            {
                item.dBAction = DBAction.INSERT;
                this.ArtifactLoots.Add(item);
            }
        }

        private ArtifactLootItem _privateSelectedArtifactLoot;
        public ArtifactLootItem SelectedArtifactLoot
        {
            get
            {
                return _privateSelectedArtifactLoot;
            }
            set
            {
                _privateSelectedArtifactLoot = value;
                OnPropertyChanged("SelectedArtifactLoot");
            }
        }

        public List<ArtifactLootItem> removeArtifactLoots = new List<ArtifactLootItem>();
        private void ArtifactLoots_Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.ArtifactLoots.Count > 0)
            {
                ArtifactLootItem lootToRemove = this.ArtifactLoots.Last<ArtifactLootItem>();
                if (this.SelectedArtifactLoot != null)
                {
                    lootToRemove = SelectedArtifactLoot;
                }
                if (lootToRemove.dBAction != DBAction.INSERT)
                {
                    lootToRemove.dBAction = DBAction.DELETE;
                    removeArtifactLoots.Add(lootToRemove);
                }
                this.ArtifactLoots.Remove(lootToRemove);
            }
        }

        private void ArtifactLoots_Add_Row_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ArtifactLootItem loot = ArtifactLootItem.CreateNewForArtifactType(this.SelectedArtifactType, this.currentRowAddItem);
                loot.dBAction = DBAction.INSERT;
                this.ArtifactLoots.Add(loot);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }
        }

        #endregion


        #region ArtifactSpawnInfo

        private ObservableCollection<ArtifactSpawnInfoRecord> _artifactSpawnInfos;
        public ObservableCollection<ArtifactSpawnInfoRecord> ArtifactSpawnInfos
        {
            get
            {
                return this._artifactSpawnInfos;
            }
            set
            {
                this._artifactSpawnInfos = value;
                OnPropertyChanged("ArtifactSpawnInfos");
            }
        }

        private ArtifactSpawnInfoRecord _selectedArtifactSpawnInfo;
        public ArtifactSpawnInfoRecord SelectedArtifactSpawnInfo
        {
            get
            {
                return this._selectedArtifactSpawnInfo;
            }
            set
            {
                this._selectedArtifactSpawnInfo = value;
                OnPropertyChanged("SelectedArtifactSpawnInfo");
            }
        }

        private void ComboBox_DropDownClosed_Zone(object sender, EventArgs e)
        {
            if (SelectedZone == null) { return; }
            this.ArtifactSpawnInfos = this.ArtifactSpawnInfoTable.GetAllByZone(SelectedZone);
            this.removeArtifactSpawnInfo.Clear();
        }



        private void ArtifactSpawnInfo_Save_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                //Set the zone for the "page" of artifactspawninfos
                sb.AppendLine(Zones.GetZoneDeclStatement());
                sb.AppendLine(ArtifactTypeRecord.GetDeclStatement());
                sb.AppendLine(SelectedZone.GetZoneLookupStatement());

                sb.AppendLine(ArtifactSpawnInfoRecord.GetDeclStatement());

                foreach (ArtifactSpawnInfoRecord info in this.ArtifactSpawnInfos)
                {
                    ArtifactSpawnInfo infoTable = info.ToTable(this.Connstr);
                    sb.AppendLine(new ArtifactTypeRecord { Name = info.ArtifactTypeName }.GetArtifactTypeLookupStatement());
                    if (info.dBAction == DBAction.UPDATE)
                    {
                        sb.AppendLine(info.GetArtifactSpawnInfoLookupStatement());
                        sb.AppendLine(infoTable.Save());
                        sb.AppendLine();
                    }
                    else if (info.dBAction == DBAction.INSERT)
                    {
                        sb.AppendLine(infoTable.Insert());
                        sb.AppendLine();
                    }
                }

                foreach (ArtifactSpawnInfoRecord info in this.removeArtifactSpawnInfo)
                {
                    ArtifactSpawnInfo infoTable = info.ToTable(this.Connstr);
                    sb.AppendLine(new ArtifactTypeRecord { Name = info.ArtifactTypeName }.GetArtifactTypeLookupStatement());
                    if (info.dBAction == DBAction.DELETE)
                    {
                        sb.AppendLine(info.GetArtifactSpawnInfoLookupStatement());
                        sb.AppendLine(infoTable.Delete());
                        sb.AppendLine();
                    }
                }
                this.removeArtifactSpawnInfo.Clear();
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\ArtifactSpawnInfo_" + SelectedZone.ConcatZoneIDName + "_" + Utilities.timestamp() + ".sql", sb.ToString());
                MessageBox.Show("Saved!", "Info", 0, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }

        }

        public List<ArtifactSpawnInfoRecord> copyArtifactSpawnInfos = new List<ArtifactSpawnInfoRecord>();
        private void Copy_ArtifactSpawnInfo(object sender, EventArgs e)
        {
            copyArtifactSpawnInfos = new List<ArtifactSpawnInfoRecord>(this.ArtifactSpawnInfos);
        }

        private void Paste_ArtifactSpawnInfo(object sender, EventArgs e)
        {
            foreach (ArtifactSpawnInfoRecord item in this.copyArtifactSpawnInfos)
            {
                item.dBAction = DBAction.INSERT;
                this.ArtifactSpawnInfos.Add(item);
            }
        }

        public List<ArtifactSpawnInfoRecord> removeArtifactSpawnInfo = new List<ArtifactSpawnInfoRecord>();
        private void ArtifactSpawnInfo_Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.ArtifactSpawnInfos.Count > 0)
            {
                ArtifactSpawnInfoRecord infoToRemove = this.ArtifactSpawnInfos.Last<ArtifactSpawnInfoRecord>();
                if (this.SelectedArtifactSpawnInfo != null)
                {
                    infoToRemove = SelectedArtifactSpawnInfo;
                }
                if (infoToRemove.dBAction != DBAction.INSERT)
                {
                    infoToRemove.dBAction = DBAction.DELETE;
                    removeArtifactSpawnInfo.Add(infoToRemove);
                }
                this.ArtifactSpawnInfos.Remove(infoToRemove);
            }
        }

        private void ArtifactSpawnInfo_Add_Row_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ArtifactSpawnInfoRecord info = ArtifactSpawnInfoRecord.CreateNewForZone(this.SelectedArtifactType, this.SelectedZone);
                this.ArtifactSpawnInfos.Add(info);
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
