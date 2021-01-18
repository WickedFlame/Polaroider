using System;
using System.Collections.Generic;
using Polaroider.Mapping;

namespace Polaroider
{
    /// <summary>
    /// definition for mapping objects to snapshots
    /// </summary>
    public class ObjectMapper
    {
        private readonly Dictionary<Type, IObjectMapper> _mappers = new Dictionary<Type, IObjectMapper>();

        private static ObjectMapper _mapper;

        private ObjectMapper()
        {
        }

        internal static ObjectMapper Mapper => _mapper ?? (_mapper = new ObjectMapper());

        /// <summary>
        /// Configure a mapping to convert a object to a snapshot 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="map"></param>
        public static void Configure<T>(Func<T, Snapshot> map) where T : class
        {
            var key = typeof(T);
            if (Mapper._mappers.ContainsKey(key))
            {
                Mapper._mappers.Remove(key);
            }

            Mapper._mappers.Add(key, new CustomMap<T>(map));
        }

        internal IObjectMapper GetMapper(Type type)
        {
            if (_mappers.ContainsKey(type))
            {
                return _mappers[type];
            }

            return new DefaultObjectMapper();
        }
    }
}
