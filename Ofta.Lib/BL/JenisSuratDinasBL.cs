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
    public interface IJenisSuratDinasBL
    {
        JenisSuratDinasModel Add(JenisSuratDinasModel jenisSuratDinas);
        JenisSuratDinasModel Update(JenisSuratDinasModel jenisSuratDinas);
        void Delete(IJenisSuratDinasKey jenisSuratDinas);
        JenisSuratDinasModel GetData(IJenisSuratDinasKey jenisSuratDinas);
        IEnumerable<JenisSuratDinasModel> ListData();
    }

    public class JenisSuratDinasBL : IJenisSuratDinasBL
    {
        private IJenisSuratDinasDal _jenisSuratDinasDal;

        public JenisSuratDinasBL(IJenisSuratDinasDal jenisSuratDinasDal)
        {
            _jenisSuratDinasDal = jenisSuratDinasDal;
        }

        private JenisSuratDinasModel Validate(JenisSuratDinasModel jenisSuratDinas)
        {
            jenisSuratDinas.Empty().Throw("JENIS SURAT DINAS kosong");
            jenisSuratDinas.JenisSuratDinasID.Empty().Throw("JENIS SURAT DINAS ID invalid");
            jenisSuratDinas.JenisSuratDinasID.Length.GreaterThan(3).Throw("JENIS SURAT DINAS ID max length is 3");
            jenisSuratDinas.JenisSuratDinasName.Empty().Throw("JENIS SURAT DINAS NAME invalid");
            jenisSuratDinas.JenisSuratDinasName.Length.GreaterThan(20).Throw("JENIS SURAT DINAS NAME max length is 20");

            return jenisSuratDinas;
        }

        public JenisSuratDinasModel Add(JenisSuratDinasModel jenisSuratDinas)
        {
            //      INPUT VALIDATION
            var kt = Validate(jenisSuratDinas);

            //      BUSINESS VALIDATION
            var jenisSuratDinasDb = _jenisSuratDinasDal.GetData(kt);
            jenisSuratDinasDb.NotEmpty().Throw("JENIS SURAT DINAS ID already exist");

            //      REPO-OP
            _jenisSuratDinasDal.Insert(kt);

            //      RETURN
            return kt;
        }

        public JenisSuratDinasModel Update(JenisSuratDinasModel jenisSuratDinas)
        {
            //      INPUT VALIDATION
            var kt = Validate(jenisSuratDinas);

            //      BUSINESS VALIDATION
            var jenisSuratDinasDb = _jenisSuratDinasDal.GetData(kt);
            jenisSuratDinasDb.Empty().Throw("JENIS SURAT DINAS ID not found");

            //      REPO-OP
            _jenisSuratDinasDal.Update(kt);

            //      RETURN
            return kt;
        }

        public void Delete(IJenisSuratDinasKey key)
        {
            //      INPUT VALIDATION
            if (key is null)
                throw new ArgumentException("JENIS SURAT DINAS ID empty");

            //      REPO-OP
            _jenisSuratDinasDal.Delete(key);
        }

        public JenisSuratDinasModel GetData(IJenisSuratDinasKey key)
        {
            //      INPUT VALIDATION
            if (key is null)
                throw new ArgumentException("JENIS SURAT DINAS ID empty");

            //      REPO-OP
            var result = _jenisSuratDinasDal.GetData(key);

            //      RETURN
            return result;
        }

        public IEnumerable<JenisSuratDinasModel> ListData()
        {
            //      REPO-OP
            var result = _jenisSuratDinasDal.ListData();

            //      RETURN
            return result;
        }
    }

}
