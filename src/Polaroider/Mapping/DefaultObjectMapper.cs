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
            if (ctx.Options.IsValueType(type, item))
            {
	            ctx.AddLine(new Line($"{item}".Indent(ctx.Indentation)));
                return;
            }

			foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance).OrderBy(p => p.Name))
			{
				var line = $"{property.Name}:".Indent(ctx.Indentation);

				var formatter = ctx.Options.Formatters[property.PropertyType];
				if (formatter != null)
				{
					ctx.AddLine(new Line($"{line} {formatter.Format(property.GetValue(item))}"));
					continue;
				}

				if (property.PropertyType.IsValueType)
				{
					ctx.AddLine(new Line($"{line} {property.GetValue(item)}"));
					continue;
				}

				ctx.AddLine(new Line(line));

				var typeMapper = ctx.Options.TypeMappers[property.PropertyType];
				if (typeMapper != null)
				{
					typeMapper.Map(new MapperContext(ctx.Snapshot, ctx.Options, ctx.Indentation + 2), property.GetValue(item));
					continue;
				}

				Map(new MapperContext(ctx.Snapshot, ctx.Options, ctx.Indentation + 2), property.GetValue(item));
			}
		}
	}
}
