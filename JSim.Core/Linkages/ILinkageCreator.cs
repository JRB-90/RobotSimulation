namespace JSim.Core.Linkages
{
    /// <summary>
    /// Interface defining the behaviour of all linkage creators.
    /// </summary>
    public interface ILinkageCreator : IDisposable
    {
        /// <summary>
        /// Creates a new linkage object, with no parent (root node).
        /// </summary>
        /// <returns>New linkage object.</returns>
        ILinkage CreateRootLinkage();

        /// <summary>
        /// Creates a new linkage object, childed to the given link.
        /// </summary>
        /// <param name="parentLinkage">Parent node this linkage should be attached to.</param>
        /// <returns>New linakge object.</returns>
        ILinkage CreateLinkage(ILinkage parentLinkage);
    }
}
