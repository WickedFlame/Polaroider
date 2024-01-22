using NUnit.Framework;
using System.Text;

namespace Polaroider.Tests
{
    public class ObjectMapTests
    {
        [Test]
        public void ObjectMap_Encoding()
        {
            new UTF8Encoding(true, true)
                .MatchSnapshot();
        }
    }
}
