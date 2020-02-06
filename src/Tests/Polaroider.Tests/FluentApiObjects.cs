using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Polaroider.Tests
{
    public class FluentApiObjects
    {
        [Test]
        public void Simple()
        {
            var item = new {value = 1, data = "this is a test"};
            item.MatchSnapshot();
        }

        [Test]
        public void Simple_AlterOrder()
        {
            var item = new { data = "this is a test", value = 1 };
            item.MatchSnapshot();
        }

        [Test]
        public void Complex()
        {
            var item = new
            {
                value = 1,
                obj = new
                {
                    sub = new
                    {
                        value = "sum"
                    }
                },
                data = "this is a test",
                list = new[]
                {
                    new
                    {
                        value = "1"
                    },
                    new
                    {
                        value = "2"
                    },
                },
                strings = new List<string>
                {
                    "value 1",
                    "value 2"
                }
            };

            item.MatchSnapshot();
        }

        [Test]
        public void AddMetadata()
        {
            var item = new { value = 1, data = "this is a test" };
            item.MatchSnapshot(() => new {id = 1});


            var snapshotResolver = new SnapshotIdResolver();
            var reader = new SnapshotReader();
            var snapshots = reader.Read(snapshotResolver.ResloveId());

            snapshots.Count().Should().Be(1);
            snapshots.Single(s => s.Metadata["id"] == "1").Should().NotBeNull();
        }
    }
}
