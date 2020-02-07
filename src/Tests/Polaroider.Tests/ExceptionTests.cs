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
                "this\r\nis\r\ninvalid".MatchSnapshot();
            }
            catch (SnapshotMatchException e)
            {
                e.Message.Should().Be(string.Join(Environment.NewLine, 
                    "",
                    "Snapshots do not match at Line 3", 
                    " - valid", 
                    " + invalid"));

                return;
            }

            Assert.Fail("this should not be reached");
        }
    }
}
