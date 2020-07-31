
namespace Polaroider
{
	/// <summary>
	/// interface to compare lines
	/// </summary>
	public interface ILineCompare
	{
		/// <summary>
		/// compare lines
		/// </summary>
		/// <param name="newLine"></param>
		/// <param name="savedLine"></param>
		/// <returns></returns>
		bool Compare(Line newLine, Line savedLine);
	}
}
