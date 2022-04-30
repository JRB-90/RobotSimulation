using Dock.Model.ReactiveUI.Controls;
using JSim.Avalonia.ViewModels;

namespace AvaloniaApp.ViewModels
{
    public class ToolSceneObjectDataViewModel : Tool
    {
        public ToolSceneObjectDataViewModel(
            string id,
            string title,
            SceneObjectViewModel sceneObjectVM)
        {
            Id = id;
            Title = title;
            SceneObjectVM = sceneObjectVM;
        }

        public SceneObjectViewModel SceneObjectVM { get; }
    }
}
