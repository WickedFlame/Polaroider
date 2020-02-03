
namespace Polaroider.SnapshotReaders
{
    public class SnapshotLineReader : ILineReader
    {
        public void ReadLine(string line, Snapshot item)
        {
            // set data row
            var row = new Line(line);
            item.Add(row);
        }
        
        public bool NewSnapshot(Snapshot snapshot)
        {
            return false;
        }
    }
}
