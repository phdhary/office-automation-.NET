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
    public class JenisSuratDinasDalTest
    {
        private readonly IJenisSuratDinasDal _sut;

        public JenisSuratDinasDalTest()
        {
            _sut = new JenisSuratDinasDal();
        }

        private JenisSuratDinasModel JenisSuratDinasTestData()
        {
            var result = new JenisSuratDinasModel
            {
                JenisSuratDinasID = "A1",
                JenisSuratDinasName = "A2"
            };
            return result;
        }

        [Fact]
        public void Insert_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = JenisSuratDinasTestData();

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
                var expected = JenisSuratDinasTestData();

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
                var expected = JenisSuratDinasTestData();

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
                var expected = JenisSuratDinasTestData();
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
                var expected = new List<JenisSuratDinasModel>{JenisSuratDinasTestData()};
                _sut.Insert(JenisSuratDinasTestData());

                //  act
                var actual = _sut.ListData();

                //  assert
                actual.Should().BeEquivalentTo(expected);
            }
        }


    }
}
