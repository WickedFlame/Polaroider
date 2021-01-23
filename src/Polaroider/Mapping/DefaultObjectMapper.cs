using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace Polaroider.Mapping
{
	/// <summary>
	/// default object mapper
	/// </summary>
	public class DefaultObjectMapper : IObjectMapper, ITypeMapper
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
			var ctx = new MapperContext(snapshot, options, 0);

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

			foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance).OrderBy(p => p.Name))
			{
				var header = $"{property.Name}:".Indent(ctx.Indentation);

				//formatter = ctx.Options.Formatters[property.PropertyType];
				//if (formatter != null)
				//{
				//	ctx.AddLine(new Line($"{header} {formatter.Format(property.GetValue(item))}"));
				//	continue;
				//}

				//if (ctx.Options.IsValueType(property.PropertyType, item))
				//{
				//	ctx.AddLine(new Line($"{header} {property.GetValue(item)}"));
				//	continue;
				//}
				var value = property.GetValue(item);
				if (MapValueType(ctx, property.PropertyType, value, $"{header} "))
				{
					continue;
				}

				ctx.AddLine(new Line(header));

				var typeMapper = ctx.Options.TypeMappers[property.PropertyType];
				if (typeMapper != null)
				{
					typeMapper.Map(new MapperContext(ctx.Snapshot, ctx.Options, ctx.Indentation + 2), value);
					continue;
				}

				Map(new MapperContext(ctx.Snapshot, ctx.Options, ctx.Indentation + 2), value);
			}
		}

		private bool MapValueType(MapperContext ctx, Type type, object item, string prefix)
		{
			var formatter = ctx.Options.Formatters[type];
			if (formatter != null)
			{
				ctx.AddLine(new Line($"{prefix}{formatter.Format(item)}"));
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
