using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Polaroider.Mapping;

namespace Polaroider.Tests.Mapper
{
	public class MapSpecialPropertiesTests
	{
		[Test]
		public void Mapper_Properties_String()
		{
			new {Value = "value"}.MatchSnapshot();
		}

		[Test]
		public void Mapper_Properties_String_Empty()
		{
			new {Value = string.Empty}.MatchSnapshot();
		}

		[Test]
		public void Mapper_Properties_String_Null()
		{
			new {Value = (string) null}.MatchSnapshot();
		}

		[Test]
		public void Mapper_Properties_Type()
		{
			new {ValueType = this.GetType()}.MatchSnapshot();
		}
	}
}
