using Polaroider.Mapping;
using System;
using System.Linq;

namespace Polaroider
{
    /// <summary>
    /// Tokenize snapshots
    /// </summary>
    public class SnapshotTokenizer
    {
		/// <summary>
		/// Singleton constructor
		/// </summary>
		protected SnapshotTokenizer() { }

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
			var parser = options.Parser ?? new LineParser();

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
		public static Snapshot Tokenize<T>(T snapshot)
		{
			return MapToToken<T>(snapshot, (SnapshotOptions) null);
		}

		/// <summary>
		/// create a snapshot token of the object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="snapshot"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public static Snapshot Tokenize<T>(T snapshot, SnapshotOptions options)
		{
			return MapToToken<T>(snapshot, options);
		}

		/// <summary>
		/// create a snapshot token of the object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="snapshot"></param>
		/// <returns></returns>
		public static Snapshot MapToToken<T>(T snapshot)
		{
			return MapToToken<T>(snapshot, (SnapshotOptions)null);
		}

		/// <summary>
		/// create a snapshot token of the object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="snapshot"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public static Snapshot MapToToken<T>(T snapshot, SnapshotOptions options)
		{
			options = options.MergeDefault();

			var mapper = ObjectMapper.Mapper.GetMapper(typeof(T));
			var token = mapper.Map(snapshot, options);

			var parser = options.Parser;
			if (parser == null)
			{
				return token;
			}

			for (var i = 0; i < token.Count; i++)
			{
				token[i] = parser.Parse(token[i]);
			}

			return token;
		}
	}
}
