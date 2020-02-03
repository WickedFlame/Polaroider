using System;
using System.Collections.Generic;
using System.Text;

namespace Polaroider
{
    internal static class StringExtensions
    {
        public static string Indent(this string value, int indentation)
        {
            return "".PadLeft(indentation) + value;
        }
    }
}
