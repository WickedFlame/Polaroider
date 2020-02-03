using System;

namespace Polaroider
{
    public class SnapshotTokenizer
    {
        public static Snapshot Tokenize(string snapshot)
        {
            var token = new Snapshot();
            var lines = snapshot.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                token.Add(new Line(line));
            }

            return token;
        }
    }
}
