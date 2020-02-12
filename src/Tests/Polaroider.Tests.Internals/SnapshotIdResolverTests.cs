using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Polaroider.Tests
{
    public class SnapshotIdResolverTests
    {
        [Test]
        public void SnapshotIdResolver_ResolveSnapshotId()
        {
            var resolver = new SnapshotSetupResolver();
            var id = resolver.ResloveSnapshotSetup();

            id.ClassName.Should().Be(@"SnapshotIdResolverTests");
            id.MethodName.Should().Be("SnapshotIdResolver_ResolveSnapshotId");

        }
    }
}
