using JSim.Core.Common;

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
        readonly IMessageCollator messageCollator;

        public GeometryCreator(
            ILogger logger,
            INameRepositoryFactory nameRepositoryFactory,
            IGeometryFactory geometryFactory,
            IMessageCollator messageCollator)
        {
            this.logger = logger;
            this.geometryFactory = geometryFactory;
            this.messageCollator = messageCollator;
            nameRepository = nameRepositoryFactory.CreateNameRepository();
            logger.Log("GeometryCreator initialised", LogLevel.Debug);
        }

        public void Dispose()
        {
            logger.Log("GeometryCreator disposed", LogLevel.Debug);
        }

        /// <summary>
        /// Creates a new IGeometry object.
        /// </summary>
        /// <param name="parentGeometry">Parent to attach this geometry node to.</param>
        /// <returns>Implementation specific geometry implementation.</returns>
        public IGeometry CreateGeometry(IGeometry? parentGeometry)
        {
            return
                geometryFactory.CreateGeometry(
                    nameRepository,
                    messageCollator,
                    this,
                    parentGeometry
                );
        }
    }
}
