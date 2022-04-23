using JSim.Core.SceneGraph;

namespace JSim.Core.Render
{
    public class DummyGeometry : GeometryBase
    {
        readonly ILogger logger;

        public DummyGeometry(
            ILogger logger,
            INameRepository nameRepository)
          :
            base(nameRepository)
        {
            this.logger = logger;
        }

        protected override void Rebuild()
        {
            logger.Log($"Rebuilding Geometry of: {Name}", LogLevel.Info);
        }
    }
}
