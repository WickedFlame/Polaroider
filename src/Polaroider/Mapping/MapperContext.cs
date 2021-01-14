using System;
using System.Collections.Generic;
using System.Text;

namespace Polaroider.Mapping
{
	public class MapperContext
	{
		private readonly int _indentation;
		private readonly Snapshot _snapshot;

		public MapperContext(Snapshot snapshot, int indentation)
		{
			_indentation = indentation;
			_snapshot = snapshot;
		}

		public void AddLine(string name, object value)
		{
			//TODO: type mapping of string....
			AddLine(BuildLine(name, value?.ToString()));
		}

		public void AddLine(Line line)
		{
			_snapshot.Add(line);
		}

		public Line BuildLine(string name, string value)
		{
			var line = $"{name}:".Indent(_indentation);
			return new Line($"{line} {value}");
		}
	}
}
