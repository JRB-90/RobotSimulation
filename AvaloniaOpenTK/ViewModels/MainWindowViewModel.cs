using Avalonia.Media;
using Avalonia.OpenTK;
using JSim.Avalonia.Controls;

namespace AvaloniaOpenTK.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            var rootContext = AvaloniaOpenTKIntegration.CreateCompatibleContext(null);
            var contextFactory = () => AvaloniaOpenTKIntegration.CreateCompatibleContext(rootContext);
            var subContext = contextFactory.Invoke();

            GraphicsControl1 = new OpenTKControl() { Name = "C1", ClearColor = Brushes.PaleTurquoise };
            GraphicsControl2 = new OpenTKControl() { Name = "C2", ClearColor = Brushes.PaleGoldenrod };
            GraphicsControl3 = new OpenTKControl() { Name = "C3", ClearColor = Brushes.PaleGreen };
            GraphicsControl4 = new OpenTKControl() { Name = "C4", ClearColor = Brushes.PaleVioletRed };
        }

        public OpenTKControl GraphicsControl1 { get; }

        public OpenTKControl GraphicsControl2 { get; }

        public OpenTKControl GraphicsControl3 { get; }

        public OpenTKControl GraphicsControl4 { get; }
    }
}
