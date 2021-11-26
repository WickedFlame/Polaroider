using System;

namespace Polaroider
{
    /// <summary>
    /// Used to mark MatchSnapshot Methods as assertion methods.
    /// This is used to tell Sonar scans that the method is used to throw assertions
    /// </summary>
    public class AssertionMethodAttribute : Attribute
    {
    }
}
