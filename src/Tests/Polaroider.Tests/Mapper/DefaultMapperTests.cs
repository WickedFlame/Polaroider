using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Polaroider.Mapping;

namespace Polaroider.Tests.Mapper
{
	public class DefaultMapperTests
	{
		[Test]
		public void DefaultMapper_ctor()
		{
			Assert.DoesNotThrow(() => new DefaultMapper());
		}

		[Test]
		public void DefaultMapper_Map_IgnoreIndexers()
		{
			
			var mapper = new DefaultMapper();

			var context = new MapperContext(mapper, new Snapshot(), new SnapshotOptions(), 0);

			var item = new WithIndexer();

			mapper.Map(context, item);

			Assert.That(0, Is.EqualTo(context.Snapshot.Count));
		}

		[Test]
		public void DefaultMapper_Map_IgnoreNoGetter()
		{

			var mapper = new DefaultMapper();

			var context = new MapperContext(mapper, new Snapshot(), new SnapshotOptions(), 0);

			var item = new OnlySetter();

			mapper.Map(context, item);

			Assert.That(0, Is.EqualTo(context.Snapshot.Count));
		}

		[Test]
		public void DefaultMapper_Map_NoSetter()
		{

			var mapper = new DefaultMapper();

			var context = new MapperContext(mapper, new Snapshot(), new SnapshotOptions(), 0);

			var item = new OnlyGetter();

			mapper.Map(context, item);

			Assert.That(1, Is.EqualTo(context.Snapshot.Count));
		}

        [Test]
        public void DefaultMapper_Map_Null()
        {

            var mapper = new DefaultMapper();

            var context = new MapperContext(mapper, new Snapshot(), new SnapshotOptions(), 0);

            mapper.Map(context, (OnlyGetter)null);

            context.Snapshot.Should().BeEmpty();
        }

        public class WithIndexer
		{
			private readonly List<string> _list = new List<string>
			{
				"one", "two"
			};

			public string this[int i]
			{
				set { _list.Insert(i, value); }
				get { return _list[i]; }
			}
		}

		public class OnlySetter
		{
			private string _value;
			public string SetterOnly
			{
				set { _value = value; }
			}
		}

		public class OnlyGetter
		{
			private string _value = "set";
			public string GetterOnly => _value;
		}
	}
}
