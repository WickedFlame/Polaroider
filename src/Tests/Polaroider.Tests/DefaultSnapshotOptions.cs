using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Polaroider.Tests
{
	public class DefaultSnapshotOptions
	{
		[Test]
		public void DefaultSnapshotOptions_Parser()
		{
			SnapshotOptions.Setup(o =>
			{
				o.SetParser(line => line.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase));
			});

			var options = SnapshotOptions.Create(o =>
			{
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

			sn.MatchSnapshot();

			// reset
			SnapshotOptions.Setup(o => { });
		}

		[Test]
		public void DefaultSnapshotOptions_Comparer()
		{
			SnapshotOptions.Setup(o =>
			{
				o.SetComparer((newline, savedline) => newline.Value.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase).Equals(savedline.Value.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase)));
			});

			var options = SnapshotOptions.Create(o =>
			{
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

			sn.MatchSnapshot();

			// reset
			SnapshotOptions.Setup(o => { });
		}
	}
}
