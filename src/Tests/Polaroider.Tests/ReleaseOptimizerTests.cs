using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Polaroider.Tests
{
    public class ReleaseOptimizerTests : BaseClass
    {
        //
        // This test does not work
        // We try to create some code that will be optimized by the compiler to test the TestMethodNotFoundException
        // Until now: We failed...
        //
        // If we succeed this has to be moved to a new Testproject so the Optimize code option can be activated
        //

        [Test]
        [TestCaseSource(nameof(GetData))]
        public void ReleaseOptimizer_Check(string value)
        {
            RunTests(value);
        }

        public static IEnumerable<string> GetData
        {
            get
            {
                for (int i = 0; i < 10; i++)
                {
                    if (true)
                        yield return i.ToString();
                }
            }
        }
    }

    public class BaseClass
    {

        public void RunTests(string str)
        {
            str.ToDictExt().MatchSnapshot(() => new { id = str[0] }, SnapshotOptions.Create(o => {}));
        }
    }

    public static class TestExtensions
    {
        public static Dictionary<char, object> ToDictExt(this IEnumerable<char> values)
        {
            return values.ToDictionary<char, char, object>(s => s, o => new { Name = o });
        }
    }
}
