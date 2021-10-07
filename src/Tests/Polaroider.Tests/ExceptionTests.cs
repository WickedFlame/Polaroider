using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Polaroider.Tests
{
    public class ExceptionTests
    {
        [Test]
        public void MismatchException()
        {
            try
            {
                "this\r\nis\r\na invalid string".MatchSnapshot();
            }
            catch (SnapshotMatchException e)
            {
                e.Message.Should().Be(string.Join(Environment.NewLine, 
                    "",
                    "Snapshots do not match",
                    " - Line 3 position 2",
                    "Expected - a valid string",
                    "Actual   - a invalid string",
                    "",
                    "Line:",
                    "Expected - a valid string",
                    "Actual   - a invalid string",
					"",
                    "Original snapshot:",
                    "this",
                    "is",
                    "a valid string",
                    "",
                    "New snapshot:",
                    "this",
                    "is",
                    "a invalid string"
                ));

                return;
            }

            Assert.Fail("this should not be reached");
        }

        [Test]
        public void MismatchException_OldSnapshot()
        {
	        try
	        {
		        "this\r\nis\r\na invalid string".MatchSnapshot();
	        }
	        catch (SnapshotMatchException e)
	        {
		        e.OldSnapshot.Should().Be(string.Join(Environment.NewLine,
			        "this",
			        "is",
			        "a valid string"
		        ));

		        return;
	        }

	        Assert.Fail("this should not be reached");
        }

        [Test]
        public void MismatchException_NewSnapshot()
        {
	        try
	        {
		        "this\r\nis\r\na invalid string".MatchSnapshot();
	        }
	        catch (SnapshotMatchException e)
	        {
		        e.NewSnapshot.Should().Be(string.Join(Environment.NewLine,
			        "this",
			        "is",
			        "a invalid string"
		        ));

		        return;
	        }

	        Assert.Fail("this should not be reached");
        }
	}
}
