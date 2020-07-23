using Ofta.Lib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Model
{
    public interface IRSKey
    {
        string RSID { get; set; }
    }

    public class RSModel : IArchaModel,
        IRSKey, IKotaKey
    {
        public string RSID { get; set; }
        public string RSName { get; set; }
        public string KotaID { get; set; }
        public string KotaName { get; set; }
    }
}
