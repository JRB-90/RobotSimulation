using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JSim.Avalonia.Views
{
    public partial class SceneEntityDataView : UserControl
    {
        public SceneEntityDataView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
