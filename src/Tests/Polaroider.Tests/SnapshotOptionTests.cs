using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

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
		}
	}
}
