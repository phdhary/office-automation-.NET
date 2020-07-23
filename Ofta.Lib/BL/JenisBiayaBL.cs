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
    public interface IJenisBiayaBL
    {
        JenisBiayaModel Add(JenisBiayaModel jenisBiaya);
        JenisBiayaModel Update(JenisBiayaModel jenisBiaya);
        void Delete(IJenisBiayaKey jenisBiaya);
        JenisBiayaModel GetData(IJenisBiayaKey jenisBiaya);
        IEnumerable<JenisBiayaModel> ListData();
    }

    public class JenisBiayaBL : IJenisBiayaBL
    {
        private IJenisBiayaDal _jenisBiayaDal;

        public JenisBiayaBL(IJenisBiayaDal jenisBiayaDal)
        {
            _jenisBiayaDal = jenisBiayaDal;
        }

        private JenisBiayaModel Validate(JenisBiayaModel jenisBiaya)
        {
            jenisBiaya.Empty().Throw("JENIS BIAYA kosong");
            jenisBiaya.JenisBiayaID.Empty().Throw("JENIS BIAYA ID invalid");
            jenisBiaya.JenisBiayaID.Length.GreaterThan(3).Throw("JENIS BIAYA ID max length is 3");
            jenisBiaya.JenisBiayaName.Empty().Throw("JENIS BIAYA NAME invalid");
            jenisBiaya.JenisBiayaName.Length.GreaterThan(20).Throw("JENIS BIAYA NAME max length is 20");

            return jenisBiaya;
        }

        public JenisBiayaModel Add(JenisBiayaModel jenisBiaya)
        {
            //      INPUT VALIDATION
            var kt = Validate(jenisBiaya);

            //      BUSINESS VALIDATION
            var jenisBiayaDb = _jenisBiayaDal.GetData(kt);
            jenisBiayaDb.NotEmpty().Throw("JENIS BIAYA ID already exist");

            //      REPO-OP
            _jenisBiayaDal.Insert(kt);

            //      RETURN
            return kt;
        }

        public JenisBiayaModel Update(JenisBiayaModel jenisBiaya)
        {
            //      INPUT VALIDATION
            var kt = Validate(jenisBiaya);

            //      BUSINESS VALIDATION
            var jenisBiayaDb = _jenisBiayaDal.GetData(kt);
            jenisBiayaDb.Empty().Throw("JENIS BIAYA ID not found");

            //      REPO-OP
            _jenisBiayaDal.Update(kt);

            //      RETURN
            return kt;
        }

        public void Delete(IJenisBiayaKey key)
        {
            //      INPUT VALIDATION
            if (key is null)
                throw new ArgumentException("JENIS BIAYA ID empty");

            //      REPO-OP
            _jenisBiayaDal.Delete(key);
        }

        public JenisBiayaModel GetData(IJenisBiayaKey key)
        {
            //      INPUT VALIDATION
            if (key is null)
                throw new ArgumentException("JENIS BIAYA ID empty");

            //      REPO-OP
            var result = _jenisBiayaDal.GetData(key);

            //      RETURN
            return result;
        }

        public IEnumerable<JenisBiayaModel> ListData()
        {
            //      REPO-OP
            var result = _jenisBiayaDal.ListData();

            //      RETURN
            return result;
        }
    }
}
