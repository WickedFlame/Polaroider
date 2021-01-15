namespace Polaroider.Mapping.Formatters
{
	/// <summary>
	/// Default formatter for Type
	/// </summary>
	public class TypeFormatter : IValueFormatter
	{
		/// <summary>
		/// format Type to a string
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public string Format(object value)
		{
			return value?.ToString();
		}
	}
}
