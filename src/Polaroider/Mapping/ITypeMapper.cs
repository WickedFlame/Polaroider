namespace Polaroider.Mapping
{
	public interface ITypeMapper : IMapper
	{
		void Map(MapperContext ctx, object item);
	}
}
