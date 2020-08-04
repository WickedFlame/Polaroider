using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Polaroider.TestFramework.Tests
{
	public class XUnitTest
	{
		[Fact]
		public void XUnitTest_TestMethod()
		{
			"XUnitTest_TestMethod".MatchSnapshot();
		}
	}
}
