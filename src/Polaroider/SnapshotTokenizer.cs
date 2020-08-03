using Polaroider.Mapping;
using System;

namespace Polaroider
{
    /// <summary>
    /// Tokenize snapshots
    /// </summary>
    public class SnapshotTokenizer
    {
	    /// <summary>
	    /// create a snapshot token of the string
	    /// </summary>
	    /// <param name="snapshot"></param>
	    /// <returns></returns>
	    public static Snapshot Tokenize(string snapshot)
		    => Tokenize(snapshot, (SnapshotOptions) null);

		/// <summary>
		/// create a snapshot token of the string
		/// </summary>
		/// <param name="snapshot"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public static Snapshot Tokenize(string snapshot, SnapshotOptions options)
		{
			options = options.MergeDefault();
			var parser = options.Parser ?? LineParser.Default;

			var token = new Snapshot();
            var lines = snapshot.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                token.Add(parser.Parse(line));
            }

            return token;
        }

		/// <summary>
		/// create a snapshot token of the object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="snapshot"></param>
		/// <returns></returns>
		[Obsolete("Use MapToToken instead", false)]
		public static Snapshot Tokenize<T>(T snapshot)
        {
            var mapper = ObjectMapper.Mapper.GetMapper(typeof(T));
            return mapper.Map(snapshot);
        }

		/// <summary>
		/// create a snapshot token of the object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="snapshot"></param>
		/// <returns></returns>
		public static Snapshot MapToToken<T>(T snapshot)
		{
			var mapper = ObjectMapper.Mapper.GetMapper(typeof(T));
			return mapper.Map(snapshot);
		}
	}
}
