using System;

namespace Polaroider
{
    /// <summary>
    /// Attribute used to update the Snapshot. This can be added to the class or the method that the snapshot haas to be updated in
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UpdateSnapshotAttribute : Attribute
    {
    }
}
