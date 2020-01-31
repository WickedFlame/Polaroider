using System.IO;

namespace Polaroid
{
    public class SnapshotWriter : ISnapshotWriter
    {
        public void Write(Snapshot snapshot, SnapshotId snapshotId)
        {
            var collection = new SnapshotCollection();
            collection.Add(snapshot);

            var file = snapshotId.GetFilePath();
            if (File.Exists(file))
            {
                var reader = new SnapshotReader();
                var tmp = reader.Read(snapshotId);
                foreach (var token in tmp)
                {
                    if (snapshot.GetId() != token.GetId())
                    {
                        collection.Add(token);
                    }
                }
            }

            Directory.CreateDirectory(Path.GetDirectoryName(snapshotId.GetFilePath()));

            using (var writer = new StreamWriter(snapshotId.GetFilePath(), false))
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
