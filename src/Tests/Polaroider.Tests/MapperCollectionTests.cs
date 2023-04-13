using FluentAssertions;
using Moq;
using NUnit.Framework;
using Polaroider.Mapping;

namespace Polaroider.Tests
{
    public class MapperCollectionTests
    {
        [Test]
        public void MapperCollection_Indexer()
        {
            var collection = new MapperCollection<IMapper>();
            var mapper = new Mock<IMapper>();
            collection.Add(typeof(IMapper), mapper.Object);

            collection[typeof(IMapper)].Should().BeSameAs(mapper.Object);
        }

        [Test]
        public void MapperCollection_Indexer_IneritedType()
        {
            var collection = new MapperCollection<IMapper>();
            var mapper = new Mock<IMapper>();
            collection.Add(typeof(IMapper), mapper.Object);

            collection[typeof(Derived)].Should().BeSameAs(mapper.Object);
        }

        public class Derived : IMapper { }
    }
}
