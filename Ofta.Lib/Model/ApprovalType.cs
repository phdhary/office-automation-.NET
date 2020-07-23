using Ofta.Lib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Model
{
    public interface IApprovalTypeKey
    {
        string ApprovalTypeID { get; set; }
    }
    public class ApprovalTypeModel : IArchaModel,
        IApprovalTypeKey
    {
        public string ApprovalTypeID { get; set; }
        public string ApprovalTypeName { get; set; }
    }
}
