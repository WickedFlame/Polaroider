using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Polaroider
{
    /// <summary>
    /// Collection of snapshots
    /// </summary>
    public class SnapshotCollection : IEnumerable<Snapshot>
    {
        private readonly List<Snapshot> _snapshots = new List<Snapshot>();

        /// <summary>
        /// Gets the enumerator for itterating the collection
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Snapshot> GetEnumerator()
        {
            return _snapshots.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Add a new snapshot to the collection
        /// </summary>
        /// <param name="snapshot"></param>
        public void Add(Snapshot snapshot)
        {
            _snapshots.Add(snapshot);
        }

        /// <summary>
        /// Get a snapshot from the collection based on the metadata
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public Snapshot GetSnapshot(SnapshotMetadata metadata)
        {
            return _snapshots.FirstOrDefault(s => s.SnapshotContainsMetadata(metadata));
        }
    }
}
