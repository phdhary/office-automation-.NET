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
    public interface IJenisKontrakBL
    {
        JenisKontrakModel Add(JenisKontrakModel jenisKontrak);
        JenisKontrakModel Update(JenisKontrakModel jenisKontrak);
        void Delete(IJenisKontrakKey jenisKontrak);
        JenisKontrakModel GetData(IJenisKontrakKey jenisKontrak);
        IEnumerable<JenisKontrakModel> ListData();
    }

    public class JenisKontrakBL : IJenisKontrakBL
    {
        private IJenisKontrakDal _jenisKontrakDal;

        public JenisKontrakBL(IJenisKontrakDal jenisKontrakDal)
        {
            _jenisKontrakDal = jenisKontrakDal;
        }

        private JenisKontrakModel Validate(JenisKontrakModel jenisKontrak)
        {
            jenisKontrak.Empty().Throw("JENIS KONTRAK kosong");
            jenisKontrak.JenisKontrakID.Empty().Throw("JENIS KONTRAK ID invalid");
            jenisKontrak.JenisKontrakID.Length.GreaterThan(3).Throw("JENIS KONTRAK ID max length is 2");
            jenisKontrak.JenisKontrakName.Empty().Throw("JENIS KONTRAK NAME invalid");
            jenisKontrak.JenisKontrakName.Length.GreaterThan(20).Throw("JENIS KONTRAK NAME max length is 20");

            return jenisKontrak;
        }

        public JenisKontrakModel Add(JenisKontrakModel jenisKontrak)
        {
            //      INPUT VALIDATION
            var kt = Validate(jenisKontrak);

            //      BUSINESS VALIDATION
            var jenisKontrakDb = _jenisKontrakDal.GetData(kt);
            jenisKontrakDb.NotEmpty().Throw("JENIS KONTRAK ID already exist");

            //      REPO-OP
            _jenisKontrakDal.Insert(kt);

            //      RETURN
            return kt;
        }

        public JenisKontrakModel Update(JenisKontrakModel jenisKontrak)
        {
            //      INPUT VALIDATION
            var kt = Validate(jenisKontrak);

            //      BUSINESS VALIDATION
            var jenisKontrakDb = _jenisKontrakDal.GetData(kt);
            jenisKontrakDb.Empty().Throw("JENIS KONTRAK ID not found");

            //      REPO-OP
            _jenisKontrakDal.Update(kt);

            //      RETURN
            return kt;
        }

        public void Delete(IJenisKontrakKey key)
        {
            //      INPUT VALIDATION
            if (key is null)
                throw new ArgumentException("JENIS KONTRAK ID empty");

            //      REPO-OP
            _jenisKontrakDal.Delete(key);
        }

        public JenisKontrakModel GetData(IJenisKontrakKey key)
        {
            //      INPUT VALIDATION
            if (key is null)
                throw new ArgumentException("JENIS KONTRAK ID empty");

            //      REPO-OP
            var result = _jenisKontrakDal.GetData(key);

            //      RETURN
            return result;
        }

        public IEnumerable<JenisKontrakModel> ListData()
        {
            //      REPO-OP
            var result = _jenisKontrakDal.ListData();

            //      RETURN
            return result;
        }
    }
}
