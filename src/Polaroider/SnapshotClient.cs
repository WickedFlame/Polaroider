
namespace Polaroider
{
    /// <summary>
    /// client object for reading, writing and validating snapshots
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
        /// <param name="setup"></param>
        /// <returns></returns>
        public SnapshotCollection Read(SnapshotSetup setup)
        {
            return _snapshotReader.Read(setup);
        }

        /// <summary>
        /// writes the snapshot to the file
        /// </summary>
        /// <param name="snapshot"></param>
        /// <param name="setup"></param>
        public void Write(Snapshot snapshot, SnapshotSetup setup)
        {
            _snapshotWriter.Write(snapshot, setup);
        }

        /// <summary>
        /// validates the snapshot against the saved snapshot
        /// </summary>
        /// <param name="setup"></param>
        /// <param name="snapshot"></param>
        /// <returns></returns>
        public SnapshotResult Validate(Snapshot snapshot, SnapshotSetup setup)
        {
            var snapshots = Read(setup);
            var savedToken = snapshots?.GetSnapshot(snapshot.Metadata);

            return _snapshotCompare.Compare(snapshot, savedToken);
        }
    }
}
