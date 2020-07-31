using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Polaroider.Tests
{
	public class SnapshotConfigurationTests
	{
		[Test]
		public void SnapshotConfig_Configure()
		{
			var sn = new StringBuilder()
				.AppendLine("Line 1")
				.AppendLine("Line 2")
				.AppendLine("Line 3");

			var savedsnap = SnapshotTokenizer.Tokenize(sn.ToString());

			var config = new SnapshotConfig();
			config.CompareLine((newline, savedline) => newline.Value.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase).Equals(savedline.Value.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase)));
			
			sn = new StringBuilder()
				.AppendLine("Line    1")
				.AppendLine("   Line 2")
				.AppendLine("  Line     3");

			var newsnap = SnapshotTokenizer.Tokenize(sn.ToString());

			var comparer = new SnapshotCompare();
			comparer.Compare(newsnap, savedsnap, config);
		}

		[Test]
		public void Snapshot_Config()
		{
			var sn = new StringBuilder()
				.AppendLine("Line 1")
				.AppendLine("Line 2")
				.AppendLine("Line 3");

			var snapshot = SnapshotTokenizer.Tokenize(sn.ToString());
			snapshot.MatchSnapshot(c =>
			{
				c.UpdateSavedSnapshot();
			});

			sn = new StringBuilder()
				.AppendLine("Line    1")
				.AppendLine("   Line 2")
				.AppendLine("  Line     3");

			snapshot = SnapshotTokenizer.Tokenize(sn.ToString());
			snapshot.MatchSnapshot(c =>
			{
				c.CompareLine((newline, savedline) => newline.Value.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase).Equals(savedline.Value.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase)));
			});
		}
	}
}
