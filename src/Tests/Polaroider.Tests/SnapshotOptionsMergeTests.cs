using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Polaroider.Mapping;

namespace Polaroider.Tests
{
	public class SnapshotOptionsMergeTests
	{
		[SetUp]
		public void Setup()
		{
			// reset
			SnapshotOptions.Setup(o => { });
		}

		[Test]
		public void SnapshotOptions_Merge_Formatters()
		{
			var options = new SnapshotOptions();
			options.Formatters.Count().Should().Be(4);

			options.MergeDefault();

			foreach (var key in options.Formatters.Keys)
			{
				options.Formatters[key].GetType().Should().BeSameAs(SnapshotOptions.Default.Formatters[key].GetType());
			}
		}

		[Test]
		public void SnapshotOptions_Merge_AddFormatters()
		{
			var formatter = new NumberFormatter();

			var options = SnapshotOptions.Create(o =>
			{
				o.AddFormatter(typeof(int), formatter);
			});

			options.MergeDefault();

			options.Formatters.Count().Should().Be(5);
			options.Formatters.Should().Contain(formatter);
		}

		[Test]
		public void SnapshotOptions_Merge_Formatters_MultipleSame()
		{
			SnapshotOptions.Setup(o =>
			{
				o.Formatters = new MapperCollection<Type, IValueFormatter>
				{
					{typeof(int), new NumberFormatter()},
					{typeof(double), new NumberFormatter()}
				};
			});

			var options = SnapshotOptions.Create(o =>
			{
				o.AddFormatter(typeof(int), new NumberFormatter());
			});

			options.MergeDefault();

			options.Formatters.Count().Should().Be(2);
		}

		[Test]
		public void SnapshotOptions_Merge_TypeMappers()
		{
			SnapshotOptions.Setup(o => { o.AddMapper<CustomType>((c, m) => { }); });

			var options = new SnapshotOptions();
			options.TypeMappers.Count().Should().Be(0);

			options.MergeDefault();

			options.TypeMappers.Count().Should().Be(1);

			foreach (var key in options.TypeMappers.Keys)
			{
				options.TypeMappers[key].Should().BeSameAs(SnapshotOptions.Default.TypeMappers[key]);
			}
		}

		[Test]
		public void SnapshotOptions_Merge_AddTypeMappers()
		{
			var options = SnapshotOptions.Create(o => { o.AddMapper<CustomType>((c, i) => { }); });

			options.MergeDefault();

			options.TypeMappers.Count().Should().Be(1);
		}

		[Test]
		public void SnapshotOptions_Merge_TypeMappers_MultipleSame()
		{
			SnapshotOptions.Setup(o =>
			{
				o.AddMapper<CustomType>((c, m) => { });
				o.AddMapper<CustomOtherType>((c, m) => { });
			});

			var options = SnapshotOptions.Create(o => { o.AddMapper<CustomType>((c, i) => { }); });

			options.MergeDefault();

			options.TypeMappers.Count().Should().Be(2);
		}

		[Test]
		public void SnapshotOptions_Merge_Comparer_Default()
		{
			var options = SnapshotOptions.Create(o => { });

			options.MergeDefault();

			options.Comparer.Should().BeSameAs(SnapshotOptions.Default.Comparer);
		}

		[Test]
		public void SnapshotOptions_Merge_Comparer_Custom()
		{
			var options = SnapshotOptions.Create(o =>
			{
				o.SetComparer((l1, l2) => true);
			});

			options.MergeDefault();

			options.Comparer.Should().NotBeSameAs(SnapshotOptions.Default.Comparer);
		}

		[Test]
		public void SnapshotOptions_Merge_UpdateSnapshot_Default()
		{
			SnapshotOptions.Default.UpdateSavedSnapshot();

			var options = SnapshotOptions.Create(o => { });

			options.MergeDefault();

			options.UpdateSnapshot.Should().Be(SnapshotOptions.Default.UpdateSnapshot).And.BeTrue();
		}

		[Test]
		public void SnapshotOptions_Merge_UpdateSnapshot_Custom()
		{
			var options = SnapshotOptions.Create(o =>
			{
				o.UpdateSavedSnapshot();
			});

			options.MergeDefault();

			options.UpdateSnapshot.Should().Be(!SnapshotOptions.Default.UpdateSnapshot).And.BeTrue();
		}

		public class NumberFormatter : IValueFormatter
		{
			public string Format(object value)
			{
				throw new NotImplementedException();
			}
		}

		public class CustomType
		{
		}

		public class CustomOtherType
		{
		}
	}
}
