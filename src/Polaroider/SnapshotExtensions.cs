using System;
using System.Linq;

namespace Polaroider
{
    public static class SnapshotExtensions
    {
        private static Lazy<ISnapshotClient> Client = new Lazy<ISnapshotClient>(() => new SnapshotClient());
        private static ISnapshotClient GetClient() => Client.Value;

        /// <summary>
        /// Compares the provided object with the saved snapshot
        /// </summary>
        /// <param name="snapshot">The object to comapre</param>
        public static void MatchSnapshot(this string snapshot)
        {
            SnapshotTokenizer.Tokenize(snapshot)
                .MatchSnapshot();
        }

        /// <summary>
        /// Compares the provided object with the saved snapshot that has the corresponding Id
        /// </summary>
        /// <param name="snapshot">The object to comapre</param>
        /// <param name="meta">The Id of the stored snapshot</param>
        public static void MatchSnapshot<T>(this string snapshot, Func<T> meta)
        {
            SnapshotTokenizer.Tokenize(snapshot)
                .SetMetadata(meta)
                .MatchSnapshot();
        }

        /// <summary>
        /// Compares the provided snapshot with the saved snapshot
        /// </summary>
        /// <param name="snapshot">The snapshot to compare</param>
        public static void MatchSnapshot(this Snapshot snapshot)
        {
            var resolver = new SnapshotIdResolver();
            var snapshotId = resolver.ResloveId();

            var client = GetClient();
            var result = client.Validate(snapshotId, snapshot);
            SnapshotAsserter.AssertSnapshot(result);
        }

        /// <summary>
        /// Add metatdata to the snapshot
        /// </summary>
        /// <typeparam name="T">The type of metadata</typeparam>
        /// <param name="snapshot">The snapshot to add the metadata to</param>
        /// <param name="meta">The object containing the metadata</param>
        /// <returns></returns>
        public static Snapshot SetMetadata<T>(this Snapshot snapshot, Func<T> meta)
        {
            var data = meta.Invoke();
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

        internal static bool HasMetadata(this Snapshot snapshot)
        {
            return snapshot.Metadata.Count > 0;
        }

        internal static bool SnapshotContainsMetadata(this Snapshot snapshot, SnapshotMetadata metadata)
        {
            if (metadata.All(item => snapshot.Metadata.ContainsKey(item.Key) && snapshot.Metadata[item.Key] == item.Value))
            {
                return true;
            }

            return false;
        }
    }
}
