
namespace Polaroider
{
    public class SnapshotResult
    {
        private SnapshotResult(SnapshotStatus status, Snapshot oldSnapshot, Snapshot newSnapshot, int index)
        {
            Status = status;
            OldSnapshot = oldSnapshot;
            NewSnapshot = newSnapshot;
            Index = index;
        }

        public SnapshotStatus Status { get; }

        public Snapshot OldSnapshot { get; }

        public Snapshot NewSnapshot { get; }

        public int Index { get; }

        internal static SnapshotResult SnapshotDoesNotExist(Snapshot snapshot)
            => new SnapshotResult(SnapshotStatus.SnapshotDoesNotExist, null, snapshot, -1);

        internal static SnapshotResult SnapshotsMatch(Snapshot newSnapshot, Snapshot savedSnapshot)
            => new SnapshotResult(SnapshotStatus.SnapshotsMatch, savedSnapshot, newSnapshot, -1);

        internal static SnapshotResult SnapshotsDoNotMatch(Snapshot newSnapshot, Snapshot savedSnapshot, int index)
            => new SnapshotResult(SnapshotStatus.SnapshotsDoNotMatch, savedSnapshot, newSnapshot, index);

        internal static SnapshotResult SnapshotUpdated(Snapshot newSnapshot, Snapshot savedSnapshot)
            => new SnapshotResult(SnapshotStatus.SnapshotUpdated, savedSnapshot, newSnapshot, -1);
    }
}
