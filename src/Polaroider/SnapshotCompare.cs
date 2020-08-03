
using System;

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
		/// <param name="newshot"></param>
		/// <param name="savedshot"></param>
		/// <param name="options">the options</param>
		/// <returns></returns>
		public SnapshotResult Compare(Snapshot newshot, Snapshot savedshot, SnapshotOptions options)
        {
            if (newshot == null || savedshot == null)
            {
                return SnapshotResult.SnapshotDoesNotExist(savedshot);
            }

            var count = newshot.Count;
            if (count < savedshot.Count)
            {
                count = savedshot.Count;
            }

            var comparer = options.Comparer ?? LineCompare.Default;

            for (var i = 0; i < count; i++)
            {
				if (!comparer.Compare(newshot[i], savedshot[i]))
				{
                    return SnapshotResult.SnapshotsDoNotMatch(newshot, savedshot, i);
                }
            }

            return SnapshotResult.SnapshotsMatch(newshot, savedshot);
        }
    }
}
