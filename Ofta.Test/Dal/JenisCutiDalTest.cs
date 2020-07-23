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
    public class JenisCutiDalTest
    {
        private readonly IJenisCutiDal _sut;

        public JenisCutiDalTest()
        {
            _sut = new JenisCutiDal();
        }

        private JenisCutiModel JenisCutiSample()
        {
            var result = new JenisCutiModel
            {
                JenisCutiID = "A",
                JenisCutiName = "B"
            };
            return result;
        }

        [Fact]
        public void Insert_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = JenisCutiSample();

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
                var expected = JenisCutiSample();

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
                var expected = JenisCutiSample();
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
                var expected = JenisCutiSample();
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
                var expected = new List<JenisCutiModel>
                {
                    JenisCutiSample()
                };
                _sut.Insert(JenisCutiSample());

                //  act
                var actual = _sut.ListData();

                //  assert
                actual.Should().BeEquivalentTo(expected);
            }
        }
    }
}
