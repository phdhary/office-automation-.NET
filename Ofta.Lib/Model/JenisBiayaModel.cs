using Ofta.Lib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Model
{
    public interface IJenisBiayaKey
    {
        string JenisBiayaID { get; set; }
    }
    public class JenisBiayaModel : IArchaModel,
        IJenisBiayaKey
    {
        public string JenisBiayaID { get; set; }
        public string JenisBiayaName { get; set; }
    }
}
