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
			SnapshotOptions.Default.AddMapper<CustomData>((ctx, o) =>
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

			SnapshotOptions.Setup(o => { });
		}

		[Test]
		public void TypeMapper_Options_ObjectMapper()
		{
			var options = SnapshotOptions.Create(o =>
			{
				o.AddMapper<CustomData>((ctx, itm) =>
				{
					//only add Value and Dbl but ignore Id
					ctx.AddLine("Dbl", itm.Dbl);
					ctx.AddLine("Value", itm.Value);
				});
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
			}.Tokenize(options);

			var expected = new
			{
				Data = new
				{
					Dbl = 2.2,
					Value = "value"
				},
				Item = "item",
			}.Tokenize();

			snapshot.ToString().Should().Be(expected.ToString());
		}

		public class CustomData
		{
			public int Id { get; set; }

			public double Dbl { get; set; }

			public string Value { get; set; }
		}
	}
}
