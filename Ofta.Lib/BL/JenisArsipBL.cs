using Ofta.Lib.Dal;
using Ofta.Lib.Helper;
using Ofta.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.BL
{
    public interface IJenisArsipBL
    {
        JenisArsipModel Add(JenisArsipModel jenisArsip);
        JenisArsipModel Update(JenisArsipModel jenisArsip);
        void Delete(IJenisArsipKey jenisArsip);
        JenisArsipModel GetData(IJenisArsipKey jenisArsip);
        IEnumerable<JenisArsipModel> ListData();
    }

    public class JenisArsipBL : IJenisArsipBL
    {
        private IJenisArsipDal _jenisArsipDal;

        public JenisArsipBL(IJenisArsipDal jenisArsipDal)
        {
            _jenisArsipDal = jenisArsipDal;
        }

        private JenisArsipModel Validate(JenisArsipModel jenisArsip)
        {
            jenisArsip.Empty().Throw("JENIS ARSIP kosong");
            jenisArsip.JenisArsipID.Empty().Throw("JENIS ARSIP ID invalid");
            jenisArsip.JenisArsipID.Length.GreaterThan(3).Throw("JENIS ARSIP ID max length is 3");
            jenisArsip.JenisArsipName.Empty().Throw("JENIS ARSIP NAME invalid");
            jenisArsip.JenisArsipName.Length.GreaterThan(20).Throw("JENIS ARSIP NAME max length is 20");

            return jenisArsip;
        }

        public JenisArsipModel Add(JenisArsipModel jenisArsip)
        {
            //      INPUT VALIDATION
            var kt = Validate(jenisArsip);

            //      BUSINESS VALIDATION
            var jenisArsipDb = _jenisArsipDal.GetData(kt);
            jenisArsipDb.NotEmpty().Throw("JENIS ARSIP ID already exist");

            //      REPO-OP
            _jenisArsipDal.Insert(kt);

            //      RETURN
            return kt;
        }

        public JenisArsipModel Update(JenisArsipModel jenisArsip)
        {
            //      INPUT VALIDATION
            var kt = Validate(jenisArsip);

            //      BUSINESS VALIDATION
            var jenisArsipDb = _jenisArsipDal.GetData(kt);
            jenisArsipDb.Empty().Throw("JENIS ARSIP ID not found");

            //      REPO-OP
            _jenisArsipDal.Update(kt);

            //      RETURN
            return kt;
        }

        public void Delete(IJenisArsipKey key)
        {
            //      INPUT VALIDATION
            if (key is null)
                throw new ArgumentException("JENIS ARSIP ID empty");

            //      REPO-OP
            _jenisArsipDal.Delete(key);
        }

        public JenisArsipModel GetData(IJenisArsipKey key)
        {
            //      INPUT VALIDATION
            if (key is null)
                throw new ArgumentException("JENIS ARSIP ID empty");

            //      REPO-OP
            var result = _jenisArsipDal.GetData(key);

            //      RETURN
            return result;
        }

        public IEnumerable<JenisArsipModel> ListData()
        {
            //      REPO-OP
            var result = _jenisArsipDal.ListData();

            //      RETURN
            return result;
        }
    }
}
