using JSim.Core.SceneGraph;

namespace JSim.Core.Render
{
    /// <summary>
    /// Interface for all geometry factories.
    /// </summary>
    public interface IGeometryFactory : IDisposable
    {
        IGeometry CreateGeometry(
            INameRepository nameRepository,
            IGeometryCreator creator,
            IGeometry? parentGeometry
        );

        /// <summary>
        /// Releases a IGeometry object.
        /// </summary>
        /// <param name="geometry">IGeometry object to release.</param>
        void Destroy(IGeometry geometry);
    }
}
