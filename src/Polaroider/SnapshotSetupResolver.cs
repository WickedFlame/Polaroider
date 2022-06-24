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
        /// Resolves the snapshot setup based on the stacktrace
        /// </summary>
        /// <returns></returns>
        public SnapshotSetup ResolveSnapshotSetup()
        {
            var stackTrace = new StackTrace(1, true);
            StackFrame lastFrame = null;

            foreach (var stackFrame in stackTrace.GetFrames() ?? new StackFrame[0])
            {
                var method = stackFrame.GetMethod();

                if (IsInternalMethod(method))
                {
                    continue;
                }

                var name = stackFrame.GetFileName();
                
                if (!string.IsNullOrEmpty(name))
                {
                    // if a snapshotcreation is done inside a thread
                    // the stackframe is not really where it started of
                    // so we try to get the last stackframe that was inside the testclass
                    lastFrame = stackFrame;
                }

                if (!TestAttributeResolver.IsTestMethod(method))
                {
                    continue;
                }

                if (string.IsNullOrEmpty(name))
                {
                    // stackFrame.GetFileName() only works when pdb files are provided
                    // When using Live Unit Testing the checkbox for "Enable debug symbol..." has to be activated
                    throw new InvalidOperationException($"Polaroider could not find the file containing the Test {method.Name}.{Environment.NewLine}Polaroider relies on pdb to determine the path of the executing testclass.{Environment.NewLine}Please ensure that pdb files are generated when building the projects.{Environment.NewLine}When using Live Unit Testing make sure the checkbox for 'Enable debug symbol and xml documentation comment generation' is enabled in the Visual Studio Options.");
                }

                return new SnapshotSetup(name, method);
            }

            if (lastFrame != null)
            {
                return new SnapshotSetup(lastFrame.GetFileName(), lastFrame.GetMethod());
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
                    var attribute = method?.GetCustomAttributes()?.FirstOrDefault(a =>
                    {
                        var type = a.GetType();
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
