using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Polaroider.Tests
{
    public class UpdateSnapshot
    {
        [Test]
        [UpdateSnapshot]
        public void Update()
        {
            var resolver = new SnapshotSetupResolver();
            var reader = new SnapshotReader();
            var snapshots = reader.Read(resolver.ResolveSnapshotSetup());

            snapshots.Count().Should().Be(1);
            snapshots.Single().ToString().Should().Be("original string");

            "updated string".MatchSnapshot();

            snapshots = reader.Read(resolver.ResolveSnapshotSetup());

            snapshots.Count().Should().Be(1);
            snapshots.Single().ToString().Should().Be("updated string");


            "original string".MatchSnapshot();
        }
    }
}
