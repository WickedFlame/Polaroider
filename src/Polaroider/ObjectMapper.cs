﻿using System;
using System.Collections.Generic;
using Polaroider.Mapping;

namespace Polaroider
{
    public class ObjectMapper
    {
        private readonly Dictionary<Type, IObjectMapper> _mappers = new Dictionary<Type, IObjectMapper>();
        private static ObjectMapper _mapper;

        private ObjectMapper()
        {
        }

        internal static ObjectMapper Mapper => _mapper ?? (_mapper = new ObjectMapper());

        public static void Configure<T>(Func<T, Snapshot> map) where T : class
        {
            Mapper._mappers.Add(typeof(T), new CustomMap<T>(map));
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