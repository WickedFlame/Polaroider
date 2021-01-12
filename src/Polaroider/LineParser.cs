using System;
using System.Collections.Generic;

namespace Polaroider
{
	/// <summary>
	/// the default line parser
	/// </summary>
	public class LineParser : ILineParser
	{
		private readonly List<Func<string,string>> _directives;

		/// <summary>
		/// creates an instance of LineParser
		/// </summary>
		public LineParser()
		{
			_directives = new List<Func<string, string>>();
		}

		/// <summary>
		/// parse the line
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public Line Parse(string line)
		{
			foreach (var direcive in _directives)
			{
				line = direcive(line);
			}

			return new Line(line);
		}

		/// <summary>
		/// parse the line
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public Line Parse(Line line)
		{
			var value = line.Value;
			foreach (var direcive in _directives)
			{
				value = direcive(value);
			}

			return new Line(value);
		}

		/// <summary>
		/// add a directive to the parser
		/// </summary>
		/// <param name="directive"></param>
		/// <returns></returns>
		public ILineParser AddDirective(Func<string, string> directive)
		{
			_directives.Add(directive);
			return this;
		}
	}
}
