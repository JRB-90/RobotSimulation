using Dock.Model.ReactiveUI.Controls;
using JSim.Avalonia.ViewModels;

namespace AvaloniaApp.ViewModels
{
    public class ToolSceneTreeViewModel : Tool
    {
        public ToolSceneTreeViewModel(
            string id,
            string title,
            SceneTreeViewModel sceneTreeVM)
        {
            Id = id;
            Title = title;
            SceneTreeVM = sceneTreeVM;
        }

        public SceneTreeViewModel SceneTreeVM { get; }
    }
}
