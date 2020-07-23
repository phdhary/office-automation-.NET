using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Helper
{
    public static class DateTimeHelper
    {
        public static DateTime ToDate(this string stringTgl)
        {
            DateTime dummyDate;
            //  coba parsing sebagai DMY
            bool isValid = DateTime.TryParseExact(stringTgl, "dd-MM-yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None,
                out dummyDate);

            //  jika tidak berhasil, parsing sebagai YMD
            if (!isValid)
            {
                isValid = DateTime.TryParseExact(stringTgl, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out dummyDate);
            }

            if (isValid)
            {
                return dummyDate;
            }
            else
            {
                throw new InvalidOperationException("Invalid string date");
            }
        }
    }
}
