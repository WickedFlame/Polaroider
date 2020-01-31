using System;

namespace Polaroider
{
    public class SnapshotTokenizer
    {
        public static Snapshot Tokenize(string snapshot)
        {
            var token = new Snapshot();
            var lines = snapshot.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            for (var i = 0; i < lines.Length; i++)
            {
                token.Add(new Line(lines[i], i));
            }

            return token;
        }
    }
}
