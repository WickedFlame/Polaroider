using System;

namespace Polaroider.Mapping.Formatters
{
	/// <summary>
	/// Defautl formatter for datetimes. Formats all DateTimes to a ISO 8601 string
	/// </summary>
	public class DateTimeFormatter : IValueFormatter
	{
		/// <summary>
		/// Format a datetime to a ISO 8601 string
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
