using JSim.Core.SceneGraph;

namespace JSimControlGallery.Models
{
    internal class SceneEntityModel : SceneObjectModel
    {
        public SceneEntityModel(ISceneEntity sceneEntity)
          :
            base (sceneEntity)
        {
            SceneEntity = sceneEntity;
        }

        public ISceneEntity SceneEntity { get; }

        public override string Icon =>
            "fa-light fa-object-ungroup";
    }
}
