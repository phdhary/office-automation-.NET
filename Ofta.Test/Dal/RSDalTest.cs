using FluentAssertions;
using Ofta.Lib.Dal;
using Ofta.Lib.Helper;
using Ofta.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ofta.Test.Dal
{
    public class RSDalTest
    {
        private readonly IRSDal _sut;
        private readonly IKotaDal _kotaDal;

        public RSDalTest()
        {
            _sut = new RSDal();
            _kotaDal = new KotaDal();
        }

        private KotaModel KotaSample()
        {
            var result = new KotaModel
            {
                KotaID = "A1",
                KotaName = "B1"

            };
            return result;
        }

        private RSModel RSSample()
        {
            var kota = KotaSample();

            var result = new RSModel
            {
                RSID = "A",
                RSName = "B",
                KotaID = kota.KotaID,
                KotaName = kota.KotaName
            };
            return result;
        }

        [Fact]
        public void Insert_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = RSSample();

                //  act
                _sut.Insert(expected);
            }
        }

        [Fact]
        public void Update_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = RSSample();

                //  act
                _sut.Update(expected);
            }
        }

        [Fact]
        public void Delete_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = RSSample();
                _sut.Insert(expected);

                //  act
                _sut.Delete(expected);
            }
        }

        [Fact]
        public void GetData_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = RSSample();
                _sut.Insert(expected);
                _kotaDal.Insert(KotaSample());

                //  act
                var actual = _sut.GetData(expected);

                //  assert
                actual.Should().BeEquivalentTo(expected);
            }
        }

        [Fact]
        public void ListData_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = new List<RSModel> { RSSample() };
                _sut.Insert(RSSample());
                _kotaDal.Insert(KotaSample());

                //  act
                var actual = _sut.ListData();

                //  assert
                actual.Should().BeEquivalentTo(expected);
            }
        }
    }
}
