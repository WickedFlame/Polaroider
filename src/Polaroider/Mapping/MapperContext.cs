using System.Diagnostics;

namespace Polaroider.Mapping
{
	/// <summary>
	/// the mappercontext used for the mapper
	/// </summary>
	public class MapperContext
	{
		/// <summary>
		/// Creates a new instance of the mapper context
		/// </summary>
		/// <param name="mapper"></param>
		/// <param name="snapshot"></param>
		/// <param name="options"></param>
		/// <param name="indentation"></param>
		[DebuggerStepThrough]
		public MapperContext(ITypeMapper mapper, Snapshot snapshot, SnapshotOptions options, int indentation)
		{
			Mapper = mapper;
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
		/// The default instance of the TypeMapper to be able to map objects to
		/// </summary>
		public ITypeMapper Mapper { get; }

		/// <summary>
		/// Add a string line to the snapshot
		/// </summary>
		/// <param name="value"></param>
		public void AddLine(string value)
		{
			AddLine(new Line(value.Indent(Indentation)));
		}

		/// <summary>
		/// add a line to the snapshot
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public void AddLine(string name, object value)
		{
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
