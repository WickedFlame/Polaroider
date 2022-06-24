
namespace Polaroider
{
    /// <summary>
    /// defines states that the snapshot can be in
    /// </summary>
    public enum SnapshotStatus
    {
        /// <summary>
        /// SnapshotDoesNotExist
        /// </summary>
        SnapshotDoesNotExist,

        /// <summary>
        /// SnapshotsMatch
        /// </summary>
        SnapshotsMatch,

        /// <summary>
        /// SnapshotsDoNotMatch
        /// </summary>
        SnapshotsDoNotMatch,

        /// <summary>
        /// SnapshotUpdated
        /// </summary>
        SnapshotUpdated,

        /// <summary>
        /// UpdateSnapshot
        /// </summary>
        UpdateSnapshot
    }
}
