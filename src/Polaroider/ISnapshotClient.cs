
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
        /// <param name="setup"></param>
        /// <returns></returns>
        SnapshotCollection Read(SnapshotSetup setup);

        /// <summary>
        /// writes the snapshot to the file
        /// </summary>
        /// <param name="snapshot"></param>
        /// <param name="snapshotId"></param>
        void Write(Snapshot snapshot, SnapshotSetup snapshotId);

        /// <summary>
        /// validates the snapshot against the saved snapshot
        /// </summary>
        /// <param name="snapshotId"></param>
        /// <param name="snapshot"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        SnapshotResult Validate(Snapshot snapshot, SnapshotSetup snapshotId, SnapshotConfig config);
    }
}
