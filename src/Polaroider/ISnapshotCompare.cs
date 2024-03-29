﻿
namespace Polaroider
{
	/// <summary>
	/// compares snapshots
	/// </summary>
    public interface ISnapshotCompare
    {
		/// <summary>
		/// compare snapshots
		/// </summary>
		/// <param name="newSnapshot"></param>
		/// <param name="savedSnapshot"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		SnapshotResult Compare(Snapshot newSnapshot, Snapshot savedSnapshot, SnapshotOptions options);
    }
}
