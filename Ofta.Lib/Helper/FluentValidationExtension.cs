using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Helper
{
    public interface IArchaModel { }

    public static class ArchaValidatorExtension
    {
        //  empty
        public static bool Empty(this IArchaModel emVal)
        {
            if (emVal is null)
                return true;
            return false;
        }
        public static bool NotEmpty(this IArchaModel emVal)
        {
            if (emVal is null)
                return false;
            return true;
        }

        public static bool Empty(this string strVal)
        {
            if (strVal is null)
                return true;

            if (strVal.Trim().Length == 0)
                return true;

            return false;
        }


        //  datetime
        public static bool LessThan(this DateTime date1, DateTime date2)
        {
            if (date1 < date2)
                return true;
            return false;
        }

        //  numerik
        public static bool LessOrEqual(this long val1, long val2)
        {
            if (val1 <= val2)
                return true;
            return false;
        }
        public static bool LessOrEqual(this decimal val1, long val2)
        {
            if (val1 <= val2)
                return true;
            return false;
        }
        public static bool GreaterThan(this int val1, int val2)
        {
            if (val1 > val2)
                return true;
            return false;
        }

        public static void Throw(this bool val, string msg)
        {
            if (val)
                throw new ArgumentException(msg);
        }

        public static bool LengthOver(this string strVal, int maxLength)
        {
            if (strVal.Trim().Length > maxLength)
                return true;
            return false;
        }

        public static bool Empty<T>(this IEnumerable<T> listData)
        {
            if (listData is null)
                return true;

            if (listData.Count() == 0)
                return true;

            return false;
        }
    }
}
