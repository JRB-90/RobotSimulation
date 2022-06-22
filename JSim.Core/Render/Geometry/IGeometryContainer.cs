using JSim.Core.Maths;

namespace JSim.Core.Render
{
    /// <summary>
    /// Interface to define a holder and maintainer of a geometry tree.
    /// </summary>
    public interface IGeometryContainer
    {
        /// <summary>
        /// Gets the top level node of the geometry tree.
        /// </summary>
        IGeometry Root { get; }

        /// <summary>
        /// Event fired when a node in the geometry tree has been modified.
        /// </summary>
        event GeometryTreeModifiedEventHandler? GeometryTreeModified;

        /// <summary>
        /// Updates the geometry container with the world position of the entity it belongs to.
        /// </summary>
        /// <param name="worldPositionOfParent">Position in world of the parent entity.</param>
        void UpdateWorldPosition(Transform3D worldPositionOfParent);
    }
}
