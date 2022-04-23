using JSim.Core.SceneGraph;

namespace JSim.Core.Render
{
    public class DummyGeometry : GeometryBase
    {
        readonly ILogger logger;

        public DummyGeometry(
            ILogger logger,
            INameRepository nameRepository,
            IGeometryCreator creator)
          :
            base(
                nameRepository, 
                creator)
        {
            this.logger = logger;
        }

        public DummyGeometry(
            ILogger logger,
            INameRepository nameRepository,
            IGeometryCreator creator,
            IGeometry? parentGeometry)
          :
            base(
                nameRepository,
                creator,
                parentGeometry)
        {
            this.logger = logger;
        }

        protected override void Rebuild()
        {
            logger.Log($"Rebuilding Geometry of: {Name}", LogLevel.Info);
        }
    }
}
