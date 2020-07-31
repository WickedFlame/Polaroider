
using System;

namespace Polaroider
{
	public delegate bool LineComparer(Line newLine, Line savedLine);

    public class SnapshotCompare : ISnapshotCompare
    {
		private static readonly LineComparer _defaultComparer = (newLine, savedLine) => newLine.Equals(savedLine);

		/// <summary>
		/// compare two snapshots with each other
		/// </summary>
		/// <param name="newshot"></param>
		/// <param name="savedshot"></param>
		/// <returns></returns>
		public SnapshotResult Compare(Snapshot newshot, Snapshot savedshot) 
		    => Compare(newshot, savedshot, SnapshotConfig.Default);

		public SnapshotResult Compare(Snapshot newshot, Snapshot savedshot, SnapshotConfig config)
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

            var comparer = config.LineComparer ?? _defaultComparer;

            for (var i = 0; i < count; i++)
            {
				if (!comparer(newshot[i], savedshot[i]))
				{
                    return SnapshotResult.SnapshotsDoNotMatch(newshot, savedshot, i);
                }
            }

            return SnapshotResult.SnapshotsMatch(newshot, savedshot);
        }
    }
}
