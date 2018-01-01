﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Perptool.db
{
    public class BotBonusObj
    {
        public int id { get; set; }
        public int definition { get; set; }
        public int extension { get; set; }
        public decimal bonus { get; set; }
        public string note { get; set; }
        public int targetpropertyID { get; set; }
        public int effectenhancer { get; set; }
        public string definitionName { get; set; }
        public string extensionName { get; set; }
        public string aggFieldName { get; set; }
    }


    class ChassisBonus : INotifyPropertyChanged
    {
        private int privateid;
        private int privatedefinition;
        private int privateextension;
        private decimal privatebonus;
        private string privatenote;
        private int privatetargetpropertyID;
        private int privateeffectenhancer;

        public ChassisBonus(string connectionString)
        {
            this.ConnString = connectionString;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        #region Fields

        public int id
        {
            get
            {
                return this.privateid;
            }

            set
            {
                this.privateid = value;
                this.OnPropertyChanged("id");
            }
        }

        public int definition
        {
            get
            {
                return this.privatedefinition;
            }

            set
            {
                this.privatedefinition = value;
                this.OnPropertyChanged("definition");
            }
        }

        public int extension
        {
            get
            {
                return this.privateextension;
            }

            set
            {
                this.privateextension = value;
                this.OnPropertyChanged("extension");
            }
        }

        public decimal bonus
        {
            get
            {
                return this.privatebonus;
            }

            set
            {
                this.privatebonus = value;
                this.OnPropertyChanged("bonus");
            }
        }

        public string note
        {
            get
            {
                return this.privatenote;
            }

            set
            {
                this.privatenote = value;
                this.OnPropertyChanged("note");
            }
        }

        public int targetpropertyID
        {
            get
            {
                return this.privatetargetpropertyID;
            }

            set
            {
                this.privatetargetpropertyID = value;
                this.OnPropertyChanged("targetpropertyID");
            }
        }

        public int effectenhancer
        {
            get
            {
                return this.privateeffectenhancer;
            }

            set
            {
                this.privateeffectenhancer = value;
                this.OnPropertyChanged("effectenchancer");
            }
        }

        private string ConnString { get; set; }

        #endregion

        public void Clear()
        {
            this.id = 0;
            this.definition = 0;
            this.extension = 0;
            this.bonus = 0;
            this.note = string.Empty;
            this.targetpropertyID = 0;
            this.effectenhancer = 0;
        }

        public ObservableCollection<BotBonusObj> getByEntity(int definitionID)
        {
            ObservableCollection<BotBonusObj> list = new ObservableCollection<BotBonusObj>();
            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append(@"SELECT chassisbonus.id,chassisbonus.definition,chassisbonus.extension,chassisbonus.note,chassisbonus.targetpropertyID,chassisbonus.bonus,
                    chassisbonus.effectenhancer,entitydefaults.definitionname,extensions.extensionname,aggregatefields.name
                    FROM chassisbonus JOIN entitydefaults on chassisbonus.definition=entitydefaults.definition JOIN extensions on extensions.extensionid=chassisbonus.extension
                    JOIN aggregatefields on aggregatefields.id=chassisbonus.targetpropertyID WHERE entitydefaults.definition=@id;");
                    command.CommandText = sqlCommand.ToString();
                    command.Parameters.AddWithValue("@id", definitionID);
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BotBonusObj tmp = new BotBonusObj();
                            tmp.id = Convert.ToInt32(reader["id"]);
                            tmp.definition = Convert.ToInt32(reader["definition"]);
                            tmp.extension = Convert.ToInt32(reader["extension"]);
                            tmp.bonus = Convert.ToDecimal(reader["bonus"]);
                            tmp.note = Convert.ToString(reader["note"]);
                            tmp.targetpropertyID = Convert.ToInt32(reader["targetpropertyID"]);
                            tmp.effectenhancer = Convert.ToInt32(reader["effectenhancer"]);
                            tmp.definitionName = Convert.ToString(reader["definitionname"]);
                            tmp.extensionName = Convert.ToString(reader["extensionname"]);
                            tmp.aggFieldName = Convert.ToString(reader["name"]);
                            list.Add(tmp);
                        }
                    }
                }
            }
            return list;
        }


        public string Save(BotBonusObj bonusChanges)
        {
            string query = "";
            using (SqlCommand command = new SqlCommand())
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("UPDATE chassisbonus SET effectenhancer=@effectenhancer, bonus=@bonus WHERE id = @id;");
                command.CommandText = sqlCommand.ToString();
                command.Parameters.AddWithValue("@id", bonusChanges.id);
                command.Parameters.AddWithValue("@bonus", bonusChanges.bonus);
                command.Parameters.AddWithValue("@effectenhancer", bonusChanges.effectenhancer);

                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();
                query = command.CommandText;
                foreach (SqlParameter p in command.Parameters)
                {
                    query = query.Replace(p.ParameterName, p.Value.ToString());
                }
            }
            return query;
        }



        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}