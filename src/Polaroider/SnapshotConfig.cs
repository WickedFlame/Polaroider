using System;
using System.Collections.Generic;
using System.Text;

namespace Polaroider
{
	/// <summary>
	/// the configuration for the snapshots
	/// </summary>
	public class SnapshotConfig
	{
		private ILineCompare _comparer;
		private bool _updateSnapshot;

		/// <summary>
		/// gets the default configuration
		/// </summary>
		public static SnapshotConfig Default { get; } = new SnapshotConfig();

		/// <summary>
		/// the configured comparer
		/// </summary>
		public ILineCompare Comparer => _comparer;

		/// <summary>
		/// update the snapshot
		/// </summary>
		public bool UpdateSnapshot => _updateSnapshot;

		/// <summary>
		/// sets the comparer
		/// </summary>
		/// <param name="comparer"></param>
		public void SetComparer(Func<Line, Line, bool> comparer)
		{
			_comparer = new LineCompare(comparer);
		}

		/// <summary>
		/// sets updatesnapshot
		/// </summary>
		public void UpdateSavedSnapshot()
		{
			_updateSnapshot = true;
		}

		/// <summary>
		/// setup the default configuration
		/// </summary>
		/// <param name="setup"></param>
		public static void Setup(Action<SnapshotConfig> setup)
		{
			setup(Default);
		}

		internal void MergeDefault()
		{
			_comparer = _comparer ?? Default.Comparer;
			_updateSnapshot = _updateSnapshot ? _updateSnapshot : Default.UpdateSnapshot;
		}
	}
}
