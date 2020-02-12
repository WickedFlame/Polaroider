using System.IO;

namespace Polaroider
{
    public static class SnapshotSetupExtensions
    {
        /// <summary>
        /// Gets the full path with the filename
        /// </summary>
        /// <param name="setup"></param>
        /// <returns></returns>
        public static string GetFilePath(this SnapshotSetup setup)
        {
            return Path.Combine(setup.Directory, "_Snapshots", $"{setup.ClassName}_{setup.MethodName}.snapshot");
        }
    }
}
