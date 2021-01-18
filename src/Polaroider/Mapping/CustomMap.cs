using System;

namespace Polaroider.Mapping
{
    internal class CustomMap<T> : IObjectMapper where T : class
    {
        private readonly Func<T, Snapshot> _map;

        public CustomMap(Func<T, Snapshot> map)
        {
            _map = map;
        }

        public Snapshot Map<T1>(T1 item, SnapshotOptions options)
        {
            return _map(item as T);
        }
    }
}
