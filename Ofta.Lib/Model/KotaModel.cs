using Ofta.Lib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Model
{
    public interface IKotaKey
    {
        string KotaID { get; set; }
    }

    public class KotaModel : IArchaModel,
        IKotaKey
    {
        public string KotaID { get; set; }
        public string KotaName { get; set; }
    }
}
