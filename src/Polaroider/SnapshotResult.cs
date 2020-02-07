
namespace Polaroider
{
    /// <summary>
    /// the result of the snapshotcompare
    /// </summary>
    public class SnapshotResult
    {
        private SnapshotResult(SnapshotStatus status, Snapshot oldSnapshot, Snapshot newSnapshot, int index)
        {
            Status = status;
            OldSnapshot = oldSnapshot;
            NewSnapshot = newSnapshot;
            Index = index;
        }

        /// <summary>
        /// the state of the compare
        /// </summary>
        public SnapshotStatus Status { get; }

        /// <summary>
        /// gets the saved snapshot
        /// </summary>
        public Snapshot OldSnapshot { get; }

        /// <summary>
        /// gets the new snapshot
        /// </summary>
        public Snapshot NewSnapshot { get; }

        /// <summary>
        /// gets the line index at which the snapshots do not match
        /// </summary>
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
