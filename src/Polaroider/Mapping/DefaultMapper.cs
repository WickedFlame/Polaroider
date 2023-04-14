using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace Polaroider.Mapping
{
	/// <summary>
	/// default object mapper
	/// </summary>
	public class DefaultMapper : IObjectMapper, ITypeMapper
	{
		/// <summary>
		/// map an object to a snapshot
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public Snapshot Map<T>(T item, SnapshotOptions options)
		{
			var snapshot = new Snapshot();
			var ctx = new MapperContext(this, snapshot, options, 0);

			Map(ctx, item);

			return snapshot;
		}

		/// <summary>
		/// map the object to the context
		/// </summary>
		/// <param name="ctx"></param>
		/// <param name="item"></param>
		public void Map(MapperContext ctx, object item)
		{
			if (item == null)
			{
				return;
			}

			if (item is IEnumerable list && !(item is string))
			{
				foreach (var lstItem in list)
				{
					Map(ctx, lstItem);
				}

				return;
			}

			var type = item.GetType();

			if (MapValueType(ctx, type, item, string.Empty.Indent(ctx.Indentation)))
			{
				return;
			}

			// check if the root object is registered as a typed map
			if (MapRegisteredType(type, ctx.Clone(ctx.Indentation), item))
			{
				return;
			}

			foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
				// indexers can't be mapped so ignore all properties with index parameters
				.Where(p => p.GetIndexParameters().Length == 0)
				// only properties that have public getters
				.Where(p => p.GetGetMethod() != null)
				.OrderBy(p => p.Name))
			{
				var header = $"{property.Name}:".Indent(ctx.Indentation);
				var value = property.GetValue(item);

				if (MapValueType(ctx, property.PropertyType, value, $"{header} "))
				{
					continue;
				}

				ctx.AddLine(new Line(header));

				if (MapRegisteredType(property.PropertyType, ctx.Clone(ctx.Indentation + 2), value))
				{
					continue;
				}

				Map(ctx.Clone(ctx.Indentation + 2), value);
			}
		}

		private bool MapRegisteredType(Type type, MapperContext ctx, object item)
		{
			var typeMapper = ctx.Options.TypeMappers[type];
			if (typeMapper != null)
			{
				typeMapper.Map(ctx, item);
				return true;
			}

			return false;
		}

		private bool MapValueType(MapperContext ctx, Type type, object item, string prefix)
		{
			var formatter = ctx.Options.Formatters[type];
			if (formatter != null)
            {
                var value = formatter.Format(item) ?? string.Empty;
				foreach(var line in value.Split(new[]{Environment.NewLine}, StringSplitOptions.None))
                {
                    ctx.AddLine(new Line($"{prefix}{line}"));
                }

				return true;
			}

			if (ctx.Options.IsValueType(type, item))
			{
				ctx.AddLine(new Line($"{prefix}{item}"));
				return true;
			}

			return false;
		}
	}
}
