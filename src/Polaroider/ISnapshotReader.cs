
namespace Polaroider
{
    public interface ISnapshotReader
    {
        SnapshotCollection Read(SnapshotId snapshotId);
    }
}
