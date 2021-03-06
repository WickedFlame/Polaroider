﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Polaroider
{
    public class SnapshotCollection : IEnumerable<Snapshot>
    {
        private readonly List<Snapshot> _snapshots = new List<Snapshot>();

        public IEnumerator<Snapshot> GetEnumerator()
        {
            return _snapshots.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Snapshot snapshot)
        {
            _snapshots.Add(snapshot);
        }

        public Snapshot GetSnapshot(SnapshotMetadata metadata)
        {
            return _snapshots.FirstOrDefault(s => s.SnapshotContainsMetadata(metadata));
        }
    }
}
