using JSim.Core.Common;

namespace JSim.Core.Render
{
    public class DummyGeometry : GeometryBase
    {
        readonly ILogger logger;

        public DummyGeometry(
            ILogger logger,
            INameRepository nameRepository,
            IMessageCollator messageCollator,
            IGeometryCreator creator)
          :
            base(
                nameRepository,
                messageCollator,
                creator)
        {
            this.logger = logger;
        }

        public DummyGeometry(
            ILogger logger,
            INameRepository nameRepository,
            IMessageCollator messageCollator,
            IGeometryCreator creator,
            IGeometry? parentGeometry)
          :
            base(
                nameRepository,
                messageCollator,
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
