using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaApp.Models;
using AvaloniaApp.ViewModels;
using AvaloniaApp.Views;
using Dock.Model.Core;

namespace AvaloniaApp
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
            {
                var mainWindow = new MainWindow();
                var appModel = new JSimAppModel(mainWindow);
                var factory = new DockFactory(appModel);
                var layout = factory.CreateLayout();
                factory.InitLayout(layout);

                var mainWindowViewModel = new MainWindowViewModel()
                {
                    Factory = factory,
                    Layout = layout
                };

                mainWindow.DataContext = mainWindowViewModel;

                mainWindow.Closing += (_, _) =>
                {
                    if (layout is IDock dock)
                    {
                        if (dock.Close.CanExecute(null))
                        {
                            dock.Close.Execute(null);
                        }
                    }
                };

                desktopLifetime.MainWindow = mainWindow;

                desktopLifetime.Exit += (_, _) =>
                {
                    if (layout is IDock dock)
                    {
                        if (dock.Close.CanExecute(null))
                        {
                            dock.Close.Execute(null);
                        }
                    }
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
