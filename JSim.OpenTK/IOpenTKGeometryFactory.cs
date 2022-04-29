using JSim.Core.Common;
using JSim.Core.Render;

namespace JSim.OpenTK
{
    /// <summary>
    /// Interface for all opentk geometry factories.
    /// </summary>
    public interface IOpenTKGeometryFactory
    {
        IGeometry CreateGeometry(
            INameRepository nameRepository,
            IGeometryCreator creator,
            IGlContextManager glContextManager,
            IGeometry? parentGeometry
        );

        void Destroy(IGeometry geometry);
    }
}
