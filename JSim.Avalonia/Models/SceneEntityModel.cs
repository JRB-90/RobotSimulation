using JSim.Core.SceneGraph;

namespace JSim.Avalonia.Models
{
    internal class SceneEntityModel : SceneObjectModelBase
    {
        public SceneEntityModel(ISceneEntity entity)
          :
            base(
                entity,
                "VectorSquare"
            )
        {
            Entity = entity;
        }

        public ISceneEntity Entity { get; }
    }
}
