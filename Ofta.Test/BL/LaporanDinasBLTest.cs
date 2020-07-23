using FluentAssertions;
using Moq;
using Ofta.Lib.BL;
using Ofta.Lib.Dal;
using Ofta.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace Ofta.Test.BL
{
	public class LaporanDinasBLTest
	{
		//public readonly Mock<ILaporanDinasDal> _mockLaporanDinasDal;
		//public readonly Mock<ISuratDinasDal> _mockSuratDinasDal;
		//public readonly Mock<IPegDal> _mockPegDal;
		//public readonly Mock<ITransportDal> _mockTransportDal;
		//public readonly Mock<IParamNoBL> _mockParamNoBL;

		//public readonly ILaporanDinasBL _sut;

		//public LaporanDinasBLTest()
		//{
		//	_mockLaporanDinasDal = new Mock<ILaporanDinasDal>();
		//	_mockSuratDinasDal = new Mock<ISuratDinasDal>();
		//	_mockPegDal = new Mock<IPegDal>();
		//	_mockTransportDal = new Mock<ITransportDal>();
		//	_mockParamNoBL = new Mock<IParamNoBL>();

		//	_sut = new LaporanDinasBL(_mockLaporanDinasDal.Object,
		//		_mockPegDal.Object,
		//		_mockTransportDal.Object,
		//		_mockParamNoBL.Object, _mockSuratDinasDal.Object
		//		);
		//}

  //      private LaporanDinasModel LaporanDinasTestData() =>
  //          new LaporanDinasModel
  //          {
  //              LaporanDinasID= $"LD-{DateTime.Now:yyMM}-0000F1",
  //              TglJamCreate = DateTime.Parse("2020-05-10T11:00:00"),
  //              ReportedBy = new PegModel
  //              {
  //                  PegID = "PG1",
  //                  PegName = "Agus",
  //              },
  //              SuratDinasID = $"SD-{DateTime.Now:yyMM}-0000F1",
  //              TglMulai = DateTime.Parse("2020-05-10"),
  //              TglSelesai = DateTime.Parse("2020-05-14"),
  //              HasilKerja = "Ini Hasilnya",
  //              KMAkhir = 13000,
  //              Diketahui = new PegModel
  //              {
  //                  PegID = "PG2",
  //                  PegName = "Budi",
  //              },
  //              IsSignedDiketahui= false,
  //          };
  //      private SuratDinasModel SuratDinasTestData() =>
  //          new SuratDinasModel
  //          {
  //              SuratDinasID = $"SD-{DateTime.Now:yyMM}-0000F1",
  //              TglJamCreate = DateTime.Parse("2020-05-10T11:00:00"),
  //              RequestBy = new PegModel
  //              {
  //                  PegID = "PG1",
  //                  PegName = "Agus",
  //              },
  //              NoSurat = "001",
  //              NoKontrak = "002",
  //              TglMulai = DateTime.Parse("2020-05-10"),
  //              TglSelesai = DateTime.Parse("2020-05-14"),
  //              Keperluan = "Implementasi Modul Pharmacy",
  //              TransportID = "T2",
  //              TransportName = "Operasional",
  //              KMAwal = 12150,
  //              RSID = "WGY",
  //              RSName = "RSUD Wangaya",
  //              JenisBiayaID = "J2",
  //              JenisBiayaName = "Kas Bon",
  //              KasBon = 200000,
  //              Diketahui = new PegModel
  //              {
  //                  PegID = "PG2",
  //                  PegName = "Budi"
  //              },
  //              IsSignedDiketahui = false,
  //              Disetujui = new PegModel
  //              {
  //                  PegID = "PG3",
  //                  PegName = "Cahya"
  //              },
  //              IsSignedDisetujui = false,
  //          };

  //      private void SetupMockingAll()
  //      {
  //          SetupMockingLaporanDinasBL();
  //          SetupMockingPegDal();
  //          SetupMockingTransportDal();
  //          SetupMockingParamNoBL();
  //          SetupMockingSuratDinasBL();
  //      }
  //      private void SetupMockingLaporanDinasBL()
  //      {
  //          var id = $"SD-{DateTime.Now:yyMM}-0000F1";
  //          _mockLaporanDinasDal.Setup(x => x
  //              .GetData(It.Is<ILaporanDinasKey>(y => y.LaporanDinasID == id)))
  //              .Returns(LaporanDinasTestData());
  //      }
  //      private void SetupMockingPegDal()
  //      {
  //          _mockPegDal.Setup(x => x
  //             .GetData(It.Is<IPegKey>(y => y.PegID == "PG1")))
  //             .Returns(new PegModel
  //             {
  //                 PegID = "PG1",
  //                 PegName = "Agus",
  //             });

  //          _mockPegDal.Setup(x => x
  //              .GetData(It.Is<IPegKey>(y => y.PegID == "PG2")))
  //              .Returns(new PegModel
  //              {
  //                  PegID = "PG2",
  //                  PegName = "Budi",
  //              });
  //      }
  //      private void SetupMockingTransportDal()
  //      {
  //          _mockTransportDal.Setup(x => x
  //               .GetData(It.Is<ITransportKey>(y => y.TransportID == "T2")))
  //               .Returns(new TransportModel
  //               {
  //                   TransportID = "T2",
  //                   TransportName = "Operasional"
  //               });

  //          _mockTransportDal.Setup(x => x
  //              .GetData(It.Is<ITransportKey>(y => y.TransportID == "T1")))
  //              .Returns(new TransportModel
  //              {
  //                  TransportID = "T1",
  //                  TransportName = "Kendaraan Pribadi"
  //              });
  //      }

  //      private void SetupMockingParamNoBL()
  //      {
  //          var id = $"LD-{DateTime.Now:yyMM}";
  //          _mockParamNoBL.Setup(x => x
  //              .GenNewID(It.Is<ParamNoModel>(y => y.ParamID == id)))
  //              .Returns("F1");
  //      }
  //      private void SetupMockingSuratDinasBL()
  //      {
		//	var id = $"SD-{DateTime.Now:yyMM}-0000F1";
		//	_mockSuratDinasDal.Setup(x => x
		//		.GetData(It.Is<ISuratDinasKey>(y => y.SuratDinasID == id)))
		//		.Returns(SuratDinasTestData());
		//}

  //      [Fact]
  //      public void Add_Valid_ReturnExpected()
  //      {
  //          //  arrange
  //          var expected = LaporanDinasTestData();
  //          SetupMockingAll();

  //          //  act
  //          var actual = _sut.Add(expected);

  //          //  assert
  //          actual.Should().BeEquivalentTo(expected);
  //      }

  //      [Fact]
  //      public void Add_R01_RepostedPegIDInvalid_ThrowArgEx()
  //      {
  //          //  arrange
  //          var expected = LaporanDinasTestData();
  //          SetupMockingAll();
  //          expected.ReportedBy.PegID = "XX";

  //          //  act-assert
  //          var ex = Assert.Throws<ArgumentException>(
  //              () => _sut.Add(expected));
  //      }

  //      [Fact]
  //      public void Add_R02_SuratDinasIDTidakTerdaftar_ThrowArgEx()
  //      {
  //          //  arrange
  //          var expected = LaporanDinasTestData();
  //          SetupMockingAll();
  //          _mockSuratDinasDal.Setup(x => x
  //              .GetData(It.IsAny<ISuratDinasKey>()))
  //              .Returns(null as SuratDinasModel);

  //          //  act-assert
  //          var ex = Assert.Throws<ArgumentException>(
  //              () => _sut.Add(expected));
  //      }

  //      [Fact]
  //      public void Add_R03_PegIDRequestAndReportTidakSama_ThrowArgEx()
  //      {
  //          //  arrange
  //          var expected = LaporanDinasTestData();
  //          SetupMockingAll();

  //          var suratDinas = SuratDinasTestData();
  //          suratDinas.RequestBy.PegID = "PG2";
  //          _mockSuratDinasDal.Setup(x => x
  //              .GetData(It.IsAny<ISuratDinasKey>()))
  //              .Returns(suratDinas);

  //          //  act-assert
  //          var ex = Assert.Throws<ArgumentException>(
  //              () => _sut.Add(expected));
  //      }

  //      [Fact]
  //      public void Add_R04_TglSelesaiMendahuluiTglMulai_ThrowArgEx()
  //      {
  //          //  arrange
  //          var expected = LaporanDinasTestData();
  //          SetupMockingAll();
  //          expected.TglSelesai = DateTime.Parse("2020-05-01");

  //          //  act-assert
  //          var ex = Assert.Throws<ArgumentException>(
  //              () => _sut.Add(expected));
  //      }

  //      [Fact]
  //      public void Add_R05_HasilKerjaKosong_ThrowArgEx()
  //      {
  //          var expected = LaporanDinasTestData();
  //          SetupMockingAll();
  //          expected.HasilKerja = string.Empty;

  //          //  act-assert
  //          var ex = Assert.Throws<ArgumentException>(
  //              () => _sut.Add(expected));
  //      }

  //      [Fact]
  //      public void Add_R06_OpsiKendaraanOperasionalKMAkhirTidakTerisi_ThrowArgEx()
  //      {
  //          var expected = LaporanDinasTestData();
  //          SetupMockingAll();
  //          expected.HasilKerja = string.Empty;

  //          //  act-assert
  //          var ex = Assert.Throws<ArgumentException>(
  //              () => _sut.Add(expected));
  //      }

  //      [Fact]
  //      public void Add_R07_OtherTransportKMAkhirSet0_ThrowArgEx()
  //      {
		//	//  arrange
		//	var expected = LaporanDinasTestData();
		//	SetupMockingAll();
		//	expected.KMAkhir = 0;

		//	//  act-assert
		//	var ex = Assert.Throws<ArgumentException>(
		//		() => _sut.Add(expected));
		//}

  //      [Fact]
  //      public void Add_R08_DisetujuiAtasan_ThrowArgEx()
  //      {
		//	//  arrange
		//	var expected = LaporanDinasTestData();
		//	SetupMockingAll();
		//	expected.Diketahui.PegID = "XX";

		//	//  act-assert
		//	var ex = Assert.Throws<ArgumentException>(
		//		() => _sut.Add(expected));
		//}

  //      [Fact]
  //      public void Add_R09_IsSignedDiketahuiDisetFalse_ThrowArgEx()
  //      {
		//	//  arrange
		//	var expected = LaporanDinasTestData();
		//	SetupMockingAll();
		//	expected.IsSignedDiketahui = true;

		//	//  act
		//	var actual = _sut.Add(expected);

		//	//  assert
		//	//actual.Should().BeEquivalentTo(expected,
		//	//	option => option
		//	//		.Excluding(x => x.IsSignedDiketahui));

		//	actual.IsSignedDiketahui.Should().Be(false);
		//}

    }
}
