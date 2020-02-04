using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Polaroider
{
    public class Snapshot : IEnumerable<Line>
    {
        private readonly List<Line> _lines = new List<Line>();

        public SnapshotMetadata Metadata = new SnapshotMetadata();

        public int Count => _lines.Count;

        public Line this[int index] => _lines[index];

        public Snapshot Add(Line row)
        {
            _lines.Add(row);
            return this;
        }

        public Snapshot Add(object row)
        {
            var line = new Line(row.ToString());
            _lines.Add(line);
            return this;
        }

        public IEnumerator<Line> GetEnumerator()
        {
            return _lines.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _lines);
        }
    }
}
