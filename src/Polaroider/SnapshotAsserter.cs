using System;
using System.Text;

namespace Polaroider
{
    internal class SnapshotAsserter
    {
        /// <summary>
        /// assert invalid snapshots
        /// </summary>
        /// <param name="snapshotResult"></param>
        public static void AssertSnapshot(SnapshotResult snapshotResult)
        {
            switch (snapshotResult.Status)
            {
                case SnapshotStatus.SnapshotsDoNotMatch:
                    Throw(snapshotResult, s => new SnapshotMatchException(s, snapshotResult));
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
            message.Append(Environment.NewLine);
            message.AppendLine($"Snapshots do not match at Line {result.Index + 1}");
            message.AppendLine($" - {result.OldSnapshot[result.Index]}");
            message.AppendLine($" + {result.NewSnapshot[result.Index]}");
            message.AppendLine(string.Empty);
            message.Append(Difference(result.OldSnapshot[result.Index].Value, result.NewSnapshot[result.Index].Value));

            throw exception(message.ToString());
        }

        private static string Difference(string savedLine, string newLine)
        {
            var index = DifferenceIndex(savedLine, newLine);
            if (index < 0)
            {
                index = 0;
            }
            
            var start = index - 10;
            if (start < 0)
            {
                start = 0;
            }

            var lengthNew = newLine.Length >= start + 40 ? 40 : newLine.Length - start;
            var lengthSaved = savedLine.Length >= start + 40 ? 40 : savedLine.Length - start;

            var sb = new StringBuilder();
            sb.AppendLine($"Strings differ at Index {index}");
            sb.AppendLine($" - {savedLine.Substring(start, lengthSaved)}");
            sb.Append($" + {newLine.Substring(start, lengthNew)}");

            return sb.ToString();
        }

        static int DifferenceIndex(string str1, string str2)
        {
            int position = 0;
            int min = Math.Min(str1.Length, str2.Length);
            while (position < min && str1[position] == str2[position])
                position++;

            return (position == min && str1.Length == str2.Length) ? -1 : position;
        }
    }
}
