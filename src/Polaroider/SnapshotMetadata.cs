using System.Collections;
using System.Collections.Generic;

namespace Polaroider
{
    public class SnapshotMetadata : IEnumerable<KeyValuePair<string, string>>
    {
        private readonly Dictionary<string, string> _items = new Dictionary<string, string>();

        public int Count => _items.Count;

        public void Add(string key, object value)
        {
            _items.Add(key, value.ToString());
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool ContainsKey(string key)
        {
            return _items.ContainsKey(key);
        }

        public string this[string key] => _items[key];
    }
}
