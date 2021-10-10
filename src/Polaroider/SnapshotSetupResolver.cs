using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Polaroider
{
    /// <summary>
    /// Create snapshotsetups
    /// </summary>
    public class SnapshotSetupResolver
    {
        /// <summary>
        /// Resoves the snapshot setup based on the stacktrace
        /// </summary>
        /// <returns></returns>
        public SnapshotSetup ResolveSnapshotSetup()
        {
            var stackTrace = new StackTrace(1, true);
            foreach (var stackFrame in stackTrace.GetFrames() ?? new StackFrame[0])
            {
                var method = stackFrame.GetMethod();

                if (IsInternalMethod(method))
                {
                    continue;
                }

                if (!TestAttributeResolver.IsTestMethod(method))
                {
                    continue;
                }

                return new SnapshotSetup(stackFrame.GetFileName(), method);
            }

            return null;
        }

        private bool IsInternalMethod(MemberInfo method)
        {
            var methodAssembly = method?.DeclaringType?.Assembly.FullName;
            var internalAssembly = Assembly.GetAssembly(typeof(Snapshot)).FullName;
            return methodAssembly == internalAssembly;
        }

        private static class TestAttributeResolver
        {
            private static readonly IEnumerable<string> _attributes = new List<string>
            {
                "nunit.framework.",
                "nunit.framework.testattribute",
                "microsoft.visualstudio.testtools.unittesting.testmethodattribute",
				"xunit.factattribute"
            };

            internal static bool IsTestMethod(MemberInfo method)
            {
                Func<string, bool> methodContainsAttribute = attributeName =>
                {
                    var attribute = method?.CustomAttributes.FirstOrDefault(a =>
                    {
                        var type = a.AttributeType;
                        do
                        {
                            if (type.FullName.ToLower().StartsWith(attributeName))
                            {
	                            return true;
                            }

                            type = type.BaseType;
                        } while (type != null);

                        return false;
                    });

                    return attribute != null;
                };

                return _attributes.Any(a => methodContainsAttribute(a));
            }
        }
    }
}
