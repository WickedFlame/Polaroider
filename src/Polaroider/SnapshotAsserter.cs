using System;
using System.Text;

namespace Polaroider
{
    internal class SnapshotAsserter
    {
        public static void AssertSnapshot(SnapshotResult snapshotResult)
        {
            switch (snapshotResult.Status)
            {
                case SnapshotStatus.SnapshotsDoNotMatch:
                    Throw(snapshotResult, s => new SnapshotsDoNotMatchException(s));
                    break;

                case SnapshotStatus.SnapshotDoesNotExist:
                    //save snapshot if it does not exist! simplest way of updating snapshots!
                    //throw new SnapshotDoesNotExistException(snapResult);
                case SnapshotStatus.SnapshotUpdated:
                case SnapshotStatus.SnapshotsMatch:
                default:
                    return;
            }
        }

        private static void Throw<T>(SnapshotResult result, Func<string, T> exception) where T : Exception
        {
            var message = new StringBuilder();
            message.AppendLine($"Snapshots do not match at Line {result.Index + 1}");
            message.AppendLine($" - {result.OldSnapshot[result.Index]}");
            message.Append($" + {result.NewSnapshot[result.Index]}");

            throw exception(message.ToString());
        }
    }

    public class SnapshotsDoNotMatchException : Exception
    {
        public SnapshotsDoNotMatchException(string message)
            : base(message)
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
