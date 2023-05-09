using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Universe.Models;
using Universe.Models.spaceobject;

namespace Tests_xUnit.FakeClasses
{
    public class MockEntityRepository<T> : Mock<IRepository<T>> where T : DbEntity
    {
        public MockEntityRepository<T> MockGetByID(T result)
        {
            Setup(x => x.GetByID(It.IsAny<int>())).Returns(result);
            return this;
        }

        public MockEntityRepository<T> MockCountAsync(int result)
        {
            Setup(x => x.GetList().Count()).Returns(result);
            return this;
        }
    }
}
