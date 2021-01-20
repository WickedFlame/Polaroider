namespace Polaroider.Mapping
{
	/// <summary>
	/// the mappercontext used for the mapper
	/// </summary>
	public class MapperContext
	{
		public MapperContext(Snapshot snapshot, SnapshotOptions options, int indentation)
		{
			Indentation = indentation;
			Options = options;
			Snapshot = snapshot;
		}

		/// <summary>
		/// the indentation of the line for the snapshot
		/// </summary>
		public int Indentation { get; }

		/// <summary>
		/// the options
		/// </summary>
		public SnapshotOptions Options { get; }

		/// <summary>
		/// the snapshot
		/// </summary>
		public Snapshot Snapshot { get; }

		/// <summary>
		/// add a line to the snapshot
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public void AddLine(string name, object value)
		{
			//TODO: type mapping of string....
			AddLine(BuildLine(name, value?.ToString()));
		}

		/// <summary>
		/// add a line to the snapshot
		/// </summary>
		/// <param name="line"></param>
		public void AddLine(Line line)
		{
			Snapshot.Add(line);
		}

		/// <summary>
		/// create a line that can be added to the snapshot
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public Line BuildLine(string name, string value)
		{
			var line = $"{name}:".Indent(Indentation);
			return new Line($"{line} {value}");
		}
	}
}
