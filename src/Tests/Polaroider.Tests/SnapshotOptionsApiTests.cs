using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NUnit.Framework;
using Polaroider.Mapping;

namespace Polaroider.Tests
{
	public class SnapshotOptionsApiTests
	{
		[Test]
		public void SnapshotOptions_Api_Full()
		{
			var options = SnapshotOptions.Create(o =>
			{
				// directive to change the string
				o.AddDirective(line => line.Replace(" ", string.Empty));
				
				o.AddFormatter(typeof(double), new NumberFormatter());
				o.AddFormatter<decimal>(value => value.ToString(CultureInfo.InvariantCulture));
				
				o.AddFormatter(typeof(MappableClass), new MappableClassFormatter());
				o.AddFormatter<MappableClass>(cls => $"Id: {cls.Id}");

				o.AddMapper<MappableClass>((ctx, item) =>
				{
					ctx.AddLine("Id", item.Id);
				});
				
				o.SetComparer((line1, line2) => line1.Equals(line2));
			});
		}

		public class NumberFormatter : IValueFormatter
		{
			public string Format(object value)
			{
				throw new NotImplementedException();
			}
		}

		public class MappableClass
		{
			public string Name { get; set; }

			public int Id { get; set; }
		}

		public class MappableClassFormatter : IValueFormatter
		{
			public string Format(object value)
			{
				throw new NotImplementedException();
			}
		}
	}
}
