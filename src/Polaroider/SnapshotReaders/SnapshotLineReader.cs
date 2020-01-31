
namespace Polaroid.SnapshotReaders
{
    public class SnapshotLineReader : ILineReader
    {
        private int _index;

        public void ReadLine(string line, Snapshot item)
        {
            _index = _index + 1;

            // set data row
            var row = new Line(line, _index);
            item.Add(row);
        }

        public void Reset()
        {
            _index = -1;
        }

        public bool NewSnapshot(Snapshot snapshot)
        {
            return false;
        }
    }
}
