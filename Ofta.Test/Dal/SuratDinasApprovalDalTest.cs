using Ofta.Lib.Dal;
using Ofta.Lib.Helper;
using Ofta.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;


namespace Ofta.Test.Dal
{

    public class SuratDinasApprovalDalTest
    {
        private readonly ISuratDinasApprovalDal _sut;

        public SuratDinasApprovalDalTest()
        {
            _sut = new SuratDinasApprovalDal();
        }

        private SuratDinasApprovalModel SuratDinasApprovalTestData() =>
            new SuratDinasApprovalModel
            {
                SuratDinasID = "A1",
                PegID = "A2",
                ApprovalTypeID = "A3",
                ApprovalTypeName = ""
            };
        [Fact]
        public void Inser_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = SuratDinasApprovalTestData();

                //  act-assert
                _sut.Insert(expected);
            }
        }

        [Fact]
        public void Delete_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = SuratDinasApprovalTestData();

                //  act-assert
                _sut.Delete(expected);
            }
        }

        [Fact]
        public void ListData_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = new List<SuratDinasApprovalModel> { SuratDinasApprovalTestData() };
                _sut.Insert(SuratDinasApprovalTestData());

                //  act
                var actual = _sut.ListData(SuratDinasApprovalTestData());

                //  assert
                actual.Should().BeEquivalentTo(expected);
            }
        }
    }
}
