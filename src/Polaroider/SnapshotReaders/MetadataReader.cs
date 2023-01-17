using System.Linq;

namespace Polaroider.SnapshotReaders
{
    /// <summary>
    /// Metadatareader
    /// </summary>
    internal class MetadataReader : ILineReader
    {
        /// <summary>
        /// Read a line of a snapshot
        /// </summary>
        /// <param name="line"></param>
        /// <param name="snapshot"></param>
        public void ReadLine(string line, Snapshot snapshot)
        {
            var index = line.IndexOf(':');
            if (index < 0)
            {
                return;
            }

            var key = line.Substring(0, index);
            var value = line.Substring(index + 1);
            snapshot.Metadata.Add(key, value.Trim());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="snapshot"></param>
        /// <returns></returns>
        public bool NewSnapshot(Snapshot snapshot)
        {
            return snapshot != null && (snapshot.Any() || snapshot.Metadata.Any());
        }
    }
}
