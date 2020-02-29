using System.Collections.Generic;
using NUnit.Framework;

namespace Polaroider.Tests.Verification
{
    public class ListVerification
    {
        [Test]
        public void TestStringList()
        {
            var lst = new List<string>
            {
                "one", "two", "three"
            };

            lst.MatchSnapshot();
        }

        [Test]
        public void TestList()
        {
            var lst = new List<ListObject>
            {
                new ListObject {Value = "one", Id = 1},
                new ListObject {Value = "two", Id = 2},
                new ListObject {Value = "three", Id = 3}
            };

            lst.MatchSnapshot();
        }

        public class ListObject
        {
            public string Value { get; set; }

            public int Id { get; set; }
        }
    }
}
