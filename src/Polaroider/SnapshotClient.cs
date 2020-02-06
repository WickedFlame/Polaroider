
namespace Polaroider
{
    /// <summary>
    /// Client object for reading, writing and validating snapshots
    /// </summary>
    internal class SnapshotClient : ISnapshotClient
    {
        private readonly ISnapshotWriter _snapshotWriter;
        private readonly ISnapshotReader _snapshotReader;
        private readonly ISnapshotCompare _snapshotCompare;

        /// <summary>
        /// creates a new SnapshotClient
        /// </summary>
        public SnapshotClient() 
            : this(new SnapshotReader(), new SnapshotWriter(), new SnapshotCompare())
        {
        }

        /// <summary>
        /// creates a new SnapshotClient
        /// </summary>
        /// <param name="snapshotReader"></param>
        /// <param name="snapshotWriter"></param>
        /// <param name="snapshotCompare"></param>
        public SnapshotClient(ISnapshotReader snapshotReader, ISnapshotWriter snapshotWriter, ISnapshotCompare snapshotCompare)
        {
            _snapshotReader = snapshotReader;
            _snapshotWriter = snapshotWriter;
            _snapshotCompare = snapshotCompare;
        }

        /// <summary>
        /// reads the snapshots from file
        /// </summary>
        /// <param name="snapshotId"></param>
        /// <returns></returns>
        public SnapshotCollection Read(SnapshotId snapshotId)
        {
            return _snapshotReader.Read(snapshotId);
        }

        /// <summary>
        /// Writes the snapshot to the file
        /// </summary>
        /// <param name="snapshot"></param>
        /// <param name="snapshotId"></param>
        public void Write(Snapshot snapshot, SnapshotId snapshotId)
        {
            _snapshotWriter.Write(snapshot, snapshotId);
        }

        /// <summary>
        /// validates the snapshot against the saved snapshot
        /// </summary>
        /// <param name="snapshotId"></param>
        /// <param name="snapshot"></param>
        /// <returns></returns>
        public SnapshotResult Validate(SnapshotId snapshotId, Snapshot snapshot)
        {
            var snapshots = Read(snapshotId);
            var savedToken = snapshots?.GetSnapshot(snapshot.Metadata);

            return _snapshotCompare.Compare(snapshot, savedToken);
        }
    }
}
