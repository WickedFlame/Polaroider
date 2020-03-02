
namespace Polaroider
{
    /// <summary>
    /// defines states that the snapshot can be in
    /// </summary>
    public enum SnapshotStatus
    {
        SnapshotDoesNotExist,
        SnapshotsMatch,
        SnapshotsDoNotMatch,
        SnapshotUpdated,
        UpdateSnapshot
    }
}
