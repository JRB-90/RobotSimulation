using Avalonia.Controls;
using Dock.Model.ReactiveUI.Controls;

namespace AvaloniaApp.ViewModels
{
    public class View3DViewModel : Document
    {
        public View3DViewModel(
            string id,
            string title,
            Control view3DControl)
        {
            Id = id;
            Title = title;
            View3DVM = view3DControl;
        }

        public Control? View3DVM { get; }
    }
}
