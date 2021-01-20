namespace Polaroider.Mapping
{
	/// <summary>
	/// Map an object to the context. Gets called from the default mapper
	/// </summary>
	public interface ITypeMapper : IMapper
	{
		/// <summary>
		/// map the object to the context
		/// </summary>
		/// <param name="ctx"></param>
		/// <param name="item"></param>
		void Map(MapperContext ctx, object item);
	}
}
