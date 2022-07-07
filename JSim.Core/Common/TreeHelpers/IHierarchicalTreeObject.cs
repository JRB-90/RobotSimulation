namespace JSim.Core.Common
{
    /// <summary>
    /// Represents a tree object that is hierarchical.
    /// </summary>
    public interface IHierarchicalTreeObject<TParent, TChild> 
      : 
        ITreeObject<TParent>
        where TChild : ITreeObject
        where TParent : ITreeObject
    {
        /// <summary>
        /// Designates that this object is the top nodes of the tree.
        /// </summary>
        bool IsTreeRoot { get; }

        /// <summary>
        /// Container for the child nodes of this object.
        /// </summary>
        IReadOnlyCollection<TChild> Children { get; }

        /// <summary>
        /// Attaches a given child node to this object.
        /// </summary>
        /// <param name="child">Child node to attach.</param>
        /// <returns>True if successful. False if child is already attached.</returns>
        bool AttachChild(TChild child);

        /// <summary>
        /// Detaches a given child from this objects children.
        /// </summary>
        /// <param name="child">Child to be detached.</param>
        /// <returns>True if successful, False if child cannot be found.</returns>
        bool DetachChild(TChild child);
    }
}
