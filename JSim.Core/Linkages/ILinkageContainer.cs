namespace JSim.Core.Linkages
{
    /// <summary>
    /// Contains and manages a linkage tree, including holding the root,
    /// bubbling up messages and ensuring the tree is informed of upstream
    /// events.
    /// </summary>
    public interface ILinkageContainer
    {
        /// <summary>
        /// Gets the base linkage, which is the root of the linkage tree.
        /// </summary>
        ILinkage BaseLinkage { get; }
    }
}
