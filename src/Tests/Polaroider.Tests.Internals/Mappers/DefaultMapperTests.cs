using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Polaroider.Mapping;

namespace Polaroider.Tests.Internals.Mappers
{
    public class DefaultMapperTests
    {
	    [SetUp]
	    public void Setup()
	    {
		    SnapshotOptions.Setup(o => { });
	    }

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

            var mapper = new DefaultMapper();
            var snapshot = mapper.Map(item, SnapshotOptions.Default);

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

	        var mapper = new DefaultMapper();
	        var snapshot = mapper.Map(list, SnapshotOptions.Default);

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

	        var mapper = new DefaultMapper();
	        var snapshot = mapper.Map(list.Select(i => new { i.Value }), SnapshotOptions.Default);

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

	        var mapper = new DefaultMapper();
	        var snapshot = mapper.Map(list.ToArray(), SnapshotOptions.Default);

	        var sb = string.Join(Environment.NewLine, new[]
	        {
		        "Value: one",
		        "Value: two",
		        "Value: three"
	        });

	        snapshot.ToString().Should().BeEquivalentTo(sb);
        }

        [Test]
        public void Map_ValueType_Top()
        {
	        var item = new
	        {
		        Id = 1,
		        Name = "one",
		        Dbl = 2.2
	        };

	        var mapper = new DefaultMapper();
	        var snapshot = mapper.Map(item, SnapshotOptions.Default);

	        var sb = string.Join(Environment.NewLine, new[]
	        {
		        "Dbl: 2.2",
		        "Id: 1",
		        "Name: one"
	        });

	        snapshot.ToString().Should().BeEquivalentTo(sb);
        }

        [Test]
        public void Map_ValueType_Deep()
        {
	        var item = new
	        {
		        value = new
		        {
			        Id = 1,
			        Name = "one",
			        Dbl = 2.2,
			        KeyValue = new KeyValuePair<string, string>("key", "value")
		        }
	        };

	        var mapper = new DefaultMapper();
	        var snapshot = mapper.Map(item, SnapshotOptions.Default);

	        var sb = string.Join(Environment.NewLine, new[]
	        {
				"value:",
		        "  Dbl: 2.2",
		        "  Id: 1",
		        "  KeyValue:",
				"    Key: key",
				"    Value: value",
				"  Name: one"
			});

	        snapshot.ToString().Should().BeEquivalentTo(sb);
        }

        [Test]
        public void Map_Nested_List()
        {
	        var item = new
	        {
		        value = new
		        {
			        Items = new []
			        {
						new { Value = "one"},
						new { Value = "two"}
					}
		        }
	        };

	        var mapper = new DefaultMapper();
	        var snapshot = mapper.Map(item, SnapshotOptions.Default);

	        var sb = string.Join(Environment.NewLine, new[]
	        {
		        "value:",
		        "  Items:",
		        "    Value: one",
		        "    Value: two"
	        });

	        snapshot.ToString().Should().BeEquivalentTo(sb);
        }

		public class MapItem
        {
	        public string Value { get; set; }
        }
	}
}
