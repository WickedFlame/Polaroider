using System;

namespace Polaroider
{
	/// <summary>
	/// 
	/// </summary>
	public class LineCompare : ILineCompare
	{
		private readonly Func<Line, Line, bool> _compare;

		/// <summary>
		/// creates an instanc of LineComparer
		/// </summary>
		/// <param name="compare"></param>
		public LineCompare(Func<Line, Line, bool> compare)
		{
			_compare = compare;
		}

		/// <summary>
		/// gets the default comparer
		/// </summary>
		public static ILineCompare Default => new LineCompare((newLine, savedLine) => newLine.Equals(savedLine));

		/// <summary>
		/// compare lines
		/// </summary>
		/// <param name="newLine"></param>
		/// <param name="savedLine"></param>
		/// <returns></returns>
		public bool Compare(Line newLine, Line savedLine)
		{
			return _compare(newLine, savedLine);
		}
	}
}
