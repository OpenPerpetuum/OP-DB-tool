using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerpTool.Common
{
    public class TabContent : INotifyPropertyChanged
    {
        private string _header;
        private object _content;
        private object _selected;

        public TabContent(string header, object content, bool selected)
        {
            Header = header;
            Content = content;
            Selected = selected;
        }

        /// <summary>
        /// header for the tab
        /// </summary>
        public string Header
        {
            get
            {
                return _header;
            }
            set
            {
                _header = value;
                OnPropertyChanged("Header");
            }
        }

        /// <summary>
        /// this holds the usercontrol object
        /// </summary>
        public object Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                OnPropertyChanged("Content");
            }

        }

        public object Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                OnPropertyChanged("Selected");
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
