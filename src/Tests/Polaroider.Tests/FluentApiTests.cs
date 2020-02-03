using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Polaroider.Tests
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
            match.Should().Throw<SnapshotMatchException>();
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
            Action match = () => "test\r\nsnapshot\r\none".MatchSnapshot(() => new { id = "one"});
            match.Should().NotThrow();

            match = () => "test\r\nsnapshot\r\ntwo".MatchSnapshot(()=>new {id="2"});
            match.Should().NotThrow();
        }

        [Test]
        public void MatchSnapshotToken()
        {
            var token = SnapshotTokenizer.Tokenize("match\r\nsnapshot\r\ntoken");
            Action match = () => token.MatchSnapshot();
            match.Should().NotThrow();
        }

        [Test]
        public void MatchMultipleSnapshotTokens()
        {
            var token = SnapshotTokenizer.Tokenize("test\r\nsnapshot\r\none")
                .SetMetadata(() => new { id = "one" });

            Action match = () => token.MatchSnapshot();
            match.Should().NotThrow();

            token= SnapshotTokenizer.Tokenize("test\r\nsnapshot\r\ntwo")
                .SetMetadata(() => new { id = "2" });

            match = () => token.MatchSnapshot();
            match.Should().NotThrow();
        }
    }
}
