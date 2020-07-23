using Ofta.Lib.Dal;
using Ofta.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ofta.Lib.Helper;
using Xunit;
using FluentAssertions;

namespace Ofta.Test.Dal
{
    public class JenisArsipDalTest
    {
        private readonly IJenisArsipDal _sut;

        public JenisArsipDalTest()
        {
            _sut = new JenisArsipDal();
        }
        
        private JenisArsipModel JenisArsipSample()
        {
            var result = new JenisArsipModel
            {
                JenisArsipID = "A",
                JenisArsipName = "B"
            };
            return result;
        }

        [Fact]
        public void Insert_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = JenisArsipSample();

                //  act
                _sut.Insert(expected);

                //  assert
            }
        }

        [Fact]
        public void Update_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = JenisArsipSample();

                //  act
                _sut.Update(expected);

                //  assert
            }
        }
        [Fact]
        public void Delete_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = JenisArsipSample();
                _sut.Insert(expected);

                //  act
                _sut.Delete(expected);

                //  assert
            }
        }
        [Fact]
        public void GetData_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = JenisArsipSample();
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
                var expected = new List<JenisArsipModel> { JenisArsipSample() };
                _sut.Insert(JenisArsipSample());

                //  act
                var actual = _sut.ListData();

                //  assert
                actual.Should().BeEquivalentTo(expected);
            }
        }
    }
}
