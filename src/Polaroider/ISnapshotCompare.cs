
namespace Polaroider
{
    public interface ISnapshotCompare
    {
        SnapshotResult Compare(Snapshot newSnapshot, Snapshot savedSnapshot);
    }
}
