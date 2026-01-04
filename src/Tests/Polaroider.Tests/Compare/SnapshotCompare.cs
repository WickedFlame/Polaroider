using System;

namespace Polaroider.Tests.Compare
{
    public class SnapshotCompare
    {
        [Test]
        public void SavedIsShorter()
        {
            Action match = () => "test\r\nsnapshot\r\nlonger".MatchSnapshot();
            match.Should().Throw<SnapshotMatchException>();
        }

        [Test]
        public void CompareIsShorter()
        {
            Action match = () => "test\r\nsnapshot".MatchSnapshot();
            match.Should().Throw<SnapshotMatchException>();
        }
    }
}
