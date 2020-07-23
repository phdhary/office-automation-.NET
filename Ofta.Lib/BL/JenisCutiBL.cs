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
    public interface IJenisCutiBL
    {
        JenisCutiModel Add(JenisCutiModel jenisCuti);
        JenisCutiModel Update(JenisCutiModel jenisCuti);
        void Delete(IJenisCutiKey jenisCuti);
        JenisCutiModel GetData(IJenisCutiKey jenisCuti);
        IEnumerable<JenisCutiModel> ListData();
    }

    public class JenisCutiBL : IJenisCutiBL
    {
        private IJenisCutiDal _jenisCutiDal;

        public JenisCutiBL(IJenisCutiDal jenisCutiDal)
        {
            _jenisCutiDal = jenisCutiDal;
        }

        private JenisCutiModel Validate(JenisCutiModel jenisCuti)
        {
            jenisCuti.Empty().Throw("JENIS CUTI kosong");
            jenisCuti.JenisCutiID.Empty().Throw("JENIS CUTI ID invalid");
            jenisCuti.JenisCutiID.Length.GreaterThan(3).Throw("JENIS CUTI ID max length is 3");
            jenisCuti.JenisCutiName.Empty().Throw("JENIS CUTI NAME invalid");
            jenisCuti.JenisCutiName.Length.GreaterThan(20).Throw("JENIS CUTI NAME max length is 20");

            return jenisCuti;
        }

        public JenisCutiModel Add(JenisCutiModel jenisCuti)
        {
            //      INPUT VALIDATION
            var kt = Validate(jenisCuti);

            //      BUSINESS VALIDATION
            var jenisCutiDb = _jenisCutiDal.GetData(kt);
            jenisCutiDb.NotEmpty().Throw("JENIS CUTI ID already exist");

            //      REPO-OP
            _jenisCutiDal.Insert(kt);

            //      RETURN
            return kt;
        }

        public JenisCutiModel Update(JenisCutiModel jenisCuti)
        {
            //      INPUT VALIDATION
            var kt = Validate(jenisCuti);

            //      BUSINESS VALIDATION
            var jenisCutiDb = _jenisCutiDal.GetData(kt);
            jenisCutiDb.Empty().Throw("JENIS CUTI ID not found");

            //      REPO-OP
            _jenisCutiDal.Update(kt);

            //      RETURN
            return kt;
        }

        public void Delete(IJenisCutiKey key)
        {
            //      INPUT VALIDATION
            if (key is null)
                throw new ArgumentException("JENIS CUTI ID empty");

            //      REPO-OP
            _jenisCutiDal.Delete(key);
        }

        public JenisCutiModel GetData(IJenisCutiKey key)
        {
            //      INPUT VALIDATION
            if (key is null)
                throw new ArgumentException("JENIS CUTI ID empty");

            //      REPO-OP
            var result = _jenisCutiDal.GetData(key);

            //      RETURN
            return result;
        }

        public IEnumerable<JenisCutiModel> ListData()
        {
            //      REPO-OP
            var result = _jenisCutiDal.ListData();

            //      RETURN
            return result;
        }
    }
}
