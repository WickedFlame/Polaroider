using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Polaroider.Mapping.Formatters;

namespace Polaroider.Tests.Mapper.Formatters
{
	public class MockDateTimeFormatterTests
	{
		[Test]
		public void Mapper_MockDateTime_Format()
		{
			var formatter = new MockDateTimeFormatter();
			formatter.Format(new DateTime(2000, 1, 1, 1, 1, 1, 1)).Should().Be("0000-00-00T00:00:00.0000");
		}

		[Test]
		public void Mapper_MockDateTime_NullableDateTime_Format()
		{
			var formatter = new MockDateTimeFormatter();
			DateTime? date = new DateTime(2000, 1, 1, 1, 1, 1, 1);
			formatter.Format(date).Should().Be("0000-00-00T00:00:00.0000");
		}

		[Test]
		public void Mapper_MockDateTime_NullableDateTime_Null_Format()
		{
			var formatter = new MockDateTimeFormatter();
			formatter.Format((DateTime?)null).Should().BeNull();
		}

		[Test]
		public void Mapper_MockDateTime_SnapshotOptions_Tokenize()
		{
			var options = SnapshotOptions.Create(o => o.MockDateTimes());
			new { Value = new DateTime(2000, 1, 1, 1, 1, 1, 1) }.Tokenize(options).ToString().Should().Be("Value: 0000-00-00T00:00:00.0000");
		}

		[Test]
		public void Mapper_MockDateTime_SnapshotOptions_Tokenize_Nullable()
		{
			DateTime? date = new DateTime(2000, 1, 1, 1, 1, 1, 1);

			var options = SnapshotOptions.Create(o => o.MockDateTimes());
			new { Value = date }.Tokenize(options).ToString().Should().Be("Value: 0000-00-00T00:00:00.0000");
		}

		[Test]
		public void Mapper_MockDateTime_Tokenize_Nullable_Nullable_Null()
		{
			var options = SnapshotOptions.Create(o => o.MockDateTimes());
			new { Value = (DateTime?)null }.Tokenize(options).ToString().Should().Be("Value: ");
		}

		[Test]
		public void Mapper_MockGuids_SnapshotOptions_Tokenize()
		{
			var options = SnapshotOptions.Create(o => o.MockGuids());
			new { Value = Guid.NewGuid() }.Tokenize(options).ToString().Should().Be("Value: 00000000-0000-0000-0000-000000000000");
		}

		[Test]
		public void Mapper_MockGuid_SnapshotOptions_Tokenize_Nullable()
		{
			Guid? guid = Guid.NewGuid();

			var options = SnapshotOptions.Create(o => o.MockGuids());
			new { Value = guid }.Tokenize(options).ToString().Should().Be("Value: 00000000-0000-0000-0000-000000000000");
		}

		[Test]
		public void Mapper_MockGuid_Tokenize_Nullable_Nullable_Null()
		{
			var options = SnapshotOptions.Create(o => o.MockGuids());
			new { Value = (Guid?)null }.Tokenize(options).ToString().Should().Be("Value: ");
		}
	}
}
