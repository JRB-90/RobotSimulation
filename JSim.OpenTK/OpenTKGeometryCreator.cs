using JSim.Core;
using JSim.Core.Common;
using JSim.Core.Render;

namespace JSim.OpenTK
{
    public class OpenTKGeometryCreator : IGeometryCreator
    {
        readonly ILogger logger;
        readonly INameRepository nameRepository;
        readonly IOpenTKGeometryFactory geometryFactory;
        readonly IGlContextManager glContextManager;

        public OpenTKGeometryCreator(
            ILogger logger,
            INameRepositoryFactory nameRepositoryFactory,
            IOpenTKGeometryFactory geometryFactory,
            IGlContextManager glContextManager)
        {
            this.logger = logger;
            this.geometryFactory = geometryFactory;
            this.glContextManager = glContextManager;
            nameRepository = nameRepositoryFactory.CreateNameRepository();
            logger.Log("GeometryCreator initialised", LogLevel.Debug);
        }

        public void Dispose()
        {
            logger.Log("GeometryCreator disposed", LogLevel.Debug);
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
