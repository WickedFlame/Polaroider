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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="method"></param>
        public SnapshotSetup(string fileName, MethodBase method)
        {
            _method = method;

            FileName = Path.GetFileName(fileName);
            Directory = Path.GetDirectoryName(fileName);

            UpdateSnapshot = _method.GetCustomAttribute<UpdateSnapshotAttribute>() != null || 
                             _method.DeclaringType.GetCustomAttribute<UpdateSnapshotAttribute>() != null;
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

        /// <summary>
        /// indicates if the snapshot has to be updated
        /// </summary>
        public bool UpdateSnapshot { get; }
    }
}
