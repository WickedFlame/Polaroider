using System.IO;
using System.Reflection;

namespace Polaroider
{
    /// <summary>
    /// SnapshotSetup
    /// </summary>
    public class SnapshotSetup
    {
        private readonly MethodBase _method;
        private readonly string _fullPath;

        public SnapshotSetup(string fileName, MethodBase method)
        {
            _fullPath = fileName;
            _method = method;

            FileName = Path.GetFileName(fileName);
            Directory = Path.GetDirectoryName(fileName);
        }

        /// <summary>
        /// the filename of the testclass
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// the directory containing the testclass
        /// </summary>
        public string Directory { get; }

        /// <summary>
        /// the name of the testmethod
        /// </summary>
        public string MethodName => _method.Name;

        /// <summary>
        /// the testclass name
        /// </summary>
        public string ClassName => _method.DeclaringType?.Name;
    }
}
