using System;

namespace Polaroider.Mapping
{
	public class TypeMapper<T> : ITypeMapper where T : class
	{
		private readonly Action<MapperContext, T> _map;

		public TypeMapper(Action<MapperContext, T> map)
		{
			_map = map;
		}

		public void Map(MapperContext ctx, object item)
		{
			Map(ctx, item as T);
		}

		public void Map(MapperContext ctx, T item)
		{
			_map.Invoke(ctx, item);
		}
	}
}
