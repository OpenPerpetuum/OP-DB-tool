using Perptool.db;
using PerpTool.db;
using PerpTool.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Shapes;

namespace PerpTool.Dialogs
{
    /// <summary>
    /// Interaction logic for CreateRobotTemplate.xaml
    /// </summary>
    public partial class CreateRobotTemplate : Window, INotifyPropertyChanged
    {
        // FIXME: this needs to be moved!
        private string Connstr = "Server=localhost\\PERPSQL;Database=perpetuumsa;Trusted_Connection=True;Pooling=True;Connection Timeout=30;Connection Lifetime=260;Connection Reset=True;Min Pool Size=20;Max Pool Size=60;";

        public CreateRobotTemplate()
        {
            InitializeComponent();
            Entities = new EntityDefaults(this.Connstr);
            RTemplate = new RobotTemplatesTable(this.Connstr);
            BotTemplate = new RobotTemplate();
            HeadSlotList = new CompositeCollection();
            ChassisSlotList = new CompositeCollection();
            LegSlotList = new CompositeCollection();

            BotsList = Entities.GetEntitiesByCategory(Types.CategoryFlags.cf_robots);
            HeadsList = Entities.GetEntitiesByCategory(Types.CategoryFlags.cf_robot_head);
            ChassisList = Entities.GetEntitiesByCategory(Types.CategoryFlags.cf_robot_chassis);
            LegsList = Entities.GetEntitiesByCategory(Types.CategoryFlags.cf_robot_leg);
            InventoryList = Entities.GetEntitiesByCategory(Types.CategoryFlags.cf_robot_inventory);
            // oh dear god. Shitshow ahead!
            Mods = Entities.GetAllEntities();


            this.DataContext = this;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public EntityDefaults Entities { get; set; }
        public RobotTemplatesTable RTemplate { get; set; }
        public RobotTemplate BotTemplate { get; set; }
        List<EntityDefaults> Mods { get; set; }

        private List<EntityDefaults> _bots;
        public List<EntityDefaults> BotsList
        {
            get
            {
                return _bots;
            }
            set
            {
                _bots = value;
                OnPropertyChanged("BotsList");
            }
        }

        private EntityDefaults _selbot;
        public EntityDefaults SelectedBot
        {
            get
            {
                return _selbot;
            }
            set
            {
                _selbot = value;              
                OnPropertyChanged("SelectedBot");
            }
        }

        private List<EntityDefaults> _heads;
        public List<EntityDefaults> HeadsList
        {
            get
            {
                return _heads;
            }
            set
            {
                _heads = value;
                OnPropertyChanged("HeadsList");
            }
        }

        private List<EntityDefaults> _chassis;
        public List<EntityDefaults> ChassisList
        {
            get
            {
                return _chassis;
            }
            set
            {
                _chassis = value;
                OnPropertyChanged("ChassisList");
            }
        }

        private List<EntityDefaults> _legs;
        public List<EntityDefaults> LegsList
        {
            get
            {
                return _legs;
            }
            set
            {
                _legs = value;
                OnPropertyChanged("LegsList");
            }
        }

        private List<EntityDefaults> _inv;
        public List<EntityDefaults> InventoryList
        {
            get
            {
                return _inv;
            }
            set
            {
                _inv = value;
                OnPropertyChanged("InventoryList");
            }
        }

        private List<EntityDefaults> _headmoduleslist;
        public List<EntityDefaults> HeadModulesList
        {
            get
            {
                return _headmoduleslist;
            }
            set
            {
                _headmoduleslist = value;
                OnPropertyChanged("HeadModulesList");
            }
        }

        private List<EntityDefaults> _chassismoduleslist;
        public List<EntityDefaults> ChassisModulesList
        {
            get
            {
                return _chassismoduleslist;
            }
            set
            {
                _chassismoduleslist = value;
                OnPropertyChanged("ChassisModulesList");
            }
        }

        private CompositeCollection _hslist;
        public CompositeCollection HeadSlotList
        {
            get
            {
                return _hslist;
            }
            set
            {
                _hslist = value;
                OnPropertyChanged("HeadSlotList");
            }
        }

        private CompositeCollection _chassissslotlist;
        public CompositeCollection ChassisSlotList
        {
            get
            {
                return _chassissslotlist;
            }
            set
            {
                _chassissslotlist = value;
                OnPropertyChanged("ChassisSlotList");
            }
        }

        private CompositeCollection _legslotlist;
        public CompositeCollection LegSlotList
        {
            get
            {
                return _legslotlist;
            }
            set
            {
                _legslotlist = value;
                OnPropertyChanged("LegSlotList");
            }
        }        

        private EntityDefaults _selhead;
        public EntityDefaults SelectedHead
        {
            get
            {
                return _selhead;
            }
            set
            {
                _selhead = value;
                // probably should not be here in the setter....
                // yes, this is HORRID. laugh it up.
                HeadSlotList.Clear();
                if (value != null)
                {
                    for (int i = 0; i < value.options.slotFlags.Length; i++)
                    {

                        List<EntityDefaults> foo = new List<EntityDefaults>();

                        if (SelectedHead.options.slotFlags[i].HasFlag(SlotFlags.chassis))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.chassis)).ToList());
                        }
                        if (SelectedHead.options.slotFlags[i].HasFlag(SlotFlags.ew_and_engineering))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.ew_and_engineering)).ToList());
                        }
                        if (SelectedHead.options.slotFlags[i].HasFlag(SlotFlags.head))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.head)).ToList());
                        }
                        if (SelectedHead.options.slotFlags[i].HasFlag(SlotFlags.industrial))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.industrial)).ToList());
                        }
                        if (SelectedHead.options.slotFlags[i].HasFlag(SlotFlags.large))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.large)).ToList());
                        }
                        if (SelectedHead.options.slotFlags[i].HasFlag(SlotFlags.leg))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.leg)).ToList());
                        }
                        if (SelectedHead.options.slotFlags[i].HasFlag(SlotFlags.medium))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.medium)).ToList());
                        }
                        if (SelectedHead.options.slotFlags[i].HasFlag(SlotFlags.melee))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.melee)).ToList());
                        }
                        if (SelectedHead.options.slotFlags[i].HasFlag(SlotFlags.missile))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.missile)).ToList());
                        }
                        if (SelectedHead.options.slotFlags[i].HasFlag(SlotFlags.small))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.small)).ToList());
                        }
                        if (SelectedHead.options.slotFlags[i].HasFlag(SlotFlags.turret))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.turret)).ToList());
                        }

                        ModuleTemplate tmp = new ModuleTemplate();
                        tmp.slot = i + 1;
                        BotTemplate.headModules.Add(tmp);
                        HeadSlotList.Add(new Usercontrols.RobotTemplateSlot()
                        {
                            ModulesList = foo,
                            Module = tmp
                        });
                    }
                }
                OnPropertyChanged("SelectedHead");
            }
        }

        private EntityDefaults _selchassis;
        public EntityDefaults SelectedChassis
        {
            get
            {
                return _selchassis;
            }
            set
            {
                _selchassis = value;
                // probably should not be here in the setter....
                ChassisSlotList.Clear();
                if (value != null)
                {
                    for (int i = 0; i < value.options.slotFlags.Length; i++)
                    {

                        List<EntityDefaults> foo = new List<EntityDefaults>();

                        if (SelectedChassis.options.slotFlags[i].HasFlag(SlotFlags.chassis))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.chassis)).ToList());
                        }
                        if (SelectedChassis.options.slotFlags[i].HasFlag(SlotFlags.ew_and_engineering))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.ew_and_engineering)).ToList());
                        }
                        if (SelectedChassis.options.slotFlags[i].HasFlag(SlotFlags.head))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.head)).ToList());
                        }
                        if (SelectedChassis.options.slotFlags[i].HasFlag(SlotFlags.industrial))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.industrial)).ToList());
                        }
                        if (SelectedChassis.options.slotFlags[i].HasFlag(SlotFlags.large))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.large)).ToList());
                        }
                        if (SelectedChassis.options.slotFlags[i].HasFlag(SlotFlags.leg))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.leg)).ToList());
                        }
                        if (SelectedChassis.options.slotFlags[i].HasFlag(SlotFlags.medium))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.medium)).ToList());
                        }
                        if (SelectedChassis.options.slotFlags[i].HasFlag(SlotFlags.melee))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.melee)).ToList());
                        }
                        if (SelectedChassis.options.slotFlags[i].HasFlag(SlotFlags.missile))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.missile)).ToList());
                        }
                        if (SelectedChassis.options.slotFlags[i].HasFlag(SlotFlags.small))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.small)).ToList());
                        }
                        if (SelectedChassis.options.slotFlags[i].HasFlag(SlotFlags.turret))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.turret)).ToList());
                        }

                        ModuleTemplate tmp = new ModuleTemplate();
                        tmp.slot = i + 1;
                        BotTemplate.chassisModules.Add(tmp);
                        ChassisSlotList.Add(new Usercontrols.RobotTemplateSlot()
                        {
                            ModulesList = foo,
                            Module = tmp
                            
                        });
                    }
                }
                OnPropertyChanged("SelectedChassis");
            }
        }

        private EntityDefaults _selleg;
        public EntityDefaults SelectedLeg
        {
            get
            {
                return _selleg;
            }
            set
            {
                _selleg = value;
                // probably should not be here in the setter....
                LegSlotList.Clear();
                if (value != null)
                {
                    for (int i = 0; i < value.options.slotFlags.Length; i++)
                    {
                        List<EntityDefaults> foo = new List<EntityDefaults>();

                        if (SelectedLeg.options.slotFlags[i].HasFlag(SlotFlags.chassis))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.chassis)).ToList());
                        }
                        if (SelectedLeg.options.slotFlags[i].HasFlag(SlotFlags.ew_and_engineering))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.ew_and_engineering)).ToList());
                        }
                        if (SelectedLeg.options.slotFlags[i].HasFlag(SlotFlags.head))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.head)).ToList());
                        }
                        if (SelectedLeg.options.slotFlags[i].HasFlag(SlotFlags.industrial))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.industrial)).ToList());
                        }
                        if (SelectedLeg.options.slotFlags[i].HasFlag(SlotFlags.large))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.large)).ToList());
                        }
                        if (SelectedLeg.options.slotFlags[i].HasFlag(SlotFlags.leg))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.leg)).ToList());
                        }
                        if (SelectedLeg.options.slotFlags[i].HasFlag(SlotFlags.medium))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.medium)).ToList());
                        }
                        if (SelectedLeg.options.slotFlags[i].HasFlag(SlotFlags.melee))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.melee)).ToList());
                        }
                        if (SelectedLeg.options.slotFlags[i].HasFlag(SlotFlags.missile))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.missile)).ToList());
                        }
                        if (SelectedLeg.options.slotFlags[i].HasFlag(SlotFlags.small))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.small)).ToList());
                        }
                        if (SelectedLeg.options.slotFlags[i].HasFlag(SlotFlags.turret))
                        {
                            foo.AddRange(Mods.Where(m => m.options.moduleFlag.HasFlag(SlotFlags.turret)).ToList());
                        }

                        ModuleTemplate tmp = new ModuleTemplate();
                        tmp.slot = i + 1;
                        BotTemplate.legModules.Add(tmp);
                        LegSlotList.Add(new Usercontrols.RobotTemplateSlot()
                        {
                            ModulesList = foo,
                            Module = tmp

                        });
                    }
                }
                OnPropertyChanged("SelectedLeg");
            }
        }

        private EntityDefaults _selinv;
        public EntityDefaults SelectedInv
        {
            get
            {
                return _selinv;
            }
            set
            {
                _selinv = value;
                OnPropertyChanged("SelectedInv");
            }
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            BotTemplate.containerID = this.SelectedInv.definition;
            BotTemplate.robotID = this.SelectedBot.definition;
            BotTemplate.chassisID = this.SelectedChassis.definition;
            BotTemplate.headID = this.SelectedHead.definition;
            BotTemplate.legID = this.SelectedLeg.definition;

            RTemplate.description = BotTemplate.ToGenXY();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(RTemplate.SaveNewBotTemplate());
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + RTemplate.name + Utilities.timestamp() + ".sql", sb.ToString());
                MessageBox.Show("New RobotTemplate Saved!!", "Info", 0, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving!" + ex.Message, "Error", 0, MessageBoxImage.Error);
                return;
            }

            this.DialogResult = true;
            this.Hide();
        }


        //private List<EntityDefaults> FilterEntitiesForSlots(EntityDefaults Entity)
        //{
        //    if (Entity.options.slotFlags[].HasFlag())

        //}

    }
}
