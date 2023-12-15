using System;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Polaroider.Mapping.Formatters;

namespace Polaroider.Tests.Mapper.Formatters
{
    public class ExceptionFormatterTests
    {
        [Test]
        public void ExceptionFormatter_Tokenize()
        {
            try
            {
                throw new CustomException("this is a test");
            }
            catch (Exception ex)
            {
                var tmp = SnapshotTokenizer.MapToToken(ex)
                    .ToString();

                SnapshotTokenizer.MapToToken(ex)
                    .ToString()
                    .Should()
                    .Be($"this is a test{Environment.NewLine}   at Polaroider.Tests.Mapper.Formatters.ExceptionFormatterTests.ExceptionFormatter_Tokenize()");
            }
        }

        [Test]
        public void ExceptionFormatter_Tokenize_With()
        {
            try
            {
                throw new CustomException("this is a test");
            }
            catch (Exception ex)
            {
                var testobj = new
                {
                    Exception = ex,
                    Name = "Test"
                };

                var expected = new StringBuilder()
                    .AppendLine("Exception: this is a test")
                    .AppendLine("Exception:    at Polaroider.Tests.Mapper.Formatters.ExceptionFormatterTests.ExceptionFormatter_Tokenize_With()")
                    .Append("Name: Test");

                SnapshotTokenizer.MapToToken(testobj)
                    .ToString()
                    .Should()
                    .Be(expected.ToString());
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

        [Test]
        public void ExceptionFormatter_Format_NotException()
        {
            var formatter = new ExceptionFormatter();
            formatter.Format("just a string").Should().Be($"just a string");
        }

        public class CustomException : Exception
        {
            public CustomException(string message) : base(message) { }
        }
    }
}
