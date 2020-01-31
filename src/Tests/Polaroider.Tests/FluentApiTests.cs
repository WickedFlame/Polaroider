using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Polaroid.Tests
{
    public class FluentApiTests
    {
        [Test]
        public void MatchSnapshot()
        {
            Action match = () => "test\r\nsnapshot".MatchSnapshot();
            match.Should().NotThrow();
        }

        [Test]
        public void MismatchSnapshot()
        {
            Action match = () => "test\r\nfail".MatchSnapshot();
            match.Should().Throw<SnapshotsDoNotMatchException>();
        }

        [Test]
        public void NoSnapshot()
        {
            Action match = () => "test\r\nsnapshot".MatchSnapshot();
            match.Should().NotThrow();

            var resolver = new SnapshotIdResolver();
            var file = resolver.ResloveId().GetFilePath();

            // Assert
            System.IO.File.Exists(file).Should().BeTrue();

            // cleanup
            System.IO.File.Delete(file);
        }

        [Test]
        public void MatchMultipleSnapshots()
        {
            Action match = () => "test\r\nsnapshot\r\none".MatchSnapshot("one");
            match.Should().NotThrow();

            match = () => "test\r\nsnapshot\r\ntwo".MatchSnapshot("2");
            match.Should().NotThrow();
        }
    }
}
