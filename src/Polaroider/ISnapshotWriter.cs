
namespace Polaroid
{
    public interface ISnapshotWriter
    {
        void Write(Snapshot snapshot, SnapshotId snapshotId);
    }
}
