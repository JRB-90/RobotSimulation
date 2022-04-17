using JSim.Core.SceneGraph;

namespace JSim.Avalonia.ViewModels
{
    internal class SceneAssemblyDataViewModel : ViewModelBase, ISceneObjectTypeDataVM
    {
        readonly ISceneAssembly assembly;

        public SceneAssemblyDataViewModel(ISceneAssembly assembly)
        {
            this.assembly = assembly;
        }
    }
}
