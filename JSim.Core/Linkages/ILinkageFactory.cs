using JSim.Core.Common;

namespace JSim.Core.Linkages
{
    /// <summary>
    /// Interface for all linkage factories.
    /// </summary>
    public interface ILinkageFactory
    {
        ILinkage CreateRootLinkage(
           INameRepository nameRepository,
           IMessageCollator messageCollator,
           ILinkageCreator creator
        );

        ILinkage CreateLinkage(
           INameRepository nameRepository,
           IMessageCollator messageCollator,
           ILinkageCreator creator,
           ILinkage parentGeometry
        );

        void Destroy(ILinkage geometry);
    }
}
