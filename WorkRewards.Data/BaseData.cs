using System;
using System.Collections.Generic;
using System.Text;

namespace WorkRewards.Data
{
    public class BaseData
    {
        public dynamic MakeSafeDBNull(object value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
                return DBNull.Value;
            else
                return value;
        }
        public dynamic MakeSafeDBNull(int value)
        {
            if (value <= 0)
                return DBNull.Value;
            else
                return value;
        }
        public dynamic MakeSafeDBNull(decimal value)
        {
            if (value <= 0)
                return DBNull.Value;
            else
                return value;
        }

        public string MakeSafeString(object strIn)
        {
            if ((strIn == DBNull.Value))
                return string.Empty;
            else
                return Convert.ToString(strIn);
        }
        public bool MakeSafeBoolean(object strIn)
        {
            if ((strIn == DBNull.Value))
                return false;
            else
                return Convert.ToBoolean(strIn);
        }
        public int MakeSafeInt(object strIn)
        {
            if ((strIn == DBNull.Value))
                return 0;
            else
                return Convert.ToInt32(strIn);
        }
        public decimal? MakeSafeDecimal(object strIn)
        {
            if ((strIn == DBNull.Value))
                return null;
            else
                return Convert.ToDecimal(strIn);
        }
        public double? MakeSafeDouble(object strIn)
        {
            if ((strIn == DBNull.Value))
                return null;
            else
                return Convert.ToDouble(strIn);
        }
        public DateTime? MakeSafeDate(object strIn)
        {
            if ((strIn == DBNull.Value))
                return null;
            else
                return Convert.ToDateTime(strIn);
        }
        public int? MakeSafeNullableInt(object strIn)
        {
            if ((strIn == DBNull.Value))
                return null;
            else
                return Convert.ToInt32(strIn);
        }
    }
}
