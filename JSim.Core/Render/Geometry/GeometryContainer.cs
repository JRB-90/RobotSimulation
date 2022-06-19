using JSim.Core.Common;
using JSim.Core.Maths;

namespace JSim.Core.Render
{
    /// <summary>
    /// Standard implementation of a geometry container.
    /// </summary>
    public class GeometryContainer : IGeometryContainer, IMessageHandler<GeometryModified>
    {
        readonly IMessageCollator messageCollator;
        readonly IGeometryCreator creator;

        public GeometryContainer(
            IMessageCollator messageCollator,
            IGeometryCreatorFactory geometryCreatorFactory)
        {
            this.messageCollator = messageCollator;
            messageCollator.Subscribe(this);
            creator = geometryCreatorFactory.CreateGeometryCreator(messageCollator);
            Root = creator.CreateGeometry(null);
            Root.Name = "Root";
        }

        public IGeometry Root { get; }

        public event GeometryTreeModifiedEventHandler? GeometryTreeModified;

        public void UpdateWorldPosition(Transform3D worldPositionOfParent)
        {
            Root.RecalculateWorldPosition(worldPositionOfParent);
        }

        public void Handle(GeometryModified message)
        {
            GeometryTreeModified?.Invoke(this, new GeometryTreeModifiedEventArgs());
        }
    }
}
