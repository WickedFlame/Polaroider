using System;

namespace Polaroider
{
    /// <summary>
    /// Tokenize snapshots
    /// </summary>
    public class SnapshotTokenizer
    {
        /// <summary>
        /// create a snapshot token of the string
        /// </summary>
        /// <param name="snapshot"></param>
        /// <returns></returns>
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

        /// <summary>
        /// create a snapshot token of the object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="snapshot"></param>
        /// <returns></returns>
        public static Snapshot Tokenize<T>(T snapshot)
        {
            var mapper = ObjectMapper.Mapper.GetMapper(typeof(T));
            return mapper.Map(snapshot);
        }
    }
}
