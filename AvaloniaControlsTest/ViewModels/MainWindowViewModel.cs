using JSim.Avalonia.ViewModels;
using ReactiveUI;

namespace AvaloniaControlsTest.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            doubleValue = -1234.505;
        }

        public double DoubleValue
        {
            get => doubleValue;
            set => this.RaiseAndSetIfChanged(ref doubleValue, value);
        }

        private double doubleValue;
    }
}
