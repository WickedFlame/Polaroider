using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Polaroider.Tests
{
	public class TokenizerTests
	{
		[Test]
		public void SnapshotTokenizer_MapToToken_MergeMeta_CustomMapper()
		{
			ObjectMapper.Configure<TokenizerTestItem>(item =>
			{
				var snapshot = SnapshotTokenizer.Tokenize(new {id = item.Id});
				snapshot.SetMetadata(() => new
				{
					Id = item.Id
				});

				return snapshot;
			});

			var token = SnapshotTokenizer.MapToToken(new TokenizerTestItem {Id = 1});
			token.Metadata["Id"].Should().Be("1");
		}

		public class TokenizerTestItem
		{
			public int Id{ get; set; }
		}
	}
}
