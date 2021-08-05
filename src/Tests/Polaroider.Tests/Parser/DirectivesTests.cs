using System;
using NUnit.Framework;

namespace Polaroider.Tests.Parser
{
	public class DirectivesTests
	{
		[Test]
		public void Directive_AddDirectives_CustomOptions()
		{
			var options = SnapshotOptions.Create(o =>
			{
				// ignore Guids when comparing
				o.AddDirective(s => s.ReplaceRegex(@"(?im)[{(]?[0-9A-Fa-f]{8}[-]?(?:[0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}[)}]?", "00000000-0000-0000-0000-000000000000"));
			});

			new { Value = Guid.NewGuid() }.MatchSnapshot(options);
		}

		[Test]
		public void Directive_AddDirectives_DefaultOptions()
		{
			// ignore Guids when comparing
			SnapshotOptions.Default.AddDirective(s => s.ReplaceRegex(@"(?im)[{(]?[0-9A-Fa-f]{8}[-]?(?:[0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}[)}]?", "00000000-0000-0000-0000-000000000000"));

			new {Value = Guid.NewGuid()}.MatchSnapshot();

			// reset
			SnapshotOptions.Setup(o => { });
		}

		[Test]
		public void Directive_AddDirectives_DefaultOptions_Parser()
		{
			// ignore Guids when comparing
			SnapshotOptions.Default.Parser.AddDirective(s => s.ReplaceRegex(@"(?im)[{(]?[0-9A-Fa-f]{8}[-]?(?:[0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}[)}]?", "00000000-0000-0000-0000-000000000000"));

			new { Value = Guid.NewGuid() }.MatchSnapshot();

			// reset
			SnapshotOptions.Setup(o => { });
		}

		[Test]
		public void Directive_Extension_ReplaceGuid()
		{
			var options = SnapshotOptions.Create(o =>
			{
				// ignore Guids when comparing
				o.AddDirective(s => s.ReplaceGuid());
			});

			new { Value = Guid.NewGuid() }.MatchSnapshot(options);
		}

		[Test]
		public void Directive_Extension_ReplaceGuid_CustomFormat()
		{
			var options = SnapshotOptions.Create(o =>
			{
				// ignore Guids when comparing
				o.AddDirective(s => s.ReplaceGuid("guid replacement"));
			});

			new { Value = Guid.NewGuid() }.MatchSnapshot(options);
		}

		[Test]
		public void Directive_Extension_ReplaceDateTime()
		{
			var options = SnapshotOptions.Create(o =>
			{
				// ignore Guids when comparing
				o.AddDirective(s => s.ReplaceDateTime());
			});

			new { Value = DateTime.Now }.MatchSnapshot(options);
		}

		[Test]
		public void Directive_Extension_ReplaceDateTime_CustomFormat()
		{
			var options = SnapshotOptions.Create(o =>
			{
				// ignore Guids when comparing
				o.AddDirective(s => s.ReplaceDateTime("datetime replacement"));
			});

			new { Value = DateTime.Now }.MatchSnapshot(options);
		}
	}
}
