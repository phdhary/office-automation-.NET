using Ofta.Lib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Model
{
    public interface ILaporanDinasKey
    {
        string LaporanDinasID { get; set; }
    }
    public class LaporanDinasModel : IArchaModel,
        ILaporanDinasKey, ISuratDinasKey, IPegKey
    {
        public LaporanDinasModel() { }
        public LaporanDinasModel(string id) => LaporanDinasID = id;
        
        public string LaporanDinasID { get; set; }
        public DateTime TglJamCreate { get; set; }
        public string PegID { get; set; }
        public string PegName { get; set; }
        public string SuratDinasID { get; set; }
		public DateTime TglMulai { get; set; }
		public DateTime TglSelesai { get; set; }
        public string HasilKerja { get; set; }
        public long KMAkhir { get; set; }
    }
}
