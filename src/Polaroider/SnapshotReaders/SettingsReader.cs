
namespace Polaroider.SnapshotReaders
{
    /// <summary>
    /// 
    /// </summary>
    internal class SettingsReader : ILineReader
    {
        /// <summary>
        /// Read the line
        /// </summary>
        /// <param name="line"></param>
        /// <param name="snapshot"></param>
        public void ReadLine(string line, Snapshot snapshot)
        {
            // do nothing for settings
        }

        /// <summary>
        /// Defaults to false
        /// </summary>
        /// <param name="snapshot"></param>
        /// <returns></returns>
        public bool NewSnapshot(Snapshot snapshot)
        {
            return false;
        }
    }
}
