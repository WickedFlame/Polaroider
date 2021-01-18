using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Polaroider.Tests.Internals
{
	public class SnapshotOptionsTests
	{
		[Test]
		public void SnapshotOptions_IsValueType_Default()
		{
			var options = SnapshotOptions.Create(o => { });

			options.IsValueType(typeof(int), 1).Should().BeTrue();
		}

		[Test]
		public void SnapshotOptions_IsValueType_Default_String()
		{
			var options = SnapshotOptions.Create(o => { });

			options.IsValueType(typeof(string), string.Empty).Should().BeTrue();
		}

		[Test]
		public void SnapshotOptions_IsValueType_Default_Generic()
		{
			var options = SnapshotOptions.Create(o => { });

			options.IsValueType(typeof(KeyValuePair<int, int>), new KeyValuePair<int, int>()).Should().BeFalse();
		}

		[Test]
		public void SnapshotOptions_IsValueType_Default_Fail()
		{
			var options = SnapshotOptions.Create(o => { });

			options.IsValueType(this.GetType(), this).Should().BeFalse();
		}

		[Test]
		public void SnapshotOptions_IsValueType_Custom()
		{
			var options = SnapshotOptions.Create(o =>
			{
				o.EvaluateValueType((type, obj) => type.IsValueType && type != typeof(string));
			});

			options.IsValueType(typeof(int), 1).Should().BeTrue();
			options.IsValueType(typeof(string), "1").Should().BeFalse();
		}
	}
}
