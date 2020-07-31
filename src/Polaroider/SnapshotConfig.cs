using System;
using System.Collections.Generic;
using System.Text;

namespace Polaroider
{
	public class SnapshotConfig
	{
		private LineComparer _lineComparer;
		private bool _updateSnapshot;

		public static SnapshotConfig Default { get; private set; } = new SnapshotConfig();

		public LineComparer LineComparer => _lineComparer;

		public bool UpdateSnapshot => _updateSnapshot;

		public void CompareLine(LineComparer comparer)
		{
			_lineComparer = comparer;
		}

		public void UpdateSavedSnapshot()
		{
			_updateSnapshot = true;
		}

		public static void Setup(Action<SnapshotConfig> setup)
		{
			setup(Default);
		}

		internal void MergeDefault()
		{
			_lineComparer = _lineComparer ?? Default.LineComparer;
			_updateSnapshot = _updateSnapshot ? _updateSnapshot : Default.UpdateSnapshot;
		}
	}
}
