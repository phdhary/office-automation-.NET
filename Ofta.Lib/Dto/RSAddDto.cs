using Ofta.Lib.Helper;
using Ofta.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Dto
{
    public class RSAddDto : IArchaModel,
        IRSKey, IKotaKey
    {
        public string RSID { get; set; }
        public string RSName { get; set; }
        public string KotaID { get; set; }
    }
}
