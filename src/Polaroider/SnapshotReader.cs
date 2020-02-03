using System;
using System.IO;
using Polaroider.SnapshotReaders;

namespace Polaroider
{
    public class SnapshotReader : ISnapshotReader
    {
        private readonly ReaderCollection _dataReaders;

        public SnapshotReader()
        {
            _dataReaders = ReaderCollection.Collection;
        }

        private string[] ReadAllLines(string file)
        {
            return File.ReadAllLines(file);
        }

        /// <summary>
        /// Loads the testdata for the given type.
        /// The name of the type defines the name of the file containing the testdata.
        /// If the type ends with Test or Tests the name of the tesdata file will be used without the ending
        /// </summary>
        /// <returns></returns>
        public SnapshotCollection Read(SnapshotId snapshotId)
        {
            var file = snapshotId.GetFilePath();

            if (!File.Exists(file))
            {
                return null;
            }

            var snapshots = new SnapshotCollection();
            Snapshot snapsot = null;

            var reader = _dataReaders[ReaderType.data];

            foreach (var line in ReadAllLines(file))
            {
                if (line.StartsWith("'Errors", System.StringComparison.InvariantCultureIgnoreCase) || line.StartsWith("'Raw", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return snapshots;
                }

                if (line.StartsWith("'"))
                {
                    continue;
                }

                if (line == "---")
                {
                    reader = _dataReaders[ReaderType.data];
                    continue;
                }

                if (line.StartsWith("---") && line.Length > 3)
                {
                    var property = line.Substring(3);
                    if (Enum.TryParse<ReaderType>(property, out var key))
                    {
                        reader = _dataReaders[key];
                        if(reader.NewSnapshot(snapsot))
                        {
                            snapsot = null;
                        }
                        continue;
                    }
                }

                if (snapsot == null)
                {
                    snapsot = new Snapshot();
                    snapshots.Add(snapsot);
                }

                reader.ReadLine(line, snapsot);
            }

            return snapshots;
        }
    }
}
