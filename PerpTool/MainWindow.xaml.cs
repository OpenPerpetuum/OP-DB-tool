﻿using System;
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
            combined = new CombinedQuery(AgValues, AgFields, Connstr);

            EntityItems = Entities.GetEntitiesWithFields();

            this.DataContext = this;
        }
        private CombinedQuery combined { get; set; }

        private AggregateModifiers AgModifiers { get; set; }
        private AggregateFields AgFields { get; set; }
        private EntityDefaults Entities { get; set; }
        private AggregateValues AgValues { get; set; }

        public List<EntityItems> EntityItems { get; set; }

        public List<AgFieldsValues> agfieldsList = new List<AgFieldsValues>();

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


        private List<JoinedData> _joinDataStuffs;
        public List<JoinedData> JoinedDataList
        {
            get
            {
                return _joinDataStuffs;
            }
            set
            {
                _joinDataStuffs = value;
                OnPropertyChanged("JoinedDataList");
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (JoinedData item in JoinedDataList)
                {
                    Console.WriteLine(item);
                    AgValues.GetById(item.id);
                    if (AgValues.value != item.value)
                    {
                        AgValues.value = item.value;
                        AgValues.Save();
                    }
                    if(AgFields.formula!= item.formula)
                    {
                        AgFields.formula = item.formula;
                        AgFields.Save();

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


            AgValues.GetById(item.Definition);
            AgFields.GetById(AgValues.field);

            JoinedDataList = this.combined.getDataFor(item.Definition);

        }
    }
}