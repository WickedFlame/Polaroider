using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Polaroider.Tests.Mapper
{
	public class MapSpecialPropertiesTests
	{
		[Test]
		public void Mapper_Properties_String()
		{
			new { Value = "value" }.MatchSnapshot();
		}

		[Test]
		public void Mapper_Properties_Type()
		{
			new {ValueType = this.GetType()}.MatchSnapshot();
		}
	}
}
