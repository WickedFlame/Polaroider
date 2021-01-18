using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Polaroider.Mapping.Formatters;

namespace Polaroider.Tests.Mapper.Formatters
{
	public class FormatSimpleString
	{
		[Test]
		public void Mapper_SimpleString_Format()
		{
			var formatter = new SimpleStringFormatter();
			formatter.Format("test").Should().Be("test");
		}

		[Test]
		public void Mapper_SimpleString_Format_Empty()
		{
			var formatter = new SimpleStringFormatter();
			formatter.Format("").Should().BeEmpty();
		}

		[Test]
		public void Mapper_SimpleString_Format_Null()
		{
			var formatter = new SimpleStringFormatter();
			formatter.Format((string)null).Should().BeEmpty();
		}
	}
}
