namespace JSim.Core.Common
{
    /// <summary>
    /// Represents a container 
    /// </summary>
    public interface IChildContainer<T> : IEnumerable<T>
    {
        /// <summary>
        /// Collection of all the child nodes attached to this container.
        /// </summary>
        IReadOnlyCollection<T> Children { get; }

        /// <summary>
        /// Event fired when a child has been added to the container.
        /// </summary>
        event ChildAddedEventHandler<T>? ChildAdded;

        /// <summary>
        /// Event fired when a child has been removed from the container.
        /// </summary>
        event ChildRemovedEventHandler<T>? ChildRemoved;

        /// <summary>
        /// Event fired when the number children in the container has been modified.
        /// </summary>
        event ChildContainerModifiedEventHandler? ChildContainerModified;

        /// <summary>
        /// Clears all currently attached child nodes.
        /// </summary>
        void ClearAllChildren();

        /// <summary>
        /// Attaches a child object to this containers children.
        /// </summary>
        /// <param name="child">Scene object to attach to this assembly.</param>
        /// <returns>Returns true if successful.</returns>
        bool AttachChild(T child);

        /// <summary>
        /// Detaches a child object from this containers children.
        /// </summary>
        /// <param name="child">Scene object to remove from this assembly.</param>
        /// <returns>Returns true if successfully removed.</returns>
        bool DetachChild(T child);
    }
}
