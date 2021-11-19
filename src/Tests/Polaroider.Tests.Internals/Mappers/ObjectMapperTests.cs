using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Polaroider.Tests.Internals.Mappers
{
    public class ObjectMapperTests
    {
        [Test]
        public void CreateMapping()
        {
            // overwrite the configured mapping
            ObjectMapper.Configure<Map>(m =>
            {
                var snapshot = new Snapshot()
                    .Add(m.Value);
                return snapshot;
            });

            var item = new Map { Value = "test" };
            item.MatchSnapshot();
        }

        [Test]
        public void OverwriteMapping()
        {
            ObjectMapper.Configure<Map>(m =>
            {
                var snapshot = new Snapshot()
                    .Add(m.Value)
                    .Add(m.Index);
                return snapshot;
            });

            // overwrite the configured mapping
            ObjectMapper.Configure<Map>(m =>
            {
                var snapshot = new Snapshot()
                    .Add(m.Value);
                return snapshot;
            });

            var item = new Map {Value = "test"};
            item.MatchSnapshot();
        }

        public class Map
        {
            public string Value { get; set; }

            public int Index
            {
                get { throw new InvalidOperationException("this should not be called"); }
                set { throw new InvalidOperationException("this should not be called"); }
            }
        }
    }
}
