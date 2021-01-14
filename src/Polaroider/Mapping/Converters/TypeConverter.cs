namespace Polaroider.Mapping.Converters
{
	public class TypeConverter : IValueConverter
	{
		public string Convert(object value)
		{
			return value?.ToString();
		}
	}
}
