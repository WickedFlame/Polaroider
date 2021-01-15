namespace Polaroider.Mapping
{
	/// <summary>
	/// Interface for formatting types to string
	/// </summary>
	public interface IValueFormatter
	{
		string Format(object value);
	}
}
