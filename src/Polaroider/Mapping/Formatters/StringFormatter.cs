namespace Polaroider.Mapping.Formatters
{
	/// <summary>
	/// Default formatter for plain strings
	/// </summary>
	public class StringFormatter : IValueFormatter
	{
		/// <summary>
		/// format a plain string
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public string Format(object value)
		{
			var str = value as string ?? "null";
			if (string.IsNullOrEmpty(str))
			{
				str = "''";
			}

			return str;
		}
	}

	/// <summary>
	/// Simple string formatter
	/// </summary>
	public class SimpleStringFormatter : IValueFormatter
	{
		/// <summary>
		/// format a plain string
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public string Format(object value)
		{
			return value as string ?? "";
		}
	}
}
