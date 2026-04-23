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
			options.MergeDefault();

			expression(options);

			return options;
		}

		private static void Initialize(SnapshotOptions options)
		{
			if (SnapshotOptions.Default == null)
			{
				return;
			}

			options.IsValueType = SnapshotOptions.Default.IsValueType;
		}
	}
}
