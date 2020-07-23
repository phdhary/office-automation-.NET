using Ofta.Lib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Model
{
    public interface ISuratDinasKey
    {
        string SuratDinasID { get; set; }
    }

    public class SuratDinasModel : IArchaModel,
        ISuratDinasKey, IRSKey, 
        IJenisBiayaKey,ITransportKey, 
        IPegKey
    {
        public SuratDinasModel() { }
        public SuratDinasModel(string id) => SuratDinasID = id;

        public string SuratDinasID { get; set; }
        public DateTime TglJamCreate { get; set; }

        public string PegID { get; set; }
        public string PegName { get; set; }
        public string NoSurat { get; set; }
        public string NoKontrak { get; set; }
        
        public DateTime TglMulai { get; set; }
        public DateTime TglSelesai { get; set; }
        public string Keperluan { get; set; }
        
        public string TransportID { get; set; }
        public string TransportName { get; set; }
        public long KMAwal { get; set; }
        public string RSID { get; set; }
        public string RSName { get; set; }
        
        public string JenisBiayaID { get; set; }
        public string JenisBiayaName { get; set; }
        public decimal KasBon { get; set; }

        public IEnumerable<SuratDinasApprovalModel> ListApproval { get; set; }

    }
}
