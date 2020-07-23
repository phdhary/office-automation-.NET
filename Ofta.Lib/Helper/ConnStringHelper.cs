using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Helper
{
    public static class ConnStringHelper
    {
        public static string Get()
        {
            var result = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return result;
        }
    }
}
