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
            var (saved, actual, index) = Difference(result.OldSnapshot[result.Index].Value, result.NewSnapshot[result.Index].Value);

            var message = new StringBuilder();
            message.Append(Environment.NewLine);
            message.AppendLine($"Snapshots do not match");
            message.AppendLine($" - Line {result.Index + 1} position {index}");
            message.AppendLine($"Expected - {saved}");
            message.AppendLine($"Actual   - {actual}");
            message.AppendLine(string.Empty);
            message.AppendLine("Line:");
            message.AppendLine($"Expected - {result.OldSnapshot[result.Index]}");
            message.AppendLine($"Actual   - {result.NewSnapshot[result.Index]}");
			message.AppendLine(string.Empty); 
			message.AppendLine("Original:");
            message.AppendLine(result.OldSnapshot.ToString());
            message.AppendLine(string.Empty);
            message.AppendLine("New snapshot:");
            message.Append(result.NewSnapshot.ToString());

			throw exception(message.ToString());
        }

        private static (string, string, int) Difference(string savedLine, string newLine)
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

            return (savedLine.Substring(start, lengthSaved), newLine.Substring(start, lengthNew), index);
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
