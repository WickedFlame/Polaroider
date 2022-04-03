using System.Linq;
using System.Reflection;

namespace Polaroider
{
    internal static class TypeExtensions
    {
        public static bool IsAnonymous(this MethodInfo method)
        {
            var invalidChars = new[] { '<', '>' };
            return method.Name.Any(invalidChars.Contains);
        }
    }
}
