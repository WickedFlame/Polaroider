using System;
using System.Linq;

namespace Polaroider
{
    public static class SnapshotExtensions
    {
        private static Lazy<ISnapshotClient> Client = new Lazy<ISnapshotClient>(() => new SnapshotClient());
        private static ISnapshotClient GetClient() => Client.Value;

        public static void MatchSnapshot(this string snapshot)
            => MatchSnapshot(snapshot, null);

        public static void MatchSnapshot(this string snapshot, string id)
        {
            var newToken = SnapshotTokenizer.Tokenize(snapshot);

            var resolver = new SnapshotIdResolver();
            var snapshotId = resolver.ResloveId();

            var client = GetClient();
            var snapshots = client.Read(snapshotId);
            if (snapshots == null)
            {
                client.Write(newToken, snapshotId);
                return;
            }
            
            var savedToken = snapshots?.FirstOrDefault(s => s.GetId() == id);

            var result = SnapshotCompare.Compare(newToken, savedToken);
            SnapshotAsserter.AssertSnapshot(result);
        }

        internal static string GetId(this Snapshot snapshot)
        {
            return snapshot.Metadata.ContainsKey("id") ? snapshot.Metadata["id"] : null;
        }

        internal static bool HasMetadata(this Snapshot snapshot)
        {
            return snapshot.Metadata.Any();
        }

        public static Snapshot SetMetadata(this Snapshot snapshot, object data)
        {
            if (data == null)
            {
                return snapshot;
            }

            var properties = data.GetType().GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(data);
                if (value == null)
                {
                    continue;
                }

                snapshot.Metadata.Add(property.Name, value.ToString());
            }

            return snapshot;
        }
    }
}
