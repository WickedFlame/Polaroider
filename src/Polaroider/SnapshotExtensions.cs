using System;
using System.Linq;

namespace Polaroider
{
    public static class SnapshotExtensions
    {
        private static Lazy<ISnapshotClient> Client = new Lazy<ISnapshotClient>(() => new SnapshotClient());

        private static ISnapshotClient GetClient() => Client.Value;

        /// <summary>
        /// compares the provided snapshot with the saved snapshot
        /// </summary>
        /// <param name="snapshot">the snapshot to compare</param>
        public static void MatchSnapshot(this Snapshot snapshot)
	        => MatchSnapshot(snapshot, (SnapshotOptions)null);

		/// <summary>
		/// compares the provided snapshot with the saved snapshot
		/// </summary>
		/// <param name="snapshot">the snapshot to compare</param>
		/// <param name="options">the options to use</param>
		public static void MatchSnapshot(this Snapshot snapshot, SnapshotOptions options)
		{
			if(options != null)
			{
				options.MergeDefault();
			}
			else
			{
				options = SnapshotOptions.Default;
			}

            var resolver = new SnapshotSetupResolver();
            var setup = resolver.ResloveSnapshotSetup();

            var client = GetClient();
            var result = client.Validate(snapshot, setup, options);
            if (result.Status == SnapshotStatus.SnapshotDoesNotExist || result.Status == SnapshotStatus.UpdateSnapshot)
            {
                client.Write(snapshot, setup);
                result = SnapshotResult.SnapshotUpdated(snapshot, null);
            }

            SnapshotAsserter.AssertSnapshot(result);
        }

		/// <summary>
		/// compares the provided string with the saved snapshot
		/// </summary>
		/// <param name="snapshot">the string to comapre</param>
		public static void MatchSnapshot(this string snapshot)
			=> MatchSnapshot(snapshot, (SnapshotOptions)null);

		/// <summary>
		/// compares the provided string with the saved snapshot
		/// </summary>
		/// <param name="snapshot">the string to comapre</param>
		/// <param name="options">the configuration</param>
		public static void MatchSnapshot(this string snapshot, SnapshotOptions options)
        {
            SnapshotTokenizer.Tokenize(snapshot, options)
                .MatchSnapshot(options);
        }

		/// <summary>
		/// compares the provided string with the saved snapshot that has the corresponding metadata
		/// </summary>
		/// <param name="snapshot">the string to comapre</param>
		/// <param name="meta">the Id of the stored snapshot</param>
		public static void MatchSnapshot<T>(this string snapshot, Func<T> meta) 
			=> MatchSnapshot(snapshot, meta, (SnapshotOptions)null);

		/// <summary>
		/// compares the provided string with the saved snapshot that has the corresponding metadata
		/// </summary>
		/// <param name="snapshot">the string to comapre</param>
		/// <param name="meta">the Id of the stored snapshot</param>
		/// <param name="options">the configuration</param>
		public static void MatchSnapshot<T>(this string snapshot, Func<T> meta, SnapshotOptions options)
        {
            SnapshotTokenizer.Tokenize(snapshot, options)
                .SetMetadata(meta)
                .MatchSnapshot(options);
        }

		/// <summary>
		/// compares the provided object with the saved snapshot
		/// </summary>
		/// <typeparam name="T">the objecttype</typeparam>
		/// <param name="snapshot">the object to comapre</param>
		public static void MatchSnapshot<T>(this T snapshot)
			=> MatchSnapshot(snapshot, (SnapshotOptions)null);

		/// <summary>
		/// compares the provided object with the saved snapshot
		/// </summary>
		/// <typeparam name="T">the objecttype</typeparam>
		/// <param name="snapshot">the object to comapre</param>
		/// <param name="options">the configuration</param>
		public static void MatchSnapshot<T>(this T snapshot, SnapshotOptions options)
        {
            SnapshotTokenizer.MapToToken(snapshot)
                .MatchSnapshot(options);
        }

		/// <summary>
		/// compares the provided object with the saved snapshot that has the corresponding metadata
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="snapshot"></param>
		/// <param name="meta"></param>
		public static void MatchSnapshot<T>(this T snapshot, Func<object> meta)
			=> MatchSnapshot(snapshot, meta, (SnapshotOptions)null);

		/// <summary>
		/// compares the provided object with the saved snapshot that has the corresponding metadata
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="snapshot"></param>
		/// <param name="meta"></param>
		/// <param name="options">the configuration</param>
		public static void MatchSnapshot<T>(this T snapshot, Func<object> meta, SnapshotOptions options)
        {
            SnapshotTokenizer.MapToToken(snapshot)
                .SetMetadata(meta)
                .MatchSnapshot(options);
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
