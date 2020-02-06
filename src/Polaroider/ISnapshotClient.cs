
namespace Polaroider
{
    /// <summary>
    /// client object for reading, writing and validating snapshots
    /// </summary>
    public interface ISnapshotClient
    {
        /// <summary>
        /// reads the snapshots from file
        /// </summary>
        /// <param name="snapshotId"></param>
        /// <returns></returns>
        SnapshotCollection Read(SnapshotId snapshotId);

        /// <summary>
        /// writes the snapshot to the file
        /// </summary>
        /// <param name="snapshot"></param>
        /// <param name="snapshotId"></param>
        void Write(Snapshot snapshot, SnapshotId snapshotId);

        /// <summary>
        /// validates the snapshot against the saved snapshot
        /// </summary>
        /// <param name="snapshotId"></param>
        /// <param name="snapshot"></param>
        /// <returns></returns>
        SnapshotResult Validate(SnapshotId snapshotId, Snapshot snapshot);
    }
}
