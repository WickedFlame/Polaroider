
namespace Polaroider.Mapping
{
    /// <summary>
    /// Interface for mapping objects to a snapshot
    /// </summary>
    public interface IObjectMapper : IMapper
    {
        /// <summary>
        /// map an object to a snapshot
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Snapshot Map<T>(T item, SnapshotOptions options);
    }
}
