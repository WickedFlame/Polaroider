using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Polaroider.Mapping;

namespace Polaroider.Tests.Internals.Mappers
{
    public class DefaultObjectMapperTests
    {
        [Test]
        public void Map_ComplexObject()
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

            var mapper = new DefaultObjectMapper();
            var snapshot = mapper.Map(item);

            var sb = string.Join(Environment.NewLine, new[]
            {
                "data: this is a test",
                "list:",
                "  value: 1",
                "  value: 2",
                "obj:",
                "  sub:",
                "    value: sum",
                "strings:",
                "  value 1",
                "  value 2",
                "value: 1"
            });

            snapshot.ToString().Should().BeEquivalentTo(sb);
        }

        [Test]
        public void Map_List()
        {
	        var list = new List<MapItem>
	        {
		        new MapItem {Value = "one"},
		        new MapItem {Value = "two"},
		        new MapItem {Value = "three"}
	        };

	        var mapper = new DefaultObjectMapper();
	        var snapshot = mapper.Map(list);

	        var sb = string.Join(Environment.NewLine, new[]
	        {
		        "Value: one",
		        "Value: two",
		        "Value: three"
	        });

	        snapshot.ToString().Should().BeEquivalentTo(sb);
		}

		[Test]
        public void Map_IEnumerable()
        {
	        var list = new List<MapItem>
	        {
		        new MapItem {Value = "one"},
		        new MapItem {Value = "two"},
		        new MapItem {Value = "three"}
	        };

	        var mapper = new DefaultObjectMapper();
	        var snapshot = mapper.Map(list.Select(i => new { i.Value }));

	        var sb = string.Join(Environment.NewLine, new[]
	        {
		        "Value: one",
		        "Value: two",
		        "Value: three"
	        });

	        snapshot.ToString().Should().BeEquivalentTo(sb);
		}

        [Test]
        public void Map_Array()
        {
	        var list = new List<MapItem>
	        {
		        new MapItem {Value = "one"},
		        new MapItem {Value = "two"},
		        new MapItem {Value = "three"}
	        };

	        var mapper = new DefaultObjectMapper();
	        var snapshot = mapper.Map(list.ToArray());

	        var sb = string.Join(Environment.NewLine, new[]
	        {
		        "Value: one",
		        "Value: two",
		        "Value: three"
	        });

	        snapshot.ToString().Should().BeEquivalentTo(sb);
        }

		public class MapItem
        {
	        public string Value { get; set; }
        }
	}
}
