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
    public class JabatanDalTest
    {
        private readonly IJabatanDal _sut;
        
        public JabatanDalTest()
        {
            _sut = new JabatanDal();
        }

        private JabatanModel JabatanSample()
        {
            var result = new JabatanModel
            {
                JabatanID = "A",
                JabatanName = "B"
            };
            return result;
        }

        [Fact]
        public void Insert_Test()
        {
            using (var trans = TransHelper.NewScope())
            {
                //  arrange
                var expected = JabatanSample();

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
                var expected = JabatanSample();

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
                var expected = JabatanSample();

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
                var expected = JabatanSample();
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
                var expected = new List<JabatanModel>
                { 
                    JabatanSample() 
                };
                _sut.Insert(JabatanSample());

                //  act
                var actual = _sut.ListData();

                //  assert
                actual.Should().BeEquivalentTo(expected);
            }
        }

    }
}
