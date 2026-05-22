namespace Polaroider.Tests
{
    public class SnapshotNameAttributeTests
    {
        [Test]
        [SnapshotName("test-snapshot-1")]
        public void SnapshotNameAttribute_Resolve_SnapshotName()
        {
            var resolver = new SnapshotSetupResolver();
            var setup = resolver.ResolveSnapshotSetup();

            setup.SnapshotName.Should().Be("test-snapshot-1");
        }

        [Test]
        [SnapshotName("test-snapshot-2")]
        public void SnapshotNameAttribute_Resolve_GetFilePath()
        {
            var resolver = new SnapshotSetupResolver();
            var setup = resolver.ResolveSnapshotSetup();

            setup.GetFilePath().Replace("\\", "/").Should().EndWith("/_Snapshots/test-snapshot-2.snapshot");
        }

        [Test]
        [SnapshotName("")]
        public void SnapshotNameAttribute_Resolve_GetFilePath_EmptyAttribute()
        {
            var resolver = new SnapshotSetupResolver();
            var setup = resolver.ResolveSnapshotSetup();

            setup.GetFilePath().Replace("\\", "/").Should().EndWith("/_Snapshots/SnapshotNameAttributeTests_SnapshotNameAttribute_Resolve_GetFilePath_EmptyAttribute.snapshot");
        }
    }
}
