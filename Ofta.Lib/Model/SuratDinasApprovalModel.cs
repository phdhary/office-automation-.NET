using Ofta.Lib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Model
{
    public class SuratDinasApprovalModel : IArchaModel,
        ISuratDinasKey, IPegKey, IApprovalTypeKey
    {
        public string SuratDinasID { get; set; }
        public string PegID { get; set; }
        public string PegName { get; set; }
        public string ApprovalTypeID { get; set; }  
        public string ApprovalTypeName { get; set; }
    }
}
