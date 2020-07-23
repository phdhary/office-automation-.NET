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
    public class SuratDinasDalTest
    {
        private readonly ISuratDinasDal _sut;

        public SuratDinasDalTest()
        {
            _sut = new SuratDinasDal();
        }

        private SuratDinasModel SuratDinasTestData() =>
            new SuratDinasModel
            {
                SuratDinasID = "A1",
                TglJamCreate = DateTime.Parse("2020-05-10T09:00:00"),
                PegID = "A2",
                PegName = "",
                NoSurat = "A3",
                NoKontrak = "A4",
                TglMulai = DateTime.Parse("2020-05-10"),
                TglSelesai = DateTime.Parse("2020-05-15"),
                Keperluan = "A5",
                TransportID = "A6",
                TransportName = "",
                KMAwal = 37122,
                RSID = "A7",
                RSName = "",
                JenisBiayaID = "A8",
                JenisBiayaName = "",
                KasBon = 250000,
            };

        [Fact]
        public void Insert_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = SuratDinasTestData();

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
                var expected = SuratDinasTestData();

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
                var expected = SuratDinasTestData();

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
                var expected = SuratDinasTestData();
                _sut.Insert(expected);

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
                var expected = new List<SuratDinasModel> { SuratDinasTestData() };
                _sut.Insert(SuratDinasTestData());
                var tgl1 = DateTime.Parse("2020-05-01");
                var tgl2 = DateTime.Parse("2020-05-31");

                //  act
                var actual = _sut.ListData(tgl1, tgl2);

                //  assert
                actual.Should().BeEquivalentTo(expected);
            }
        }
    }
}
