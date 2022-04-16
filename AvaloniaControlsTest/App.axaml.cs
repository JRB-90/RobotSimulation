using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaControlsTest.ViewModels;
using AvaloniaControlsTest.Views;
using JSim.Avalonia.Shared;

namespace AvaloniaControlsTest
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();

                var inputManager = new InputManager(desktop.MainWindow);
                var dialogMAnager = new DialogManager(desktop.MainWindow);

                desktop.MainWindow.DataContext =
                    new MainWindowViewModel(
                        inputManager,
                        dialogMAnager
                    );
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
