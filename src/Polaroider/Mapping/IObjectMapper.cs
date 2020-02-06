
namespace Polaroider.Mapping
{
    public interface IObjectMapper
    {
        /// <summary>
        /// map an object to a snapshot
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        Snapshot Map<T>(T item);
    }
}
