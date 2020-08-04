using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Polaroider.Tests.Parser
{
	public class LineParserTests
	{
		[Test]
		public void LineParser_Parse()
		{
			var options = new SnapshotOptions();
			options.AddDirective(line => line.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase));


			var sn = new StringBuilder()
				.AppendLine("Line 1")
				.AppendLine("Line 2")
				.AppendLine("Line 3");

			var savedsnap = SnapshotTokenizer.Tokenize(sn.ToString(), options);

			sn = new StringBuilder()
				.AppendLine("Line    1")
				.AppendLine("   Line 2")
				.AppendLine("  Line     3");

			var newsnap = SnapshotTokenizer.Tokenize(sn.ToString());

			var comparer = new SnapshotCompare();
			var result = comparer.Compare(newsnap, savedsnap, options);
			result.Status.Should().Be(SnapshotStatus.SnapshotsDoNotMatch);


			newsnap = SnapshotTokenizer.Tokenize(sn.ToString(), options);
			result = comparer.Compare(newsnap, savedsnap, options);
			result.Status.Should().Be(SnapshotStatus.SnapshotsMatch);
		}

		[Test]
		public void LineParser_MatchSnapshot()
		{
			var options = SnapshotOptions.Create(o =>
			{
				o.AddDirective(line => line.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase));
				o.UpdateSavedSnapshot();
			});


			var sn = new StringBuilder()
				.AppendLine("Line 1")
				.AppendLine("Line 2")
				.AppendLine("Line 3")
				.ToString();

			sn.MatchSnapshot(options);


			sn = new StringBuilder()
				.AppendLine("Line    1")
				.AppendLine("   Line 2")
				.AppendLine("  Line     3")
				.ToString();

			options = SnapshotOptions.Create(o =>
			{
				o.AddDirective(line => line.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase));
			});

			sn.MatchSnapshot(options);
		}
	}
}
