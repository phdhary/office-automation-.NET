using FluentAssertions;
using FluentValidation;
using Moq;
using Ofta.Lib.BL;
using Ofta.Lib.Dal;
using Ofta.Lib.Dto;
using Ofta.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ofta.Test.BL
{
    public class SuratDinasBLTest
    {
        public readonly Mock<ISuratDinasDal> _mockSuratDinasDal;
        public readonly Mock<IPegDal> _mockPegDal;
        public readonly Mock<ITransportDal> _mockTransportDal;
        public readonly Mock<IJenisBiayaDal> _mockJenisBiayaDal;
        public readonly Mock<IParamNoBL> _mockParamNoBL;
        public readonly Mock<IRSDal> _mockRSDal;
        public readonly Mock<ILaporanDinasDal> _mockLaporanDinasDal;

        public readonly ISuratDinasBL _sut;

        public SuratDinasBLTest()
        {
            _mockSuratDinasDal = new Mock<ISuratDinasDal>();
            _mockPegDal = new Mock<IPegDal>();
            _mockTransportDal = new Mock<ITransportDal>();
            _mockJenisBiayaDal = new Mock<IJenisBiayaDal>();
            _mockParamNoBL = new Mock<IParamNoBL>();
            _mockRSDal = new Mock<IRSDal>();
            _mockLaporanDinasDal = new Mock<ILaporanDinasDal>();

            _sut = new SuratDinasBL(_mockSuratDinasDal.Object,
                _mockPegDal.Object,
                _mockTransportDal.Object,
                _mockJenisBiayaDal.Object,
                _mockParamNoBL.Object,
                _mockRSDal.Object,
                _mockLaporanDinasDal.Object);
        }

        private SuratDinasModel SuratDinasTestData() =>
            new SuratDinasModel
            {
                SuratDinasID = $"SD-{DateTime.Now:yyMM}-0000F1",
                TglJamCreate = DateTime.Parse("2020-05-10T11:00:00"),
                PegID = "PG1",
                PegName = "",
                NoSurat = "001",
                NoKontrak = "002",
                TglMulai = DateTime.Parse("2020-05-10"),
                TglSelesai = DateTime.Parse("2020-05-14"),
                Keperluan = "Implementasi Modul Pharmacy",
                TransportID = "T2",
                TransportName = "Operasional",
                KMAwal = 12150,
                RSID = "WGY",
                RSName = "RSUD Wangaya",
                JenisBiayaID = "J2",
                JenisBiayaName = "Kas Bon",
                KasBon = 200000,
            };

        private SuratDinasAddDto SuratDinasAddTestData() =>
            new SuratDinasAddDto
            {
                PegID = "PG1",
                NoSurat = "001",
                NoKontrak = "002",
                TglMulai = DateTime.Parse("2020-05-10"),
                TglSelesai = DateTime.Parse("2020-05-14"),
                Keperluan = "Implementasi Modul Pharmacy",
                TransportID = "T2",
                KMAwal = 12150,
                RSID = "WGY",
                JenisBiayaID = "J2",
                KasBon = 200000,
            };



        private void SetupMockingAll()
        {
            SetupMockingPegDal();
            SetupMockingTransportDal();
            SetupMockingJenisBiayaDal();
            SetupMockingParamNoBL();
            SetupMockingParamNoBL();
            SetupMockingSuratDinasBL();
            SetupMockingRSDal();
        }
        private void SetupMockingPegDal()
        {
            _mockPegDal.Setup(x => x
                .GetData(It.Is<IPegKey>(y => y.PegID == "PG1")))
                .Returns(new PegModel
                {
                    PegID = "PG1",
                    PegName = "Agus",
                });

            _mockPegDal.Setup(x => x
                .GetData(It.Is<IPegKey>(y => y.PegID == "PG2")))
                .Returns(new PegModel
                {
                    PegID = "PG1",
                    PegName = "Budi",
                });

            _mockPegDal.Setup(x => x
                .GetData(It.Is<IPegKey>(y => y.PegID == "PG3")))
                .Returns(new PegModel
                {
                    PegID = "PG3",
                    PegName = "Cahya",
                });
        }
        private void SetupMockingTransportDal()
        {
            _mockTransportDal.Setup(x => x
                .GetData(It.Is<ITransportKey>(y => y.TransportID == "T2")))
                .Returns(new TransportModel
                {
                    TransportID = "T2",
                    TransportName = "Operasional"
                });

            _mockTransportDal.Setup(x => x
                .GetData(It.Is<ITransportKey>(y => y.TransportID == "T1")))
                .Returns(new TransportModel
                {
                    TransportID = "T1",
                    TransportName = "Kendaraan Pribadi"
                });

        }
        private void SetupMockingJenisBiayaDal()
        {
            _mockJenisBiayaDal.Setup(x => x
                .GetData(It.Is<IJenisBiayaKey>(y => y.JenisBiayaID == "J2")))
                .Returns(new JenisBiayaModel
                {
                    JenisBiayaID = "J2",
                    JenisBiayaName = "Kas Bon"
                });

            _mockJenisBiayaDal.Setup(x => x
                .GetData(It.Is<IJenisBiayaKey>(y => y.JenisBiayaID == "J1")))
                .Returns(new JenisBiayaModel
                {
                    JenisBiayaID = "J1",
                    JenisBiayaName = "Reimburstment"
                });

        }
        private void SetupMockingParamNoBL()
        {
            var id = $"SD-{DateTime.Now:yyMM}";
            _mockParamNoBL.Setup(x => x
                .GenNewID("SD",ParamNoLengthEnum.Code_13))
                .Returns("F1");
        }
        private void SetupMockingSuratDinasBL()
        {
            var id = $"SD-{DateTime.Now:yyMM}-0000F1";
            _mockSuratDinasDal.Setup(x => x
                .GetData(It.Is<ISuratDinasKey>(y => y.SuratDinasID == id)))
                .Returns(SuratDinasTestData());
        }
        private void SetupMockingRSDal()
        {
            _mockRSDal.Setup(x => x
                .GetData(It.Is<IRSKey>(y => y.RSID == "WGY")))
                .Returns(new RSModel
                {
                    RSID = "WGY",
                    RSName = "RSUD Wangaya"
                });
        }

        [Fact]
        public void Add_Valid_ReturnExpected()
        {
            //  arrange
            var expected = SuratDinasTestData();
            var paramAdd = SuratDinasAddTestData();
            SetupMockingAll();

            //  act
            var actual = _sut.Propose(paramAdd);

            //  assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Add_R01_RequestByInvalid_ThrowArgEx()
        {
            //  arrange
            var expected = SuratDinasTestData();
            var paramAdd = SuratDinasAddTestData();
            SetupMockingAll();
            expected.PegID = "XX";

            //  act-assert
            var ex = Assert.Throws<ArgumentException>(
                () => _sut.Propose(paramAdd));
        }

        [Fact]
        public void Add_R02_TglSelesaiMendahuluiTglMulai_ThrowArgEx()
        {
            //  arrange
            var expected = SuratDinasTestData();
            var paramAdd = SuratDinasAddTestData();
            SetupMockingAll();
            expected.TglSelesai = DateTime.Parse("2020-05-01");

            //  act-assert
            var ex = Assert.Throws<ArgumentException>(
                () => _sut.Propose(paramAdd));
        }

        [Fact]
        public void Add_R03_KeperluanKosong_ThrowArgEx()
        {
            //  arrange
            var expected = SuratDinasTestData();
            var paramAdd = SuratDinasAddTestData();
            SetupMockingAll();
            expected.Keperluan = string.Empty;

            //  act-assert
            var ex = Assert.Throws<ArgumentException>(
                () => _sut.Propose(paramAdd));
        }

        [Fact]
        public void Add_R04_TransporTidakTerdaftar_ThrowArgEx()
        {
            //  arrange
            var expected = SuratDinasTestData();
            var paramAdd = SuratDinasAddTestData();
            SetupMockingAll();
            expected.Keperluan = string.Empty;

            //  act-assert
            var ex = Assert.Throws<ArgumentException>(
                () => _sut.Propose(paramAdd));
        }

        [Fact]
        public void Add_R05_TransportOperasionalKM0_ThrowArgEx()
        {
            //  arrange
            var expected = SuratDinasTestData();
            var paramAdd = SuratDinasAddTestData();
            SetupMockingAll();
            expected.KMAwal = 0;

            //  act-assert
            var ex = Assert.Throws<ArgumentException>(
                () => _sut.Propose(paramAdd));
        }

        [Fact]
        public void Add_R06_TransportNonOperasional_KMAwalSetJadi0()
        {
            //  arrange
            var expected = SuratDinasTestData();
            var paramAdd = SuratDinasAddTestData();
            SetupMockingAll();
            expected.TransportID = "T1";

            //  act
            var actual = _sut.Propose(paramAdd);

            //  assert
            actual.Should().BeEquivalentTo(expected,
                option => option
                    .Excluding(x => x.KMAwal));
            actual.KMAwal.Should().Be(0);
        }

        [Fact]
        public void Add_R07_JenisBiayaTidakTerdaftar_ThrowArgEx()
        {
            //  arrange
            var expected = SuratDinasTestData();
            var paramAdd = SuratDinasAddTestData();
            SetupMockingAll();
            paramAdd.JenisBiayaID = "";

            //  act-assert
            var ex = Assert.Throws<ArgumentException>(
                () => _sut.Propose(paramAdd));
        }
        [Fact]
        public void Add_R08_KasBonNilaiNol_ThrowArgEx()
        {
            //  arrange
            var expected = SuratDinasTestData();
            var paramAdd = SuratDinasAddTestData();
            SetupMockingAll();
            expected.JenisBiayaID = "J2";
            expected.KasBon = 0;

            //  act-assert
            var ex = Assert.Throws<ArgumentException>(
                () => _sut.Propose(paramAdd));
        }

        [Fact]
        public void Add_R09_JenisBiayaNonKasBon_NilaiDisetNol()
        {
            //  arrange
            var expected = SuratDinasTestData();
            var paramAdd = SuratDinasAddTestData();
            SetupMockingAll();
            expected.JenisBiayaID = "J1";

            //  act
            var actual = _sut.Propose(paramAdd);

            //  assert
            actual.Should().BeEquivalentTo(expected,
                option => option
                    .Excluding(x => x.KasBon));
            actual.KasBon.Should().Be(0);
        }
    }
}
