using AvaloniaApp.Controls;
using Dock.Model.Core;
using JSim.Avalonia.ViewModels;
using ReactiveUI;

namespace AvaloniaApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public IFactory? Factory
        {
            get => factory;
            set => this.RaiseAndSetIfChanged(ref factory, value);
        }

        public IDock? Layout
        {
            get => layout;
            set => this.RaiseAndSetIfChanged(ref layout, value);
        }

        public string? CurrentView
        {
            get => currentView;
            set => this.RaiseAndSetIfChanged(ref currentView, value);
        }

        public JSimTitleBar? TitleBar
        {
            get => titleBar;
            set => this.RaiseAndSetIfChanged(ref titleBar, value);
        }

        private IFactory? factory;
        private IDock? layout;
        private string? currentView;
        private JSimTitleBar? titleBar;
    }
}
