using System.IO;

namespace Polaroider
{
    /// <summary>
    /// Extensions for SnapshotSetup
    /// </summary>
    public static class SnapshotSetupExtensions
    {
        /// <summary>
        /// Gets the full path with the filename
        /// </summary>
        /// <param name="setup"></param>
        /// <returns></returns>
        public static string GetFilePath(this SnapshotSetup setup)
        {
            var className = setup.ClassName;
            if (className.Contains("<"))
            {
                className = setup.FileName.Replace(".cs", string.Empty);
            }

            var method = setup.MethodName.Replace("<", string.Empty).Replace(">", string.Empty);

            return Path.Combine(setup.Directory, "_Snapshots", $"{className}_{method}.snapshot");
        }
    }
}
