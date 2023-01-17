using NUnit.Framework;

namespace Polaroider.TestFramework.Tests
{
	public class NUnitTest
	{
		[Test]
		public void NUnitTest_TestMethod()
		{
			"NUnitTest_TestMethod".MatchSnapshot();
		}
	}
}
