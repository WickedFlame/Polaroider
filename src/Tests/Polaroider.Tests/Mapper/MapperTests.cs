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
