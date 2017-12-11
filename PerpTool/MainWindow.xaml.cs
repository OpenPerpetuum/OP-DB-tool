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

namespace PerpTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public class comboitems
        {
            int Definition { get; set; }
            string Name { get; set; }
        }

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

            EntityItems = Entities.GetEntitiesWithFields();

            this.DataContext = this;
        }

        private AggregateModifiers AgModifiers { get; set; }
        private AggregateFields AgFields { get; set; }
        private EntityDefaults Entities { get; set; }
        private AggregateValues AgValues { get; set; }
        private Accounts PerpAccounts { get; set; }
        private Characters PerpChars { get; set; }

        public List<EntityItems> EntityItems { get; set; }

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (FieldValuesStuff item in FieldValuesList)
                {
                    AgValues.GetById(item.FieldId);
                    if (AgValues.value != item.FieldValue)
                    {
                        AgValues.value = item.FieldValue;
                        AgValues.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doh! Could not save somthing!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
            }
            MessageBox.Show("Saved.", "Info", 0, MessageBoxImage.Information);
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            EntityItems item = (EntityItems)combo.SelectedItem;
            if (item == null) { return; }
            FieldValuesList = AgValues.GetValuesForEntity(item.Definition);
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

        private void GetAccounts_Click(object sender, RoutedEventArgs e)
        {
            AccountsList = PerpAccounts.GetAllAccounts();
            // get characters on account..
            //CharactersList = PerpChars.GetCharactersOnAccount(1);
        }

        private void SaveCharBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectedChar != null)
            {
                try
                {
                    this.SelectedChar.Save();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Failed to save character!\n" + ex.Message, "Error", 0, MessageBoxImage.Error);
                }
            }
        }
    }
}
