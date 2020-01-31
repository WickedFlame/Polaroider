using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Polaroid
{
    public class SnapshotIdResolver
    {
        public SnapshotId ResloveId()
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

                return new SnapshotId(stackFrame.GetFileName(), method);
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
                "NUnit.Framework.",
                "NUnit.Framework.TestAttribute",
                "Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute"
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
                            if (type.FullName.StartsWith(attributeName))
                                return true;

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
