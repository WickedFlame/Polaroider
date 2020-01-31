using System;
using System.Collections.Generic;
using System.Text;

namespace Polaroid.SnapshotReaders
{
    public class ReaderCollection
    {
        private readonly Dictionary<ReaderType, ILineReader> _readers = new Dictionary<ReaderType, ILineReader>();
        private static ReaderCollection _collection;

        public void Add(ReaderType type, ILineReader reader)
        {
            _readers.Add(type, reader);
        }

        public ILineReader Get(ReaderType type)
        {
            return _readers[type];
        }

        public ILineReader this[ReaderType type]
        {
            get { return _readers[type]; }
        }

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
