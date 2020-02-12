using System.IO;

namespace Polaroider
{
    /// <summary>
    /// write snapshots to file
    /// </summary>
    public class SnapshotWriter : ISnapshotWriter
    {
        /// <summary>
        /// write the snapshot to file
        /// </summary>
        /// <param name="snapshot"></param>
        /// <param name="setup"></param>
        public void Write(Snapshot snapshot, SnapshotSetup setup)
        {
            var collection = new SnapshotCollection();
            collection.Add(snapshot);

            var file = setup.GetFilePath();
            if (File.Exists(file))
            {
                var reader = new SnapshotReader();
                var tmp = reader.Read(setup);

                foreach (var token in tmp)
                {
                    if (!token.SnapshotContainsMetadata(snapshot.Metadata))
                    {
                        collection.Add(token);
                    }
                }
            }

            Directory.CreateDirectory(Path.GetDirectoryName(setup.GetFilePath()));

            using (var writer = new StreamWriter(setup.GetFilePath(), false))
            {
                foreach(var token in collection)
                {
                    if (snapshot.HasMetadata())
                    {
                        writer.WriteLine("---metadata");
                    }

                    foreach (var info in token.Metadata)
                    {
                        writer.WriteLine($"{info.Key}: {info.Value}");
                    }

                    writer.WriteLine("---data");

                    foreach (var line in token)
                    {
                        writer.WriteLine(line.Value);
                    }
                }
            }
        }
    }
}
