using Ofta.Lib.Dal;
using Ofta.Lib.Helper;
using FluentAssertions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ofta.Lib.Model;
using Xunit;

namespace Ofta.Test.Dal
{
    public class PegDalTest
    {
        private readonly IPegDal _sut;

        public PegDalTest()
        {
            _sut = new PegDal();
        }

        private PegModel PegTestData() => 
            new PegModel
            {
                PegID = "A1",
                PegName = "A2"
            };

        [Fact]
        public void Insert_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = PegTestData();

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
                var expected = PegTestData();

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
                var expected = PegTestData();

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
                var expected = PegTestData();
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
                var expected = new List<PegModel> { PegTestData() };
                _sut.Insert(PegTestData());

                //  act
                var actual = _sut.ListData();

                //  assert
                actual.Should().BeEquivalentTo(expected);
            }
        }

    }
}
