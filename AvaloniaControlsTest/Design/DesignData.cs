using Avalonia.Controls;
using AvaloniaControlsTest.ViewModels;
using JSim.Avalonia.Shared;

namespace AvaloniaControlsTest.Design
{
    internal static class DesignData
    {
        static DesignData()
        {
            var window = new Window();
            var inputManager = new InputManager(window);
            var dialogManager = new DialogManager(window);

            MainWindowVM =
                new MainWindowViewModel(
                    inputManager,
                    dialogManager
                );
        }

        public static MainWindowViewModel MainWindowVM { get; }
    }
}
