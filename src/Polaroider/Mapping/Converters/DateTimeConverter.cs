using System;

namespace Polaroider.Mapping.Converters
{
	public class DateTimeConverter : IValueConverter
	{
		public string Convert(object value)
		{
			if (value is DateTime dte)
			{
				return dte.ToString("o");
			}

			return value?.ToString();
		}
	}
}
