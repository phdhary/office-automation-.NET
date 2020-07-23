using Ofta.Lib.Dal;
using Ofta.Lib.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.BL
{
    public interface IParamNoBL
    {
        string GenNewID(string prefix, ParamNoLengthEnum length);
    }

    public enum ParamNoLengthEnum
    {
        Code_5,
        Code_10,
        Code_13,
        Code_15
    }


    public class ParamNoBL : IParamNoBL
    {
        private readonly IParamNoDal _paramNoDal;

        public ParamNoBL()
        {
            _paramNoDal = new ParamNoDal();
        }

        public ParamNoBL(IParamNoDal paramNoDal)
        {
            _paramNoDal = paramNoDal;
        }
        private string GenNewID(IParamNoKey key)
        {
            /*  Cek apakah paramkey sudah ada di database
             *  Jika sudah ada, ambil nilai-nya, setu retVal, 
             *  dan tambahkan satu (hexa-desimal mode)
             */
            var param = _paramNoDal.GetData(key);
            if (param == null)
                param = new ParamNoModel
                {
                    ParamID = key.ParamID,
                    ParamValue = "0"
                };
            var retVal = param.ParamValue;
            param.ParamValue = AddHexa(param.ParamValue, "1");
            _paramNoDal.Delete(key);
            _paramNoDal.Insert(param);

            return retVal;
        }

        public string GenNewID(string prefix, ParamNoLengthEnum length)
        {
            /*  CI-206-0000-00A = Code_15
             *  US-206-00-00A   = Code_13
             *  BE-206-00A      = Code_10
             *  PG001           = Code_5
             */

            var periode = DateTime.Now.ToString("yy");
            switch (DateTime.Now.Month)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    periode += DateTime.Now.Month.ToString();
                    break;
                case 10:
                    periode += "A";
                    break;
                case 11:
                    periode += "B";
                    break;
                case 12:
                    periode += "C";
                    break;
            }

            var param = new ParamNoModel
            {
                ParamID = $"{prefix}-{periode}"
            };

            if (length == ParamNoLengthEnum.Code_5)
                param.ParamID = prefix;

            var noUrutHex = GenNewID(param);
            var random = new Random();
            var checkDigit = random.Next(0, 9);
            string noUrutBlok = "";
            switch (length)
            {
                case ParamNoLengthEnum.Code_10:
                    noUrutBlok = $"{noUrutHex.PadLeft(2, '0')}{checkDigit}";
                    break;
                case ParamNoLengthEnum.Code_13:
                    noUrutBlok = $"{noUrutHex.PadLeft(4, '0')}";
                    noUrutBlok = $"{noUrutBlok.Substring(0, 2)}-{noUrutBlok.Substring(2, 2)}";
                    noUrutBlok = $"{noUrutBlok}{checkDigit}";

                    break;
                case ParamNoLengthEnum.Code_15:
                    noUrutBlok = $"{noUrutHex.PadLeft(6, '0')}";
                    noUrutBlok = $"{noUrutBlok.Substring(0, 4)}-{noUrutBlok.Substring(4, 2)}";
                    noUrutBlok = $"{noUrutBlok}{checkDigit}";
                    break;
                case ParamNoLengthEnum.Code_5:
                    noUrutBlok = $"{noUrutHex.PadLeft(3, '0')}";
                    break;
                default:
                    break;
            }

            var result = $"{prefix}-{periode}-{noUrutBlok}";
            if(length == ParamNoLengthEnum.Code_5)
                result = $"{prefix}{noUrutBlok}";
            return result;
        }

        private string AddHexa(string h1, string h2)
        {
            BigInteger number1 = BigInteger.Parse(h1, NumberStyles.HexNumber);
            BigInteger number2 = BigInteger.Parse(h2, NumberStyles.HexNumber);
            BigInteger sum = BigInteger.Add(number1, number2);
            return sum.ToString("X");
        }
    }
}
