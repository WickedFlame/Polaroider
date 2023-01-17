using System;

namespace Polaroider.Mapping
{
	/// <summary>
	/// Map a give type
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class TypeMapper<T> : ITypeMapper where T : class
	{
		private readonly Action<MapperContext, T> _map;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="map"></param>
		public TypeMapper(Action<MapperContext, T> map)
		{
			_map = map;
		}

		/// <summary>
		/// Default mapper to  map the object to the context
		/// </summary>
		/// <param name="ctx"></param>
		/// <param name="item"></param>
		public void Map(MapperContext ctx, object item)
		{
			Map(ctx, item as T);
		}

		/// <summary>
		/// Mapper to  map the object to the context
		/// </summary>
		/// <param name="ctx"></param>
		/// <param name="item"></param>
		public void Map(MapperContext ctx, T item)
		{
			_map.Invoke(ctx, item);
		}
	}
}
