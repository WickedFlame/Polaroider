using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Polaroider.Mapping.Formatters;

namespace Polaroider.Tests.Mapper.Formatters
{
	public class ValueFormatterTests
	{
		[Test]
		public void ValueFormatter()
		{
			var formatter = new ValueFormatter<double>(dbl => ((int) dbl).ToString());
			formatter.Format(2.2).Should().Be("2");
		}
	}
}
