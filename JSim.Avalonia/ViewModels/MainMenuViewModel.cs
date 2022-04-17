using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using JSim.Avalonia.Shared;
using JSim.Core;

namespace JSim.Avalonia.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        readonly ISimApplication app;
        readonly DialogManager dialog;

        public MainMenuViewModel(
            ISimApplication app,
            DialogManager dialog)
        {
            this.app = app;
            this.dialog = dialog;
        }

        public void NewScene()
        {
            app.SceneManager.NewScene();
        }

        public async Task LoadScene()
        {
            var filters =
                new List<FileDialogFilter>()
                {
                    new FileDialogFilter()
                    {
                        Name = "Scene Files (.jsc)",
                        Extensions = new List<string>() { "jsc" }
                    },
                    new FileDialogFilter()
                    {
                        Name = "All Files (.*)",
                        Extensions = new List<string>() { "*" }
                    }
                };

            var files =
                await dialog.ShowOpenFileDialog(
                    "Open Scene File",
                    filters
                );

            if (files != null)
            {
                if (files.Length > 0)
                {
                    app.SceneManager.LoadScene(files[0]);
                }
            }
        }

        public async Task SaveScene()
        {
            var filters =
                new List<FileDialogFilter>()
                {
                    new FileDialogFilter()
                    {
                        Name = "Scene Files (.jsc)",
                        Extensions = new List<string>() { "jsc" }
                    },
                    new FileDialogFilter()
                    {
                        Name = "All Files (.*)",
                        Extensions = new List<string>() { "*" }
                    }
                };

            var file =
                await dialog.ShowSaveFileDialog(
                    "Save Scene File",
                    filters
                );

            if (file != null)
            {
                app.SceneManager.SaveScene(file);
            }
        }

        public void Exit()
        {
            if (Application.Current != null)
            {
                if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
                {
                    lifetime.Shutdown(0);
                }
            }

            Environment.Exit(1);
        }
    }
}
