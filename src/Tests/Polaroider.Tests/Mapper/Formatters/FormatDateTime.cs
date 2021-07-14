using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Polaroider.Mapping.Formatters;

namespace Polaroider.Tests.Mapper.Formatters
{
	public class FormatDateTime
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
		public void Mapper_DateTime_Format()
		{
			var formatter = new DateTimeFormatter();
			formatter.Format(new DateTime(2000, 1, 1, 1, 1, 1, 1)).Should().Be("2000-01-01T01:01:01.0010000");
		}

		[Test]
		public void Mapper_NullableDateTime_Format()
		{
			var formatter = new DateTimeFormatter();
			DateTime? date = new DateTime(2000, 1, 1, 1, 1, 1, 1);
			formatter.Format(date).Should().Be("2000-01-01T01:01:01.0010000");
		}

		[Test]
		public void Mapper_NullableDateTime_Null_Format()
		{
			var formatter = new DateTimeFormatter();
			formatter.Format((DateTime?)null).Should().BeNull();
		}
	}
}
