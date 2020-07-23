using Ofta.Lib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Model
{

    public interface IJenisArsipKey
    {
        string JenisArsipID { get; set; }
    }

    public class JenisArsipModel : IArchaModel,
        IJenisArsipKey
    {
        public string JenisArsipID { get; set; }
        public string JenisArsipName { get; set; }
    }
}
