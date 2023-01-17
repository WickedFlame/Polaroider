
namespace Polaroider
{
	/// <summary>
	/// compare snapshots
	/// </summary>
    public class SnapshotCompare : ISnapshotCompare
    {
        /// <summary>
        /// compare two snapshots with each other
        /// </summary>
        /// <param name="newSnapshot"></param>
        /// <param name="savedSnapshot"></param>
        /// <param name="options">the options</param>
        /// <returns></returns>
        public SnapshotResult Compare(Snapshot newSnapshot, Snapshot savedSnapshot, SnapshotOptions options)
        {
            if (newSnapshot == null || savedSnapshot == null)
            {
                return SnapshotResult.SnapshotDoesNotExist(savedSnapshot);
            }

            var count = newSnapshot.Count;
            if (count < savedSnapshot.Count)
            {
                count = savedSnapshot.Count;
            }

            var comparer = options.Comparer ?? LineCompare.Default;

            for (var i = 0; i < count; i++)
            {
				if (!comparer.Compare(newSnapshot[i], savedSnapshot[i]))
				{
                    return SnapshotResult.SnapshotsDoNotMatch(newSnapshot, savedSnapshot, i);
                }
            }

            return SnapshotResult.SnapshotsMatch(newSnapshot, savedSnapshot);
        }
    }
}
