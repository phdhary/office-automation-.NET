using Ofta.Lib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Model
{
    public interface IJabatanKey
    {
        string JabatanID { get; set; }
    }
    public class JabatanModel : IArchaModel,
        IJabatanKey
    {
        public string JabatanID { get; set; }
        public string JabatanName { get; set; }
    }
}
