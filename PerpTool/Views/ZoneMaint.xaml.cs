using Perpetuum.ExportedTypes;
using Perptool.db;
using PerpTool.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace PerpTool.Views
{
    /// <summary>
    /// Interaction logic for Zones.xaml
    /// </summary>
    public partial class ZoneMaint : UserControl, INotifyPropertyChanged
    {

        public class ZoneType
        {
            public ZoneType(int v, string d)
            {
                value = v;
                description = d;
            }
            public int value { get; set; } 
            public string description { get; set; }
        }

        public ZoneMaint()
        {
            InitializeComponent();

            SaveZoneCommand = new RelayCommand(p => SaveZoneCommandAction());

            ZoneTbl = new Zones(Global.ConnectionString);
            ZoneList = ZoneTbl.GetAllZones();

            Spawn = new NPCSpawn(Global.ConnectionString);
            SpawnList = Spawn.GetAllSpawns();

            ZoneList.Add(new Zones(Global.ConnectionString) { name = "Create New Zone", id=9999, description="New Zone", note = "New Zone" });

            ZoneTypeList = new List<ZoneType>
            {
                new ZoneType(0, "Undefined"),
                new ZoneType(1, "PvE"),
                new ZoneType(2, "PvP"),
                new ZoneType(3, "Training")
            };

            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Zones ZoneTbl { get; set; }
        public NPCSpawn Spawn { get; set; }

        public RelayCommand SaveZoneCommand { get; set; }

        private List<Zones> _zones;
        public List<Zones> ZoneTypes
        {
            get
            {
                return _zones;
            }
            set
            {
                _zones = value;
                OnPropertyChanged();
            }
        }

        private List<Zones> _zonelist;
        public List<Zones> ZoneList
        {
            get
            {
                return _zonelist;
            }
            set
            {
                _zonelist = value;
                OnPropertyChanged();
            }
        }


        private List<ZoneType> _zonetypelist;
        public List<ZoneType> ZoneTypeList
        {
            get
            {
                return _zonetypelist;
            }
            set
            {
                _zonetypelist = value;
                OnPropertyChanged();
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
               OnPropertyChanged();
            }
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
                OnPropertyChanged();
            }
        }

        private void SaveZoneCommandAction()
        {
            // FIXME: support new zones.
            try
            {
                if (this.SelectedZone.id > 0)
                {
                    this.SelectedZone.Save();
                }
            }
            catch
            {

            }
        }

    }
}
