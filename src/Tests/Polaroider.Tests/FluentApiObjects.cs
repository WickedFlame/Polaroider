using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Polaroider.Tests
{
    public class FluentApiObjects
    {
        [Test]
        public void Simple()
        {
            var item = new {value = 1, data = "this is a test"};
            item.MatchSnapshot();
        }

        [Test]
        public void Simple_AlterOrder()
        {
            var item = new { data = "this is a test", value = 1 };
            item.MatchSnapshot();
        }

        [Test]
        public void Complex()
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

            item.MatchSnapshot();
        }
    }
}
