using Ofta.Lib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Model
{
    public interface IJenisKontrakKey
    {
        string JenisKontrakID { get; set; }
    }

    public class JenisKontrakModel : IArchaModel,
        IJenisKontrakKey
    {
        public string JenisKontrakID { get; set; }
        public string JenisKontrakName { get; set; }
    }
}
