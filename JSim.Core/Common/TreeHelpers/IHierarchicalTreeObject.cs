namespace JSim.Core.Common
{
    public interface IHierarchicalTreeObject : ITreeObject
    {
    }

    /// <summary>
    /// Represents a tree object that is hierachical, with a 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHierarchicalTreeObject<T> : IHierarchicalTreeObject
    {
        /// <summary>
        /// Designates that this object is the top nodes of the tree.
        /// </summary>
        bool IsTreeRoot { get; }

        /// <summary>
        /// Container for the child nodes of this object.
        /// </summary>
        IReadOnlyCollection<T> Children { get; }
    }
}
