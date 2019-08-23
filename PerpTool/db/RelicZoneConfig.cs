using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Perptool.db;

namespace PerpTool.db
{
    public class RelicZoneConfig
    {
        public int Id { get; set; }
        public int RelicTypeID { get; set; }
        public string RelicTypeName { get; set; }
        public int ZoneID { get; set; }
        public string ZoneName { get; set; }
        public int Rate { get; set; }
        public int? X { get; set; }
        public int? Y { get; set; }
        public DBAction dBAction { get; set; }

        public RelicZoneConfig()
        {

        }
        public static string IDKey = "@relicZoneConfigID";

        public static string GetDeclStatement()
        {
            return "DECLARE " + IDKey + " int;";
        }

        public string GetLookupStatement()
        {
            return "SET " + IDKey + " = (SELECT TOP 1 id FROM reliczoneconfig WHERE zone = " + Zones.IDKey + "  AND relictypeid = " + RelicTypeRecord.IDKey + ");";
        }

        public static RelicZoneConfig CreateNewForZoneAndType(Zones zone, RelicTypeRecord relic)
        {
            RelicZoneConfig conf = new RelicZoneConfig();
            conf.dBAction = DBAction.INSERT;
            conf.RelicTypeID = relic.Id;
            conf.RelicTypeName = relic.Name;
            conf.ZoneID = zone.id;
            conf.ZoneName = zone.ConcatZoneIDName;
            conf.Rate = 0;
            conf.X = null;
            conf.Y = null;
            return conf;
        }

    }


    public class RelicZoneConfigTable
    {
        private int _id;
        private int _relictypeid;
        private int _zoneid;
        private int _rate;
        private int? _x;
        private int? _y;

        public int id
        {
            get
            {
                return this._id;
            }

            set
            {
                this._id = value;
                this.OnPropertyChanged("id");
            }
        }

        public int relicTypeId
        {
            get
            {
                return this._relictypeid;
            }

            set
            {
                this._relictypeid = value;
                this.OnPropertyChanged("relictypeid");
            }
        }

        public int zoneId
        {
            get
            {
                return this._zoneid;
            }

            set
            {
                this._zoneid = value;
                this.OnPropertyChanged("zoneid");
            }
        }

        public int rate
        {
            get
            {
                return this._rate;
            }

            set
            {
                this._rate = value;
                this.OnPropertyChanged("rate");
            }
        }

        public int? x
        {
            get
            {
                return this._x;
            }

            set
            {
                this._x = value;
                this.OnPropertyChanged("x");
            }
        }

        public int? y
        {
            get
            {
                return this._y;
            }

            set
            {
                this._y = value;
                this.OnPropertyChanged("y");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private string ConnString { get; set; }
        public RelicZoneConfigTable(string connectionString)
        {
            this.ConnString = connectionString;
        }
    }
}
