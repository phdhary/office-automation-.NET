using Ofta.Lib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Model
{
    public interface IJenisCutiKey
    {
        string JenisCutiID { get; set; }
    }

    public class JenisCutiModel : IArchaModel,
        IJenisCutiKey
    {
        public string JenisCutiID { get; set; }
        public string JenisCutiName { get; set; }
    }
}
