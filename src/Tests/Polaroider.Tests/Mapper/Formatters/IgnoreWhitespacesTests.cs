using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Polaroider.Tests.Mapper.Formatters
{
    public class IgnoreWhitespacesTests
    {
        [Test]
        public void SnapshotOptions_IgnoreWhitespaces()
        {
            var options = SnapshotOptions.Create(o => o.IgnoreWhiteSpaces());
            new { Value = "t h i s  h a s  m a n y  s p a c e s" }.Tokenize(options).ToString().Should().Be("Value:thishasmanyspaces");
        }
    }
}
