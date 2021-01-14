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
        private readonly Dictionary<Type, ITypeMapper> _typeMappers = new Dictionary<Type, ITypeMapper>();

        private static ObjectMapper _mapper;

        private ObjectMapper()
        {
        }

        internal static ObjectMapper Mapper => _mapper ?? (_mapper = new ObjectMapper());

        public Dictionary<Type, ITypeMapper> TypeMappers => _typeMappers;

        public ITypeMapper GetTypeMapper(Type type)
        {
	        if (_typeMappers.ContainsKey(type))
	        {
		        return _typeMappers[type];
	        }

			return null;
        }

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

        public static void Configure<T>(Action<MapperContext, T> map) where T : class
        {
	        Mapper.TypeMappers.Add(typeof(T), new TypeMapper<T>(map));
        }

        internal IObjectMapper GetMapper(Type type)
        {
            if (_mappers.ContainsKey(type))
            {
                return _mappers[type];
            }

            return new DefaultObjectMapper();
        }

        public static void Remove<T>()
        {
	        if (Mapper.TypeMappers.ContainsKey(typeof(T)))
	        {
		        Mapper.TypeMappers.Remove(typeof(T));
	        }
        }
    }
}
