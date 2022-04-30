using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaApp.Views
{
    public partial class View3DView : UserControl
    {
        public View3DView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
