
namespace Polaroid
{
    internal class SnapshotClient : ISnapshotClient
    {
        private readonly ISnapshotWriter _snapshotWriter;
        private readonly ISnapshotReader _snapshotReader;

        public SnapshotClient() 
            : this(new SnapshotReader(), new SnapshotWriter())
        {
        }

        public SnapshotClient(ISnapshotReader snapshotReader, ISnapshotWriter snapshotWriter)
        {
            _snapshotReader = snapshotReader;
            _snapshotWriter = snapshotWriter;
        }
        public SnapshotCollection Read(SnapshotId snapshotId)
        {
            return _snapshotReader.Read(snapshotId);
        }

        public void Write(Snapshot snapshot, SnapshotId snapshotId)
        {
            _snapshotWriter.Write(snapshot, snapshotId);
        }
    }
}
