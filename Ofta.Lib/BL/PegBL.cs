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
    public interface IPegBL
    {
        PegModel Add(PegAddDto peg);
        PegModel Update(PegModel peg);
        void Delete(IPegKey peg);
        PegModel GetData(IPegKey peg);
        IEnumerable<PegModel> ListData();
    }

    public class PegBL : IPegBL
    {
        private readonly IPegDal _pegDal;
        private readonly IParamNoBL _paramNoBL;
        private readonly IJabatanDal _jabatanDal;
        public PegBL(IPegDal pegDal,
            IParamNoBL paramNoBL,
            IJabatanDal jabatanDal)
        {
            _pegDal = pegDal;
            _paramNoBL = paramNoBL;
            _jabatanDal = jabatanDal;
        }

        private PegModel Validate(PegModel peg)
        {
            peg.Empty().Throw("PEGAWAI kosong");
            peg.PegName.Empty().Throw("PEGAWAI NAME invalid");
            peg.PegName.Length.GreaterThan(20).Throw("PEGAWAI NAME max length is 20");
            peg.JabatanID.Empty().Throw("JABATAN empty");

            return peg;
        }

        public PegModel Add(PegAddDto peg)
        {
            //      INPUT VALIDATION
            peg.Empty().Throw("DATA PEGAWAI empty");

            //      CONVERT DTO >> MODEL
            var pg = new PegModel
            {
                PegName = peg.PegName,
                JabatanID = peg.JabatanID
            };
            pg = Validate(pg);

            //      BUSINESS VALIDATION
            var listPeg = _pegDal.ListData();
            if (listPeg != null)
            {
                var exist = listPeg.FirstOrDefault(x => x
                    .PegName.Trim().ToLower() == peg.PegName.Trim().ToLower());
                exist.NotEmpty().Throw("Pegawai already exist");
            }

            var jbtn = _jabatanDal.GetData(pg);
            jbtn.Empty().Throw("JABATAN ID invalid");
            pg.JabatanName = jbtn.JabatanName;

            //      REPO-OP
            using (var trans = TransHelper.NewScope())
            {
                pg.PegID = _paramNoBL.GenNewID("PG", ParamNoLengthEnum.Code_5);
                _pegDal.Insert(pg);
                trans.Complete();
            }

            //      RETURN
            return pg;
        }

        public PegModel Update(PegModel peg)
        {
            //      INPUT VALIDATION
            var pg = Validate(peg);

            //      BUSINESS VALIDATION
            var pegDb = _pegDal.GetData(pg);
            pegDb.Empty().Throw("PEGAWAI ID not found");

            var jbtn = _jabatanDal.GetData(pg);
            jbtn.Empty().Throw("JABATAN ID invalid");
            pg.JabatanName = jbtn.JabatanName;


            //      REPO-OP
            _pegDal.Update(pg);

            //      RETURN
            return pg;
        }

        public void Delete(IPegKey key)
        {
            //      INPUT VALIDATION
            if (key is null)
                throw new ArgumentException("PEGAWAI ID empty");

            //      REPO-OP
            _pegDal.Delete(key);
        }

        public PegModel GetData(IPegKey key)
        {
            //      INPUT VALIDATION
            if (key is null)
                throw new ArgumentException("PEGAWAI ID empty");

            //      REPO-OP
            var result = _pegDal.GetData(key);

            //      RETURN
            return result;
        }

        public IEnumerable<PegModel> ListData()
        {
            //      REPO-OP
            var result = _pegDal.ListData();

            //      RETURN
            return result;
        }
    }
}
