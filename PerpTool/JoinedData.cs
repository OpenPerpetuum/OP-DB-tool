using Perptool.db;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerpTool
{
    public class JoinedData
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal value { get; set; }
        public int formula { get; set; }
    }


    class CombinedQuery
    {
        private AggregateValues aggregateValues;

        private AggregateFields aggregateFields;

        private String ConnString;

        public CombinedQuery(AggregateValues aggVal, AggregateFields aggFields, String connString)
        {
            this.aggregateValues = aggVal;
            this.aggregateFields = aggFields;
            this.ConnString = connString;
        }


        public List<JoinedData> getDataFor(int entityDef)
        {
            List<JoinedData> data = new List<JoinedData>();
            
            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("Select * from dbo.aggregatevalues join dbo.aggregatefields on (dbo.aggregatevalues.field = aggregatefields.id) where definition = @entitydef");
                    command.CommandText = sqlCommand.ToString();
                    command.Parameters.AddWithValue("@entitydef", entityDef);
                    command.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            JoinedData jData = new JoinedData();
                            jData.id = Convert.ToInt32(reader["id"]);
                            jData.name = Convert.ToString(reader["name"]);
                            jData.value = Convert.ToDecimal(reader["value"]);
                            jData.formula = Convert.ToInt32(reader["formula"]);
                            data.Add(jData);
                        }

                    }

                }

            }
            return data;
        }
    }
}
