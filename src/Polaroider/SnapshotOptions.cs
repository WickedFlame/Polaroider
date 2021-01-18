using System;
using System.Collections.Generic;
using System.Text;
using Polaroider.Mapping;
using Polaroider.Mapping.Formatters;

namespace Polaroider
{
	/// <summary>
	/// the options for the snapshots
	/// </summary>
	public class SnapshotOptions
	{
		private ILineParser _parser;

		/// <summary>
		/// setup the default options
		/// </summary>
		static SnapshotOptions()
		{
			Setup(o =>{ });
		}

		/// <summary>
		/// gets the default configuration
		/// </summary>
		public static SnapshotOptions Default { get; private set; }

		/// <summary>
		/// the configured comparer
		/// </summary>
		public ILineCompare Comparer { get; set; }

		/// <summary>
		/// the configured line parser
		/// </summary>
		public ILineParser Parser 
		{
			get => _parser ?? (_parser = new LineParser());
			set => _parser = value;
		}

		/// <summary>
		/// update the snapshot
		/// </summary>
		public bool UpdateSnapshot { get; set; }

		/// <summary>
		/// gets a set of type mappers
		/// </summary>
		public MapperCollection<Type, ITypeMapper> TypeMappers { get; } = new MapperCollection<Type, ITypeMapper>();

		/// <summary>
		/// gets a set of value formatters
		/// </summary>
		public MapperCollection<Type, IValueFormatter> Formatters { get; set; } = new MapperCollection<Type, IValueFormatter>();

		/// <summary>
		/// setup the default options
		/// </summary>
		/// <param name="setup"></param>
		public static void Setup(Action<SnapshotOptions> setup)
		{
			var options = new SnapshotOptions
			{
				Formatters = new MapperCollection<Type, IValueFormatter>
				{
					{typeof(Type), new TypeFormatter()},
					{typeof(string), new StringFormatter()},
					{typeof(DateTime), new DateTimeFormatter()},
					{typeof(DateTime?), new DateTimeFormatter()}
				}
			};

			setup(options);

			Default = options;
		}

		/// <summary>
		/// create a set of options
		/// </summary>
		/// <param name="setup"></param>
		public static SnapshotOptions Create(Action<SnapshotOptions> setup)
		{
			var options = new SnapshotOptions();
			setup(options);

			return options;
		}
	}

	/// <summary>
	/// extensions for snapshotoptions
	/// </summary>
	public static class SnapshotOptionsExtensions
	{
		public static SnapshotOptions MergeDefault(this SnapshotOptions options)
		{
			if (options == null)
			{
				return SnapshotOptions.Default;
			}

			options.Comparer = options.Comparer ?? SnapshotOptions.Default.Comparer;
			options.UpdateSnapshot = options.UpdateSnapshot ? options.UpdateSnapshot : SnapshotOptions.Default.UpdateSnapshot;
			options.Parser = options.Parser ?? SnapshotOptions.Default.Parser;

			foreach (var formatter in SnapshotOptions.Default.Formatters.Keys)
			{
				if (options.Formatters.ContainsKey(formatter))
				{
					continue;
				}

				options.Formatters.Add(formatter, SnapshotOptions.Default.Formatters[formatter]);
			}

			foreach (var mapper in SnapshotOptions.Default.TypeMappers.Keys)
			{
				if (options.TypeMappers.ContainsKey(mapper))
				{
					continue;
				}

				options.TypeMappers.Add(mapper, SnapshotOptions.Default.TypeMappers[mapper]);
			}

			return options;
		}

		/// <summary>
		/// add a type mapper to the options
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="options"></param>
		/// <param name="map"></param>
		public static void AddMapper<T>(this SnapshotOptions options, Action<MapperContext, T> map) where T : class
		{
			options.TypeMappers.Add(typeof(T), new TypeMapper<T>(map));
		}

		/// <summary>
		/// alter the way lines are compared
		/// </summary>
		/// <param name="options"></param>
		/// <param name="comparer"></param>
		/// <returns></returns>
		public static SnapshotOptions SetComparer(this SnapshotOptions options, Func<Line, Line, bool> comparer)
		{
			options.Comparer = new LineCompare(comparer);
			return options;
		}

		/// <summary>
		/// add a parser directive to the tokenizer to alter the input value that used for the compare
		/// </summary>
		/// <param name="options"></param>
		/// <param name="directive"></param>
		/// <returns></returns>
		public static SnapshotOptions AddDirective(this SnapshotOptions options, Func<string, string> directive)
		{
			if (options.Parser == null)
			{
				options.Parser = new LineParser();
			}

			options.Parser.AddDirective(directive);
			return options;
		}

		/// <summary>
		/// sets updatesnapshot
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public static SnapshotOptions UpdateSavedSnapshot(this SnapshotOptions options)
		{
			options.UpdateSnapshot = true;
			return options;
		}

		public static SnapshotOptions AddFormatter(this SnapshotOptions options, Type key, IValueFormatter formatter)
		{
			options.Formatters.Add(key, formatter);
			return options;
		}

		public static SnapshotOptions UseBasicFormatters(this SnapshotOptions options)
		{
			options.Formatters = new MapperCollection<Type, IValueFormatter>
			{
				{typeof(Type), new TypeFormatter()},
				{typeof(string), new SimpleStringFormatter()}
			};

			return options;
		}
	}
}
