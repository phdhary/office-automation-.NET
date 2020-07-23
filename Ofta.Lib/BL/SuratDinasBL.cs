using FluentValidation;
using Ofta.Lib.Dal;
using Ofta.Lib.Dto;
using Ofta.Lib.Helper;
using Ofta.Lib.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.BL
{
    public interface ISuratDinasBL
    {
        SuratDinasModel Propose(SuratDinasAddDto sd);
        SuratDinasModel Revise(SuratDinasModel sd);
        void Void(ISuratDinasKey key);
        SuratDinasModel GetData(ISuratDinasKey key);
        IEnumerable<SuratDinasModel> ListData(DateTime tgl1, DateTime tgl2);
    }
    public class SuratDinasBL : ISuratDinasBL
    {
        private readonly ISuratDinasDal _suratDinasDal;
        private readonly IPegDal _pegDal;
        private readonly ITransportDal _transportDal;
        private readonly IJenisBiayaDal _jenisBiayaDal;
        private readonly IParamNoBL _paramNoBL;
        private readonly IRSDal _rsDal;
        private readonly ILaporanDinasDal _laporanDinasDal;
        private readonly IApprovalTypeDal _approvalTypeDal;
        private readonly ISuratDinasApprovalDal _suratDinasApprovalDal;

        private const string TRANSPORT_ID_OPERASIONAL = "T2";
        private const string JENISBIAYA_ID_KASBON = "J2";
        private const string PREFIX_SURATDINAS_ID = "SD";

        public SuratDinasBL()
        {
            _suratDinasDal = new SuratDinasDal();
            _pegDal = new PegDal();
            _transportDal = new TransportDal();
            _jenisBiayaDal = new JenisBiayaDal();
            _paramNoBL = new ParamNoBL();
            _rsDal = new RSDal();
            _laporanDinasDal = new LaporanDinasDal();
            _approvalTypeDal = new ApprovalTypeDal();
            _suratDinasApprovalDal = new SuratDinasApprovalDal();
        }

        public SuratDinasBL(ISuratDinasDal suratDinasDal,
            IPegDal pegDal,
            ITransportDal transportDal,
            IJenisBiayaDal jenisBiayaDal,
            IParamNoBL paramNoBL,
            IRSDal rsDal,
            ILaporanDinasDal laporanDinasDal,
            IApprovalTypeDal approvalTypeDal,
            ISuratDinasApprovalDal suratDinasApprovalDal)
        {
            _suratDinasDal = suratDinasDal;
            _pegDal = pegDal;
            _transportDal = transportDal;
            _jenisBiayaDal = jenisBiayaDal;
            _paramNoBL = paramNoBL;
            _rsDal = rsDal;
            _laporanDinasDal = laporanDinasDal;
            _approvalTypeDal = approvalTypeDal;
            _suratDinasApprovalDal = suratDinasApprovalDal;
        }

        private SuratDinasModel Validate(SuratDinasModel sd)
        {
            //  mandatory check
            sd.JenisBiayaID.Empty().Throw("JENIS BIAYA kosong");
            sd.PegID.Empty().Throw("PEGAWAI kosong");
            sd.RSID.Empty().Throw("RS TUJUAN kosong");
            sd.TransportID.Empty().Throw("TRANSPORT kosong");
            sd.Keperluan.Empty().Throw("KEPERLUAN kosong");

            //  pegawai
            var peg = _pegDal.GetData(sd);
            peg.Empty().Throw("PEGAWAI invalid");
            sd.PegName = peg.PegName;

            //  transport
            var transport = _transportDal.GetData(sd);
            transport.Empty().Throw("TRANSPORT invalid");
            sd.TransportName = transport.TransportName;

            //  jenis biaya
            var jb = _jenisBiayaDal.GetData(sd);
            jb.Empty().Throw("JENIS BIAYA invalid");
            sd.JenisBiayaName = jb.JenisBiayaName;

            //  rumah sakit
            var rs = _rsDal.GetData(sd);
            rs.Empty().Throw("RS TUJUAN invalid");
            sd.RSName = rs.RSName;

            //  urutan tgl mulai - selesai
            sd.TglSelesai.LessThan(sd.TglMulai).Throw("Tgl Mulasi / Selesai invalid");

            //  km awal => terisi jika mobil kantor, kosong jika sebaliknya
            if (sd.TransportID == TRANSPORT_ID_OPERASIONAL)
                sd.KMAwal.LessOrEqual(0).Throw("KM Awal kosong");

            if (sd.TransportID != TRANSPORT_ID_OPERASIONAL)
                sd.KMAwal = 0;

            //  nilai kas bon => terisi jika kasbon, kosongkan jika bukan kasbon
            if (sd.JenisBiayaID == JENISBIAYA_ID_KASBON)
                sd.KasBon.LessOrEqual(0).Throw("Nilai KasBon kosong");
            if (sd.JenisBiayaID != JENISBIAYA_ID_KASBON)
                sd.KasBon = 0;

            //  approval
            sd.ListApproval.Empty().Throw("List Approval empty");
            var listApproval = new List<SuratDinasApprovalModel>();
            foreach(var item in sd.ListApproval)
            {
                peg = _pegDal.GetData(item);
                peg.Empty().Throw("PegID Approval invalid");
                item.PegName = peg.PegName;

                var aprvType = _approvalTypeDal.GetData(item);
                aprvType.Empty().Throw("Approval Type invalid");
                item.ApprovalTypeName = aprvType.ApprovalTypeName;
                var newItem = new SuratDinasApprovalModel
                {
                    PegID = item.PegID,
                    PegName = peg.PegName,
                    ApprovalTypeID = item.ApprovalTypeID,
                    ApprovalTypeName = item.ApprovalTypeName,
                };
                listApproval.Add(newItem);
            }
            sd.ListApproval = listApproval;

            return sd;
        }

        public SuratDinasModel Propose(SuratDinasAddDto suratDinas)
        {
            //  CHECK ARGUMENT
            suratDinas.Empty().Throw("Data Surat Dinas kosong");

            //  CONVERT DTO >> MODEL
            var sd = new SuratDinasModel
            {
                TglJamCreate = DateTime.Now,
                PegID = suratDinas.PegID,
                NoSurat = suratDinas.NoSurat,
                NoKontrak = suratDinas.NoKontrak,
                TglMulai = suratDinas.TglMulai,
                TglSelesai = suratDinas.TglSelesai,
                Keperluan = suratDinas.Keperluan,
                TransportID = suratDinas.TransportID,
                KMAwal = suratDinas.KMAwal,
                RSID = suratDinas.RSID,
                JenisBiayaID = suratDinas.JenisBiayaID,
                KasBon = suratDinas.KasBon,
                ListApproval = 
                    from c in suratDinas.ListApproval
                    select new SuratDinasApprovalModel
                    {
                        PegID = c.PegID,
                        ApprovalTypeID = c.ApprovalTypeID,
                    }
            };

            //  VALIDATE MODEL
            Validate(sd);

            //  UPDATE REPO
            using (var trans = TransHelper.NewScope())
            {
                //  generate id
                sd.SuratDinasID = _paramNoBL.GenNewID("LG", ParamNoLengthEnum.Code_13);
                foreach (var item in sd.ListApproval)
                    item.SuratDinasID = sd.SuratDinasID;

                //  insert ke db
                _suratDinasDal.Insert(sd);
                _suratDinasApprovalDal.Delete(sd);
                foreach (var item in sd.ListApproval)
                    _suratDinasApprovalDal.Insert(item);

                //  commit changes
                trans.Complete();
            }

            return sd;
        }

        public SuratDinasModel Revise(SuratDinasModel sd)
        {
            //  jika sudah dibuatkan laporan, tidak boleh update data surat dinas
            //sd = R14_SuratDinasTerlaporTidakBolehUpdate(sd);
            
            Validate(sd);

            //  proses simpan
            using (var trans = TransHelper.NewScope())
            {
                //  update ke db
                _suratDinasDal.Update(sd);
                //  commit changes
                trans.Complete();
            }

            return sd;
        }

        public void Void(ISuratDinasKey key)
        {
            var sd = _suratDinasDal.GetData(key);

            //  jika sudah dibuatkan laporan, tidak boleh update data surat dinas
            //sd = R14_SuratDinasTerlaporTidakBolehUpdate(sd);

            //  proses simpan
            using (var trans = TransHelper.NewScope())
            {
                //  update ke db
                _suratDinasDal.Delete(sd);
                //  commit changes
                trans.Complete();
            }
        }

        public SuratDinasModel GetData(ISuratDinasKey key)
        {
            //  get header
            var result = _suratDinasDal.GetData(key);
            if (result is null)
                return null;

            //  get detil
            var listApproval = _suratDinasApprovalDal.ListData(key);
            result.ListApproval = listApproval;

            return result;
        }

        public IEnumerable<SuratDinasModel> ListData(DateTime tgl1, DateTime tgl2)
        {
            var result = _suratDinasDal.ListData(tgl1, tgl2);
            return result;
        }
    }
}
