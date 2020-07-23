using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Helper
{
    public static class SqlCommandHelper
    {
        public static void AddParam(this SqlCommand cmd, string param, object value, SqlDbType type)
        {
            var p = new SqlParameter
            {
                ParameterName = param,
                Value = value,
                SqlDbType = type
            };
            cmd.Parameters.Add(p);
        }
    }
}
