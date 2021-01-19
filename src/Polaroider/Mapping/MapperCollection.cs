using System.Collections;
using System.Collections.Generic;

namespace Polaroider.Mapping
{
	/// <summary>
	/// defines a key/value collection for imappers
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TVal"></typeparam>
	public class MapperCollection<TKey, TVal> : IEnumerable<TVal> where TVal :class, IMapper
	{
		private readonly Dictionary<TKey, TVal> _items = new Dictionary<TKey, TVal>();

		/// <summary>
		/// gets the value stored behind the key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public TVal this[TKey key]
		{
			get
			{
				if (_items.ContainsKey(key))
				{
					return _items[key];
				}

				return null;
			}
		}

		/// <summary>
		/// gets a collection of keys in the mappercollection
		/// </summary>
		public IEnumerable<TKey> Keys => _items.Keys;

		/// <summary>
		/// add a new entry to the collection
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public void Add(TKey key, TVal value)
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
		public bool ContainsKey(TKey key)
		{
			return _items.ContainsKey(key);
		}

		/// <summary>
		/// remove the item from the collection
		/// </summary>
		/// <param name="key"></param>
		public void Remove(TKey key)
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
