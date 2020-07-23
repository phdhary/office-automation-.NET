using Ofta.Lib.Helper;
using Ofta.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Dto
{
    public class SuratDinasAddDto : IArchaModel,
        IPegKey, ITransportKey, IJenisBiayaKey
    {
        public string PegID { get; set; }
        public string NoSurat { get; set; }
        public string NoKontrak { get; set; }

        public DateTime TglMulai { get; set; }
        public DateTime TglSelesai { get; set; }
        public string Keperluan { get; set; }

        public string TransportID { get; set; }
        public long KMAwal { get; set; }
        public string RSID { get; set; }

        public string JenisBiayaID { get; set; }
        public decimal KasBon { get; set; }

        public IEnumerable<SuratDinasAddApprovalModel> ListApproval { get; set; }
    }

    public class SuratDinasAddApprovalModel :
        IPegKey, IApprovalTypeKey
    {
        public string PegID { get; set; }
        public string ApprovalTypeID { get; set; }
    }
}
