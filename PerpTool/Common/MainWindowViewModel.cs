using Dragablz;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PerpTool.Common
{

    class MainWindowViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private ObservableCollection<TabContent> _tabContents;
        public ObservableCollection<TabContent> TabContents
        {
            get
            {
                return _tabContents;
            }
            set
            {
                _tabContents = value;
                OnPropertyChanged("TabContents");
            }
        }

        TabContent _tb;
        public TabContent ActiveTab
        {
            get
            {
                return _tb;
            }
            set
            {
                _tb = value;
                OnPropertyChanged("ActiveTab");
            }
        }

        public MainWindowViewModel()
        {
            TabContents = new ObservableCollection<TabContent>();
            ZoneMaintenanceCommand = new RelayCommand(p => ZoneMaintenanceCommandAction());
        }

        public RelayCommand ZoneMaintenanceCommand { get; set; }

        private void ZoneMaintenanceCommandAction()
        {

            TabContent newtab = new TabContent("Zone Maintenance", new Views.ZoneMaint(), false);
            TabContents.Add(newtab);
            ActiveTab = newtab; //force that tab active


        }

    }
}
