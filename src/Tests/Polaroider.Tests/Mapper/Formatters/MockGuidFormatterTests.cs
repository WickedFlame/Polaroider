using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Polaroider.Mapping.Formatters;

namespace Polaroider.Tests.Mapper.Formatters
{
	public class MockGuidFormatterTests
	{
		[Test]
		public void MockGuidFormatter_ctor()
		{
			Assert.DoesNotThrow(() => new MockGuidFormatter());
		}

		[Test]
		public void MockGuidFormatter_Guid()
		{
			var formatter = new MockGuidFormatter();
			var value = formatter.Format(Guid.NewGuid());
			Assert.AreEqual("00000000-0000-0000-0000-000000000000", value);
		}

		[Test]
		public void MockGuidFormatter_Guid_Nullable()
		{
			var formatter = new MockGuidFormatter();
			Guid? guid = Guid.NewGuid();
			var value = formatter.Format(guid);
			Assert.AreEqual("00000000-0000-0000-0000-000000000000", value);
		}

		[Test]
		public void MockGuidFormatter_Guid_Null()
		{
			var formatter = new MockGuidFormatter();
			Guid? guid = null;
			var value = formatter.Format(guid);
			Assert.IsNull(value);
		}

		[Test]
		public void MockGuidFormatter_InvalidObject()
		{
			var formatter = new MockGuidFormatter();
			var value = formatter.Format("guid");
			Assert.AreEqual("guid", value);
		}
	}
}
