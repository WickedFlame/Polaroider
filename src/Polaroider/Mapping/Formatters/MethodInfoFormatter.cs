using System.Linq;
using System.Reflection;

namespace Polaroider.Mapping.Formatters
{
	/// <summary>
	/// Default formatter for MethodInfo
	/// </summary>
	public class MethodInfoFormatter : IValueFormatter
	{
		/// <summary>
		/// Format MethodInfo to a string
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public string Format(object value)
		{
			if (value is MethodInfo mi)
			{
				return $"{mi.Name}({string.Join(",", mi.GetParameters().Select(x => x.ParameterType.FullName))})";
			}

			return value?.ToString();
		}
	}
}
