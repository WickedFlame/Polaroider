using System;
using System.Collections;
using System.Collections.Generic;

namespace Polaroider
{
    public class Snapshot : IEnumerable<Line>
    {
        private readonly List<Line> _lines = new List<Line>();

        /// <summary>
        /// gets the metatdata associated with the snapshot
        /// </summary>
        public SnapshotMetadata Metadata { get; } = new SnapshotMetadata();

        /// <summary>
        /// gets the count of lines
        /// </summary>
        public int Count => _lines.Count;

        /// <summary>
        /// gets the line at the given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Line this[int index]
        {
	        get => index < _lines.Count ? _lines[index] : new Line(string.Empty);
	        internal set => _lines[index] = value;
        }

        /// <summary>
        /// add a line to the snapshot
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public Snapshot Add(Line row)
        {
            _lines.Add(row);
            return this;
        }

        /// <summary>
        /// add a line to the snapshot
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public Snapshot Add(object row)
        {
            var line = new Line(row.ToString());
            _lines.Add(line);
            return this;
        }

        /// <summary>
        /// get the enumerator
        /// </summary>
        /// <returns></returns>
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
