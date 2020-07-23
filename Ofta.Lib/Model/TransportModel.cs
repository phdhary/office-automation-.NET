using Ofta.Lib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Model
{
    public interface ITransportKey
    {
        string TransportID { get; set; }
    }
    public class TransportModel : IArchaModel,
        ITransportKey
    {
        public string TransportID { get; set; }
        public string TransportName { get; set; }
    }
}
