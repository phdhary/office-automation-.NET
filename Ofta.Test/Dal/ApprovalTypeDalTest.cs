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

    public class ApprovalTypeDalTest
    {
        private readonly IApprovalTypeDal _sut;

        public ApprovalTypeDalTest()
        {
            _sut = new ApprovalTypeDal();
        }

        private ApprovalTypeModel ApprovalTypeTestData() =>
            new ApprovalTypeModel
            {
                ApprovalTypeID = "A1",
                ApprovalTypeName = "A2"
            };
        [Fact]
        public void Inser_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = ApprovalTypeTestData();

                //  act-assert
                _sut.Insert(expected);
            }
        }
        [Fact]
        public void Update_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = ApprovalTypeTestData();

                //  act-assert
                _sut.Update(expected);
            }
        }

        [Fact]
        public void Delete_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = ApprovalTypeTestData();

                //  act-assert
                _sut.Delete(expected);
            }
        }

        [Fact]
        public void GetData_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = ApprovalTypeTestData();
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
                var expected = new List<ApprovalTypeModel> { ApprovalTypeTestData() };
                _sut.Insert(ApprovalTypeTestData());

                //  act
                var actual = _sut.ListData();

                //  assert
                actual.Should().BeEquivalentTo(expected);
            }
        }
    }
}
