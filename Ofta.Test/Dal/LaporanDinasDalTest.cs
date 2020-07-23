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
    public class LaporanDinasDalTest
    {
        private readonly ILaporanDinasDal _sut;

        public LaporanDinasDalTest()
        {
            _sut = new LaporanDinasDal();
        }

        private LaporanDinasModel LaporanDinasTestData() =>
            new LaporanDinasModel
            {
                LaporanDinasID = "A1",
                TglJamCreate = DateTime.Parse("2020-05-20T09:00:00"),
                PegID = "A2",
                PegName = "",
                SuratDinasID = "A3",
                TglMulai = DateTime.Parse("1900-01-01T00:00:00"),
                TglSelesai = DateTime.Parse("2020-05-25"),
                HasilKerja = "A4",
                KMAkhir = 37500,
            };

        [Fact]
        public void Insert_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                // arrange
                var expected = LaporanDinasTestData();

                // act
                _sut.Insert(expected);
            }
        }
        [Fact]
        public void Update_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                // arrange
                var expected = LaporanDinasTestData();

                // act
                _sut.Update(expected);
            }
        }
        [Fact]
        public void Delete_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                // arrange
                var expected = LaporanDinasTestData();

                // act
                _sut.Delete(expected);
            }
        }
        [Fact]
        public void GetData_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                // arrange
                var expected = LaporanDinasTestData();

                _sut.Insert(expected);

                // act
                var actual = _sut.GetData((ILaporanDinasKey)expected);

                // assert
                actual.Should().BeEquivalentTo(expected);
            }
        }
        [Fact]
        public void ListData_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                // arrange
                var expectedList = new List<LaporanDinasModel>
                {
                    LaporanDinasTestData()
                };

                var expected = LaporanDinasTestData();

                _sut.Insert(expected);

                var tgl1 = DateTime.Parse("2020-05-01");
                var tgl2 = DateTime.Parse("2020-05-31");

                //  act
                var actual = _sut.ListData(tgl1, tgl2);

                //  assert
                actual.Should().BeEquivalentTo(expectedList);
            }
        }

    }
}
