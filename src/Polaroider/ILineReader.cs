
namespace Polaroider
{
	/// <summary>
	/// read the saved snapshot
	/// </summary>
    public interface ILineReader
    {
		/// <summary>
		/// read a line from the saved snapshot
		/// </summary>
		/// <param name="line"></param>
		/// <param name="snapshot"></param>
        void ReadLine(string line, Snapshot snapshot);

        /// <summary>
        /// Checks if it is needed to create a new Snapshot after the current shot
        /// </summary>
        /// <param name="snapshot"></param>
        /// <returns></returns>
        bool NewSnapshot(Snapshot snapshot);
    }
}
