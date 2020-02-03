
namespace Polaroider
{
    public interface ILineReader
    {
        void ReadLine(string line, Snapshot snapshot);

        /// <summary>
        /// Checks if it is needed to create a new Snapshot after the current shot
        /// </summary>
        /// <param name="snapshot"></param>
        /// <returns></returns>
        bool NewSnapshot(Snapshot snapshot);
    }
}
