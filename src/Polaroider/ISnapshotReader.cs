
namespace Polaroider
{
    /// <summary>
    /// Read snapshots from files
    /// </summary>
    public interface ISnapshotReader
    {
        /// <summary>
        /// Read snapshots from file
        /// </summary>
        /// <param name="setup"></param>
        /// <returns></returns>
        SnapshotCollection Read(SnapshotSetup setup);
    }
}
