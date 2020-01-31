using System.IO;

namespace Polaroid
{
    public static class SnapshotIdExtensions
    {
        /// <summary>
        /// Gets the full path with the filename
        /// </summary>
        /// <param name="snapshotId"></param>
        /// <returns></returns>
        public static string GetFilePath(this SnapshotId snapshotId)
        {
            return Path.Combine(snapshotId.Directory, "_Snapshots", $"{snapshotId.ClassName}_{snapshotId.MethodName}.snapshot");
        }
    }
}
