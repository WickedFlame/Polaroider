
namespace Polaroider
{
    internal class SnapshotCompare : ISnapshotCompare
    {
        public SnapshotResult Compare(Snapshot newshot, Snapshot savedshot)
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

            for (var i = 0; i < count; i++)
            {
                if (!newshot[i].Equals(savedshot[i]))
                {
                    return SnapshotResult.SnapshotsDoNotMatch(newshot, savedshot, i);
                }
            }

            return SnapshotResult.SnapshotsMatch(newshot, savedshot);
        }
    }
}
