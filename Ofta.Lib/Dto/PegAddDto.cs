using Ofta.Lib.Helper;
using Ofta.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Dto
{
    public class PegAddDto : IArchaModel,
        IJabatanKey
    {
        public string PegName { get; set; }
        public string JabatanID { get; set; }
    }
}
