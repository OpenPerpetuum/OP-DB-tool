using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerpTool.db
{
    public static class Utilities
    {



        public static string parseCommandString(SqlCommand command, List<string> ignoreParams)
        {
            string query = command.CommandText;
            foreach (SqlParameter p in command.Parameters)
            {
                if (ignoreParams.Contains(p.ParameterName))
                {
                    continue;
                }
                else if (SqlDbType.NVarChar.Equals(p.SqlDbType) || SqlDbType.VarChar.Equals(p.SqlDbType))
                {
                    query = query.Replace(p.ParameterName, "'" + p.Value.ToString() + "'");
                }
                else
                {
                    query = query.Replace(p.ParameterName, String.Format(CultureInfo.InvariantCulture, "{0}", p.Value));
                }
            }
            return query;
        }

        public static string handleNullableString(object readValue)
        {
            if (DBNull.Value == readValue)
            {
                return "NULL";
            }
            return Convert.ToString(readValue);
        }

        public static object getNullableString(string s)
        {
            if (s=="NULL")
            {
                return (object)DBNull.Value;
            }
            return s;
        }

        public static int handleNullableInt(object readValue)
        {
            if (DBNull.Value == readValue)
            {
                return -1;
            }
            return Convert.ToInt32(readValue);
        }

        public static object getNullableInt(int v)
        {
            if (v < 0)
            {
                return (object)DBNull.Value;
            }
            return v;
        }
        public static string timestamp()
        {
            return DateTime.Now.ToString("__yyyy_MM_dd_HH_mm_ss");
        }
    }



    public enum DBAction
    {
        UPDATE,
        INSERT,
        DELETE
    }
}
