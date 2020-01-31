using System;
using System.Collections.Generic;
using System.Text;

namespace Polaroid.SnapshotReaders
{
    public class SettingsReader : ILineReader
    {
        public void ReadLine(string line, Snapshot item)
        {
        }

        public void Reset()
        {
        }

        public bool NewSnapshot(Snapshot snapshot)
        {
            return false;
        }
    }
}
