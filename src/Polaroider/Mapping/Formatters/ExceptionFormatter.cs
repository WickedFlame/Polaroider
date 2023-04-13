using System;

namespace Polaroider.Mapping.Formatters
{
    /// <summary>
    /// <see cref="IValueFormatter"/> for <see cref="Exception"/>
    /// </summary>
    public class ExceptionFormatter : IValueFormatter
    {
        /// <summary>
        /// Format the exception to a string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Format(object value)
        {
            if (value is Exception e)
            {
                return $"{e.Message}{Environment.NewLine}{e.StackTrace}";
            }

            return value?.ToString();
        }
    }
}
