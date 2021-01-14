using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Polaroider.Tests.Mapper
{
	public class TypeMapperApiTests
	{
		[Test]
		public void TypeMapper_ObjectMapper()
		{
			ObjectMapper.Configure<CustomData>((ctx, o) =>
			{
				//only add Value and Dbl but ignore Id
				ctx.AddLine("Dbl", o.Dbl);
				ctx.AddLine("Value", o.Value);
			});

			var snapshot = new
			{
				Item = "item",
				Data = new CustomData
				{
					Id = 1,
					Dbl = 2.2,
					Value = "value"
				}
			}.Tokenize();

			var expected = new
			{
				Item = "item",
				Data = new
				{
					Value = "value",
					Dbl = 2.2
				}
			}.Tokenize();

			snapshot.ToString().Should().Be(expected.ToString());

			ObjectMapper.Remove<CustomData>();
		}

		

		public class CustomData
		{
			public int Id { get; set; }

			public double Dbl { get; set; }

			public string Value { get; set; }
		}
	}
}
