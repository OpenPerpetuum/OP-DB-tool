﻿using Perptool.db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                HeadSlotList.Clear();
                if (value != null)
                {
                    for (int i = 0; i < value.options.slotFlags.Length; i++)
                    {
                        ModuleTemplate tmp = new ModuleTemplate();
                        BotTemplate.headModules.Add(tmp);
                        HeadSlotList.Add(new Usercontrols.RobotTemplateSlot(tmp)
                        {
                            ModulesList = Mods.Where(m => m.options.moduleFlag == SelectedHead.options.slotFlags[i]).ToList()
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
                        ModuleTemplate tmp = new ModuleTemplate();
                        BotTemplate.chassisModules.Add(tmp);
                        ChassisSlotList.Add(new Usercontrols.RobotTemplateSlot(tmp)
                        {
                            ModulesList = Mods.Where(m => m.options.moduleFlag == SelectedChassis.options.slotFlags[i]).ToList()
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
                        ModuleTemplate tmp = new ModuleTemplate();
                        BotTemplate.headModules.Add(tmp);
                        LegSlotList.Add(new Usercontrols.RobotTemplateSlot(tmp) { });
                    }
                }
                OnPropertyChanged("SelectedLeg");
            }
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {

        }
    }
}
