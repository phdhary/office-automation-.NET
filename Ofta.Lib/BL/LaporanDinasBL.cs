using Ofta.Lib.Dal;
using Ofta.Lib.Dto;
using Ofta.Lib.Helper;
using Ofta.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.BL
{
    public interface ILaporanDinasBL
    {
        LaporanDinasModel Add(LaporanDinasAddDto ld);
        LaporanDinasModel Update(LaporanDinasModel ld);
        void Delete(ILaporanDinasKey key);
        LaporanDinasModel GetData(ILaporanDinasKey key);
        IEnumerable<LaporanDinasModel> ListData(DateTime tgl1, DateTime tgl2);
    }
    public class LaporanDinasBL : ILaporanDinasBL
    {
        private readonly ILaporanDinasDal _laporanDinasDal;
        private readonly IPegDal _pegDal;
        private readonly ITransportDal _transportDal;
        private readonly IParamNoBL _paramNoBL;
        private readonly ISuratDinasDal _suratDinasDal;
        

        private const string PREFIX_LAPORANDINAS_ID = "LD";
        private const string TRANSPORT_ID_OPERASIONAL = "T2";

        public LaporanDinasBL()
        {
            _laporanDinasDal = new LaporanDinasDal();
            _pegDal = new PegDal();
            _transportDal = new TransportDal();
            _paramNoBL = new ParamNoBL();
            _suratDinasDal = new SuratDinasDal();
        }

        public LaporanDinasBL(ILaporanDinasDal laporanDinasDal,
            IPegDal pegDal,
            ITransportDal transportDal,
            IParamNoBL paramNoBL,
            ISuratDinasDal suratDinasDal)
            {
            _pegDal = pegDal;
            _transportDal = transportDal;
            _laporanDinasDal = laporanDinasDal;
            _paramNoBL = paramNoBL;
            _suratDinasDal = suratDinasDal;
        }

        private LaporanDinasModel R01_PegIDHarusVaid(LaporanDinasModel ld)
        {
			var peg = _pegDal.GetData(ld);
			if (peg is null)
				throw new ArgumentException("'PegID' invalid");
			else
				ld.PegName = peg.PegName;
			return ld;
		}

        private LaporanDinasModel R02_SuratDinasIDHarusTerdaftar(LaporanDinasModel ld)
        {

            var suratdinas = _suratDinasDal.GetData(ld);
            if (suratdinas is null)
                throw new ArgumentException("'suratdinas' invalid");
            else
                ld.SuratDinasID = suratdinas.SuratDinasID;
            return ld;
        }

        private LaporanDinasModel R03_PegIDRequestAndReportHarusSama(LaporanDinasModel ld)
        {
            var sd = _suratDinasDal.GetData(ld);
            if (sd.PegID != ld.PegID)
                throw new ArgumentException("PegID Report berbeda dengan Surat Dinas");
            return ld;
		}

        private LaporanDinasModel R04_TglSelesaiSetelahTglMulaiDiSuratDinas(LaporanDinasModel ld)
        {
            if (ld.TglSelesai <= ld.TglMulai)
                throw new ArgumentException("'Tgl Selesai/Mulai' invalid");
            return ld;
        }

        private LaporanDinasModel R05_HasilKerjaTidakBolehKosong(LaporanDinasModel ld)
        {
            if (ld.HasilKerja.Length == 0)
                throw new ArgumentException("'HasilKerja' empty");
            return ld;
        }

        private LaporanDinasModel R06_OpsiKendaraanOperasionalKMAkhirHarusTerisi(LaporanDinasModel ld)
        {
            if (ld.KMAkhir <= 0)
                throw new ArgumentException("'KM Akhir' empty");

            return ld;
        }

        private LaporanDinasModel R07_OtherTransportKMAkhirSet0(LaporanDinasModel ld)
        {
            ld.KMAkhir = 0;
            return ld;
		}

        private LaporanDinasModel R08_DisetujuiAtasanHarusTerdaftarDiDatabase(LaporanDinasModel ld)
        {
            var peg = _pegDal.GetData(ld);
            if (peg is null)
                throw new ArgumentException("PegID Atasan invalid");
            ld.PegName = peg.PegName;
            return ld;
        }

        private LaporanDinasModel R09_IsSignedDiketahuiDisetFalse(LaporanDinasModel ld)
        {
            //ld.IsSignedDiketahui = false;
            return ld;
        }

        public LaporanDinasModel Add(LaporanDinasAddDto laporanDinas)
		{
            //  validate argument



            //  convert DTO >> Model
            var ld = new LaporanDinasModel
            {
                TglJamCreate = DateTime.Now,
                PegID = laporanDinas.PegID,
                SuratDinasID = laporanDinas.SuratDinasID,
                TglMulai = laporanDinas.TglMulai,
                TglSelesai = laporanDinas.TglSelesai,
                HasilKerja = laporanDinas.HasilKerja,
                KMAkhir = laporanDinas.KMAkhir
            };

			if (ld is null)
				throw new ArgumentNullException(nameof(ld));

			ld = R01_PegIDHarusVaid(ld);
			ld = R02_SuratDinasIDHarusTerdaftar(ld);
			ld = R03_PegIDRequestAndReportHarusSama(ld);
			ld = R04_TglSelesaiSetelahTglMulaiDiSuratDinas(ld);
			ld = R05_HasilKerjaTidakBolehKosong(ld);
			ld = R06_OpsiKendaraanOperasionalKMAkhirHarusTerisi(ld);
			ld = R07_OtherTransportKMAkhirSet0(ld);
			ld = R08_DisetujuiAtasanHarusTerdaftarDiDatabase(ld);
			ld = R09_IsSignedDiketahuiDisetFalse(ld);

			//proses simpan
			using (var trans = TransHelper.NewScope())
			{
                //generate id
                ld.LaporanDinasID = _paramNoBL.GenNewID("LD", ParamNoLengthEnum.Code_13);
                //insert ke db
                _laporanDinasDal.Insert(ld);
				//commit changes
				trans.Complete();
			}
			return ld;
		}

		public void Delete(ILaporanDinasKey key)
        {
            var ld = _laporanDinasDal.GetData(key);

            //  proses simpan
            using (var trans = TransHelper.NewScope())
            {
                //  update ke db
                _laporanDinasDal.Delete(ld);
                //  commit changes
                trans.Complete();
            }
        }

        public LaporanDinasModel GetData(ILaporanDinasKey key)
        {
            var result = _laporanDinasDal.GetData(key);
            return result;
        }

        public IEnumerable<LaporanDinasModel> ListData(DateTime tgl1, DateTime tgl2)
        {
            var result = _laporanDinasDal.ListData(tgl1, tgl2);
            return result;
        }

        public LaporanDinasModel Update(LaporanDinasModel ld)
        {
            ld = R01_PegIDHarusVaid(ld);
            ld = R02_SuratDinasIDHarusTerdaftar(ld);
            ld = R03_PegIDRequestAndReportHarusSama(ld);
            ld = R04_TglSelesaiSetelahTglMulaiDiSuratDinas(ld);
            ld = R05_HasilKerjaTidakBolehKosong(ld);
            ld = R06_OpsiKendaraanOperasionalKMAkhirHarusTerisi(ld);
            ld = R07_OtherTransportKMAkhirSet0(ld);
            ld = R08_DisetujuiAtasanHarusTerdaftarDiDatabase(ld);
            ld = R09_IsSignedDiketahuiDisetFalse(ld);

            //  proses simpan
            using (var trans = TransHelper.NewScope())
            {
                //  update ke db
                _laporanDinasDal.Update(ld);
                //  commit changes
                trans.Complete();
            }

            return ld;
        }
    }
}
