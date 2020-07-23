using Ofta.Lib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Model
{
    public interface IPegKey
    {
        string PegID { get; set; }
    }

    public class PegModel : IArchaModel,
        IPegKey, IJabatanKey
    {
        public PegModel() { }
        public PegModel(string id) => PegID = id;

        public string PegID { get; set; }
        public string PegName { get; set; }
        public string JabatanID { get; set; }
        public string JabatanName { get; set; }

    }
}
