
namespace Polaroid
{
    public interface ISnapshotReader
    {
        SnapshotCollection Read(SnapshotId snapshotId);
    }
}
