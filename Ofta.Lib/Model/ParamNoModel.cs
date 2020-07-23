using Ofta.Lib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Model
{
    public interface IParamNoKey
    {
        string ParamID { get; set; }
    }

    public interface IParamNoModel : IParamNoKey
    {
        string ParamValue { get; set; }
    }

    public class ParamNoModel : IArchaModel,
        IParamNoModel
    {
        public string ParamValue { get; set; }
        public string ParamID { get; set; }
    }
}
