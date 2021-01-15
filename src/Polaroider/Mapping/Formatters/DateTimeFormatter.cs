using System;

namespace Polaroider.Mapping.Formatters
{
	/// <summary>
	/// Defautl formatter for datetimes
	/// </summary>
	public class DateTimeFormatter : IValueFormatter
	{
		/// <summary>
		/// format a datetime to a string
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public string Format(object value)
		{
			if (value is DateTime dte)
			{
				return dte.ToString("o");
			}

			return value?.ToString();
		}
	}
}
