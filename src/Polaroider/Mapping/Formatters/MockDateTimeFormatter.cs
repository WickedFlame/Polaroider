using System;

namespace Polaroider.Mapping.Formatters
{
	/// <summary>
	/// Mocking formatter for datetimes. Returns all DateTimes as "0000-00-00T00:00:00.0000" to ensure that changing datetimes can be compared.
	/// </summary>
	public class MockDateTimeFormatter : IValueFormatter
	{
		/// <summary>
		/// Mock a datetime to a string
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public string Format(object value)
		{
			if (value is DateTime)
			{
				return "0000-00-00T00:00:00.0000";
			}

			return value?.ToString();
		}
	}
}
