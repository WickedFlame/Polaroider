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
		public void Mapper_Set_Formatters_Options()
		{
			var options = SnapshotOptions.Create(o =>
			{
				o.AddFormatter(typeof(CustomObjectFormatter), new CustomObjectFormatter());
			});

			new
			{
				Value = new CustomObject {Value = "TesT"},
				Str = (string)null
			}.Tokenize(options).ToString().Should().Be("");
		}

		[Test]
		public void Mapper_Set_Formatters_DefaultOptions()
		{
			SnapshotOptions.Default.AddFormatter(typeof(CustomObjectFormatter), new CustomObjectFormatter());

			new
			{
				Value = new CustomObject { Value = "TesT" },
				Str = (string)null
			}.Tokenize().ToString().Should().Be("");
		}

		[Test]
		public void Mapper_Set_Formatters_DefaultOptions_Setup()
		{
			SnapshotOptions.Setup(o =>
			{
				o.AddFormatter(typeof(CustomObjectFormatter), new CustomObjectFormatter());
			});

			new
			{
				Value = new CustomObject { Value = "TesT" },
				Str = (string)null
			}.Tokenize().ToString().Should().Be("");

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
			}.Tokenize().ToString().Should().Be("");

			// reset
			SnapshotOptions.Setup(o => { });
		}





		[Test]
		public void Mapper_Set_Formatters_Options_MatchSnapshot()
		{
			var options = SnapshotOptions.Create(o =>
			{
				o.AddFormatter(typeof(CustomObjectFormatter), new CustomObjectFormatter());
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
				o.AddFormatter(typeof(CustomObjectFormatter), new CustomObjectFormatter());
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

		public struct CustomObject
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
