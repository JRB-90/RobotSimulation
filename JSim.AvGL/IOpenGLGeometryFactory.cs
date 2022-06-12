using JSim.Core.Common;
using JSim.Core.Render;

namespace JSim.AvGL
{
    /// <summary>
    /// Interface for all OpenGL geometry factories.
    /// </summary>
    public interface IOpenGLGeometryFactory
    {
        /// <summary>
        /// Creates an OpenGL compatible geometry object.
        /// </summary>
        IGeometry CreateGeometry(
            INameRepository nameRepository,
            IGeometryCreator creator,
            IGlContextManager glContextManager,
            IGeometry? parentGeometry
        );

        /// <summary>
        /// Destroys an OpenGL compatible geometry object.
        /// </summary>
        void Destroy(IGeometry geometry);
    }
}
