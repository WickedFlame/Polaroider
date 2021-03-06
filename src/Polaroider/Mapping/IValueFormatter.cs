﻿namespace Polaroider.Mapping
{
	/// <summary>
	/// Interface for formatting types to string
	/// </summary>
	public interface IValueFormatter : IMapper
	{
		/// <summary>
		/// Format the object to a string
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		string Format(object value);
	}
}
