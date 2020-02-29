using NUnit.Framework;

namespace Polaroider.Tests.Verification
{
    public class ArrayVerification
    {
        [Test]
        public void TestStringArray()
        {
            var lst = new []
            {
                "one", "two", "three"
            };

            lst.MatchSnapshot();
        }

        [Test]
        public void TestList()
        {
            var lst = new []
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
