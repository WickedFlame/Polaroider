using System;
using System.Collections.Generic;

namespace Polaroider
{
	/// <summary>
	/// interface for parsind lines
	/// </summary>
	public interface ILineParser
	{
        /// <summary>
        /// Gets all directives of the parser
        /// </summary>
        IEnumerable<Func<string, string>> Directives { get; }

        /// <summary>
        /// parse the line
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        Line Parse(string line);

		/// <summary>
		/// parse the line
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		Line Parse(Line line);

		/// <summary>
		/// add a parser directive to the parser
		/// </summary>
		/// <param name="directive"></param>
		/// <returns></returns>
		ILineParser AddDirective(Func<string, string> directive);
	}
}
