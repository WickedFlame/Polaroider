using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polaroider.Tests
{
    public class ReleaseOptimizerTests
    {
        [Test]
        public void ReleaseOptimizer_Check()
        {
            GetData().ToDictExt().MatchSnapshot();
        }

        public IEnumerable<string> GetData()
        {
            for (int i = 0; i < 10; i++)
            {
                if (true) 
                    yield return i.ToString();
            }
        }
    }

    public static class TestExtensions
    {
        public static Dictionary<string, string> ToDictExt(this IEnumerable<string> values)
        {
            return values.ToDictionary(s => s, s => s);
        }
    }
}
