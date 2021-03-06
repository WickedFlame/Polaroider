﻿using System.Text.RegularExpressions;

namespace Polaroider
{
    public static class StringExtensions
    {
        internal static string Indent(this string value, int indentation)
        {
            return "".PadLeft(indentation) + value;
        }

		/// <summary>
		/// replace a string based on a regex
		/// </summary>
		/// <param name="value"></param>
		/// <param name="regex"></param>
		/// <param name="replacement"></param>
		/// <returns></returns>
        public static string ReplaceRegex(this string value, string regex, string replacement)
        {
	        return Regex.Replace(value, regex, replacement, RegexOptions.Compiled);
        }

		/// <summary>
		/// replace a guid value with a common default
		/// </summary>
		/// <param name="value"></param>
		/// <param name="replacement"></param>
		/// <returns></returns>
        public static string ReplaceGuid(this string value, string replacement = "00000000-0000-0000-0000-000000000000")
        {
	        return value.ReplaceRegex("(?im)[{(]?[0-9A-Fa-f]{8}[-]?(?:[0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}[)}]?", replacement);
        }
    }
}
