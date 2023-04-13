using System;
using FluentAssertions;
using NUnit.Framework;
using Polaroider.Mapping.Formatters;

namespace Polaroider.Tests.Mapper.Formatters
{
    public class ExceptionFormatterTests
    {
        [Test]
        public void ExceptionFormatter_MatchSnapshot()
        {
            try
            {
                throw new CustomException("this is a test");
            }
            catch (Exception ex)
            {
                SnapshotTokenizer.MapToToken(ex)
                    .ToString()
                    .Should()
                    .Be($"this is a test{Environment.NewLine}   at Polaroider.Tests.Mapper.Formatters.ExceptionFormatterTests.ExceptionFormatter_MatchSnapshot()");
            }
        }

        [Test]
        public void ExceptionFormatter_Format()
        {
            var formatter = new ExceptionFormatter();
            formatter.Format(new CustomException("this is a test"))
                .Should()
                .Be($"this is a test{Environment.NewLine}");
        }

        public class CustomException : Exception
        {
            public CustomException(string message) : base(message) { }
        }
    }
}
