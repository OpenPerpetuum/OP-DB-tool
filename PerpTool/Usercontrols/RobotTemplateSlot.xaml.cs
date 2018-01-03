using Perptool.db;
using PerpTool.Types;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PerpTool.Usercontrols
{
    /// <summary>
    /// Interaction logic for RobotTemplateSlot.xaml
    /// </summary>
    public partial class RobotTemplateSlot : UserControl, INotifyPropertyChanged
    {
        public RobotTemplateSlot()
        {
            InitializeComponent();
            Entities = new EntityDefaults(Connstr);
            this.PropertyChanged += RobotTemplateSlot_PropertyChanged;
        }

        private void RobotTemplateSlot_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedModule")
            {
                AmmoList = Entities.GetEntitiesByCategory((CategoryFlags)this.SelectedModule.options.ammoType);
            }
        }

        // MOVE IT!!!!!
        private string Connstr = "Server=localhost\\PERPSQL;Database=perpetuumsa;Trusted_Connection=True;Pooling=True;Connection Timeout=30;Connection Lifetime=260;Connection Reset=True;Min Pool Size=20;Max Pool Size=60;";

        public ModuleTemplate Module
        {
            get;
            set;
        }



        private EntityDefaults Entities { get; set; }

        List<EntityDefaults> _modlist;
        public List<EntityDefaults> ModulesList
        {
            get
            {
                return _modlist;
            }
            set
            {
                _modlist = value;
                OnPropertyChanged("ModulesList");
            }
        }

        List<EntityDefaults> _ammolist;
        public List<EntityDefaults> AmmoList
        {
            get
            {
                return _ammolist;
            }
            set
            {
                _ammolist = value;
                OnPropertyChanged("AmmoList");
            }
        }

        EntityDefaults _selmod;
        public EntityDefaults SelectedModule
        {
            get
            {
                return _selmod;
            }
            set
            {
                _selmod = value;                
                OnPropertyChanged("SelectedModule");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
