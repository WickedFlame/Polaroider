namespace Polaroider.Mapping
{
	public interface ITypeMapper
	{
		void Map(MapperContext ctx, object item);
	}
}
