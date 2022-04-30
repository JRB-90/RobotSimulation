using JSim.Core.Common;

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

        void Destroy(IGeometry geometry);
    }
}
