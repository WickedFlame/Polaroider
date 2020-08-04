using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Polaroider.TestFramework.Tests
{
	[TestClass]
	public class MSTest
	{
		[TestMethod]
		public void MSTest_TestMethod()
		{
			"MSTest_TestMethod".MatchSnapshot();
		}
	}
}
