using JSim.Core.Common;
using JSim.Core.Render;

namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Standard implementation of a Scene entity.
    /// </summary>
    public class SceneEntity : SceneObjectBase, ISceneEntity
    {
        public SceneEntity(
            INameRepository nameRepository,
            IMessageCollator collator,
            IGeometryContainer geometryContainer,
            ISceneAssembly parentAssembly,
            string nameRoot = "Entity")
          :
            base(
                nameRepository,
                collator,
                parentAssembly,
                nameRoot)
        {
            GeometryContainer = geometryContainer;
            GeometryContainer.GeometryTreeModified += OnGeometryTreeModified;
            this.SceneObjectModified += OnEntityModified;
        }

        public SceneEntity(
            INameRepository nameRepository,
            IMessageCollator collator,
            IGeometryContainer geometryContainer,
            Guid id,
            string name,
            ISceneAssembly parentAssembly)
          :
            base(
                nameRepository,
                collator,
                id,
                name,
                parentAssembly)
        {
            GeometryContainer = geometryContainer;
            GeometryContainer.GeometryTreeModified += OnGeometryTreeModified;
            this.SceneObjectModified += OnEntityModified;
        }

        public IGeometryContainer GeometryContainer { get; }

        private void OnGeometryTreeModified(object sender, GeometryTreeModifiedEventArgs e)
        {
            RaiseSceneObjectChangedEvent();
        }

        private void OnEntityModified(object sender, SceneObjectModifiedEventArgs e)
        {
            GeometryContainer.UpdateWorldPosition(WorldFrame);
        }
    }
}
