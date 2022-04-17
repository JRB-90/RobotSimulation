using JSim.Core.SceneGraph;

namespace JSim.Avalonia.Models
{
    internal class SceneObjectModel : SceneObjectModelBase
    {
        public SceneObjectModel(ISceneObject sceneObject)
          :
            base(
                sceneObject,
                "")
        {
        }
    }
}
