using System.Collections;
using System.Linq;
using System.Reflection;

namespace Polaroider.Mapping
{
    public class DefaultObjectMapper : IObjectMapper
    {
        public Snapshot Map<T>(T item)
        {
            var snapshot = new Snapshot();
            MapProperies(item, snapshot, 0);

            return snapshot;
        }

        private void MapProperies<TObj>(TObj item, Snapshot sb, int indentation)
        {
            if (item is IList list)
            {
                foreach (var lstItem in list)
                {
                    MapProperies(lstItem, sb, indentation);
                }

                return;
            }

            var type = item.GetType();
            if (type.IsValueType || type == typeof(string))
            {
                sb.Add(new Line($"{item}".Indent(indentation)));
                return;
            }

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance).OrderBy(p => p.Name))
            {
                var line = $"{property.Name}:".Indent(indentation);

                if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                {
                    sb.Add(new Line($"{line} {property.GetValue(item)}"));
                    continue;
                }

                sb.Add(new Line(line));
                MapProperies(property.GetValue(item), sb, indentation + 2);
            }
        }
    }
}
