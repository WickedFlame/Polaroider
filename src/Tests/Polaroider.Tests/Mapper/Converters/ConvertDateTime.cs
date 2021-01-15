using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Polaroider.Mapping.Converters;

namespace Polaroider.Tests.Mapper.Converters
{
	public class ConvertDateTime
	{
		[Test]
		public void Mapper_DateTime_Tokenize()
		{
			new {Value = new DateTime(2000, 1, 1, 1, 1, 1, 1)}.Tokenize().ToString().Should().Be("Value: 2000-01-01T01:01:01.0010000");
		}

		[Test]
		public void Mapper_DateTime_Tokenize_Nullable()
		{
			new { Value = (DateTime?)new DateTime(2000, 1, 1, 1, 1, 1, 1) }.Tokenize().ToString().Should().Be("Value: 2000-01-01T01:01:01.0010000");
		}

		[Test]
		public void Mapper_DateTime_Tokenize_Nullable_Null()
		{
			new { Value = (DateTime?)null }.Tokenize().ToString().Should().Be("Value: ");
		}

		[Test]
		public void Mapper_DateTime_Convert()
		{
			var converter = new DateTimeConverter();
			converter.Convert(new DateTime(2000, 1, 1, 1, 1, 1, 1)).Should().Be("2000-01-01T01:01:01.0010000");
		}
	}
}
