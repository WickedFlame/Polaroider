using System;
using System.Linq;

namespace Polaroider.Tests
{
    public class SnapshotOptionsCloneTests
    {
        [Test]
        public void SnapshotOptions_Clone_ShouldCopyComparer()
        {
            var options = new SnapshotOptions();
            options.SetComparer((a, b) => true);

            var clone = options.Clone();

            clone.Comparer.Should().BeSameAs(options.Comparer);
        }

        [Test]
        public void SnapshotOptions_Clone_ShouldCopyParser()
        {
            var directive = new Func<string, string>(s => s);
            var options = new SnapshotOptions { Parser = new LineParser() };
            options.AddDirective(directive);

            var clone = options.Clone();

            clone.Parser.Should().NotBeSameAs(options.Parser);
            clone.Parser.Directives.Should().Contain(directive);
        }

        [Test]
        public void SnapshotOptions_Clone_ShouldCopyUpdateSnapshot()
        {
            var options = new SnapshotOptions { UpdateSnapshot = true };

            var clone = options.Clone();

            clone.UpdateSnapshot.Should().BeTrue();
        }

        [Test]
        public void SnapshotOptions_Clone_ShouldCopyTypeMappers()
        {
            var options = new SnapshotOptions();
            options.AddMapper<string>((ctx, s) => { });

            var clone = options.Clone();

            clone.TypeMappers.Keys.Count().Should().Be(1);
        }

        [Test]
        public void SnapshotOptions_Clone_ShouldReturnNewInstance()
        {
            var options = new SnapshotOptions();

            var clone = options.Clone();

            clone.Should().NotBeSameAs(options);
        }
    }
}
