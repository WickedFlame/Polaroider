using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Polaroider.Mapping;

namespace Polaroider.Tests.Mapper
{
	public class MapperContextTests
	{
		[Test]
		public void MapperContext_Snapshot()
		{
			var snapshot = new Snapshot();
			var ctx = new MapperContext(snapshot, new SnapshotOptions(), 0);
			Assert.AreSame(ctx.Snapshot, snapshot);
		}

		[Test]
		public void MapperContext_Options()
		{
			var options = new SnapshotOptions();
			var ctx = new MapperContext(new Snapshot(), options, 0);
			Assert.AreSame(ctx.Options, options);
		}

		[Test]
		public void MapperContext_Indentation()
		{
			var ctx = new MapperContext(new Snapshot(), new SnapshotOptions(), 2);
			Assert.AreEqual(ctx.Indentation, 2);
		}

		[Test]
		public void MapperContext_AddLine()
		{
			var ctx = new MapperContext(new Snapshot(), new SnapshotOptions(), 2);
			ctx.AddLine(new Line("key: value"));

			ctx.Snapshot.Single().Value.Should().Be("key: value");
		}

		[Test]
		public void MapperContext_AddLine_KeyValue()
		{
			var ctx = new MapperContext(new Snapshot(), new SnapshotOptions(), 2);
			ctx.AddLine("key", "value");

			ctx.Snapshot.Single().Value.Should().Be("  key: value");
		}

		[Test]
		public void MapperContext_BuildLine()
		{
			var ctx = new MapperContext(new Snapshot(), new SnapshotOptions(), 2);
			var line = ctx.BuildLine("key", "value");

			line.Value.Should().Be("  key: value");
		}
	}
}
