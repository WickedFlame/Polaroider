using System;
using System.Collections.Generic;
using System.Text;

namespace Polaroider
{
	/// <summary>
	/// the options for the snapshots
	/// </summary>
	public class SnapshotOptions
	{
		/// <summary>
		/// gets the default configuration
		/// </summary>
		public static SnapshotOptions Default { get; private set; } = new SnapshotOptions();

		/// <summary>
		/// the configured comparer
		/// </summary>
		public ILineCompare Comparer { get; set; }

		/// <summary>
		/// the configured line parser
		/// </summary>
		public ILineParser Parser { get; set; }

		/// <summary>
		/// update the snapshot
		/// </summary>
		public bool UpdateSnapshot { get; set; }

		/// <summary>
		/// setup the default options
		/// </summary>
		/// <param name="setup"></param>
		public static void Setup(Action<SnapshotOptions> setup)
		{
			var options = new SnapshotOptions();
			setup(options);

			Default = options;
		}

		/// <summary>
		/// create a set of options
		/// </summary>
		/// <param name="setup"></param>
		public static SnapshotOptions Create(Action<SnapshotOptions> setup)
		{
			var options = new SnapshotOptions();
			setup(options);

			return options;
		}
	}

	/// <summary>
	/// extensions for snapshotoptions
	/// </summary>
	public static class SnapshotOptionsExtensions
	{
		internal static SnapshotOptions MergeDefault(this SnapshotOptions options)
		{
			if (options == null)
			{
				return SnapshotOptions.Default;
			}

			options.Comparer = options.Comparer ?? SnapshotOptions.Default.Comparer;
			options.UpdateSnapshot = options.UpdateSnapshot ? options.UpdateSnapshot : SnapshotOptions.Default.UpdateSnapshot;
			options.Parser = options.Parser ?? SnapshotOptions.Default.Parser;

			return options;
		}

		/// <summary>
		/// alter the way lines are compared
		/// </summary>
		/// <param name="options"></param>
		/// <param name="comparer"></param>
		/// <returns></returns>
		public static SnapshotOptions SetComparer(this SnapshotOptions options, Func<Line, Line, bool> comparer)
		{
			options.Comparer = new LineCompare(comparer);
			return options;
		}

		/// <summary>
		/// add a directive to the tokenizer to alter the input value that used for the compare
		/// </summary>
		/// <param name="options"></param>
		/// <param name="directive"></param>
		/// <returns></returns>
		public static SnapshotOptions AddDirective(this SnapshotOptions options, Func<string, string> directive)
		{
			if (options.Parser == null)
			{
				options.Parser = new LineParser();
			}

			options.Parser.AddDirective(directive);
			return options;
		}

		/// <summary>
		/// sets updatesnapshot
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public static SnapshotOptions UpdateSavedSnapshot(this SnapshotOptions options)
		{
			options.UpdateSnapshot = true;
			return options;
		}
	}
}
