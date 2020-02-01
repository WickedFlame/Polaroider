
using System.Linq;

namespace Polaroider
{
    internal class SnapshotClient : ISnapshotClient
    {
        private readonly ISnapshotWriter _snapshotWriter;
        private readonly ISnapshotReader _snapshotReader;
        private readonly ISnapshotCompare _snapshotCompare;

        public SnapshotClient() 
            : this(new SnapshotReader(), new SnapshotWriter(), new SnapshotCompare())
        {
        }

        public SnapshotClient(ISnapshotReader snapshotReader, ISnapshotWriter snapshotWriter, ISnapshotCompare snapshotCompare)
        {
            _snapshotReader = snapshotReader;
            _snapshotWriter = snapshotWriter;
            _snapshotCompare = snapshotCompare;
        }

        public SnapshotCollection Read(SnapshotId snapshotId)
        {
            return _snapshotReader.Read(snapshotId);
        }

        public void Write(Snapshot snapshot, SnapshotId snapshotId)
        {
            _snapshotWriter.Write(snapshot, snapshotId);
        }

        public SnapshotResult Validate(SnapshotId snapshotId, Snapshot snapshot, string id = null)
        {
            var snapshots = Read(snapshotId);
            if (snapshots == null)
            {
                Write(snapshot, snapshotId);
                return SnapshotResult.SnapshotUpdated(snapshot, null);
            }

            var savedToken = snapshots?.FirstOrDefault(s => s.GetId() == id);

            return _snapshotCompare.Compare(snapshot, savedToken);
        }
    }
}
