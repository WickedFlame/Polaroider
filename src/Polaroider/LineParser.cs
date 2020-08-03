using System;

namespace Polaroider
{
	public class LineParser : ILineParser
	{
		private readonly Func<string, Line> _parser;

		/// <summary>
		/// creates an instance of LineParser
		/// </summary>
		/// <param name="parser"></param>
		public LineParser(Func<string, Line> parser)
		{
			_parser = parser;
		}

		/// <summary>
		/// gets the default parser
		/// </summary>
		public static ILineParser Default => new LineParser(line => new Line(line));

		/// <summary>
		/// parse the line
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public Line Parse(string line)
		{
			return _parser(line);
		}
	}
}
