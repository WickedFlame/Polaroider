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
			var ctx = new MapperContext(null, snapshot, new SnapshotOptions(), 0);
			Assert.AreSame(ctx.Snapshot, snapshot);
		}

		[Test]
		public void MapperContext_Options()
		{
			var options = new SnapshotOptions();
			var ctx = new MapperContext(null, new Snapshot(), options, 0);
			Assert.AreSame(ctx.Options, options);
		}

		[Test]
		public void MapperContext_Indentation()
		{
			var ctx = new MapperContext(null, new Snapshot(), new SnapshotOptions(), 2);
			Assert.AreEqual(ctx.Indentation, 2);
		}

		[Test]
		public void MapperContext_AddLine()
		{
			var ctx = new MapperContext(null, new Snapshot(), new SnapshotOptions(), 2);
			ctx.AddLine(new Line("key: value"));

			ctx.Snapshot.Single().Value.Should().Be("key: value");
		}

		[Test]
		public void MapperContext_AddLine_KeyValue()
		{
			var ctx = new MapperContext(null, new Snapshot(), new SnapshotOptions(), 2);
			ctx.AddLine("key", "value");

			ctx.Snapshot.Single().Value.Should().Be("  key: value");
		}

		[Test]
		public void MapperContext_BuildLine()
		{
			var ctx = new MapperContext(null, new Snapshot(), new SnapshotOptions(), 2);
			var line = ctx.BuildLine("key", "value");

			line.Value.Should().Be("  key: value");
		}

		[Test]
		public void MapperContext_BuildLine_Clone_Snapshot()
		{
			var ctx = new MapperContext(null, new Snapshot(), new SnapshotOptions(), 2);
			var clone = ctx.Clone(2);

			ctx.Snapshot.Should().BeSameAs(clone.Snapshot);
		}

		[Test]
		public void MapperContext_BuildLine_Clone_Options()
		{
			var ctx = new MapperContext(null, new Snapshot(), new SnapshotOptions(), 2);
			var clone = ctx.Clone(2);

			ctx.Options.Should().BeSameAs(clone.Options);
		}

		[Test]
		public void MapperContext_BuildLine_Clone_Indentation()
		{
			var ctx = new MapperContext(null, new Snapshot(), new SnapshotOptions(), 2);
			var clone = ctx.Clone(3);

			ctx.Indentation.Should().Be(2);
			clone.Indentation.Should().Be(3);
		}

		[Test]
		public void MapperContext_BuildLine_Clone_Mapper()
		{
			var ctx = new MapperContext(new DefaultMapper(), new Snapshot(), new SnapshotOptions(), 2);
			var clone = ctx.Clone(2);

			ctx.Mapper.Should().BeSameAs(clone.Mapper);
		}
	}
}
