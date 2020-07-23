using Ofta.Lib.Dal;
using Ofta.Lib.Model;
using Ofta.Lib.Helper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.BL
{
    public interface IKotaBL
    {
        KotaModel Add(KotaModel kota);
        KotaModel Update(KotaModel kota);
        void Delete(IKotaKey key);
        KotaModel GetData(IKotaKey key);
        IEnumerable<KotaModel> ListData();
    }
    public class KotaBL : IKotaBL
    {
        private IKotaDal _kotaDal;

        public KotaBL(IKotaDal kotaDal)
        {
            _kotaDal = kotaDal;
        }

        private KotaModel Validate(KotaModel kota)
        {
            kota.Empty().Throw("KOTA kosong");
            kota.KotaID.Empty().Throw("KOTA ID invalid");
            kota.KotaID.Length.GreaterThan(3).Throw("KOTA ID max length is 3");
            kota.KotaName.Empty().Throw("KOTA NAME invalid");
            kota.KotaName.Length.GreaterThan(20).Throw("KOTA NAME max length is 20");

            return kota;
        }

        public KotaModel Add(KotaModel kota)
        {
            //      INPUT VALIDATION
            var kt = Validate(kota);

            //      BUSINESS VALIDATION
            var kotaDb = _kotaDal.GetData(kt);
            kotaDb.NotEmpty().Throw("KOTA ID already exist");

            //      REPO-OP
            _kotaDal.Insert(kt);

            //      RETURN
            return kt;
        }

        public KotaModel Update(KotaModel kota)
        {
            //      INPUT VALIDATION
            var kt = Validate(kota);

            //      BUSINESS VALIDATION
            var kotaDb = _kotaDal.GetData(kt);
            kotaDb.Empty().Throw("KOTA ID not found");

            //      REPO-OP
            _kotaDal.Update(kt);

            //      RETURN
            return kt;
        }

        public void Delete(IKotaKey key)
        {
            //      INPUT VALIDATION
            if (key is null)
                throw new ArgumentException("KOTA ID empty");

            //      REPO-OP
            _kotaDal.Delete(key);
        }

        public KotaModel GetData(IKotaKey key)
        {
            //      INPUT VALIDATION
            if (key is null)
                throw new ArgumentException("KOTA ID empty");

            //      REPO-OP
            var result = _kotaDal.GetData(key);

            //      RETURN
            return result;
        }

        public IEnumerable<KotaModel> ListData()
        {
            //      REPO-OP
            var result = _kotaDal.ListData();

            //      RETURN
            return result;
        }
    }
}
