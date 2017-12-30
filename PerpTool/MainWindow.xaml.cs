using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Perptool.db;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Collections.ObjectModel;

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


            EntityItems = Entities.GetEntitiesWithFields();
            ZoneList = ZoneTbl.GetAllZones();
            SpawnList = Spawn.GetAllSpawns();
            LootableBots = Entities.GetAllNPCLootableBots();
            LootableEntityDefaults = Entities.GetLootableEntities();
            BotItems = Entities.GetAllDistinctBotItems();

            this.selectedEntity = new ObservableCollection<EntityItems>();


            this.DataContext = this;
        }

        private EntityItems currentSelection { get; set; }
        private AggregateModifiers AgModifiers { get; set; }
        private AggregateFields AgFields { get; set; }
        private EntityDefaults Entities { get; set; }
        private AggregateValues AgValues { get; set; }
        private Accounts PerpAccounts { get; set; }
        private Characters PerpChars { get; set; }
        public Zones ZoneTbl { get; set; }
        public NPCSpawn Spawn { get; set; }
        public NPCLoot Loot { get; set; }
        private ChassisBonus BotBonus { get; set; }
        public List<EntityItems> BotItems { get; set; }

        public List<EntityItems> EntityItems { get; set; }


        //TODO hack, using observable collection for one item BAD
        private ObservableCollection<EntityItems> _selectedEntity;
        public ObservableCollection<EntityItems> selectedEntity
        {
            get
            {
                return _selectedEntity;
            }
            set
            {
                _selectedEntity = value;
                OnPropertyChanged("selectedEntity");
            }
        }


        private List<FieldValuesStuff> _valstuffs;
        public List<FieldValuesStuff> FieldValuesList
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (FieldValuesStuff item in FieldValuesList)
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

                if (selectedEntity.Count == 1) //TODO hack -- impl object that is observable by gui framework
                {
                    foreach (EntityItems eItem in this.selectedEntity)
                    {
                        sb.AppendLine(Entities.SaveWithEntityItemChange(eItem));
                    }
                }

                Console.WriteLine(sb.ToString());
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + currentSelection.Name + ".sql", sb.ToString());
                MessageBox.Show("Saved!", "Info", 0, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            EntityItems item = (EntityItems)combo.SelectedItem;
            this.selectedEntity.Clear();
            this.selectedEntity.Add(item);
            this.currentSelection = item;
            if (item == null) { return; }
            FieldValuesList = AgValues.GetValuesForEntity(item.Definition);
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
                //TODO
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
