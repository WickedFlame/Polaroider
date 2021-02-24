using System;

namespace Polaroider.Mapping.Formatters
{
	/// <summary>
	/// A generic formatter that is used internally when using the generic formatter
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ValueFormatter<T> : IValueFormatter
	{
		private readonly Func<T, string> _formatter;

		/// <summary>
		/// Creates a valueformatter
		/// </summary>
		/// <param name="formatter"></param>
		public ValueFormatter(Func<T, string> formatter)
		{
			_formatter = formatter;
		}

		/// <summary>
		/// Uses the Formatter from the constructor to format the object to a string
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public string Format(object value)
		{
			return _formatter((T)value);
		}
	}
}
