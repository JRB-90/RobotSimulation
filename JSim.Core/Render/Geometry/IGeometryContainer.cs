using System;

namespace JSim.Core.Render
{
    /// <summary>
    /// Interface to define a holder and maintainer of a geometry tree.
    /// </summary>
    public interface IGeometryContainer
    {
        /// <summary>
        /// Gets the root of the geometry tree.
        /// </summary>
        IGeometry Root { get; }
    }
}
