namespace JSim.Core.Common
{
    /// <summary>
    /// Represents an object that resides in a tree structure.
    /// </summary>
    public interface ITreeObject
    {
        /// <summary>
        /// Unique identifier for the object.
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// Unique name for the object.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Parent of this object.
        /// </summary>
        IHierarchicalTreeObject<ITreeObject> Parent { get; }

        /// <summary>
        /// Event fired when the objects properties have been modified.
        /// </summary>
        event TreeObjectModifiedEventHandler? ObjectModified;

        /// <summary>
        /// Moves the tree object to a new container.
        /// </summary>
        /// <param name="newAssembly">New container to attach this tree object to.</param>
        /// <returns>True if move was successful.</returns>
        bool MoveContainer(IHierarchicalTreeObject<ITreeObject> newAssembly);
    }
}
