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
