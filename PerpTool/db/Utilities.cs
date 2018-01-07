using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerpTool.db
{
    public static class Utilities
    {

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
            return DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
        }
    }

    

    public enum DBAction
    {
        UPDATE,
        INSERT,
        DELETE
    }
}
