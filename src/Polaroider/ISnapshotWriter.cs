
namespace Polaroider
{
    /// <summary>
    /// Write snapshots to file
    /// </summary>
    public interface ISnapshotWriter
    {
        /// <summary>
        /// Write snapshot to a file
        /// </summary>
        /// <param name="snapshot"></param>
        /// <param name="setup"></param>
        void Write(Snapshot snapshot, SnapshotSetup setup);
    }
}
