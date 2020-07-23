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
    public class JenisKontrakDalTest
    {
        public readonly IJenisKontrakDal _sut;

        public JenisKontrakDalTest()
        {
            _sut = new JenisKontrakDal();
        }

        private JenisKontrakModel JenisKontrakTestData()
        {
            var result = new JenisKontrakModel
            {
                JenisKontrakID = "A1",
                JenisKontrakName = "A2"
            };
            return result;
        }

        [Fact]
        public void Insert_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = JenisKontrakTestData();

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
                var expected = JenisKontrakTestData();

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
                var expected = JenisKontrakTestData();

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
                var expected = JenisKontrakTestData();
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
                var expected = new List<JenisKontrakModel> { JenisKontrakTestData() };
                _sut.Insert(JenisKontrakTestData());

                //  act
                var actual = _sut.ListData();

                //  assert
                actual.Should().BeEquivalentTo(expected);
            }
        }

    }
}
