using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Polaroider.Mapping
{
	/// <summary>
	/// defines a key/value collection for imappers
	/// </summary>
	/// <typeparam name="TVal"></typeparam>
	public class MapperCollection<TVal> : IEnumerable<TVal> where TVal : class, IMapper
	{
		private readonly Dictionary<Type, TVal> _items = new Dictionary<Type, TVal>();

		/// <summary>
		/// gets the value stored behind the key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public TVal this[Type key]
		{
			get
			{
				if (_items.ContainsKey(key))
				{
					return _items[key];
				}

                if (Keys.Any(k => k.IsAssignableFrom(key)))
                {
                    var type = Keys.FirstOrDefault(k => k.IsAssignableFrom(key));
                    return _items[type];
                }

				return null;
			}
		}

		/// <summary>
		/// gets a collection of keys in the mappercollection
		/// </summary>
		public IEnumerable<Type> Keys => _items.Keys;

		/// <summary>
		/// add a new entry to the collection
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public void Add(Type key, TVal value)
		{
			if (ContainsKey(key))
			{
				Remove(key);
			}

			_items.Add(key, value);
		}

		/// <summary>
		/// gets if the key is already contained in the collection
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool ContainsKey(Type key)
		{
			return _items.ContainsKey(key);
		}

		/// <summary>
		/// remove the item from the collection
		/// </summary>
		/// <param name="key"></param>
		public void Remove(Type key)
		{
			_items.Remove(key);
		}

		/// <summary>
		/// gets the enumerator for the collection
		/// </summary>
		/// <returns></returns>
		public IEnumerator<TVal> GetEnumerator()
		{
			return _items.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
