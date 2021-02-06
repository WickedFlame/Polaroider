using Polaroider.Mapping;

namespace Polaroider
{
	public static class MapperContextExtensions
	{
		/// <summary>
		/// Map the object to the Snapshot
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="context"></param>
		/// <param name="property">The propertyname that the item is mapped to</param>
		/// <param name="item">The object to be mapped</param>
		public static void MapObject<T>(this MapperContext context, string property, T item)
		{
			context.AddLine(new Line($"{property}:".Indent(context.Indentation)));
			context.Mapper.Map(context.Clone(context.Indentation + 2), item);
		}

		/// <summary>
		/// Clones the current instance of the MapperContext with the new indentation
		/// </summary>
		/// <param name="context"></param>
		/// <param name="indentation"></param>
		/// <returns></returns>
		public static MapperContext Clone(this MapperContext context, int indentation)
		{
			return new MapperContext(context.Mapper, context.Snapshot, context.Options, indentation);
		}
	}
}
