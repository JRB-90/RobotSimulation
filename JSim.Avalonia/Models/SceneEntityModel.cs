using JSim.Core.SceneGraph;

namespace JSim.Avalonia.Models
{
    internal class SceneEntityModel : SceneObjectModelBase
    {
        readonly ISceneEntity entity;

        public SceneEntityModel(ISceneEntity entity)
          :
            base(
                entity,
                "VectorSquare"
            )
        {
            this.entity = entity;
        }
    }
}
