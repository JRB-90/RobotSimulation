using JSim.Core.Common;
using JSim.Core.Render;

namespace JSim.Core.Linkages
{
    /// <summary>
    /// Represents a mechanical linkage, used to build kinematic objects.
    /// </summary>
    public interface ILinkage : IHierarchicalTreeObject<ILinkage>, IPositionable
    {
        IGeometryContainer GeometryContainer { get; }

        /// <summary>
        /// Creates a new linkage attached to this one.
        /// </summary>
        /// <returns>Newly created linkage object.</returns>
        ILinkage CreateNewLinkage();
    }
}
