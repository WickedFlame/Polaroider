using FluentAssertions;
using NUnit.Framework;

namespace Polaroider.Tests.Internals
{
    public class UpdateSnapshotAttributeCheck
    {
        [Test]
        [UpdateSnapshot]
        public void CheckAttributeInSetup()
        {
            var resolver = new SnapshotSetupResolver();
            var setup = resolver.ResloveSnapshotSetup();

            // attribute on 
            setup.UpdateSnapshot.Should().BeTrue();
        }

        [Test]
        public void CheckAttributeInSetupIsNotSet()
        {
            var resolver = new SnapshotSetupResolver();
            var setup = resolver.ResloveSnapshotSetup();

            // no attribute set
            setup.UpdateSnapshot.Should().BeFalse();
        }
    }

    [UpdateSnapshot]
    public class UpdateSnapshotAttributeTypeCheck
    {
        [Test]
        public void CheckAttributeInSetup()
        {
            var resolver = new SnapshotSetupResolver();
            var setup = resolver.ResloveSnapshotSetup();

            // attribute on class
            setup.UpdateSnapshot.Should().BeTrue();
        }
    }

    public class UpdateSnapshotAttributeNotSet
    {
        [Test]
        public void CheckAttributeInSetup()
        {
            var resolver = new SnapshotSetupResolver();
            var setup = resolver.ResloveSnapshotSetup();

            // no attribute set
            setup.UpdateSnapshot.Should().BeFalse();
        }
    }
}
