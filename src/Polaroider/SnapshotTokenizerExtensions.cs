namespace Polaroider
{
	/// <summary>
	/// Extensions for <see cref="SnapshotTokenizer"/>
	/// </summary>
	public static class SnapshotTokenizerExtensions
	{
		/// <summary>
		/// create a tokenized snapshot of the object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="token"></param>
		/// <returns></returns>
		public static Snapshot Tokenize<T>(this T token)
		{
			return SnapshotTokenizer.Tokenize(token);
		}

		/// <summary>
		/// create a tokenized snapshot of the object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="token"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public static Snapshot Tokenize<T>(this T token, SnapshotOptions options)
		{
			return SnapshotTokenizer.Tokenize(token, options);
		}
	}
}
