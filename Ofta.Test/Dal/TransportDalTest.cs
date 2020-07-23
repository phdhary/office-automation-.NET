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
    public class TransportDalTest
    {
        private readonly ITransportDal _sut;

        public TransportDalTest()
        {
            _sut = new TransportDal();
        }

        private TransportModel TransportSample()
        {
            var result = new TransportModel
            {
                TransportID = "A",
                TransportName = "B"
            };
            return result;
        }

        [Fact]
        public void Insert_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = TransportSample();

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
                var expected = TransportSample();

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
                var expected = TransportSample();

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
                var expected = TransportSample();
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
                var expected = new List<TransportModel>
                {
                    TransportSample()
                };
                _sut.Insert(TransportSample());

                //  act
                var actual = _sut.ListData();

                //  assert
                actual.Should().BeEquivalentTo(expected);
            }
        }

    }
}
