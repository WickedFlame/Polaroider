using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Polaroider
{
    public class Snapshot : IEnumerable<Line>
    {
        private List<Line> _lines = new List<Line>();

        public Dictionary<string, string> Metadata = new Dictionary<string, string>();

        public int Count => _lines.Count;

        public Line this[int index] => _lines[index];

        public void Add(Line row)
        {
            _lines.Add(row);
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
