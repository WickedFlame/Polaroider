using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Polaroider.Tests.Mapper
{
    public class MapperTests
    {
        [Test]
        public void CustomObjectMap()
        {
            ObjectMapper.Configure<CustomMapClass>(m =>
            {
                var token = SnapshotTokenizer.Tokenize(m.Value);
                return token;
            });

            var itm = new CustomMapClass
            {
                Value = "some value",
                Index = 0
            };

            itm.MatchSnapshot();
        }

        [Test]
        public void CustomObjectMap_MapDateTime()
        {
	        SnapshotTokenizer.Tokenize(new {Value = new DateTime(2000, 1, 1, 1, 1, 1, 1)}).ToString().Should().Be("Value: 2000-01-01T01:01:01.0010000");
        }

        public class CustomMapClass
        {
            public string Value { get; set; }

            public int Index
            {
                get { throw new Exception("this should not be valled"); }
                set { //donothing
                }
            }
        }
    }
}
