
namespace Polaroid
{
    public class SnapshotResult
    {
        private SnapshotResult(SnapshotStatus status, object oldSnapshot, object newSnapshot, int index)
        {
            Status = status;
            OldSnapshot = oldSnapshot;
            NewSnapshot = newSnapshot;
            Index = index;
        }

        public SnapshotStatus Status { get; }

        public object OldSnapshot { get; }

        public object NewSnapshot { get; }

        public int Index { get; }

        public static SnapshotResult SnapshotDoesNotExist(Snapshot snapshot)
            => new SnapshotResult(SnapshotStatus.SnapshotDoesNotExist, null, snapshot, -1);

        public static SnapshotResult SnapshotsMatch(Snapshot newSnapshot, Snapshot savedSnapshot)
            => new SnapshotResult(SnapshotStatus.SnapshotsMatch, savedSnapshot, newSnapshot, -1);

        public static SnapshotResult SnapshotsDoNotMatch(Snapshot newSnapshot, Snapshot savedSnapshot, int index)
            => new SnapshotResult(SnapshotStatus.SnapshotsDoNotMatch, savedSnapshot, newSnapshot, index);

        public static SnapshotResult SnapshotUpdated(Snapshot newSnapshot, Snapshot savedSnapshot)
            => new SnapshotResult(SnapshotStatus.SnapshotUpdated, savedSnapshot, newSnapshot, -1);
    }
}
