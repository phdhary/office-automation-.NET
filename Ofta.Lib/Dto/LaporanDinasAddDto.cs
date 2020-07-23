using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Dto
{
    public class LaporanDinasAddDto
    {
        public string PegID { get; set; }
        public string SuratDinasID { get; set; }
        public DateTime TglMulai { get; set; }
        public DateTime TglSelesai { get; set; }
        public string HasilKerja { get; set; }
        public long KMAkhir { get; set; }
    }
}
