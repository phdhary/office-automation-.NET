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
    public interface IJabatanBL
    {
        JabatanModel Add(JabatanModel jabatan);
        JabatanModel Update(JabatanModel jabatan);
        void Delete(IJabatanKey jabatan);
        JabatanModel GetData(IJabatanKey jabatan);
        IEnumerable<JabatanModel> ListData();
    }

    public class JabatanBL : IJabatanBL
    {
        private IJabatanDal _jabatanDal;

        public JabatanBL(IJabatanDal jabatanDal)
        {
            _jabatanDal = jabatanDal;
        }

        private JabatanModel Validate(JabatanModel jabatan)
        {
            jabatan.Empty().Throw("JABATAN kosong");
            jabatan.JabatanID.Empty().Throw("JABATAN ID invalid");
            jabatan.JabatanID.Length.GreaterThan(3).Throw("JABATAN ID max length is 3");
            jabatan.JabatanName.Empty().Throw("JABATAN NAME invalid");
            jabatan.JabatanName.Length.GreaterThan(20).Throw("JABATAN NAME max length is 20");

            return jabatan;
        }

        public JabatanModel Add(JabatanModel jabatan)
        {
            //      INPUT VALIDATION
            var kt = Validate(jabatan);

            //      BUSINESS VALIDATION
            var jabatanDb = _jabatanDal.GetData(kt);
            jabatanDb.NotEmpty().Throw("JABATAN ID already exist");

            //      REPO-OP
            _jabatanDal.Insert(kt);

            //      RETURN
            return kt;
        }

        public JabatanModel Update(JabatanModel jabatan)
        {
            //      INPUT VALIDATION
            var kt = Validate(jabatan);

            //      BUSINESS VALIDATION
            var jabatanDb = _jabatanDal.GetData(kt);
            jabatanDb.Empty().Throw("JABATAN ID not found");

            //      REPO-OP
            _jabatanDal.Update(kt);

            //      RETURN
            return kt;
        }

        public void Delete(IJabatanKey key)
        {
            //      INPUT VALIDATION
            if (key is null)
                throw new ArgumentException("JABATAN ID empty");

            //      REPO-OP
            _jabatanDal.Delete(key);
        }

        public JabatanModel GetData(IJabatanKey key)
        {
            //      INPUT VALIDATION
            if (key is null)
                throw new ArgumentException("JABATAN ID empty");

            //      REPO-OP
            var result = _jabatanDal.GetData(key);

            //      RETURN
            return result;
        }

        public IEnumerable<JabatanModel> ListData()
        {
            //      REPO-OP
            var result = _jabatanDal.ListData();

            //      RETURN
            return result;
        }
    }
}
