using FluentAssertions;
using NUnit.Framework;

namespace Polaroider.Tests
{
    public class ObjectExtensionsTests
    {
        [Test]
        public void ObjectExtensions_MapTo()
        {
            var obj = new
            {
                Age = 25,
                Name = "John",
                LastName = "Doe"
            };

            obj.MapTo(o =>
                new
                {
                    Name = o.Name,
                    LastName = o.LastName
                })
                .Should()
                .BeEquivalentTo(new
                {
                    Name = "John",
                    LastName = "Doe"
                });
        }
    }
}
