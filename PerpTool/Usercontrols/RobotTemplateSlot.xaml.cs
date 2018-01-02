using Perptool.db;
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
        public RobotTemplateSlot(ModuleTemplate template)
        {
            module = template;
            InitializeComponent();
        }

        ModuleTemplate module
        {
            get;
            set;
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
