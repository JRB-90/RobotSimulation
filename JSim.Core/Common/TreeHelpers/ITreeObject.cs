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
        /// Event fired when the objects properties have been modified.
        /// </summary>
        event TreeObjectModifiedEventHandler? ObjectModified;
    }

    /// <summary>
    /// Represents an object that resides in a tree structure.
    /// </summary>
    public interface ITreeObject<TParent> : ITreeObject
    {
        /// <summary>
        /// Parent of this object.
        /// </summary>
        TParent? Parent { get; set; }

        /// <summary>
        /// Attaches the tree object to a new node.
        /// </summary>
        /// <param name="newParent">New parent to attach this node to.</param>
        /// <returns>True if move was successful.</returns>
        bool AttachTo(TParent? newParent);

        /// <summary>
        /// Removes the object from it's parent.
        /// </summary>
        /// <returns>True if successful.</returns>
        bool Detach();
    }
}
