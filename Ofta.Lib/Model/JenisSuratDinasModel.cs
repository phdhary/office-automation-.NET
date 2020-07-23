using Ofta.Lib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Model
{
    public interface IJenisSuratDinasKey
    {
        string JenisSuratDinasID { get; set; }
    }
    
    public class JenisSuratDinasModel : IArchaModel,
        IJenisSuratDinasKey
    {
        public string JenisSuratDinasID { get; set; }
        public string JenisSuratDinasName { get; set; }
    }
}
