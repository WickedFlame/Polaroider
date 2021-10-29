using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Polaroider.Tests.Mapper
{
	public class MapDictionaries
	{
		[Test]
		public void Mapper_Dictionaries()
		{
			var item = new
			{
				Dict = new Dictionary<string, DictionaryItem>
				{
					{"1", new DictionaryItem {Id = 1, Value = "one"}},
					{"2", new DictionaryItem {Id = 2, Value = "two"}}
				}
			};
				
			SnapshotTokenizer.Tokenize(item).ToString().Should().Be($"Dict:{Environment.NewLine}  Key: 1{Environment.NewLine}  Value:{Environment.NewLine}    Id: 1{Environment.NewLine}    Value: one{Environment.NewLine}  Key: 2{Environment.NewLine}  Value:{Environment.NewLine}    Id: 2{Environment.NewLine}    Value: two");
		}

		[Test]
		public void Mapper_Dictionaries_String()
		{
			var item = new
			{
				Dict = new Dictionary<string, string>
				{
					{"1", "one"},
					{"2", "two"}
				}
			};

			SnapshotTokenizer.Tokenize(item).ToString().Should().Be($"Dict:{Environment.NewLine}  Key: 1{Environment.NewLine}  Value: one{Environment.NewLine}  Key: 2{Environment.NewLine}  Value: two");
		}

		public class DictionaryItem
		{
			public int Id { get; set; }

			public string Value { get; set; }
		}
	}
}
