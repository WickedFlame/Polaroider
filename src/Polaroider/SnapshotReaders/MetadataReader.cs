
using System.Linq;

namespace Polaroid.SnapshotReaders
{
    public class MetadataReader : ILineReader
    {
        public void ReadLine(string line, Snapshot item)
        {
            var index = line.IndexOf(':');
            if (index < 0)
            {
                return;
            }

            var key = line.Substring(0, index);
            var value = line.Substring(index + 1);
            item.Metadata.Add(key, value.Trim());
        }

        public void Reset() { }

        public bool NewSnapshot(Snapshot snapshot)
        {
            return snapshot != null && (snapshot.Any() || snapshot.Metadata.Any());
        }
    }
}
