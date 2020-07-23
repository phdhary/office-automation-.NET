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
    public interface IRSBL
    {
        RSModel Save(RSAddDto rs);
        void Delete(IRSKey rs);
        RSModel GetData(IRSKey rs);
        IEnumerable<RSModel> ListData();
    }

    public class RSBL : IRSBL
    {
        private IRSDal _rsDal;
        private readonly IKotaDal _kotaDal;

        public RSBL(IRSDal rsDal,
            IKotaDal kotaDal)
        {
            _rsDal = rsDal;
            _kotaDal = kotaDal;
        }

        private void Validate(RSAddDto rs)
        {
        }

        public RSModel Save(RSAddDto rs)
        {
            //      INPUT VALIDATION
            rs.Empty().Throw("DATA RUMAH SAKIT empty");
            rs.Empty().Throw("RUMAH SAKIT kosong");
            rs.RSID.Empty().Throw("RUMAH SAKIT ID invalid");
            rs.RSID.Length.GreaterThan(5).Throw("RUMAH SAKIT ID max length is 5");
            rs.RSName.Empty().Throw("RUMAH SAKIT NAME empty");
            rs.RSName.Length.GreaterThan(30).Throw("RUMAH SAKIT NAME max length is 30");
            rs.KotaID.Empty().Throw("KOTA ID empty");

            //      CONSTRUCT MODEL
            var result = new RSModel
            {
                RSID = rs.RSID,
                RSName = rs.RSName,
                KotaID = rs.KotaID
            };

            //      BUSINESS VALIDATION
            var kt = _kotaDal.GetData(result);
            kt.Empty().Throw("KOTA ID invalid");
            result.KotaName = kt.KotaName;

            //      APPLY
            var exist = _rsDal.GetData(result);
            if (exist != null)
                _rsDal.Update(result);
            else
                _rsDal.Insert(result);

            //      RETURN
            return result;
        }

        public void Delete(IRSKey key)
        {
            //      INPUT VALIDATION
            if (key is null)
                throw new ArgumentException("RUMAH SAKIT ID empty");

            //      APPLY
            _rsDal.Delete(key);
        }

        public RSModel GetData(IRSKey key)
        {
            //      INPUT VALIDATION
            if (key is null)
                throw new ArgumentException("RUMAH SAKIT ID empty");

            //      REPO-OP
            var result = _rsDal.GetData(key);

            //      RETURN
            return result;
        }

        public IEnumerable<RSModel> ListData()
        {
            //      REPO-OP
            var result = _rsDal.ListData();

            //      RETURN
            return result;
        }
    }
}
