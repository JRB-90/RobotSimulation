using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaApp.Views
{
    public partial class ToolSceneTreeView : UserControl
    {
        public ToolSceneTreeView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
