using System.Collections;
using System.Collections.Generic;

namespace Polaroider
{
    /// <summary>
    /// Metadata of a snapshot. These are set when creating the snapshot
    /// </summary>
    public class SnapshotMetadata : IEnumerable<KeyValuePair<string, string>>
    {
        private readonly Dictionary<string, string> _items = new Dictionary<string, string>();

        /// <summary>
        /// Gets the count of entries
        /// </summary>
        public int Count => _items.Count;

        /// <summary>
        /// Add a value to the metadata
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, object value)
        {
            _items.Add(key, value.ToString());
        }

        /// <summary>
        /// Get the enumerator for itterating the collection
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns true when the collection contains the key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return _items.ContainsKey(key);
        }

        /// <summary>
        /// Get the entry based on the key indexer
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key] => _items[key];
    }
}
