using System;

namespace Polaroider
{
    /// <summary>
    /// Defines the name of the snapfhotfile that is used for the test
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SnapshotNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the SnapshotNameAttribute class with the specified snapshot name.
        /// </summary>
        /// <param name="name">The name to associate with the snapshot. Cannot be null or empty.</param>
        public SnapshotNameAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name for the snapshotfile
        /// </summary>
        public string Name { get; }
    }
}
