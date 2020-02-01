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
            => MatchSnapshot(snapshot, null);

        /// <summary>
        /// Compares the provided object with the saved snapshot that has the corresponding Id
        /// </summary>
        /// <param name="snapshot">The object to comapre</param>
        /// <param name="id">The Id of the stored snapshot</param>
        public static void MatchSnapshot(this string snapshot, string id)
        {
            var newToken = SnapshotTokenizer.Tokenize(snapshot);

            var resolver = new SnapshotIdResolver();
            var snapshotId = resolver.ResloveId();

            var client = GetClient();
            var result = client.Validate(snapshotId, newToken, id);
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

        /// <summary>
        /// Add metatdata to the snapshot
        /// </summary>
        /// <typeparam name="T">The type of metadata</typeparam>
        /// <param name="snapshot">The snapshot to add the metadata to</param>
        /// <param name="obj">The object containing the metadata</param>
        /// <returns></returns>
        public static Snapshot SetMetadata<T>(this Snapshot snapshot, Func<T> obj)
        {
            var data = obj.Invoke();
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
