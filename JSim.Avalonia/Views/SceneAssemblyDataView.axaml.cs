using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JSim.Avalonia.Views
{
    public partial class SceneAssemblyDataView : UserControl
    {
        public SceneAssemblyDataView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
