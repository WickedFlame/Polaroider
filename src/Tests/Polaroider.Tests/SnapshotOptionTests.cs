using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Polaroider.Mapping.Formatters;

namespace Polaroider.Tests
{
	public class SnapshotOptionsTests
	{
		[Test]
		public void SnapshotOptions_Configure()
		{
			var sn = new StringBuilder()
				.AppendLine("Line 1")
				.AppendLine("Line 2")
				.AppendLine("Line 3");

			var savedsnap = SnapshotTokenizer.Tokenize(sn.ToString());

			var config = new SnapshotOptions();
			config.SetComparer((newline, savedline) => newline.Value.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase).Equals(savedline.Value.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase)));
			
			sn = new StringBuilder()
				.AppendLine("Line    1")
				.AppendLine("   Line 2")
				.AppendLine("  Line     3");

			var newsnap = SnapshotTokenizer.Tokenize(sn.ToString());

			var comparer = new SnapshotCompare();
			var result = comparer.Compare(newsnap, savedsnap, config);
			result.Status.Should().Be(SnapshotStatus.SnapshotsMatch);

			SnapshotOptions.Setup(o => { });
		}

		[Test]
		public void Snapshot_Options()
		{
			var sn = new StringBuilder()
				.AppendLine("Line 1")
				.AppendLine("Line 2")
				.AppendLine("Line 3");

			var snapshot = SnapshotTokenizer.Tokenize(sn.ToString());
			snapshot.MatchSnapshot(SnapshotOptions.Create(c =>
			{
				c.UpdateSavedSnapshot();
			}));

			sn = new StringBuilder()
				.AppendLine("Line    1")
				.AppendLine("   Line 2")
				.AppendLine("  Line     3");

			snapshot = SnapshotTokenizer.Tokenize(sn.ToString());

			var options = new SnapshotOptions
			{
				Comparer = new LineCompare((newline, savedline) => newline.Value.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase).Equals(savedline.Value.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase)))
			};
			
			snapshot.MatchSnapshot(options);



			options = SnapshotOptions.Create(o =>
			{
				o.SetComparer((newline, savedline) => newline.Value.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase).Equals(savedline.Value.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase)));
			});

			snapshot.MatchSnapshot(options);

			// reset
			SnapshotOptions.Setup(o => { });
		}




		[Test]
		public void Snapshot_Options_Merge()
		{
			Assert.AreNotSame(new SnapshotOptions().MergeDefault(), SnapshotOptions.Default);
		}

		[Test]
		public void Snapshot_Options_ParserAfterMerge()
		{
			var options = new SnapshotOptions();

			// ensure parser is created
			var parser = options.Parser;
			_ = SnapshotOptions.Default.Parser;
			options = options.MergeDefault();
			
			Assert.AreSame(options.Parser, parser);
		}

		[Test]
		public void Snapshot_Options_DefaultParserAfterMerge()
		{
			var options = new SnapshotOptions();

			// ensure parser is created
			_ = options.Parser;
			_ = SnapshotOptions.Default.Parser;
			options = options.MergeDefault();

			Assert.AreNotSame(options.Parser, SnapshotOptions.Default.Parser);
		}
		
		[Test]
		public void Snapshot_Options_AddDirectives()
		{
			var options = SnapshotOptions.Create(o =>
			{
				// ignore Guids when comparing
				o.AddDirective(s => s.ReplaceRegex(@"(?im)[{(]?[0-9A-Fa-f]{8}[-]?(?:[0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}[)}]?", "00000000-0000-0000-0000-000000000000"));
			});

			new { Value = Guid.NewGuid() }.MatchSnapshot(options);
		}

		[Test]
		public void Snapshot_Options_AddDirectives_DefaultOptions()
		{
			// ignore Guids when comparing
			SnapshotOptions.Default.AddDirective(s => s.ReplaceRegex(@"(?im)[{(]?[0-9A-Fa-f]{8}[-]?(?:[0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}[)}]?", "00000000-0000-0000-0000-000000000000"));

			new { Value = Guid.NewGuid() }.MatchSnapshot();

			// reset
			SnapshotOptions.Setup(o => { });
		}

		[Test]
		public void Snapshot_Options_AddDirectives_DefaultOptions_Parser()
		{
			// ignore Guids when comparing
			SnapshotOptions.Default.Parser.AddDirective(s => s.ReplaceRegex(@"(?im)[{(]?[0-9A-Fa-f]{8}[-]?(?:[0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}[)}]?", "00000000-0000-0000-0000-000000000000"));

			new { Value = Guid.NewGuid() }.MatchSnapshot();

			// reset
			SnapshotOptions.Setup(o => { });
		}

		[Test]
		public void Options_Directives_ReplaceGuid()
		{
			var options = SnapshotOptions.Create(o =>
			{
				// ignore Guids when comparing
				o.AddDirective(s => s.ReplaceGuid());
			});

			new { Value = Guid.NewGuid() }.MatchSnapshot(options);
		}

		[Test]
		public void Snapshot_Options_DefaultOptions_Parser_NotNull()
		{
			// ignore Guids when comparing
			Assert.IsNotNull(SnapshotOptions.Default.Parser);
		}

		[Test]
		public void Snapshot_Options_DefaultOptions_Parser_AfterReset()
		{
			// ignore Guids when comparing
			var parser = SnapshotOptions.Default.Parser;

			// reset
			SnapshotOptions.Setup(o => { });

			Assert.AreNotSame(SnapshotOptions.Default.Parser, parser);
		}

		[Test]
		public void Snapshot_Options_UseBasicFormatters_Count()
		{
			// reset
			SnapshotOptions.Setup(o =>
			{
				o.UseBasicFormatters();
			});

			SnapshotOptions.Default.Formatters.Count().Should().Be(2);
		}

		[Test]
		public void Snapshot_Options_UseBasicFormatters_Types()
		{
			// reset
			SnapshotOptions.Setup(o =>
			{
				o.UseBasicFormatters();
			});

			var keys = SnapshotOptions.Default.Formatters.Keys.ToArray();

			keys[0].Should().Be(typeof(Type));
			keys[1].Should().Be(typeof(string));
		}

		[Test]
		public void Snapshot_Options_UseBasicFormatters_Formatter_Type()
		{
			// reset
			SnapshotOptions.Setup(o =>
			{
				o.UseBasicFormatters();
			});

			SnapshotOptions.Default.Formatters[typeof(Type)].Should().BeOfType<TypeFormatter>();
		}

		[Test]
		public void Snapshot_Options_UseBasicFormatters_Formatter_SimpleString()
		{
			// reset
			SnapshotOptions.Setup(o =>
			{
				o.UseBasicFormatters();
			});

			SnapshotOptions.Default.Formatters[typeof(string)].Should().BeOfType<SimpleStringFormatter>();
		}
	}
}
