using System;
using System.Text.RegularExpressions;

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
                return $"{e.Message}{Environment.NewLine}{CleanStackTrace(e.StackTrace)}";
            }

            return value?.ToString();
        }

        private string CleanStackTrace(string stackTrace)
        {
            if (stackTrace == null)
            {
                return string.Empty;
            }

            var regex = new Regex("( in )(.*)(:line )([0-9]*)");
            return regex.Replace(stackTrace, string.Empty);
        }
    }
}
