using JSim.Core.SceneGraph;

namespace JSim.Av.Models
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
