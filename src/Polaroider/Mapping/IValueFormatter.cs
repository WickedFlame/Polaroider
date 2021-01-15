namespace Polaroider.Mapping
{
	/// <summary>
	/// Interface for formatting types to string
	/// </summary>
	public interface IValueFormatter
	{
		/// <summary>
		/// format the object to a string
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		string Format(object value);
	}
}
