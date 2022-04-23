using JSim.Core.SceneGraph;

namespace JSim.Core.Render
{
    /// <summary>
    /// Standard implementation of a geometry creator.
    /// </summary>
    public class GeometryCreator : IGeometryCreator
    {
        readonly ILogger logger;
        readonly INameRepository nameRepository;
        readonly IGeometryFactory geometryFactory;

        public GeometryCreator(
            ILogger logger,
            INameRepositoryFactory nameRepositoryFactory,
            IGeometryFactory geometryFactory)
        {
            this.logger = logger;
            this.geometryFactory = geometryFactory;
            nameRepository = nameRepositoryFactory.CreateNameRepository();
            logger.Log("DummyGeometryCreator initialised", LogLevel.Debug);
        }

        public void Dispose()
        {
            logger.Log("DummyGeometryCreator disposed", LogLevel.Debug);
        }

        /// <summary>
        /// Creates a new dummy geometry object.
        /// </summary>
        /// <param name="parentGeometry"></param>
        /// <returns></returns>
        public IGeometry CreateGeometry(IGeometry? parentGeometry)
        {
            return
                geometryFactory.CreateGeometry(
                    nameRepository,
                    this,
                    parentGeometry
                );
        }
    }
}
