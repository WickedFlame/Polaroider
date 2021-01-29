using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Polaroider.Mapping;

namespace Polaroider.Tests.Mapper
{
	public class FormatPropertiesTests
	{
		[NUnit.Framework.SetUp]
		public void Setup()
		{
			SnapshotOptions.Setup(o => { });
		}

		[Test]
		public void Mapper_Properties_String()
		{
			new {Value = "value"}.MatchSnapshot();
		}

		[Test]
		public void Mapper_Properties_String_Empty()
		{
			new {Value = string.Empty}.MatchSnapshot();
		}

		[Test]
		public void Mapper_Properties_String_Null()
		{
			new {Value = (string) null}.MatchSnapshot();
		}

		[Test]
		public void Mapper_Properties_Type()
		{
			new {ValueType = this.GetType()}.MatchSnapshot();
		}

		[Test]
		public void Mapper_Properties_DateTime()
		{
			new { Value = new DateTime(2000, 1, 1, 1, 1, 1) }.MatchSnapshot();
		}

		[Test]
		public void Mapper_Properties_DateTime_Nullable()
		{
			DateTime? value = new DateTime(2000, 1, 1, 1, 1, 1);
			new { Value = value }.MatchSnapshot();
		}

		[Test]
		public void Mapper_Properties_DateTime_Null()
		{
			DateTime? value = null;
			new { Value = value }.MatchSnapshot();
		}


		[Test]
		public void Mapper_Set_Formatters_Options()
		{
			var options = SnapshotOptions.Create(o =>
			{
				o.AddFormatter(typeof(CustomObject), new CustomObjectFormatter());
			});

			new
			{
				Value = new CustomObject {Value = "TesT"},
				Str = (string)null
			}.Tokenize(options).ToString().Should().Be("Str: null\r\nValue: custom - TesT");
		}

		[Test]
		public void Mapper_Set_Formatters_TopLevel()
		{
			var options = SnapshotOptions.Create(o =>
			{
				o.AddFormatter(typeof(CustomObject), new CustomObjectFormatter());
			});

			new CustomObject { Value = "TesT" }
				.Tokenize(options)
				.ToString()
				.Should()
				.Be("custom - TesT");
		}

		[Test]
		public void Mapper_Set_Formatters_DefaultOptions()
		{
			SnapshotOptions.Default.AddFormatter(typeof(CustomObject), new CustomObjectFormatter());

			new
			{
				Value = new CustomObject { Value = "TesT" },
				Str = (string)null
			}.Tokenize().ToString().Should().Be("Str: null\r\nValue: custom - TesT");
		}

		[Test]
		public void Mapper_Set_Formatters_DefaultOptions_Setup()
		{
			SnapshotOptions.Setup(o =>
			{
				o.AddFormatter(typeof(CustomObject), new CustomObjectFormatter());
			});

			new
			{
				Value = new CustomObject { Value = "TesT" },
				Str = (string)null
			}.Tokenize().ToString().Should().Be("Str: null\r\nValue: custom - TesT");

			// reset
			SnapshotOptions.Setup(o => { });
		}

		[Test]
		public void Mapper_Set_Formatters_UseBasicFormatters()
		{
			SnapshotOptions.Default.UseBasicFormatters();

			new
			{
				Value = new CustomObject { Value = "TesT" },
				Str = (string)null
			}.Tokenize().ToString().Should().Be("Str: \r\nValue:\r\n  Id: 0\r\n  Value: TesT");

			// reset
			SnapshotOptions.Setup(o => { });
		}





		[Test]
		public void Mapper_Set_Formatters_Options_MatchSnapshot()
		{
			var options = SnapshotOptions.Create(o =>
			{
				o.AddFormatter(typeof(CustomObject), new CustomObjectFormatter());
			});

			new
			{
				Value = new CustomObject { Value = "TesT" },
				Str = (string)null
			}.MatchSnapshot(options);
		}


		[Test]
		public void Mapper_Set_Formatters_DefaultOptions_MatchSnapshot()
		{
			SnapshotOptions.Setup(o =>
			{
				o.AddFormatter(typeof(CustomObject), new CustomObjectFormatter());
			});

			new
			{
				Value = new CustomObject { Value = "TesT" },
				Str = (string)null
			}.MatchSnapshot();

			// reset
			SnapshotOptions.Setup(o => { });
		}

		[Test]
		public void Mapper_Set_Formatters_UseBasicFormatters_MatchSnapshot()
		{
			SnapshotOptions.Default.UseBasicFormatters();

			new
			{
				Value = new CustomObject { Value = "TesT" },
				Str = (string)null
			}.MatchSnapshot();

			// reset
			SnapshotOptions.Setup(o => { });
		}

		public class CustomObject
		{
			public string Value { get; set; }

			public int Id{ get; set; }
		}

		public class CustomObjectFormatter : IValueFormatter
		{
			public string Format(object value)
			{
				return $"custom - {((CustomObject) value).Value}";
			}
		}
	}
}
