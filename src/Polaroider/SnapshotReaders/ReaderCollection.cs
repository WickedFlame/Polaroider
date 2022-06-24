using System;
using System.Collections.Generic;
using System.Text;

namespace Polaroider.SnapshotReaders
{
    /// <summary>
    /// Collection of ILineReaders
    /// </summary>
    public class ReaderCollection
    {
        private readonly Dictionary<ReaderType, ILineReader> _readers = new Dictionary<ReaderType, ILineReader>();
        private static ReaderCollection _collection;

        /// <summary>
        /// Add a new linereader to the collection
        /// </summary>
        /// <param name="type"></param>
        /// <param name="reader"></param>
        public void Add(ReaderType type, ILineReader reader)
        {
            _readers.Add(type, reader);
        }

        /// <summary>
        /// Get the <see cref="ILineReader"/> associated with the <see cref="ReaderType"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ILineReader Get(ReaderType type)
        {
            return _readers[type];
        }

        /// <summary>
        /// Get the <see cref="ILineReader"/> associated with the <see cref="ReaderType"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ILineReader this[ReaderType type] => _readers[type];

        /// <summary>
        /// Gets the static ReaderCollection
        /// </summary>
        public static ReaderCollection Collection
        {
            get
            {
                if (_collection == null)
                {
                    _collection = new ReaderCollection();
                    _collection.Add(ReaderType.data, new SnapshotLineReader());
                    _collection.Add(ReaderType.metadata, new MetadataReader());
                    _collection.Add(ReaderType.settings, new SettingsReader());
                }

                return _collection;
            }
        }
    }
}
