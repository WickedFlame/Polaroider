namespace Polaroider.Mapping.Converters
{
	public class StringConverter : IValueConverter
	{
		public string Convert(object value)
		{
			var str = value as string ?? "null";
			if (string.IsNullOrEmpty(str))
			{
				str = "''";
			}

			return str;
		}
	}
}
