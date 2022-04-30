using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaApp.Views
{
    public partial class ToolSceneObjectDataView : UserControl
    {
        public ToolSceneObjectDataView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
