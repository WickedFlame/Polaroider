using System.Collections;
using System.Collections.Generic;

namespace Polaroider.Mapping
{
	public class MapperCollection<TKey, TVal> : IEnumerable<TVal> where TVal :class, IMapper
	{
		private readonly Dictionary<TKey, TVal> _items = new Dictionary<TKey, TVal>();

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

		public IEnumerable<TKey> Keys => _items.Keys;

		public void Add(TKey key, TVal value)
		{
			if (ContainsKey(key))
			{
				Remove(key);
			}

			_items.Add(key, value);
		}

		public bool ContainsKey(TKey key)
		{
			return _items.ContainsKey(key);
		}

		public void Remove(TKey key)
		{
			_items.Remove(key);
		}

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
