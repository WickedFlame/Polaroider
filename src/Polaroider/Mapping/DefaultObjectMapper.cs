﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Polaroider.Mapping.Converters;

namespace Polaroider.Mapping
{
	/// <summary>
	/// default object mapper
	/// </summary>
	public class DefaultObjectMapper : IObjectMapper
	{
		/// <summary>
		/// map an object to a snapshot
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item"></param>
		/// <returns></returns>
		public Snapshot Map<T>(T item)
		{
			var snapshot = new Snapshot();
			MapProperies(item, snapshot, 0);

			return snapshot;
		}

		public static Dictionary<Type, IValueConverter> _converters = new Dictionary<Type, IValueConverter>
		{
			{typeof(Type), new TypeConverter()},
			{typeof(string), new StringConverter()}
		};

		private void MapProperies<TObj>(TObj item, Snapshot sb, int indentation)
		{
			if (item == null)
			{
				return;
			}

			if (item is IEnumerable list && !(item is string))
			{
				foreach (var lstItem in list)
				{
					MapProperies(lstItem, sb, indentation);
				}

				return;
			}

			var type = item.GetType();
			if (type.IsValueType || type == typeof(string))
			{
				sb.Add(new Line($"{item}".Indent(indentation)));
				return;
			}

			foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance).OrderBy(p => p.Name))
			{
				var line = $"{property.Name}:".Indent(indentation);

				if (property.PropertyType.IsValueType)
				{
					sb.Add(new Line($"{line} {property.GetValue(item)}"));
					continue;
				}

				if (_converters.ContainsKey(property.PropertyType))
				{
					var converter = _converters[property.PropertyType];
					sb.Add(new Line($"{line} {converter.Convert(property.GetValue(item))}"));
					continue;
				}

				sb.Add(new Line(line));

				var typeMapper = ObjectMapper.Mapper.GetTypeMapper(property.PropertyType);
				if (typeMapper != null)
				{
					var ctx = new MapperContext(sb, indentation + 2);
					typeMapper.Map(ctx, property.GetValue(item));
					continue;
				}

				MapProperies(property.GetValue(item), sb, indentation + 2);
			}
		}
	}
}
