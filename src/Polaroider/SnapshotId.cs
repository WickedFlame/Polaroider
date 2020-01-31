using System.IO;
using System.Reflection;

namespace Polaroid
{
    public class SnapshotId
    {
        private readonly MethodBase _method;
        private readonly string _fullPath;

        public SnapshotId(string fileName, MethodBase method)
        {
            _fullPath = fileName;
            _method = method;

            FileName = Path.GetFileName(fileName);
            Directory = Path.GetDirectoryName(fileName);
        }

        public string FileName { get; }

        public string Directory { get; }

        public string MethodName => _method.Name;

        public string ClassName => _method.DeclaringType?.Name;
    }
}
