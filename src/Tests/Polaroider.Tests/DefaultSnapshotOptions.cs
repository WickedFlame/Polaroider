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
				o.AddDirective(line => line.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase));
			});

			var options = SnapshotOptions.Create(o =>
			{
				o.UpdateSavedSnapshot();
			});


			var sn = new StringBuilder()
				.AppendLine("Line1")
				.AppendLine("Line2")
				.AppendLine("Line3")
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

		[Test]
		public void DefaultSnapshotOptions_UpdateSnapshot()
		{
			var options = SnapshotOptions.Create(o =>
			{
				o.UpdateSavedSnapshot();
			});
			
			var sn = new StringBuilder()
				.AppendLine("Line 1")
				.AppendLine("Line 2")
				.AppendLine("Line 3")
				.ToString();

			// rewrite the snapshot
			sn.MatchSnapshot(options);


			sn = new StringBuilder()
				.AppendLine("Line    1")
				.AppendLine("   Line 2")
				.AppendLine("  Line     3")
				.ToString();

			Action fail = () => sn.MatchSnapshot();
			Assert.Throws<SnapshotMatchException>(() => fail.Invoke());

			// rewrite snapshot
			sn.MatchSnapshot(options);

			sn = new StringBuilder()
				.AppendLine("Line    1")
				.AppendLine("   Line 2")
				.AppendLine("  Line     3")
				.ToString();

			// Final Assert
			sn.MatchSnapshot();

			// reset
			SnapshotOptions.Setup(o => { });
		}
	}
}
