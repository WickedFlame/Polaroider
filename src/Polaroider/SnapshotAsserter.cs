using System;

namespace Polaroid
{
    internal class SnapshotAsserter
    {
        public static void AssertSnapshot(SnapshotResult snapResult)
        {
            switch (snapResult.Status)
            {
                case SnapshotStatus.SnapshotDoesNotExist:
                    //save snapshot if it does not exist! simplest way of updating snapshots!
                    //throw new SnapshotDoesNotExistException(snapResult);
                case SnapshotStatus.SnapshotsDoNotMatch:
                    throw new SnapshotsDoNotMatchException(snapResult);
                case SnapshotStatus.SnapshotUpdated:
                case SnapshotStatus.SnapshotsMatch:
                default:
                    return;
            }
        }
    }

    public class SnapshotsDoNotMatchException : Exception
    {
        public SnapshotsDoNotMatchException(SnapshotResult result)
            /*: base(Messages.GetSnapResultMessage(result))*/
        {
        }
    }

    //public class SnapshotDoesNotExistException : Exception
    //{
    //    public SnapshotDoesNotExistException(SnapshotResult result)
    //        /*: base(Messages.GetSnapResultMessage(result))*/
    //    {
    //    }
    //}
}
