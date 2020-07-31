using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Polaroider.Tests.Compare
{
	public class ComparerPerSnapshotTests
	{
		//[Test]
		//public void MapperPerSnapshot()
		//{
		//	//ObjectMapper.Configure<CustomMapClass>(m =>
		//	//{
		//	//	var token = SnapshotTokenizer.Tokenize(m.Value);
		//	//	return token;
		//	//});

		//	var itm = new CustomMapClass
		//	{
		//		Value = "some value",
		//		Index = 0
		//	};

			

		//	//itm.MatchSnapshot(c =>
		//	//{
		//	//	c.CompareLines((newline, savedline) => newline.Equals(savedline));
		//	//});

		//	Assert.Fail();
		//}

		public class CustomMapClass
		{
			public string Value { get; set; }

			public int Index
			{
				get { throw new Exception("this should not be valled"); }
				set
				{ //donothing
				}
			}
		}
	}
}
