using System.Collections;
using System.Linq;
using System.Reflection;

namespace Polaroider.Mapping
{
	/// <summary>
	/// default object mapper
	/// </summary>
	public class DefaultObjectMapper : IObjectMapper
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
			MapProperies(item, snapshot, 0, options);

			return snapshot;
		}

		private void MapProperies<TObj>(TObj item, Snapshot sb, int indentation, SnapshotOptions options)
		{
			if (item == null)
			{
				return;
			}

			if (item is IEnumerable list && !(item is string))
			{
				foreach (var lstItem in list)
				{
					MapProperies(lstItem, sb, indentation, options);
				}

				return;
			}

            var type = item.GetType();
            if (options.IsValueType(type, item))
            {
                sb.Add(new Line($"{item}".Indent(indentation)));
                return;
            }

			foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance).OrderBy(p => p.Name))
			{
				var line = $"{property.Name}:".Indent(indentation);

				var formatter = options.Formatters[property.PropertyType];
				if (formatter != null)
				{
					sb.Add(new Line($"{line} {formatter.Format(property.GetValue(item))}"));
					continue;
				}

				if (property.PropertyType.IsValueType)
				{
					sb.Add(new Line($"{line} {property.GetValue(item)}"));
					continue;
				}

				sb.Add(new Line(line));

				var typeMapper = options.TypeMappers[property.PropertyType];
				if (typeMapper != null)
				{
					var ctx = new MapperContext(sb, indentation + 2);
					typeMapper.Map(ctx, property.GetValue(item));
					continue;
				}

				MapProperies(property.GetValue(item), sb, indentation + 2, options);
			}
		}
	}
}
