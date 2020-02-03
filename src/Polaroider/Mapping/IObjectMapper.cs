
namespace Polaroider.Mapping
{
    public interface IObjectMapper
    {
        Snapshot Map<T>(T item);
    }
}
