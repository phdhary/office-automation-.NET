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
    public class JenisBiayaDalTest
    {
        private readonly IJenisBiayaDal _sut;

        public JenisBiayaDalTest()
        {
            _sut = new JenisBiayaDal();
        }

        private JenisBiayaModel JenisBiayaSample()
        {
            var result = new JenisBiayaModel 
            { 
                JenisBiayaID = "A",
                JenisBiayaName = "B"
            };
            return result;
        }

        [Fact]
        public void Insert_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = JenisBiayaSample();

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
                var expected = JenisBiayaSample();

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
                var expected = JenisBiayaSample();
                _sut.Insert(expected);

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
                var expected = JenisBiayaSample();
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
                var expected = new List<JenisBiayaModel> { JenisBiayaSample() };
                _sut.Insert(JenisBiayaSample());

                //  act
                var actual = _sut.ListData();

                //  assert
                actual.Should().BeEquivalentTo(expected);
            }
        }
    }
}
