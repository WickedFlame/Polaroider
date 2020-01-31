
namespace Polaroider
{
    public interface ISnapshotClient
    {
        SnapshotCollection Read(SnapshotId snapshotId);

        void Write(Snapshot snapshot, SnapshotId snapshotId);
    }
}
