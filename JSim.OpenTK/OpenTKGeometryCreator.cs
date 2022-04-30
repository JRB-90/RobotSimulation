using JSim.Core;
using JSim.Core.Common;
using JSim.Core.Render;

namespace JSim.OpenTK
{
    /// <summary>
    /// OpenTK compatible implementation of a geometry creator.
    /// Provides correct dependencies when creating OpenTK geometry objects.
    /// </summary>
    public class OpenTKGeometryCreator : IGeometryCreator
    {
        readonly INameRepository nameRepository;
        readonly IOpenTKGeometryFactory geometryFactory;
        readonly IGlContextManager glContextManager;

        public OpenTKGeometryCreator(
            INameRepositoryFactory nameRepositoryFactory,
            IOpenTKGeometryFactory geometryFactory,
            IGlContextManager glContextManager)
        {
            this.geometryFactory = geometryFactory;
            this.glContextManager = glContextManager;
            nameRepository = nameRepositoryFactory.CreateNameRepository();
        }

        /// <summary>
        /// Disposes this object.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Creates a new OpenTKGeometry object.
        /// </summary>
        /// <param name="parentGeometry">Parent to attach this geometry node to.</param>
        /// <returns>OpenTKGeometry object.</returns>
        public IGeometry CreateGeometry(IGeometry? parentGeometry)
        {
            return
                geometryFactory.CreateGeometry(
                    nameRepository,
                    this,
                    glContextManager,
                    parentGeometry
                );
        }
    }
}
