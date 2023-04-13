using System;
using System.Reflection;
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
		/// create a instance of the snapshotoptions
		/// </summary>
		public SnapshotOptions()
		{
			Initialize(this);
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
		public MapperCollection<ITypeMapper> TypeMappers { get; } = new MapperCollection<ITypeMapper>();

		/// <summary>
		/// gets a set of value formatters
		/// </summary>
		public MapperCollection<IValueFormatter> Formatters { get; set; } = new MapperCollection<IValueFormatter>();

		/// <summary>
		/// Gets the Evaluator used by the Mapper for value types. Value types are simply added to the match with ToString()
		/// </summary>
		public Func<Type, object, bool> IsValueType{ get; internal set; }

		/// <summary>
		/// setup the default options
		/// </summary>
		/// <param name="expression"></param>
		public static void Setup(Action<SnapshotOptions> expression)
		{
			var options = new SnapshotOptions
			{
				Formatters = new MapperCollection<IValueFormatter>
				{
                    {typeof(Exception), new ExceptionFormatter()},
					{typeof(Type), new TypeFormatter()}, 
					{typeof(string), new StringFormatter()}, 
					{typeof(DateTime), new DateTimeFormatter()}, 
					{typeof(DateTime?), new DateTimeFormatter()},
					{typeof(MethodInfo), new MethodInfoFormatter()}
				}, 
				IsValueType = (type, obj) => (type.IsValueType || type == typeof(string)) && !type.IsGenericType && obj != null
			};


			expression(options);

			Default = options;
		}

		/// <summary>
		/// create a set of options
		/// </summary>
		/// <param name="expression"></param>
		public static SnapshotOptions Create(Action<SnapshotOptions> expression)
		{
			var options = new SnapshotOptions();
			Initialize(options);

			expression(options);

			return options;
		}

		private static void Initialize(SnapshotOptions options)
		{
			if (SnapshotOptions.Default == null)
			{
				return;
			}

			foreach (var formatter in SnapshotOptions.Default.Formatters.Keys)
			{
				if (options.Formatters.ContainsKey(formatter))
				{
					continue;
				}

				options.Formatters.Add(formatter, SnapshotOptions.Default.Formatters[formatter]);
			}
			options.IsValueType = SnapshotOptions.Default.IsValueType;
		}
	}

	/// <summary>
	/// extensions for snapshotoptions
	/// </summary>
	public static class SnapshotOptionsExtensions
	{
		/// <summary>
		/// merge the default options to the custom instance
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
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
		/// Add a parser directive to the tokenizer to alter the input value that used for the compare
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

		/// <summary>
		/// Add a formatter to the options to define how the type is converted to a string
		/// </summary>
		/// <param name="options"></param>
		/// <param name="key"></param>
		/// <param name="formatter"></param>
		/// <returns></returns>
		public static SnapshotOptions AddFormatter(this SnapshotOptions options, Type key, IValueFormatter formatter)
		{
			options.Formatters.Add(key, formatter);
			return options;
		}

		/// <summary>
		/// Add a formatter to the options to define how the type is converted to a string
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="options"></param>
		/// <param name="formatter"></param>
		/// <returns></returns>
		public static SnapshotOptions AddFormatter<T>(this SnapshotOptions options, Func<T, string> formatter)
		{
			options.Formatters.Add(typeof(T), new ValueFormatter<T>(formatter));
			return options;
		}

		/// <summary>
		/// Use the basic formatters to revert the breaking changes from v1 to v2.
		/// This removes all <see cref="IValueFormatter"/> from the options and adds a basic <see cref="TypeFormatter"/> and <see cref="SimpleStringFormatter"/>
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public static SnapshotOptions UseBasicFormatters(this SnapshotOptions options)
		{
			options.Formatters = new MapperCollection<IValueFormatter>
			{
				{typeof(Type), new TypeFormatter()},
				{typeof(string), new SimpleStringFormatter()}
			};

			return options;
		}

		/// <summary>
		/// Mocks changing <see cref="DateTime"/> values to a compareable format "0000-00-00T00:00:00.0000". Else the Snapshot would always fail due to changes in the time.
		/// This adds a <see cref="MockDateTimeFormatter"/> and a directive to the options.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public static SnapshotOptions MockDateTimes(this SnapshotOptions options)
		{
			var formatter = new MockDateTimeFormatter();
			options.AddFormatter(typeof(DateTime), formatter);
			options.AddFormatter(typeof(DateTime?), formatter);

			options.AddDirective(d => d.ReplaceDateTime());

			return options;
		}

		/// <summary>
		/// Mocks a <see cref="Guid"/> values to a compareable default format "00000000-0000-0000-0000-000000000000". Else the Snapshot would always fail due to changes in the Guid.
		/// This adds a <see cref="MockGuidFormatter"/> and a directive to the options.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public static SnapshotOptions MockGuids(this SnapshotOptions options)
		{
			var formatter = new MockGuidFormatter();
			options.AddFormatter(typeof(Guid), formatter);
			options.AddFormatter(typeof(Guid?), formatter);

			options.AddDirective(d => d.ReplaceGuid());

			return options;
		}

		/// <summary>
		/// Ignore whitespaces in the snapshot
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
        public static SnapshotOptions IgnoreWhiteSpaces(this SnapshotOptions options)
        {
			options.AddDirective(line => line.Replace(" ", string.Empty));

			return options;
        }

		/// <summary>
		/// set an expression to evaluate valuetypes. defaults to (type, obj) => (type.IsValueType || type == typeof(string)) &amp;&amp; !type.IsGenericType
		/// </summary>
		/// <param name="options"></param>
		/// <param name="evaluator"></param>
		/// <returns></returns>
		public static SnapshotOptions EvaluateValueType(this SnapshotOptions options, Func<Type, object, bool> evaluator)
		{
			options.IsValueType = evaluator;
			return options;
		}
	}
}
