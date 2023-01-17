
namespace Polaroider.SnapshotReaders
{
    /// <summary>
    /// 
    /// </summary>
    internal class SnapshotLineReader : ILineReader
    {
        /// <summary>
        /// Read the line and add it to the snapshot
        /// </summary>
        /// <param name="line"></param>
        /// <param name="snapshot"></param>
        public void ReadLine(string line, Snapshot snapshot)
        {
            // set data row
            var row = new Line(line);
            snapshot.Add(row);
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
