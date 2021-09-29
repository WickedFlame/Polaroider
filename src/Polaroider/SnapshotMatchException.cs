using System;

namespace Polaroider
{
	/// <summary>
	/// Exception that is thrown by Plaroider when a snapshot does not match
	/// </summary>
    public class SnapshotMatchException : Exception
    {
		/// <summary>
		/// Create a new instance of the snapshot exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="result"></param>
        public SnapshotMatchException(string message, SnapshotResult result)
            : base(message)
        {
            SnapshotResult = result;
            OldSnapshot = result.OldSnapshot?.ToString();
            NewSnapshot = result.NewSnapshot?.ToString();
		}

		/// <summary>
		/// Gets the value of the original snapshot
		/// </summary>
		public string OldSnapshot{ get; }

		/// <summary>
		/// Gets the value of the new snapshot
		/// </summary>
		public string NewSnapshot { get; }

		/// <summary>
		/// Represents the snapshotresult
		/// </summary>
		public SnapshotResult SnapshotResult { get; }
    }
}
