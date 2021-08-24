using System;

namespace Polaroider.Mapping.Formatters
{
	/// <summary>
	/// Mocking formatter for Guids. Returns all Guids as "00000000-0000-0000-0000-000000000000" to ensure that changing guids can be compared.
	/// </summary>
	public class MockGuidFormatter : IValueFormatter
	{
		/// <summary>
		/// Mock a Guid to a string
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public string Format(object value)
		{
			if (value is Guid)
			{
				return "00000000-0000-0000-0000-000000000000";
			}

			return value?.ToString();
		}
	}
}
