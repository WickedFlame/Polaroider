using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Polaroider.Tests
{
    public class SnapshotSetupResolverTests
    {
        [Test]
        public void SnapshotSetupResolver_ResolveSnapshotSetup()
        {
            var resolver = new SnapshotSetupResolver();
            var id = resolver.ResolveSnapshotSetup();

            id.Should().NotBeNull();
        }

        [Test]
        public void SnapshotSetupResolver_ResolveSnapshotSetup_ClassName()
        {
            var resolver = new SnapshotSetupResolver();
            var id = resolver.ResolveSnapshotSetup();

            id.ClassName.Should().Be(@"SnapshotSetupResolverTests");
        }

        [Test]
        public void SnapshotSetupResolver_ResolveSnapshotSetup_MethodName()
        {
            var resolver = new SnapshotSetupResolver();
            var id = resolver.ResolveSnapshotSetup();

            id.MethodName.Should().Be("SnapshotSetupResolver_ResolveSnapshotSetup_MethodName");
        }

        [Test]
        public void SnapshotSetupResolver_ResolveSnapshotSetup_Directory()
        {
            var resolver = new SnapshotSetupResolver();
            var id = resolver.ResolveSnapshotSetup();

            id.Directory.Should().EndWith("\\src\\Tests\\Polaroider.Tests.Internals");
        }

        [Test]
        public void SnapshotSetupResolver_ResolveSnapshotSetup_FileName()
        {
            var resolver = new SnapshotSetupResolver(); 
            var id = resolver.ResolveSnapshotSetup();

            id.FileName.Should().Be("SnapshotSetupResolverTests.cs");
        }
    }
}
