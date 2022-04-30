using JSim.Core.Common;
using JSim.Core.Render;

namespace JSim.OpenTK
{
    /// <summary>
    /// Interface for all OpenTK geometry factories.
    /// </summary>
    public interface IOpenTKGeometryFactory
    {
        /// <summary>
        /// Creates an OpenTK compatible geometry object.
        /// </summary>
        IGeometry CreateGeometry(
            INameRepository nameRepository,
            IGeometryCreator creator,
            IGlContextManager glContextManager,
            IGeometry? parentGeometry
        );

        /// <summary>
        /// Destroys an OpenTK compatible geometry object.
        /// </summary>
        void Destroy(IGeometry geometry);
    }
}
