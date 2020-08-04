using System;
using System.Collections.Generic;
using System.Text;

namespace Polaroider.SnapshotReaders
{
    internal class SettingsReader : ILineReader
    {
        public void ReadLine(string line, Snapshot item)
        {
        }

        public bool NewSnapshot(Snapshot snapshot)
        {
            return false;
        }
    }
}
