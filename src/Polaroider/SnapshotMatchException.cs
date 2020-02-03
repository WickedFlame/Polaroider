using System;

namespace Polaroider
{
    public class SnapshotMatchException : Exception
    {
        public SnapshotMatchException(string message, SnapshotResult result)
            : base(message)
        {
            SnapshotResult = result;
        }

        public SnapshotResult SnapshotResult { get; }
    }
}
