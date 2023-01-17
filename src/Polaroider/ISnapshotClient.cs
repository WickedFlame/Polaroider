
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
        /// <param name="setup"></param>
        void Write(Snapshot snapshot, SnapshotSetup setup);

        /// <summary>
        /// validates the snapshot against the saved snapshot
        /// </summary>
        /// <param name="setup"></param>
        /// <param name="snapshot"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        SnapshotResult Validate(Snapshot snapshot, SnapshotSetup setup, SnapshotOptions options);
    }
}
