using JSim.Core.Common;
using JSim.Core.Render;

namespace JSim.Core.Linkages
{
    /// <summary>
    /// Represents a mechanical linkage, used to build kinematic objects.
    /// </summary>
    public interface ILinkage : IPositionableHierarcyTreeObject<ILinkage>
    {
        /// <summary>
        /// Container for the geometry attached to this link.
        /// </summary>
        IGeometryContainer GeometryContainer { get; }

        /// <summary>
        /// Creates a new linkage attached to this one.
        /// </summary>
        /// <returns>Newly created linkage object.</returns>
        ILinkage CreateNewLinkage();
    }
}
