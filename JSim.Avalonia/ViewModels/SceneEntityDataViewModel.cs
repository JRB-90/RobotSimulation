using JSim.Core.SceneGraph;

namespace JSim.Avalonia.ViewModels
{
    internal class SceneEntityDataViewModel : ViewModelBase, ISceneObjectTypeDataVM
    {
        readonly ISceneEntity entity;

        public SceneEntityDataViewModel(ISceneEntity entity)
        {
            this.entity = entity;
        }
    }
}
